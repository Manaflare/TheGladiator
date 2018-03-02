using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MM = MasterManager;
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

        attackerStats = atkStat;
        attackeeStats = defStat;

        delayTime = delay;
    }
}

public class AIManager : MonoBehaviour {

    #region Variables
    public List<Move> moves;

    MM master;

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

    [Header("Repeat/Replay Settings")]
    public bool repeatMove;
    [Range(0, 55)]
    public int moveToRepeat;


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
    bool playTheAnim = false;
    private bool noOneDead = true;
    #endregion
   
    #endregion

    void Start()
    {
        master = GameObject.FindGameObjectWithTag("mastermanager").GetComponent<MasterManager>();
        directValues();
        ResetValues();
        SimulateBattle(player1Stats, player2Stats);
    }

    // Points the Values from the Game Object to shorten Code
    private void directValues()
    {
        player1Stats = player1Object.GetComponent<Attribute>().getSTATS();
        player2Stats = player2Object.GetComponent<Attribute>().getSTATS();
        Attribute a = player1Object.GetComponent<Attribute>();
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
        secondTime = 0.0f;
        repeatTime = 0.0f;

        firstIndex = 0;
        secondIndex = 1;

        totalIndex = 0;

        if (moves == null)  moves = new List<Move>();

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
                playTheAnim = false;
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
        while (true)
        {
            if (!repeatMove)
            {
                if (!player1Character.isAttacking && !player2Character.isAttacking)
                {
                    firstTime += 0.01f;
                    secondTime += 0.01f;
                }

                if (!player2Character.isAttacking && firstIndex < moves.Count && firstTime >= moves[firstIndex].delayTime)
                {
                    player1Character.isAttacking = true;
                    playMove(moves[firstIndex]);
                    firstIndex += 2;
                    totalIndex++;
                    firstTime = 0.0f;
                }
                if (!player1Character.isAttacking && secondIndex < moves.Count && secondTime >= moves[secondIndex].delayTime)
                {
                    player2Character.isAttacking = true;
                    playMove(moves[secondIndex]);
                    secondIndex += 2;
                    totalIndex++;
                    secondTime = 0.0f;
                }
            }
            else if (repeatMove)
            {
                player1Object.SetActive(true);
                player2Object.SetActive(true);



                repeatTime += Time.deltaTime;
                if (repeatTime >= moves[moveToRepeat].delayTime)
                {
                    playMove(moves[moveToRepeat]);
                    repeatTime = 0.0f;

                }
            }
            yield return new WaitForSeconds(0.01f); //Fixed delay of 0.01 seconds between each loop allowing for more accuracy when dealing with similar values
        }
    }//Needs SimulateBattle to Have run

    void attack(Stats player1Stats, Stats player2Stats, Attribute attrib, Animator player1Animator, Animator player2Animator, float delayTime)
    {
        string player1Name = Utility.getStringFromName(player1Stats.PlayerType);
        string player2Name = Utility.getStringFromName(player2Stats.PlayerType);

        if (player1Stats.HP <= 0)
        {
            Debug.Log(player1Name + " Died");
            Move m = new Move(player1Stats, player2Stats, attrib, player1Animator, player2Animator, delayTime);
            m.type = Constants.MoveType.DEATH;
            moves.Add(m);
            noOneDead = false;
        }

        if (noOneDead)
        {

            Move m = new Move(player1Stats, player2Stats, attrib, player1Animator, player2Animator, delayTime);

            bool hit = Calculations.playerAttacks(player1Stats.Dexterity);
            bool dodge = Calculations.enemyDodges(player2Stats.Agility);

            if (hit && !dodge)
            {
                m.type = Constants.MoveType.ATTACK;

                player2Stats.HP -= player1Stats.Strength;
                Debug.Log(player1Name + " Attack");
            }
            else if (!hit && !dodge)
            {
                m.type = Constants.MoveType.MISS;
                Debug.Log(player1Name + " Missed");
            }
            else if (dodge)
            {
                m.type = Constants.MoveType.DODGE;
                Debug.Log(player1Name + "attacked, " + player2Name + " Dodged");
            }
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
        foreach(Move m in moves)
        {
            Debug.Log(m.type);
        }
    }//

    void Update ()
    {
        //Temporary Until we have a go to battle system.
        if (Input.GetButton("Fire1")) 
        {
            StartCoroutine("playAnimation");
            ResetValues();
        }
        if (playTheAnim)
        {
            playAnimation();
        }

    }
}
