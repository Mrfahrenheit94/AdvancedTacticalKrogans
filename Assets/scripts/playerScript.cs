using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class playerScript : NetworkBehaviour {
	[SyncVar]
	public Vector2 enemyMovement;
	public gameManagerScript gameManagerScriptRef ;
	public GameObject wallPrefab;
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

	public void wallPlacement(Vector2 coords){
		gameManagerScriptRef.wallPlacementMode = false;
		GameObject.Find("wallButton").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
		gameManagerScriptRef.moveFlag = true;
		CmdWallPlacement (coords);
	}

	public void turretPlacement(Vector2 coords){
		gameManagerScriptRef.turretFlag = true;
		CmdTurretPlacement (coords);
	}

	public void pingLocation (Vector2 coords){
		gameManagerScriptRef.pingFlag = true;
		CmdPingLocation (coords);
	}
	public void deadPlayer(Vector2 coords){
		gameManagerScriptRef.deathFlag = true;
		CmdDeadPlayer (coords);
	}
	public void decoyPlacement(Vector2 coords){
		gameManagerScriptRef.decoyFlag = true;
		CmdDecoyPlacement (coords);
	}
	public void attackTilePlacement(Vector2 coords){
		gameManagerScriptRef.attackTileFlag = true;
		CmdAttackTilePlacement(coords);
	}
	public void deleteAttackTiles(){
		CmdDeleteAttackTiles ();
	}
	[Command]
	public void CmdDeleteAttackTiles(){
		RpcDeleteAttackTiles ();
	}
	[Command]
	public void CmdAttackTilePlacement(Vector2 coords){
		RpcAttackTilePlacement (coords);
	}
	[Command]
	public void CmdDecoyPlacement (Vector2 coords){
		RpcDecoyPlacement (coords, "hello");
	}
	[Command]
	public void CmdDeadPlayer (Vector2 coords){
		RpcDeadPlayer (coords);
	}
	[Command]
	public void CmdTurretPlacement(Vector2 coords){
		RpcTurretPlacement (coords);
	}
	[Command]
	public void CmdPingLocation(Vector2 coords){
		RpcPingLocation (coords);
	}
	[Command]
	public void CmdMoveEnemy1(Vector2 coords, int SPI){
		RpcUpdateEnemy (coords, SPI);
	}
	[Command]
	public void CmdRotateEnemy(int angle, int SPI){
		RpcRotateEnemy (angle, SPI);
	}
	[Command]
	public void CmdSetCloakEnemy(int SPI){
		RpcSetCloakEnemy(SPI);
	}
	[Command]
	public void CmdWallPlacement(Vector2 coords){
		RpcWallPlacement (coords);
	}
	[ClientRpc]
	public void RpcDeadPlayer (Vector2 coords){
		if (!gameManagerScriptRef.deathFlag) {
			Destroy(gameManagerScriptRef.gridContents[(int) coords.x, (int) coords.y]);
		}
		gameManagerScriptRef.deathFlag = false;
	}
	[ClientRpc]
	public void RpcAttackTilePlacement (Vector2 coords){
		if (!gameManagerScriptRef.attackTileFlag) {

				Instantiate(gameManagerScriptRef.attackTilePrefab, new Vector3(coords.x ,coords.y, 0), Quaternion.identity);

		}
		gameManagerScriptRef.attackTileFlag = false;
	}
	[ClientRpc]
	public void RpcDeleteAttackTiles (){
		foreach(GameObject attackTiles in GameObject.FindGameObjectsWithTag("attackTile"))
			Destroy(attackTiles);
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
		if (GameObject.FindWithTag ("turret") != null) {
			GameObject.FindWithTag ("turret").SendMessage("updateTurret");
		}
		if (GameObject.FindWithTag ("sensor") != null) {
			GameObject.FindWithTag ("sensor").SendMessage("updateTurret");
		}
		gameManagerScriptRef.moveFlag = false;

	}
	[ClientRpc]
	public void RpcPingLocation(Vector2 coords){
		if (!gameManagerScriptRef.pingFlag) {
			Instantiate(gameManagerScriptRef.pingPrefab, new Vector3 (coords.x, coords.y, 0), Quaternion.identity);
		}
		gameManagerScriptRef.pingFlag = false;
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
			} 
			else {
				gameManagerScriptRef.enemies [SPI].tag = "wall";
			}
			if (gameManagerScriptRef.selectedPlayer != null) {
				
				gameManagerScriptRef.selectedPlayer.SendMessage ("checkOOB");
			}
		}
		gameManagerScriptRef.moveFlag = false;
	}

	[ClientRpc]
	public void RpcWallPlacement(Vector2 coords){
		if (!gameManagerScriptRef.moveFlag) {
			if(gameManagerScriptRef.gridContents[(int) coords.x, (int) coords.y]==null)
			{
				gameManagerScriptRef.gridContents[(int) coords.x, (int) coords.y] = (GameObject) Instantiate (wallPrefab, new Vector3 (coords.x, coords.y, 0), Quaternion.identity);

			}
			else{
				Destroy(gameManagerScriptRef.gridContents[(int) coords.x, (int) coords.y]);
			}

		}
		if (gameManagerScriptRef.selectedPlayer != null) {
			
			gameManagerScriptRef.selectedPlayer.SendMessage ("checkOOB");
		}
		gameManagerScriptRef.moveFlag = false;

	}

	[ClientRpc]
	public void RpcTurretPlacement(Vector2 coords){
		if (!gameManagerScriptRef.turretFlag) {
			if(gameManagerScriptRef.gridContents[(int) coords.x, (int) coords.y]==null)
			{
				gameManagerScriptRef.gridContents[(int) coords.x, (int) coords.y] = (GameObject) Instantiate (gameManagerScriptRef.enemyTurretPrefab, new Vector3 (coords.x, coords.y, 0), Quaternion.identity);
				
			}
			else{
				Destroy(gameManagerScriptRef.gridContents[(int) coords.x, (int) coords.y]);
			}
			
		}
		if (gameManagerScriptRef.selectedPlayer != null) {
			
			gameManagerScriptRef.selectedPlayer.SendMessage ("checkOOB");
		}
		gameManagerScriptRef.turretFlag = false;
		
	}
	[ClientRpc]
	public void RpcDecoyPlacement(Vector2 coords, string enemyType){
		if (!gameManagerScriptRef.decoyFlag) {
			if(gameManagerScriptRef.gridContents[(int) coords.x, (int) coords.y]==null)
			{
				gameManagerScriptRef.gridContents[(int) coords.x, (int) coords.y] = (GameObject) Instantiate (gameManagerScriptRef.enemyTurretPrefab, new Vector3 (coords.x, coords.y, 0), Quaternion.identity);
				
			}
			else{
				Destroy(gameManagerScriptRef.gridContents[(int) coords.x, (int) coords.y]);
			}
			
		}
		if (gameManagerScriptRef.selectedPlayer != null) {
			
			gameManagerScriptRef.selectedPlayer.SendMessage ("checkOOB");
		}
		gameManagerScriptRef.decoyFlag = false;
		
	}

}
