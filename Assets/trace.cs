using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class trace : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		List<MovePoint> pts = Global.Video.Points;
		if (pts.Count < 2)
			return;

		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetVertexCount (Global.Video.Points.Count);
		int i = 0;
		while (i < pts.Count) {
			lineRenderer.SetPosition(i, new Vector3(pts[i].p.x, pts[i].p.y, -0.1f));
			i++;
		}
	}
}
