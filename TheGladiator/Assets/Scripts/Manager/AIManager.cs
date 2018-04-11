using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    
    Dictionary<string, int> ValidEnemies;
    public GameObject bracket;
    List<int> first = new List<int>();
    List<int> nextThree = new List<int>();
    List<int> final = new List<int>();
    public int wins = 0;

    // declare variables for BGM
    public AudioClip introMusic;
    public AudioClip backgroundMusic;
    public AudioClip victoryMusic;
    public AudioClip defeatMusic;


    private void Start()
    {
        BracketController.layers = 1;
        animators = new Dictionary<C.PlayerType, Animator>()
        {
            {C.PlayerType.PLAYER, player1.GetComponent<Animator>() },
            {C.PlayerType.ENEMY, player2.GetComponent<Animator>() }
        };
        ValidEnemies = new Dictionary<string, int>();

        PopulateEnemies();
        findAutoOpponents();
        displayBracket();
        drawPlayers(first[0]);



    }

    public void play()
    {
        animators[C.PlayerType.ENEMY].gameObject.SetActive(true);
        foreach(var a in animators[C.PlayerType.ENEMY].gameObject.GetComponentsInChildren<Image>())
        {
            Destroy(a.gameObject);
        }
        
        Battle = new List<Move>();
        if (wins == 0)
        {
            Battle = BattleSimulator(MasterManager.ManagerGlobalData.GetPlayerDataInfo().GetActualStats(), MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[first[0]].GetActualStats());
            animators[C.PlayerType.ENEMY].gameObject.GetComponent<BattleCharacterDisplay>().Draw(MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[first[0]]);
        }
        else if (wins == 1)
        {
            Battle = BattleSimulator(MasterManager.ManagerGlobalData.GetPlayerDataInfo().GetActualStats(), MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[nextThree[0]].GetActualStats());
            animators[C.PlayerType.ENEMY].gameObject.GetComponent<BattleCharacterDisplay>().Draw(MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[nextThree[0]]);

        }
        else if ( wins == 2)
        {
            Battle = BattleSimulator(MasterManager.ManagerGlobalData.GetPlayerDataInfo().GetActualStats(), MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[final[0]].GetActualStats());
            animators[C.PlayerType.ENEMY].gameObject.GetComponent<BattleCharacterDisplay>().Draw(MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[final[0]]);
            if (Battle[Battle.Count - 1].DefenderStats.PlayerType == C.PlayerType.PLAYER)
            {
                MasterManager.ManagerGlobalData.GetPlayerDataInfo().playerTier++;
            }
        }
        StartCoroutine(animateBattle());
    }

    void drawPlayers(int index)
    {
        foreach (var a in FindObjectsOfType<BattleCharacterDisplay>())
        {
            if (a.name == "Player1")
            {
                a.Draw(MasterManager.ManagerGlobalData.GetPlayerDataInfo());
            }
            else
            {
                a.Draw(MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[index]);
            }
        }
    }

    void findAutoOpponents()
    {
        first = firstSeven();

        List<Move> battle1 = BattleSimulator(MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[first[1]].statsList[0], MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[first[2]].GetActualStats());
        List<Move> battle2 = BattleSimulator(MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[first[3]].statsList[0], MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[first[4]].GetActualStats());
        List<Move> battle3 = BattleSimulator(MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[first[5]].statsList[0], MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[first[6]].GetActualStats());

        nextThree.Add(ValidEnemies[battle1[battle1.Count - 1].DefenderStats.Name]);
        nextThree.Add(ValidEnemies[battle2[battle2.Count - 1].DefenderStats.Name]);
        nextThree.Add(ValidEnemies[battle3[battle3.Count - 1].DefenderStats.Name]);

        List<Move> lastNPB = BattleSimulator(MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[nextThree[1]].statsList[0], MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[nextThree[2]].GetActualStats());

        final.Add(ValidEnemies[lastNPB[lastNPB.Count - 1].DefenderStats.Name]);

    }

    void PopulateEnemies()
    {
        int index = 0;
        foreach (var a in MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData)
        {
            List<C.ItemIndex> avail = new List<C.ItemIndex>()
        {
            C.ItemIndex.ARMOR,
            C.ItemIndex.HELMET,
            C.ItemIndex.LEFT_HAND,
            C.ItemIndex.PANTS,
            C.ItemIndex.RIGHT_HAND,
            C.ItemIndex.SHOES

        };
            if (a.playerTier == MasterManager.ManagerGlobalData.GetPlayerDataInfo().playerTier)
            {
                a.itemList.Clear();
                a.equipedItensId.Clear();
                foreach (var item in MasterManager.ManagerGlobalData.GetItemDataInfo().itemData)
                {
                    if (avail.Contains(item.Item_type))
                    {
                        if (Random.value >= 0.5f)
                        {
                            avail.Remove(item.Item_type);
                            a.itemList.Add(item);
                        }
                    }
                }
                
                foreach (var i in a.itemList)
                {
                    if (Random.value >= 0.4f)
                    {
                        a.equipedItensId.Add(i.id);
                    }
                }

                ValidEnemies.Add(a.statsList[0].Name, index);
            }
            index++;

        }
    }

    public void displayBracket()
    {
        Instantiate(bracket);

        GameObject[] Bracket = GameObject.FindGameObjectsWithTag("islayer");

        foreach (var a in Bracket)
        {
            if (a.name == "Bottom Layer" && BracketController.layers >= 1)
            {
                a.GetComponent<BracketLayer>().drawLayer(first);
            }
            if (a.name == "Second Layer" && BracketController.layers >= 2)
            {
                a.GetComponent<BracketLayer>().drawLayer(nextThree);

                //MasterManager.ManagerGlobalData.GetPlayerDataInfo().GetActualStats().HP += ((MasterManager.ManagerGlobalData.GetPlayerDataInfo().GetActualStats().MAXHP * 5) / 3);
                //MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[nextThree[0]].statsList[0].HP += ((MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[nextThree[0]].statsList[0].HP * 5) / 2);
                //MasterManager.ManagerGlobalData.GetPlayerDataInfo().GetActualStats().HP = (MasterManager.ManagerGlobalData.GetPlayerDataInfo().GetActualStats().HP > MasterManager.ManagerGlobalData.GetPlayerDataInfo().GetActualStats().MAXHP * 5) ? MasterManager.ManagerGlobalData.GetPlayerDataInfo().GetActualStats().MAXHP : MasterManager.ManagerGlobalData.GetPlayerDataInfo().GetActualStats().HP;
                //MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[nextThree[0]].statsList[0].HP = (MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[nextThree[0]].statsList[0].HP > MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[nextThree[0]].statsList[0].MAXHP * 5) ? MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[nextThree[0]].statsList[0].MAXHP : MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[nextThree[0]].statsList[0].HP;
                //Battle = BattleSimulator(MasterManager.ManagerGlobalData.GetPlayerDataInfo().GetActualStats(), MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[nextThree[0]].GetActualStats());
                //StartCoroutine(animateBattle());
            }
            if (a.name == "Third Layer" && BracketController.layers >= 3)
            {
                a.GetComponent<BracketLayer>().drawLayer(final);
            }
            if (a.name == "Winner" && BracketController.layers >= 4)
            {
                a.GetComponent<BracketLayer>().drawLayer(final);
            }
        }
        doOnce = true;
        BracketController.layers++;
    }

    List<int> firstSeven()
    {
        List<int> res = new List<int>();
        List<int> Valid = new List<int>();
        foreach(var a in ValidEnemies)
        {
            Valid.Add(a.Value);
        }
        while (res.Count < 7)
        {
            int ran = Random.Range(0, Valid.Count);
            res.Add(Valid[ran]);
            Valid.RemoveAt(ran);
        }
        return res;
    }

    void playMove(Move m)
    {

        switch (m.typeOfMove)
        {
            
            case Constants.MoveType.ATTACK:

                // call SFX random Attacks
                MasterManager.ManagerSound.PlayRandomSound("Sword Clash Short", "Attack Knife", "Whip Crack", "Attack Shield", "Attack Sword");

                animators[m.AttackerStats.PlayerType].Play("Attack");
                break;

            case Constants.MoveType.DODGE:
                animators[m.AttackerStats.PlayerType].Play("Attack");

                // call SFX Dodge
                MasterManager.ManagerSound.PlaySingleSound("Quick Swinging Swish");
                // call SFX Wow
                MasterManager.ManagerSound.PlayRandomSound("People Saying Wow", "People Saying Ahh");

                animators[m.DefenderStats.PlayerType].Play("Dodge");
                break;

            case Constants.MoveType.MISS:

                // call SFX Miss
                MasterManager.ManagerSound.PlaySingleSound("Fast Whoosh By");

                // call SFX Oohhh on Miss
                MasterManager.ManagerSound.PlaySingleSound("People Saying Oohhh");
                animators[m.AttackerStats.PlayerType].Play("Miss");
                break;

            case Constants.MoveType.DEATH:
                GameObject reff = Instantiate(WinnerPopup);

                // call SFX characater dies
                MasterManager.ManagerSound.PlaySingleSound("Character dies");
                if (m.DefenderStats.PlayerType == C.PlayerType.PLAYER)
                {
                    wins++;
                    CanAttack = true;
                    // call BGM Victory Music
                    MasterManager.ManagerSound.PlayBackgroundMusic(victoryMusic, false);

                    // call SFX Crowd Victory
                    MasterManager.ManagerSound.PlayRandomSound("Crowd Victory", "Crowd Angry");
                }
                else
                {
                    reff.GetComponent<BattleResultScript>().enemyDrawIndex = ValidEnemies[m.DefenderStats.Name];
                   
                    // call BGM Defeat Music (you lose)
                    MasterManager.ManagerSound.PlayBackgroundMusic(defeatMusic, false);

                    // call SFX Crowd Boo on Defeat
                    MasterManager.ManagerSound.PlaySingleSound("Crowd Boo");

                }
                reff.GetComponent<BattleResultScript>().winner = m.DefenderStats;
                animators[m.AttackerStats.PlayerType].gameObject.GetComponent<Attribute>().onDeath();
                StopCoroutine(animateBattle());
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
            if (GameObject.FindGameObjectWithTag("battlepopup") != null)
            {
                break;
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

}
