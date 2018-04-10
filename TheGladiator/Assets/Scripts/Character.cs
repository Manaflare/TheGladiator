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
        g.GetComponent<RectTransform>().rect.Set(300, 0, 0, 0);
        g.GetComponent<DamageDisplayScript>().value = 100;
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
