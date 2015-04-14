using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics;
using OpenTK.MiniProjects.Shared;

namespace OpenTK.MiniProjects.FlyingShip
{
	public class Ship : IDrawable
	{
		public GameEnvironment Environment { get; set; }
		public Random R { get; set; }
		public Vector3 Position { get; set; }
		public Vector3 Velocity { get; set; }

		List<Thruster> Thrusters { get; set; }
		public int RollAngle { get; set; }
		public int PitchAngle { get; set; }
		public int YawAngle { get; set; }

		public Ship(Vector3 position, Vector3 velocity, GameEnvironment environment, Random r)
		{
			Position = position;
			Velocity = velocity;
			Environment = environment;
			R = r;

			RollAngle = 0;
			YawAngle = 0;
			PitchAngle = 0;
		}

		public void Init()
		{
			Thrusters = new List<Thruster>()
			{
				new Thruster(new Vector3(1, 0, -0.3f), this, Environment),
				new Thruster(new Vector3(-1, 0, -0.3f), this, Environment)
			};
		}

		public void Draw()
		{
			Position += Velocity;

			GL.PushMatrix();
			GL.Translate(Position);

			GL.Rotate(RollAngle++, Vector3.UnitZ);

			GL.Begin(BeginMode.LineStrip);

			GL.Color3(Color.DarkGray);

			GL.Vertex3(0, 0, -2);		//	Nose
			GL.Vertex3(0, 0.5f, 0);		//	Tail top
			GL.Vertex3(1, 0, 0);		//	Wing tip

			GL.Vertex3(0, 0, -2);		//	Nose
			GL.Vertex3(-1, 0, 0);		//	Wing tip
			GL.Vertex3(0, 0.5f, 0);		//	Tail top

			GL.End();

			Thrusters.ForEach(x => x.Draw());
			GL.PopMatrix();
		}

		public void UpdateFrame()
		{
			Thrusters.ForEach(x => x.UpdateFrame());
		}
	}
}