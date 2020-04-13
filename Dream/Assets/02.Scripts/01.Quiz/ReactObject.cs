using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ReactObject : MonoBehaviour
{


    //[Header("+ 공통 프로퍼티")]
    [SerializeField] private bool isEnd = false;
    public bool IsEnd { get { return isEnd; } }
    public bool isRepeat = false; // 어떻게든 버튼을 반복해서 조작할 수 있는 것인지?
    private bool isExistButton = false;
    private Button targetButton;
    [SerializeField] WaitForSeconds waitTime;
    private float tempTime;
    [HideInInspector] public bool isRecipt = false;
    //public List<StreamData> streamData_OnEnd;

    //[Header("+ 대상 GameObject")]
    public List<GameObject> targetObject;
    public List<StreamShowTarget> targetShowObject;

    //[Header("+ ReactObject 행동")]
    public enum_ObjectAction objectAction;
    //public float endCheckingTick = 0.5f;

    //[Header("+ Case : Talk")]
    public List<string> dialogs;
    

   // [Header("+ Case : PlaySoundEffect")]
    public SoundEffect targetSound;
    

    //[Header("+ Case : FadeIn/FadeOut")]
    public bool isImmediately;
    private FadeCanvas fadeCanvas;

    //[Header("+ Case : MoveBy/MoveTo/RotateBy/RotateTo")]
    public Vector3 reactVector;
    public float reactTime;
    public Ease reactEase;
    private List<Vector3> beforereactVector;

    //[Header("++ Case : MoveByButton/MoveToButton 위의 Move 와 프로퍼티를 공유")]
    private bool isButton = false;
    List<Button> moveButtonlist;



    //[Header("+ Case : DoTween")]
    public List<DOTweenAnimation> tweenAnimations;
    public bool isSequence = false; // 순서대로 발동 여부
    public float sequenceInterval = 0f;
    public Sequence seq;

    //[Header("+ Case : DoTweenPath")]
    public DOTweenPath tweenPathAnimation;
    

    //[Header("+ Case : CameraMove")]
    public CameraObj targetCamera;
    private CameraObj preCamera;
    
    //[Header("+ QInput 에 종속된 데이터")]
    [HideInInspector] public bool isQInputObject = false;
    //[Header("++ 역재생")]
    public bool isReverse = false;
    public bool isReverseActed = false;

    //[Header("+ SteramObject 에 종속된 데이터")]
    //[Header("++ 초기화 시 무시되는 데이터인가?")]
    //[SerializeField] private bool isIgnoreOnLoad = true;
    [HideInInspector] public bool isStreamObject = false;
    //[SerializeField] private bool isStreamCompletedonLoad = false; // for test : 어디에 쓰는 거
    private bool isplayafterComplete = false;
    public List<StreamData> targetDatas;

    public AllButtonsLock targetLock;

    public void Start()
    {
        //CheckIsIgnoreOnLoad();
        InitObjectAction();  
    }

    public void DoAction(bool _isplayafterComplete, Button _targetButton = null, bool _isStreamObject = false)
    {

        isplayafterComplete = _isplayafterComplete;
        if(_targetButton != null)
        {
            targetButton = _targetButton;
            isExistButton = true;
        }
        if (_isStreamObject)
        {
            this.isStreamObject = true;
        }

        switch (objectAction)
        {
            case enum_ObjectAction.Defalut:
                {
                    Debug.LogError(this.gameObject.name + "의 objectAction 이 설정되어있지 않다 (default)");
                    break;
                }
            case enum_ObjectAction.Hide:
                {
                    ObjectAction_Hide();
                    break;
                }

            case enum_ObjectAction.Show:
                {
                    ObjectAction_Show();
                    break;
                }

            case enum_ObjectAction.Talk:
                {
                    ObjectAction_Talk();
                    break;
                }

            case enum_ObjectAction.PlaySoundEffect:
                {
                    ObjectAction_PlaySE();
                    break;
                }

            case enum_ObjectAction.FadeIn:
                {
                    ObjectAction_FadeIn();
                    break;
                }

            case enum_ObjectAction.FadeOut:
                {
                    ObjectAction_FadeOut();
                    break;
                }

            case enum_ObjectAction.DoTween:
                {
                    ObjectAction_DoTween();
                    break;
                }

            case enum_ObjectAction.MoveBy:
                {
                    ObjectAction_Move(true);
                    break;
                }
            case enum_ObjectAction.MoveTo:
                {
                    ObjectAction_Move(false);
                    break;
                }
            case enum_ObjectAction.RotateBy:
                {
                    ObjectAction_Rotate(true);
                    break;
                }
            case enum_ObjectAction.RotateTo:
                {
                    ObjectAction_Rotate(false);
                    break;
                }
            case enum_ObjectAction.CameraMove:
                {
                    ObjectAciton_CameraMove();
                    break;
                }
            case enum_ObjectAction.MoveByButton:
                {
                    ObjectAction_MoveButton(true);
                    break;
                }

            case enum_ObjectAction.MoveToButton:
                {
                    ObjectAction_MoveButton(false);
                    break;
                }
            case enum_ObjectAction.CompleteStream:
                {
                    ObjectAction_CompleteStream();
                    break;
                }
            case enum_ObjectAction.AllButtonsActive:
                {
                    ObjectAciton_AllButtonsAcitve(true);
                    break;
                }
            case enum_ObjectAction.AllButtonsDisable:
                {
                    ObjectAciton_AllButtonsAcitve(false);
                    break;
                }
            case enum_ObjectAction.DoTweenPath:
                {
                    ObjectAction_DoTweenPath();
                    break;
                }
            default:
                {
                    Debug.Log("해당 " + objectAction.ToString() + "은 아직 구현되지 않았습니다.");
                    break;
                }
                
        }
    }

    //SetEnable_TargetButton() 구현이 필요함.
    private void SetButton_Disable()
    {
        if (isButton)
        {
            for(int i = 0; i < moveButtonlist.Count; i++)
            {
                moveButtonlist[i].enabled = false;
            }
        }
        if (isExistButton && isRepeat) targetButton.enabled = false;
    }

    private void DoEnd()
    {
        if (isButton)
        {
            for (int i = 0; i < moveButtonlist.Count; i++)
            {
                moveButtonlist[i].enabled = true;
            }
        }
        if(isExistButton && isRepeat) targetButton.enabled = true;
        if (isStreamObject) isEnd = true;
       /*
        if(streamData_OnEnd != null)
        {
            if (isQInputObject)
            {
                foreach (var e in streamData_OnEnd)
                {
                    e.CompleteStream();
                }
            }
            else if (isStreamObject)
            {
                Debug.LogError(this.gameObject.name + "의 종료시에 StreamData 가 설정되어 있으나, StreamObject 의 Object 는 다른 StreamObject 를 완료하여 발생할 수 없습니다.");
                return;
            }
        }*/
    }

    #region ObjectAction - Init
    private void InitObjectAction()
    {
        if (isReverse && !isRepeat)
        {
            Debug.LogError(this.gameObject.name + "설정 에러 : isReverse 는 체크가 되어있는데, isRepeat 가 체크되지 않았다.");
        }
        switch (objectAction)
        {
            case enum_ObjectAction.Show:
                {
                    Init_Show(true);
                    break;
                }
            case enum_ObjectAction.Hide:
                {
                    Init_Show(false);
                    break;
                }
            case enum_ObjectAction.MoveTo:
                {
                    Init_Move(false);
                    break;
                }
            case enum_ObjectAction.MoveBy:
                {
                    Init_Move(true);
                    break;
                }
            case enum_ObjectAction.RotateTo:
                {
                    Init_Rotate(false);
                    break;
                }
            case enum_ObjectAction.RotateBy:
                {
                    Init_Rotate(true);
                    break;
                }
            case enum_ObjectAction.DoTween:
                {
                    Init_Tween();
                    break;
                }
            case enum_ObjectAction.FadeIn:
                {
                    Init_Fade();
                    break;
                }
            case enum_ObjectAction.FadeOut:
                {
                    Init_Fade();
                    break;
                }
            case enum_ObjectAction.CameraMove:
                {
                    Init_CameraMove();
                    break;
                }

            case enum_ObjectAction.MoveByButton:
                {
                    Init_Move(true, true);
                    break;
                }
            case enum_ObjectAction.MoveToButton:
                {
                    Init_Move(false, true);
                    break;
                }
            case enum_ObjectAction.PlaySoundEffect:
                {
                    Init_SoundEffect();
                    break;
                }
            case enum_ObjectAction.CompleteStream:
                {
                    Init_CompleteStream();
                    break;
                }
            case enum_ObjectAction.AllButtonsActive:
                {
                    Init_AllButtonsLock();
                    break;
                }
            case enum_ObjectAction.AllButtonsDisable:
                {
                    Init_AllButtonsLock();
                    break;
                }
            case enum_ObjectAction.DoTweenPath:
                {
                    Init_TweenPath();
                    break;
                }
        }
    }
    private void Init_Show(bool isShow)
    {
        if(targetShowObject == null)
        {
            Debug.LogError(this.gameObject.name + "의 조건이 Show 이지만 아무런 TargetShowObject 가 설정되어 있지 않습니다.");
            return;
        }
        foreach(var e in targetShowObject)
        {
            if(e.targetObject == null)
            {
                Debug.LogError(e.gameObject.name + "의 targetObject 가 null 입니다.");
                
            }
        }
    }
    private void Init_AllButtonsLock()
    {
        targetLock = FindObjectOfType<AllButtonsLock>();
        if(targetLock == null)
        {
            Debug.LogError(this.gameObject.name + "에서 targetLock 을 찾지 못했습니다. null 값입니다.");
            return;
        }
    }
    private void Init_CompleteStream()
    {
        if (isQInputObject)
        {
            Debug.LogError(this.gameObject.name + "는 QInputObject 이므로 InitCompleteStream 을 발동할 수 없음! 강제로 Clear 시킴");
            targetDatas.Clear();
            return;
        }
        if(targetDatas == null)
        {
            Debug.LogError(this.gameObject.name + "의 targetDatas 가 없음. 완료할 스트림 데이터가 없습니다.");
            return;
        }
    }
    private void Init_Fade()
    {
        fadeCanvas = FindObjectOfType<FadeCanvas>();
    }
    private void Init_Tween()
    {
        if (tweenAnimations == null)
        {
            Debug.LogError(this.gameObject.name + "의 ObjectAction_DoTween 을 실행했으나, tweenAnimation 이 할당되어있지 않다. null 값임");
            return;
        }
        waitTime = new WaitForSeconds(GetMaxTweenTime());
    }
    private void Init_TweenPath()
    {
        if (tweenPathAnimation == null)
        {
            Debug.LogError(this.gameObject.name + "의 ObjectAction_DoTweenPath 를 실행했으나, tweenPathAnimation 이 할당되어있지 않다. null 값임");
            return;
        }
        waitTime = new WaitForSeconds(GetMaxTweenTime());
    }
    private void Init_Move(bool isBy, bool _isButton = false)
    {
        if (_isButton)
        {
            isButton = true;
            moveButtonlist = new List<Button>();
            for(int i = 0; i < targetObject.Count; i++)
            {
                moveButtonlist.Add(targetObject[i].gameObject.GetComponentInChildren<Button>());
            }
            if(moveButtonlist.Count == 0)
            {
                Debug.LogError(this.gameObject.name + "의 moveButtonlist 가 비어있다. isButton 이면, Button 은 있어야 하는걸!");
            }
        }
        if (reactVector == Vector3.zero)
        {
            Debug.Log(this.gameObject.name + "의 reactVector 가 설정되어있지 않음. 의도임?");
        }
        if (reactEase == null)
        {
            Debug.Log(this.gameObject.name + "의 reactEase 가 설정되어있지 않음. 내 맘대로 linear 로 바꿀 거임.");
            reactEase = Ease.Linear;
        }
        if (reactTime == 0f)
        {
            Debug.Log(this.gameObject.name + "의 reactTime 이 0 임. 의도임?");
        }

        if (!isBy)
        {
            beforereactVector = new List<Vector3>();
            for (int i = 0; i < targetObject.Count; i++)
            {
                beforereactVector.Add(targetObject[i].transform.localPosition);
            }
        }
    }
    private void Init_Rotate(bool isBy)
    {
        if (reactVector == Vector3.zero)
        {
            Debug.Log(this.gameObject.name + "의 reactVector 가 설정되어있지 않음. 의도임?");
        }
        if (reactEase == null)
        {
            Debug.Log(this.gameObject.name + "의 reactEase 가 설정되어있지 않음. 내 맘대로 linear 로 바꿀 거임.");
            reactEase = Ease.Linear;
        }
        if (reactTime == 0f)
        {
            Debug.Log(this.gameObject.name + "의 reactTime 이 0 임. 의도임?");
        }

        if (!isBy)
        {
            beforereactVector = new List<Vector3>();
            for (int i = 0; i < targetObject.Count; i++)
            {
                beforereactVector.Add(targetObject[i].transform.localRotation.eulerAngles);
            }
        }

    }
    private void Init_CameraMove()
    {
        if(targetCamera == null)
        {
            Debug.LogError(this.gameObject.name + "의 targetCamera 가 설정되어 있지 않습니다. null 입니다. 설정 필요.");
        }
    }
    private void Init_SoundEffect()
    {
        if(targetSound == null)
        {
            Debug.LogError(this.gameObject.name + "의 SoundEffect 가 null 입니다.");
            return;
        }
        tempTime = SoundEffectManager.singleton.GetPlayTime(targetSound);
        //for test
        Debug.Log("tempTime = " + tempTime);
        waitTime = new WaitForSeconds(tempTime);
        
    }
    #endregion

    #region ObjectAciton - DoAction
    private void ObjectAciton_AllButtonsAcitve(bool isActive)
    {
        targetLock.SetActive_LockImage(!isActive);
        DoEnd();
    }
    private void ObjectAction_CompleteStream()
    {
        foreach(var e in targetDatas)
        {
            e.CompleteStream();
        }
    }
    private void ObjectAciton_CameraMove()
    {
        preCamera = CameraManager.singleton.m_nowCamera;
        CameraManager.singleton.ChangeCamera(preCamera, targetCamera);
    }
    private void ObjectAction_MoveButton(bool isBy)
    {
        if (isBy)
        {
            if (isReverse && isReverseActed)
            {
                for (int i = 0; i < moveButtonlist.Count; i++)
                {
                    moveButtonlist[i].GetComponent<RectTransform>().DOBlendableLocalMoveBy(-1 * reactVector, reactTime)
                    //moveButtonlist[i].transform.DOBlendableLocalMoveBy(-1 * reactVector, reactTime)
                        .SetEase(reactEase)
                        .OnStart(SetButton_Disable)
                        .OnComplete(DoEnd).Play();
                }
                isReverseActed = false;
            }
            else
            {
                for (int i = 0; i < moveButtonlist.Count; i++)
                {
                    moveButtonlist[i].GetComponent<RectTransform>().DOBlendableLocalMoveBy(reactVector, reactTime)
                    //moveButtonlist[i].transform.DOBlendableLocalMoveBy(reactVector, reactTime)
                        .SetEase(reactEase)
                        .OnStart(SetButton_Disable)
                        .OnComplete(DoEnd).Play();
                }
                if (isReverse) isReverseActed = true;
            }
        }

        else
        {
            if (isReverse && isReverseActed)
            {
                for (int i = 0; i < targetObject.Count; i++)
                {
                    moveButtonlist[i].GetComponent<RectTransform>().DOLocalMove(beforereactVector[i], reactTime)
                        .SetEase(reactEase)
                        .OnStart(SetButton_Disable)
                        .OnComplete(DoEnd).Play();
                }
                isReverseActed = false;
            }
            else
            {
                for (int i = 0; i < targetObject.Count; i++)
                {
                    moveButtonlist[i].GetComponent<RectTransform>().DOLocalMove(reactVector, reactTime)
                        .SetEase(reactEase)
                        .OnStart(SetButton_Disable)
                        .OnComplete(DoEnd).Play();
                }
                if (isReverse) isReverseActed = true;
            }
        }
    }
    private void ObjectAction_Move(bool isBy)
    {
        if (isBy)
        {
            if (isReverse && isReverseActed)
            {
                for (int i = 0; i < targetObject.Count; i++)
                {
                    targetObject[i].transform.DOBlendableLocalMoveBy(-1 * reactVector, reactTime)
                        .SetEase(reactEase)
                        .OnStart(SetButton_Disable)
                        .OnComplete(DoEnd).Play();
                }
                isReverseActed = false;
            }
            else
            {
                for (int i = 0; i < targetObject.Count; i++)
                {
                    targetObject[i].transform.DOBlendableLocalMoveBy(reactVector, reactTime)
                        .SetEase(reactEase)
                        .OnStart(SetButton_Disable)
                        .OnComplete(DoEnd).Play();
                }
                if (isReverse) isReverseActed = true;
            }
        }

        else
        { 
            if (isReverse && isReverseActed)
            {
                for (int i = 0; i < targetObject.Count; i++)
                {
                    targetObject[i].transform.DOLocalMove(beforereactVector[i], reactTime)
                        .SetEase(reactEase)
                        .OnStart(SetButton_Disable)
                        .OnComplete(DoEnd).Play();
                }
                isReverseActed = false;
            }
            else
            {
                for (int i = 0; i < targetObject.Count; i++)
                {
                    targetObject[i].transform.DOLocalMove(reactVector, reactTime)
                        .SetEase(reactEase)
                        .OnStart(SetButton_Disable)
                        .OnComplete(DoEnd).Play();
                }
                if (isReverse) isReverseActed = true;
            }
        }
    } 
    private void ObjectAction_Rotate(bool isBy)
    {

        if (isBy)
        {
            if (isReverse && isReverseActed)
            {
                for (int i = 0; i < targetObject.Count; i++)
                {
                    targetObject[i].transform.DOBlendableLocalRotateBy(-1 * reactVector, reactTime, RotateMode.LocalAxisAdd)
                        .SetEase(reactEase)
                        .OnStart(SetButton_Disable)
                        .OnComplete(DoEnd).Play();
                }
                isReverseActed = false;
            }
            else
            {
                for (int i = 0; i < targetObject.Count; i++)
                {
                    targetObject[i].transform.DOBlendableLocalRotateBy(reactVector, reactTime, RotateMode.LocalAxisAdd)
                        .SetEase(reactEase)
                        .OnStart(SetButton_Disable)
                        .OnComplete(DoEnd).Play();
                }
                if (isReverse) isReverseActed = true;
            }
        }
        else
        {

            if (isReverse && isReverseActed)
            {
                for (int i = 0; i < targetObject.Count; i++)
                {
                    targetObject[i].transform.DOLocalRotate(beforereactVector[i], reactTime)
                        .SetEase(reactEase)
                        .OnStart(SetButton_Disable)
                        .OnComplete(DoEnd).Play();
                }
                isReverseActed = false;
            }
            else
            {
                for (int i = 0; i < targetObject.Count; i++)
                {
                    targetObject[i].transform.DOLocalRotate(reactVector, reactTime)
                        .SetEase(reactEase)
                        .OnStart(SetButton_Disable)
                        .OnComplete(DoEnd).Play();
                }
                if (isReverse) isReverseActed = true;
            }
        }
    }

    private void ObjectAction_DoTweenPath()
    {
        tweenPathAnimation.tween.OnStart(SetButton_Disable).OnComplete(DoEnd).Play();
    }

    private void ObjectAction_DoTween()
    {

        if (isSequence)
        {
            //시퀀스인 경우, 순차적으로 발동한다.
            //for test
            Debug.Log("시퀀스인 경우 (구현 필요)");
            //seq = new Sequence()

            seq.PrependCallback(SetButton_Disable);
            for(int i = 0; i < tweenAnimations.Count; i++)
            {
                seq.Prepend(tweenAnimations[i].tween);
                if(sequenceInterval!=0f) seq.PrependInterval(sequenceInterval);
            }
            seq.PrependCallback(DoEnd);
            seq.Play();
        }
        else
        {
            //시퀀스가 아닌 경우에는 동시에 DoTween 을 발동한다.
            if (tweenAnimations.Count == 1)
            {
                tweenAnimations[0].tween.OnStart(SetButton_Disable).OnComplete(DoEnd).Play();
            }

            else if(tweenAnimations.Count > 1)
            {
                SetButton_Disable();
                for (int i = 0; i < tweenAnimations.Count; i++)
                {
                    tweenAnimations[i].tween.Play();
                }
                //for test
                Debug.Log("Step 1");
                StartCoroutine(DoEnd_TweenList());
            }
        }

        
    }    
    IEnumerator DoEnd_TweenList()
    {
        yield return waitTime;      
        DoEnd();
        yield return null;
    }
    private float GetMaxTweenTime()
    {
        float[] tempArr = new float[tweenAnimations.Count];
        for(int i = 0; i < tweenAnimations.Count; i++)
        {
            tempArr[i] = tweenAnimations[i].duration + tweenAnimations[i].delay;
        }
        return Mathf.Max(tempArr);
    }

    private void ObjectAction_FadeIn()
    {
        fadeCanvas.StartFadeIn(this);
    }
    private void ObjectAction_FadeOut()
    {
        fadeCanvas.StartFadeOut(this);
    }
    public void DoEnd_Fade()
    {
        DoEnd();
    }

    private void ObjectAction_PlaySE()
    {
        SoundEffectManager.singleton.PlaySoundEffect(targetSound.name);

        if (isplayafterComplete)
        {
            StartCoroutine(DoEnd_SE());
        }
        else
        {
            DoEnd();
        }
        //for test
        //Debug.Log("소리 완료!");
    }
    IEnumerator DoEnd_SE()
    {
        yield return waitTime;
        //for test
        Debug.Log("코루틴에서의 tempTime = " + tempTime);
        DoEnd();
        yield return null;
    }

    private void ObjectAction_Talk()
    {
        Dialog.singleton.ShowDialog(this);
    }
    public void DoEnd_Talk()
    {
        DoEnd();
    }

    private void ObjectAction_Show()
    {
        /*
        foreach (var e in targetObject)
        {
            e.SetActive(true);
        }
        */
        foreach(var e in targetShowObject)
        {
            e.TargetShow(true);
        }
        DoEnd();
    }
    private void ObjectAction_Hide()
    {
        /*
        foreach (var e in targetObject)
        {
            e.SetActive(false);
        }
        */
        foreach(var e in targetShowObject)
        {
            e.TargetShow(false);
        }
        DoEnd();
    }

    #endregion
    
}
