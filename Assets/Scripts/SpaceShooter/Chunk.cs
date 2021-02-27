using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceShooter {
	public class Chunk : MonoBehaviour {
		public Vector3 position;
		public SavedNebula[] nebulas = new SavedNebula[GameManager.Instance.nebulaCount];
		public SavedAsteroid[] asteroids = new SavedAsteroid[GameManager.Instance.asteroidCount];

		private bool isLoaded;

		public void Init(Vector3 position) {
			this.position = position;
			Generate();
		}
		
		public void Init(Vector3 position, Vector3[] nebulaPositions, float[] nebulaColors) {
			this.position = position;
			for (int i = 0; i < nebulaPositions.Length; i++) {
				nebulas[i] = new SavedNebula();
				nebulas[i].position = nebulaPositions[i];
				nebulas[i].color = nebulaColors[i];
			}
		}

		private void Generate() {
			if (GameManager.Instance.enableNebulas) {
				for (var i = 0; i < nebulas.Length; i++) {
					nebulas[i] = new SavedNebula();
					nebulas[i].position = position * GameManager.Instance.chunkSize + new Vector3(
						Random.Range(0, GameManager.Instance.chunkSize),
						Random.Range(0, GameManager.Instance.chunkSize),
						Random.Range(0, GameManager.Instance.chunkSize));
					nebulas[i].color = Random.value;
				}
			}
			if (GameManager.Instance.enableAsteroids) {
				for (var i = 0; i < asteroids.Length; i++) {
					asteroids[i] = new SavedAsteroid();
					asteroids[i].position = position * GameManager.Instance.chunkSize +
					                        new Vector3(Random.Range(0, GameManager.Instance.chunkSize),
						                        Random.Range(0, GameManager.Instance.chunkSize),
						                        Random.Range(0, GameManager.Instance.chunkSize));
					asteroids[i].rotation = Random.rotation;
					asteroids[i].guid = GameManager.Instance.asteroidIDs[Random.Range(0, GameManager.Instance.asteroidIDs.Count-1)];
				}
			}
		}

		public void Load() {
			if (!isLoaded) {
				foreach (var nebula in nebulas) {
					GameObject nebula2 = Instantiate(GameManager.Instance.nebula, GameManager.Instance.chunkSize * position + nebula.position, new Quaternion(), transform);
					ParticleSystem.MainModule mainModule = nebula2.GetComponent<ParticleSystem>().main;
					mainModule.startColor = GameManager.Instance.nebulaColors.Evaluate(nebula.color);
				}
				foreach (var asteroid in asteroids) {
					GameObject asteroid2 = Instantiate(GameManager.Instance.asteroids[Random.Range(0, GameManager.Instance.asteroids.Count)], GameManager.Instance.chunkSize * position + asteroid.position, new Quaternion(), transform);
					asteroid2.transform.position = asteroid.position;
					asteroid2.transform.rotation = asteroid.rotation;
					asteroid.gameObject = asteroid2;
					asteroid.guid = asteroid2.GetComponent<Asteroid>().id;
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

		public class SavedNebula {
			public Vector3 position;
			public float color;
		}

		public class SavedAsteroid {
			public string guid;
			public Vector3 position;
			public Quaternion rotation;
			[NonSerialized] public GameObject gameObject;
		}
	}
}