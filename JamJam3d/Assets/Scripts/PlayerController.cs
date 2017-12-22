using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public GameObject wheat;
	public GameObject coal;
	public GameObject wood;

	public int[,] board = new int[16, 16];
	public GameObject[,] clones = new GameObject[16,16];

	void Start()
	{
		makeLocalBoard ();
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

		this.renderLocalBoard(this.board);

	}
	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.blue;
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
				int xpos = i - (board.GetLength (0) / 2);
				int ypos = j - (board.GetLength (1) / 2);
				Destroy (clones [i, j]);
				if (board[i,j] == 0) {
					clones[i,j] = (GameObject) Instantiate (wood, new Vector3 (xpos, 0, ypos), new Quaternion(0,0,0,0));
				}else if (board[i,j] == 1) {
					clones[i,j] = (GameObject) Instantiate (coal, new Vector3 (xpos, 0, ypos), new Quaternion(0,0,0,0));
				}else if (board[i,j] == 2) {
					clones[i,j] = (GameObject) Instantiate (wheat, new Vector3 (xpos, 0, ypos), new Quaternion(0,0,0,0));
				}
			}
		}
	}
}

