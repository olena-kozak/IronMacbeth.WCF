using System;
using System.IO;

namespace IronMacbeth.FileStorage
{
    internal interface IFileStorage
    {
        Stream GetFile(Guid fileId);
        Guid StoreFile(Stream fileContent);
    }
}
