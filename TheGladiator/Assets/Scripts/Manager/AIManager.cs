using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {
    float playerTime;
    float enemyTime;

    int p1Strength = 10, p1Health = 100;
    int eStrength = 15, eHealth = 100;

    public float playerDelayTime;
    public float enemyDelayTime;
    bool noOneDead = true;
	// Use this for initialization
	void Start () {
        playerTime = 0.0f;
        enemyTime = 0.0f;
	}
    void attack()
    {


        if (playerTime >= playerDelayTime)
        {
            eHealth -= p1Strength;
            playerTime = 0.0f;
        }
        if (enemyTime >= enemyDelayTime)
        {
            p1Health -= eStrength;
            enemyTime = 0.0f;
        }

        if (p1Health <= 0)
        {
            Debug.Log("Player Lost");
            noOneDead = false;
        }
        if (eHealth <= 0)
        {
            Debug.Log("Enemy Lost");
            noOneDead = false;
        }

    }
	// Update is called once per frame
	void Update () {
        playerTime += Time.deltaTime;
        enemyTime += Time.deltaTime;
        if (noOneDead) attack();
        //Debug.Log("Player Hp: " + p1Health);
        //Debug.Log("Enemy Hp: " + eHealth);
    }
}
