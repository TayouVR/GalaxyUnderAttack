using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

namespace SpaceShooter {
	public class GameManager : MonoBehaviour {
		
		public List<GameObject> playerShips = new List<GameObject>();
		public List<GameObject> enemyShips = new List<GameObject>();

		private int currentShipIndex;
		private GameObject[] displayedShips = new GameObject[5];
		
		public bool enableNebulas = true;
		
		public Transform player;
		
		public Gradient nebulaColors;
		public int chunkSize = 1000;
		public Vector3 currentChunk;

		// menus
		[Header("UI")]
		[SerializeField] private UIDocument menus;
		private VisualElement mainMenu;
		private VisualElement shipSelectionMenu;
		private VisualElement settingsMenu;
		private VisualElement hud;
		
		// values for other classes to work on
		[Header("Global things")]
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

		public CameraFollow cameraFollow;
		
		void Start() {
			Instance = this;

			cameraFollow = GetComponent<CameraFollow>();

			SetupMenus();
			
			DontDestroyOnLoad(gameObject);
			
			currentChunk = Vector3.zero;
			GetOrCreateCurrentChunks();
		}

		private void SetupMenus() {
			// main menu
			mainMenu = menus.rootVisualElement.Q<VisualElement>("main-menu");
			mainMenu.Q<Button>("start-game-button").clickable.clicked += StartGame;
			mainMenu.Q<Button>("ship-selection-button").clickable.clicked += ToShipSelectionMenu;
			mainMenu.Q<Button>("settings-button").clickable.clicked += () => SetMenuTo(settingsMenu);
			mainMenu.Q<Button>("quit-button").clickable.clicked += Quit;

			// ship selection menu
			shipSelectionMenu = menus.rootVisualElement.Q<VisualElement>("ship-selection-menu");
			shipSelectionMenu.Q<Button>("select-ship-button").clickable.clicked += StartGame;
			shipSelectionMenu.Q<Button>("details-button").clickable.clicked += StartGame;
			shipSelectionMenu.Q<Button>("back-to-main-menu-button").clickable.clicked += () => SetMenuTo(mainMenu);

			// Settings
			settingsMenu = menus.rootVisualElement.Q<VisualElement>("settings-menu");
			settingsMenu.Q<Button>("back-to-main-menu-button").clickable.clicked += () => SetMenuTo(mainMenu);
			
			// HUD
			hud = menus.rootVisualElement.Q<VisualElement>("hud");

			SetMenuTo(mainMenu);

		}

		private void ToShipSelectionMenu() {
			for (int i = -2; i < 2; i++) {
				if (playerShips[currentShipIndex - 2]) {
					displayedShips[i + 2] = Instantiate(playerShips[currentShipIndex - 2]);
				}
			}
			SetMenuTo(shipSelectionMenu);
		}

		private void SetMenuTo(VisualElement menu) {
			mainMenu.visible = mainMenu == menu;
			shipSelectionMenu.visible = shipSelectionMenu == menu;
			settingsMenu.visible = settingsMenu == menu;
			hud.visible = hud == menu;
		}

		private void StartGame() {
			Cursor.lockState = CursorLockMode.Locked;
			SetMenuTo(hud);
			cameraFollow.enabled = true;
			player.GetComponent<Player>().Init();
		}

		private void Quit() {
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#endif
			Application.Quit();
		}
		
		// Update is called once per frame
		void Update() {

			currentChunk.x = (int)Math.Floor((player.transform.position.x - (player.transform.position.x % chunkSize)) / chunkSize);
			currentChunk.y = (int)Math.Floor((player.transform.position.y - (player.transform.position.y % chunkSize)) / chunkSize);
			currentChunk.z = (int)Math.Floor((player.transform.position.z - (player.transform.position.z % chunkSize)) / chunkSize);
			
			/*
			if (player.transform.position.x > chunkSize * currentChunk.x + chunkSize) {
				currentChunk.x += 1;
				GetOrCreateCurrentChunks();
			} else if (player.transform.position.y > chunkSize * currentChunk.y + chunkSize) {
				currentChunk.y += 1;
				GetOrCreateCurrentChunks();
			} else if (player.transform.position.z > chunkSize * currentChunk.z + chunkSize) {
				currentChunk.z += 1;
				GetOrCreateCurrentChunks();
			} else if (player.transform.position.x < chunkSize * currentChunk.x) {
				currentChunk.x -= 1;
				GetOrCreateCurrentChunks();
			} else if (player.transform.position.y < chunkSize * currentChunk.y) {
				currentChunk.y -= 1;
				GetOrCreateCurrentChunks();
			} else if (player.transform.position.z < chunkSize * currentChunk.z) {
				currentChunk.z -= 1;
				GetOrCreateCurrentChunks();
			}*/
			
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

		private void GetOrCreateCurrentChunks() {
			Chunk chunk;
			Vector3[] positions = new[] {
				currentChunk,
				new Vector3(currentChunk.x, currentChunk.y + 1, currentChunk.z),
				new Vector3(currentChunk.x, currentChunk.y + 1, currentChunk.z + 1),
				new Vector3(currentChunk.x, currentChunk.y, currentChunk.z + 1),
				new Vector3(currentChunk.x, currentChunk.y - 1, currentChunk.z),
				new Vector3(currentChunk.x, currentChunk.y - 1, currentChunk.z - 1),
				new Vector3(currentChunk.x, currentChunk.y, currentChunk.z - 1),
				new Vector3(currentChunk.x, currentChunk.y + 1, currentChunk.z - 1),
				new Vector3(currentChunk.x, currentChunk.y - 1, currentChunk.z + 1),
				
				new Vector3(currentChunk.x + 1, currentChunk.y, currentChunk.z),
				new Vector3(currentChunk.x + 1, currentChunk.y + 1, currentChunk.z),
				new Vector3(currentChunk.x + 1, currentChunk.y + 1, currentChunk.z + 1),
				new Vector3(currentChunk.x + 1, currentChunk.y, currentChunk.z + 1),
				new Vector3(currentChunk.x + 1, currentChunk.y - 1, currentChunk.z),
				new Vector3(currentChunk.x + 1, currentChunk.y - 1, currentChunk.z - 1),
				new Vector3(currentChunk.x + 1, currentChunk.y, currentChunk.z - 1),
				new Vector3(currentChunk.x + 1, currentChunk.y + 1, currentChunk.z - 1),
				new Vector3(currentChunk.x + 1, currentChunk.y - 1, currentChunk.z + 1),
				
				new Vector3(currentChunk.x - 1, currentChunk.y, currentChunk.z),
				new Vector3(currentChunk.x - 1, currentChunk.y + 1, currentChunk.z),
				new Vector3(currentChunk.x - 1, currentChunk.y + 1, currentChunk.z + 1),
				new Vector3(currentChunk.x - 1, currentChunk.y, currentChunk.z + 1),
				new Vector3(currentChunk.x - 1, currentChunk.y - 1, currentChunk.z),
				new Vector3(currentChunk.x - 1, currentChunk.y - 1, currentChunk.z - 1),
				new Vector3(currentChunk.x - 1, currentChunk.y, currentChunk.z - 1),
				new Vector3(currentChunk.x - 1, currentChunk.y + 1, currentChunk.z - 1),
				new Vector3(currentChunk.x - 1, currentChunk.y - 1, currentChunk.z + 1)
			};
			foreach (var position in positions) {
				using (chunk = GetChunkFromVector3(position)) {
					if ((object)chunk == null) {
						var go = new GameObject("Chunk X:" + position.x + " Y:" + position.y + " Z:" + position.z);
						go.transform.SetParent(transform);
						chunk = go.AddComponent<Chunk>();
						chunk.Init(currentChunk);
						chunks.Add(chunk);
						loadedChunks.Add(chunk);
					}
					chunk.Load();
				}
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
