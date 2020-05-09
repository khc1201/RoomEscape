using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

namespace Hint
{
    public class HintInventory : MonoBehaviour
    {
        public List<HintSlot> hintSlots;
        private List<HintObject> activateHints;
        public ShowADUI showAd;


        public Sprite beforeADSprite;
        public Sprite afterADSprite;

        private void Start()
        {
            InitSlots();
        }

        private void InitSlots()
        {
            hintSlots = new List<HintSlot>();
            showAd = FindObjectOfType<ShowADUI>();
            foreach (var e in this.gameObject.GetComponentsInChildren<HintSlot>())
            {
                hintSlots.Add(e);
            }
        }

        public void RefreshInventory()
        {
            ClearInventory();
            GetNowHint();

            if(activateHints.Count > hintSlots.Count)
            {
                Debug.LogError(this.gameObject.name + "에서 보내는 경고 : 현재 활성화 된 힌트의 개수가 최대 HintSlot 의 개수보다 많습니다!");
            }

            for(int i = 0; i < hintSlots.Count; i++)
            {
                hintSlots[i].SetObject(activateHints[i]);
                if(i+1 == activateHints.Count)
                {
                    break;
                }
            }
        }

        private void ClearInventory()
        {
            for (int i = 0; i < hintSlots.Count; i++)
            {
                if (hintSlots[i] != null)
                {
                    hintSlots[i].DeleteObject();
                }
            }
        }

        private void GetNowHint()
        {
            var tempActivateHints =
                from nowHint in HintManager.singleton.hintObjects
                where nowHint.isNowHint
                select nowHint;

            activateHints = new List<HintObject>();
            foreach (var e in tempActivateHints)
            {
                activateHints.Add(e);
            }
        }
    }
}
