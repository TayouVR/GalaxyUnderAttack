using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceShooter {
	public class Chunk : MonoBehaviour, IDisposable {
		public Vector3 position;
		public Vector3[] nebulaPositions = new Vector3[GameManager.Instance.nebulaCount];
		public float[] nebulaColors = new float[GameManager.Instance.nebulaCount];

		public void Init(Vector3 position) {
			this.position = position;
			Generate();
		}

		public void Dispose() {
		}

		private void Generate() {
			if (GameManager.Instance.enableNebulas) {
				for (int i = 0; i < nebulaPositions.Length; i++) {
					nebulaPositions[i] = position * GameManager.Instance.chunkSize + new Vector3( Random.Range(-1000, 1000), Random.Range(-1000, 1000), Random.Range(-1000, 1000));
					nebulaColors[i] = Random.value;
				}
			}
			
		}

		public void Load() {
			for (int i = 0; i < nebulaPositions.Length; i++) {
				GameObject nebula2 = Instantiate(GameManager.Instance.nebula, GameManager.Instance.chunkSize * position + nebulaPositions[i], new Quaternion(), transform);
				nebula2.GetComponent<ParticleSystem>().startColor = GameManager.Instance.nebulaColors.Evaluate(nebulaColors[i]);
			}
		}

		public void Unload() {
			for (int i = 0; i < transform.childCount; i++) {
				Destroy(transform.GetChild(i).gameObject);
			}
		}
	}
}