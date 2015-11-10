﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class playerScript : NetworkBehaviour {
	[SyncVar]
	public Vector2 enemyMovement;
	public gameManagerScript gameManagerScriptRef ;
	// Use this for initialization
	void Start () {
		gameManagerScriptRef = GameObject.Find ("gameManager").GetComponent<gameManagerScript> ();
		if (isLocalPlayer)
			gameManagerScriptRef.localPlayer = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	//if (Input.GetKeyDown ("space") && isLocalPlayer) {
			//CmdMoveEnemy1(new Vector2((int)Random.Range(0,6), 6));

		//}
	}

	public void moveEnemy(Vector2 coords){
		int SPI = gameManagerScriptRef.selectedPlayerIndex;
		CmdMoveEnemy1 (coords, SPI);

	}

	public void rotateEnemy(int angle){
		int SPI = gameManagerScriptRef.selectedPlayerIndex;
		CmdRotateEnemy (angle, SPI);
		
	}

	public void setCloak(){
		if (gameManagerScriptRef.selectedPlayer != null && isLocalPlayer) {
			int SPI = gameManagerScriptRef.selectedPlayerIndex;
			CmdSetCloakEnemy (SPI);
			if(gameManagerScriptRef.selectedPlayer.GetComponent<SpriteRenderer>().color == new Color(1f, 1f, 1f, 0.5f))
					gameManagerScriptRef.selectedPlayer.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
			else
					gameManagerScriptRef.selectedPlayer.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);

		}
	}


	[Command]
	public void CmdMoveEnemy1(Vector2 coords, int SPI){
//		enemyMovement = coords;
		RpcUpdateEnemy (coords, SPI);


	}

	[Command]
	public void CmdRotateEnemy(int angle, int SPI){
		//		enemyMovement = coords;
		RpcRotateEnemy (angle, SPI);
		
		
	}

	[Command]
	public void CmdSetCloakEnemy(int SPI){

		RpcSetCloakEnemy(SPI);
		
		
	}

	[ClientRpc]
	public void RpcUpdateEnemy(Vector2 coords, int SPI){
		if (!gameManagerScriptRef.moveFlag) {//if you weren't the one to move it
			gameManagerScriptRef.gridContents [(int)gameManagerScriptRef.enemies [SPI].transform.position.x, (int)gameManagerScriptRef.enemies [SPI].transform.position.y] = null;
			gameManagerScriptRef.enemies [SPI].transform.position = new Vector3 (coords.x, coords.y, 0);
			gameManagerScriptRef.gridContents [(int)coords.x, (int)coords.y] = gameManagerScriptRef.enemies [SPI];

		}
		if(gameManagerScriptRef.selectedPlayer!=null){
			
			gameManagerScriptRef.selectedPlayer.SendMessage("checkOOB");
		}
		gameManagerScriptRef.moveFlag = false;

	}

	[ClientRpc]
	public void RpcRotateEnemy(int angle, int SPI){
		if (!gameManagerScriptRef.moveFlag) {//if you weren't the one to move it
			gameManagerScriptRef.enemies [SPI].transform.Rotate(new Vector3 (0,0, angle));
		}
		gameManagerScriptRef.moveFlag = false;
		if (gameManagerScriptRef.selectedPlayer != null) {
			
			gameManagerScriptRef.selectedPlayer.SendMessage ("checkOOB");
		}
	}
	
	[ClientRpc]
	public void RpcSetCloakEnemy(int SPI){
		if (!gameManagerScriptRef.moveFlag) {//if you weren't the one to move it
			if (gameManagerScriptRef.enemies [SPI].tag == "wall") {
				gameManagerScriptRef.enemies [SPI].tag = "cloaked";
			} else {
				gameManagerScriptRef.enemies [SPI].tag = "wall";
			}
			if (gameManagerScriptRef.selectedPlayer != null) {
				
				gameManagerScriptRef.selectedPlayer.SendMessage ("checkOOB");
			}
		}
		gameManagerScriptRef.moveFlag = false;
	}
}