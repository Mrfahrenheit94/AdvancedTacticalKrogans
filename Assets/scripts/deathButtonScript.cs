using UnityEngine;
using System.Collections;

public class deathButtonScript : MonoBehaviour {
	public gameManagerScript gameManagerScript;
	// Use this for initialization
	void Start () {
		
		gameManagerScript = GameObject.Find ("gameManager").GetComponent<gameManagerScript> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown(){
		if (gameManagerScript.selectedPlayer != null){
			gameManagerScript.localPlayer.SendMessage("deadPlayer", new Vector2(6-gameManagerScript.selectedPlayer.transform.position.x, 6-gameManagerScript.selectedPlayer.transform.position.y));

			Destroy(gameManagerScript.selectedPlayer);
		} 
	}
}
