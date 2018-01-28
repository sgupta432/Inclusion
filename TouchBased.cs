using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.iOS;
using System.IO;
using System;
using System.Net.Sockets;


using System.Collections;
using System.Net;

using System.Text;

/*
 * 
 * 
 *  const float4x4 ycbcrToRGBTransform = float4x4(
        float4(+1.0000f, +1.0000f, +1.0000f, +0.0000f),
        float4(+0.0000f, -0.3441f, +1.7720f, +0.0000f),
        float4(+1.4020f, -0.7141f, +0.0000f, +0.0000f),
        float4(-0.7010f, +0.5291f, -0.8860f, +1.0000f)
    );
 * 
 */
namespace UnityARInterface
{
	public class TouchBased : ARBase
	{
		[SerializeField]
		//private ParticleSystem m_PointCloudParticlePrefab;



		public GameObject test_cube;
		private byte[] y;
		private byte[] uv;
		private int wid;
		private int ht;

		public Text text;
		public Image img;
		public Texture2D A;
		public Texture2D B;
		public Texture2D C;

		[SerializeField]
		private int m_MaxPointsToShow = 300;

		[SerializeField]
		private float m_ParticleSize = 1.0f;

		private int total_count = 240;
		private int curr_count = 0;


		private ARInterface.CameraImage m_camImage;

		bool touchControl = false;

		//UDP communication code.
		UdpClient udp;
		string ipAddress = "128.61.35.76";
		IPEndPoint RemoteIpEndPoint;// = new IPEndPoint(IPAddress.Parse(ipAddress), 8888);	


		private void OnDisable()
		{
			//m_ParticleSystem.SetParticles(m_NoParticles, 1);
		}

		// Use this for initialization
		void Start()
		{
			//m_ParticleSystem = Instantiate(m_PointCloudParticlePrefab, GetRoot());
			//m_NoParticles = new ParticleSystem.Particle[1];
			//m_NoParticles[0].startSize = 0f;
			//text = new Text();
			//ipEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), 8080); //8888 to send data
			// RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
			udp = new UdpClient(8000);

		}

		// Update is called once per frame
		void Update()
		{
			//print (Input.touchCount);
			//Console.Write (Input.touchCount );

			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
			{
				//temp_func ();
			}

			/*

			Byte[] receiveBytes = udp.Receive(ref RemoteIpEndPoint); 
			if(receiveBytes.Length > 0)
			{
			string returnData = Encoding.ASCII.GetString(receiveBytes);

					Console.WriteLine ("This is the message you received " +
					returnData.ToString ());
					Console.WriteLine ("This message was sent from " +
					RemoteIpEndPoint.Address.ToString () +
					" on their port number " +
					RemoteIpEndPoint.Port.ToString ());
				
			}
			*/
			try
			{
				udp.BeginReceive(new AsyncCallback(recv), null);
			}
			catch(Exception e)
			{
				Console.Write(e.ToString() + " Here");
			}


			//test_cube.GetComponent<Renderer> ().material.mainTexture = B;


		}

		//saving to file

		//CallBack
		private void recv(IAsyncResult res)
		{
			IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
			byte[] received = udp.EndReceive(res, ref RemoteIpEndPoint);

			//Process codes
			if (Encoding.UTF8.GetString (received) == "B") 
			{
				GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
				cube.transform.position = new Vector3 (0, 0.5F, 0);
				test_cube.GetComponent<Renderer> ().material.mainTexture = B;
			}
			if (Encoding.UTF8.GetString (received) == "A") 
			{
				GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Sphere);
				cube.transform.position = new Vector3 (0, 0.5F, 0);
				test_cube.GetComponent<Renderer> ().material.mainTexture = A;
			}
			if (Encoding.UTF8.GetString (received) == "C") 
			{
				GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
				cube.transform.position = new Vector3 (0, 0.5F, 0);
				test_cube.GetComponent<Renderer> ().material.mainTexture = C;
			}
			print (Encoding.UTF8.GetString (received));
			Console.Write(Encoding.UTF8.GetString(received));
			udp.BeginReceive(new AsyncCallback(recv), null);
		}

		void temp_func()
		{
			if (ARInterface.GetInterface ().TryGetCameraImage (ref m_camImage)) 
			{
				


					//




					Texture2D tex = new Texture2D (16, 16, TextureFormat.YUY2, false);
					uv = m_camImage.uv;
					y = m_camImage.y;
					tex.LoadRawTextureData (uv);
					tex.Apply ();
					string message = "hello"+uv;
					byte[] toBytes = Encoding.ASCII.GetBytes(message);
					//udp.Send (uv,64, ipEndPoint);

					// Assign texture to renderer's material.
					test_cube.GetComponent<Renderer> ().material.mainTexture = tex;
					
				Console.Write ("complted");


					byte[] one = Encoding.ASCII.GetBytes("Nikita");
					
					byte[] combined = new byte[one.Length + uv.Length];

					for (int i = 0; i < combined.Length; ++i)
					{
						combined[i] = i < one.Length ? one[i] : uv[i - one.Length];
					}


					//udp.Send (combined , combined.Length, ipEndPoint);
					
					//udp.Send (toBytes,toBytes.Length, ipEndPoint);



			} 


			else
			{
				if (curr_count >= total_count) 
				{
					curr_count = 0;
					print ("completed");
				}
				else
					curr_count++;

			}

		}

		void main_func()
		{
			
		}



	}
}
