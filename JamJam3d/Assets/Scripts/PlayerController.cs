using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public GameObject gameSquare;

	public Material wheat;
	public Material coal;
	public Material wood;

	public int[,] board = new int[16, 16];
	public GameObject[,] clones = new GameObject[16,16];

	void Start()
	{
		makeLocalBoard ();
		renderLocalBoard(board);
	}

	void Update()
	{
		if (!isLocalPlayer)
		{
			return;
		}
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);
	}
	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.blue;
	}

	void OnDisable(){
		destroyLocalBoard ();
	}

	public void makeLocalBoard(){
		for (int i = 0; i < board.GetLength(0); i++) {
			for (int j = 0; j < board.GetLength(1); j++) {
				board[i,j] = Random.Range(0, 3);
			}
		}
	}

	public void renderLocalBoard(int[,] board)
	{
		for (int i = 0; i < board.GetLength(0); i++) {
			for (int j = 0; j < board.GetLength(1); j++) {
				float xpos = i - (board.GetLength (0) / 2) + 0.5f;
				float ypos = j - (board.GetLength (1) / 2) + 0.5f;
				clones[i,j] = (GameObject) Instantiate (gameSquare, new Vector3 (xpos, 0, ypos), new Quaternion(0,0,0,0));
				if (board[i,j] == 0) {
					clones [i, j].GetComponentInChildren<Renderer>().material = wood;
				}else if (board[i,j] == 1) {
					clones [i, j].GetComponentInChildren<Renderer>().material = coal;
				}else if (board[i,j] == 2) {
					clones [i, j].GetComponentInChildren<Renderer>().material = wheat;
				}
			}
		}
	}

	public void destroyLocalBoard(){
		for (int i = 0; i < board.GetLength (0); i++) {
			for (int j = 0; j < board.GetLength (1); j++) {
				Destroy (clones [i, j]);
			}
		}
	}
}

