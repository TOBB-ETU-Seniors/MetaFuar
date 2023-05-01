using System.Security.Cryptography.X509Certificates;
using UnityEngine.Networking;

class AcceptAllCertificatesSignedWithASpecificKeyPublicKey : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        
            return true;
        
    }
}