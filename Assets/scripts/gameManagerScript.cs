using UnityEngine;
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
	public bool wallPlacementMode=false;
	public bool pingLocationMode=false;
	public bool turretPlacementMode=false;
	public bool pingFlag=false;
	public GameObject pingPrefab;
	public GameObject friendlyTurretPrefab;
	public GameObject enemyTurretPrefab;
	public bool turretFlag = false;
	public GameObject enemySymbolPrefab;
	public GameObject enemySensorPrefab;
	public bool sensorPlacementMode = false;
	public GameObject sensorPrefab;
	//public GameObject[] wallObjects = new GameObject[20];
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (gridContents [0, 0]);
	}
}
