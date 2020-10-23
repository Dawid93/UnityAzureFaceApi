using System;
using FacialExpression.AzureFaceApi;
using FacialExpression.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace FacialExpression.UI
{
    public class CameraViewPanel : BaseViewPanel
    {
        [SerializeField] private RawImage cameraTexture;
        [SerializeField] private AspectRatioFitter aspectRatioFitter;
        [SerializeField] private Texture2D defaultTexture;

        private bool _camIsAvailable;
        private WebCamTexture _photoCameraTexture;
        private RectTransform _rawImageRectTransform;

        public override void PrepareView(ViewPanelsController viewPanelsController)
        {
            base.PrepareView(viewPanelsController);
            cameraTexture.texture = defaultTexture;
            
            viewPanelsController.OnViewChange += HandleOnViewChange;
            _rawImageRectTransform = cameraTexture.rectTransform;

            WebCamDevice[] devices = WebCamTexture.devices;

            if (devices.Length > 0)
            {
                foreach (var device in devices)
                {
                    if (device.isFrontFacing)
                    {
                        _photoCameraTexture = new WebCamTexture(device.name, Screen.width, Screen.height);
                        _photoCameraTexture.requestedFPS = 60;
                    }
                }
            }

            _camIsAvailable = _photoCameraTexture != null;
            
            if(_camIsAvailable)
                StartRecording();
        }

        private void HandleOnViewChange(ViewType viewType)
        {
            if(viewType == ViewType.Camera && !_photoCameraTexture.isPlaying)
                StartRecording();
            else if(viewType != ViewType.Camera)
                StopRecording();
        }

        private void Update()
        {
            if(!_camIsAvailable) return;

            float ratio = (float) _photoCameraTexture.width / _photoCameraTexture.height;
            aspectRatioFitter.aspectRatio = ratio;
            
            float scaleY = _photoCameraTexture.videoVerticallyMirrored ? -1 : 1f;
            int orient = -_photoCameraTexture.videoRotationAngle;
            
            _rawImageRectTransform.localScale = new Vector3(-1f, scaleY, 1f);
            _rawImageRectTransform.localEulerAngles = new Vector3(0, 0, orient);
        }

        public void TakePicture()
        {
            Texture2D picture = new Texture2D(_photoCameraTexture.width, _photoCameraTexture.height);
            picture.SetPixels(_photoCameraTexture.GetPixels());
            picture.Apply();
            
            string path = FileHelper.SaveImageHelper(picture);
            
            ViewPanelsController.ShowDetailView(path, picture);
        }

        private void StartRecording()
        {
            _photoCameraTexture.Play();
            cameraTexture.texture = _photoCameraTexture;
        }

        private void StopRecording()
        {
            _photoCameraTexture.Stop();
            cameraTexture.texture = defaultTexture;
        }

    }
}
