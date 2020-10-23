using System;
using System.Collections.Generic;
using FacialExpression.AzureFaceApi.Models;
using UnityEngine;

namespace FacialExpression.AzureFaceApi
{
    public class ImageAnalyzer : MonoBehaviour
    {
        [SerializeField] private FaceApiConnection faceApiConnection;

        public void MakeAnalysisRequest(byte[] image, Action<Response> callback)
        {
            List<RequestHeader> headers = new List<RequestHeader>();
            headers.Add(faceApiConnection.ClientRequestHeader);
            StartCoroutine(faceApiConnection.HttpPostImage(faceApiConnection.ConnectionString, image, callback, headers));
        }
    }
}