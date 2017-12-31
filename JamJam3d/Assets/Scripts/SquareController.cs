using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : MonoBehaviour {

	public bool owned = false;
	private float nextResource = 0.0F;
	private float resourceRate = 5.0F;
	private int resourceStep = 10;

	public Material squareMaterial;
	public Material squareMaterialSelected;

	public int squareType;

	public GameObject playerObject;

	void Update(){
		if (owned && Time.time > nextResource) {
			if (squareType == 0) {
				playerObject.GetComponent<PlayerController> ().totalWood += resourceStep;
			} else if (squareType == 1) {
				playerObject.GetComponent<PlayerController> ().totalCoal += resourceStep;
			} else {
				playerObject.GetComponent<PlayerController> ().totalWheat += resourceStep;
			}
			nextResource += resourceRate;
		}
	}

	public void OnMouseDown(){
		if (!owned) {
			Debug.Log ("Unowned, now owned.");
			owned = true;
			nextResource = Time.time;
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
