using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public GameObject gameSquare;

	public Material wheat;
	public Material wheatSelected;
	public Material coal;
	public Material coalSelected;
	public Material wood;
	public Material woodSelected;

	public static int[,] board = new int[16, 16];
	public GameObject[,] clones = new GameObject[16,16];

	public int totalCoal = 50;
	public int totalWheat = 50;
	public int totalWood = 50;
	public static int boardCount = 0;

	public Text woodText;
	public Text coalText;
	public Text wheatText;


	void Start()
	{
		woodText = GameObject.FindGameObjectWithTag ("WoodTexto").GetComponent<Text>();
		coalText = GameObject.FindGameObjectWithTag ("CoalTexto").GetComponent<Text>();
		wheatText = GameObject.FindGameObjectWithTag ("WheatTexto").GetComponent<Text>();
		if (isServer) {
			makeLocalBoard ();
			renderLocalBoard (board);
			Debug.Log ("making Board");
		}
	}

	void Update(){
		woodText.text = "Wood: " + totalWood;
		coalText.text = "Coal: " + totalCoal;
		wheatText.text = "Wheat: " + totalWheat;
	}

	void OnDisable(){
		destroyLocalBoard ();
	}

	public void makeLocalBoard(){
		Random.InitState(10);
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
					clones [i, j].GetComponentInChildren<SquareController>().squareMaterial = wood;
					clones [i, j].GetComponentInChildren<SquareController>().squareMaterialSelected = woodSelected;
				}else if (board[i,j] == 1) {
					clones [i, j].GetComponentInChildren<Renderer>().material = coal;
					clones [i, j].GetComponentInChildren<SquareController>().squareMaterial = coal;
					clones [i, j].GetComponentInChildren<SquareController>().squareMaterialSelected = coalSelected;
				}else if (board[i,j] == 2) {
					clones [i, j].GetComponentInChildren<Renderer>().material = wheat;
					clones [i, j].GetComponentInChildren<SquareController>().squareMaterial = wheat;
					clones [i, j].GetComponentInChildren<SquareController>().squareMaterialSelected = wheatSelected;
				}
				clones [i, j].GetComponentInChildren<SquareController> ().squareType = board [i, j];
				clones [i, j].GetComponentInChildren<SquareController>().playerObject = gameObject;
				NetworkServer.Spawn (clones [i, j]);
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

