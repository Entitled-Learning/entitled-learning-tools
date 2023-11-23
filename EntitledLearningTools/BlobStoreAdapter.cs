using Azure.Storage.Blobs;
using CommunityToolkit.Diagnostics;

namespace EntitledLearningTools;

public class BlobStoreAdapter
{
    private const string ContainerName = "input";
    private readonly BlobServiceClient? _blobServiceClient;

    public BlobStoreAdapter(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ELBlobConnectionString");
        _blobServiceClient = new BlobServiceClient(connectionString);
    }

    public BlobContainerClient GetBlobContainerClient()
    {
        Guard.IsNotNull(_blobServiceClient);

        return _blobServiceClient.GetBlobContainerClient(ContainerName);
    }
}

