using System.Text.Json;

using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

/// <summary>
/// Initializes a new instance of the <see cref="BlobHelper"/> class.
/// </summary>
/// <param name="connectionString">Storage Account connection string.</param>
public class BlobHelper(string? connectionString)
{
    /// <summary>
    /// Storage Account connection string.
    /// </summary>
    private readonly string? _connectionString = connectionString;

    /// <summary>
    /// Gets a WAV audio file from Azure Blob Storage.
    /// </summary>
    /// <param name="containerName">Name of the blob container.</param>
    /// <param name="fileName">Name of the blob.</param>
    /// <returns>MemoryStream with the blob contents.</returns>
    public async Task<MemoryStream> GetAudioFileAsync(string? containerName, string? fileName)
    {
        BlobContainerClient blobContainerClient = new (_connectionString, containerName);
        BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

        MemoryStream memoryStream = new ();
        await blobClient.DownloadToAsync(memoryStream);

        return memoryStream;
    }

    /// <summary>
    /// Gets a call file from Azure Blob Storage.
    /// </summary>
    /// <param name="containerName">Name of the blob container.</param>
    /// <param name="fileName">Name of the blob.</param>
    /// <returns>Call file.</returns>
    public async Task<Call?> GetCallAsync(string? containerName, string? fileName)
    {
        BlobContainerClient blobContainerClient = new(_connectionString, containerName);
        BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

        var response = await blobClient.DownloadAsync();

        Call? call = await JsonSerializer.DeserializeAsync<Call>(response.Value.Content,
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        return call;
    }

    /// <summary>
    /// Gets a list of call summaries from Azure Blob Storage.
    /// </summary>
    /// <param name="containerName">Name of the blob container.</param>
    /// <param name="pageSize">Number of records to return.</param>
    /// <param name="continuationToken">Continuation token from previous query.</param>
    /// <returns>List of call summaries.</returns>
    public async Task<(List<CallSummary>, string?)> GetCallSummaryListAsync(string? containerName, 
        int? pageSize, string? continuationToken)
    {
        BlobContainerClient blobContainerClient = new(_connectionString, containerName);

        List<CallSummary> callSummaries = [];

        var pages = blobContainerClient.GetBlobsAsync()
            .AsPages(continuationToken, pageSize);

        await foreach (Page<BlobItem> page in pages)
        {
            foreach (BlobItem blobItem in page.Values)
            {
                CallSummary callSummary = new CallSummary
                {
                    CreationDate = blobItem.Properties.CreatedOn.ToString(),
                    FileName = blobItem.Name,
                    Url = blobContainerClient.Uri + "/" + blobItem.Name
                };

                callSummaries.Add(callSummary);
            }

            continuationToken = page.ContinuationToken;

            // Only return the first page of results, starting from
            // the continuation token, if provided.
            break;
        }

        return (callSummaries, continuationToken);
    }
}
