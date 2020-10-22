using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FacialExpression.Pooler
{
    public abstract class BasePoolObject : MonoBehaviour
    {
        public string PoolTag { get; private set; }
        
        public virtual void OnCreate(string poolTag)
        {
            PoolTag = poolTag;
        }

        public virtual void OnSpawn(Dictionary<string, string> data)
        {
            
        }

        public virtual void OnReturn()
        {
            
        }
    }
}
