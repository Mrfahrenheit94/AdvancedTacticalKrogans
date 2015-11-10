using UnityEngine;
using System.Collections;

public class passButtonScript : MonoBehaviour {
	public GameObject readyButton;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void passTurnFunction(){

		readyButton.SetActive (true);
		//clear out the ghost location
	}
}
