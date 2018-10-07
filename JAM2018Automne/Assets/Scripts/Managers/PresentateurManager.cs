using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresentateurManager : MonoBehaviour {

    public static PresentateurManager PManager;
    public Text dialog;
    public Text[] props = new Text[3];
    public Image dialogImg;
    public Image[] propsImg;

	void Awake () {
        PManager = this;
	}
	
    public void setStatementText(string statement)
    {
        dialog.text = statement;
        dialog.enabled = true;
        dialogImg.enabled = true;
    }

    public void setPropText(string prop, int index)
    {
        props[index].text = prop;
        props[index].enabled = true;
        propsImg[index].enabled = true;
    }

    public void hideText()
    {
        dialog.enabled = false;
        dialogImg.enabled = false;
        for (int i = 0; i < props.Length; i++)
        {
            props[i].enabled = false;
            propsImg[i].enabled = false;
        }
        cleanTexts();
    }

    private void cleanTexts()
    {
        dialog.text = "";

        for (int i = 0; i < props.Length; i++)
        {
            props[i].text = "";
        }
    }
}
