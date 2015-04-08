using System;
using OpenTK.Objects;

namespace OpenTK.MiniProjects.ParticleFountain
{
	public class Particle
	{
		private Random R { get; set; }

		private float X, Y, Z;
		private float A, B;

		private float ParticleSpeed = 0.2f;
		private const int FULL_CIRCLE = 360;
		private const int NUM_POINTS = 20;

		private float InitialSpeed = 3;
		private float GRAVITY = 0.0981f;

		private const float SPEED_FACTOR = 10;

		private float Speed;

		public bool ParticleExpired
		{
			get { return HorizontalDistance > 20; }
		}

		public float HorizontalDistance
		{
			get { return (float)System.Math.Sqrt((X*X) + (Z*Z)); }
		}

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

			Speed = InitialSpeed / SPEED_FACTOR;
			GRAVITY /= SPEED_FACTOR;
		}

		public void Move()
		{
			Y += Speed;
			X += A / SPEED_FACTOR;
			Z += B / SPEED_FACTOR;

			Speed -= GRAVITY;

			if (Y < -1)
			{
				Speed *= -0.8f;
				Y += Speed;
			}
		}

		public void Draw()
		{
			Shapes.DrawCube(X, Y, Z, 0.01f);
		}
	}
}