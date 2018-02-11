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

    public float delayTime;

    public Move(Stats atkStat, Stats defStat, Animator attacker, Animator attackee, float delay)
    {
        attackerAnimator = attacker;
        attackeeAnimator = attackee;

        attackerStats = atkStat;
        attackeeStats = defStat;

        delayTime = delay;
    }
}

public class AIManager : MonoBehaviour {
    
    public List<Move> moves;


    [Header("Player Settings")]
    //The stats we provide
    public Stats p1;
    public Stats e;

    //The information if a player is in an animation
    public Character p1c;
    public Character ec;

    //The Animator controls
    public Animator anim;
    public Animator anim2;

    [Header("Repeat/Replay Settings")]
    public bool repeatMove;
    [Range(0, 10)]
    public int moveToRepeat;


    [Header("Delay Settings")]

    private float playerDelayTime;
    private float enemyDelayTime;

    float firstTime;
    float secondTime;

    int firstIndex;
    int secondIndex;

    int totalIndex;

    bool noOneDead = true;
    bool animationPlayed = false;
    bool gore = true;
    bool doOnce = true;

    void Start()
    {
        Reset();
    }

    private void Reset()
    {
        firstTime = 0.0f;
        secondTime = 0.0f;

        firstIndex = 0;
        secondIndex = 1;

        totalIndex = 0;

        Calculations.agilityTest(p1, e);
        moves = new List<Move>();

        playerDelayTime = Calculations.calculateDelay(p1);
        enemyDelayTime = Calculations.calculateDelay(e);
    }

    void attack(Stats stats, Stats enemy, Animator playerAnimator, Animator enemyAnimator, float delayTime)
    {
        string name = Utility.getStringFromName(stats.name);
        string enemyName = Utility.getStringFromName(enemy.name);

        if (stats.HP <= 0)
        {
            Debug.Log(name + " Died");
            Move m = new Move(stats, enemy, playerAnimator, enemyAnimator, 0.5f);
            m.type = Constants.MoveType.DEATH;
            moves.Add(m);
            noOneDead = false;
        }

        if (noOneDead)
        {

            Move m = new Move(stats, enemy, playerAnimator, enemyAnimator, delayTime);

            bool hit = Calculations.attackHits(stats.Dexterity);
            bool dodge = Calculations.enemyDodges(stats.Agility);

            if (hit && !dodge)
            {
                m.type = Constants.MoveType.ATTACK;

                enemy.HP -= stats.Strength;
                Debug.Log(name + " Attack");
            }
            else if (!hit && !dodge)
            {
                m.type = Constants.MoveType.MISS;
                Debug.Log(name + " Missed");
            }
            else if (dodge)
            {
                m.type = Constants.MoveType.DODGE;
                Debug.Log(name + "attacked, " + enemyName + " Dodged");
            }
            moves.Add(m);
        }
    }

    void SimulateBattle(Stats player, Stats enemy)
    {
       
        while (noOneDead)
        {
            if (playerDelayTime < enemyDelayTime)
            {
                attack(player, enemy, anim, anim2, playerDelayTime);
                attack(enemy, player, anim2, anim, enemyDelayTime);
            }
            else
            {
                attack(enemy, player, anim2, anim, enemyDelayTime);
                attack(player, enemy, anim, anim2, playerDelayTime);
            }
        }     
        foreach(Move m in moves)
        {
            Debug.Log(m.type);
        }
    }


    void playMove(Move m)
    {
        switch (m.type)
        {
            case Constants.MoveType.ATTACK:
                m.attackerAnimator.Play("Attack");
                break;
            case Constants.MoveType.DODGE:
                m.attackerAnimator.Play("Attack");
                m.attackeeAnimator.Play("dodge");
                break;
            case Constants.MoveType.MISS:
                break;
            case Constants.MoveType.DEATH:
                animationPlayed = true;
                break;
        }
    }

    void playAnimation(int moveToPlay = 0, bool repeat = false)
    {
        if (!repeat)
        {

            if (!p1c.isAttacking && !ec.isAttacking)
            {
                firstTime += Time.deltaTime;
                secondTime += Time.deltaTime;
            }

            if (firstIndex < moves.Count && firstTime >= moves[firstIndex].delayTime)
            {
                playMove(moves[firstIndex]);
                firstIndex += 2;
                totalIndex++;
                firstTime = 0.0f;
            }
            if (secondIndex < moves.Count && secondTime >= moves[secondIndex].delayTime)
            {
                playMove(moves[secondIndex]);
                secondIndex += 2;
                totalIndex++;
                secondTime = 0.0f;
            }
        }
        else if (repeat)
        {
            playMove(moves[moveToPlay]);
        }
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

    void Update () {

        //Debug.Log(p1c.isAttacking);
        if (noOneDead)
        {
            SimulateBattle(p1, e);
        }

        if (gore && !p1c.isAttacking && !ec.isAttacking && p1.HP <= 0 && animationPlayed)
        {
            gore = false;
            Invoke("playGore", 0.84f);
        }
        if (gore && !p1c.isAttacking && !ec.isAttacking && e.HP <= 0 && animationPlayed)
        {
            gore = false;
            Invoke("playGoreEnemy", 0.84f);
        }

        if (!noOneDead && doOnce && totalIndex <= moves.Count)
        {    
            //Debug.Log("===========From the List===========");
            //doOnce = false;
            playAnimation(moveToRepeat, repeatMove);
        }
    }
}
