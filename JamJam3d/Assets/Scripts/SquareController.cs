using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SquareController : MonoBehaviour {

	public bool owned = false;
	private float nextResource = 0.0F;
	private float resourceRate = 1.0F;
	private int resourceStep = 1;

	public int ownedBy = 0;

	public Material squareMaterial;
	public Material squareMaterialSelected;

	public int squareType;

	public GameObject playerObject;

	void Update(){
		if (owned) {
			if(ownedBy == 1){
				// Light Blue Player
				GetComponent<Renderer> ().material.color = new Color(0, 3, 3);
			} else if(ownedBy == 2){
				// Red Player
				GetComponent<Renderer> ().material.color = new Color(5, 0, 0);
			} else if(ownedBy == 3) {
				// Green Player
				GetComponent<Renderer> ().material.color = new Color(0, 1, 0);
			} else if(ownedBy == 4){
				// Purple Player
				GetComponent<Renderer> ().material.color = new Color(0, 0, 5);
			}
			if (Time.time > nextResource) {
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
	}

	public void OnMouseDown(){
		if (!owned) {
			if (playerObject.GetComponent<PlayerController> ().totalWood >= 20) {
				playerObject.GetComponent<PlayerController> ().totalWood -= 20;
				owned = true;
				nextResource = Time.time + resourceRate;
				ownedBy = int.Parse (playerObject.GetComponent<NetworkIdentity> ().netId.ToString ());
			} else {
				Debug.Log ("Not enough resources.");
			}
		}else{
			Debug.Log ("Owned by: " + ownedBy.ToString());
		}
	}

	void OnMouseOver(){
		this.GetComponent<Renderer> ().material = this.squareMaterialSelected;
	}

	void OnMouseExit(){
		this.GetComponent<Renderer> ().material = this.squareMaterial;
	}

}
