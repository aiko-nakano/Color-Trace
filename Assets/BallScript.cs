using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

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
		
		//print (mc.x);
		mc.x = mc.x * 10f - 5f;
		mc.y = mc.y * 10f - 5f;

		//rigidbody.AddForce (new Vector3 (fl.x*800.0f, fl.y*800.0f, 0.0f));
		//rigidbody.AddForceAtPosition(new Vector3 (fl.x*800.0f, fl.y*800.0f, 0.0f), new Vector3(mc.x, mc.y, 0.0f));
	}
}
