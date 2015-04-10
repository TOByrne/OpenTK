using OpenTK.Graphics;
using System;
using OpenTK.Objects;

namespace OpenTK.MiniProjects.FlyingShip
{
	class Particle
	{
		Random R { get; set; }

		public Vector3 Position { get; protected set; }
		public Vector3 Velocity { get; protected set; }

		public Particle(Vector3 velocity, Random r = null)
		{
			//	No position sent to constructor, because starting position
			//	will be 0, 0, 0 +/- some randomness

			Velocity = Randomize(velocity);
			Position = Randomize(new Vector3(0, 0, 0));
		}

		private Vector3 Randomize(Vector3 v)
		{
			var oX = v.X;
			var oY = v.Y;
			var oZ = v.Z;

			var nX = (float)(oX + (R.NextDouble() - 0.5) / 100);
			var nY = (float)(oY + (R.NextDouble() - 0.5) / 100);
			var nZ = (float)(oZ + (R.NextDouble() - 0.5) / 100);

			return new Vector3(nX, nY, nZ);
		}

		public void Draw()
		{
			GL.PushMatrix();

			GL.Translate(Position);
			Shapes.DrawCube(0, 0, 0, 0.1f);

			GL.PopMatrix();

			Velocity = Velocity * 0.9;
		}
	}
}
