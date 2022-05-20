using System;
using System.IO;

namespace IronMacbeth.FileStorage
{
    internal class LocalFileStorage : IFileStorage
    {
        private readonly string _storageFolder;

        public LocalFileStorage(string storageFolder)
        {
            _storageFolder = Path.GetFullPath(storageFolder);

            Directory.CreateDirectory(_storageFolder);
        }

        public Stream GetFile(Guid fileId)
        {
            var fileStream = File.OpenRead(GetFilePath(fileId));

            return fileStream;
        }

        public Guid StoreFile(Stream fileContent)
        {
            var fileId = Guid.NewGuid();
            var filePath = GetFilePath(fileId);
            
            var fileInfo = new FileInfo(filePath);
            using (var stream = fileInfo.Open(FileMode.CreateNew, FileAccess.Write))
            {
                fileContent.CopyTo(stream);
                stream.Close();
            }

            var fileDbRecord = new Model.File { FileId = fileId, Path = filePath, Size = fileContent.Position, CreatedTimestamp = DateTime.UtcNow };

            using (var dbContext = new DbContext())
            {
                dbContext.Files.Add(fileDbRecord);

                dbContext.SaveChanges();
            }

            return fileId;
        }

        private string GetFilePath(Guid fileId) => Path.Combine(_storageFolder, fileId.ToString());
    }
}
