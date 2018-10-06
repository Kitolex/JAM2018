using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDeJeuBehaviour : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		
        if(other.tag.Equals("Player")) {
			other.gameObject.GetComponent<PersonnageBehaviour>().sortDeLaMap();
		}
    }
}
