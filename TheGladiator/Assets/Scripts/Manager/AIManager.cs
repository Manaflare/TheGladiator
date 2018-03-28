﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using C = Constants;
#region Move Class
public class Move
{
    public Constants.MoveType type;

    #region Attacker
    public Stats attackerStats;
    public Attribute attackerAttribute;
    public Animator attackerAnimator; //Attacker is who is attacking
    #endregion
    #region Attackee
    public Animator attackeeAnimator; //Attackee is who is being attacked. Used for Dodge.
    public Attribute attackeeAttribute;
    public Stats attackeeStats;
    #endregion

    public float delayTime;

    public Move(Stats atkStat, Stats defStat, Attribute attrib, Animator attacker, Animator attackee, float delay, Attribute eAttrib)
    {
        attackerAnimator = attacker;
        attackeeAnimator = attackee;

        attackerAttribute = attrib;
        attackeeAttribute = eAttrib;

        attackerStats = Stats.copy(atkStat);
        attackeeStats = Stats.copy(defStat);

        delayTime = delay;
    }
    public static Move copy(Move src)
    {
        return new Move(src.attackerStats, src.attackeeStats, src.attackerAttribute, src.attackerAnimator, src.attackeeAnimator, src.delayTime, src.attackeeAttribute);
    }
}
#endregion
public class AIManager : MonoBehaviour
{

    #region Variables
    public List<Move> DisplayMoves;
    bool once = true;
    #region Player Settings
    [Header("Player Settings")]
    public GameObject player1Object;
    public GameObject player2Object;
    #endregion

    #region Battle Result
    [Header("Battle Result")]
    public GameObject battleResult;
    #endregion

    #region Enemy Loading
    Queue<int> enemyIndexes;
    #endregion

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
    #endregion

    #region Playthrough Indexes
    private int firstIndex; //for moving through the list
    private int totalIndex;
    #endregion

    #region Playthrough Conditions
    private bool noOneDead = true;
    #endregion

    #region Fake Stamina
    float firstMove = 0.0f;
    float secondMove = 0.0f;
    #endregion

    #endregion

    #region Functions
    #region Start up
    void Start()
    {
        Random.InitState(100);
        enemySelection();
        directValues();
        ResetValues();
        DisplayMoves = SimulateBattle(player1Stats, player2Stats);

    }
    private void enemySelection()
    {
        //For an entire tournament we need atleast 15
        int maxIndex = MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData.Count;
        enemyIndexes = new Queue<int>();
        List<int> possibleIndexes = new List<int>();
        for (int i = 0; i < maxIndex; i++) possibleIndexes.Add(i);
        

        for (int i = 0; i < 15; i++)
        {
            int ran = Random.Range(0, maxIndex);
            if (possibleIndexes.Contains(ran))
            {
                enemyIndexes.Enqueue(ran);
                possibleIndexes.Remove(ran);
            }
        }
    }
    // Points the Values from the Game Object to shorten Code
    private void directValues()
    {
        //player1Stats = MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0];
        //player1Object.GetComponent<PlayerAttribute>().setSTATS(player1Stats);
        //player2Stats = MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[enemyIndexes.Dequeue()].statsList[0];
        //player2Object.GetComponent<EnemyAttribute>().setSTATS(player2Stats);
        player1Stats = player1Object.GetComponent<Attribute>().getSTATS();
        player2Stats = player2Object.GetComponent<Attribute>().getSTATS();

        player1Character = player1Object.GetComponent<Character>();
        player2Character = player2Object.GetComponent<Character>();

        player1Animator = player1Object.GetComponent<Animator>();
        player2Animator = player2Object.GetComponent<Animator>();
    }
    private void ResetValues()
    {
        player1Object.SetActive(true);
        player2Object.SetActive(true);

        firstTime = 0.0f;

        firstIndex = 0;

        totalIndex = 0;

        if (DisplayMoves == null) DisplayMoves = new List<Move>();

        Calculations.agilityTest(player1Stats, player2Stats);

        playerDelayTime = Calculations.calculateDelay(player1Stats);
        enemyDelayTime = Calculations.calculateDelay(player2Stats);

        GameObject battlePopup = GameObject.FindGameObjectWithTag("battlepopup");
        if (battlePopup != null) Destroy(battlePopup);
    }
    #endregion

    #region Display Animation
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
                StopCoroutine("playAnimation");
                GameObject refferenceGameObject = Instantiate(battleResult);
                refferenceGameObject.GetComponent<BattleResultScript>().winner = m.attackeeStats;
                m.attackeeAttribute.onDeath();
                break;
        }
    }//Called by Play Animation

    IEnumerator playAnimation()
    {

        float Move1 = DisplayMoves[firstIndex].delayTime;
        while (true)
        {
            if (!player1Character.isAttacking && !player2Character.isAttacking)
            {
                firstTime += Time.fixedDeltaTime;
            }
            if (firstIndex < DisplayMoves.Count && firstTime > Move1)
            {
                Debug.LogWarning(DisplayMoves[firstIndex].attackerStats.PlayerType + ": " +  DisplayMoves[firstIndex].type);
                playMove(DisplayMoves[firstIndex]);
                firstIndex++;
                if (firstIndex < DisplayMoves.Count)
                {
                    totalIndex++;
                    firstTime = 0.0f;
                    Move1 = DisplayMoves[firstIndex].delayTime;
                }
            }
            yield return new WaitForFixedUpdate();       
        }
    }//Needs SimulateBattle to Have run
    #endregion

    #region SimulateBattle
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

    void attack(Stats p1Stats, Stats p2Stats, Attribute attrib, Animator player1Animator, Animator player2Animator, float delayTime, Attribute eAttrib, ref List<Move> moves)
    {
        if (moves.Count != 0 && moves[moves.Count - 1].attackerStats.HP <= 0)
        {
            moves.RemoveAt(moves.Count - 1);

            #region Move Creation
            Move m = Move.copy(moves[moves.Count - 1]);
            Debug.Log(m.attackeeStats.PlayerType + " Died");
            m.type = Constants.MoveType.DEATH;
            #endregion

            #region Stat Swap
            Stats t = Stats.copy(moves[moves.Count - 1].attackeeStats);
            m.attackeeStats = Stats.copy(moves[moves.Count - 1].attackerStats);
            m.attackerStats = Stats.copy(t);
            #endregion
            moves.Add(m);
            noOneDead = false;
        }

        if (noOneDead)
        {
            #region Move Creation
            Move m = null;
            if (moves.Count <= 1)
            {
                m = new Move(p1Stats, p2Stats, attrib, player1Animator, player2Animator, delayTime, eAttrib);
            }
            else
            {
                m = Move.copy(moves[moves.Count - 2]);
                Stats t = Stats.copy(moves[moves.Count - 1].attackeeStats);
                m.attackeeStats = Stats.copy(moves[moves.Count - 1].attackerStats);
                m.attackerStats = Stats.copy(t);
            }
            #endregion

            #region Attack Conditions
            bool playerHits = Calculations.playerAttacks(p1Stats.Dexterity);
            bool enemyDodge = Calculations.enemyDodges(p2Stats.Agility);
            #endregion

            #region Move Type Conditions
            if (playerHits && !enemyDodge)
            {
                m.type = Constants.MoveType.ATTACK;
                m.attackerStats.Stamina -= 5;
                m.attackeeStats.HP -= m.attackerStats.Strength;
            }
            else if (!playerHits && !enemyDodge)
            {
                m.type = Constants.MoveType.MISS;
                m.attackerStats.Stamina -= 6;
            }
            else if (enemyDodge)
            {
                m.type = Constants.MoveType.DODGE;
            }
            #endregion

            #region Stamina Delay Time
            if (m.attackerStats.PlayerType == Constants.PlayerType.PLAYER)
            {
                m.delayTime = stamDelay(m.attackerStats, playerDelayTime);
            }
            else
            {
                m.delayTime = stamDelay(m.attackerStats, enemyDelayTime);
            }
            if (m.attackerStats.Stamina < 0) m.attackerStats.Stamina = 0;
            #endregion

            #region Does Stamina Increase?
            if (moves.Count % 2 == 0)
            {
                firstMove += m.delayTime;
                if (firstMove >= C.STAMINA_REGEN_INTERVAL)
                {
                    while (firstMove >= C.STAMINA_REGEN_INTERVAL)
                    {
                        firstMove -= C.STAMINA_REGEN_INTERVAL;
                        m.attackerStats.Stamina += 1;
                    }
                }
            }
            else
            {
                secondMove += m.delayTime;
                if (secondMove >= C.STAMINA_REGEN_INTERVAL)
                {
                    while (secondMove >= C.STAMINA_REGEN_INTERVAL)
                    {
                        secondMove -= C.STAMINA_REGEN_INTERVAL;
                        m.attackerStats.Stamina += 1;
                    }
                }
            }
            if (m.attackerStats.Stamina > m.attackerStats.MaxStamina) m.attackerStats.Stamina = m.attackerStats.MaxStamina;
            #endregion
            moves.Add(m);
        }
    }//Called by Simulate Battle

    List<Move> SimulateBattle(Stats player1, Stats player2)
    {
        List<Move> moves = new List<Move>();
        int index = 0; //Prevents endless loop
        moves.Clear(); //Ensures that the moves Only contain 1 battle;
        while (noOneDead && index < 1000)
        {
            index++;
            if (playerDelayTime < enemyDelayTime)
            {
                attack(player1, player2, player1Object.GetComponent<Attribute>(), player1Animator, player2Animator, playerDelayTime, player2Object.GetComponent<Attribute>(), ref moves);
                attack(player2, player1, player2Object.GetComponent<Attribute>(), player2Animator, player1Animator, enemyDelayTime, player1Object.GetComponent<Attribute>(), ref moves);
            }
            else
            {
                attack(player2, player1, player2Object.GetComponent<Attribute>(), player2Animator, player1Animator, enemyDelayTime, player1Object.GetComponent<Attribute>(), ref moves);
                attack(player1, player2, player1Object.GetComponent<Attribute>(), player1Animator, player2Animator, playerDelayTime, player2Object.GetComponent<Attribute>(), ref moves);
            }

        }
        //foreach (Move m in moves)
        //{
        //    Debug.Log(m.type);
        //}
        return moves;
    }//
    #endregion
    #endregion

    void Update()
    {
        //Temporary Until we have a go to battle system.
        if (Input.GetButton("Fire1") && once)
        {
            once = false;
            StartCoroutine("playAnimation");
            ResetValues();
        }
    }
}
