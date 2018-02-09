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

    bool animationPlaying = false;

    bool noOneDead = true;
    bool timeIsRunning = true;
    // Use this for initialization
    void Start() {
        playerTime = 0.0f;
        enemyTime = 0.0f;

        playerDelayTime = calculateDelay(p1);
        enemyDelayTime = calculateDelay(e);

        agilityTest(p1, e);



    }
    void agilityTest(Stats player, Stats enemy)
    {   
        if (player.Agility == enemy.Agility)
        {
            player.Agility++;
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

    void attack(Stats player, Stats enemy)
    {
        timeIsRunning = false;

        if (playerTime >= playerDelayTime)
        {
            bool hit = attackHits(player.Dexterity);
            if (hit)
            {
                enemy.HP -= player.Strength;
                Debug.Log("Player Attack");
                animationPlaying = true;
                anim.SetBool("playerAttack", true);
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
                anim2.SetBool("enemyAttack", true);
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
        anim.SetBool("playerAttack", false);
        anim2.SetBool("enemyAttack", false);
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
