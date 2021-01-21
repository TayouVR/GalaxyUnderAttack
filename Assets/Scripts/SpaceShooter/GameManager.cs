using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
	public class GameManager : MonoBehaviour {

		public GameObject nebula;

		public Transform player;

		public Gradient nebulaColors;
		public static int chunkSize;
		public Vector3 currentChunk;



		private List<Chunk> chunks = new List<Chunk>();

		public static int nebulaCount = 100;
		//public int interval = 60;
	
		//private int frameCount;
		// Start is called before the first frame update
		void Start() {
			currentChunk = Vector3.zero;
			GenerateNewEnvironmentStuff();
		}

		// Update is called once per frame
		void Update() {
			if (player.transform.position.x > chunkSize * currentChunk.x) {
				currentChunk.x += 1;
				GenerateNewEnvironmentStuff();
			}
			if (player.transform.position.y > chunkSize * currentChunk.y) {
				currentChunk.y += 1;
				GenerateNewEnvironmentStuff();
			}
			if (player.transform.position.y > chunkSize * currentChunk.z) {
				currentChunk.z += 1;
				GenerateNewEnvironmentStuff();
			}
			if (player.transform.position.x < chunkSize * currentChunk.x) {
				currentChunk.x -= 1;
				GenerateNewEnvironmentStuff();
			}
			if (player.transform.position.y < chunkSize * currentChunk.y) {
				currentChunk.y -= 1;
				GenerateNewEnvironmentStuff();
			}
			if (player.transform.position.y < chunkSize * currentChunk.z) {
				currentChunk.z -= 1;
				GenerateNewEnvironmentStuff();
			}

			/*if (frameCount >= interval) {
		    GameObject nebula2 = Instantiate(nebula, new Vector3(Random.Range(-1000, 1000), Random.Range(-1000, 1000), Random.Range(-1000, 1000)), new Quaternion());
		    nebula2.GetComponent<ParticleSystem>().startColor = Random.ColorHSV();
	    }*/
		}

		private void GenerateNewEnvironmentStuff() {
			for (int i = 0; i < nebulaCount; i++) {
				Vector3 currentNebulaPosition = GetOrCreateCurrentChunk().nebulaPositions[i];
				GameObject nebula2 = Instantiate(nebula, player.transform.position + currentNebulaPosition, new Quaternion());
				nebula2.GetComponent<ParticleSystem>().startColor = nebulaColors.Evaluate(Perlin3D(currentNebulaPosition));
			}
		}

		private Chunk GetOrCreateCurrentChunk() {
			Chunk chunk;
			using (chunk = GetChunkFromVector3(currentChunk)) {
				if (chunk == null) {
					chunk = new Chunk(currentChunk);
					chunks.Add(chunk);
				}
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
    
		public static float Perlin3D(Vector3 pos){
			float AB = Mathf.PerlinNoise(pos.x,pos.y);
			float BC = Mathf.PerlinNoise(pos.y,pos.z);
			float AC = Mathf.PerlinNoise(pos.x,pos.z);

			float BA = Mathf.PerlinNoise(pos.y,pos.x);
			float CB = Mathf.PerlinNoise(pos.z,pos.y);
			float CA = Mathf.PerlinNoise(pos.z,pos.x);

			float ABC = AB+BC+AC+BA+CB+CA;
			Debug.Log(ABC/6f);
			return ABC/6f;
		}
	}
}
