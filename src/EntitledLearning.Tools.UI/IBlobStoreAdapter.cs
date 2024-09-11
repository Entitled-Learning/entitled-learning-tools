// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using Azure.Storage.Blobs;

namespace EntitledLearning.Tools.UI;

public interface IBlobStoreAdapter
{
    BlobContainerClient GetBlobContainerClient();
}


