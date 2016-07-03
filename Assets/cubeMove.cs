using UnityEngine;
using System.Collections;

public class cubeMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {



	}

	void FixedUpdate() {
		//...................................Edit
		Vector2 mc = Global.Video.MovementCentroid;
		Vector2 fl = Global.Video.AvgFlow;

		transform.position = new Vector3 (mc.x, mc.y, 0.0f);
		transform.LookAt (new Vector3 (mc.x+fl.x*10.0f, mc.y+fl.y*10.0f, 0.0f));
	}
}
