using UnityEngine;

namespace FacialExpression.UI
{
    public class ControlPanel : MonoBehaviour
    {
        [SerializeField] private ViewPanelsController viewPanelController;
        [SerializeField] private CameraViewPanel cameraViewPanel;
        
        
        public void ShowGallery()
        {
            viewPanelController.MoveToView(ViewType.Gallery);
        }

        public void ShowCamera()
        {
            viewPanelController.MoveToView(ViewType.Camera);
        }

        public void TakePicture()
        {
            
        }
    }
}
