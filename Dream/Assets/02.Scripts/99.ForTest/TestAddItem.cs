using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAddItem : MonoBehaviour
{
    Button thisButton;
    public StreamData addcompletestream;
    // Start is called before the first frame update
    void Start()
    {
        thisButton = this.transform.GetComponent<Button>();
        thisButton.onClick.AddListener(OnTest);
    }
    public void OnTest()
    {
        if(addcompletestream == null)
        {
            Debug.LogError("addcompletestream 이 null 이다");
            return;
        }

        //for test
        Debug.Log("STEP 1");

        StreamDataManager.singleton.CompleteStream(addcompletestream);
    }

}
