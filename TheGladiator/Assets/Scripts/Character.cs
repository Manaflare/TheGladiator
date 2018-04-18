using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public bool isAttacking = false;
    public GameObject dmgPrefab;
    public void startAttack()
    {
        isAttacking = true;
    }
    public void endAttack()
    {
        GameObject ai = GameObject.FindGameObjectWithTag("aimanager");
        ai.GetComponent<AIManager>().CanAttack = true;
        isAttacking = false;
    }
    public void displayDamage()
    {
        Vector2 pos = new Vector2(-300, -55);
        GameObject g = Instantiate(dmgPrefab, this.transform.root);
        g.GetComponent<DamageDisplayScript>().multiplier = 2;
        if (this.name == "Player1")
        {
            g.GetComponent<DamageDisplayScript>().multiplier = -1;
            pos.x = 275;
        }
        g.GetComponent<RectTransform>().localPosition = pos;
        g.GetComponent<DamageDisplayScript>().value = this.GetComponent<Attribute>().getSTATS().Strength;
        g.SetActive(true);
    }
	// Use this for initialization
	protected virtual void Start ()
    {
		
	}

    // Update is called once per frame
    protected virtual void Update ()
    {
		
	}
}
