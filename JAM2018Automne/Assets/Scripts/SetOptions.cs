using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SetOptions : MonoBehaviour {

    public Button[] buttons;
    public Sprite uncheckImg;
    public Sprite checkImg;

    public AudioMixer audioMixer;

    private int numberHearts;


    public void SetHearts(int buttonNumber)
    {
        foreach(Button btn in buttons)
        {
            btn.image.sprite = uncheckImg;
        }
        for(int i = 0; i < buttonNumber; i++ )
        {
            buttons[i].image.sprite = checkImg;
        }
        numberHearts = buttonNumber;

    }

    public int getNumberOfHearts() { return numberHearts; }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
