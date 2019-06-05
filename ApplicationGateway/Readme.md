

Load balance using application gateways 


1. path based routing - routing based a /content/video or /content/audio 

2. multiple site hosting 


Uses round robin to load balance request. Worked on layer 7, works with hostname and path, as compare to Azure Load Balancer works on layer 4 - distributing traffic based on ip address 

Front-end IP address

Listeners

Routing rules

Back-end pools

Web application firewall



az network vnet create \
  --resource-group [sandbox resource group] \
  --name vehicleAppVnet \
  --address-prefix 10.0.0.0/16 \
  --subnet-name webServerSubnet \
  --subnet-prefix 10.0.1.0/24


git clone https://github.com/MicrosoftDocs/mslearn-load-balance-web-traffic-with-application-gateway/ module-files



az vm create \
  --resource-group 502d0e8c-3106-4fe2-85b1-56006e30cc8c \
  --name webServer1 \
  --image Canonical:UbuntuServer:16.04.0-LTS:16.04.201610200 \
  --admin-username azureuser \
  --generate-ssh-keys \
  --vnet-name vehicleAppVnet \
  --subnet webServerSubnet \
  --public-ip-address "" \
  --nsg "" \
  --custom-data module-files/scripts/vmconfig.sh \
  --no-wait

az vm create \
  --resource-group 502d0e8c-3106-4fe2-85b1-56006e30cc8c \
  --name webServer2 \
  --image Canonical:UbuntuServer:16.04.0-LTS:16.04.201610200 \
  --admin-username azureuser \
  --generate-ssh-keys \
  --vnet-name vehicleAppVnet \
  --subnet webServerSubnet \
  --public-ip-address "" \
  --nsg "" \
  --custom-data module-files/scripts/vmconfig.sh



az vm list \
  --resource-group 502d0e8c-3106-4fe2-85b1-56006e30cc8c \
  --show-details \
  --output table


APPSERVICE="licenserenewal$RANDOM"

az appservice plan create \
    --resource-group 502d0e8c-3106-4fe2-85b1-56006e30cc8c \
    --name vehicleAppServicePlan \
    --sku S1

az appservice plan create \
    --resource-group 502d0e8c-3106-4fe2-85b1-56006e30cc8c \
    --name vehicleAppServicePlan \
    --sku S1










