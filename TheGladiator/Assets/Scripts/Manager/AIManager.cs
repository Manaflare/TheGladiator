﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

    [Header("Player Settings")]
    public Stats p1;
    public Stats e;

    [Header("Delay Settings")]
    [Range(0.0f, 3.0f)]
    public float playerDelayTime;
    [Range(0.0f, 3.0f)]
    public float enemyDelayTime;

    float playerTime;
    float enemyTime;

    bool noOneDead = true;
	// Use this for initialization
	void Start () {
        playerTime = 0.0f;
        enemyTime = 0.0f;
	}
    void attack(Stats player, Stats enemy)
    {
        if (playerTime >= playerDelayTime)
        {
            enemy.HP -= player.Strength;
            playerTime = 0.0f;
            Debug.Log("Player Attack");
        }
        if (enemyTime >= enemyDelayTime)
        {
            player.HP -= enemy.Strength;
            enemyTime = 0.0f;
            Debug.Log("Enemy Attack");
        }

        if (player.HP <= 0)
        {
            Debug.Log("Player Lost");
            noOneDead = false;
        }
        if (enemy.HP <= 0)
        {
            Debug.Log("Enemy Lost");
            noOneDead = false;
        }

    }
	// Update is called once per frame
	void Update () {
        playerTime += Time.deltaTime;
        enemyTime += Time.deltaTime;
        if (noOneDead) attack(p1, e);
        //Debug.Log("Player Hp: " + p1Health);
        //Debug.Log("Enemy Hp: " + eHealth);
    }
}
