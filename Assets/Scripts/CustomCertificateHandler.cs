using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomCertificateHandler : CertificateHandler
{
    // Override this method to validate the certificate
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        // In this example, always return true to accept all certificates without validation
        // WARNING: This is for demonstration purposes only and is not secure!
        return true;
    }
}
