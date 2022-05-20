using System;
using System.Configuration;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace IronMacbeth.Common.CertificateValidation
{
    public class IronMacbethX509CertificateValidator : X509CertificateValidator
    {
        private static readonly Lazy<string[]> _trustedCertificatesLazy = new Lazy<string[]>(ReadTrustedCertificatesFromConfig, LazyThreadSafetyMode.ExecutionAndPublication);

        private static string[] ReadTrustedCertificatesFromConfig()
        {
            var trustedCertificates = ConfigurationManager.GetSection("trustedCertificates") as string[];

            if (trustedCertificates == null)
            {
                throw new ConfigurationErrorsException("Failed reading 'trustedCertificates' config.");
            }

            return trustedCertificates;
        }

        public override void Validate(X509Certificate2 certificate)
        {
            var trustedCertificates = _trustedCertificatesLazy.Value;

            if (!trustedCertificates.Contains(certificate.SerialNumber, StringComparer.OrdinalIgnoreCase))
            {
                throw new Exception($"Certificate (SN='{certificate.SerialNumber}') is not trusted.");
            }
        }
    }
}
