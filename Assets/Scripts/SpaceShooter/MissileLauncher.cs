using UnityEngine;

namespace SpaceShooter {
	public class MissileLauncher : Weapon {

		public float range = 1000;

		public Transform missileSpawn;
		
		public override void Shoot() {
			GameObject flyingProjectile = Instantiate(projectile, missileSpawn.position, transform.rotation);
			var missile = flyingProjectile.GetComponent<GuidedMissile>();
			missile.target = GetClosestTarget();
			missile.missileReady = true;
			
			base.Shoot();
		}

		public GameObject GetClosestTarget() {
			GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
			
			GameObject closestTarget = null;
			float closestDistance = range;
			
			foreach (var target in targets) {
				var distanceFromTarget = Vector3.Distance(gameObject.transform.position, target.transform.position);
				if(distanceFromTarget < closestDistance)
				{
					//We have a new closest target.
					closestTarget = target;
					closestDistance = distanceFromTarget;
				}
			}

			return closestTarget;
		}
	}
}