using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public Constants.MoveType type;

    public Animator attackerAnimator;
    public Animator attackeeAnimator;

    public Stats attackerStats;
    public Stats attackeeStats;

    public Vector2 times;
}

public class AIManager : MonoBehaviour {
    
    public List<Move> moves;


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
    // Use this for initialization
    void Start() {
        playerTime = 0.0f;
        enemyTime = 0.0f;
        agilityTest(p1, e);
        moves = new List<Move>();
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

    float a(Stats stats, Stats enemy, Animator playerAnimator, Animator enemyAnimator,  float time, float delayTime)
    {
        string name = Utility.getStringFromName(stats.name);
        string enemyName = Utility.getStringFromName(enemy.name);

        if (time >= delayTime)
        {
            Move m = new Move();
            m.times = new Vector2(time, delayTime);
            m.attackerStats = stats;
            m.attackeeStats = enemy;

            m.attackerAnimator = playerAnimator;
            m.attackeeAnimator = enemyAnimator;

            bool hit = attackHits(stats.Dexterity);
            bool dodge = enemyDodges(stats.Agility);

            if (hit && !dodge)
            {
                m.type = Constants.MoveType.ATTACK;

                enemy.HP -= stats.Strength;
                Debug.Log(name + " Attack");
                playerAnimator.SetBool("playerAttack", true);
            }
            else if (!hit && !dodge)
            {
                m.type = Constants.MoveType.MISS;
                Debug.Log(name + " Missed");
            }
            else if (dodge)
            {
                m.type = Constants.MoveType.DODGE;
                Debug.Log(enemyName + " Dodged");
            }
            moves.Add(m);
            time = 0.0f;
        }
        if (stats.HP <= 0)
        {
            Debug.Log(name + "Lost");
            noOneDead = false;
        }
        return time;
        //animator.SetBool("playerAttack", false);

    }

    void attack(Stats player, Stats enemy)
    {
        //while(player.HP > 0 && enemy.HP > 0)
        //{
            playerTime = a(player, enemy, anim, anim2, playerTime, playerDelayTime);
            enemyTime = a(enemy, player, anim2, anim, enemyTime, enemyDelayTime);
        //}
    }

    void playGore()
    {
        GameObject player1Object = GameObject.FindGameObjectWithTag("player1");
        player1Object.GetComponent<PlayerAttribute>().onDeath();
        player1Object.SetActive(false);
    }
    void playGoreEnemy()
    {
        GameObject.FindGameObjectWithTag("player2").GetComponent<EnemyAttribute>().onDeath();
        GameObject.FindGameObjectWithTag("player2").SetActive(false);
    }
    // Update is called once per frame
    bool doOnce = true;
    void Update () {
        if (GameObject.FindGameObjectWithTag("player1") != null && GameObject.FindGameObjectWithTag("player1").activeSelf)
        {
            anim.SetBool("playerAttack", false);
        }
        if (GameObject.FindGameObjectWithTag("player2") != null && GameObject.FindGameObjectWithTag("player2").activeSelf)
        {
            anim2.SetBool("playerAttack", false);
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
        //forLater
        //if (!gore && !noOneDead && doOnce)
        //{
        //    Debug.ClearDeveloperConsole();
        //    doOnce = false;
        //    foreach(Move m in moves)
        //    {
        //        Debug.Log("Moves List: " + m.type);
        //        if (m.type != Constants.MoveType.DODGE)
        //        {
        //            m.attackerAnimator.SetBool("playerAttack", true);
        //            Debug.Log(Utility.getStringFromName(m.attackerStats.name) + " " + m.type);
        //        }
        //        else if(m.type == Constants.MoveType.DODGE)
        //        {
        //            Debug.Log(Utility.getStringFromName(m.attackeeStats.name) + " " + m.type);
        //        }
        //    }
        //}

    }
}
