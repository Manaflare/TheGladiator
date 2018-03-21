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

    public GameObject settingsPrefab;

    // Use this for initialization
    void Start ()
    {
        Button ngb = newGameButton.GetComponent<Button>();
        ngb.onClick.AddListener(toCharacterCreation);

        Button lgb = continueButton.GetComponent<Button>();
        lgb.onClick.AddListener(continueFile);

        Button setb = settingsButton.GetComponent<Button>();
        setb.onClick.AddListener(loadSettingsMenu);
    }

    void toCharacterCreation()
    {
        //Application.LoadLevel("CharacterCreator");
        SceneManager.LoadScene("CharacterCreator", LoadSceneMode.Single);
    }

    void continueFile()
    {
        SceneManager.LoadScene("Town", LoadSceneMode.Single);
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
