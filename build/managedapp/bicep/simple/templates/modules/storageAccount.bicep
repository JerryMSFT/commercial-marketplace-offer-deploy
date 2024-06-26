
@description('Deployment Location')
param location string

@description('Storage Account Access Tier')
param tier string


var storageAccountName = toLower(format('sa{0}', uniqueString(resourceGroup().id)))


resource st 'Microsoft.Storage/storageAccounts@2022-09-01' = {
  name: storageAccountName

  location: location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
   properties: {
     accessTier: tier
   }
  tags: {
    environment: 'DevTest'
  }
}

output storageAccountId string = st.id
