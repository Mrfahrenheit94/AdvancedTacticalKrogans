using UnityEngine;
using System.Collections;

public class tileGenerator : MonoBehaviour {
	public GameObject tilePrefab;
	// Use this for initialization
	void Start () {
		for(int i=0;i<7;i++){
			for(int j=0;j<7;j++){
				Instantiate (tilePrefab, new Vector3(i, j, 0), Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
