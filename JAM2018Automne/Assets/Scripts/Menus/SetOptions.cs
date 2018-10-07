using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class SetOptions : MonoBehaviour {

    public Button[] buttons;
    public Sprite uncheckImg;
    public Sprite checkImg;

    public AudioMixer audioMixer;

    private int numberHearts;

    public EventSystem e;
    public GameObject go;

    public void Setactive()
    {
        gameObject.SetActive(true);
        e.SetSelectedGameObject(go);
    }


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
