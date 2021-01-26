using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
	public class GameManager : MonoBehaviour {

		public bool enableNebulas = true;

		public Transform player;

		public Gradient nebulaColors;
		public int chunkSize = 1000;
		public Vector3 currentChunk;


		public GameObject nebula;
		public int nebulaCount = 100;
		public Material enemyEngineMat;
		public Material playerEngineMat;
		public Material neutralEngineMat;
		public Gradient enemyTrailGradient;
		public Gradient playerTrailGradient;
		public Gradient neutralTrailGradient;

		public static GameManager Instance;

		private List<Chunk> chunks = new List<Chunk>();
		private List<Chunk> loadedChunks = new List<Chunk>();
		//public int interval = 60;
	
		//private int frameCount;
		// Start is called before the first frame update
		void Start() {
			Instance = this;
			
			currentChunk = Vector3.zero;
			GetOrCreateCurrentChunk();
		}

		// Update is called once per frame
		void Update() {
			if (player.transform.position.x > chunkSize * currentChunk.x + chunkSize) {
				currentChunk.x += 1;
				GetOrCreateCurrentChunk();
			} else if (player.transform.position.y > chunkSize * currentChunk.y + chunkSize) {
				currentChunk.y += 1;
				GetOrCreateCurrentChunk();
			} else if (player.transform.position.z > chunkSize * currentChunk.z + chunkSize) {
				currentChunk.z += 1;
				GetOrCreateCurrentChunk();
			} else if (player.transform.position.x < chunkSize * currentChunk.x) {
				currentChunk.x -= 1;
				GetOrCreateCurrentChunk();
			} else if (player.transform.position.y < chunkSize * currentChunk.y) {
				currentChunk.y -= 1;
				GetOrCreateCurrentChunk();
			} else if (player.transform.position.z < chunkSize * currentChunk.z) {
				currentChunk.z -= 1;
				GetOrCreateCurrentChunk();
			}

			Chunk[] list = loadedChunks.ToArray();
			foreach (var chunk in list) {
				if (Math.Abs(chunk.position.x - currentChunk.x) > 1) {
					Debug.Log(Math.Abs(chunk.position.x - currentChunk.x));
					chunk.Unload();
					loadedChunks.Remove(chunk);
				}
				if (Math.Abs(chunk.position.y - currentChunk.y) > 1) {
					Debug.Log(Math.Abs(chunk.position.y - currentChunk.y));
					chunk.Unload();
					loadedChunks.Remove(chunk);
				}
				if (Math.Abs(chunk.position.z - currentChunk.z) > 1) {
					Debug.Log(Math.Abs(chunk.position.z - currentChunk.z));
					chunk.Unload();
					loadedChunks.Remove(chunk);
				}
			}

			/*if (frameCount >= interval) {
			    GameObject nebula2 = Instantiate(nebula, new Vector3(Random.Range(-1000, 1000), Random.Range(-1000, 1000), Random.Range(-1000, 1000)), new Quaternion());
			    nebula2.GetComponent<ParticleSystem>().startColor = Random.ColorHSV();
		    }*/
		}

		private Chunk GetOrCreateCurrentChunk() {
			Chunk chunk;
			using (chunk = GetChunkFromVector3(currentChunk)) {
				if ((object)chunk == null) {
					var go = new GameObject("Chunk X:" + currentChunk.x + " Y:" + currentChunk.y + " Z:" + currentChunk.z);
					chunk = go.AddComponent<Chunk>();
					chunk.Init(currentChunk);
					chunks.Add(chunk);
					loadedChunks.Add(chunk);
				}
				chunk.Load();
				return chunk;
			}
		}

		private Chunk GetChunkFromVector3(Vector3 vec) {
			foreach (var chunk in chunks) {
				if (chunk.position == vec) {
					return chunk;
				}
			}
			return null;
		}
    
		public static float Perlin3D(Vector3 pos) {
			pos.Normalize();
			
			float ab = Mathf.PerlinNoise(pos.x,pos.y);
			float bc = Mathf.PerlinNoise(pos.y,pos.z);
			float ac = Mathf.PerlinNoise(pos.x,pos.z);

			float ba = Mathf.PerlinNoise(pos.y,pos.x);
			float cb = Mathf.PerlinNoise(pos.z,pos.y);
			float ca = Mathf.PerlinNoise(pos.z,pos.x);

			float abc = ab+bc+ac+ba+cb+ca;
			
			Debug.Log("ab: " + ab);
			Debug.Log("bc: " + bc);
			Debug.Log("ac: " + ac);
			Debug.Log("ba: " + ba);
			Debug.Log("cb: " + cb);
			Debug.Log("ca: " + ca);
			Debug.Log("abc: " + abc/6f);
			
			return abc/6f;
		}
	}
}
