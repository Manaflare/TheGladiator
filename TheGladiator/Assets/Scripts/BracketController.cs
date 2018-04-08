using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BracketController : MonoBehaviour {
    float time;
    public float delayTime;
    public static int layers = 1;
    // declare variables for BGM
    public AudioClip introMusic;
    public AudioClip backgroundMusic;
    // Use this for initialization
    void Start () {
        time = 0.0f;
        // call BGM intro Music
        MasterManager.ManagerSound.PlayBackgroundMusic(introMusic);
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > delayTime)
        {
            Destroy(this.transform.root.gameObject);
        }
    }
    private void OnDestroy()
    {
        if (GameObject.FindGameObjectWithTag("aimanager").GetComponent<AIManager>().wins == 3)
        {
            if(MasterManager.ManagerGlobalData.GetPlayerDataInfo().playerTier >= Constants.MAX_ENEMY_RANK)
            {
                PlayerPrefs.SetInt("Ending", 1);
            }

            MasterManager.ManagerLoadScene.LoadScene("Town");
        }
        else
        {
            // call BGM Background Music
            MasterManager.ManagerSound.PlayBackgroundMusic(backgroundMusic);
            GameObject.FindGameObjectWithTag("aimanager").GetComponent<AIManager>().play();
        }
    }
}
