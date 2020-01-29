using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ReactObject : MonoBehaviour
{
    public bool isReverse = false;
    public bool isReverseActed = false;
    public List<DOTweenAnimation> tweenAnimations;

    public void OnReact()
    {
        DoTweening();
    }


    private void DoTweening()
    {
        for(int i = 0; i < tweenAnimations.Count; i++)
        {
            if (isReverse)
            {
                if (!isReverseActed) tweenAnimations[i].DOPlay();
                else tweenAnimations[i].DORewind();
            }
            else
            {
                tweenAnimations[i].DOPlay();
            }
        }
    }
}
