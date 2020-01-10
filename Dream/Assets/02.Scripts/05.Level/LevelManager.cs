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
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(this.gameObject);
            return;
        }
    }
    [Header("+ 모든 LevelObj 목록")]
    public GameObject m_levelParent;
    [SerializeField] private List<GameObject> list_gameobject;
    [Header("+ 레벨 최적화 목록 (직접 등록)")]
    public List<LevelOptimization> list_levelopt;

    public void Start()
    {
        InitLevel();
    }

    public void InitLevel()
    {
        if(m_levelParent == null)
        {
            Debug.LogError("m_levelParent 가 null 이다");
            return;
        }
        list_gameobject = new List<GameObject>();
        foreach (var e in m_levelParent.transform.GetComponentsInChildren<LevelObj>())
        {
            list_gameobject.Add(e.gameObject);
        }
    }


    #region 레벨 최적화
    public void InitLevelOptimization()
    {
        //CameraManager 에서 해당 작업을 수행한다.
        ChangeLevelOptimization(CameraManager.singleton.m_nowCamera.m_levelOptimizationIndex);

    }
    public void ChangeLevelOptimization(string targetIndex)
    {
        //CameraManager 에서 해당 작업을 수행한다.
        LevelOptimization tempOpt = list_levelopt.Find(x => x.m_index == targetIndex);

        foreach (var e in list_gameobject)
        {
            LevelObj tempObj = e.GetComponent<LevelObj>();
            if (tempOpt.list_showLevel.Contains(tempObj))
            {
                tempObj.SetActiveObject(true);
            }
            else
            {
                tempObj.SetActiveObject(false);
            }
        }
    }
    #endregion

}
