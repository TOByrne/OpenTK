using System;
using System.Collections.Generic;
using OpenTK.Graphics;
using OpenTK.MiniProjects.ParticleFountain;

namespace OpenTK.MiniProjects.ShipEngine
{
	public class Thruster
	{
		public Vector3 Position { get; set; }
		public List<Particle> Particles { get; set; }
		public Random R { get; set; }

		public bool FireThrust { get; set; }

		protected Vector3 Orientation { get; set; }

		public Maneuvering.Thrust Direction { get; protected set; }

		public Thruster Port
		{
			get
			{
				Direction = Maneuvering.Thrust.Port;
				Orientation = Vector3.UnitZ * -1;
				return this;
			}
		}
		public Thruster Starboard
		{
			get
			{
				Direction = Maneuvering.Thrust.Starboard;
				Orientation = Vector3.UnitZ;
				return this;
			}
		}
		public Thruster Forward
		{
			get
			{
				Direction = Maneuvering.Thrust.Forward;
				Orientation = Vector3.UnitX;
				return this;
			}
		}
		public Thruster Reverse
		{
			get
			{
				Direction = Maneuvering.Thrust.Reverse;
				Orientation = Vector3.UnitX * -1;
				return this;
			}
		}

		public Thruster(Vector3 position, Random r)
		{
			Position = position;
			R = r;
			Particles = new List<Particle>();

			FireThrust = false;
		}

		public void Draw()
		{
			Particles.RemoveAll(x => x.ParticleExpired);

			if (FireThrust)
			{
				Thrust();
			}

			foreach (var particle in Particles)
			{
				particle.Move();
				particle.Draw();
			}
		}

		public void Thrust()
		{
			var startX = Position.X;
			var startY = Position.Y;
			var startZ = Position.Z;

			float rX = (float)(R.Next(0, 10000) - 5000) / 50000;
			float rY = (float)(R.Next(0, 10000) - 5000) / 50000;
			float rZ = (float)(R.Next(0, 10000) - 5000) / 50000;
			var randomizer = new Vector3(rX, rY, rZ);
			var particleVelocity = (Orientation + randomizer) / 100;

			Particles.Add(
				new Particle(Position, particleVelocity, R)
				{
					Gravity = false,
					Orientation = Orientation
				}
					.Slow
				);
		}

		public void ThrustOn()
		{
			FireThrust = true;
		}

		public void ThrustOff()
		{
			FireThrust = false;
		}

		public void Set(Maneuvering.ThrusterPosition thrusterPosition)
		{
			ThrusterPosition = thrusterPosition;
		}

		public void Set(Vector3 orientation)
		{
			Orientation = orientation;
		}

		public Maneuvering.ThrusterPosition ThrusterPosition { get; protected set; }
	}
}