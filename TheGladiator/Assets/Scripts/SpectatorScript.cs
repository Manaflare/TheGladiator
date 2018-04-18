using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
public class SpectatorScript : MonoBehaviour {

   public Image body;
   public Image hair;
   public Image facehair;
   public Image armor;
   public Image pants;
   public Image shoes;

    Animator anim;
    public AnimationClip[] clips;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        body.sprite = MasterManager.ManagerSprite.BodyList[Random.Range(0, MasterManager.ManagerSprite.BodyList.Count)];
        hair.sprite = MasterManager.ManagerSprite.HairList[Random.Range(1, MasterManager.ManagerSprite.HairList.Count)];
        facehair.sprite = MasterManager.ManagerSprite.FacialHairList[Random.Range(1, MasterManager.ManagerSprite.FacialHairList.Count)];
        armor.sprite = MasterManager.ManagerSprite.ArmorList[Random.Range(0, MasterManager.ManagerSprite.ArmorList.Count)];
        shoes.sprite = MasterManager.ManagerSprite.ShoesList[Random.Range(0, MasterManager.ManagerSprite.ShoesList.Count)];
        pants.sprite = MasterManager.ManagerSprite.PantsList[Random.Range(0, MasterManager.ManagerSprite.BodyList.Count)];
        PlayRandomAnimation();
    }

    public void PlayRandomAnimation()
    {
        float randomStart = Random.Range(0f, 1f);
        AnimationClip selectedClip = clips[Random.Range(0, clips.Length)];

        anim.Play(selectedClip.name);

    }
}
