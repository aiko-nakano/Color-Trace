using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VideoPixel {
	public VideoPixel() {
		for (int i=0; i<4; ++i) {
			npx[i]=this;
		}
	}
	public float r;
	public float g;
	public float b;
	public float v;
	public float v0;
	public float dv;
	public Vector2 p=new Vector2();
	public Vector2 grad=new Vector2();
	public Vector2 flow=new Vector2();
	public Vector2 temp=new Vector2();

	public VideoPixel [] npx=new VideoPixel[4];

	public float r2;
	public float g2;
	public float b2;
	public float a2=1.0f;
}

public class MovePoint {
	public MovePoint(Vector2 _p, Vector2 _f, Color _mc, Color _ac) {
		p = _p;
		f = _f;
		mcol = _mc;
		acol = _ac;
	}

	public Vector2 p;
	public Vector2 f;
	public Color mcol;
	public Color acol;
}

public class VideoFrame{
	public VideoPixel [,] Pixels;
	public int Width=0;
	public int Height=0;

	public Color32[] videoData;
	public Color[] resultData;
	public Texture2D resultTexture;

	public Vector2 MovementCentroid;
	public Color MovementColor;
	public Vector2 AvgFlow;
	public Color AvgColor;

	public int MaxTraceCount = 200;

	public List<MovePoint> Points = new List<MovePoint> ();

	public void Update(WebCamTexture wt) {
		if (wt == null || !wt.isPlaying)
			return;

		if (wt.width != Width || wt.height != Height) {
			Width=wt.width;
			Height=wt.height;

			videoData=new Color32[Width*Height];
			resultData=new Color[Width*Height];
			Pixels=new VideoPixel[Width,Height];

			for(int j=0; j<Height; ++j) {
				for(int i=0; i<Width; ++i) {
					Pixels[i,j]=new VideoPixel();

					Pixels[i,j].p=new Vector2(i/(float)(Width-1), j/(float)(Height-1));
				}
			}

			for(int j=0; j<Height; ++j) {
				for(int i=0; i<Width; ++i) {
					VideoPixel px=Pixels[i,j];

					if (i>0) px.npx[0]=Pixels[i-1,j];
					if (i<Width-1) px.npx[1]=Pixels[i+1,j];

					if (j>0) px.npx[2]=Pixels[i,j-1];
					if (j<Height-1) px.npx[3]=Pixels[i,j+1];
					

				}
			}

			resultTexture=new Texture2D(Width, Height);
		}

		wt.GetPixels32 (videoData);
		//wt.GetPixels(videoData);

		float ar = 0f;
		float ag = 0f;
		float ab = 0f;

		int k = 0;
		float i255 = 1.0f / 255.0f;
		for(int j=0; j<Height; ++j) {
			for(int i=0; i<Width; ++i) {
				VideoPixel px=Pixels[i,j];
				px.r=videoData[k].r*i255;
				px.g=videoData[k].g*i255;
				px.b=videoData[k].b*i255;

				ar+=px.r;
				ag+=px.g;
				ab+=px.b;

				px.v0=px.v;
				px.v=(px.r+px.g+px.b)/3.0f;

				px.dv=Mathf.Abs(px.v-px.v0);

				/*px.r2=px.r;
				px.g2=px.g;
				px.b2=px.b;
				px.a2=1.0f;*/

				k++;
			}
		}

		ar/=(float)(Width*Height);
		ag/=(float)(Width*Height);
		ab/=(float)(Width*Height);

		AvgColor = new Color (ar, ag, ab, 1f);

		ComputeGradient ();
		ComputeOpticalFlow ();
		//if (wt.didUpdateThisFrame) {

		//}

		Points.Add (new MovePoint (MovementCentroid, AvgFlow, MovementColor, AvgColor));
		if (Points.Count > MaxTraceCount)
			Points.RemoveAt (0);
	}

	public void ComputeGradient(){
		for(int j=1; j<Height-1; ++j) {
			for(int i=1; i<Width-1; ++i) {
				VideoPixel px=Pixels[i,j];
				px.grad.x=Pixels[i+1, j].v-Pixels[i-1, j].v;
				px.grad.y=Pixels[i, j+1].v-Pixels[i, j+1].v;
			}
		}
	}

	public void ComputeOpticalFlow() {
		int rdc = 2;
		float div = 4.0f;
		float threshold = 0.06f;

		int ni=0;
		int j ,i ,sj, si, pos;
		
		
		int flw=Width-2*rdc;
		int flh=Height-2*rdc;
		
		float tflx=0.0f; 
		float tfly=0.0f;
		float dum=0.0f;
		//float max_vec=255.0f*Mathf.Sqrt(2.0f)*rdc;
		
		ni=0;
		
		float idiv=1.0f/div;
		
		for (j=rdc; j<Height-rdc; j++) {
			for (i=rdc; i<Width-rdc; i++) {
				VideoPixel px=Pixels[i,j];

				tflx=0.0f;
				tfly=0.0f;
				dum=0.0f;						
				if (px.dv>threshold) {
					for (sj=-rdc; sj<=rdc; sj++) {
						for (si=-rdc; si<=rdc; si++) {
							VideoPixel px2=Pixels[i+si,j+sj];

							dum=Mathf.Abs(px.v0-px2.v)*idiv;
							tflx-=si*dum;
							tfly-=sj*dum;
						}					
					}
				}

				px.flow.x=px.flow.x*0.8f+tflx*0.2f;
				px.flow.y=px.flow.y*0.8f+tfly*0.2f;
			}
		}

			
		for(j=0; j<Height; ++j) {
			for(i=0; i<Width; ++i) {
				VideoPixel px=Pixels[i,j];

				px.temp.x=(px.flow.x+px.npx[0].flow.x+px.npx[1].flow.x+px.npx[2].flow.x+px.npx[3].flow.x)*0.2f;
				px.temp.y=(px.flow.y+px.npx[0].flow.y+px.npx[1].flow.y+px.npx[2].flow.y+px.npx[3].flow.y)*0.2f;
			}
		}

		Vector2 mc = new Vector2 ();
		AvgFlow.Set (0f, 0f);
		float totalm = 0.0f;

		float mr = 0f;
		float mg = 0f;
		float mb = 0f;
		
		for(j=0; j<Height; ++j) {
			for(i=0; i<Width; ++i) {
				VideoPixel px=Pixels[i,j];
								
				px.flow.x=px.temp.x;
				px.flow.y=px.temp.y;

				float mv=px.flow.SqrMagnitude();

				mc.x+=px.p.x*mv;
				mc.y+=px.p.y*mv;

				mr+=px.r*mv;
				mg+=px.g*mv;
				mb+=px.b*mv;

				totalm+=mv;

				AvgFlow.x+=px.flow.x;
				AvgFlow.y+=px.flow.y;
			}
		}

		if (totalm != 0f) {
			mc.x/=totalm;
			mc.y/=totalm;

			mr/=totalm;
			mg/=totalm;
			mb/=totalm;

			MovementCentroid.x=MovementCentroid.x*0.8f+mc.x*0.2f;
			MovementCentroid.y=MovementCentroid.y*0.8f+mc.y*0.2f;
		}
		AvgFlow.x/=(float)(Width*Height);
		AvgFlow.y/=(float)(Width*Height);

		MovementColor = new Color (mr, mg, mb, 1f);

	}

	public void UpdateOutputTexture() {

		int k = 0;
		float i255 = 1.0f / 255.0f;
		for(int j=0; j<Height; ++j) {
			for(int i=0; i<Width; ++i) {
				VideoPixel px=Pixels[i,j];
				resultData[k].r=px.r2;//px.r2;
				resultData[k].g=px.g2;
				resultData[k].b=px.b2;
				resultData[k].a=px.a2;
				//resultTexture.SetPixel(
				
				k++;
			}
		}

		resultTexture.SetPixels (resultData);
		resultTexture.Apply (true);
	}
}

public class Global : MonoBehaviour {

	protected static  WebCamTexture videoTexture;
	public Color32[] videoData;

	public static VideoFrame Video=new VideoFrame();

	public static WebCamTexture VideoTexture {
		get{
			StartCamera();
			return videoTexture;
		}
	}

	static void StartCamera() {
		if (videoTexture == null) {
			videoTexture = new WebCamTexture();
			
			WebCamDevice []d= WebCamTexture.devices;
			print (d.Length);
			
			videoTexture.requestedWidth=80;
			videoTexture.requestedHeight=60;
			videoTexture.Play();
			
			Video.Update(videoTexture);
		}
	}
	// Use this for initialization
	void Start () {
		print ("starting");
		StartCamera ();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("escape")) Application.Quit();
		//Video.Update(VideoTexture);
		if (VideoTexture != null) {
			if (VideoTexture.didUpdateThisFrame) {
				Video.Update(VideoTexture);
				VideoUpdated();
				//Video.UpdateOutputTexture ();
			}
		}

	}


	//...................................Edit
	void VideoUpdated() {

		for(int j=0; j<Video.Height; ++j) {
			for(int i=0; i<Video.Width; ++i) {
				VideoPixel px=Video.Pixels[i,j];

				px.r2=px.v;//px.flow.x*10.0f;// px.dv*100.0f;//2*0.9f+px.r*0.1f;
				px.g2=px.v;//px.flow.y*10.0f;// px.dv*100.0f;//2*0.9f+px.r*0.1f;
				px.b2=px.v;
			}
		}
	}
}
