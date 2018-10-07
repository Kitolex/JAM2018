using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public List<Cercle> listCercle;
    public GameObject goCentral;
    public int nb;

	// Use this for initialization
	void Start () {
        nb = listCercle.Count;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void activeDescente()
    {
        if (nb!=0)
        {
            listCercle[nb - 1].tomber();
            nb--;
        }

    }

    public float getDiametreMap()
    {
        Debug.Log("getDiametreMap" + listCercle[nb - 1].getDiametre());
        if (nb!=0)
        {
            return listCercle[nb - 1].getDiametre();
        }

        return goCentral.transform.localPosition.z;

        
    }

    public void InitMap()
    {
        nb = listCercle.Count;
        
        foreach (Cercle c in listCercle)
        {
            c.repop();
        }
    }


}
