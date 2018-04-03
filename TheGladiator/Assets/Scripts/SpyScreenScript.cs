using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//currently set to use a winstreak to call it, not really properly set up yet
public class SpyScreenScript : MonoBehaviour
{
    private ListDataInfo playerDataInfo;
    private ListEnemiesInfo enemyDataInfo;
    List<ListDataInfo> enemyData;

    private int currentTier;

    public Button test;

    public Text enemyName;
    public Text enemyHP;
    public Text enemySTR;
    public Text enemyAGI;
    public Text enemyDEX;
    public Text enemySTA;

    public Image bodySprite;
    public Image hairSprite;
    public Image facialHairSprite;

    public GameObject result;
    public GameObject popUp;
    // Use this for initialization
    void Start()
    {
        playerDataInfo = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        currentTier = playerDataInfo.playerTier == 0 ? 1 : playerDataInfo.playerTier;

        enemyDataInfo = MasterManager.ManagerGlobalData.GetEnemyDataInfo();
        enemyData = new List<ListDataInfo>();

        //Button btn = test.GetComponent<Button>();
        //btn.onClick.AddListener(findRandomEnemyData);
        findRandomEnemyData();
    }
    public void Cancel()
    {
        TownManager.Instance.CloseCurrentWindow(false);
    }
    public void Continue()
    {
        TownManager.Instance.CloseCurrentWindow(true);
    }
    public void OnEnable()
    {
        result.SetActive(false);
        popUp.SetActive(true);
    }
    public void SpendGold()
    {
        if (MasterManager.ManagerGlobalData.GetEnvData().gold >= 5)
        {
            MasterManager.ManagerGlobalData.GetEnvData().gold -= 5;
            MasterManager.ManagerGlobalData.SaveEnvData();
            result.SetActive(true);
            popUp.SetActive(false);
            Start();
        }
        else
        {
            MasterManager.ManagerPopup.ShowMessageBox("Oh No!","Sorry you don't have enough gold for this",Constants.PopupType.POPUP_NO);
        }
    }


    void continueButtonClick()
    {
        this.transform.root.gameObject.SetActive(false);
    }

    void findRandomEnemyData()
    { 
        for (int i = 0; i < enemyDataInfo.enemyData.Count; i++)
        {
            if (enemyDataInfo.enemyData[i].playerTier == currentTier)
            {
                enemyData.Add(enemyDataInfo.enemyData[i]);
            }
        }

        int enemyID = Random.Range(0, enemyData.Count - 1);

        enemyName.text = enemyData[enemyID].statsList[0].Name.ToString();
            
        enemyHP.text = (enemyData[enemyID].statsList[0].MAXHP * Constants.HP_MULTIPLIER).ToString();
        enemySTR.text = enemyData[enemyID].statsList[0].Strength.ToString();
        enemyAGI.text = enemyData[enemyID].statsList[0].Agility.ToString();
        enemyDEX.text = enemyData[enemyID].statsList[0].Dexterity.ToString();
        enemySTA.text = enemyData[enemyID].statsList[0].MaxStamina.ToString();

        bodySprite.sprite = MasterManager.ManagerSprite.BodyList[enemyData[enemyID].spriteList[0].BodyIndex];
        hairSprite.sprite = MasterManager.ManagerSprite.HairList[enemyData[enemyID].spriteList[0].HairIndex];
        facialHairSprite.sprite = MasterManager.ManagerSprite.FacialHairList[enemyData[enemyID].spriteList[0].FaceHairIndex];
        if (enemyData[enemyID].spriteList[0].HairIndex == 0)
        {
            hairSprite.color = new Color(1, 1, 1, 0);
        }
        else
        {
            hairSprite.color = new Color(1, 1, 1, 1);
        }
        if (enemyData[enemyID].spriteList[0].FaceHairIndex == 0)
        {
            facialHairSprite.color = new Color(1, 1, 1, 0);
        }
        else
        {
            hairSprite.color = new Color(1, 1, 1, 1);
        }
    }
    
    void showSprite()
    {

    }
}