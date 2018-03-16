﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Move
{
    public Constants.MoveType type;

    public Animator attackerAnimator; //Attacker is who is attacking
    public Animator attackeeAnimator; //Attackee is who is being attacked. Used for Dodge.
    public Attribute attackerAttribute;

    public Stats attackerStats;
    public Stats attackeeStats;

    public float delayTime;

    public Move(Stats atkStat, Stats defStat, Attribute attrib, Animator attacker, Animator attackee, float delay)
    {
        attackerAnimator = attacker;
        attackeeAnimator = attackee;

        attackerAttribute = attrib;

        attackerStats = Stats.copy(atkStat);
        attackeeStats = Stats.copy(defStat);

        delayTime = delay;
    }
    public static Move copy(Move src)
    {
        return new Move(src.attackerStats, src.attackeeStats, src.attackerAttribute, src.attackerAnimator, src.attackeeAnimator, src.delayTime);
    }
}

public class AIManager : MonoBehaviour
{

    #region Variables
    public List<Move> moves;
    [Header("Player Settings")]
    public GameObject player1Object;
    public GameObject player2Object;
    [Header("Battle Result")]
    public GameObject battleResult;

    #region Sub Variables from GameObjects
    Stats player1Stats;
    Stats player2Stats;

    Character player1Character;
    Character player2Character;

    Animator player1Animator;
    Animator player2Animator;
    #endregion

    #region DelayValues
    [SerializeField]
    private float playerDelayTime;
    [SerializeField]
    private float enemyDelayTime;
    #endregion

    #region Timers
    private float firstTime; // The player with the Higher Agility uses this timer
    private float secondTime; // The lower uses this time
    private float repeatTime; // If one action is being repeated than this is being played
    #endregion

    #region Playthrough Indexes
    private int firstIndex; //for moving through the list
    private int secondIndex;
    private int totalIndex;
    #endregion

    #region Playthrough Conditions
    private bool noOneDead = true;
    #endregion

    #endregion

    void Start()
    {
        directValues();
        ResetValues();
        SimulateBattle(player1Stats, player2Stats);

    }

    // Points the Values from the Game Object to shorten Code
    private void directValues()
    {
        player1Stats = player1Object.GetComponent<Attribute>().getSTATS();
        player2Stats = player2Object.GetComponent<Attribute>().getSTATS();

        player1Character = player1Object.GetComponent<Character>();
        player2Character = player2Object.GetComponent<Character>();

        player1Animator = player1Object.GetComponent<Animator>();
        player2Animator = player2Object.GetComponent<Animator>();
    }

    private void ResetValues()
    {
        Random.InitState(100);
        player1Object.SetActive(true);
        player2Object.SetActive(true);

        firstTime = 0.0f;
        secondTime = 0.0f;
        repeatTime = 0.0f;

        firstIndex = 0;
        secondIndex = 1;

        totalIndex = 0;

        if (moves == null) moves = new List<Move>();

        Calculations.agilityTest(player1Stats, player2Stats);

        playerDelayTime = Calculations.calculateDelay(player1Stats);
        enemyDelayTime = Calculations.calculateDelay(player2Stats);

        GameObject battlePopup = GameObject.FindGameObjectWithTag("battlepopup");
        if (battlePopup != null) Destroy(battlePopup);
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
                m.attackeeAnimator.Play("Dodge");
                break;
            case Constants.MoveType.MISS:
                m.attackerAnimator.Play("Miss");
                break;
            case Constants.MoveType.DEATH:
                m.attackerAttribute.onDeath();
                StopCoroutine("playAnimation");
                GameObject refferenceGameObject = Instantiate(battleResult);
                refferenceGameObject.GetComponent<BattleResultScript>().Player1 = player1Object;
                refferenceGameObject.GetComponent<BattleResultScript>().Player2 = player2Object;
                if (m.attackerStats.PlayerType == 0)
                {
                    player1Object.SetActive(false);
                }
                else
                {
                    player2Object.SetActive(false);
                }
                break;
        }
    }//Called by Play Animation

    IEnumerator playAnimation()
    {
        float Move1 = moves[firstIndex].delayTime;
        float Move2 = moves[secondIndex].delayTime;
        while (true)
        {
            if (!player1Character.isAttacking && !player2Character.isAttacking)
            {
                firstTime += 0.01f;
                secondTime += 0.01f;
            }

            if (!player2Character.isAttacking && firstIndex < moves.Count && firstTime >= Move1)
            {
                player1Character.isAttacking = true;
                playMove(moves[firstIndex]);
                firstIndex += 2;
                totalIndex++;
                firstTime = 0.0f;
                Move1 = moves[firstIndex].delayTime;
            }
            if (!player1Character.isAttacking && secondIndex < moves.Count && secondTime >= Move2)
            {
                player2Character.isAttacking = true;
                playMove(moves[secondIndex]);
                secondIndex += 2;
                totalIndex++;
                secondTime = 0.0f;
                Move2 = moves[secondIndex].delayTime;
            }
            yield return new WaitForSeconds(0.01f); //Fixed delay of 0.01 seconds between each loop allowing for more accuracy when dealing with similar values
        }
    }//Needs SimulateBattle to Have run
    float stamDelay(Stats s, float time)
    {
        float res = 2.0f;
        float stamCalc = (float)s.Stamina / (float)s.MaxStamina;
        if (stamCalc >= 0.8f)
        {
            res = time;
        }
        else if (stamCalc >= 0.4f)
        {
            res = time * 1.2f;
        }
        else if (stamCalc >= 0.0f)
        {
            res = time * 1.4f;
        }
        return res;

    }
    void attack(Stats p1Stats, Stats p2Stats, Attribute attrib, Animator player1Animator, Animator player2Animator, float delayTime)
    {
        string player1Name = Utility.getStringFromName(p1Stats.PlayerType);
        string player2Name = Utility.getStringFromName(p2Stats.PlayerType);

        if (moves.Count != 0 && moves[moves.Count - 1].attackerStats.HP <= 0)
        {
            Debug.Log(player1Name + " Died");
            Move m = new Move(p1Stats, p2Stats, attrib, player1Animator, player2Animator, 0.0f);
            m.type = Constants.MoveType.DEATH;
            moves.Add(m);
            noOneDead = false;
        }

        if (noOneDead)
        {
            Move m = null;
            if (moves.Count == 0)
                m = new Move(p1Stats, p2Stats, attrib, player1Animator, player2Animator, delayTime);
            else
            {
                Move tmpMove = Move.copy(moves[moves.Count - 1]);
                m = new Move(tmpMove.attackerStats, tmpMove.attackeeStats, tmpMove.attackerAttribute, tmpMove.attackerAnimator, tmpMove.attackeeAnimator, tmpMove.delayTime);
                Stats t = m.attackeeStats;
                m.attackeeStats = m.attackerStats;
                m.attackerStats = t;
            }
            //Move m = new Move(p1Stats, p2Stats, attrib, player1Animator, player2Animator, stamDelay(p1Stats, delayTime));
            bool hit = Calculations.playerAttacks(p1Stats.Dexterity);
            bool dodge = Calculations.enemyDodges(p2Stats.Agility);

            if (hit && !dodge)
            {
                m.type = Constants.MoveType.ATTACK;
                short.Parse(0.ToString());
                m.attackerStats.Stamina -= 5;
                m.attackeeStats.HP -= m.attackerStats.Strength;
                Debug.Log(player1Name + " Attack");
            }
            else if (!hit && !dodge)
            {
                m.type = Constants.MoveType.MISS;
                m.attackerStats.Stamina -= 6;
                Debug.Log(player1Name + " Missed");
            }
            else if (dodge)
            {
                m.type = Constants.MoveType.DODGE;
                Debug.Log(player1Name + "attacked, " + player2Name + " Dodged");
            }
            if (m.attackerStats.Stamina < 0) m.attackerStats.Stamina = 0;
            Debug.LogWarning(m.attackerStats.PlayerType + ": " + m.attackerStats.Stamina);
            moves.Add(m);
        }
    }//Called by Simulate Battle

    void SimulateBattle(Stats player1, Stats player2)
    {
        int index = 0; //Prevents endless loop
        moves.Clear(); //Ensures that the moves Only contain 1 battle;
        while (noOneDead && index < 1000)
        {
            index++;
            if (playerDelayTime < enemyDelayTime)
            {
                attack(player1, player2, player1Object.GetComponent<Attribute>(), player1Animator, player2Animator, playerDelayTime);
                attack(player2, player1, player2Object.GetComponent<Attribute>(), player2Animator, player1Animator, enemyDelayTime);
            }
            else
            {
                attack(player2, player1, player2Object.GetComponent<Attribute>(), player2Animator, player1Animator, enemyDelayTime);
                attack(player1, player2, player1Object.GetComponent<Attribute>(), player1Animator, player2Animator, playerDelayTime);
            }

        }
        foreach (Move m in moves)
        {
            Debug.Log(m.type);
        }
    }//
    void Update()
    {
        //Temporary Until we have a go to battle system.
        if (Input.GetButton("Fire1"))
        {
            StartCoroutine("playAnimation");
            ResetValues();
        }
    }
}
