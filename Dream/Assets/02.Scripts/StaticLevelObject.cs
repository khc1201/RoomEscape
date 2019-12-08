using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticLevelObject : MonoBehaviour
{
    [Header("Static Object 에 포함될 수 있는 Child 의 이름")]
    [SerializeField]
    private string str_wall = "Wall";
    [SerializeField]
    private string str_floor = "Floor";
    [SerializeField]
    private string str_object = "Object";

    
    [Header("이 Object 의 속성")]
    [Header("----------------")]
    public bool m_isCeiling = false; // 천장 오브젝트인지의 여부. 천장 오브젝트면 기본적으로 hide / show 를 하지 않는다. 
    [SerializeField]
    private GameObject obj_wall;
    [SerializeField]
    private GameObject obj_floor;
    [SerializeField]
    private GameObject obj_object;

    void Start()
    {
        InitObject();
        
    }

    void InitObject()
    {
        if (m_isCeiling)
            return;

        else
        {
            obj_wall = this.transform.Find(str_wall).gameObject;
            obj_floor = this.transform.Find(str_floor).gameObject;
            obj_object = this.transform.Find(str_object).gameObject;
        }
    }
    
}
