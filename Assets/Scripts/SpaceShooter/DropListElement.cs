using System;

namespace SpaceShooter {
	[Serializable]
	public class DropListElement {
		public DropElement element;
		public int maxAmount;
		public float chance;
	}
}