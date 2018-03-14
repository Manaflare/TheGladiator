using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public Button newGameButton;
    public Button continueButton;
    public Button settingsButton;
    public Button quitButton;

    // Use this for initialization
    void Start ()
    {
        Button ngb = newGameButton.GetComponent<Button>();
        ngb.onClick.AddListener(toCharacterCreation);

        //Button lgb = continueButton.GetComponent<Button>();
        //lgb.onClick.AddListener(continueFile);

        Button setb = settingsButton.GetComponent<Button>();
        setb.onClick.AddListener(loadSettingsMenu);

        Button quitb = quitButton.GetComponent<Button>();
        quitb.onClick.AddListener(quitGame);
    }

    void toCharacterCreation()
    {
        //Application.LoadLevel("CharacterCreator");
        SceneManager.LoadScene("CharacterCreator", LoadSceneMode.Single);
    }

    void continueFile()
    {
        SceneManager.LoadScene("TownScreen", LoadSceneMode.Single);
    }

    void loadSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu", LoadSceneMode.Single);
    }

    void quitGame()
    {
        Application.Quit();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
