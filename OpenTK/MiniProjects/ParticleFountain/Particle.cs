using System;
using OpenTK.Objects;

namespace OpenTK.MiniProjects.ParticleFountain
{
	public class Particle
	{
		private Random R { get; set; }

		private float X, Y, Z;
		private readonly float A, B;

		private float ParticleSpeed = 0.2f;
		private const int FULL_CIRCLE = 360;
		private const int NUM_POINTS = 20;

		private const float BOUNCE = -0.6f;

		private const float InitialSpeed = 3;
		private const float GRAVITY = 0.0981f / SPEED_FACTOR;

		private const float SPEED_FACTOR = 10;

		private float Speed;

		public bool ParticleExpired
		{
			get { return Red <= 0; }
		}

		public float HorizontalDistance
		{
			get { return (float)System.Math.Sqrt((X*X) + (Z*Z)); }
		}

		public float Red { get; set; }
		public float Green { get; set; }
		public float Blue { get; set; }

		public Particle(float x, float y, float z, Random r)
		{
			X = x;
			Y = y;
			Z = z;

			R = r;
			const double baseCalc = System.Math.PI * FULL_CIRCLE / NUM_POINTS / 180;

			//	Random direction along the X axis
			A = (float)(baseCalc * (r.Next(0, 10000) - 5000) / 5000);
			//	Random direction along the Z axis
			B = (float)(baseCalc * (r.Next(0, 10000) - 5000) / 5000);

			Red = 1.0f;
			Green = 0.0f;
			Blue = 0.0f;

			Speed = InitialSpeed / SPEED_FACTOR;
		}

		public void Move()
		{
			Y += Speed;
			X += A / SPEED_FACTOR;
			Z += B / SPEED_FACTOR;

			Speed -= GRAVITY;

			if (Y < -1)
			{
				Speed *= BOUNCE;
				Y += Speed;
			}

			Red -= 0.005f;
			Green += 0.0005f;
			Blue += 0.005f;
		}

		public void Draw()
		{
			Shapes.DrawCube(X, Y, Z, 0.01f, Red, Green, Blue);
		}
	}
}