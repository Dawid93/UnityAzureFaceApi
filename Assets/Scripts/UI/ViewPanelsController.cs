using System;
using System.Linq;
using FacialExpression.AzureFaceApi.Models;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FacialExpression.UI
{
    public enum ViewType
    {
        Camera,
        Gallery,
    }
    public class ViewPanelsController : MonoBehaviour
    {
        public event Action<ViewType> OnViewChange; 
        public ViewType CurrentView { 
            get => _currentView;
            set
            {
                _currentView = value;
                OnViewChange?.Invoke(_currentView);
            }
        }
        
        [SerializeField] private ViewType startViewType;
        [SerializeField] private BaseViewPanel[] panels;
        [SerializeField] private DetailView detailView;

        private RectTransform _rectTransform;
        private ViewType _currentView;

        private void Start()
        {
            CloseDetailView();
            _rectTransform = GetComponent<RectTransform>();
            for(int i = 0; i < panels.Length; i++)
                SetPanelPosition(i);
            
            SetView(startViewType);
        }

        private void SetPanelPosition(int id)
        {
            var rectTransform = panels[id].GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(Screen.width * id, 0);
            panels[id].PrepareView(this);
        }

        private void SetView(ViewType viewType)
        {
            var viewPanel = panels.FirstOrDefault(x => x.ViewType == viewType);
            CurrentView = viewType;
            
            if (viewPanel != null) 
                _rectTransform.anchoredPosition = new Vector3(-viewPanel.Position.x, 0f,0f);
        }

        public void MoveToView(ViewType viewType)
        {
            var viewPanel = panels.FirstOrDefault(x => x.ViewType == viewType);
            CurrentView = viewType;

            if (viewPanel != null)
                LeanTween.moveX(_rectTransform, -viewPanel.Position.x, 0.5f);
        }

        public void ShowDetailView(string path, Texture2D image)
        {
            detailView.gameObject.SetActive(true);
            detailView.ShowView(path, image);
        }

        public void CloseDetailView()
        {
            detailView.CloseView();
        }
    }
}
