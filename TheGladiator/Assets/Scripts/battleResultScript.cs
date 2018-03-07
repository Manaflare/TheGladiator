using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleResultScript : MonoBehaviour {
    [HideInInspector]
    public GameObject Player1;
    [HideInInspector]
    public GameObject Player2;

    public Text[] winnerStats;
    public Text[] loserStats;

    public Text WinnerName;
    public Text LoserName;

    [Range(0.0f, 5.0f)]
    public float duration;
    private float onScreenTime = 0.0f;
	// Use this for initialization

    void setStatValues(Text[] arrayVals, Stats s)
    {
        arrayVals[0].text = s.Strength.ToString();
        arrayVals[1].text = s.Dexterity.ToString();
        arrayVals[2].text = s.Agility.ToString();
        arrayVals[3].text = s.HP.ToString();
    }
    private void Awake()
    {
    }
    void Start () {
        
        Attribute player1Atrrib = Player1.GetComponent<Attribute>();
        Attribute player2Atrrib = Player2.GetComponent<Attribute>();

        BattlePopupCharacterDsiplay[] popUp = GetComponentsInChildren<BattlePopupCharacterDsiplay>();

        if (player1Atrrib.getSTATS().HP > player2Atrrib.getSTATS().HP)
        {
            WinnerName.text = Utility.getStringFromName(player1Atrrib.getSTATS().PlayerType);
            setStatValues(winnerStats, player1Atrrib.getSTATS());
            setStatValues(loserStats, player2Atrrib.getSTATS());
            LoserName.text = Utility.getStringFromName(player2Atrrib.getSTATS().PlayerType);

            popUp[0].SetSprite(MasterManager.ManagerGlobalData.GetPlayerDataInfo().spriteList[0]);
            popUp[1].SetSprite(MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[0].spriteList[0]);
        }
        else 
        {
            WinnerName.text = Utility.getStringFromName(player2Atrrib.getSTATS().PlayerType);
            setStatValues(winnerStats, player2Atrrib.getSTATS());
            setStatValues(loserStats, player1Atrrib.getSTATS());
            LoserName.text = Utility.getStringFromName(player1Atrrib.getSTATS().PlayerType);

            popUp[0].SetSprite(MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[0].spriteList[0]);
            popUp[1].SetSprite(MasterManager.ManagerGlobalData.GetPlayerDataInfo().spriteList[0]);


        }
    }
	void battleEnd()
    {
        Destroy(this.transform.root.gameObject);
    }
	// Update is called once per frame
	void Update () {
        onScreenTime += Time.deltaTime;
        if (onScreenTime >= duration)
        {
            battleEnd();
        }
	}
}
