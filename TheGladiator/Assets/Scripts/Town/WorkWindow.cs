using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkWindow : MonoBehaviour
{
    public Text workTitle;
    public Image workImage;
    public Text workDesc;
    public Scrollbar scrollbar;
    // Use this for initialization

    public void SetWorkWindow(string title, string desc, string imageName)
    {
        workTitle.text = Utility.GetLocalizedString(title);
        workDesc.text = Utility.GetLocalizedString(desc);
        Sprite sprite = Resources.Load<Sprite>("Sprites/Work/" + imageName);
        if(sprite == null)
        {
            sprite = Resources.Load<Sprite>("Sprites/Work/no");
        }
        workImage.sprite = sprite;
        scrollbar.value = 1;
    }
}
