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
}
