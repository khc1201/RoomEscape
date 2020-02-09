using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDir
{
    Forward,
    Left,
    Back,
    Right
}

public enum enum_Language
{
    Korean,
    English,
    Chinese,
    Japanese
}

public enum enum_EventType
{
    Complete_StreamData,
    Init_StreamData
};

public enum enum_ShowHide
{
    SHOW,
    HIDE
}

public enum enum_ObjectAction
{
    //enum_ObjectAction 의 규칙 : 50 보다 작으면 Ignore 하는 데이터
    Defalut,
    Talk,
    PlaySoundEffect,
    FadeIn,
    FadeOut,
    CameraMove,
    AllButtonsActive,
    AllButtonsDisable,

    //이 아래에서 부터는 Load 시에 수행 되어야하는 Action 이다.
    Show = 51,
    Hide = 52,
    CompleteStream = 53, 
    DoTween = 54, // QInput 인 경우에는 무시하도록 한다.
    MoveBy = 55,
    MoveTo = 56,
    RotateBy = 57,
    RotateTo = 58,
    MoveByButton = 59,
    MoveToButton = 60,
}

public enum enum_AnswerType
{
    Default,
    Number,
    Click //의미 없이 그냥 클릭만 해서 react 를 유도하는 것
}

public enum enum_AnimalType
{
    Default,
    Dog_Retriever,
    Dog_Chihuahua,
    Cat_Normal,
    Error
}

public enum enum_ObjectType
{
    //Stream Show / Hide 를 위해서
    Default,
    GameObject,
    Button
}
