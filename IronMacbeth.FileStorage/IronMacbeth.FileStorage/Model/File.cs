using System;

namespace IronMacbeth.FileStorage.Model
{
    internal class File
    {
        public Guid FileId { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public DateTime CreatedTimestamp { get; set; }
    }
}
