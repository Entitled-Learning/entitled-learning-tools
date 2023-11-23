using Azure.Storage.Blobs;

namespace EntitledLearningTools;

public interface IBlobStoreAdapter
{
    BlobContainerClient GetBlobContainerClient();
}


