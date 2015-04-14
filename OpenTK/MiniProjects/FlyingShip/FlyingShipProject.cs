using System.Drawing;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.MiniProjects.Shared;
using System;
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
			Ship = new Ship(new Vector3(0, 0, 0), new Vector3(0, 0, -0.01f), Environment, R);
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
