﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class gameManagerScript : NetworkBehaviour {
	public GameObject selectedPlayer;
	public int selectedPlayerIndex;
	public GameObject[,] gridContents = new GameObject[8,8];
	public GameObject[] enemies = new GameObject[5];
	public GameObject[] squadMembers = new GameObject[5];
	public GameObject localPlayer;
	public bool moveFlag = false;
	//public GameObject[] wallObjects = new GameObject[20];
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (gridContents [0, 0]);
	}
}