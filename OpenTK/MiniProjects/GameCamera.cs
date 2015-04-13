using System;

namespace OpenTK.MiniProjects
{
	public class GameCamera
	{
		public Vector3 Position { get; set; }
		public Vector3 Orientation { get; set; }

		public float Distance { get; set; }
		public float HorizontalAngle { get; set; }
		public float VerticalAngle { get; set; }

		const double baseCalc = System.Math.PI / 180;

		public GameCamera()
		{
			Distance = 20.0f;
			HorizontalAngle = 0.0f;
			VerticalAngle = 0.0f;

			Position = new Vector3(Distance, 1.0f, 0.0f);
		}

		public Matrix4 GetViewMatrix()
		{
			var lookAt = new Vector3(0.0f, 0.0f, 0.0f);

			return Matrix4.LookAt(Position, lookAt - Position, Vector3.UnitY);
		}

		public void LookLeft()
		{
			//	Actually Rotates the Camera Counter-Clockwise around the Y axis
			HorizontalAngle--;
			if (HorizontalAngle < 0) { HorizontalAngle += 360; }

			SetNewPosition();
		}

		public void LookRight()
		{
			//	Actually Rotates the Camera Clockwise around the Y axis
			HorizontalAngle++;
			if (HorizontalAngle > 360) { HorizontalAngle -= 360; }

			SetNewPosition();
		}

		public void LookUp()
		{
			VerticalAngle++;
			if (VerticalAngle > 360) { VerticalAngle -= 360; }

			SetNewPosition();
		}

		public void LookDown()
		{
			VerticalAngle--;
			if (VerticalAngle < 0) { VerticalAngle += 360; }

			SetNewPosition();
		}

		private void SetNewPosition()
		{
			var a = baseCalc * HorizontalAngle;
			var b = baseCalc * VerticalAngle;

			var x = (float)(Distance * System.Math.Cos(a) * System.Math.Sin(b));
			var z = (float)(Distance * System.Math.Sin(a) * System.Math.Sin(b));
			var y = (float)(Distance * System.Math.Cos(b));


			Position = new Vector3(x, y, z);
		}

		public override string ToString()
		{
			var s = String.Format("(X {0}\tY {1}\tZ {2})",
				Position.X.ToString("000.00000"),
				Position.Y.ToString("000.00000"),
				Position.Z.ToString("000.00000")
			);

			s += "\tAngle: " + HorizontalAngle;

			return s;
		}
	}
}