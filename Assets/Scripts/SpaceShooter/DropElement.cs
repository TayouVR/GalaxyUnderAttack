using System;
using SpaceShooter;
using UnityEngine;

	[CreateAssetMenu(fileName = "New Drop Element", menuName = "SpaceShooter/Drop Element", order = 0)]
	public class DropElement : ScriptableObject {
		public GameObject element;
		public DropType type;
	}