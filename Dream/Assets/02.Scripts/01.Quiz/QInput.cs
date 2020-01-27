using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QInput : MonoBehaviour
{

    [Header("+ 버튼 클릭 시 입력되어야 하는 항목")]
    public string input;

    [Header("+ 프로퍼티 (미입력 항목)")]
    [SerializeField] int index;
    private QParent qParent;
    private Button qButton;

    private void Start()
    {
        if(input == null)
        {
            Debug.LogError(this.gameObject.name + "의 input string 이 null 이다.");
            return;
        }
        InitProperties();
    }

    private void InitProperties()
    {

        index = this.gameObject.transform.GetSiblingIndex();
        qParent = this.transform.GetComponentInParent<QParent>();

        qButton = this.transform.GetComponentInChildren<Button>();
        if(qButton != null)
        {
            qButton.onClick.RemoveAllListeners();
            qButton.onClick.AddListener(OnButtonClick);
        }
    }

    public void OnButtonClick()
    {
        qParent.OnInput(childIndex: index, childInput: input);
    }

}
