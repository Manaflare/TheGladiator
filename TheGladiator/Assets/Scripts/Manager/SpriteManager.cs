using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour, IManager {

    public List<Sprite> BodyList;
    public List<Sprite> HairList;
    public List<Sprite> ArmorList;
    public List<Sprite> FacialHairList;
    public List<Sprite> LeftHandList;
    public List<Sprite> RightHandList;
    public List<Sprite> HelmetList;
    public List<Sprite> PantsList;
    public List<Sprite> ShoesList;


    private static SpriteManager instance;
    public static SpriteManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<SpriteManager>();
                if (instance == null)
                {
                    GameObject container = new GameObject("SpriteManager");
                    instance = container.AddComponent<SpriteManager>();
                }
            }

            return instance;
        }
    }



    // Use this for initialization
    void Start () {
		
	}

    public void Initialize()
    {
       
    }

    // Update is called once per frame
    void Update () {
		
	}
}
