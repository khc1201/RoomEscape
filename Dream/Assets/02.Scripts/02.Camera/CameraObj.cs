using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObj : MonoBehaviour
{
    [Header("+ 기본 정보")]
    [SerializeField]
    private Camera m_cam;
    [SerializeField]
    private GameObject m_ui;

    [Header("+ 방향 조작에 따른 카메라 이동")]
    public CameraObj m_dircamobj_forward;
    public CameraObj m_dircamobj_left;
    public CameraObj m_dircamobj_back;
    public CameraObj m_dircamobj_right;

    [Header("+ 보여야 하는 Object 리스트")]
    public List<GameObject> m_showLevelObject;


    // Start is called before the first frame update
    void Start()
    {
        m_cam = transform.GetComponentInChildren<Camera>();
    }

    public void SetActiveObject()
    {

    }

    public void CameraMove(MoveDir direction)
    {
        switch (direction)
        {
            case MoveDir.Forward:
                {
                    if (m_dircamobj_forward == null)
                    {
                        Debug.LogError(string.Format($"{this.gameObject.name} 의 forward camera 가 null 입니다."));
                        return;
                    }
                    break;
                }
            case MoveDir.Left:
                {
                    if (m_dircamobj_left == null)
                    {
                        Debug.LogError(string.Format($"{this.gameObject.name} 의 left camera 가 null 입니다."));
                        return;
                    }
                    break;
                }
            case MoveDir.Back:
                {
                    if (m_dircamobj_back == null)
                    {
                        Debug.LogError(string.Format($"{this.gameObject.name} 의 back camera 가 null 입니다."));
                        return;
                    }
                    break;
                }
            case MoveDir.Right:
                {
                    if (m_dircamobj_right == null)
                    {
                        Debug.LogError(string.Format($"{this.gameObject.name} 의 right camera 가 null 입니다."));
                        return;
                    }
                    break;
                }
        }
    }
}
