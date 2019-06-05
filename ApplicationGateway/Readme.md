

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


################  section 2 ##################



create a new subnet


az network vnet subnet create \
  --resource-group 502d0e8c-3106-4fe2-85b1-56006e30cc8c \
  --vnet-name vehicleAppVnet  \
  --name appGatewaySubnet \
  --address-prefixes 10.0.0.0/24


get a public ip 

az network public-ip create \
  --resource-group 502d0e8c-3106-4fe2-85b1-56006e30cc8c \
  --name appGatewayPublicIp \
  --sku Standard \
  --dns-name vehicleapp${RANDOM}

create application gateway 


az network application-gateway create \
--resource-group 502d0e8c-3106-4fe2-85b1-56006e30cc8c \
--name vehicleAppGateway \
--sku WAF_v2 \
--capacity 2 \
--vnet-name vehicleAppVnet \
--subnet appGatewaySubnet \
--public-ip-address appGatewayPublicIp \
--http-settings-protocol Http \
--http-settings-port 8080 \
--frontend-port 8080

Get ip address 

WEBSERVER1IP="$(az vm list-ip-addresses \
  --resource-group 502d0e8c-3106-4fe2-85b1-56006e30cc8c \
  --name webServer1 \
  --query [0].virtualMachine.network.privateIpAddresses[0] \
  --output tsv)"

WEBSERVER2IP="$(az vm list-ip-addresses \
  --resource-group 502d0e8c-3106-4fe2-85b1-56006e30cc8c \
  --name webserver2 \
  --query [0].virtualMachine.network.privateIpAddresses[0] \
  --output tsv)"



# create our pool / setup our backend pool server 


az network application-gateway address-pool create \
  --gateway-name vehicleAppGateway \
  --resource-group 502d0e8c-3106-4fe2-85b1-56006e30cc8c \
  --name vmPool \
  --servers $WEBSERVER1IP $WEBSERVER2IP


## Add app service to our pool

az network application-gateway address-pool create \
    --resource-group 502d0e8c-3106-4fe2-85b1-56006e30cc8c \
    --gateway-name vehicleAppGateway \
    --name appServicePool \
    --servers $APPSERVICE.azurewebsites.net



Create from end 

az network application-gateway frontend-port create \
    --resource-group 502d0e8c-3106-4fe2-85b1-56006e30cc8c \
    --gateway-name vehicleAppGateway \
    --name port80 \
    --port 80


# Create listener 

az network application-gateway http-listener create \
    --resource-group 502d0e8c-3106-4fe2-85b1-56006e30cc8c \
    --name vehicleListener \
    --frontend-port port80 \
    --gateway-name vehicleAppGateway



###  Add health probe 


az network application-gateway probe create \
    --resource-group 502d0e8c-3106-4fe2-85b1-56006e30cc8c \
    --gateway-name vehicleAppGateway \
    --name customProbe \
    --path / \
    --interval 15 \
    --threshold 3 \
    --timeout 10 \
    --protocol Http \
    --host-name-from-http-settings true


az network application-gateway probe create \
    --resource-group 502d0e8c-3106-4fe2-85b1-56006e30cc8c \
    --gateway-name vehicleAppGateway \
    --name customProbe \
    --path / \
    --interval 15 \
    --threshold 3 \
    --timeout 10 \
    --protocol Http \
    --host-name-from-http-settings true


az network application-gateway http-settings update \
    --resource-group 502d0e8c-3106-4fe2-85b1-56006e30cc8c \
    --gateway-name vehicleAppGateway \
    --name appGatewayBackendHttpSettings \
    --host-name-from-backend-pool true \
    --port 80 \
    --probe customProbe



Setup path routing 

az network application-gateway url-path-map create \
    --resource-group 502d0e8c-3106-4fe2-85b1-56006e30cc8c \
    --gateway-name vehicleAppGateway \
    --name urlPathMap \
    --paths /VehicleRegistration/* \
    --http-settings appGatewayBackendHttpSettings \
    --address-pool vmPool



az network application-gateway url-path-map rule create \
    --resource-group 502d0e8c-3106-4fe2-85b1-56006e30cc8c \
    --gateway-name vehicleAppGateway \
    --name appServiceUrlPathMap \
    --paths /LicenseRenewal/* \
    --http-settings appGatewayBackendHttpSettings \
    --address-pool appServicePool \
    --path-map-name urlPathMap



### This becomes our routing rule


az network application-gateway rule create \
    --resource-group 502d0e8c-3106-4fe2-85b1-56006e30cc8c \
    --gateway-name vehicleAppGateway \
    --name appServiceRule \
    --http-listener vehicleListener \
    --rule-type PathBasedRouting \
    --address-pool appServicePool \
    --url-path-map urlPathMap


### Delete route rules 


az network application-gateway rule delete \
    --resource-group 502d0e8c-3106-4fe2-85b1-56006e30cc8c \
    --gateway-name vehicleAppGateway \
    --name rule1



