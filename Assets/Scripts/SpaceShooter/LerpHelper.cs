using System;
using UnityEngine;

namespace SpaceShooter {
	public class LerpHelper : MonoBehaviour, IDisposable {

		public Vector3 target;
		public float time = 0.075f;
		
		private void FixedUpdate() {
			transform.position = Vector3.Lerp(transform.position, target, time);
		}

		public void Dispose() {
			
		}
	}
}