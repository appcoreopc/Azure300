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


                // Storage account credentials
        private const string StorageAccountName = "";
        private const string StorageAccountKey = "";



        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            CloudBlobClient blobClient = CreateCloudBlobClient(StorageAccountName, StorageAccountKey);


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
    }
}
