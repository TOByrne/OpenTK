using System.Drawing;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.MiniProjects.Shared;
using System;
using OpenTK.MiniProjects.ShipEngine;
using OpenTK.Objects;

namespace OpenTK.MiniProjects.FlyingShip
{
	public class FlyingShipProject : GameProject
	{
		readonly GameEnvironment Environment = new GameEnvironment();
		Random R = new Random(DateTime.Now.Millisecond);

		Ship Ship { get; set; }

		public FlyingShipProject()
		{
			ClearColor = Color.Black;
		}

		public override void Init()
		{
			Ship = new Ship(new Vector3(0, 0, 0), new Vector3(0, 0, 0), Environment, R);
			Ship.Init();

			Environment.AddWorldObject(Ship);
		}

		public override void OnUpdateFrame(FrameEventArgs e, GameWindow gw)
		{
			base.OnUpdateFrame(e, gw);

			Ship.UpdateFrame();
		}

		public override void OnRenderFrame(FrameEventArgs e, GameWindow gw)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			var modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref modelview);

			GL.PushMatrix();
			GL.Translate(0, -1, 0);
			Shapes.Platform();
			GL.PopMatrix();

			Environment.Render();

			gw.SwapBuffers();
		}

		public override void OnKeyDown(KeyboardKeyEventArgs keyboardKeyEventArgs, GameWindow game)
		{
			switch (keyboardKeyEventArgs.Key)
			{
				case Key.W:
					Ship.BeginManeuver(Maneuvering.Flight.Forward);
					break;
				case Key.S:
					Ship.BeginManeuver(Maneuvering.Flight.Backward);
					break;
				case Key.A:
					Ship.BeginManeuver(Maneuvering.Flight.RollLeft);
					break;
				case Key.D:
					Ship.BeginManeuver(Maneuvering.Flight.RollRight);
					break;
				case Key.Z:
					Ship.BeginManeuver(Maneuvering.Flight.YawLeft);
					break;
				case Key.C:
					Ship.BeginManeuver(Maneuvering.Flight.YawRight);
					break;
			}
		}

		public override void OnKeyUp(KeyboardKeyEventArgs keyboardKeyEventArgs, GameWindow game)
		{
			switch (keyboardKeyEventArgs.Key)
			{
				case Key.W:
					Ship.StopMeneuver(Maneuvering.Flight.Forward);
					break;
				case Key.S:
					Ship.StopMeneuver(Maneuvering.Flight.Backward);
					break;
				case Key.A:
					Ship.StopMeneuver(Maneuvering.Flight.RollLeft);
					break;
				case Key.D:
					Ship.StopMeneuver(Maneuvering.Flight.RollRight);
					break;
				case Key.Z:
					Ship.StopMeneuver(Maneuvering.Flight.YawLeft);
					break;
				case Key.C:
					Ship.StopMeneuver(Maneuvering.Flight.YawRight);
					break;
			}
		}
	}

	public class Engine : IDrawable
	{
		public Vector3 Position { get; set; }
		public Vector3 Velocity { get; set; }

		public void Draw()
		{
			
		}

		public void UpdateFrame()
		{
		}
	}
}
