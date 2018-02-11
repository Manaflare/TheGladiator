using UnityEngine;

static class Calculations
{
    public static void agilityTest(Stats player, Stats enemy)
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

    public static float calculateDelay(Stats s)
    {

        float agilityDifference = (Constants.MAX_STAT_LEVEL - s.Agility);
        float maxPercentage = agilityDifference / Constants.MAX_STAT_LEVEL;
        float timeWithoutMinimum = Constants.MODIFIABLE_TIME * maxPercentage;
        float attackDelay = timeWithoutMinimum + Constants.MINIMUM_DELAY;

        return attackDelay;
    }
    public static bool attackHits(int dex)
    {
        float accuracy = 100 * (Constants.MINIMUM_ACCURACY + (Constants.ACCURACY_STEP_AMOUNT * dex));
        float r = Random.Range(0, 100);
        bool result = (r < accuracy) ? true : false;

        return result;
    }

    public static bool enemyDodges(int agi)
    {
        //Random.InitState(100);
        float dodge = 100 * (Constants.MINIMUM_DODGE + (Constants.DODGE_STEP_AMOUNT * agi));
        bool result = (Random.Range(0, 100) < dodge) ? true : false;

        return result;
    }
}
