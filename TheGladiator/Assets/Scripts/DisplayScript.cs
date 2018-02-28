using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScript : MonoBehaviour {
    Image body;
    Image hair;
    Image armor;
    Image facialHair;
    Image leftHand;
    Image rightHand;
    Image helmet;
    Image pants;
    Image shoes;
    //TEMPORARY
    [Header("Temporary Customization In Inspector")]
    [Range(0, 7)]
    public int bodyValue;
    [Range(0, 59)]
    public int hairValue;
    [Range(0, 19)]
    public int facialHairValue;
    //END TEMPORARY

    // Use this for initialization
    void Start() {
        Image[] bodyParts = GetComponentsInChildren<Image>();
        
        body       = bodyParts[0];
        hair       = bodyParts[1];
        armor      = bodyParts[2];
        facialHair = bodyParts[3];
        leftHand   = bodyParts[4];
        rightHand  = bodyParts[5];
        helmet     = bodyParts[6];
        pants      = bodyParts[7];
        shoes      = bodyParts[8];

        Color t = bodyParts[0].color;
        t.a = 0;
        armor.color = t;
        leftHand.color = t;
        rightHand.color = t;
        helmet.color = t;
        pants.color = t;
        shoes.color = t;


        updateSprite();
    }
    private void OnValidate()
    {
        if (body != null)
            updateSprite();
    }

    void updateSprite()
    {
        body.sprite         = SpriteManager.Instance.BodyList[bodyValue];
        hair.sprite         = SpriteManager.Instance.HairList[hairValue];
        facialHair.sprite   = SpriteManager.Instance.FacialHairList[facialHairValue];
        //leftHand   = bodyParts[3];
        //rightHand  = bodyParts[4];
        //helmet     = bodyParts[5];
        //pants      = bodyParts[6];
        //shoes      = bodyParts[7];
    }
}
