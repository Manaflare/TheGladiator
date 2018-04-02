using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    public Button newGameButton;
    public Button continueButton;
    public Button settingsButton;

    public GameObject dataBlock;
    public Text playerName;
    public Text HP;
    public Text STR;
    public Text AGI;
    public Text DEX;
    public Text STA;

    public GameObject settingsPrefab;
    private ListDataInfo playerData;

    // declare variable for BGM
    public AudioClip backgroundMusic;

    // Use this for initialization
    void Start ()
    {
        // call BGM and SFX
        MasterManager.ManagerSound.PlayBackgroundMusic(backgroundMusic);
        MasterManager.ManagerSound.PlaySingleSound("Attack Sword");

        Button ngb = newGameButton.GetComponent<Button>();
        ngb.onClick.AddListener(toCharacterCreation);

        Button lgb = continueButton.GetComponent<Button>();
        lgb.onClick.AddListener(continueFile);

        Button setb = settingsButton.GetComponent<Button>();
        setb.onClick.AddListener(loadSettingsMenu);
        playerData = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        if (playerData.spriteList.Count == 0)
        {
            lgb.interactable = false;
            dataBlock.SetActive(false);
        }else
        {
            playerName.text = playerData.statsList[0].Name;
            HP.text = (playerData.statsList[0].MAXHP * Constants.HP_MULTIPLIER).ToString();
            STR.text = playerData.statsList[0].Strength.ToString();
            AGI.text = playerData.statsList[0].Agility.ToString();
            DEX.text = playerData.statsList[0].Dexterity.ToString();
            STA.text = playerData.statsList[0].MaxStamina.ToString();
        }


    }

    void toCharacterCreation()
    {
        //Application.LoadLevel("CharacterCreator");
        MasterManager.ManagerLoadScene.LoadScene("CharacterCreator");
    }

    void continueFile()
    {
        MasterManager.ManagerLoadScene.LoadScene("Town");
    }

    void loadSettingsMenu()
    {
        settingsPrefab.SetActive(true);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
