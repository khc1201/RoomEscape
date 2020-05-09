using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hint
{
    public class HintObject : MonoBehaviour
    {
        public string targetStringDataIndex;
        public bool isOpenned;
        public bool isNowHint;
        public StreamData StreamDataForShow;
        public StreamData StreamDataForHide;

        private ReactObject nowReactObject; 

        //for test
        public string descOfShowStream;
        public string descOfHideStream;

        private void Start()
        {
            CheckValid();
            nowReactObject = GetComponent<ReactObject>();
        }

        void CheckValid()
        {
            if (StreamDataForShow == null || StreamDataForHide == null)
            {
                Debug.LogError(this.gameObject.name + "의 StreamData 가 정상적으로 입력되어 있지 않습니다.");
            }
            else
            {
                descOfShowStream = StreamDataForShow.desc;
                descOfHideStream = StreamDataForHide.desc;
            }
        }
        
        public void ShowHint()
        {
            Dialog.singleton.ShowDialog(nowReactObject);
        }

        public void CheckNowHint()
        {
            if (UserData.singleton.list_completestream.Contains(StreamDataForShow.index)
            && !(UserData.singleton.list_completestream.Contains(StreamDataForHide.index)))
            {
                isNowHint = true;
            }
            else
            {
                isNowHint = false;
            }
        }
        
    }
}
