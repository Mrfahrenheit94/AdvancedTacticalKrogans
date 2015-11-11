using UnityEngine;
using System.Collections;

public class pingScript : MonoBehaviour {
	public float maxSize;
	public float rate=0.002f;
	public float timer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.localScale += new Vector3 (rate, rate, 0);
		timer += Time.deltaTime;
		if (timer >= 1.0f) {
			Destroy(gameObject);
		}
	}


}
