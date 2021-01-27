using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceShooter {
	public class Chunk : MonoBehaviour, IDisposable {
		public Vector3 position;
		public Vector3[] nebulaPositions = new Vector3[GameManager.Instance.nebulaCount];
		public float[] nebulaColors = new float[GameManager.Instance.nebulaCount];

		private bool isLoaded;

		public void Init(Vector3 position) {
			this.position = position;
			Generate();
		}
		
		public void Init(Vector3 position, Vector3[] nebulaPositions, float[] nebulaColors) {
			this.position = position;
			for (int i = 0; i < nebulaPositions.Length; i++) {
				this.nebulaPositions[i] = nebulaPositions[i];
				this.nebulaColors[i] = nebulaColors[i];
			}
		}

		public void Dispose() {
		}

		private void Generate() {
			if (GameManager.Instance.enableNebulas) {
				for (int i = 0; i < nebulaPositions.Length; i++) {
					nebulaPositions[i] = position * GameManager.Instance.chunkSize + new Vector3( Random.Range(0, GameManager.Instance.chunkSize), Random.Range(0, GameManager.Instance.chunkSize), Random.Range(0, GameManager.Instance.chunkSize));
					nebulaColors[i] = Random.value;
				}
			}
			
		}

		public void Load() {
			if (!isLoaded) {
				for (int i = 0; i < nebulaPositions.Length; i++) {
					GameObject nebula2 = Instantiate(GameManager.Instance.nebula, GameManager.Instance.chunkSize * position + nebulaPositions[i], new Quaternion(), transform);
					ParticleSystem.MainModule mainModule = nebula2.GetComponent<ParticleSystem>().main;
					mainModule.startColor = GameManager.Instance.nebulaColors.Evaluate(nebulaColors[i]);
				}
				isLoaded = true;
			}
		}

		public void Unload() {
			if (isLoaded) {
				for (int i = 0; i < transform.childCount; i++) {
					Destroy(transform.GetChild(i).gameObject);
				}
				isLoaded = false;
			}
		}
	}
}