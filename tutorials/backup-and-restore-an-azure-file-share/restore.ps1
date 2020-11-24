$storageSubscription = ""
$storageAccountName = ""
$storageAccountKey = ""
$storageRessourceGroup = ""
$storageFileShareName = ""

$expiry = (Get-Date).AddHours(12).ToString("yyyy-MM-ddTHH:mmZ")

$sasToken = az storage share generate-sas `
    --subscription $storageSubscription `
    --account-key $storageAccountKey `
    --account-name $storageAccountName `
    --name $storageFileShareName `
    --expiry $expiry `
    --permissions dlrw

azcopy rm "https://${storageAccountName}.file.core.windows.net/${storageFileShareName}?${sasToken}" --recursive=true
azcopy copy "./$storageFileShareName/*" "https://${storageAccountName}.file.core.windows.net/${storageFileShareName}/?${sasToken}" --recursive 

az storage account revoke-delegation-keys `
    --subscription $storageSubscription `
    --name $storageAccountName `
    --resource-group $storageRessourceGroup