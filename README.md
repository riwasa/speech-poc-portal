# speech-poc-portal
This repo contains a Blazor version of speech PoC portal.

## Prerequisites

- Visual Studio 2022
- .NET 8

## Storage Account CORS rule

In the Azure portal, go to the "Settings / Resource sharing (CORS)" blade. Under "Blob service", add a new CORS rule, with the following values:

- Allowed origins: *
- Allowed methods: select all 8
- Allowed headers: *
- Exposed headers: *
- Max age: 120 

Click Save to save the new rule.

## Local app settings

When running locally, you can create an appsettings.Development.json file, with the following contents:

```
{
  "ConnectionStrings": {
    "StorageAccount": "<storage-account-connection-string>"
  },
  "OutputContainer": "<name-of-the-output-container>",
  "AudioContainer": "<name-of-the-audio-file-container>",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

Note that appsettings.Development.json is in the .gitignore file and is not added to the repo.

## App Service configuration

### Application settings

Two application settings are required.

1. AudioContainer
- Name of the container in the Storage Account containing .wav audio files.
- e.g. aoai-processed

2. OutputContainer
- Name of the container in the Storage Account containing .json files with the output from Azure OpenAI for each audio file.
- e.g. aoai-json-result-output

#### Connection strings

One connection string is required.

1. StorageAccount
- Value is the account connection string (not a SAS token) for the Storage Account containing the AudioContainer and the OutputContainer.
- The connection string type is Custom.

## Project files

Main files of interest in the project are:

| File                             | Description                                                                            |
|----------------------------------|----------------------------------------------------------------------------------------|
| Components/Pages/CallPanel.razor | Flyout panel showing the details of a call.                                            |
| Components/Pages/Home.razor      | Home page showing a pageable grid of recordings.                                       |
| Entities/Call.cs                 | Details about a call, matching the structure of the .json file for an audio recording. |
| Entities/CallSummary.cs          | Summary of call, displayed on Home.razor.                                              |
| Helpers/BlobHelper.cs            | Contains methods to enumerate containers and download blobs.                           |