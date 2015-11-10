using UnityEngine;
using System.Collections;

public class readyButtonScript : MonoBehaviour {
	public GameObject gameManagerRef;
	// Use this for initialization
	void Start () {
		gameManagerRef = GameObject.Find ("gameManager");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playerReadyButtonFunction(){

		gameManagerRef.SendMessage ("newTurnFunction");
		gameObject.SetActive (false);
	}
}
