using UnityEngine;
using System.Collections;

public class friendlyTurretScript : MonoBehaviour {
	public int[] fovX = new int [100];
	public int[] fovY = new int [100];
	public gameManagerScript gameManagerScript;
	// Use this for initialization
	void Start () {
		
		gameManagerScript = GameObject.Find ("gameManager").GetComponent<gameManagerScript> ();
		for (int i=0; i<3; i++) {
			for(int j=0;j<3;j++){
				fovX[i+(j*3)] = (int) transform.position.x+j-1;
				fovY[i+(j*3)] = (int) transform.position.y+i-1;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
