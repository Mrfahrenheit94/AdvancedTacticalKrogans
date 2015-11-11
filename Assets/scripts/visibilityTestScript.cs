using UnityEngine;
using System.Collections;

public class visibilityTestScript : MonoBehaviour {

	public gameManagerScript gameManagerScript;
	// Use this for initialization
	void Start () {
		
		gameManagerScript = GameObject.Find ("gameManager").GetComponent<gameManagerScript> ();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){

	}

}
