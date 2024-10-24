﻿using System;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using HeyRed.Mime;
using Microsoft.Extensions.Configuration;
using SVK.Domain.Files;

namespace SVK.Services.Files
{
    public class BlobStorageService : IStorageService
    {
        private readonly string connectionString;

        public Uri BasePath => new Uri("https://hogentsvk.blob.core.windows.net/images");

        public BlobStorageService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("Storage");
        }

        public Uri GenerateImageUploadSas(Domain.Files.Image file)
        {
            string containerName = "images";
            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(file.Filename);

            var blobSasBuilder = new BlobSasBuilder
            {
                ExpiresOn = DateTime.UtcNow.AddMinutes(5),
                BlobContainerName = containerName,
                BlobName = file.Filename,
            };

            blobSasBuilder.SetPermissions(BlobSasPermissions.Create | BlobSasPermissions.Write);
            var sas = blobClient.GenerateSasUri(blobSasBuilder);
            return sas;
        }
    }
}
