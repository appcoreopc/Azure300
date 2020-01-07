
Creating an Azure container regustry


az acr create --name myregistry --resource-group mygroup --sku standard --admin-enabled true (create a container)



docker login myregistry.azurecr.io - login into azure container registry


Next we will use a container instance to run it. 

To provision one, we just have to do the following :- 


az container create --resource-group mygroup --name myinstance --image myregistryjerwo.azurecr.io/myapp:latest --dns-name-label mydnsname --registry-username myregistryjerwo --registry-password g2OKgL5BnPaRNu3lnf+rNK66mZxOtgCc
