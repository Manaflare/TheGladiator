using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BracketController : MonoBehaviour {
    float time;
    public float delayTime;
    public static int layers = 1;
	// Use this for initialization
	void Start () {
        time = 0.0f;
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
            MasterManager.ManagerLoadScene.LoadScene("Town");
        }
        else
        {
            GameObject.FindGameObjectWithTag("aimanager").GetComponent<AIManager>().play();
        }
    }
}
