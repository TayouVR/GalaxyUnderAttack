using System.Collections;
using UnityEngine;

namespace SpaceShooter {
	public class DelayedDestroyer : MonoBehaviour {

		public float time;

		public void StartCountdown() {
			StartCoroutine(kill());
		}
		

		private IEnumerator kill() {
			yield return new WaitForSeconds(time);
			Destroy(this.gameObject);
			Destroy(this);
		}
	}
}