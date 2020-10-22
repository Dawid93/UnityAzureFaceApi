using System.Collections.Generic;
using UnityEngine;

namespace FacialExpression.UI
{
    public class GalleryViewPanel : BaseViewPanel
    {
        private List<Texture> _imageTextures;

        public override void PrepareView(ViewPanelsController viewPanelsController)
        {
            base.PrepareView(viewPanelsController);
            LoadGallery();
        }

        private void LoadGallery()
        {
            _imageTextures = new List<Texture>();
        }

        public void ShowImage()
        {
            
        }
    }
}
