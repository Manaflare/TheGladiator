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

    public Image enemySprite;

    // Use this for initialization
    void Start()
    {
        playerDataInfo = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        currentTier = playerDataInfo.playerTier == 0 ? 1 : playerDataInfo.playerTier;

        enemyDataInfo = MasterManager.ManagerGlobalData.GetEnemyDataInfo();
        enemyData = new List<ListDataInfo>();

        Button btn = test.GetComponent<Button>();
        btn.onClick.AddListener(findRandomEnemyData);
    }

    // Update is called once per frame
    void Update()
    {

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
            
        enemyHP.text = enemyData[enemyID].statsList[0].MAXHP.ToString();
        enemySTR.text = enemyData[enemyID].statsList[0].Strength.ToString();
        enemyAGI.text = enemyData[enemyID].statsList[0].Agility.ToString();
        enemyDEX.text = enemyData[enemyID].statsList[0].Dexterity.ToString();
        enemySTA.text = enemyData[enemyID].statsList[0].MaxStamina.ToString();
    }
    
    void showSprite()
    {

    }
}