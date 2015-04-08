using System;

namespace OpenTK.MiniProjects
{
	public class GameCamera
	{
		public Vector3 Position { get; set; }
		public Vector3 Orientation { get; set; }

		public float Distance { get; set; }
		public float Angle { get; set; }

		const double baseCalc = System.Math.PI / 180;

		public GameCamera()
		{
			Distance = 10.0f;
			Angle = 0.0f;

			Position = new Vector3(Distance, 1.0f, 0.0f);
		}

		public Matrix4 GetViewMatrix()
		{
			Vector3 lookAt = new Vector3(0.0f, 0.0f, 0.0f);

			return Matrix4.LookAt(Position, lookAt - Position, Vector3.UnitY);
		}

		public void LookLeft()
		{
			//	Actually Rotates the Camera Counter-Clockwise around the Y axis
			Angle++;
			if (Angle > 360) { Angle -= 360; }

			SetNewPosition();
		}

		public void LookRight()
		{
			//	Actually Rotates the Camera Clockwise around the Y axis
			Angle--;
			if (Angle < 0) { Angle += 360; }

			SetNewPosition();
		}

		private void SetNewPosition()
		{
			var a = baseCalc*Angle;
			var x = (float) (Distance*System.Math.Cos(a));
			var z = (float) (Distance*System.Math.Sin(a));

			Position = new Vector3(x, Position.Y, z);
		}

		public void LookUp()
		{
		}

		public void LookDown()
		{
		}

		public override string ToString()
		{
			var s = String.Format("(X {0}\tY {1}\tZ {2})",
				Position.X.ToString("000.00000"),
				Position.Y.ToString("000.00000"),
				Position.Z.ToString("000.00000")
				);

			s += "\tAngle: " + Angle;

			return s;
		}
	}
}