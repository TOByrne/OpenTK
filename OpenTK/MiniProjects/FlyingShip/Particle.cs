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
		GameEnvironment Environment { get; set; }

		public Particle(Vector3 position, Vector3 direction, GameEnvironment environment, Random r = null)
		{
			Velocity = direction * new Vector3(0.5f, 0.5f, 0.5f);
			Position = position;

			Environment = environment;
			R = r;
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
	}
}