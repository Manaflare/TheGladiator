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
        GameObject g = Instantiate(dmgPrefab, this.transform.root);
        g.SetActive(true);
        Vector2 pos = new Vector2(40, 200);
        if (this.name == "Player1")
        {
            pos.x = 800;
        }

        g.GetComponent<RectTransform>().position = pos;
        g.GetComponent<DamageDisplayScript>().value = this.GetComponent<Attribute>().getSTATS().Strength;
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
