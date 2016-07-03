using UnityEngine;
using System.Collections;

public class DualDot : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	 	c0=transform.GetChild (0);
		c1=transform.GetChild (1);

	}

	Transform c0;
	Transform c1;

	public float blurSize=0.5f;
	public float blurSpeed=3.0f;
	// Update is called once per frame
	void Update () {
		c0.localPosition = new Vector3(Mathf.Cos (Time.time*blurSpeed)*blurSize, 0.0f, 0.0f);
		c1.localPosition = new Vector3(-Mathf.Cos (Time.time*blurSpeed)*blurSize, 0.0f, 0.0f);
	}
}
