using System;
using UnityEngine;

namespace SpaceShooter {
	public class Ship : MonoBehaviour {

		public Fraction fraction;

		public Renderer mesh;
		public Transform[] weapons;

		public Transform turretMount;
		
		public int engineMatIndex;

		public TrailRenderer[] engineTrails;
		private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
		private static readonly int Color = Shader.PropertyToID("_Color");


		// Start is called before the first frame update
		void Start() {
			SetFractionAspects();
		}

		// Update is called once per frame
		void Update()
		{
        
		}

		private void SetFractionAspects() {
			Material mat = null;
			Gradient g = null;
			switch (fraction) {
				case Fraction.PLAYER:
					mat = GameManager.PlayerEngineMat;
					g = GameManager.PlayerTrailGradient;
					break;
				case Fraction.ENEMY:
					mat = GameManager.EnemyEngineMat;
					g = GameManager.EnemyTrailGradient;
					break;
				case Fraction.NEUTRAL:
					mat = GameManager.NeutralEngineMat;
					g = GameManager.NeutralTrailGradient;
					break;
				default:
					mat = GameManager.PlayerEngineMat;
					g = GameManager.PlayerTrailGradient;
					break;
			}
			Material[] materials = mesh.materials;
			materials[engineMatIndex] = mat;
			mesh.materials = materials;
			foreach (var trail in engineTrails) {
				trail.colorGradient = g;
				trail.material.SetColor(Color, mat.color);
				//trail.material.SetColor(EmissionColor, mat.color);
			}
		}
		
		public enum Fraction {
			PLAYER,
			ENEMY,
			NEUTRAL
		}
	}
}
