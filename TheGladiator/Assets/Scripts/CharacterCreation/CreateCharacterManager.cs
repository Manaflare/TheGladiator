using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacterManager : MonoBehaviour {

    public Image bodySprite;
    public Text bodyText;
    private int bodyIndex;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        bodyText.text = bodyIndex.ToString();

    }
    
    public void bodyArrowPressed(string direction)
    {
        if(direction == "LEFT")
        {
            bodyIndex--;
            if (bodyIndex < 0)
            {
                bodyIndex = SpriteManager.Instance.BodyList.Count - 1;
            }
        }
        else
        {
            bodyIndex++;
            if (bodyIndex > SpriteManager.Instance.BodyList.Count - 1)
            {
                bodyIndex = 0;
            }
        }

        bodySprite.sprite = SpriteManager.Instance.BodyList[bodyIndex];
    }
}
