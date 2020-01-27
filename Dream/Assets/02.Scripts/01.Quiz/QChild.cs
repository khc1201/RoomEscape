using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QChild : MonoBehaviour
{
    public List<QInput> qInputs;
    private void Start()
    {
        InitList();
    }
    void InitList()
    {
        qInputs = new List<QInput>();
        QInput[] tempInputs = this.GetComponentsInChildren<QInput>();
        for (int i = 0; i < tempInputs.Length; i++)
        {
            qInputs.Add(tempInputs[i]);
        }
    }
}
