using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class brush : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//brushstroke
		/*List<MovePoint> pts = Global.Video.Points;
		if (pts.Count < 2)
			return;

		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetVertexCount (Global.Video.Points.Count);	

		for (int i=0; i<pts.Count-1; i++) {
			Vector2 df0 = pts [i + 1].f - pts [i].f;
			Vector2 df1 = pts [i + 2].f - pts [i + 1].f;
			while (df1.magnitude < 0.3f) {
				float theta_scale = 0.1f;             //Set lower to add more points
				int size = (int)((2.0 * Mathf.PI) / theta_scale); //Total number of points in circle.
				
				//lineRenderer.SetPosition(i, new Vector3(pts[i].p.x, pts[i].p.y, 0.0f));

				for(float theta = 0.0f; theta < 2.0f * Mathf.PI; theta += 0.1f) {
					float x = pts[i].p.x*Mathf.Cos(theta);
					float y = pts[i].p.y*Mathf.Sin(theta);
					
					Vector3 pos = new Vector3(x, y, 0.0f);
					lineRenderer.SetPosition(i, pos);
				}
			}	
		}*/
		List<MovePoint> pts = Global.Video.Points;
		if (pts.Count < 2)
			return;
		
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetVertexCount (Global.Video.Points.Count);
		int i = 0;
		while (i < pts.Count) {
			float t = 0.05f;
			Vector2 df0 = pts [i + 1].f - pts [i].f;
			//Vector2 df1 = pts [i + 2].f - pts [i + 1].f;
			lineRenderer.SetPosition(i, new Vector3(pts[i].p.x, pts[i].p.y, -0.1f));
			//while (df0.magnitude<0.5){
				lineRenderer.SetWidth(t, 0.0f);
			//}
			//while (df1.magnitude < 0.3f) {
			lineRenderer.SetColors(new Color(1.0f, 0.0f, 0.0f, 0.1f), new Color(0.0f, 0.0f, 0.0f, 0.1f));	
			//}
			t++;
			i++;
		}
	}
}
