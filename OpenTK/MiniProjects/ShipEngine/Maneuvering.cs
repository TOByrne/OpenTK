using System;

namespace OpenTK.MiniProjects.ShipEngine
{
	public struct Maneuvering
	{
		public enum Thrust
		{
			Port,
			Starboard,
			Forward,
			Reverse
		}

		[Flags]
		public enum ThrusterPosition
		{
			None = 0x0000,
			Nose = 0x0001,
			Tail = 0x0002,
			Bottom = 0x0004,
			Top = 0x0008,
			Front = 0x0016,
			Rear = 0x0032,
			Left = 0x0064,
			Right = 0x0128,
		}
	}
}
