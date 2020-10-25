using System;
using UnityEngine;
using UnityEngine.UI;

namespace FacialExpression.UI
{
    public class ControlPanel : MonoBehaviour
    {
        [SerializeField] private ViewPanelsController viewPanelController;
        [SerializeField] private CameraViewPanel cameraViewPanel;
        [SerializeField] private Button takePictureBtn;

        private void Start()
        {
            viewPanelController.OnViewChange += SetPictureBtnInteractable;
            SetPictureBtnInteractable(viewPanelController.CurrentView);
        }

        private void SetPictureBtnInteractable(ViewType currentViewType)
        {
            takePictureBtn.interactable = currentViewType == ViewType.Camera;
        }

        public void ShowGallery()
        {
            if(viewPanelController.CurrentView != ViewType.Gallery)
                viewPanelController.MoveToView(ViewType.Gallery);
        }

        public void ShowCamera()
        {
            if(viewPanelController.CurrentView != ViewType.Camera)
                viewPanelController.MoveToView(ViewType.Camera);
        }

        public void TakePicture()
        {
            cameraViewPanel.TakePicture();
        }
    }
}
