using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ReactObject : MonoBehaviour
{


    [Header("+ 공통 프로퍼티")]
    [SerializeField] private bool isEnd = false;
    public bool IsEnd { get { return isEnd; } }
    public bool isRepeat = false;
    [SerializeField] private bool isExistButton = false;
    [SerializeField] private Button targetButton;

    [Header("+ 대상 GameObject")]
    public List<GameObject> m_targetObject;

    [Header("+ ReactObject 행동")]
    public enum_ObjectAction m_objectAction;
    public float endCheckingTick = 0.5f;

    [Header("+ Case : Talk")]
    public List<string> dialogs;
    public List<StreamData> streamData_onEndDialog;

    [Header("+ Case : PlaySoundEffect")]
    public SoundEffect targetSound;

    [Header("+ Case : FadeIn/FadeOut")]
    public bool isImmediately;

    [Header("+ Case : MoveBy/MoveTo")]
    public Vector3 moveVector;
    public float moveTime;
    public Ease moveEase;
    private List<Vector3> beforemoveVector;

    [Header("+ Case : RotateBy/RotateTo")]
    public Vector3 rotateVector;
    public float rotateTime;
    public Ease rotateEase;
    private List<Vector3> beforerotateVector;


    [Header("+ Case : DoTween")]
    public List<DOTweenAnimation> tweenAnimations;
    public bool isSequence = false; // 순서대로 발동 여부
    public float sequenceInterval = 0f;
    public Sequence seq;

    [Header("+ QInput 에 종속된 데이터")]
    [HideInInspector] public bool isQInputObject = false;
    [Header("++ 역재생")]
    public bool isReverse = false;
    public bool isReverseActed = false;

    [Header("+ SteramObject 에 종속된 데이터")]
    [Header("++ 초기화 시 무시되는 데이터인가?")]
    [SerializeField] private bool isIgnoreOnLoad = true;
    [HideInInspector] public bool isStreamObject = false;
    [SerializeField] private bool isStreamCompletedonLoad = false;
    private bool isplayafterComplete = false;


    public bool IsIgnoreOnLoad
    {
        get
        {
            return isIgnoreOnLoad;
        }
    }

    public void Start()
    {
        /*
        if (!CheckValid())
        {
            DevDescriptionManager.singleton.LogNullError(this.gameObject, "CheckValid");
            return;
        }
        */
        CheckIsIgnoreOnLoad();
        InitObjectAction();
        
    }

    public void DoAction(bool _isplayafterComplete, Button _targetButton = null, bool _isStreamObject = false, bool _isStreamObjectComplete = false)
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
            if (_isStreamObjectComplete)
            {
                isStreamCompletedonLoad = true;
                //for test
                Debug.Log(this.gameObject.name + "은 StreamObject 이면서 현재 완료되었으므로 Load 시에 즉시 실행합니다.");
            }
        }

        switch (m_objectAction)
        {
            case enum_ObjectAction.Defalut:
                {
                    Debug.LogError(this.gameObject.name + "의 m_objectAction 이 설정되어있지 않다 (default)");
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

            case enum_ObjectAction.Init:
                {
                    ObjectAction_Init();
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
            default:
                {
                    Debug.Log("해당 " + m_objectAction.ToString() + "은 아직 구현되지 않았습니다.");
                    break;
                }
                
        }
    }

    //SetEnable_TargetButton() 구현이 필요함.
    private void SetButton_Disable()
    {
        if (isExistButton && isRepeat) targetButton.enabled = false;
        
    }
    /*
    private void SetButton_Enable()
    {
        if (isExistButton && isRepeat) targetButton.enabled = true;
    }
    */

    private void DoEnd()
    {
        if(isStreamObject) isEnd = true;
        if (isExistButton && isRepeat) targetButton.enabled = true;
    }
    #region ObjectAction - Init
    private void InitObjectAction()
    {
        if (isReverse && !isRepeat)
        {
            Debug.LogError(this.gameObject.name + "설정 에러 : isReverse 는 체크가 되어있는데, isRepeat 가 체크되지 않았다.");
        }
        switch (m_objectAction)
        {
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
        }
    }
    private void Init_Tween()
    {
        if (tweenAnimations == null)
        {
            Debug.LogError(this.gameObject.name + "의 ObjectAction_DoTween 을 실행했으나, tweenAnimation 이 할당되어있지 않다. null 값임");
            return;
        }
    }
    private void Init_Move(bool isBy)
    {

        if (moveVector == Vector3.zero)
        {
            Debug.Log(this.gameObject.name + "의 moveVector 가 설정되어있지 않음. 의도임?");
        }
        if (moveEase == null)
        {
            Debug.Log(this.gameObject.name + "의 moveEase 가 설정되어있지 않음. 내 맘대로 linear 로 바꿀 거임.");
            moveEase = Ease.Linear;
        }
        if (moveTime == 0f)
        {
            Debug.Log(this.gameObject.name + "의 moveTime 이 0 임. 의도임?");
        }

        if (!isBy)
        {
            beforemoveVector = new List<Vector3>();
            for (int i = 0; i < m_targetObject.Count; i++)
            {
                beforemoveVector.Add(m_targetObject[i].transform.localPosition);
            }
        }
    }
    private void Init_Rotate(bool isBy)
    {
        if (rotateVector == Vector3.zero)
        {
            Debug.Log(this.gameObject.name + "의 rotateVector 가 설정되어있지 않음. 의도임?");
        }
        if (rotateEase == null)
        {
            Debug.Log(this.gameObject.name + "의 rotateEase 가 설정되어있지 않음. 내 맘대로 linear 로 바꿀 거임.");
            rotateEase = Ease.Linear;
        }
        if (rotateTime == 0f)
        {
            Debug.Log(this.gameObject.name + "의 rotateTime 이 0 임. 의도임?");
        }

        if (!isBy)
        {
            beforerotateVector = new List<Vector3>();
            for (int i = 0; i < m_targetObject.Count; i++)
            {
                beforerotateVector.Add(m_targetObject[i].transform.localRotation.eulerAngles);
            }
        }

    }
    #endregion

    #region ObjectAciton - DoAction

    private void ObjectAction_Move(bool isBy)
    {
        if (isBy)
        {
            if (isReverse && isReverseActed)
            {
                for (int i = 0; i < m_targetObject.Count; i++)
                {
                    m_targetObject[i].transform.DOBlendableLocalMoveBy(-1 * moveVector, moveTime)
                        .SetEase(moveEase)
                        .OnStart(SetButton_Disable)
                        .OnComplete(DoEnd).Play();
                }
                isReverseActed = false;
            }
            else
            {
                for (int i = 0; i < m_targetObject.Count; i++)
                {
                    m_targetObject[i].transform.DOBlendableLocalMoveBy(moveVector, moveTime)
                        .SetEase(moveEase)
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
                for (int i = 0; i < m_targetObject.Count; i++)
                {
                    m_targetObject[i].transform.DOLocalMove(beforemoveVector[i], moveTime)
                        .SetEase(moveEase)
                        .OnStart(SetButton_Disable)
                        .OnComplete(DoEnd).Play();
                }
                isReverseActed = false;
            }
            else
            {
                for (int i = 0; i < m_targetObject.Count; i++)
                {
                    m_targetObject[i].transform.DOLocalMove(moveVector, moveTime)
                        .SetEase(moveEase)
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
                for (int i = 0; i < m_targetObject.Count; i++)
                {
                    m_targetObject[i].transform.DOBlendableLocalRotateBy(-1 * rotateVector, rotateTime, RotateMode.LocalAxisAdd)
                        .SetEase(rotateEase)
                        .OnStart(SetButton_Disable)
                        .OnComplete(DoEnd).Play();
                }
                isReverseActed = false;
            }
            else
            {
                for (int i = 0; i < m_targetObject.Count; i++)
                {
                    m_targetObject[i].transform.DOBlendableLocalRotateBy(rotateVector, rotateTime, RotateMode.LocalAxisAdd)
                        .SetEase(rotateEase)
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
                for (int i = 0; i < m_targetObject.Count; i++)
                {
                    m_targetObject[i].transform.DOLocalRotate(beforerotateVector[i], rotateTime)
                        .SetEase(rotateEase)
                        .OnStart(SetButton_Disable)
                        .OnComplete(DoEnd).Play();
                }
                isReverseActed = false;
            }
            else
            {
                for (int i = 0; i < m_targetObject.Count; i++)
                {
                    m_targetObject[i].transform.DOLocalRotate(rotateVector, rotateTime)
                        .SetEase(rotateEase)
                        .OnStart(SetButton_Disable)
                        .OnComplete(DoEnd).Play();
                }
                if (isReverse) isReverseActed = true;
            }
        }
    }

    private void ObjectAction_DoTween()
    {

        if (!isIgnoreOnLoad)
        {
            //for test
            Debug.Log(this.gameObject.name + "은 Load 시에 Ignore 하지 않으므로, 즉시 행동을 실행한다. 이때 시간은 0.0f");

        }

        if (isSequence)
        {
            //시퀀스인 경우, 순차적으로 발동한다.
            //for test
            Debug.Log("시퀀스인 경우 (구현 필요)");
            //seq = new Sequence();
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
            //ㅅ퀀스가 아닌 경우에는 동시에 DoTween 을 발동한다.
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
                StartCoroutine(DoEnd_TweenList());
            }
        }

        
    }
    IEnumerator DoEnd_TweenList()
    {;   
        while (!isEnd)
        {
            if (tweenAnimations.TrueForAll(x => x.tween.IsComplete()))
            {
                DoEnd();
            }
            else
            {
                yield return new WaitForSeconds(endCheckingTick);
            }
        }
        yield return null;
    }
    private void DoInit_TweenList()
    {
        //for test
        Debug.Log("반복 가능 설정 여부에 따라 해당 Tween 을 반복 가능한 상태로 설정 (구현 필요)");
        for(int i = 0; i < tweenAnimations.Count; i++)
        {

        }
    }


    private void ObjectAction_FadeIn()
    {
        FadeCanvas fadeCanvas = FindObjectOfType<FadeCanvas>();
        
        fadeCanvas.StartFadeIn(this);
        if (isplayafterComplete)
        {
            //StartCoroutine(DoEnd_Fade(fadeCanvas));
        }
    }
    private void ObjectAction_FadeOut()
    {
        FadeCanvas fadeCanvas = FindObjectOfType<FadeCanvas>();
        fadeCanvas.StartFadeOut(this);
        if (isplayafterComplete)
        {
            //StartCoroutine(DoEnd_Fade(fadeCanvas));
        }
    }
    public void DoEnd_Fade()
    {
        isEnd = true;
    }

    private void ObjectAction_PlaySE()
    {
        SoundEffectManager.singleton.PlaySoundEffect(targetSound.name);

        if (isplayafterComplete)
        {
            StartCoroutine(DoEnd_SE());
        }
        //for test
        Debug.Log("소리 완료!");
    }

    IEnumerator DoEnd_SE()
    {
        while (!isEnd)
        {
            if (!SoundEffectManager.singleton.IsPlay(targetSound.name))
            {
                isEnd = true;
            }
            yield return new WaitForSeconds(endCheckingTick);
        }
        yield return null;
    }


    private void ObjectAction_Talk()
    {
        Dialog.singleton.ShowDialog(this);
    }
    public void DoEnd_Talk()
    {
        isEnd = true;
    }


    public void OjbectAction_Talk_onComplete()
    {
        if (streamData_onEndDialog != null)
        {
            foreach (var e in streamData_onEndDialog)
            {
                //for test
                Debug.Log("talk 완료에 따라 " + e.index + " 의 complete 발동!");
                e.CompleteStream();
            }
        }
    }

    private void ObjectAction_Show()
    {
        foreach (var e in m_targetObject)
        {
            e.SetActive(true);
        }
    }

    private void ObjectAction_Hide()
    {
        foreach (var e in m_targetObject)
        {
            e.SetActive(false);
        }
    }

    private void ObjectAction_Init()
    {
        //for test
        Debug.Log("Init 을 구현해야 합니다! - 게임을 처음 켰을 때 Load 된 정보에 따라서 해당 프랍을 show / hide / move ... 시켜야 합니다");


    }
    #endregion


    private void CheckIsIgnoreOnLoad()
    {
        if (isQInputObject)
        {
            //for test
            //Debug.Log(this.gameObject.name + "의 속성이 QInputObject 에 종속된 데이터이므로, 모든 값을 Ignore 한다.");
            isIgnoreOnLoad = true;
        }
        else if(isStreamObject)
        {
            //for test
            //Debug.Log(this.gameObject.name + "의 속성이 StreamObject 에 종속된 데이터이므로, 선별적으로 값을 Ignore 한다.");
            //enum_ObjectAction 의 규칙 : 50 보다 작으면 Ignore 하는 데이터
            isIgnoreOnLoad = (int)m_objectAction < 50 ? true : false;
        }


    }
}
