using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    public Button newGameButton;
    public Button continueButton;
    public Button settingsButton;
    public Button creditButton;

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
        MasterManager.ManagerSound.PlaySingleSound("Sword Clash");

        Button ngb = newGameButton.GetComponent<Button>();
        ngb.onClick.AddListener(toCharacterCreation);

        Button lgb = continueButton.GetComponent<Button>();
        lgb.onClick.AddListener(continueFile);

        Button setb = settingsButton.GetComponent<Button>();
        setb.onClick.AddListener(loadSettingsMenu);

        Button creditb = creditButton.GetComponent<Button>();
        creditb.onClick.AddListener(loadCredit);

        playerData = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        if (playerData.spriteList.Count == 0)
        {
            lgb.interactable = false;
            dataBlock.SetActive(false);
        }else
        {
            Stats actualStat = playerData.GetActualStats();

            playerName.text = actualStat.Name;
            HP.text = (actualStat.MAXHP * Constants.HP_MULTIPLIER).ToString();
            STR.text = actualStat.Strength.ToString();
            AGI.text = actualStat.Agility.ToString();
            DEX.text = actualStat.Dexterity.ToString();
            STA.text = actualStat.MaxStamina.ToString();
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

    void loadCredit()
    {
        MasterManager.ManagerLoadScene.LoadScene("Credits");
    }

    void loadSettingsMenu()
    {
        settingsPrefab.SetActive(true);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (MasterManager.ManagerInput.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
