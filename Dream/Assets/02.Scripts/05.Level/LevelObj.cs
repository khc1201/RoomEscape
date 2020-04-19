using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObj : MonoBehaviour
{
    [SerializeField] private List<GameObject> list_gameObject;

    public void Start()
    {
        InitLevelObj();
    }

    private void InitLevelObj()
    {
        list_gameObject = new List<GameObject>();
        list_gameObject.Add(this.transform.Find("Ceiling").gameObject);
        list_gameObject.Add(this.transform.Find("Wall").gameObject);
        list_gameObject.Add(this.transform.Find("Floor").gameObject);
        list_gameObject.Add(this.transform.Find("Object").gameObject);
        list_gameObject.Add(this.transform.Find("Light").gameObject);
    }

    public void SetActiveObject(bool isActive)
    {
        for (int i = 0; i < list_gameObject.Count; i++)
        {
            list_gameObject[i].SetActive(isActive);
        }
    }
}
