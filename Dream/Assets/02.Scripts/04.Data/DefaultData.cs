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
    public WaitForEndOfFrame waitForEndofFrame;
    public int LanguageCount; //현재 번역할 수 있는 언어의 최대값
    public Vector3 upperUIValue; // 위로 UI 가 사라질 수 있는 최댓값

    private void Start()
    {
        InitDefaultData();
    }

    private void OnLevelWasLoaded(int level)
    {
        InitDefaultData();
    }

    void InitDefaultData()
    {
        if(qParentNumberCheckDelay == null) qParentNumberCheckDelay = new WaitForSeconds(0.5f);
        if(allButtonLock == null && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "GameScene") allButtonLock = FindObjectOfType<AllButtonsLock>();
        if(forTestADWaitTime == null) forTestADWaitTime = new WaitForSeconds(3.0f);
        if(waitForEndofFrame == null) waitForEndofFrame = new WaitForEndOfFrame();
        LanguageCount = 6;
        upperUIValue = new Vector3(0, 2000, 0);
    }
}
