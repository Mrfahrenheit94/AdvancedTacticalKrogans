using UnityEngine;
using System.Collections;

public class tileManager : MonoBehaviour {
	public gameManagerScript gameManagerScriptRef;
	public GameObject wallPrefab;
	// Use this for initialization
	void Start () {
		gameManagerScriptRef = GameObject.Find ("gameManager").GetComponent<gameManagerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown(){
		if (gameManagerScriptRef.wallPlacementMode) {
			if (gameManagerScriptRef.gridContents [(int)transform.position.x, (int)transform.position.y] == null) {
				gameManagerScriptRef.gridContents [(int)transform.position.x, (int)transform.position.y] = (GameObject) Instantiate (wallPrefab, new Vector3 (transform.position.x, transform.position.y, 0), Quaternion.identity);
				gameManagerScriptRef.localPlayer.SendMessage("wallPlacement", new Vector2(6-transform.position.x, 6-transform.position.y));
			} 


			else if (gameManagerScriptRef.gridContents [(int)transform.position.x, (int)transform.position.y].tag == "wall") {
				Destroy(gameManagerScriptRef.gridContents [(int)transform.position.x, (int)transform.position.y]);
				gameManagerScriptRef.localPlayer.SendMessage("wallPlacement", new Vector2(6-transform.position.x, 6-transform.position.y));

			}
		}
		else if (gameManagerScriptRef.pingLocationMode) {
			Instantiate(gameManagerScriptRef.pingPrefab, new Vector3(transform.position.x, transform.position.y, 2), Quaternion.identity);
			gameManagerScriptRef.localPlayer.SendMessage("pingLocation", new Vector2(6-transform.position.x, 6-transform.position.y));
		}
		else if (gameManagerScriptRef.turretPlacementMode) {
			if (gameManagerScriptRef.gridContents [(int)transform.position.x, (int)transform.position.y] == null) {
				gameManagerScriptRef.gridContents[(int) transform.position.x, (int) transform.position.y] = (GameObject) Instantiate(gameManagerScriptRef.friendlyTurretPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
			}
			else if (gameManagerScriptRef.gridContents [(int)transform.position.x, (int)transform.position.y].tag == "turret") {
				Destroy(gameManagerScriptRef.gridContents [(int)transform.position.x, (int)transform.position.y]);
			}

				gameManagerScriptRef.localPlayer.SendMessage("turretPlacement", new Vector2(6-transform.position.x, 6-transform.position.y));
		}
	}


	void OnMouseOver(){

		if (Input.GetMouseButtonDown (1)) {

			if(gameManagerScriptRef.selectedPlayer !=null) 
			{
				if(gameManagerScriptRef.gridContents[(int)gameObject.transform.position.x, (int)gameObject.transform.position.y]!= null){
					if(gameManagerScriptRef.gridContents[(int)gameObject.transform.position.x, (int)gameObject.transform.position.y].tag=="wall" || gameManagerScriptRef.gridContents[(int)gameObject.transform.position.x, (int)gameObject.transform.position.y].tag=="cloaked"){

					}
					else{
						gameManagerScriptRef.selectedPlayer.transform.position = gameObject.transform.position;
						gameManagerScriptRef.moveFlag=true;
						gameManagerScriptRef.localPlayer.SendMessage("moveEnemy", new Vector2(6-gameManagerScriptRef.selectedPlayer.transform.position.x,6- gameManagerScriptRef.selectedPlayer.transform.position.y));
						                                      
						switch((int)gameManagerScriptRef.selectedPlayer.transform.eulerAngles.z){
							
						case 180:
							gameManagerScriptRef.selectedPlayer.SendMessage("updateFOVUp");
							break;
						case 90:
							gameManagerScriptRef.selectedPlayer.SendMessage("updateFOVRight");
							break;
						case 270:
							gameManagerScriptRef.selectedPlayer.SendMessage("updateFOVLeft");
							break;
						case 0:
							gameManagerScriptRef.selectedPlayer.SendMessage("updateFOVDown");
							break;
							
						}
					}
				}
				else{
					gameManagerScriptRef.selectedPlayer.transform.position = gameObject.transform.position;
					gameManagerScriptRef.moveFlag = true;
					gameManagerScriptRef.localPlayer.SendMessage("moveEnemy", new Vector2(6-gameManagerScriptRef.selectedPlayer.transform.position.x, 6-gameManagerScriptRef.selectedPlayer.transform.position.y));
					switch((int)gameManagerScriptRef.selectedPlayer.transform.eulerAngles.z){
						
					case 180:
						gameManagerScriptRef.selectedPlayer.SendMessage("updateFOVUp");
						break;
					case 90:
						gameManagerScriptRef.selectedPlayer.SendMessage("updateFOVRight");
						break;
					case 270:
						gameManagerScriptRef.selectedPlayer.SendMessage("updateFOVLeft");
						break;
					case 0:
						gameManagerScriptRef.selectedPlayer.SendMessage("updateFOVDown");
						break;
						
					}
				}
			}
		}
	}
}
