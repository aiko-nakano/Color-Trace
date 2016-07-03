using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMove : MonoBehaviour {

	public Material lineMaterial;
	public Material spriteMaterial;

	public float flowScale=0.5f;
	public float quadScale=0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnPostRender() {

		//............................................start GL rendering
		GL.PushMatrix ();
		lineMaterial.SetPass (0);


		//................................................extract  video properties
		Vector2 mc = Global.Video.MovementCentroid;
		Vector2 fl = Global.Video.AvgFlow;
		Color mcol = Global.Video.MovementColor;
		Color acol = Global.Video.AvgColor;
		List<MovePoint> pts = Global.Video.Points;

		GL.Begin (GL.QUADS);

		//line to x=0; coloring with pixel
		for (int i=0; i<	-1; i++) {
			GL.Color (new Color (pts[i].mcol.r, pts[i].mcol.g, pts[i].mcol.b, 0.2f)); //last one is channel alpha is last item, fractional number (0-1, alpha 1 is opaque 0 is 255)
			GL.TexCoord(new Vector3(0.0f, 0.0f, 0.0f));
			GL.Vertex3 (pts[i].p.x, pts[i].p.y, 0.0f);
			//GL.Color (new Color (0.0f, 0.0f, 0.0f, 0.1f));
			GL.TexCoord(new Vector3(1.0f, 0.0f, 0.0f));
			GL.Vertex3 (pts[i].p.x, 0.0f, 0.0f);
			GL.TexCoord(new Vector3(1.0f, 1.0f, 0.0f));
			GL.Vertex3 (pts[i+1].p.x, 0.0f, 0.0f);
			GL.TexCoord(new Vector3(0.0f, 1.0f, 0.0f));
			GL.Vertex3 (pts[i+1].p.x, pts[i+1].p.y, 0.0f);
			//GL.Vertex3 (mc.x + fl.x * 5.0f, mc.y + fl.y * 5.0f, 0.0f);
		}
		GL.End();



			
					//GL.Vertex3(i/(-1f),df0.sqrMagnitude, 0 );
					//GL.Vertex3((i+1)/(-1f),df1.sqrMagnitude*8000f, 0 );


		/*GL.Color (new Color (pts[i].mcol.r, pts[i].mcol.g, pts[i].mcol.b, 0.1f)); //last one is channel alpha is last item, fractional number (0-1, alpha 1 is opaque 0 is 255)
			GL.Vertex3 (pts[i].p.x, pts[i].p.y, 0.0f);
			GL.Color (new Color (0.0f, 0.0f, 0.0f, 0.1f));
			GL.Vertex3 (pts[i+1].p.x, pts[i+1].p.y, 0.0f);*/
		//GL.Vertex3 (mc.x + fl.x * 5.0f, mc.y + fl.y * 5.0f, 0.0f);

		//.....NETS 
		/*GL.Begin (GL.LINES);

		for (int j = 0; j<Global.Video.Height; j+=10) {
			for (int i = 0; i<Global.Video.Width; i+=10) {//skip every 3
				Vector2 p = Global.Video.Pixels [i, j].p; //position
				//Vector2 f = Global.Video.Pixels [i, j].flow; //flow; top ones are ag
				Vector2 d = p - mc;
				float dist = Vector2.Distance(p,mc)/;
				GL.Color (new Color (1.0f, 0.0f,0.0f, 1.0f)); //last one is channel alpha is last item, fractional number (0-1, alpha 1 is opaque 0 is 255)
				GL.Vertex3 (p.x, p.y, 0.0f);
				GL.Color (new Color (1.0f * dist, 0.0f,0.0f, 0.5f));
				GL.Vertex3 (mc.x + fl.x * 5.0f, mc.y + fl.y * 5.0f, 0.0f);
				//GL.Vertex3 (mc.x + fl.x * 5.0f, mc.y + fl.y * 5.0f, 0.0f);
			}
		}
		GL.End ();*/

		//spriteMaterial.SetPass (0);
		/*for (int j=0; j<Global.Video.Height; j+=2) {
			for (int i=0; i<Global.Video.Width; i+=2) {
				VideoPixel px = Global.Video.Pixels [i, j];
				DrawQuad (px.p, px.flow.magnitude * quadScale);
			}
		}*/
		GL.End ();

		//.......................................Draw a think line along the direction of movement
		/*GL.Begin( GL.LINES );
		GL.Color( new Color(0f,0f,0f,1.0f) );
		GL.Vertex3(mc.x, mc.y, 0 );
		GL.Color( new Color(0f,0f,0f,0.1f) );
		GL.Vertex3( mc.x+fl.x*100.0f, mc.y+fl.y*100.0f, 0 );
		GL.End();*/


		//.........................................Draw optical flow vectors
		/*GL.Begin( GL.LINES );
		for (int j=0; j<Global.Video.Height; j+=2) {
			for(int i=0; i<Global.Video.Width; i+=2) {
				VideoPixel px=Global.Video.Pixels[i,j];

				GL.Color( new Color(0f,0f,0f,0.5f) );
				GL.Vertex3(px.p.x, px.p.y, 0 );
				GL.Color( new Color(0f,0f,0f,0.1f) );
				GL.Vertex3( px.p.x+px.flow.x*flowScale, px.p.y+px.flow.y*flowScale, 0 );
			}
		}
		GL.End();*/

		//.........................................Draw optical flow vectors
		/*GL.Begin( GL.LINES );
		for (int j=0; j<Global.Video.Height; j+=2) {
			for(int i=0; i<Global.Video.Width; i+=2) {
				VideoPixel px=Global.Video.Pixels[i,j];

				GL.Color( new Color(0f,0f,0f,0.5f) );
				GL.Vertex3(px.p.x, px.p.y, 0 );
				GL.Color( new Color(0f,0f,0f,0.1f) );
				GL.Vertex3( px.p.x+px.flow.x*flowScale, px.p.y+px.flow.y*flowScale, 0 );
			}
		}
		GL.End();*/


		//...........................................Draw a thick line along direction of movement
		//DrawLine (mc, mc + fl*20f, mcol, new Color (mcol.r, mcol.g, mcol.b, 0.1f), 0.08f, 0.02f);

		//..........................................Draw a wedge at each trace point
		/*Color c0 = new Color (0f, 0f, 0f, 0.5f);
		Color c1 = new Color (1f, 1f, 1f, 0.1f);
	
		for(int i=0; i<; i+=3) {
			DrawLine (pts[i].p, pts[i].p + pts[i].f*10f, c0, c1, 0.02f, 0.02f);
		}*/


		//..........................................Draw Graphs

		//movement
		/*GL.Color (Color.black);
		GL.Begin( GL.LINES );
		for (int i=0; i<-1; i++) {
			GL.Vertex3(i/(-1f),pts[i].f.magnitude*2f, 0 );
			GL.Vertex3((i+1)/(-1f),pts[i+1].f.magnitude*2f, 0 );
		}
		GL.End();

		//acceleration
		GL.Color (Color.gray);
		GL.Begin( GL.LINES );
		for (int i=0; i<-2; i++) {
			Vector2 df0=pts[i+1].f-pts[i].f;
			Vector2 df1=pts[i+2].f-pts[i+1].f;

			GL.Vertex3(i/(-1f),df0.sqrMagnitude*8000f, 0 );
			GL.Vertex3((i+1)/(-1f),df1.sqrMagnitude*8000f, 0 );
		}
		GL.End();

		//average color
		GL.Color (Color.blue);
		GL.Begin( GL.LINES );
		for (int i=0; i<-1; i++) {
			GL.Vertex3(i/(-1f),pts[i].acol.b*0.2f, 0 );
			GL.Vertex3((i+1)/(-1f),pts[i+1].acol.b*0.2f, 0 );
		}
		GL.End();

		GL.Color (Color.red);
		GL.Begin( GL.LINES );
		for (int i=0; i<-1; i++) {
			GL.Vertex3(i/(-1f),pts[i].acol.r*0.2f, 0 );
			GL.Vertex3((i+1)/(-1f),pts[i+1].acol.r*0.2f, 0 );
		}
		GL.End();

		GL.Color (Color.green);
		GL.Begin( GL.LINES );
		for (int i=0; i<-1; i++) {
			GL.Vertex3(i/(-1f),pts[i].acol.g*0.2f, 0 );
			GL.Vertex3((i+1)/(-1f),pts[i+1].acol.g*0.2f, 0 );
		}
		GL.End();*/

		//.............................draw using sprite material
		/*spriteMaterial.SetPass (0);

		//.............................draw a quad at each video pixel location with size proprtional to movement
		GL.Color (new Color (1f,1f,1f,0.2f));
		for (int j=0; j<Global.Video.Height; j+=2) {
			for(int i=0; i<Global.Video.Width; i+=2) {
				VideoPixel px=Global.Video.Pixels[i,j];
				DrawQuad(px.p, px.flow.magnitude*quadScale);
			}
		}*/

		//....................................end GL rendering

		GL.PopMatrix();
	}



	//............................................Helper functions

	void DrawLine(Vector2 p0, Vector2 p1, Color c0, Color c1, float t0, float t1) {
		Vector2 dv = p1 - p0;
		dv.Normalize ();

		GL.Begin (GL.QUADS);
		GL.Color (c0);
		GL.TexCoord(new Vector3(0.0f, 0.0f, 0.0f));
		GL.Vertex3 (p0.x + dv.y * t0, p0.y - dv.x * t0, 0.0f);

		GL.TexCoord(new Vector3(1.0f, 0.0f, 0.0f));
		GL.Vertex3 (p0.x - dv.y * t0, p0.y + dv.x * t0, 0.0f);

		GL.Color (c1);
		GL.TexCoord(new Vector3(1.0f, 1.0f, 0.0f));
		GL.Vertex3 (p1.x - dv.y * t1, p1.y + dv.x * t1, 0.0f);

		GL.TexCoord(new Vector3(0.0f, 1.0f, 0.0f));
		GL.Vertex3 (p1.x + dv.y * t1, p1.y - dv.x * t1, 0.0f);

		GL.End ();
	}

	void DrawQuad(Vector2 p0, float size) {

		GL.Begin (GL.QUADS);

		float hs = size * 0.5f;

		GL.TexCoord(new Vector3(0.0f, 0.0f, 0.0f));
		GL.Vertex3 (p0.x -hs, p0.y - hs, 0.0f);
		
		GL.TexCoord(new Vector3(1.0f, 0.0f, 0.0f));
		GL.Vertex3 (p0.x +hs, p0.y - hs, 0.0f);
		
		GL.TexCoord(new Vector3(1.0f, 1.0f, 0.0f));
		GL.Vertex3 (p0.x +hs, p0.y + hs, 0.0f);
		
		GL.TexCoord(new Vector3(0.0f, 1.0f, 0.0f));
		GL.Vertex3 (p0.x -hs, p0.y + hs, 0.0f);
		
		GL.End ();
	}


}
