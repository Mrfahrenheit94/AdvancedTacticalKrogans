using UnityEngine;
using System.Collections;

public class squadMemberScript : MonoBehaviour {
	public gameManagerScript gameManagerScript;
	public int[] fovX = new int [100];
	public int[] fovY = new int [100];
	public GameObject fovPrefab;
	public GameObject[]visibleTiles = new GameObject[25];
	//public float boxOffset=0.22f;
	// Use this for initialization
	void Start () {
		hideAllWalls ();
		//fov [0] = new Vector2 (2, 2);
		gameManagerScript = GameObject.Find ("gameManager").GetComponent<gameManagerScript> ();
		gameManagerScript.gridContents [(int)gameObject.transform.position.x, (int)gameObject.transform.position.y] = gameObject;

		gameManagerScript.squadMembers[(int)transform.position.x-2]=gameObject;



		for(int j=0;j<2;j++){
			for(int i=0;i<7;i++){
				fovX[i+(j*7)] = (int) gameObject.transform.position.x-3+i;
				fovY[i+(j*7)] = (int) gameObject.transform.position.y+j; 
			}
		}
		for (int i=0; i<5; i++) {
			fovX [i +14] = (int)gameObject.transform.position.x - 2 + i;
			fovY [i + 14] = (int)gameObject.transform.position.y+2; 
		}
		for (int i=0; i<3; i++) {
			fovX [i +19] = (int)gameObject.transform.position.x - 1 + i;
			fovY [i + 19] = (int)gameObject.transform.position.y+3; 
		}




		for (int i=0; i<22; i++){
			visibleTiles[i] = (GameObject) Instantiate (fovPrefab, new Vector3(fovX [i], fovY [i], 0),  Quaternion.identity);

			//visibleTiles[i] = visibleTile;
			visibleTiles[i].transform.SetParent(gameObject.transform);

		}
	//	checkOOB();
		hideAllVisibilityTiles ();
		//updateFOVUp ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if (gameManagerScript.selectedPlayer != null && gameManagerScript.selectedPlayer != gameObject) {
			gameManagerScript.selectedPlayer.SendMessage ("hideAllVisibilityTiles");
			gameManagerScript.selectedPlayer = gameObject;
			for(int i=0;i<5;i++){
				if(gameManagerScript.squadMembers[i] == gameObject){
					gameManagerScript.selectedPlayerIndex = i;
					break;
				}
			}
			checkOOB ();
		} 
		else if (gameManagerScript.selectedPlayer == gameObject) {
			hideAllVisibilityTiles();
			hideAllWalls();
			gameManagerScript.selectedPlayer=null;
		}
		else {
			gameManagerScript.selectedPlayer = gameObject;
			for(int i=0;i<5;i++){
				if(gameManagerScript.squadMembers[i] == gameObject){
					gameManagerScript.selectedPlayerIndex = i;
					break;
				}
			}
			checkOOB ();

		}
	}

	void updateFOVUp(){
		for(int j=0;j<2;j++){
			for(int i=0;i<7;i++){
				fovX[i+(j*7)] = (int) gameObject.transform.position.x-3+i;
				fovY[i+(j*7)] = (int) gameObject.transform.position.y+j; 
			}
		}
		for (int i=0; i<5; i++) {
			fovX [i +14] = (int)gameObject.transform.position.x - 2 + i;
			fovY [i + 14] = (int)gameObject.transform.position.y+2; 
		}
		for (int i=0; i<3; i++) {
			fovX [i +19] = (int)gameObject.transform.position.x - 1 + i;
			fovY [i + 19] = (int)gameObject.transform.position.y+3; 
		}
	
			checkOOB();
		


	}

	void updateFOVDown(){
		for(int j=0;j<2;j++){
			for(int i=0;i<7;i++){
				fovX[i+(j*7)] = (int) gameObject.transform.position.x-3+i;
				fovY[i+(j*7)] = (int) gameObject.transform.position.y-j; 
			}
		}
		for (int i=0; i<5; i++) {
			fovX [i +14] = (int)gameObject.transform.position.x - 2 + i;
			fovY [i + 14] = (int)gameObject.transform.position.y-2; 
		}
		for (int i=0; i<3; i++) {
			fovX [i +19] = (int)gameObject.transform.position.x - 1 + i;
			fovY [i + 19] = (int)gameObject.transform.position.y-3; 
		}
	
		checkOOB();
		
		

	}

	void updateFOVRight(){
		for(int j=0;j<2;j++){
			for(int i=0;i<7;i++){
				fovY[i+(j*7)] = (int) gameObject.transform.position.y-3+i;
				fovX[i+(j*7)] = (int) gameObject.transform.position.x+j; 
			}
		}
		for (int i=0; i<5; i++) {
			fovY[i +14] = (int)gameObject.transform.position.y - 2+ i;
			fovX [i + 14] = (int)gameObject.transform.position.x+2; 
		}
		for (int i=0; i<3; i++) {
			fovY [i +19] = (int)gameObject.transform.position.y - 1 + i;
			fovX [i + 19] = (int)gameObject.transform.position.x+3; 
		}


			checkOOB();
		

	}

	
	void updateFOVLeft(){
		for(int j=0;j<2;j++){
			for(int i=0;i<7;i++){
				fovY[i+(j*7)] = (int) gameObject.transform.position.y-3+i;
				fovX[i+(j*7)] = (int) gameObject.transform.position.x-j; 
			}
		}
		for (int i=0; i<5; i++) {
			fovY[i +14] = (int)gameObject.transform.position.y - 2+ i;
			fovX [i + 14] = (int)gameObject.transform.position.x-2; 
		}
		for (int i=0; i<3; i++) {
			fovY [i +19] = (int)gameObject.transform.position.y - 1 + i;
			fovX [i + 19] = (int)gameObject.transform.position.x-3; 
		}
		//for (int i=0; i<22; i++) {
	//		Instantiate(gameObject, new Vector3(fovX[i], fovY[i], 0), Quaternion.identity);
//		}
		checkOOB ();
	}

	public void checkOOB(){
		hideAllWalls ();
		for (int i=0; i<22; i++) {
			if (visibleTiles [i].transform.position.x > 6 || visibleTiles[i].transform.position.x < -0.1f || visibleTiles[i].transform.position.y > 6 || visibleTiles[i].transform.position.y < -0.1f) {
				visibleTiles[i].GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
			} else
				visibleTiles[i].GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		}

		for (int i=0; i<22; i++) {
			if(validTile(i)){//if it's in the range of the board size

				GameObject objectInTile = gameManagerScript.gridContents[fovX[i], fovY[i]];
				if(objectInTile != null && objectInTile.tag == "wall"){//if it's a wall, check to see if you see it
					//Debug.Log((int)visibleTiles[i].transform.position.x);
					//Debug.Log((int)visibleTiles[i].transform.position.y);
					//Debug.Log("theres a wall here");
					if(checkVisibility(i)){
						objectInTile.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
					}
					else{
						objectInTile.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
					}
				}
				else if(objectInTile !=  null && objectInTile.tag == "cloaked")
				{
					if(!checkVisibility(i)){
						if((transform.position-objectInTile.transform.position).magnitude<=1.6f){//if you are within one square of the cloaked enemy
							objectInTile.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.2f);

						}
					}

				}
			}
		}
	}

	public void hideAllVisibilityTiles(){
		for (int i=0; i<22; i++) {
			visibleTiles[i].GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
		}
	}

	public bool validTile(int i){
		if (fovX[i] > 6 || fovX[i] < -0.1f || fovY[i]> 6 || fovY[i] < -0.1f) {
			return false;
		}
			else 
				return true;
		
	}

	public bool checkVisibility(int i){
		int modA=1;
		int modB=1;
		float modC=0.5f;
		float modD=0.5f;
		float boxOffset = 0.325f;
		bool somethingInTheWay = true;
		gameManagerScript.gridContents [fovX [i], fovY [i]].layer = 0;//move the object in question out of the raycasting layer so that it doesn't trigger itself
		for (int j=0; j<16; j++) {

			if(j%2==0)
				modA=1;
			else
				modA=-1;


			if(j>=2 && j<4)
				modB =-1;
			else if(j>=6 && j<8)
				modB=-1;
			else if(j>=10 && j<12)
				modB=-1;
			else if(j>=14)
				modB=-1;
			else
				modB=1;


			if(j>3 && j<8 )
				modC = -0.5f;
			else if(j>11)
				modC=-0.5f;
			else
				modC=0.5f;


			if(j>7)
				modD =  -0.5f;
		//	modC=0;//TEST
		//	modD=0;
			//Debug.Log(modA.ToString()+" "+modB.ToString()+" "+modC.ToString()+" "+modD.ToString());
			RaycastHit2D hit = Physics2D.Raycast(new Vector2 (fovX[i]+(boxOffset*modA), fovY[i]+(boxOffset*modB)), (gameObject.transform.position+ new Vector3(boxOffset*modC, boxOffset*modD))-new Vector3(fovX[i]+(boxOffset*modA), fovY[i]+(boxOffset*modB), 0) , ( new Vector3(fovX[i]+(boxOffset*modA), fovY[i]+(boxOffset*modB), 0) - (gameObject.transform.position+ new Vector3(boxOffset*modC, boxOffset*modD))).magnitude, 1<<LayerMask.NameToLayer("wall"));

			if (hit.collider != null) {
				//Debug.DrawLine (transform.position+ new Vector3(boxOffset*modC, boxOffset*modD), hit.point, Color.cyan, 3f);
				//Debug.Log (hit.collider.name);
				//return true;//there's an object in the way, it's invisible
			} 
			else{
				somethingInTheWay = false;//it is visible
				break;
			}
		}
		//if(gameManagerScript.gridContents[fovX[i], fovY[i]].tag !="cloaked")
			gameManagerScript.gridContents [fovX [i], fovY [i]].layer = 8;//set the object in question back into the raycasting layer
		return somethingInTheWay;


	}

	public void hideAllWalls(){

		GameObject[] wallObjects = GameObject.FindGameObjectsWithTag ("wall");

		for (int i=0; i<wallObjects.Length; i++) {
			wallObjects[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
		}

		GameObject[] cloakedEnemies = GameObject.FindGameObjectsWithTag ("cloaked");

		for (int i=0; i<cloakedEnemies.Length; i++) {
			cloakedEnemies[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
		}
	}
}
