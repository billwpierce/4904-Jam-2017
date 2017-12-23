using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSquareScript : MonoBehaviour {

	public bool owned = false;
	private float nextResource = 0.0F;
	private float resourceRate = 5.0F;

	void Update(){
		if (owned && Time.time > nextResource) {
			Debug.Log ("Resource Gained.");
			nextResource += resourceRate;
		}
	}

	public void OnMouseDown(){
		if (!owned) {
			Debug.Log ("Unowned, now owned.");
			owned = true;
		}else{
			Debug.Log ("Still owned.");
		}
	}

}
