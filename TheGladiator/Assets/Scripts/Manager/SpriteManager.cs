using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpriteManager : MonoBehaviour, IManager
{

    public Texture2D characterTexture;

    [HideInInspector]
    public List<Sprite> BodyList;
    [HideInInspector]
    public List<Sprite> HairList;
    [HideInInspector]
    public List<Sprite> ArmorList;
    [HideInInspector]
    public List<Sprite> FacialHairList;
    [HideInInspector]
    public List<Sprite> LeftHandList;
    [HideInInspector]
    public List<Sprite> RightHandList;
    [HideInInspector]
    public List<Sprite> HelmetList;
    [HideInInspector]
    public List<Sprite> PantsList;
    [HideInInspector]
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

    private void Awake()
    {
        Sprite empty = new Sprite(); //Here so that there is an option for no hair
        HairList.Add(empty);
        FacialHairList.Add(empty);

        foreach (Object o in AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(characterTexture)))
        {
            if (o.name.Contains("Pants"))
            {
                PantsList.Add(o as Sprite);
            }
            else if (o.name.Contains("Helmet"))
            {
                HelmetList.Add(o as Sprite);
            }
            else if (o.name.Contains("RightHand"))
            {
                RightHandList.Add(o as Sprite);
            }
            else if (o.name.Contains("LeftHand"))
            {
                LeftHandList.Add(o as Sprite);
            }
            else if (o.name.Contains("FaceHair"))
            {
                FacialHairList.Add(o as Sprite);
            }
            else if (o.name.Contains("Armor"))
            {
                ArmorList.Add(o as Sprite);
            }
            else if (o.name.Contains("Hair"))
            {
                HairList.Add(o as Sprite);
            }
            else if (o.name.Contains("Body"))
            {
                BodyList.Add(o as Sprite);
            }
            else if (o.name.Contains("Shoes"))
            {
                ShoesList.Add(o as Sprite);
            }
        }
    }

    public Sprite GetSprite(int index, Constants.SpriteType type)
    {
        Sprite result = new Sprite();

        switch (type)
        {
            case Constants.SpriteType.ARMOR:
                result = ArmorList[index];
                break;
            case Constants.SpriteType.BODY:
                result = BodyList[index];
                break;
            case Constants.SpriteType.FACIAL_HAIR:
                result = FacialHairList[index];
                break;
            case Constants.SpriteType.FOOT:
                result = ShoesList[index];
                break;
            case Constants.SpriteType.HAIR:
                result = HairList[index];
                break;
            case Constants.SpriteType.HELMET:
                result = HelmetList[index];
                break;
            case Constants.SpriteType.LEFT_HAND:
                result = LeftHandList[index];
                break;
            case Constants.SpriteType.PANTS:
                result = PantsList[index];
                break;
            case Constants.SpriteType.RIGHT_HAND:
                result = RightHandList[index];
                break;
        }

        return result;
    }

    // Use this for initialization
    void Start()
    {

    }

    public void Initialize()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
