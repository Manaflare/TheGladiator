using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class ListCreditInfo
{
    public List<Credit> creditList;
}

[System.Serializable]
public class Credit
{
    public string title;
    public string desc;
}

public class CreditText : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private float speed = 1.5f;

    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Text descText;

    [SerializeField]
    private AudioClip backgroundMusic;
    private Vector2 originPos;
    private Credit[] creditData;
    private int index_count;
    private bool bMoving = false;
    void Start () {
        MasterManager.ManagerSound.PlayBackgroundMusic(backgroundMusic);
        index_count = -1;
        originPos = GetComponent<RectTransform>().anchoredPosition;
        creditData = MasterManager.ManagerGlobalData.GetAllCreditData().creditList.ToArray();

        SetNextScript();


    }
	
	// Update is called once per frame
	void Update () {
        if (index_count == creditData.Length  || bMoving)
            return;

        RectTransform rt = GetComponent<RectTransform>();
        if(rt.anchoredPosition.y <= -10)
           GetComponent<RectTransform>().anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y + speed);
        else
        {
            bMoving = true;
            Invoke("SetNextScript", 5.0f);
        }
	}


    private void SetNextScript()
    {
        if (index_count >= creditData.Length - 1)
            return;

        index_count++;
        bMoving = false;
        GetComponent<RectTransform>().anchoredPosition = originPos;

        titleText.text = creditData[index_count].title;
        descText.text = creditData[index_count].desc;
    }

    public void GoBackToIntro()
    {
        MasterManager.ManagerSound.StopBackgroundMusic();
        MasterManager.ManagerLoadScene.LoadScene("MainMenu",false);
    }

}
