using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {

	public gameManagerScript gameManagerScriptRef;
	// Use this for initialization
	void Start () {
		
		gameManagerScriptRef = GameObject.Find ("gameManager").GetComponent<gameManagerScript> ();
		gameManagerScriptRef.gridContents [(int)gameObject.transform.position.x,(int)gameObject.transform.position.y] = gameObject;

		gameManagerScriptRef.enemies[4-(int)transform.position.x]=gameObject;
			
			

	}

	// Update is called once per frame
	void Update () {
	
	}
/*
	void OnMouseDown(){
		if (gameManagerScriptRef.attackTilePlacementMode) {
			if (gameManagerScriptRef.gridContents [(int)transform.position.x, (int)transform.position.y] == null) {
				//gameManagerScriptRef.gridContents [(int)transform.position.x, (int)transform.position.y] = (GameObject)
				Instantiate (gameManagerScriptRef.attackTilePrefab, new Vector3 (transform.position.x, transform.position.y, 0), Quaternion.identity);
				//gameManagerScriptRef.gridContents [(int)transform.position.x, (int)transform.position.y].tag = "decoy";
				gameManagerScriptRef.localPlayer.SendMessage ("attackTilePlacement", new Vector2 (6 - transform.position.x, 6 - transform.position.y));
			} 
		}
	}
	*/
}
