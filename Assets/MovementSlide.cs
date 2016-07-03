using UnityEngine;
using System.Collections;

public class MovementSlide : MonoBehaviour {

	// Use this for initialization
	void Start () {
		velocityx = Random.value * 10.0f - 5.0f;
	}

	public float t0=-10.0f;
	public float t1=10.0f;
	public float velocityx = 10.0f; //units per second

	public float acc=5.0f;
	public float u=0.0f;

	//public float rotation=0.0f;
	public float rotationVelocity=0.0f;
	// Update is called once per frame
	void Update () {

		//transform.position = new Vector3(t0+Random.value*(t1-t0), 0.0f, 0.0f);
		//transform.position = new Vector3(t0+Random.value*(t1-t0), 0.0f, 0.0f);

		/*transform.Translate (velocityx*Time.deltaTime, 0.0f, 0.0f);
		if (transform.position.x > t1) {
			transform.position = new Vector3 (t0, transform.position.y, transform.position.z);
		}*/

		/*transform.Translate (velocityx*Time.deltaTime*(Random.value-0.2f), 0.0f, 0.0f);
		if (transform.position.x > t1) {
			transform.position = new Vector3 (t0, transform.position.y, transform.position.z);
		}*/



		/*transform.Translate (velocityx*Time.deltaTime, 0.0f, 0.0f);
		if (transform.position.x > t1 && velocityx>0.0) {
			velocityx=-velocityx;
		}

		if (transform.position.x < t0 && velocityx<0.0) {
			velocityx=-velocityx;
		}*/

		/*u += acc * Time.deltaTime;
		transform.Translate (u*Time.deltaTime, 0.0f, 0.0f);

		if (transform.position.x > t1) {
			u=0.0f;
			transform.position = new Vector3 (t0, transform.position.y, transform.position.z);
		}*/

		transform.position=new Vector3 (transform.position.x+velocityx*Time.deltaTime, transform.position.y, transform.position.z);
		if (transform.position.x > t1 && velocityx>0.0) {
			velocityx=-velocityx;
		}
		
		if (transform.position.x < t0 && velocityx<0.0) {
			velocityx=-velocityx;
		}

		transform.localEulerAngles = new Vector3 (
			transform.localEulerAngles.x, 
			transform.localEulerAngles.y, 
			transform.localEulerAngles.z+rotationVelocity * Time.deltaTime);
	}
}
