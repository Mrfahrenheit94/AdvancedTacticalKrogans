using UnityEngine;
using System.Collections;

public class rotateScript : MonoBehaviour {
	gameManagerScript gameManagerScriptRef;
	// Use this for initialization
	void Start () {
		gameManagerScriptRef = GameObject.Find ("gameManager").GetComponent<gameManagerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		Debug.Log("poop");
		if (gameManagerScriptRef.selectedPlayer != null) {
			if(gameObject.name == "rotateCW"){
				gameManagerScriptRef.selectedPlayer.transform.Rotate(0,0,-90);
				gameManagerScriptRef.localPlayer.SendMessage("rotateEnemy", -90);
				gameManagerScriptRef.moveFlag = true;
			}
			else{
				gameManagerScriptRef.selectedPlayer.transform.Rotate(0,0,90);
				gameManagerScriptRef.localPlayer.SendMessage("rotateEnemy", 90);
				gameManagerScriptRef.moveFlag = true;

			}
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
