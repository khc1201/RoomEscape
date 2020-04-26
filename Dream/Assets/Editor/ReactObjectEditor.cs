using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ReactObject))]
public class ReactObjectEditor : Editor
{
    ReactObject objTarget;
    private SerializedObject soTarget;

    private SerializedProperty p_isRepeat;
    private SerializedProperty p_targetObject;
    private SerializedProperty p_targetShowObject;
    private SerializedProperty p_objectAction;
    private SerializedProperty p_dialogs;
    //private SerializedProperty p_streamData_OnEnd;    
    private SerializedProperty p_targetSound;
    private SerializedProperty p_isImmediately;    
    private SerializedProperty p_reactVector;
    private SerializedProperty p_reactTime;    
    private SerializedProperty p_reactEase;
    private SerializedProperty p_tweenAnimaitions;
    private SerializedProperty p_tweenPathAnimation;
    private SerializedProperty p_isSequence;    
    private SerializedProperty p_sequenceInterval;    
    private SerializedProperty p_seq;    
    private SerializedProperty p_targetCamera;    
    private SerializedProperty p_isQInputObject;    
    private SerializedProperty p_isReverse;
    private SerializedProperty p_isStreamObject;
    private SerializedProperty p_animalType;
    private SerializedProperty p_isRecipt;
    private SerializedProperty p_targetDatas;
    private SerializedProperty p_isTargetLock;
    private SerializedProperty p_motionTarget;
    private SerializedProperty p_motionType;



    string content_repeat = "반복 여부 체크";
    string content_helbox;

    private void OnEnable()
    {
        objTarget = (ReactObject)target;
        soTarget = new SerializedObject(target);
        p_isTargetLock = soTarget.FindProperty("isTargetLock");
        p_objectAction = soTarget.FindProperty("objectAction");
        p_isRepeat = soTarget.FindProperty("isRepeat");
        p_targetObject = soTarget.FindProperty("targetObject");
        p_targetShowObject = soTarget.FindProperty("targetShowObject");
        p_dialogs = soTarget.FindProperty("dialogs");
        //p_streamData_OnEnd = soTarget.FindProperty("streamData_OnEnd");
        p_targetSound = soTarget.FindProperty("targetSound");
        p_isImmediately = soTarget.FindProperty("isImmediately");
        p_reactVector = soTarget.FindProperty("reactVector");
        p_reactTime = soTarget.FindProperty("reactTime");
        p_reactEase = soTarget.FindProperty("reactEase");
        p_tweenAnimaitions = soTarget.FindProperty("tweenAnimations");
        p_tweenPathAnimation = soTarget.FindProperty("tweenPathAnimation");
        p_isSequence = soTarget.FindProperty("isSequence");
        p_sequenceInterval = soTarget.FindProperty("sequenceInterval");
        p_seq = soTarget.FindProperty("seq");
        p_targetCamera = soTarget.FindProperty("targetCamera");
        p_isQInputObject = soTarget.FindProperty("isQInputObject");
        p_isReverse = soTarget.FindProperty("isReverse");
        p_isStreamObject = soTarget.FindProperty("isStreamObject");
        p_animalType = soTarget.FindProperty("animalType");
        p_isRecipt = soTarget.FindProperty("isRecipt");
        p_targetDatas = soTarget.FindProperty("targetDatas");
        p_motionTarget = soTarget.FindProperty("motionTarget");
        p_motionType = soTarget.FindProperty("motionType");
}

    public override void OnInspectorGUI()
    {

        soTarget.Update();
        EditorGUI.BeginChangeCheck();

        GuiEnter();
        GuiText("1. 오브젝트 액션");
        EditorGUILayout.PropertyField(p_objectAction, new GUIContent("액션 목록"));
        //EditorGUILayout.Toggle("설명서", isClicked,new GUIStyle(GUIStyle.none), GUILayout.Toggle);
        EditorGUILayout.PropertyField(p_isRecipt, new GUIContent("설명서 확인"));

        if (objTarget.isRecipt)
        {
            content_helbox = "";
            switch (objTarget.objectAction)
            {
                case enum_ObjectAction.Defalut:
                    {
                        content_helbox = "디폴트 값입니다. 아무런 설정이 되지 않았습니다.";
                        break;
                    }
                case enum_ObjectAction.Talk:
                    {
                        content_helbox = "대사를 출력합니다." +
                            "\n\n※입력 방법" +
                            "\n 1. 엑셀 파일에서 대사 인덱스를 등록" +
                            "\n  1.1. /Assets/03.Table/Excel/String.xlsm 파일" +
                            "\n  1.2. Json 파일을 추출" +
                            "\n  1.3. Json 파일은 /Assets/Resources/02.Json 경로에 붙여넣기" +
                            "\n 2. 대사 개수에 대사의 수를 입력" +
                            "\n 3. 대사 인덱스를 위에서부터 아래로 순서대로 입력";
                        break;
                    }
                case enum_ObjectAction.PlaySoundEffect:
                    {
                        content_helbox = "사운드를 1회 출력합니다." +
                            "\n\n※입력 방법" +
                            "\n 1. Hierarchy - Manager_SoundEffect 하단의 리스트에서 사운드 찾음" +
                            "\n 2. 대상 사운드를 이곳으로 드래그 & 드롭";
                        break;
                    }
                case enum_ObjectAction.FadeIn:
                    {
                        break;
                    }
                case enum_ObjectAction.FadeOut:
                    {
                        break;
                    }
                case enum_ObjectAction.CameraMove:
                    {
                        break;
                    }
                case enum_ObjectAction.Show:
                    {
                        break;
                    }
                case enum_ObjectAction.Hide:
                    {
                        break;
                    }
                case enum_ObjectAction.DoTween:
                    {
                        break;
                    }
                case enum_ObjectAction.MoveBy:
                    {
                        break;
                    }
                case enum_ObjectAction.MoveTo:
                    {
                        break;
                    }
                case enum_ObjectAction.RotateBy:
                    {
                        break;
                    }
                case enum_ObjectAction.RotateTo:
                    {
                        break;
                    }
                case enum_ObjectAction.MoveByButton:
                    {
                        break;
                    }
                case enum_ObjectAction.MoveToButton:
                    {
                        break;
                    }
                case enum_ObjectAction.CompleteStream:
                    {
                        break;
                    }
                case enum_ObjectAction.DoTweenPath:
                    {
                        break;
                    }
                case enum_ObjectAction.AllButtonsActive:
                    {
                        content_helbox = "모든 입력 버튼을 조작 가능으로 설정합니다.\n별도로 입력해야 할 프로퍼티는 없습니다.";
                        break;
                    }
                case enum_ObjectAction.AllButtonsDisable:
                    {
                        content_helbox = "모든 입력 버튼을 조작 불가로 설정합니다.\n별도로 입력해야 할 프로퍼티는 없습니다.";
                        break;
                    }
                case enum_ObjectAction.SetMotion:
                    {
                        break;
                    }

            }
            EditorGUILayout.HelpBox(new GUIContent(content_helbox));
        }
        GuiLine();
        GuiText("2. 오브젝트 액션의 프로퍼티");
        switch (objTarget.objectAction)
        {
            case enum_ObjectAction.Defalut:
                {
                    break;
                }
            case enum_ObjectAction.Talk:
                {
                    //EditorGUILayout.PropertyField(p_dialogs, new GUIContent("대사 인덱스"));
                    DrawList(p_dialogs, "대사 개수", "대사 인덱스");
                    EditorGUILayout.PropertyField(p_isRepeat, new GUIContent(content_repeat));
                    break;
    
                }
            case enum_ObjectAction.PlaySoundEffect:
                {
                    EditorGUILayout.PropertyField(p_targetSound, new GUIContent("실행할 SoundEffect 대상"));
                    
                    EditorGUILayout.PropertyField(p_isRepeat, new GUIContent(content_repeat));
                    break;
                }
            case enum_ObjectAction.FadeIn:
                {
                    EditorGUILayout.PropertyField(p_isImmediately);
                    break;
                }
            case enum_ObjectAction.FadeOut:
                {
                    EditorGUILayout.PropertyField(p_isImmediately);
                    break;
                }
            case enum_ObjectAction.SetMotion:
                {
                    EditorGUILayout.PropertyField(p_motionTarget);
                    EditorGUILayout.PropertyField(p_motionType);

                    break;
                }
            case enum_ObjectAction.CameraMove:
                {
                    EditorGUILayout.PropertyField(p_targetCamera);
                    break;
                }
            case enum_ObjectAction.Show:
                {
                    //EditorGUILayout.PropertyField(p_targetObject);
                    //DrawList(p_targetObject, "대상이 되는 게임 오브젝트", "게임 오브젝트 ");
                    DrawList(p_targetShowObject, "대상이 되는 게임 오브젝트", "게임 오브젝트 ");
                    EditorGUILayout.PropertyField(p_isRepeat, new GUIContent(content_repeat));
                    break;
                }
            case enum_ObjectAction.Hide:
                {

                    //EditorGUILayout.PropertyField(p_targetObject);
                    //DrawList(p_targetObject, "대상이 되는 게임 오브젝트", "게임 오브젝트 ");
                    DrawList(p_targetShowObject, "대상이 되는 게임 오브젝트", "게임 오브젝트 ");
                    EditorGUILayout.PropertyField(p_isRepeat, new GUIContent(content_repeat));
                    break;
                }
            case enum_ObjectAction.DoTween:
                {
                    //EditorGUILayout.PropertyField(p_targetObject);
                    DrawList(p_targetObject, "대상이 되는 게임 오브젝트", "게임 오브젝트 ");
                    //EditorGUILayout.PropertyField(p_tweenAnimaitions);
                    DrawList(p_tweenAnimaitions, "트윈 애니메이션 목록", "트윈 애니메이션");
                    EditorGUILayout.PropertyField(p_isSequence, new GUIContent("시퀀스 여부"));
                    EditorGUILayout.PropertyField(p_sequenceInterval, new GUIContent("시퀀스 사이 간격(초)"));
                    //EditorGUILayout.PropertyField(p_seq);

                    EditorGUILayout.PropertyField(p_isTargetLock, new GUIContent("실행 중 버튼 잠금 여부"));
                    break;
                }
            case enum_ObjectAction.MoveBy:
                {
                    //EditorGUILayout.PropertyField(p_targetObject);
                    DrawList(p_targetObject, "대상이 되는 게임 오브젝트", "게임 오브젝트 ");
                    EditorGUILayout.PropertyField(p_reactVector);
                    EditorGUILayout.PropertyField(p_reactTime);
                    EditorGUILayout.PropertyField(p_reactEase);
                    EditorGUILayout.PropertyField(p_isRepeat, new GUIContent(content_repeat));
                    EditorGUILayout.PropertyField(p_isReverse);

                    EditorGUILayout.PropertyField(p_isTargetLock, new GUIContent("실행 중 버튼 잠금 여부"));
                    break;
                }
            case enum_ObjectAction.MoveTo:
                {

                    //EditorGUILayout.PropertyField(p_targetObject);
                    DrawList(p_targetObject, "대상이 되는 게임 오브젝트", "게임 오브젝트 ");
                    EditorGUILayout.PropertyField(p_reactVector);
                    EditorGUILayout.PropertyField(p_reactTime);
                    EditorGUILayout.PropertyField(p_reactEase);
                    EditorGUILayout.PropertyField(p_isRepeat, new GUIContent(content_repeat));
                    EditorGUILayout.PropertyField(p_isReverse);

                    EditorGUILayout.PropertyField(p_isTargetLock, new GUIContent("실행 중 버튼 잠금 여부"));
                    break;
                }
            case enum_ObjectAction.RotateBy:
                {

                    //EditorGUILayout.PropertyField(p_targetObject);
                    DrawList(p_targetObject, "대상이 되는 게임 오브젝트", "게임 오브젝트 ");
                    EditorGUILayout.PropertyField(p_reactVector);
                    EditorGUILayout.PropertyField(p_reactTime);
                    EditorGUILayout.PropertyField(p_reactEase);
                    EditorGUILayout.PropertyField(p_isRepeat, new GUIContent(content_repeat));
                    EditorGUILayout.PropertyField(p_isReverse);

                    EditorGUILayout.PropertyField(p_isTargetLock, new GUIContent("실행 중 버튼 잠금 여부"));
                    break;
                }
            case enum_ObjectAction.RotateTo:
                {

                    //EditorGUILayout.PropertyField(p_targetObject);
                    DrawList(p_targetObject, "대상이 되는 게임 오브젝트", "게임 오브젝트 ");
                    EditorGUILayout.PropertyField(p_reactVector);
                    EditorGUILayout.PropertyField(p_reactTime);
                    EditorGUILayout.PropertyField(p_reactEase);
                    EditorGUILayout.PropertyField(p_isRepeat, new GUIContent(content_repeat));
                    EditorGUILayout.PropertyField(p_isReverse);

                    EditorGUILayout.PropertyField(p_isTargetLock, new GUIContent("실행 중 버튼 잠금 여부"));
                    break;
                }
            case enum_ObjectAction.MoveByButton:
                {

                    //EditorGUILayout.PropertyField(p_targetObject);
                    DrawList(p_targetObject, "대상이 되는 게임 오브젝트", "게임 오브젝트 ");
                    EditorGUILayout.PropertyField(p_reactVector);
                    EditorGUILayout.PropertyField(p_reactTime);
                    EditorGUILayout.PropertyField(p_reactEase);
                    EditorGUILayout.PropertyField(p_isRepeat, new GUIContent(content_repeat));
                    EditorGUILayout.PropertyField(p_isReverse);

                    EditorGUILayout.PropertyField(p_isTargetLock, new GUIContent("실행 중 버튼 잠금 여부"));
                    break;
                }
            case enum_ObjectAction.DoTweenPath:
                {
                    EditorGUILayout.PropertyField(p_tweenPathAnimation);
                    EditorGUILayout.PropertyField(p_isTargetLock, new GUIContent("실행 중 버튼 잠금 여부"));
                    break;
                }
            case enum_ObjectAction.MoveToButton:
                {

                    //EditorGUILayout.PropertyField(p_targetObject);
                    DrawList(p_targetObject, "대상이 되는 게임 오브젝트", "게임 오브젝트 ");
                    EditorGUILayout.PropertyField(p_reactVector);
                    EditorGUILayout.PropertyField(p_reactTime);
                    EditorGUILayout.PropertyField(p_reactEase);
                    EditorGUILayout.PropertyField(p_isRepeat, new GUIContent(content_repeat));
                    EditorGUILayout.PropertyField(p_isReverse);

                    EditorGUILayout.PropertyField(p_isTargetLock, new GUIContent("실행 중 버튼 잠금 여부"));
                    break;
                }
            case enum_ObjectAction.CompleteStream:
                {
                    DrawList(p_targetDatas, "대상이 되는 스트림 데이터", "스트림 데이터");
                    break;
                }
            case enum_ObjectAction.AllButtonsActive:
                {
                    break;
                }
            case enum_ObjectAction.AllButtonsDisable:
                {
                    break;
                }

        }

        GuiLine();
        //GuiText("3. 완료시 발동할 StreaData");
        //DrawList(p_streamData_OnEnd,"", "StreamData ");
        //GuiLine();
        //GuiText("4. 동물 타입");
        //EditorGUILayout.PropertyField(p_animalType, new GUIContent("동물의 종류"));
        //DrawList(p_animalType, "행동할 동물의 수", "동물 타입");

        if (EditorGUI.EndChangeCheck())
        {
            soTarget.ApplyModifiedProperties();
            GUI.FocusControl(null);
        }
    }
    public void DrawList(SerializedProperty _listProperty, string _listName, string _labelName)
    {
        if(_listProperty.isExpanded == EditorGUILayout.Foldout(_listProperty.isExpanded, _listProperty.name))
        {
            EditorGUILayout.PropertyField(_listProperty.FindPropertyRelative("Array.size"), new GUIContent(_listName));
            int Count = _listProperty.arraySize;
            for (int i = 0; i < Count; ++i)
            {
                EditorGUILayout.PropertyField(_listProperty.GetArrayElementAtIndex(i), new GUIContent(_labelName + i));
            }
        }
    }
    void GuiEnter()
    {
        GuiText(" ");
    }
    void GuiText(string targetString)
    {
        EditorGUILayout.LabelField(targetString, EditorStyles.boldLabel);
    }
    void GuiLine(int i_height = 1)
    {
        GuiEnter();
        Rect rect = EditorGUILayout.GetControlRect(false, i_height);
        rect.height = i_height;
        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
        GuiEnter();
    }
}
