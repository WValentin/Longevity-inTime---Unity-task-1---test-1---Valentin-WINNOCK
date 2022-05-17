using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarCustomization : MonoBehaviour
{
    public GameObject[] hair;
    public GameObject avatarSkin;
    public Color[] SkinColors;

    private int currentHair;

    void Update()
    {
        for (int i = 0; i < hair.Length; i++)
        {
            if (i == currentHair)
                hair[i].SetActive(true);
            else
                hair[i].SetActive(false);
        }
    }

    public void SwitchHair()
    {
        if (currentHair == hair.Length - 1)
            currentHair = 0;
        else
            currentHair++;
    }

    public void SwitchSkinColor1()
    {
        avatarSkin.GetComponent<Renderer>().material.color = SkinColors[0];
    }

    public void SwitchSkinColor2()
    {
        avatarSkin.GetComponent<Renderer>().material.color = SkinColors[1];
    }

    public void SwitchSkinColor3()
    {
        avatarSkin.GetComponent<Renderer>().material.color = SkinColors[2];
    }
    
    public void SwitchSkinColor4()
    {
        avatarSkin.GetComponent<Renderer>().material.color = SkinColors[3];
    }

    public void SwitchSkinColor5()
    {
        avatarSkin.GetComponent<Renderer>().material.color = SkinColors[4];
    }

    public void SwitchSkinColor6()
    {
        avatarSkin.GetComponent<Renderer>().material.color = SkinColors[5];
    }
}
