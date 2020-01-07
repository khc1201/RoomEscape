﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMoveButton : MonoBehaviour
{
    [SerializeField]
    private Button m_button;
    private GameObject m_text;
    private CameraManager m_cameraManager;

    [Header("+ 클릭시 CameraManager 로 보낼 정보")]
    public MoveDir m_moveDir;

    private void Start()
    {
        InitButton();
    }

    void InitButton()
    {
        m_button = transform.GetComponent<Button>();
        m_text = transform.GetComponentInChildren<Text>().gameObject;
        m_cameraManager = FindObjectOfType<CameraManager>();
    }
    public void SetButton(bool isTrue)
    {
        m_button.enabled = isTrue;
        m_text.SetActive(isTrue);
    }

    public void OnClickButton()
    {
        m_cameraManager.CameraMove(m_moveDir);
    }
}