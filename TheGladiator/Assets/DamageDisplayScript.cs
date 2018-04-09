using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageDisplayScript : MonoBehaviour {
    public Texture2D numbersImage;
    public GameObject NumberPrefab;
    Dictionary<string, Sprite> numbers;

	// Use this for initialization
	void Start () {
        numbers = new Dictionary<string, Sprite>();
        foreach (Object o in Resources.LoadAll<Sprite>("UI\\" + numbersImage.name))
        {
            numbers.Add(numbers.Count.ToString(), o as Sprite);
        }
        Display(100);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Display(float damage)
    {
        string dmgAsString = (int.Parse(damage.ToString())).ToString();

        foreach(char a in dmgAsString)
        {
            GameObject g = Instantiate(NumberPrefab, this.transform);
            g.GetComponent<Image>().sprite = numbers[a.ToString()];
        }

    }
}
