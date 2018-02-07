using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

    [Header("Player Settings")]
    public Stats p1;
    public Stats e;

    [Header("Delay Settings")]
    [SerializeField]
    private float playerDelayTime;
    [SerializeField]
    private float enemyDelayTime;

    float playerTime;
    float enemyTime;

    bool noOneDead = true;
	// Use this for initialization
	void Start () {
        playerTime = 0.0f;
        enemyTime = 0.0f;

        playerDelayTime = calculateDelay(p1);
        enemyDelayTime = calculateDelay(e);

	}
    float calculateDelay(Stats s)
    {
        
        float agilityDifference = (Constants.MAX_STAT_LEVEL - s.Agility);
        float maxPercentage = agilityDifference / Constants.MAX_STAT_LEVEL;
        float timeWithoutMinimum = Constants.MODIFIABLE_TIME * maxPercentage;
        float attackDelay = timeWithoutMinimum + Constants.MINIMUM_DELAY;

        return attackDelay;
    }

    bool attackHits(int dex)
    {
        float accuracy = 100 * (Constants.MINIMUM_ACCURACY + (Constants.ACCURACY_STEP_AMOUNT * dex));

        bool result = (Random.Range(0, 100) > accuracy) ? true : false; 

        return result;
    }
    void attack(Stats player, Stats enemy)
    {

        if (playerTime >= playerDelayTime)
        {
            bool hit = attackHits(player.Dexterity);
            if (hit)
            {
                enemy.HP -= player.Strength;
                Debug.Log("Player Attack");
            }
            else if (!hit)
            {
                Debug.Log("Player Missed");
            }
            playerTime = 0.0f;
        }
        if (enemyTime >= enemyDelayTime && attackHits(enemy.Dexterity))
        {
            bool hit = attackHits(enemy.Dexterity);
            if (hit)
            {
                player.HP -= enemy.Strength;
                Debug.Log("Enemy Attack");
            }
            else if (!hit)
            {
                Debug.Log("Enemy Missed");
            }
            enemyTime = 0.0f;

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
