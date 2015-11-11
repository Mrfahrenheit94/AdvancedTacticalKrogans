using UnityEngine;
using System.Collections;

public class friendlyTurretScript : MonoBehaviour {
	public int[] fovX = new int [100];
	public int[] fovY = new int [100];
	public GameObject[] turretEnemies= new GameObject[10];
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

	public void updateTurret(){

		for(int i=0;i<9;i++){
			if(gameManagerScript.gridContents[fovX[i], fovY[i]] != null){//if theres something in the range of the turret
				if(gameManagerScript.gridContents[fovX[i], fovY[i]].name =="enemyShotgun" || gameManagerScript.gridContents[fovX[i], fovY[i]].name =="enemyAssault" || gameManagerScript.gridContents[fovX[i], fovY[i]].name =="enemySniper"){ 
					if(gameManagerScript.gridContents[fovX[i], fovY[i]].tag != "cloaked" && turretEnemies[i]==null)
						turretEnemies[i] = (GameObject) Instantiate(gameManagerScript.enemySymbolPrefab, new Vector3(fovX[i], fovY[i], 0), Quaternion.identity);
					else if(gameObject.tag == "sensor"  && turretEnemies[i]==null)
						turretEnemies[i] = (GameObject) Instantiate(gameManagerScript.enemySymbolPrefab, new Vector3(fovX[i], fovY[i], 0), Quaternion.identity);
			}
			   
			   }
			else if(turretEnemies[i]!=null){
				Destroy(turretEnemies[i]);

			}
	}
}
}
