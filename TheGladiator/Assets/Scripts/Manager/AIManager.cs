using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using C = Constants;

public class Move
{

    public Constants.MoveType typeOfMove;
    public Stats AttackerStats;
    public Stats DefenderStats;
    public float delayTime;

    public Move()
    {
        typeOfMove = C.MoveType.ATTACK;
        AttackerStats = null;
        DefenderStats = null;
        delayTime = 0.0f;
    }
    public Move(Constants.MoveType type, Stats p1, Stats p2, float time)
    {
        typeOfMove = type;
        AttackerStats = p1;
        DefenderStats = p2;
        delayTime = time;
    }

}

public class AIManager : MonoBehaviour
{
    #region Battle Variables
    [Header("Battle Result")]
    public GameObject WinnerPopup;

    [Header("Player Object")]
    public GameObject player1;
    public GameObject player2;

    [Header("Battle Start Delay")]
    [Range(0.0f, 10.0f)]
    public float Delay;
    private float time = 0.0f;
    private bool doOnce = true;

    [HideInInspector]
    public bool CanAttack = true;
    private List<Move> Battle;
    private Stats player1Stats;
    private Stats player2Stats;
    private Dictionary<C.PlayerType, Animator> animators;
    #endregion

    int playerCurrentOpponent;

    private void Start()
    {
        Random.InitState(100);

        Battle = BattleSimulator(MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0], MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[playerCurrentOpponent].statsList[0]);

        //Finals();

        animators = new Dictionary<C.PlayerType, Animator>()
        {
            {C.PlayerType.PLAYER, player1.GetComponent<Animator>() },
            {C.PlayerType.ENEMY, player2.GetComponent<Animator>() }
        };

        player1Stats = player1.GetComponent<Attribute>().getSTATS();
        player2Stats = player2.GetComponent<Attribute>().getSTATS();
    }

    private List<int> getAllEnemies()
    {
        List<int> result = new List<int>();
        int index = 0;
        int max = 0;

        foreach (var a in MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData)
        {
            if (a.playerTier == MasterManager.ManagerGlobalData.GetPlayerDataInfo().playerTier)
            {
                result.Add(index);
                max++;
            }
            index++;
        }
        return result;
    }

    private void getCurrentEnemy()
    {

    }

    void playMove(Move m)
    {
        switch (m.typeOfMove)
        {
            case Constants.MoveType.ATTACK:
                animators[m.AttackerStats.PlayerType].Play("Attack");
                break;
            case Constants.MoveType.DODGE:
                animators[m.AttackerStats.PlayerType].Play("Attack");
                animators[m.DefenderStats.PlayerType].Play("Dodge");
                break;
            case Constants.MoveType.MISS:
                animators[m.AttackerStats.PlayerType].Play("Miss");
                break;
            case Constants.MoveType.DEATH:
                GameObject reff = Instantiate(WinnerPopup);
                reff.GetComponent<BattleResultScript>().winner = m.DefenderStats;
                animators[m.AttackerStats.PlayerType].gameObject.GetComponent<Attribute>().onDeath();
                break;
        }
    }

    IEnumerator animateBattle()
    {
        Queue<Move> moveQueue = new Queue<Move>();

        foreach (Move m in Battle)
        {
            moveQueue.Enqueue(m);
        }
        float cummulativeTime = 0.0f;
        Move CurrentMove = moveQueue.Dequeue();
        
        while (true)
        {
            if (CanAttack && !player1.GetComponent<Character>().isAttacking && !player2.GetComponent<Character>().isAttacking)
            {
                cummulativeTime += Time.fixedDeltaTime;
            }
            if (CanAttack && CurrentMove.delayTime <= cummulativeTime)
            {
                CanAttack = false;
                playMove(CurrentMove);
                if (moveQueue.Count > 0)
                    CurrentMove = moveQueue.Dequeue();
            }
            yield return new WaitForFixedUpdate();
        }
    }
    
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

    void MoveAssembler(Stats Attacker, Stats Defender, Dictionary<Constants.PlayerType, float> delays, ref Dictionary<Constants.PlayerType, float> staminaTimes,
        ref List<Move> moves, ref Stats sourcePlayer, ref Stats sourceEnemy, ref Dictionary<Constants.PlayerType, float> cummulativeTime)
    {
        Move temp = new Move();

        Stats atk = Stats.copy(Attacker);
        Stats def = Stats.copy(Defender);

        bool playerHit = Calculations.playerAttacks(atk.Dexterity);
        bool enemyDodges = Calculations.enemyDodges(def.Agility);

        if (playerHit && !enemyDodges)
        {
            atk.Stamina -= 2;
            def.HP -= atk.Strength;
        }
        else if (!playerHit)
        {
            temp.typeOfMove = C.MoveType.MISS;
            atk.Stamina -= 3;
        }
        else if (playerHit && enemyDodges)
        {
            temp.typeOfMove = C.MoveType.DODGE;
            def.Stamina -= 1;
        }


        //STAMINA INCREASE

        float tempStamTime = staminaTimes[atk.PlayerType];

        if (tempStamTime >= Constants.STAMINA_REGEN_INTERVAL)
        {
            while (tempStamTime >= Constants.STAMINA_REGEN_INTERVAL)
            {
                atk.Stamina += 1;

                tempStamTime -= Constants.STAMINA_REGEN_INTERVAL;
                staminaTimes[atk.PlayerType] = 0.0f;
            }
        }
        else
        {
           staminaTimes[atk.PlayerType] += delays[atk.PlayerType];
        }

        //END STAMINA



        atk.Stamina = (atk.Stamina < 0) ? short.Parse("0") : atk.Stamina;
        def.Stamina = (def.Stamina < 0) ? short.Parse("0") : def.Stamina;

        temp.AttackerStats = Stats.copy(atk);
        temp.DefenderStats = Stats.copy(def);
        temp.delayTime = cummulativeTime[atk.PlayerType];
        cummulativeTime[atk.PlayerType] += delays[atk.PlayerType];


        moves.Add(temp);

        if (atk.PlayerType == C.PlayerType.PLAYER)
        {
            sourcePlayer = Stats.copy(atk);
            sourceEnemy = Stats.copy(def);
        }
        else
        {
            sourcePlayer = Stats.copy(def);
            sourceEnemy = Stats.copy(atk);
        }
    }

    List<Move> BattleSimulator(Stats p1, Stats p2)
    {
        List<Move> moves = new List<Move>();
        int noInfloops = 0;
        float player1delay = Calculations.calculateDelay(p1);
        float player2delay = Calculations.calculateDelay(p2);

        float player1currentTime = 0.0f;
        float player2currentTime = 0.0f;

        Dictionary<Constants.PlayerType, float> delayDictionary = new Dictionary<Constants.PlayerType, float>()
        {
            { Constants.PlayerType.PLAYER, player1delay },
            { Constants.PlayerType.ENEMY, player2delay },
        };

        Dictionary<Constants.PlayerType, float> staminaTimes = new Dictionary<C.PlayerType, float>()
        {
            { Constants.PlayerType.PLAYER, 0.0f },
            { Constants.PlayerType.ENEMY, 0.0f },
        };

        Dictionary<Constants.PlayerType, float> cummulativeTime = new Dictionary<Constants.PlayerType, float>()
        {
            { Constants.PlayerType.PLAYER, player1delay },
            { Constants.PlayerType.ENEMY, player2delay },
        };

        bool playerStart = player1delay < player2delay;

        if (playerStart)
        {
            player1currentTime += player1delay;
            player2currentTime += player2delay;
        }
        else
        {
            player1currentTime += player1delay;
            player2currentTime += player2delay;
        }

        while (true && noInfloops < 100)
        {
            noInfloops++;
            if (playerStart)
            {
                if (player1currentTime < player2currentTime)
                {
                    MoveAssembler(p1, p2, delayDictionary, ref staminaTimes, ref moves, ref p1, ref p2, ref cummulativeTime);
                    player1currentTime += player1delay;
                }
                else
                {
                    MoveAssembler(p2, p1, delayDictionary, ref staminaTimes, ref moves, ref p1, ref p2, ref cummulativeTime);
                    player2currentTime += player2delay;
                }
            }
            else
            {
                if (player1currentTime > player2currentTime)
                {
                    MoveAssembler(p2, p1, delayDictionary, ref staminaTimes, ref moves, ref p1, ref p2, ref cummulativeTime);
                    player2currentTime += player2delay;
                }
                else
                {
                    MoveAssembler(p1, p2, delayDictionary, ref staminaTimes, ref moves, ref p1, ref p2, ref cummulativeTime);
                    player1currentTime += player1delay;
                }
            }

            if (moves[moves.Count - 1].DefenderStats.HP <= 0)
            {
                Move tmp = new Move(C.MoveType.DEATH, moves[moves.Count - 1].DefenderStats, moves[moves.Count - 1].AttackerStats, moves[moves.Count - 1].delayTime + 0.5f);
                moves.Add(tmp);
                break;
            }
            
        }
        return moves;
    }
    
    void Update()
    {
        
        if (doOnce && time > Delay)
        {
            doOnce = false;
            StartCoroutine(animateBattle());
        }
        else if (doOnce)
        {
            time += Time.deltaTime;
        }
    }
}
