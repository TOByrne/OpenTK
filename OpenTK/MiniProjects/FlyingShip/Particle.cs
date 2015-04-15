using System;
using OpenTK.Graphics;
using OpenTK.MiniProjects.Shared;
using OpenTK.Objects;

namespace OpenTK.MiniProjects.FlyingShip
{
	public class Particle : IDrawable
	{
		Random R { get; set; }
		public Vector3 Position { get; set; }
		public Vector3 Velocity { get; set; }
		public bool Dead { get { return Position.Length > 10; } }
		GameEnvironment Environment { get; set; }

		public Particle(Vector3 position, Vector3 direction, GameEnvironment environment, Random r = null)
		{
			if (r == null)
			{
				r = new Random(DateTime.Now.Millisecond);
			}
			R = r;

			Velocity = ParticleRandomizer(direction * new Vector3(0.1f, 0.1f, 0.1f));
			Position = position;

			Environment = environment;
		}

		public void Draw()
		{
			Position += Velocity;

			GL.PushMatrix();
			GL.Translate(Position);
			Shapes.DrawCube(0, 0, 0, 0.01f);
			GL.PopMatrix();
		}

		public void UpdateFrame()
		{
		}

		public Vector3 ParticleRandomizer(Vector3 v)
		{
			const double baseCalc = System.Math.PI / 180;


			//	Random along X
			var x = (float)(baseCalc * (R.Next(0, 10000) - 5000) / 10000);
			//	Random along Z
			var y = (float)(baseCalc * (R.Next(0, 10000) - 5000) / 10000);
			//	Random along Y
			var z = (float)(baseCalc * (R.Next(0, 10000) - 5000) / 10000);

			return v + new Vector3(x, y, z);
		}
	}
}