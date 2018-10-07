using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresentateurManager : MonoBehaviour {

    public static PresentateurManager PManager;
    public Text dialog;
    public Text[] props = new Text[3];

	void Awake () {
        PManager = this;
	}
	
    public void setStatementText(string statement)
    {
        dialog.text = statement;
        dialog.enabled = true;
    }

    public void setPropText(string prop, int index)
    {
        props[index].text = prop;
        props[index].enabled = true;
    }

    public void hideText()
    {
        dialog.enabled = false;
        for (int i = 0; i < props.Length; i++)
        {
            props[i].enabled = false;
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
