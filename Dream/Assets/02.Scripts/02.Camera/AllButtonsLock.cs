using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllButtonsLock : MonoBehaviour
{
    private RawImage lockImage;
    private bool isLocked = false;
    public bool IsLocked { get { return isLocked; } set { isLocked = value; } }

    private void Start()
    {
        InitLock();
    }

    private void InitLock()
    {
        lockImage = this.GetComponentInChildren<RawImage>();
        lockImage.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
        lockImage.enabled = false;
    }

    public void SetActive_LockImage(bool isAcitve)
    {
        //for test
        Debug.Log("Step 4 / isActive = " + isAcitve);
        isLocked = !isAcitve;
        lockImage.enabled = isAcitve;
    }
}
