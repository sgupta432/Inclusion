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
	public class ARCamImageViz : ARBase
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
		[SerializeField]
		private int m_MaxPointsToShow = 300;

		[SerializeField]
		private float m_ParticleSize = 1.0f;

		private int total_count = 240;
		private int curr_count = 0;


		private ARInterface.CameraImage m_camImage;



		//UDP communication code.
		UdpClient udp = new UdpClient();
		string ipAddress = "128.61.35.76";
		IPEndPoint ipEndPoint;// = new IPEndPoint(IPAddress.Parse(ipAddress), 8888);	


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
			ipEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), 8888);

		}

		// Update is called once per frame
		void Update()
		{
			
			if (ARInterface.GetInterface ().TryGetCameraImage (ref m_camImage)) 
			{
				if (curr_count >= total_count) 
				{
					

					//




				   Texture2D tex = new Texture2D (16, 16, TextureFormat.YUY2, false);
					uv = m_camImage.uv;
					y = m_camImage.y;
					tex.LoadRawTextureData (uv);
					tex.Apply ();
					string message = "hello"+uv;
					byte[] toBytes = Encoding.ASCII.GetBytes(message);
					udp.Send (uv,8, ipEndPoint);

					// Assign texture to renderer's material.
					test_cube.GetComponent<Renderer> ().material.mainTexture = tex;
					//img.GetComponent<Renderer> ().material.mainTexture = tex;
					curr_count = 0;
					print ("complted");

					byte[] one = Encoding.ASCII.GetBytes("Nikita");
				//	byte[] two = getBytesForTwo();
					byte[] combined = new byte[one.Length + uv.Length];

					for (int i = 0; i < combined.Length; ++i)
					{
						combined[i] = i < one.Length ? one[i] : uv[i - one.Length];
					}


					udp.Send (combined , combined.Length, ipEndPoint);
					//text.text = m_camImage.height.ToString() + " " + m_camImage.width.ToString();
					udp.Send (toBytes,toBytes.Length, ipEndPoint);

				
				}
				else
				curr_count++;
				
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

		//saving to file








	}
}
