using System.Collections.Generic;
using FacialExpression.Helpers;
using FacialExpression.Pooler;
using UnityEngine;
using UnityEngine.UI;

namespace FacialExpression.UI
{
    public class GalleryViewPanel : BaseViewPanel
    {
        [SerializeField] private Transform contentParent;
        
        private List<Texture2D> _imageTextures;

        public override void PrepareView(ViewPanelsController viewPanelsController)
        {
            base.PrepareView(viewPanelsController);
            LoadGallery();
        }

        private void LoadGallery()
        {
            _imageTextures = new List<Texture2D>();
            
            string[] imagesPaths = FileHelper.GetAllImagesName();
            
            if(imagesPaths == null || imagesPaths.Length == 0)
                return;

            foreach (var path in imagesPaths)
            {
                var imageData = new Dictionary<string, string>
                {
                    { "path", path}
                };
                ObjectPooler.Instance.GetFromPool(ObjectPooler.ImagePoolTag, Vector3.zero, Quaternion.identity, contentParent,
                    imageData);
            }
        }

        public void ShowImage()
        {
            
        }
    }
}
