using UnityEditor;
using UnityEngine;

namespace FacialExpression.UI
{
    [CreateAssetMenu(fileName = "RawImageSettings", menuName = "RawImageSettings", order = 0)]
    public class RawImageSettings : ScriptableObject
    {
        public Texture2D DefaultTexture => defaultTexture;
        public float ScaleY { get; set; }
        public float AspectRatio { get; set; }
        public float Orientation { get; set; }
        
        [SerializeField] private Texture2D defaultTexture;
    }
}