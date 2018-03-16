using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUnlockScript : MonoBehaviour
{
    private ListDataInfo playerDataInfo;
    private ListItemsInfo itemDataInfo;

    public byte winStreak;

    private int currentTier;
    private int itemTier;
    private int rngCannon;
    

    // Use this for initialization
	void Start ()
    {
		Random random = new Random();
        rngCannon = Random.Range(0, 100);

        playerDataInfo = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        currentTier = playerDataInfo.playerTier;

        itemDataInfo = MasterManager.ManagerGlobalData.GetItemDataInfo();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (winStreak == 3)
        {

        }
    }

    void randomItem()
    {

        if (rngCannon < 50)
        {
            itemTier = currentTier - 1;
        }
        else if (rngCannon > 50 && rngCannon < 85)
        {
            itemTier = currentTier;
        }
        else
        {
            itemTier = currentTier + 1;
        }

        List<ItemDataInfo> itemData = new List<ItemDataInfo>();

        for (int i = 0; i < itemDataInfo.itemData.Count; i++)
        {
            if (itemDataInfo.itemData[i].Tier == itemTier)
            {
                itemData.Add(itemDataInfo.itemData[i]);
            }
        }

        int itemID = Random.Range(0, itemData.Count - 1);

        //itemData[itemID]
        
    }

}
