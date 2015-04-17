using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenTK.Graphics;
using OpenTK.MiniProjects.Shared;
using OpenTK.MiniProjects.ShipEngine;

namespace OpenTK.MiniProjects.FlyingShip
{
	public class Ship : IDrawable
	{
		public GameEnvironment Environment { get; set; }
		public Random R { get; set; }
		public Vector3 Position { get; set; }
		public Vector3 Velocity { get; set; }
		public bool Dead { get { return false; } }
		float xVelocity { get; set; }
		float yVelocity { get; set; }
		float zVelocity { get; set; }

		List<Thruster> Thrusters { get; set; }
		public int RollAngle { get; set; }
		public int PitchAngle { get; set; }
		public int YawAngle { get; set; }

		List<Thruster> MainEngines { get; set; }
		List<Thruster> ReverseThrusters { get; set; }
		Maneuvering.Flight engagedFlight { get; set; }


		public Ship(Vector3 position, Vector3 velocity, GameEnvironment environment, Random r)
		{
			Position = position;
			Velocity = velocity;
			xVelocity = Velocity.X;
			yVelocity = Velocity.Y;
			zVelocity = Velocity.Z;

			Environment = environment;
			R = r;

			RollAngle = 0;
			YawAngle = 0;
			PitchAngle = 0;
		}

		public void Init()
		{
			InitForwardThrusters();
			InitReverseThrusters();
			InitYawLeftThrusters();
			InitYawRightThrusters();
			InitRollLeftThrusters();
			InitRollRightThrusters();
			//InitPitchUp();
			//InitPitchDown();
		}

		private void InitForwardThrusters()
		{
			var thruster01 = new Thruster(new Vector3(0.5f, 0, 0.0f), this, Vector3.UnitZ, Environment);
			var thruster02 = new Thruster(new Vector3(-0.5f, 0, 0.0f), this, Vector3.UnitZ, Environment);

			thruster01.Location = Thruster.Placement.Back;
			thruster02.Location = Thruster.Placement.Back;

			Thrusters = new List<Thruster>()
			{
				thruster01,
				thruster02
			};

			MainEngines = new List<Thruster>() {thruster01, thruster02};
		}

		private void InitReverseThrusters()
		{
			var thruster01 = new Thruster(new Vector3(0.01f, 0, -1.8f), this, Vector3.UnitZ * -1, Environment);
			var thruster02 = new Thruster(new Vector3(-0.01f, 0, -1.8f), this, Vector3.UnitZ * -1, Environment);

			thruster01.Location = Thruster.Placement.Front;
			thruster02.Location = Thruster.Placement.Front;

			Thrusters.Add(thruster01);
			Thrusters.Add(thruster02);

			ReverseThrusters = new List<Thruster>() { thruster01, thruster02 };
		}

		private void InitRollLeftThrusters()
		{
			var thruster01 = new Thruster(new Vector3(1f, 0, 0), this, Vector3.UnitY * -1, Environment);
			thruster01.Location = Thruster.Placement.Right | Thruster.Placement.Bottom;
			Thrusters.Add(thruster01);

			var thruster02 = new Thruster(new Vector3(-1f, 0, 0), this, Vector3.UnitY, Environment);
			thruster02.Location = Thruster.Placement.Left | Thruster.Placement.Top;
			Thrusters.Add(thruster02);
		}

		private void InitRollRightThrusters()
		{
			var thruster01 = new Thruster(new Vector3(1f, 0, 0), this, Vector3.UnitY, Environment);
			thruster01.Location = Thruster.Placement.Right | Thruster.Placement.Top;
			Thrusters.Add(thruster01);

			var thruster02 = new Thruster(new Vector3(-1f, 0, 0), this, Vector3.UnitY * -1, Environment);
			thruster02.Location = Thruster.Placement.Left | Thruster.Placement.Bottom;
			Thrusters.Add(thruster02);
		}

		private void InitYawLeftThrusters()
		{
			var thruster01 = new Thruster(new Vector3(0.01f, 0, -1.8f), this, Vector3.UnitX, Environment);
			thruster01.Location = Thruster.Placement.Front | Thruster.Placement.Right;
			Thrusters.Add(thruster01);

			var thruster02 = new Thruster(new Vector3(-0.8f, 0, 0f), this, Vector3.UnitX * -1, Environment);
			thruster02.Location = Thruster.Placement.Back | Thruster.Placement.Left;
			Thrusters.Add(thruster02);
		}

		private void InitYawRightThrusters()
		{
			var thruster01 = new Thruster(new Vector3(-0.01f, 0, -1.8f), this, Vector3.UnitX * -1, Environment);
			thruster01.Location = Thruster.Placement.Front | Thruster.Placement.Left;
			Thrusters.Add(thruster01);

			var thruster02 = new Thruster(new Vector3(0.8f, 0, 0f), this, Vector3.UnitX, Environment);
			thruster02.Location = Thruster.Placement.Back | Thruster.Placement.Right;
			Thrusters.Add(thruster02);
		}

		public void Draw()
		{
			Position += Velocity;

			//	Reading about quaternions here:
			//	http://www.opengl-tutorial.org/intermediate-tutorials/tutorial-17-quaternions/
			//
			//	And here:
			//	http://content.gpwiki.org/index.php/OpenGL%3aTutorials%3aUsing_Quaternions_to_represent_rotation


			//Quaternion rot = new Quaternion();


			GL.PushMatrix();
			GL.Translate(Position);

			var xRot = Matrix3.CreateRotationX(PitchAngle);
			var yRot = Matrix3.CreateRotationY(YawAngle);
			var zRot = Matrix3.CreateRotationZ(RollAngle);

			var shipRotation = Matrix4.CreateRotationZ(RollAngle) *
				Matrix4.CreateRotationY(YawAngle) *
				Matrix4.CreateRotationX(PitchAngle);


			//GL.Rotate(RollAngle, Vector3.UnitZ);
			//GL.Rotate(YawAngle, Vector3.UnitY);



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
			if (engagedFlight.HasFlag(Maneuvering.Flight.Forward)) { Forward(); }
			if (engagedFlight.HasFlag(Maneuvering.Flight.Backward)) { Backward(); }
			if (engagedFlight.HasFlag(Maneuvering.Flight.RollLeft)) { RollLeft(); }
			if (engagedFlight.HasFlag(Maneuvering.Flight.RollRight)) { RollRight(); }
			if (engagedFlight.HasFlag(Maneuvering.Flight.PitchDown)) { PitchDown(); }
			if (engagedFlight.HasFlag(Maneuvering.Flight.PitchUp)) { PitchUp(); }
			if (engagedFlight.HasFlag(Maneuvering.Flight.YawLeft)) { YawLeft(); }
			if (engagedFlight.HasFlag(Maneuvering.Flight.YawRight)) { YawRight(); }

			Position += Velocity;
		}

		private void Forward()
		{
			MainEngines.ForEach(x => x.Fire());
			Velocity += new Vector3(xVelocity, yVelocity, zVelocity);
		}

		private void Backward()
		{
			ReverseThrusters.ForEach(x => x.Fire());
			Velocity += new Vector3(xVelocity, yVelocity, zVelocity);
		}

		private void RollLeft()
		{
			RollAngle++;

			//	Thrusters on the right and oriented down
			//	Thrusters on the left and oriented up
			var thrusters = Thrusters.Where(x =>
				x.Orientation == Vector3.UnitY && x.Location.HasFlag(Thruster.Placement.Left) ||
				x.Orientation == Vector3.UnitY * -1 && x.Location.HasFlag(Thruster.Placement.Right)
			).ToList();

			thrusters.ForEach(x => x.Fire());
		}

		private void RollRight()
		{
			RollAngle--;

			//	Thrusters on the right and oriented up
			//	Thrusters on the left and oriented down.
			var thrusters = Thrusters.Where(x =>
				x.Orientation == Vector3.UnitY && x.Location.HasFlag(Thruster.Placement.Right) ||
				x.Orientation == Vector3.UnitY * -1 && x.Location.HasFlag(Thruster.Placement.Left)
			).ToList();

			thrusters.ForEach(x => x.Fire());
		}

		private void PitchUp()
		{
			//	Thrusters at the front and oriented down
			//	Thrusters at the back and oriented up
			var thrusters = Thrusters.Where(x =>
				(x.Location.HasFlag(Thruster.Placement.Front) && x.Orientation == Vector3.UnitY) ||
				(x.Location.HasFlag(Thruster.Placement.Back) && x.Orientation == (Vector3.UnitY*-1))
				).ToList();

			thrusters.ForEach(x => x.Fire());
		}

		private void PitchDown()
		{
			//	Thrusters at the front and oriented up
			//	Thrusters at the back and oriented down
			var thrusters = Thrusters.Where(x =>
				(x.Location.HasFlag(Thruster.Placement.Front) && x.Orientation == Vector3.UnitY * -1) ||
				(x.Location.HasFlag(Thruster.Placement.Back) && x.Orientation == (Vector3.UnitY))
				).ToList();

			thrusters.ForEach(x => x.Fire());
		}

		private void YawLeft()
		{
			YawAngle++;

			//	Thrusters at the front and oriented right
			//	Thrusters at the back and oriented left
			var thrusters = Thrusters.Where(x =>
				(x.Location.HasFlag(Thruster.Placement.Front | Thruster.Placement.Right) && x.Orientation == Vector3.UnitX) ||
				(x.Location.HasFlag(Thruster.Placement.Back | Thruster.Placement.Left) && x.Orientation == (Vector3.UnitX * -1))
				).ToList();

			thrusters.ForEach(x => x.Fire());
		}

		private void YawRight()
		{
			YawAngle--;

			//	Thrusters at the front and oriented left
			//	Thrusters at the back and oriented right
			var thrusters = Thrusters.Where(x =>
				(x.Location.HasFlag(Thruster.Placement.Front | Thruster.Placement.Left) && x.Orientation == Vector3.UnitX * -1) ||
				(x.Location.HasFlag(Thruster.Placement.Back | Thruster.Placement.Right) && x.Orientation == (Vector3.UnitX))
				).ToList();

			thrusters.ForEach(x => x.Fire());
		}

		public void BeginManeuver(Maneuvering.Flight maneuver)
		{
			//	Add the current maneuver to the already started maneuvers
			engagedFlight |= maneuver;
		}

		public void StopMeneuver(Maneuvering.Flight maneuver)
		{
			engagedFlight &= ~maneuver;
		}
	}
}