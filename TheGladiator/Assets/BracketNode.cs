using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BracketNode : MonoBehaviour {

    bool player;
    void resetIndex()
    {
        
        player = true;
    }
    public void drawNode(int index, List<int> enemieIdexes)
    {
        player = GetComponentInParent<BracketLayer>().player;
        index = (index >= enemieIdexes.Count) ? 0 : index;
        if (player && this.tag == "playerablenode")
        {
            player = false;
            this.GetComponentInChildren<BracketCharacterDisplay>().Draw(MasterManager.ManagerGlobalData.GetPlayerDataInfo());
            this.GetComponentInChildren<Text>().text = MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Name;
        }
        else if (this.tag != "playerablenode")
        {
            this.GetComponentsInChildren<BracketCharacterDisplay>()[0].Draw(MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[enemieIdexes[index]]);
            this.GetComponentInChildren<Text>().text = MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[enemieIdexes[index]].statsList[0].Name;
            index++;
        }
    }

}
