using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
	[CreateAssetMenu(fileName = "New Droptable", menuName = "SpaceShooter/Droptable", order = 0)]
	public class Droptable : ScriptableObject {
		[SerializeField]public List<DropListElement> dropList;
	}
}