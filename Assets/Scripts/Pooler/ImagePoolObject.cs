using System.Collections.Generic;
using FacialExpression.Helpers;
using FacialExpression.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FacialExpression.Pooler
{
    public class ImagePoolObject : BasePoolObject, IPointerClickHandler
    {
        private RawImage _rawImage;
        private Texture2D _texture;
        private ViewPanelsController _viewPanelsController;
        
        private const string PathKey = "path";
        
        public string PathToCurrentImage { get; private set; }

        public override void OnCreate(string poolTag)
        {
            base.OnCreate(poolTag);
            _rawImage = GetComponent<RawImage>();
            _viewPanelsController = FindObjectOfType<ViewPanelsController>();
        }

        public override void OnSpawn(Dictionary<string, string> data)
        {
            base.OnSpawn(data);
            
            
            if(data == null)
                return;
            if (!data.ContainsKey(PathKey))
                return;
            
            PathToCurrentImage = data[PathKey];
            _texture = FileHelper.LoadImage(PathToCurrentImage);
            _rawImage.texture = _texture;
        }

        public override void OnReturn()
        {
            base.OnReturn();
            _rawImage.texture = null;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _viewPanelsController.ShowDetailView(PathToCurrentImage, _texture);
        }
    }
}
