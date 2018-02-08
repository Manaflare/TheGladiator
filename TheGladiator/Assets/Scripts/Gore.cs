using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEditor;
using UnityEngine.UI;

public class Gore : MonoBehaviour {
    public Texture2D goreTexture;
    public Canvas canvas;
    public Vector2 basePosition;
    Image[] imageChildren;
    float timeAlive;
    float maxTime = 2.5f;
    // Use this for initialization
    void OnEnable ()
    {
        canvas = GameObject.FindObjectOfType<Canvas>();
        timeAlive = 0.0f;
        Vector2 startPos = new Vector2(-18.75f, +31.25f);
        imageChildren = GameObject.FindGameObjectWithTag("goreparent").GetComponentsInChildren<Image>();

        Vector2 offset = new Vector2(0, 0);
        foreach(var enable in imageChildren)
        {
            enable.GetComponent<PointEffector2D>().enabled = true;
            enable.GetComponent<Rigidbody2D>().AddTorque(18000.0f);
        }
        int currentLayerLength = 3;
        
        int index = 0;
        foreach (Object o in AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(goreTexture)))
        {
            if (o.GetType().ToString() == "UnityEngine.Sprite")
            {
                if (index > currentLayerLength)
                {
                    offset.x = 0;
                    offset.y -= 12.5f;
                    if (index == 4)
                    {
                        currentLayerLength += 4;
                    }
                    if (index == 8 || index == 39)
                    {
                        currentLayerLength += 7;
                        offset.x = -18.75f;
                    }
                    if (index == 15 || index == 23 || index == 31)
                    {
                        currentLayerLength += 8;
                        offset.x = -25.0f;
                    }
                    if (index == 39)
                    {

                    }
                }

                Sprite temp = o as Sprite;
                imageChildren[index].sprite = temp;
                Rect r = new Rect(index, 0, 2, 2);
                //imageChildren[index].rectTransform.anchoredPosition = new Vector3(-368.75f + offset.x, -103.75f + offset.y,0);
                imageChildren[index].rectTransform.anchoredPosition = new Vector3(startPos.x + offset.x, startPos.y + offset.y, 0);
                offset.x += 12.5f;
                index++;
            }
        }
    }

    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive < maxTime)
        {

            float MinBoundaryY = Mathf.Abs(canvas.GetComponent<RectTransform>().rect.height / 2 + this.GetComponent<RectTransform>().anchoredPosition.y) * -1;
            float MaxBoundaryY = canvas.GetComponent<RectTransform>().rect.height + MinBoundaryY;

            float MinBoundaryX = Mathf.Abs(canvas.GetComponent<RectTransform>().rect.width / 2 + this.GetComponent<RectTransform>().anchoredPosition.x) * -1;
            float MaxBoundaryX = canvas.GetComponent<RectTransform>().rect.width + MinBoundaryX;

            foreach (Image i in imageChildren)
            {
                if (i.GetComponent<RectTransform>().anchoredPosition.y <= MinBoundaryY)
                {
                    i.GetComponent<RectTransform>().anchoredPosition = new Vector2(i.GetComponent<RectTransform>().anchoredPosition.x, MinBoundaryY);
                    i.GetComponent<PointEffector2D>().enabled = false;
                }

                if (i.GetComponent<RectTransform>().anchoredPosition.y >= MaxBoundaryY)
                {
                    i.GetComponent<RectTransform>().anchoredPosition = new Vector2(i.GetComponent<RectTransform>().anchoredPosition.x, MaxBoundaryY);
                    i.GetComponent<PointEffector2D>().enabled = false;

                }

                if (i.GetComponent<RectTransform>().anchoredPosition.x <= MinBoundaryX)
                {
                    i.GetComponent<RectTransform>().anchoredPosition = new Vector2(MinBoundaryX, i.GetComponent<RectTransform>().anchoredPosition.y);
                    i.GetComponent<PointEffector2D>().enabled = false;

                }

                if (i.GetComponent<RectTransform>().anchoredPosition.x >= MaxBoundaryX)
                {
                    i.GetComponent<RectTransform>().anchoredPosition = new Vector2(MaxBoundaryX, i.GetComponent<RectTransform>().anchoredPosition.y);
                    i.GetComponent<PointEffector2D>().enabled = false;

                }

            }
        }

        else
        {
            foreach(Image i in imageChildren)
            {
                Destroy(i);
            }
            imageChildren = new Image[46];
        }
    }
}
