using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager singleton;
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
    public void Start()
    {
        InitSoundList();
    }

    public List<SoundEffect> list_soundEffect;
    private void InitSoundList()
    {
        list_soundEffect = new List<SoundEffect>();
        SoundEffect[] tempArr = this.GetComponentsInChildren<SoundEffect>();
        for(int i = 0; i < tempArr.Length; i++)
        {
            list_soundEffect.Add(tempArr[i]);
        }
        foreach(var e in list_soundEffect)
        {
            e.InitSoundEffect();
        }
    }

    public void PlaySoundEffect(string soundName)
    {
        SoundEffect tempTarget = list_soundEffect.Find(x => x.name == soundName);
        if(tempTarget == null)
        {
            Debug.LogError(soundName + "이 list_sondEffect 에 등록되어있지 않습니다.");
            return;
        }

        //for test
        //Debug.Log("음악 재생 : " + soundName);    
        tempTarget.sound.Play();
    
    }

    public bool IsPlay(string soundName)
    {
        SoundEffect tempTarget = list_soundEffect.Find(x => x.name == soundName);
        if (tempTarget == null)
        {
            Debug.LogError(soundName + "이 list_sondEffect 에 등록되어있지 않습니다.");
        }

        return tempTarget.sound.isPlaying;
    }

    public float GetPlayTime(SoundEffect soundEffect)
    {
        SoundEffect tempTarget = list_soundEffect.Find(x=> soundEffect);
        return tempTarget.sound.clip.length;
    }
}
