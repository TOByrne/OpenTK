using System;
using OpenTK.Graphics;
using OpenTK.MiniProjects.Shared;
using OpenTK.Objects;

namespace OpenTK.MiniProjects.FlyingShip
{
	public class Thruster : IDrawable
	{
		[Flags]
		public enum Placement
		{
			None = 1,
			Front = 2,
			Back = 4,
			Left = 8,
			Right = 16,
			Top = 32,
			Bottom = 64
		};
		public Placement Location { get; set; }

		public GameEnvironment Environment { get; set; }
		public Vector3 Orientation { get; set; }
		public Vector3 Position { get; set; }
		public Vector3 Velocity { get; set; }
		public bool Dead { get { return false; } }
		public Ship Ship { get; set; }


		public Thruster(Vector3 position, Ship ship, Vector3 orientation, GameEnvironment environment)
		{
			Position = position;
			Ship = ship;
			Environment = environment;
			Orientation = orientation;
		}

		public void Draw()
		{
			//GL.PushMatrix();

			//Shapes.DrawCuboid(Position.X, Position.Y, Position.Z, 0.05f, 0.05f, 0.4f);

			//GL.PopMatrix();
		}

		public void UpdateFrame()
		{

		}

		public void Fire()
		{
			var rA = (float)(Ship.RollAngle * System.Math.PI / 180);
			var yA = (float)(Ship.YawAngle * System.Math.PI / 180);
			var pA = (float)(Ship.PitchAngle * System.Math.PI / 180);

			var shipRotation = Matrix4.CreateRotationZ(rA)*
				Matrix4.CreateRotationX(pA)*
				Matrix4.CreateRotationY(yA);

			var particlePosition = Ship.Position + Position;
			particlePosition = Vector3.Transform(particlePosition, shipRotation);
			var particleDirection = Vector3.Transform(Orientation, shipRotation);

			var particle = new Particle(particlePosition, particleDirection, Environment);
			Environment.AddWorldObject(particle);
		}
	}
}