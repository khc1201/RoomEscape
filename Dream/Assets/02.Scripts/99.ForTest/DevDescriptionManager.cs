using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevDescriptionManager : MonoBehaviour
{
    public static DevDescriptionManager singleton;
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
        }
        m_descriptions = new List<DevDescription>();
    }
    public List<DevDescription> m_descriptions;
    public void AddDescription(DevDescription target)
    {
        m_descriptions.Add(target);
    }

    [Header("+ ForTest 전용 텍스트 출력 여부")]
    public bool m_isFortestConsoleShow = true;
}
