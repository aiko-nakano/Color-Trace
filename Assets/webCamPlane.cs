using UnityEngine;
using System.Collections;

public class webCamPlane : MonoBehaviour {


	// Use this for initialization
	void Start () {
		//...................................Edit
		renderer.material.mainTexture = Global.VideoTexture;
		//renderer.material.mainTexture = Global.Video.resultTexture;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
