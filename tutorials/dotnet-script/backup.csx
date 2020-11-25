#r "nuget: CliWrap, 3.2.3"

using System;
using CliWrap;
using CliWrap.Buffered;

var storageSubscription = "";
var storageAccountName = "";
var storageAccountKey = "";
var storageRessourceGroup = "";
var storageFileShareName = "";

var expiry = DateTime.Now.AddHours(12).ToString("yyyy-MM-ddTHH:mmZ");
const string AZ = @"C:\Program Files (x86)\Microsoft SDKs\Azure\CLI2\wbin\az.cmd";

var sasTokenCmdResult = await Cli.Wrap(AZ)
    .WithArguments($@"storage share generate-sas --subscription {storageSubscription} --account-key {storageAccountKey} --account-name {storageAccountName} --name {storageFileShareName} --expiry {expiry} --permissions lr")
    .ExecuteBufferedAsync();
var sasToken = sasTokenCmdResult.StandardOutput.Replace("\r\n", "");

if(Directory.Exists(storageFileShareName))
    Directory.Delete(storageFileShareName, true);

var a = await Cli.Wrap(@"azcopy.exe")
    .WithArguments($"copy https://{storageAccountName}.file.core.windows.net/{storageFileShareName}?{sasToken} ./ --recursive")
    .ExecuteAsync();

var b = await Cli.Wrap(AZ)
    .WithArguments($@"storage account revoke-delegation-keys --subscription {storageSubscription} --name {storageAccountName} --resource-group {storageRessourceGroup}")
    .ExecuteAsync();