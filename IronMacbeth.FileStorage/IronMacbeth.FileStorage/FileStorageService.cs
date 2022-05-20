using IronMacbeth.FileStorage.Contract;
using System;
using System.IO;
using System.Configuration;

namespace IronMacbeth.FileStorage
{
    class FileStorageService : IFileStorageService
    {
        private readonly IFileStorage _fileStorage;

        public FileStorageService()
        {
            _fileStorage = (IFileStorage)ConfigurationManager.GetSection("fileStorageMethods");
        }

        public Guid AddFile(Stream fileStream)
        {
            var result = _fileStorage.StoreFile(fileStream);

            return result;
        }

        public Stream GetFile(Guid fileId)
        {
            // do not dispose stream here. It's disposed by WCF whenever it's done sending it to the client
            var fileContent = _fileStorage.GetFile(fileId);

            return fileContent;
        }
    }
}
