using System;
using UnityEngine;
using UnityEngine.UI;

namespace FacialExpression.UI
{
    public class NavigationButtonStatus : MonoBehaviour
    {
        [SerializeField] private Color activeView = Color.green;
        [SerializeField] private Color inactiveView = Color.white;
        [SerializeField] private BaseViewPanel view;

        private ViewPanelsController _viewPanelsController;
        private Image _image;
        
        public void PrepareButton(ViewPanelsController viewPanelsController)
        {
            _image = GetComponent<Image>();
            _viewPanelsController = viewPanelsController;
            _viewPanelsController.OnViewChange += HandleOnViewChange;
            
            HandleOnViewChange(viewPanelsController.CurrentView);
        }

        private void HandleOnViewChange(ViewType viewType)
        {
            _image.color = viewType == view.ViewType ? activeView : inactiveView;
        }
    }
}