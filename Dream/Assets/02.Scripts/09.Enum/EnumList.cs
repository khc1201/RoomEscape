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

public enum EVENT_TYPE
{
    Complete_StreamData
};

public enum enum_ObjectAction
{
    //enum_ObjectAction 의 규칙 : 50 보다 작으면 Ignore 하는 데이터


    //이 아래에서 부터는 Load 시에 수행 되어야하는 Action 이다.
    Show = 51,
    Hide = 52,
    Init = 53
}

public enum enum_AnswerType
{
    Default,
    Number
}
