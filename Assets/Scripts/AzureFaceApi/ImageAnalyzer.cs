using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FacialExpression.AzureFaceApi.Models;
using FacialExpression.Helpers;
using UnityEngine;

namespace FacialExpression.AzureFaceApi
{
    public class ImageAnalyzer : MonoBehaviour
    {
        [SerializeField] private FaceApiConnection faceApiConnection;
        
        public async Task MakeAnalysisRequest(string imageFilePath)
        {
            HttpClient client = new HttpClient();

            // Request headers.
            client.DefaultRequestHeaders.Add(
                "Ocp-Apim-Subscription-Key", faceApiConnection.ClientRequestHeader.Value);

            HttpResponseMessage response;

            // Request body. Posts a locally stored JPEG image.
            byte[] byteData = FileHelper.LoadImageAsArray(imageFilePath);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json"
                // and "multipart/form-data".
                content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");

                // Execute the REST API call.
                response = await client.PostAsync(faceApiConnection.ConnectionString, content);

                // Get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();

                // Display the JSON response.
                Console.WriteLine("\nResponse:\n");
                Console.WriteLine((contentString));
            }
        }
        // public void MakeAnalysisRequest(byte[] image)
        // {
        //     MultipartFormFileSection fileToUpload = new MultipartFormFileSection("url", image);
        //     StartCoroutine(faceApiConnection.HttpPost(faceApiConnection.ConnectionString, fileToUpload, (r) => OnCallbackResponse(r)));
        // }

        private void OnCallbackResponse(Response response)
        {
            Debug.Log(response.Error);
        }
    }
}