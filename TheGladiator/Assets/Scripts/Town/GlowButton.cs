using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowButton : MonoBehaviour {

    public Image image;
    private float overlayIntensity = 0.0f;

    [SerializeField]
    private bool glow_on = false;

    [SerializeField]
    private float fade_time = 2.0f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
        if(glow_on)
        {
            if (overlayIntensity >= 1.0f)
                return;

            overlayIntensity = Mathf.Min(1.0f, overlayIntensity + Time.deltaTime / fade_time);
            
        }
        else
        {
            if (overlayIntensity <= 0.0f)
                return;

            overlayIntensity = Mathf.Max(0.0f, overlayIntensity - Time.deltaTime / fade_time);
        }

        image.color = new Color(image.color.r, 0, 0, overlayIntensity);
	}

    public void StartGlow()
    {
        glow_on = true;
    }

    public void EndGlow()
    {
        glow_on = false;
    }
}
