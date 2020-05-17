using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    AsyncOperation asyncOper;
    public OptionChanger optionChanger;
    GameObject optionChangerObj;

    // Start is called before the first frame update

    private void Start()
    {
        if(optionChangerObj == null)
        {
            optionChangerObj = optionChanger.gameObject;
        }
        optionChangerObj.SetActive(false);
    }

    public void OnButtonClick_Start()
    {
        asyncOper = SceneManager.LoadSceneAsync("GameScene");
        

    }
    public void OnButtonClick_Option(bool isOptionShow)
    {
        if (isOptionShow)
        {
            this.gameObject.GetComponent<RectTransform>().localPosition = Vector3.up * 2000;
            optionChangerObj.SetActive(true);
        }
        else
        {
            optionChangerObj.SetActive(false);
            this.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;  
        }
    }
    public void OnButtonClick_Exit()
    {
        Application.Quit();
    }

}
