using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : MonoBehaviour {

	public bool owned = false;
	private float nextResource = 0.0F;
	private float resourceRate = 5.0F;

	public Material squareMaterial;
	public Material squareMaterialSelected;

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

	void OnMouseOver(){
		this.GetComponent<Renderer> ().material = this.squareMaterialSelected;
	}

	void OnMouseExit(){
		this.GetComponent<Renderer> ().material = this.squareMaterial;
	}

}
