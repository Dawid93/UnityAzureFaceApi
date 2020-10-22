using UnityEngine;

namespace FacialExpression.AzureFaceApi
{
    public class FaceApiConnection
    {
        private string SecretKey { get; set; }
        private string EndPoint { get; set; }
        
        public FaceApiConnection(ConnectionSettings connectionSettings)
        {
            SecretKey = connectionSettings.PrivateKey;
            EndPoint = connectionSettings.EndPoint;
        }
    }
}