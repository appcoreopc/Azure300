using Microsoft.Azure.Batch;
using Microsoft.Azure.Batch.Auth;
using Microsoft.Azure.Batch.Common;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;


namespace BatchApp
{
    class Program
    {        
        private const string BatchAccountName = "";
        private const string BatchAccountKey = "";

        private const string BatchAccountUrl = "";



                // Storage account credentials
        private const string StorageAccountName = "";
        private const string StorageAccountKey = "";


        private const string PoolId = "DotNetQuickstartPool";
        private const string JobId = "DotNetQuickstartJob";
        private const int PoolNodeCount = 2;
        private const string PoolVMSize = "STANDARD_A1_v2";
      
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CloudBlobClient blobClient = CreateCloudBlobClient(StorageAccountName, StorageAccountKey);
           
            var files = UploadWorkFiles(blobClient);

            StartBatchTasks(files);
        }

        private static void StartBatchTasks(List<ResourceFile> inputFiles)
        {
            
            BatchSharedKeyCredentials cred = new BatchSharedKeyCredentials(BatchAccountUrl, BatchAccountName, BatchAccountKey);

            using (BatchClient batchClient = BatchClient.Open(cred))
            {

                  // Create a Windows Server image, VM configuration, Batch pool
                  ImageReference imageReference = CreateImageReference();

                  VirtualMachineConfiguration vmConfiguration = CreateVirtualMachineConfiguration(imageReference);

                  CreateBatchPool(batchClient, vmConfiguration);

                  CreateJob(batchClient);

                  List<CloudTask> tasks = new List<CloudTask>();

                  for (int i = 0; i < inputFiles.Count; i++)
                  {
                        string taskId = String.Format("Task{0}", i);
                        string inputFilename = inputFiles[i].FilePath;
                        string taskCommandLine = String.Format("cmd /c type {0}", inputFilename);

                        CloudTask task = new CloudTask(taskId, taskCommandLine);
                        task.ResourceFiles = new List<ResourceFile> { inputFiles[i] };
                        tasks.Add(task);
                   }    

                    batchClient.JobOperations.AddTask(JobId, tasks);       

                    TimeSpan timeout = TimeSpan.FromMinutes(30);
                    Console.WriteLine("Monitoring all tasks for 'Completed' state, timeout in {0}...", timeout);

                    IEnumerable<CloudTask> addedTasks = batchClient.JobOperations.ListTasks(JobId);
                
                    batchClient.Utilities.CreateTaskStateMonitor().WaitAll(addedTasks, TaskState.Completed, timeout);

                    // List tasks 

                    IEnumerable<CloudTask> completedtasks = batchClient.JobOperations.ListTasks(JobId);

                    foreach (CloudTask task in completedtasks)
                    {
                        string nodeId = String.Format(task.ComputeNodeInformation.ComputeNodeId);
                        Console.WriteLine("Task: {0}", task.Id);
                        Console.WriteLine("Node: {0}", nodeId);
                        Console.WriteLine("Standard out:");
                        Console.WriteLine(task.GetNodeFile(Constants.StandardOutFileName).ReadAsString());
                    }

                    // delete job 

                    batchClient.JobOperations.DeleteJob(JobId);

                    // delete pool

                    batchClient.PoolOperations.DeletePool(PoolId);
            }
        }

        private static void CreateJob(BatchClient batchClient) {


               try
                    {
                        CloudJob job = batchClient.JobOperations.CreateJob();
                        job.Id = JobId;
                        job.PoolInformation = new PoolInformation { PoolId = PoolId };
                        job.Commit();
                    }
                    catch (BatchException be)
                    {
                        // Accept the specific error code JobExists as that is expected if the job already exists
                        if (be.RequestInformation?.BatchError?.Code == BatchErrorCodeStrings.JobExists)
                        {
                            Console.WriteLine("The job {0} already existed when we tried to create it", JobId);
                        }
                        else
                        {
                            throw; // Any other exception is unexpected
                        }
                    }
        }


        private static VirtualMachineConfiguration CreateVirtualMachineConfiguration(ImageReference imageReference)
        {
            return new VirtualMachineConfiguration(
                imageReference: imageReference,
                nodeAgentSkuId: "batch.node.windows amd64");
        }



          private static void CreateBatchPool(BatchClient batchClient, VirtualMachineConfiguration vmConfiguration)
        {
            try
            {
                CloudPool pool = batchClient.PoolOperations.CreatePool(
                    poolId: PoolId,
                    targetDedicatedComputeNodes: PoolNodeCount,
                    virtualMachineSize: PoolVMSize,
                    virtualMachineConfiguration: vmConfiguration);

                pool.Commit();
            }
            catch (BatchException be)
            {
                // Accept the specific error code PoolExists as that is expected if the pool already exists
                if (be.RequestInformation?.BatchError?.Code == BatchErrorCodeStrings.PoolExists)
                {
                    Console.WriteLine("The pool {0} already existed when we tried to create it", PoolId);
                }
                else
                {
                    throw; // Any other exception is unexpected
                }
            }
        }


        private static ImageReference CreateImageReference()
        {
            return new ImageReference(
                publisher: "MicrosoftWindowsServer",
                offer: "WindowsServer",
                sku: "2016-datacenter-smalldisk",
                version: "latest");
        }

        private static  List<ResourceFile> UploadWorkFiles(CloudBlobClient blobClient) {

            const string inputContainerName = "input";
            CloudBlobContainer container = blobClient.GetContainerReference(inputContainerName);       container.CreateIfNotExistsAsync().Wait();

              List<string> inputFilePaths = new List<string>
                {
                    "taskdata0.txt",
                    "taskdata1.txt",
                    "taskdata2.txt"
                };

                // Upload the data files to Azure Storage. This is the data that will be processed by each of the tasks that are
                // executed on the compute nodes within the pool.
                List<ResourceFile> inputFiles = new List<ResourceFile>();

                foreach (string filePath in inputFilePaths)
                {
                    inputFiles.Add(UploadFileToContainer(blobClient, inputContainerName, filePath));
                }

                return  inputFiles;
        }

         private static CloudBlobClient CreateCloudBlobClient(string storageAccountName, string storageAccountKey)
        {
            // Construct the Storage account connection string
            string storageConnectionString =
                $"DefaultEndpointsProtocol=https;AccountName={storageAccountName};AccountKey={storageAccountKey}";

            // Retrieve the storage account
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);

            // Create the blob client
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient;
        }

        private static ResourceFile UploadFileToContainer(CloudBlobClient blobClient, string containerName, string filePath)
        {
            Console.WriteLine("Uploading file {0} to container [{1}]...", filePath, containerName);

            string blobName = Path.GetFileName(filePath);

            filePath = Path.Combine(Environment.CurrentDirectory, filePath);

            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            CloudBlockBlob blobData = container.GetBlockBlobReference(blobName);
            blobData.UploadFromFileAsync(filePath).Wait();

            // Set the expiry time and permissions for the blob shared access signature. 
            // In this case, no start time is specified, so the shared access signature 
            // becomes valid immediately
            SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(2),
                Permissions = SharedAccessBlobPermissions.Read
            };

            // Construct the SAS URL for blob
            string sasBlobToken = blobData.GetSharedAccessSignature(sasConstraints);
            string blobSasUri = String.Format("{0}{1}", blobData.Uri, sasBlobToken);

            return ResourceFile.FromUrl(blobSasUri, filePath);
        }
    }
}
