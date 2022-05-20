using System;
using System.IO;
using System.ServiceModel;

namespace IronMacbeth.FileStorage.Contract
{
    [ServiceContract]
    public interface IFileStorageService
    {
        [OperationContract]
        Guid AddFile(Stream fileStream);

        [OperationContract]
        Stream GetFile(Guid fileId);
    }
}
