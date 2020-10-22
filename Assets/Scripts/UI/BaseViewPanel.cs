using UnityEngine;

namespace FacialExpression.UI
{
    public abstract class BaseViewPanel : MonoBehaviour
    {
        public ViewType ViewType => viewType;
        public Vector3 Position => _position;
        
        [SerializeField] private ViewType viewType;
        [SerializeField] private NavigationButtonStatus navigationButton;

        protected ViewPanelsController ViewPanelsController;
        
        private Vector3 _position;

        public void PrepareView(ViewPanelsController viewPanelsController)
        {
            ViewPanelsController = viewPanelsController;
            _position = GetComponent<RectTransform>().anchoredPosition;
            navigationButton.PrepareButton(viewPanelsController);
        }
    }
}