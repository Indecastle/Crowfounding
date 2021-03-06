﻿using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.Services
{
    public interface IS3Service
    {
        Task CreateBucketAsync();
        Task InitBucketAsync();
        Task CreateUserAsync(string username);
        Task CreateFolderAsync(string newDirPath);
        Task UploadObjectAsync(Stream stream, string pathObject);
        Task DeleteFilesAsync(string fullPathName);
        Task<IEnumerable<S3Object>> ListFilesAsync(string dirpath);
        string GeneratePreSignedURL(string filepath, bool attachment, string fileName = null, bool useHttp = false);
        string GetPublicURL(string filepath, string fileName = null);

    }
}
