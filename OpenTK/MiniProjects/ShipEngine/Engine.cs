using System.Drawing;
using System.Linq;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Objects;
using System;
using System.Collections.Generic;

namespace OpenTK.MiniProjects.ShipEngine
{
	public class Engine : GameProject
	{
		private Random R { get; set; }
		public List<Thruster> Engines { get; set; }
		public List<Thruster> ManeuveringThrusters { get; set; }

		int Angle { get; set; }
		int ShipTurn { get; set; }

		public Engine()
		{
			R = new Random(DateTime.Now.Millisecond);

			ClearColor = Color.Black;

			Angle = 0;
			ShipTurn = 0;
		}

		public override void Init()
		{
			Engines = new List<Thruster>
			{
				new Thruster(new Vector3(0.1f, 0, 0.2f), R).Forward,
				new Thruster(new Vector3(0.1f, 0, -0.2f), R).Forward,
				new Thruster(new Vector3(0, -0.05f, 0), R).Forward,
				new Thruster(new Vector3(0, 0.05f, 0), R).Forward
			};

			ManeuveringThrusters = new List<Thruster>
			{
				new Thruster(new Vector3(-1f, 0, 0.1f), R).Starboard,
				new Thruster(new Vector3(-1f, 0, -0.1f), R).Port,
				new Thruster(new Vector3(-1.1f, 0, 0.0f), R).Reverse
			};

			Initialized = true;
		}

		public override void OnRenderFrame(FrameEventArgs e, GameWindow gw)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			var modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref modelview);

			//	Translate to 0,0,0


			//	Translate back

			foreach (var engine in Engines)
			{
				engine.Draw();
			}

			foreach (var thruster in ManeuveringThrusters)
			{
				thruster.Draw();
			}

			Shapes.Platform();

			gw.SwapBuffers();
		}

		public override void OnKeyDown(KeyboardKeyEventArgs keyboardKeyEventArgs, GameWindow game)
		{
			switch (keyboardKeyEventArgs.Key)
			{
				case Key.W:
					foreach (var thruster in Engines)
					{
						thruster.ThrustOn();
					}
					break;

				case Key.S:
					var reverseThrusters = ManeuveringThrusters.Where(x => x.Direction == Maneuvering.Thrust.Reverse);
					foreach (var thruster in reverseThrusters)
					{
						thruster.ThrustOn();
					}
					break;

				case Key.A:
					var portThrusters = ManeuveringThrusters.Where(x => x.Direction == Maneuvering.Thrust.Port);
					foreach (var thruster in portThrusters)
					{
						thruster.ThrustOn();

						//	We increase for each thruster since more thrust => more turning
						ShipTurn++;
					}
					if (Angle >= 360)
					{
						Angle -= 360;
					}
					break;

				case Key.D:
					var starboardThrusters = ManeuveringThrusters.Where(x => x.Direction == Maneuvering.Thrust.Starboard);
					foreach (var thruster in starboardThrusters)
					{
						thruster.ThrustOn();

						//	We increase for each thruster since more thrust => more turning
						ShipTurn--;
					}
					break;
			}

			Angle += ShipTurn;
			if (Angle < 0) { Angle += 360; }
			else if (Angle > 360) { Angle -= 360; }
		}

		public override void OnKeyUp(KeyboardKeyEventArgs keyboardKeyEventArgs, GameWindow game)
		{
			switch (keyboardKeyEventArgs.Key)
			{
				case Key.W:
					foreach (var thruster in Engines)
					{
						thruster.ThrustOff();
					}
					break;

				case Key.S:
					var reverseThrusters = ManeuveringThrusters.Where(x => x.Direction == Maneuvering.Thrust.Reverse);
					foreach (var thruster in reverseThrusters)
					{
						thruster.ThrustOff();
					}
					break;

				case Key.A:
					var portThrusters = ManeuveringThrusters.Where(x => x.Direction == Maneuvering.Thrust.Port);
					foreach (var thruster in portThrusters)
					{
						thruster.ThrustOff();
					}
					break;

				case Key.D:
					var starboardThrusters = ManeuveringThrusters.Where(x => x.Direction == Maneuvering.Thrust.Starboard);
					foreach (var thruster in starboardThrusters)
					{
						thruster.ThrustOff();
					}
					break;
			}
		}
	}
}
