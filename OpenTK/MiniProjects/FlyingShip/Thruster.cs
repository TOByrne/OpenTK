using OpenTK.Graphics;
using OpenTK.MiniProjects.Shared;
using OpenTK.Objects;

namespace OpenTK.MiniProjects.FlyingShip
{
	public class Thruster : IDrawable
	{
		public GameEnvironment Environment { get; set; }
		public Vector3 Position { get; set; }
		public Vector3 Velocity { get; set; }
		public Ship Ship { get; set; }


		public Thruster(Vector3 position, Ship ship, GameEnvironment environment)
		{
			Position = position;
			Ship = ship;
			Environment = environment;
		}

		public void Draw()
		{
			GL.PushMatrix();

			Shapes.DrawCuboid(Position.X, Position.Y, Position.Z, 0.1f, 0.1f, 0.8f);

			GL.PopMatrix();
		}

		public void UpdateFrame()
		{
			float rA = (float)(Ship.RollAngle * System.Math.PI / 180);
			float yA = (float)(Ship.YawAngle * System.Math.PI / 180);
			float pA = (float)(Ship.PitchAngle * System.Math.PI / 180);

			var particlePosition = Ship.Position + Position;
			particlePosition = Vector3.Transform(particlePosition,
				Matrix4.CreateRotationZ(rA)
			);
			var particle = new Particle(particlePosition, Vector3.UnitZ, Environment);
			Environment.AddWorldObject(particle);
		}
	}
}