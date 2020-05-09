using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hint {
    public class HintSlot : MonoBehaviour
    {
        public HintObject hintObject;
        private Button thisButton;
        private HintInventory hintInven;
        private Image bgImage;

        private void Start()
        {
            InitButton();
        }

        public void InitButton()
        {
            thisButton = GetComponentInChildren<Button>();
            thisButton.onClick.RemoveAllListeners();
            thisButton.onClick.AddListener(OnButtonClick);
            bgImage = transform.GetChild(0).GetComponent<Image>();
            hintInven = GetComponentInParent<HintInventory>();
        }

        public void DeleteObject()
        {
            hintObject = null;
            thisButton.enabled = false;
            RefreshButtonUI();
        }

        public void SetObject(HintObject target)
        {
            hintObject = target;
            thisButton.enabled = true;
            RefreshButtonUI();
        }

        public void OnButtonClick()
        {
            if(hintObject.isOpenned)
            {
                //for test : 바로 다이얼로그 출력
                hintObject.ShowHint();
            }
            else
            {
                hintInven.showAd.CheckShowAD(this);
                //for test : 광고 출력에 대한 UI 출력

            }
        }

        public void RefreshButtonUI()
        {
            if(hintObject == null)
            {
                thisButton.image.canvasRenderer.SetAlpha(0f);
                bgImage.canvasRenderer.SetAlpha(0f);
                thisButton.image.sprite = null;
            }
            else
            {
                thisButton.image.canvasRenderer.SetAlpha(1f);
                bgImage.canvasRenderer.SetAlpha(1f);
                thisButton.image.sprite = hintObject.isOpenned ? hintInven.afterADSprite : hintInven.beforeADSprite ;

            }
        }
    }
}
