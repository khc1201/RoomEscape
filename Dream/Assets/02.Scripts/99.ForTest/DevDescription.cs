using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevDescription : MonoBehaviour
{
    // 이거는 진짜 설명용...
    public string memo;

    public GameObject target;

    public void Start()
    {
        if (this.gameObject == null) return;
        target = this.gameObject;

        FindObjectOfType<DevDescriptionManager>().AddDescription(this);
    }
}
