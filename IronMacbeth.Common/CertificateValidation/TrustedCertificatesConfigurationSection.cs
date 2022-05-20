using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace IronMacbeth.Common.CertificateValidation
{
    public class TrustedCertificatesConfigurationSection : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            List<string> trustedCertificates = new List<string>();

            foreach (XmlNode childNode in section.ChildNodes)
            {
                foreach (XmlAttribute attrib in childNode.Attributes)
                {
                    trustedCertificates.Add(attrib.Value);
                }
            }

            return trustedCertificates.ToArray();
        }
    }
}
