﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace FacialExpression.Pooler
{
    [Serializable]
    public class Pool
    {
        public string PoolTag => poolTag;
        public BasePoolObject PoolObject => basePoolObject;
        public int PoolSize => poolSize;

        [SerializeField] private string poolTag;
        [SerializeField] private BasePoolObject basePoolObject;
        [SerializeField] private int poolSize;
    }
    
    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<ObjectPooler>();
                return _instance;
            }
        }

        public static readonly string ImagePoolTag = "imageTag";

        private static ObjectPooler _instance;

        [SerializeField] private List<Pool> pools;
        [SerializeField] private Vector3 spawnPoint;
        [SerializeField] private Transform poolParent;

        private Dictionary<string, Queue<BasePoolObject>> _poolsDict;
        private bool _isInit = false;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            
            if(!_isInit)
                InitializePools();
        }

        private void InitializePools()
        {
            if(_isInit)
                return;
            
            _poolsDict = new Dictionary<string, Queue<BasePoolObject>>();

            foreach (var pool in pools)
            {
                Queue<BasePoolObject> tempQ = new Queue<BasePoolObject>();

                for (int i = 0; i < pool.PoolSize; i++)
                {
                    var bpo = Instantiate(pool.PoolObject, spawnPoint, Quaternion.identity, poolParent);
                    bpo.OnCreate(pool.PoolTag);
                    bpo.gameObject.SetActive(false);
                    tempQ.Enqueue(bpo);
                }
                _poolsDict.Add(pool.PoolTag, tempQ);
            }

            _isInit = true;
        }
        
        public BasePoolObject GetFromPool(string poolTag, Vector3 pos, Quaternion rot, Transform parent, Dictionary<string, string> data = null)
        {
            if(!_isInit)
                InitializePools();
            
            if (_poolsDict == null)
                return null;
            if (!_poolsDict.ContainsKey(poolTag))
                return null;

            var bpo = _poolsDict[poolTag].Dequeue();
            bpo.transform.SetParent(parent);
            bpo.transform.localPosition = pos;
            bpo.transform.localRotation = rot;
            bpo.gameObject.SetActive(true);
            bpo.OnSpawn(data);
            return bpo;
        }

        public void ReturnToPool(BasePoolObject basePoolObject)
        {
            if (_poolsDict == null)
                return;
            if (!_poolsDict.ContainsKey(basePoolObject.PoolTag))
                return;
            
            basePoolObject.OnReturn();
            basePoolObject.gameObject.SetActive(false);
            _poolsDict[basePoolObject.PoolTag].Enqueue(basePoolObject);
        }
    }
}
