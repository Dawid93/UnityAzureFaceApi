using UnityEngine;

namespace FacialExpression.UI
{
    public abstract class BaseViewPanel : MonoBehaviour
    {
        public ViewType ViewType => viewType;
        public Vector3 Position { get; private set; }

        [SerializeField] private ViewType viewType;
        [SerializeField] private NavigationButtonStatus navigationButton;

        protected ViewPanelsController ViewPanelsController;

        public virtual void PrepareView(ViewPanelsController viewPanelsController)
        {
            ViewPanelsController = viewPanelsController;
            Position = GetComponent<RectTransform>().anchoredPosition;
            navigationButton.PrepareButton(viewPanelsController);
        }
    }
}