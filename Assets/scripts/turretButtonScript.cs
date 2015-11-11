using UnityEngine;
using System.Collections;

public class turretButtonScript : MonoBehaviour {

	public gameManagerScript gameManagerScript;
	// Use this for initialization
	void Start () {
		
		gameManagerScript = GameObject.Find ("gameManager").GetComponent<gameManagerScript> ();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if (gameManagerScript.turretPlacementMode == false) {
			gameManagerScript.turretPlacementMode = true;
			GetComponent<SpriteRenderer> ().color = new Color (0.1f, 0.1f, 1f, 0.3f);
		} 
		else {
			gameManagerScript.turretPlacementMode = false;
			GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		}
	}
}
