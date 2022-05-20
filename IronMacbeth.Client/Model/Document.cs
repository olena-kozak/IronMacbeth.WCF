using System;
using System.IO;

namespace IronMacbeth.Client
{
    public class Document
    {
        public MemoryStream ElectronicVersion { get; set; }

        public Guid? ElectronicVersionFileId { get; set; }
    }
}
