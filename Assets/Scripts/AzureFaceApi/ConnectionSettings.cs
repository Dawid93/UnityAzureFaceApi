using UnityEngine;

namespace FacialExpression.AzureFaceApi
{
    [CreateAssetMenu(fileName = "ConnectionSettings", menuName = "AzureFaceApi/ConnectionSettings", order = 0)]
    public class ConnectionSettings : ScriptableObject
    {
        public string EndPoint => endPoint;
        public string PrivateKey => privateKey; 
        
        [SerializeField] private string endPoint;
        [SerializeField] private string privateKey;
    }
}