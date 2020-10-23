using System;
using System.Collections;
using System.Collections.Generic;
using FacialExpression.AzureFaceApi.Models;
using UnityEngine;
using UnityEngine.Networking;

namespace FacialExpression.AzureFaceApi
{
    public class FaceApiConnection : MonoBehaviour
    {
        private const string DefaultContentType = "application/json";
        private const string OptionalParameters = "detect?returnFaceId=false&returnFaceLandmarks=false&returnFaceAttributes=emotion&recognitionModel=recognition_01&returnRecognitionModel=false&detectionModel=detection_01";

        public RequestHeader ClientRequestHeader
        {
            get
            {
                if (_clientRequestHeader == null)
                    _clientRequestHeader = CreateClientRequestHeader();
                return _clientRequestHeader;
            }
        }

        public string ConnectionString => $"{connectionSettings.EndPoint}/face/v1.0/{OptionalParameters}";
        
        [SerializeField] private ConnectionSettings connectionSettings;

        private RequestHeader _clientRequestHeader;

        private RequestHeader CreateClientRequestHeader()
        {
            return new RequestHeader(connectionSettings.ClientId, connectionSettings.PrivateKey);
        }

        
        public IEnumerator HttpPostImage(string url, byte[] bytes, Action<Response> callback, IEnumerable<RequestHeader> headers = null)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Post(url, UnityWebRequest.kHttpVerbPOST))
            {
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }

                webRequest.uploadHandler.contentType = "application/octet-stream";
                webRequest.uploadHandler = new UploadHandlerRaw(bytes);

                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    callback(new Response
                    {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error
                    });
                }

                if (webRequest.isDone)
                {
                    string data = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                    callback(new Response
                    {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                        Data = data
                    });
                }
            }
        }
    }
}