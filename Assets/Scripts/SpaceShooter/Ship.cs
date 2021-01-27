using System;
using UnityEngine;

namespace SpaceShooter {
	public class Ship : MonoBehaviour {

		public Fraction fraction;

		public Renderer mesh;
		public Transform[] weapons;

		public Transform[] turretMounts;
		
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
					mat = GameManager.Instance.playerEngineMat;
					g = GameManager.Instance.playerTrailGradient;
					break;
				case Fraction.ENEMY:
					mat = GameManager.Instance.enemyEngineMat;
					g = GameManager.Instance.enemyTrailGradient;
					break;
				case Fraction.NEUTRAL:
					mat = GameManager.Instance.neutralEngineMat;
					g = GameManager.Instance.neutralTrailGradient;
					break;
				default:
					mat = GameManager.Instance.playerEngineMat;
					g = GameManager.Instance.playerTrailGradient;
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
