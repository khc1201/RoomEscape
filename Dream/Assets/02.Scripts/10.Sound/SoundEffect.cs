using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundEffect : MonoBehaviour
{ 
    public string name;
    public AudioSource sound;


    public void InitSoundEffect()
    {
        sound = this.GetComponent<AudioSource>();
        if(sound == null)
        {
            Debug.LogError(this.gameObject.name + "의 sound 가 null 이다");
            return;
        }
        this.gameObject.name = this.name;
    }
}
