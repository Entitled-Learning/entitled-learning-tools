// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using Azure.Storage.Blobs;

namespace EntitledLearningTools;

public interface IBlobStoreAdapter
{
    BlobContainerClient GetBlobContainerClient();
}


