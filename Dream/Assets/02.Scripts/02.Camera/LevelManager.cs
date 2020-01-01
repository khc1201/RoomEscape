using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager singleton;
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(this);
            return;
        }
    }
    public List<GameObject> m_list_gameobject;
    public void Start()
    {
        m_list_gameobject = new List<GameObject>();
    }

    public void ShowLevelObject(StaticLevelObject target, bool isshow)
    {
        target.SetActiveObject(isshow:isshow);
    }


    #region 레벨 최적화
    

    #endregion

}
