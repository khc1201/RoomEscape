using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultData : MonoBehaviour
{
    public static DefaultData singleton;
    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(this);
            return;
        }
    }

    // 공통으로 저장하는 데이터이다.
    public WaitForSeconds qParentNumberCheckDelay;
    public AllButtonsLock allButtonLock;
    public WaitForSeconds forTestADWaitTime;

    private void Start()
    {
        InitDefaultData();
    }

    void InitDefaultData()
    {
        qParentNumberCheckDelay = new WaitForSeconds(0.5f);
        allButtonLock = FindObjectOfType<AllButtonsLock>();
        forTestADWaitTime = new WaitForSeconds(3.0f);
    }
}
