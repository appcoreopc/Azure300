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



