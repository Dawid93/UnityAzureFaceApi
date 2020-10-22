using UnityEngine;

namespace FacialExpression.UI
{
    public class ControlPanel : MonoBehaviour
    {
        [SerializeField] private ViewPanelsController viewPanelController;
        [SerializeField] private CameraViewPanel cameraViewPanel;
        
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
