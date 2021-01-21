using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceShooter {
	public class Chunk : IDisposable {
		public Vector3 position;
		public Vector3[] nebulaPositions = new Vector3[GameManager.nebulaCount];

		public Chunk(Vector3 position) {
			this.position = position;
			for (int i = 0; i < nebulaPositions.Length; i++) {
				nebulaPositions[i] = position * GameManager.chunkSize + new Vector3( Random.Range(-1000, 1000), Random.Range(-1000, 1000), Random.Range(-1000, 1000));
			}
		}

		public void Dispose() {
		}
	}
}