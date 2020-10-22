using System.Collections.Generic;
using FacialExpression.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace FacialExpression.Pooler
{
    public class ImagePoolObject : BasePoolObject
    {
        private RawImage _rawImage;
        
        private const string PathKey = "path";
        
        public string PathToCurrentImage { get; private set; }

        public override void OnCreate(string poolTag)
        {
            base.OnCreate(poolTag);
            _rawImage = GetComponent<RawImage>();
        }

        public override void OnSpawn(Dictionary<string, string> data)
        {
            base.OnSpawn(data);
            if(data == null)
                return;
            if (!data.ContainsKey(PathKey))
                return;
            
            PathToCurrentImage = data[PathKey];
            _rawImage.texture = FileHelper.LoadImage(PathToCurrentImage);
        }

        public override void OnReturn()
        {
            base.OnReturn();
            _rawImage.texture = null;
        }
    }
}
