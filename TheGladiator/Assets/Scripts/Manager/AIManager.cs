using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

    [Header("Player Settings")]
    public Stats p1;
    public Stats e;
    public Character p1c;
    public Character ec;

    public Animator anim;
    public Animator anim2;

    bool gore = true;

    [Header("Delay Settings")]
    [SerializeField]
    private float playerDelayTime;
    [SerializeField]
    private float enemyDelayTime;

    float playerTime;
    float enemyTime;

    bool noOneDead = true;
    bool timeIsRunning = true;
    // Use this for initialization
    void Start() {
        playerTime = 0.0f;
        enemyTime = 0.0f;
        agilityTest(p1, e);

        playerDelayTime = calculateDelay(p1);
        enemyDelayTime = calculateDelay(e);




    }
    void agilityTest(Stats player, Stats enemy)
    {   
        if (player.Agility == enemy.Agility && player.Agility != Constants.MAX_STAT_LEVEL)
        {
            player.Agility++;
        }
        else if (enemy.Agility == Constants.MAX_STAT_LEVEL)
        {
            enemy.Agility -= 5;
        }
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
        bool result = (Random.Range(0, 100) < accuracy) ? true : false; 

        return result;
    }
    bool enemyDodges(int agi)
    {
        float dodge = 100 * (Constants.MINIMUM_DODGE + (Constants.DODGE_STEP_AMOUNT * agi));
        bool result = (Random.Range(0, 100) < dodge) ? true : false;

        return result;
    }

    void attack(Stats player, Stats enemy)
    {
        timeIsRunning = false;

        if (playerTime >= playerDelayTime)
        {
            bool hit = attackHits(player.Dexterity);
            bool dodge = enemyDodges(enemy.Agility);
            if (hit && !dodge)
            {
                enemy.HP -= player.Strength;
                Debug.Log("Player Attack");
                anim.SetBool("playerAttack", true);
            }
            else if (!hit && !dodge)
            {
                Debug.Log("Player Missed");
            }
            else if (dodge)
            {
                //Enemy Dodge
                anim2.SetBool("enemyDodge", true);
                anim.SetBool("playerAttack", true);
                Debug.Log("Enemy Dodged");
            }
            playerTime = 0.0f;
        }
        if (enemyTime >= enemyDelayTime)
        {
            bool hit = attackHits(enemy.Dexterity);
            bool dodge = enemyDodges(player.Agility);
            if (hit && !dodge)
            {
                anim.enabled = false;
                player.HP -= enemy.Strength;
                anim2.SetBool("enemyAttack", true);
                Debug.Log("Enemy Attack");
            }
            else if (!hit && !dodge)
            {
                Debug.Log("Enemy Missed");
            }
            else if (dodge)
            {
                anim.SetBool("dodge", true);
                anim2.SetBool("enemyAttack", true);
                Debug.Log("Player Dodged");
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
    void playGore()
    {
        GameObject.FindGameObjectWithTag("player1").GetComponent<PlayerAttribute>().onDeath();
        GameObject.FindGameObjectWithTag("player1").SetActive(false);
    }
    void playGoreEnemy()
    {
        GameObject.FindGameObjectWithTag("player2").GetComponent<EnemyAttribute>().onDeath();
        GameObject.FindGameObjectWithTag("player2").SetActive(false);
    }
    // Update is called once per frame
    void Update () {


        if (GameObject.FindGameObjectWithTag("player1") != null && GameObject.FindGameObjectWithTag("player1").activeSelf)
        {
            anim.SetBool("playerAttack", false);
            anim.SetBool("dodge", false);

        }
        if (GameObject.FindGameObjectWithTag("player2") != null && GameObject.FindGameObjectWithTag("player2").activeSelf)
        {
            anim2.SetBool("enemyDodge", false);
            anim2.SetBool("enemyAttack", false);

        }


        //Debug.Log(p1c.isAttacking);
        if (!p1c.isAttacking && !ec.isAttacking)
        {    
            playerTime += Time.deltaTime;
            enemyTime += Time.deltaTime;
        }
        if (noOneDead) attack(p1, e);

        if (gore && !p1c.isAttacking && !ec.isAttacking && p1.HP <= 0)
        {
            gore = false;
            Invoke("playGore", 0.84f);
        }
        if (gore && !p1c.isAttacking && !ec.isAttacking && e.HP <= 0)
        {
            gore = false;
            Invoke("playGoreEnemy", 0.84f);
        }

    }
}
