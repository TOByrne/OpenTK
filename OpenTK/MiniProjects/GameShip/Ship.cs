using System.Drawing;
using OpenTK.Graphics;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.MiniProjects.ShipEngine;
using OpenTK.Objects;

namespace OpenTK.MiniProjects.GameShip
{
	public class Ship : GameProject
	{
		Random R { get; set; }

		private List<Thruster> Thrusters { get; set; }

		public int RollAngle { get; set; }
		public bool IncRoll { get; set; }
		public bool DecRoll { get; set; }

		public int PitchAngle { get; set; }
		public int YawAngle { get; set; }

		public Ship(Random r = null)
		{
			if (r == null)
			{
				r = new Random(DateTime.Now.Millisecond);
			}

			R = r;

			Thrusters = new List<Thruster>();

			ClearColor = Color.Black;

			IncRoll = false;
			DecRoll = false;
		}

		public override void Init()
		{
			InitEngines();
			InitManeuveringThrusters();
		}

		public override void OnRenderFrame(FrameEventArgs e, GameWindow gw)
		{
			if (IncRoll) RollAngle++;
			if (DecRoll) RollAngle--;

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			var modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref modelview);

			GL.PushMatrix();
				Shapes.Platform();
			GL.PopMatrix();

			GL.PushMatrix();
				GL.Rotate(RollAngle, Vector3.UnitX);
				//	Draw the thrusters
				Thrusters.ForEach(x => x.Draw());
			GL.PopMatrix();

			GL.PushMatrix();
				GL.Rotate(RollAngle, Vector3.UnitX);
				//	Draw the ship

			GL.PopMatrix();

			gw.SwapBuffers();
		}

		public override void OnKeyDown(KeyboardKeyEventArgs keyboardKeyEventArgs, GameWindow game)
		{
			switch (keyboardKeyEventArgs.Key)
			{
				case Key.W:
					On(ForwardThrusters);
					break;
				case Key.S:
					On(ReverseThrusters);
					break;
				case Key.A:
					On(RollLeftThrusters);
					DecRoll = true;
					break;
				case Key.D:
					On(RollRightThrusters);
					IncRoll = true;
					break;
			}
		}

		public override void OnKeyUp(KeyboardKeyEventArgs keyboardKeyEventArgs, GameWindow game)
		{
			switch (keyboardKeyEventArgs.Key)
			{
				case Key.W:
					Off(ForwardThrusters);
					break;
				case Key.S:
					Off(ReverseThrusters);
					break;
				case Key.A:
					Off(RollLeftThrusters);
					DecRoll = false;
					break;
				case Key.D:
					Off(RollRightThrusters);
					IncRoll = false;
					break;
			}
		}

		public override void OnMouseMove(MouseMoveEventArgs mouseMoveEventArgs, GameWindow game)
		{
			var mouseState = Mouse.GetState();
			if (mouseState.RightButton == ButtonState.Pressed)
			{
				base.OnMouseMove(mouseMoveEventArgs, game);
			}
			else
			{
				Off(PitchDownThrusters);
				Off(PitchUpThrusters);

				if (mouseMoveEventArgs.YDelta > 0)
				{
					On(PitchDownThrusters);
				}
				else if (mouseMoveEventArgs.YDelta < 0)
				{
					On(PitchUpThrusters);
				}
			}
		}

		private void On(List<Thruster> thrusters)
		{
			if (thrusters != null && thrusters.Any())
			{
				thrusters.ForEach(x => x.ThrustOn());
			}
		}
		private void Off(List<Thruster> thrusters)
		{
			if (thrusters != null && thrusters.Any())
			{
				thrusters.ForEach(x => x.ThrustOff());
			}
		}

		protected List<Thruster> ForwardThrusters { get; set; }
		protected List<Thruster> ReverseThrusters { get; set; }
		protected List<Thruster> RollLeftThrusters { get; set; }
		protected List<Thruster> RollRightThrusters { get; set; }
		protected List<Thruster> PitchUpThrusters { get; set; }
		protected List<Thruster> PitchDownThrusters { get; set; } 

		private void InitEngines()
		{
			var engine1Position = new Vector3(0.1f, 0, 0.2f);
			var engine2Position = new Vector3(0.1f, 0, -0.2f);
			var engine3Position = new Vector3(0f, -0.05f, 0f);
			var engine4Position = new Vector3(0f, 0.05f, 0f);

			var engine1 = new Thruster(engine1Position, R, this).Forward;
			var engine2 = new Thruster(engine2Position, R, this).Forward;
			var engine3 = new Thruster(engine3Position, R, this).Forward;
			var engine4 = new Thruster(engine4Position, R, this).Forward;

			Thrusters.Add(engine1);
			Thrusters.Add(engine2);
			Thrusters.Add(engine3);
			Thrusters.Add(engine4);

			ForwardThrusters = new List<Thruster>()
			{
				engine1,
				engine2,
				engine3,
				engine4
			};
		}

		private void InitManeuveringThrusters()
		{
			InitReverseThruster();
			InitYawThrusters();
			InitPitchThrusters();
			InitRollThrusters();
		}

		private void InitReverseThruster()
		{
			var thruster01Position = new Vector3(-1.1f, 0, 0.0f); //	Nose, front - slows the ship down
			var thruster01 = new Thruster(thruster01Position, R, this).Reverse;
			thruster01.Set(Maneuvering.ThrusterPosition.Nose | Maneuvering.ThrusterPosition.Front);
			Thrusters.Add(thruster01);
		}

		private void InitYawThrusters()
		{
			InitYawRight();
			InitYawLeft();
		}

		private void InitPitchThrusters()
		{
			InitPitchDown();
			InitPitchUp();
		}

		private void InitRollThrusters()
		{
			InitRollLeft();
			InitRollRight();
		}

		private void InitYawRight()
		{
			var thruster02Position = new Vector3(); //	Nose, left side - points ship right
			var thruster03Position = new Vector3(); //	Wing, right side - points ship right
			var thruster02 = new Thruster(thruster02Position, R, this);
			var thruster03 = new Thruster(thruster03Position, R, this);
			thruster02.Set(Maneuvering.ThrusterPosition.Nose | Maneuvering.ThrusterPosition.Left);
			thruster03.Set(Maneuvering.ThrusterPosition.Rear | Maneuvering.ThrusterPosition.Right);
			Thrusters.Add(thruster02);
			Thrusters.Add(thruster03);
		}

		private void InitYawLeft()
		{
			var thruster04Position = new Vector3(); //	Nose, right side - points ship left
			var thruster05Position = new Vector3(); //	Wing, left side - points ship left
			var thruster04 = new Thruster(thruster04Position, R, this);
			var thruster05 = new Thruster(thruster05Position, R, this);
			thruster04.Set(Maneuvering.ThrusterPosition.Nose | Maneuvering.ThrusterPosition.Right);
			thruster05.Set(Maneuvering.ThrusterPosition.Rear | Maneuvering.ThrusterPosition.Left);
			Thrusters.Add(thruster04);
			Thrusters.Add(thruster05);
		}

		private void InitPitchDown()
		{
			var thruster06Position = new Vector3(); //	Nose, top - points the ship down
			var thruster06 = new Thruster(thruster06Position, R, this);
			thruster06.Set(Maneuvering.ThrusterPosition.Nose | Maneuvering.ThrusterPosition.Top);
			Thrusters.Add(thruster06);

			var thruster07Position = new Vector3(); //	Tail, bottom - points the ship down
			var thruster07 = new Thruster(thruster07Position, R, this);
			thruster07.Set(Maneuvering.ThrusterPosition.Tail | Maneuvering.ThrusterPosition.Bottom);
			Thrusters.Add(thruster07);

			PitchDownThrusters = new List<Thruster>()
			{
				thruster06,
				thruster07
			};
		}

		private void InitPitchUp()
		{
			var thruster08Position = new Vector3(); //	Nose, bottom - points the ship up
			var thruster08 = new Thruster(thruster08Position, R, this);
			thruster08.Set(Maneuvering.ThrusterPosition.Nose | Maneuvering.ThrusterPosition.Bottom);
			Thrusters.Add(thruster08);

			var thruster09Position = new Vector3(); //	Tail, top - points the ship up
			var thruster09 = new Thruster(thruster09Position, R, this);
			thruster09.Set(Maneuvering.ThrusterPosition.Tail | Maneuvering.ThrusterPosition.Top);
			Thrusters.Add(thruster09);

			PitchUpThrusters = new List<Thruster>()
			{
				thruster08,
				thruster09
			};
		}

		private void InitRollLeft()
		{
			var thruster10Position = new Vector3(0.1f, 0, -0.2f); //	Wing, top left - rolls the ship left
			var thruster10 = new Thruster(thruster10Position, R, this);
			thruster10.Set(Vector3.UnitY);
			thruster10.Set(Maneuvering.ThrusterPosition.Rear | Maneuvering.ThrusterPosition.Top |
							Maneuvering.ThrusterPosition.Left);
			Thrusters.Add(thruster10);

			var thruster11Position = new Vector3(0.1f, 0, 0.2f); //	Wing, bottom right - rolls the ship left
			var thruster11 = new Thruster(thruster11Position, R, this);
			thruster11.Set(Vector3.UnitY * -1);
			thruster11.Set(Maneuvering.ThrusterPosition.Rear | Maneuvering.ThrusterPosition.Bottom |
							Maneuvering.ThrusterPosition.Right);
			Thrusters.Add(thruster11);

			RollLeftThrusters = new List<Thruster>()
			{
				thruster10,
				thruster11
			};
		}

		private void InitRollRight()
		{
			var thruster12Position = new Vector3(0.1f, 0, 0.2f); //	Wing, top right - rolls the ship right
			var thruster12 = new Thruster(thruster12Position, R, this);
			thruster12.Set(Vector3.UnitY);
			thruster12.Set(Maneuvering.ThrusterPosition.Rear | Maneuvering.ThrusterPosition.Top |
							Maneuvering.ThrusterPosition.Right);
			Thrusters.Add(thruster12);

			var thruster13Position = new Vector3(0.1f, 0, -0.2f); //	Wing, bottom left - rolls the ship right
			var thruster13 = new Thruster(thruster13Position, R, this);
			thruster13.Set(Vector3.UnitY * -1);
			thruster13.Set(Maneuvering.ThrusterPosition.Rear | Maneuvering.ThrusterPosition.Bottom |
							Maneuvering.ThrusterPosition.Left);
			Thrusters.Add(thruster13);

			RollRightThrusters = new List<Thruster>()
			{
				thruster12,
				thruster13
			};
		}
	}
}
