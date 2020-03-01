Azure300


53 Questions ( 40 MCQ Qs , 1 Case Study - 4 Qs , 9 Lab Simulation Qs )

How to backup different types of resources

https://docs.microsoft.com/en-us/azure/backup/tutorial-sql-backup

AD stuff 

https://docs.microsoft.com/en-us/azure/active-directory/

Resources for hands on lab 

https://docs.microsoft.com/en-sg/learn/browse/?roles=solution-architect&products=azure

az-300-exam-prep
More area to study

VM

https://docs.microsoft.com/en-us/azure/site-recovery/vmware-azure-tutorial

Prepare vm for migrations

https://docs.microsoft.com/en-us/azure/migrate/tutorial-prepare-vmware

Backing up VM for recovery processes

https://docs.microsoft.com/en-gb/azure/backup/backup-overview

VPN






https:


Storage 

-Blob  (
  -append - 195T 
  -block - text or binary file up to ~5T 
  -page - random access until 8T

-File - highly available file shares (SMB - server message block)
-Table
-Queue - 64k message size queue shared between application for throttling reasons. 

Storage account  types 

- Gpv2 - latest and greatest. alsmo more expensive. 

- Gpv1 - cheaper but not latest

- blob storaege accounts - get a flavour of gpv2 but only for blob.

Active directory stuff.


https://docs.microsoft.com/en-us/azure/active-directory/hybrid/how-to-connect-sync-staging-server

Autoscale set

https://docs.microsoft.com/en-us/azure/azure-monitor/platform/autoscale-get-started

Single sign on

https://docs.microsoft.com/en-us/azure/active-directory/manage-apps/what-is-single-sign-on



OS disk size Up to 2,048 GB for generation 1 VMs.
Up to 300 GB for generation 2 VMs.
Data disk VHD size Up to 4,095 GB


### Backup and recovery services for onprem 

Install Recovery Services agent

Download Agent for Windows Server or Windows Client

Download vault credentials to register the server to the vault. Vault credentials will expire after 2 days.

Already downloaded or using the latest Recovery Services Agent

Schedule backup using Recovery Services Agent UI. Learn More

Once the backups are scheduled, you can use backup jobs page to monitor the backups. Browse jobs page

You can also Configure Notifications from alerts page to receive email alerts for backup failures. Browse alerts page






Q1

You may start the lab by clicking the Next button.
You need to create a virtual network named VNET1008 that contains three subnets named subnet0, subnet1, and subnet2. The solution must meet the following requirements:
✑ Connections from any of the subnets to the Internet must be blocked
✑ Connections from the Internet to any of the subnets must be blocked
✑ The number of network security groups (NSGs) and NSG rules must be minimized
What should you do from the Azure portal?



You plan to connect several virtual machines to the VNET01-USEA2 virtual network.
In the Web-RGlod8322489 resource group, you need to create a virtual machine that uses the Standard_B2ms size named Web01 that runs Windows Server
2016. Web01 must be added to an availability set.
What should you do from the Azure portal?


You plan to back up all the Azure virtual machines in your Azure subscription at 02:00 Coordinated Universal Time (UTC) daily.
You need to prepare the Azure environment to ensure that any new virtual machines can be configured quickly for backup. The solution must ensure that all the daily backups performed at 02:00 UTC are stored for only 90 days.
What should you do from your Recovery Services vault on the Azure portal?
 
You plan to allow connections between the VNET01-USEA2 and VNET01-USWE2 virtual networks.
You need to ensure that virtual machines can communicate across both virtual networks by using their private IP address.
The solution must NOT require any virtual network gateways.
What should you do from the Azure portal?

You may start the lab by clicking the Next button.
Your on-premises network uses an IP address range of 131.107.2.0 to 131.107.2.255.
You need to ensure that only devices from the on-premises network can connect to the rg1lod8322490n1 storage account.
What should you do from the Azure portal?


You need to create a container named bios that will host the documents in the storagelod8322489 storage account. The solution must ensure anonymous access and must ensure that users can browse folders in the container.
What should you do from the Azure portal?


You plan to back up all the Azure virtual machines in your Azure subscription at 02:00 Coordinated Universal Time (UTC) daily.
You need to prepare the Azure environment to ensure that any new virtual machines can be configured quickly for backup. The solution must ensure that all the daily backups performed at 02:00 UTC are stored for only 90 days.
What should you do from your Recovery Services vault on the Azure portal?



You plan to connect several virtual machines to the VNET01-USEA2 virtual network.
In the Web-RGlod8322489 resource group, you need to create a virtual machine that uses the Standard_B2ms size named Web01 that runs Windows Server
2016. Web01 must be added to an availability set.
What should you do from the Azure portal?

You may start the lab by clicking the Next button.
You recently created a virtual machine named Web01.
You need to attach a new 80-GB standard data disk named Web01-Disk1 to Web01.
What should you do from the Azure portal?



You plan to allow connections between the VNET01-USEA2 and VNET01-USWE2 virtual networks.
You need to ensure that virtual machines can communicate across both virtual networks by using their private IP address.
The solution must NOT require any virtual network gateways.
What should you do from the Azure portal?



To start the lab -
You may start the lab by clicking the Next button.
You plan to host several secured websites on Web01.
You need to allow HTTPS over TCP port 443 to Web01 and to prevent HTTP over TCP port 80 to Web01.
What should you do from the Azure portal?


Your on-premises network uses an IP address range of 131.107.2.0 to 131.107.2.255.
You need to ensure that only devices from the on-premises network can connect to the rg1lod8322490n1 storage account.
What should you do from the Azure portal?


You plan to store media files in the rg1lod8322490 storage account.
You need to configure the storage account to store the media files. The solution must ensure that only users who have access keys can download the media files and that the files are accessible only over HTTPS.
What should you do from the Azure portal?

Another administrator attempts to establish connectivity between two virtual networks named VNET1 and VNET2. The administrator reports that connections across the virtual networks fail.
You need to ensure that network connections can be established successfully between VNET1 and VNET2 as quickly as possible.
What should you do from the Azure portal?


You plan to configure VM1 to be accessible from the internet.
You need to add a public IP address to the network interface used by VM1.
What should you do from the Azure portal?

