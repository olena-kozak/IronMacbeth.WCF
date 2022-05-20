using IronMacbeth.FileStorage.Model;
using System;
using System.Configuration;
using System.Xml;

namespace IronMacbeth.FileStorage.Configuration
{
    internal class FileStorageMethodConfigurationSection : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            object result = null;

            var selectedStorageMethodName = section.Attributes["selected"].Value;
            var selectedStorageMethod = (FileStorageMethod)Enum.Parse(typeof(FileStorageMethod), selectedStorageMethodName);

            foreach (XmlNode childNode in section.ChildNodes)
            {
                if (string.Equals(selectedStorageMethodName, childNode.Attributes["name"].Value, StringComparison.OrdinalIgnoreCase))
                {
                    if (selectedStorageMethod == FileStorageMethod.LocalStorage)
                    {
                        var storageFolder = childNode.Attributes["storageFolder"].Value;

                        var localStorageConfiguration = new LocalFileStorage(storageFolder);

                        result = localStorageConfiguration;
                    }
                }
            }

            if (result == null)
            {
                throw new ConfigurationErrorsException("Failed to read fileStorageMethods. No file storage methods were found.");
            }

            return result;
        }
    }
}
