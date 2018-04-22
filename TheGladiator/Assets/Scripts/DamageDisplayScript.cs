using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageDisplayScript : MonoBehaviour {
    public Texture2D numbersImage;
    public GameObject NumberPrefab;

    Dictionary<string, Sprite> numbers;

    public float value;
    public int multiplier = -1;
	// Use this for initialization
	void Start ()
    {
        
        foreach(Transform o in transform)
        {
            Destroy(o.gameObject);
        }

        numbers = new Dictionary<string, Sprite>();

        foreach (Object o in Resources.LoadAll<Sprite>("UI\\" + numbersImage.name))
        {
            numbers.Add(numbers.Count.ToString(), o as Sprite);
        }

        this.GetComponent<Rigidbody2D>().AddForce(new Vector3(3500 * multiplier, 12500));
        Display(value);
	}
    private void OnEnable()
    {
        Start();
    }

    public void Display(float damage)
    {
        string dmgAsString = Mathf.FloorToInt(damage).ToString();

        foreach(char a in dmgAsString)
        {
            GameObject g = Instantiate(NumberPrefab, this.transform);
            g.GetComponent<Image>().sprite = numbers[a.ToString()];
        }

    }
}
