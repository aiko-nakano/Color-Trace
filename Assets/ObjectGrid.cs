using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectGrid : MonoBehaviour {

	// Use this for initialization
	void Start () {

		float dx = x1 - x0;
		float dy = y1 - y0;
		float dz = z1 - z0;

		float ddx = 0.0f;
		float ddy = 0.0f;
		float ddz = 0.0f;

		if (CountX>1)  ddx = dx / (float)CountX;
		if (CountY>1)  ddy = dy / (float)CountY;
		if (CountZ>1)  ddz = dz / (float)CountZ;
		
		GameObject d = Proto;//GameObject.Find("DualDot");
		if (d == null)
			return;

		for (int k=0; k<CountZ; ++k) {
			for (int j=0; j<CountY; ++j) {
				for (int i=0; i<CountX; ++i) {
					GameObject cp = (GameObject)Object.Instantiate (d);
					Objects.Add (cp);
					cp.transform.parent=transform;

					cp.transform.localPosition = new Vector3 (
						x0+i*ddx+Random.value*Randomness*ddx, 
						y0+j*ddy+Random.value*Randomness*ddy,
						z0+k*ddz+Random.value*Randomness*ddz);

				}
			}
		}
	}
	
	public GameObject Proto;
	
	public int CountX=20;
	public int CountY=10;
	public int CountZ=1;
	
	public float x0 = -20.0f;
	public float y0 = -20.0f;
	public float z0 = 20.0f;
	
	public float x1 = 20.0f;
	public float y1 = 20.0f;
	public float z1 = 25.0f;
	
	public float Randomness=0.0f;
	
	List<GameObject> Objects=new List<GameObject>();
	
	// Update is called once per frame
	void Update () {
	}
}
