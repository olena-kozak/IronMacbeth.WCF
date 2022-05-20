using IronMacbeth.Client.Model;
using System;

namespace IronMacbeth.Client
{
    public interface IDisplayable
    {
        Guid? ImageFileId { get; set; }
        Image Image { get; set; }
    }
}