using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleResultScript : MonoBehaviour {
    [HideInInspector]
    public Stats winner;

    public Text[] winnerStats;

    public Text WinnerName;


    [Range(0.0f, 10.0f)]
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
    void Start ()
    {
        WinnerName.text = winner.Name;
        setStatValues(winnerStats, winner);

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
