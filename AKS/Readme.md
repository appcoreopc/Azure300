
Creating an Azure container regustry


az acr create --name myregistry --resource-group mygroup --sku standard --admin-enabled true (create a container)



docker login myregistry.azurecr.io - login into azure container registry