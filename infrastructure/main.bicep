targetScope = 'subscription'

param systemName string

@allowed([
  'dev'
  'tst'
  'prd'
])
param environmentName string
param location string = deployment().location
param locationAbbreviation string
param containerVersion string = '1.0.0'
param developersGroup string
param integrationEnvironment object
param acrLoginServer string
param acrUsername string
@secure()
param acrPassword string
param corsHostnames array

var apiResourceGroupName = toLower('${systemName}-${environmentName}-${locationAbbreviation}')

var storageAccountQueues = [
  'vouchersToValidate'
]

var storageAccountTables = [
  'vouchers'
]

resource apiResourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: apiResourceGroupName
  location: location
}

module vouchersResourcesModule 'resources.bicep' = {
  name: 'vouchersResourcesModule'
  scope: apiResourceGroup
  params: {
    defaultResourceName: apiResourceGroupName
    location: location
    storageAccountQueues: storageAccountQueues
    storageAccountTables: storageAccountTables
    containerAppName: systemName
    containerVersion: containerVersion
    integrationEnvironment: integrationEnvironment
    environmentName: environmentName
    developersGroup: developersGroup
    acrLoginServer: acrLoginServer
    acrUsername: acrUsername
    acrPassword: acrPassword
    corsHostnames: corsHostnames
  }
}
