using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionTarget : MonoBehaviour
{
    private Animator controller;

    public enum_MotionType initMotion;

    string[] mTypes = { "Idle", "Walk", "Special_1", "Special_2", "Special_3", "Run", "SitDown" };
    float[] mTimes = { 0, 0, 0, 0, 0, 0, 2 };

    private void Start()
    {
        InitController();
        InitMotion();
        InitTime();
    }

    private void InitController()
    {
        this.controller = GetComponent<Animator>();
        
        if(controller == null)
        {
            Debug.LogError(this.gameObject.name + "의 animationController 가 null 입니다.");
            return;
        }
    }

    private void InitMotion()
    {
        this.controller.SetInteger("NowStatus", (int)initMotion);
        
    }

    private void InitTime()
    {
        AnimationClip[] tempClips = controller.runtimeAnimatorController.animationClips;
        for (int i = 0; i < tempClips.Length; i++)
        {
            
            for(int j = 0; j<mTypes.Length; j++)
            {
                if(tempClips[i].name == mTypes[j])
                {

                    //for test
                    Debug.Log(string.Format($"{i}번째 tempClips인 {tempClips[i]} //// {j}번째 mTypes인 {mTypes[j]} 를 비교하여 tempClips의 시간인 {tempClips[i].length} 를 더한다"));
                    

                    mTimes[j] += tempClips[i].averageDuration;
                    break;
                }
            }
        }
    }

    public float GetAnimSeconds(enum_MotionType mType)
    {
        


        foreach(var e in controller.runtimeAnimatorController.animationClips)
        {
            //for test
            Debug.Log(string.Format($"{e.name} 과 {mTypes[(int)mType]} 을 비교하여..."));

            if (mTypes[(int)mType] == e.name)
            {
                //for test
                Debug.Log("같으므로 e 의 시간인 " + e.length + "을 반환한다");

                if(mType == enum_MotionType.SitDown)
                {
                    
                }

                return e.length;
               
            }
        }
        return 0;
    }

    public void SetMotion(enum_MotionType mType)
    {
        this.controller.SetInteger("NowStatus", (int)mType);
    }
}
