using UnityEngine;
using System.Collections;

public class wallScript : MonoBehaviour {
	public gameManagerScript gameManagerScript;
	// Use this for initialization
	void Start () {

		gameManagerScript = GameObject.Find ("gameManager").GetComponent<gameManagerScript> ();
		gameManagerScript.gridContents [(int)gameObject.transform.position.x,(int)gameObject.transform.position.y] = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if (gameManagerScript.wallPlacementMode) {
			Destroy(gameObject);
			gameManagerScript.localPlayer.SendMessage("wallPlacement", new Vector2(6-transform.position.x, 6-transform.position.y));
		}
		else if (gameManagerScript.pingLocationMode) {
			Instantiate(gameManagerScript.pingPrefab, new Vector3(transform.position.x, transform.position.y, -2), Quaternion.identity);
			gameManagerScript.localPlayer.SendMessage("pingLocation", new Vector2(6-transform.position.x, 6-transform.position.y));
		}


	}
}
