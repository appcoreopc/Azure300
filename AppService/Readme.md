
App service plan decides that feature / treatment you get 

Free tier -  10 free app, shared, 1g memory
Shared - 100 app, shared
Basic - scale up to 3 instance
Standard - unlimited app, scale up to 10
Premium - 20 instance 
Isolated tier - runs dedicated vpn 


Setting up service plan
- basically going through and selecting the runtime, 
plan etc. 


build your app using common commands like 

dotnet build 
dotnet 


az webapp deployment source config-zip --src website.zip --name <your-webapp-name> --resource-group 1455cbdd-8865-41a8-9773-8ba550f80eb8



Goto App Service -> Monitoring -> Metrics 

add common metrics, cpu response time, http errors, average request time 



To scale out 

-> App Service -> Scale out 

To scale up 

-> App service -> Scale up 


