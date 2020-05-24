using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hint
{
    public class HintManager : MonoBehaviour
    {
        public static HintManager singleton;
        private void Awake()
        {
            if(singleton == null)
            {
                singleton = this;
                //DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                DestroyImmediate(this);
                return;
            }
        }

        public List<HintObject> hintObjects;
        public HintInventory hintInven;

        private void Start()
        {
            InitHintObjects();
        }

        public void OnChangeStreamData()
        {
            for(int i = 0; i < hintObjects.Count; i++)
            {
                hintObjects[i].CheckNowHint();
            }
            hintInven.RefreshInventory();
        }

        private void InitHintObjects()
        {
            hintInven = FindObjectOfType<HintInventory>();
            hintObjects = new List<HintObject>();
            foreach(var e in this.GetComponentsInChildren<HintObject>())
            {
                hintObjects.Add(e);
            }
        }
    }
}
