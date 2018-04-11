using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleBarController : MonoBehaviour {
    public Slider healthBar;
    public Slider StaminaBar;
    public Text currentHPValue;
    public Text currentStamValue;
    public Text maxHPValue;
    public Text maxStamValue;

    // Use this for initialization
    void Start () {
        set(80000, 101, 10, 15);
	}
	
    public void set(float currentHP, float maxHP, float stamina, float maxStamina )
    {

        currentHP = (currentHP <= 0) ? 0 : currentHP;

        currentHPValue.text = ((int)currentHP).ToString();
        currentStamValue.text = ((int)stamina).ToString();

        maxHPValue.text = ((int)maxHP).ToString();
        maxStamValue.text = ((int)maxStamina).ToString();

        healthBar.maxValue = maxHP;
        StaminaBar.maxValue = maxStamina;

        healthBar.value = currentHP;
        StaminaBar.value = stamina;

        healthBar.minValue = 0.0f;
        StaminaBar.minValue = 0.0f;
    }
}
