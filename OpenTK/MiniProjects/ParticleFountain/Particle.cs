using System;
using OpenTK.Graphics;
using OpenTK.Objects;

namespace OpenTK.MiniProjects.ParticleFountain
{
	public class Particle
	{
		private Random R { get; set; }

		private Vector3 Velocity { get; set; }
		private Vector3 Position { get; set; }
		private readonly float A, B;

		private float ParticleSpeed = 0.2f;
		private const int FULL_CIRCLE = 360;
		private const int NUM_POINTS = 20;

		private const float BOUNCE = -0.6f;

		private const float InitialSpeed = 3;
		private float GRAVITY = 0.0981f;

		private float SPEED_FACTOR = 100;

		public bool DecBlue { get; set; }
		public bool Gravity { get; set; }

		public Particle Slow
		{
			get
			{
				SPEED_FACTOR = 100;
				return this;
			}
		}
		public Particle Fast
		{
			get
			{
				SPEED_FACTOR = 10;
				return this;
			}
		}

		private float Speed;

		public bool ParticleExpired
		{
			get { return Blue <= 0; }
		}

		public float Red { get; set; }
		public float Green { get; set; }
		public float Blue { get; set; }
		public Vector3 Orientation { get; set; }

		public Particle(Vector3 initialPosition, Vector3 velocity, Random r)
		{
			Position = initialPosition;
			Velocity = velocity;
			R = r;

			const double baseCalc = System.Math.PI * FULL_CIRCLE / NUM_POINTS / 180;

			//	Random direction along the X axis
			A = (float)(baseCalc * (r.Next(0, 10000) - 5000) / 5000);
			//	Random direction along the Z axis
			B = (float)(baseCalc * (r.Next(0, 10000) - 5000) / 5000);

			Red = 1.0f; Green = 0.0f; Blue = 0.1f;
			DecBlue = false;

			Gravity = true;
			GRAVITY /= SPEED_FACTOR;

			var startSpeedDiff = (float) (r.NextDouble() / 100 - 0.005);
			Speed = InitialSpeed / SPEED_FACTOR + startSpeedDiff;

			Orientation = Vector3.UnitY;
		}

		public void Move()
		{

			//if (Orientation == Vector3.UnitX)
			//{
			//	Y += B / SPEED_FACTOR;
			//	X += Speed;
			//	Z += A / SPEED_FACTOR;
			//}
			//else if (Orientation == Vector3.UnitX * -1)
			//{
			//	Y += B / SPEED_FACTOR;
			//	X -= Speed;
			//	Z += A / SPEED_FACTOR;
			//}
			//else if (Orientation == Vector3.UnitY)
			//{
			//	Y += Speed;
			//	X += A / SPEED_FACTOR;
			//	Z += B / SPEED_FACTOR;
			//}
			//else if (Orientation == Vector3.UnitY * -1)
			//{
			//	Y -= Speed;
			//	X += A / SPEED_FACTOR;
			//	Z += B / SPEED_FACTOR;
			//}
			//else if (Orientation == Vector3.UnitZ)
			//{
			//	Y += B / SPEED_FACTOR;
			//	X += A / SPEED_FACTOR;
			//	Z += Speed;
			//}
			//else if (Orientation == Vector3.UnitZ * -1)
			//{
			//	Y += B / SPEED_FACTOR;
			//	X += A / SPEED_FACTOR;
			//	Z -= Speed;
			//}

			ApplyGravity();
			AdjustColor();
		}

		private void ApplyGravity()
		{
			if (Gravity)
			{
				Speed -= GRAVITY;

				if (Position.Y < -1)
				{
					Speed *= BOUNCE;
					Position += new Vector3(0, Speed, 0);
				}
			}
		}

		private void AdjustColor()
		{
			Red -= 0.005f;
			Green += 0.0005f;

			if (DecBlue)
			{
				Blue -= 0.005f;
			}
			else
			{
				Blue += 0.005f;
			}

			if (Blue > 0.9f)
			{
				DecBlue = true;
			}
		}

		public void Draw()
		{
			Position += Velocity;
			GL.PushMatrix();

			GL.Translate(Position);
			Shapes.DrawCube(0, 0, 0, 0.01f, Red, Green, Blue);

			GL.PopMatrix();
		}
	}
}