using System;
using System.Linq;
using UnityEngine;

namespace FacialExpression.UI
{
    public enum Views
    {
        Camera,
        Gallery,
    }
    public class ViewPanelsManager : MonoBehaviour
    {
        [SerializeField] private Views startView;
        
        private RectTransform _rectTransform;
        private ViewPanel[] _panels;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _panels = GetComponentsInChildren<ViewPanel>();
            SetWidth();
        }

        private void SetWidth()
        {
            _rectTransform.sizeDelta = new Vector2(Screen.width * _panels.Length, Screen.height);
        }

        private void SetView(Views view)
        {
            var viewPanel = _panels.Where(x => x).FirstOrDefault();
        }

        private void MoveToView(int viewId)
        {
            
        }
    }
}
