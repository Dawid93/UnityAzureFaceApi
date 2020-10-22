using System;
using UnityEngine;
using UnityEngine.UI;

namespace FacialExpression.UI
{
    public class CameraViewPanel : BaseViewPanel
    {
        [SerializeField] private RawImage cameraTexture;
        [SerializeField] private AspectRatioFitter aspectRatioFitter;
        
        private bool _camIsAvailable;
        private WebCamTexture _photoCameraTexture;

        public override void PrepareView(ViewPanelsController viewPanelsController)
        {
            base.PrepareView(viewPanelsController);
        
            WebCamDevice[] devices = WebCamTexture.devices;

            if (devices.Length > 0)
            {
                foreach (var device in devices)
                {
                    if (device.isFrontFacing)
                    {
                        _photoCameraTexture = new WebCamTexture(device.name, Screen.width, Screen.height);
                    }
                }
            }

            _camIsAvailable = _photoCameraTexture != null;
            
            if(_camIsAvailable)
                StartRecording();
        }

        private void Update()
        {
            if(ViewPanelsController.CurrentView != ViewType.Camera) return;
            if(!_camIsAvailable) return;

            float ratio = (float) _photoCameraTexture.width / _photoCameraTexture.height;
            aspectRatioFitter.aspectRatio = ratio;
            
            float scaleY = _photoCameraTexture.videoVerticallyMirrored ? -1 : 1f;
            cameraTexture.rectTransform.localScale = new Vector3(1f, scaleY, 1f);
        }

        private void StartRecording()
        {
            _photoCameraTexture.Play();
            cameraTexture.texture = _photoCameraTexture;
        }

    }
}
