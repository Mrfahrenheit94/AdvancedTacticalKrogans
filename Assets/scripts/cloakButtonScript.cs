using UnityEngine;
using System.Collections;

public class cloakButtonScript : MonoBehaviour {
	gameManagerScript gameManagerScript;
	// Use this for initialization
	void Start () {
		gameManagerScript = GameObject.Find ("gameManager").GetComponent<gameManagerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		gameManagerScript.moveFlag = true;
		gameManagerScript.localPlayer.SendMessage ("setCloak");
	}
}
