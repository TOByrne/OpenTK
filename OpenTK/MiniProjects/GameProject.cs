using System.Drawing;
using OpenTK.Graphics;
using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using EnableCap = OpenTK.Graphics.EnableCap;
using GL = OpenTK.Graphics.GL;
using MatrixMode = OpenTK.Graphics.MatrixMode;

namespace OpenTK.MiniProjects
{
	public abstract class GameProject
	{
		public virtual int WIDTH { get { return 800; } }
		public virtual int HEIGHT { get { return 600; } }
		public virtual GraphicsMode MODE { get { return GraphicsMode.Default; } }
		public virtual string TITLE { get { return "OpenTK Quick Start Sample"; } }

		public GameWindow Window { get; set; }

		public Color ClearColor { get; set; }

		public virtual bool Initialized { get; set; }

		public abstract void Init();

		GameCamera Camera { get; set; }

		protected GameProject()
		{
			Camera = new GameCamera();
			ClearColor = Color.DarkBlue;

			Initialized = false;
		}

		public virtual void OnLoad(EventArgs e)
		{
			GL.ClearColor(ClearColor);
			GL.Enable(EnableCap.DepthTest);
		}

		public virtual void OnResize(EventArgs e, GameWindow gw)
		{
			GL.Viewport(gw.ClientRectangle.X, gw.ClientRectangle.Y, gw.ClientRectangle.Width, gw.ClientRectangle.Height);

			SetView(gw);
		}

		private void SetView(GameWindow gw)
		{
			//Matrix4 projection1 = Matrix4.CreatePerspectiveFieldOfView(
			//	(float) System.Math.PI/4,
			//	gw.Width/(float) gw.Height,
			//	1.0f,
			//	64.0f
			//	);

			var projection = Camera.GetViewMatrix() *
				Matrix4.CreatePerspectiveFieldOfView(
					(float) System.Math.PI/4,
					gw.Width/(float) gw.Height,
					1.0f,
					64.0f
				);

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref projection);
		}

		public virtual void OnUpdateFrame(FrameEventArgs e, GameWindow gw)
		{
			if (gw.Keyboard[Key.Escape])
				gw.Exit();
		}
		public abstract void OnRenderFrame(FrameEventArgs e, GameWindow gw);

		public virtual void OnMouseWheel(MouseWheelEventArgs mouseWheelEventArgs, GameWindow game)
		{
		}

		public void OnMouseMove(MouseMoveEventArgs mouseMoveEventArgs, GameWindow game)
		{
			if (mouseMoveEventArgs.XDelta > 0)
			{
				Camera.LookRight();
				SetView(game);
			}

			if (mouseMoveEventArgs.XDelta < 0)
			{
				Camera.LookLeft();
				SetView(game);
			}

			if (mouseMoveEventArgs.YDelta > 0)
			{
				Camera.LookUp();
				SetView(game);
			}

			if (mouseMoveEventArgs.YDelta < 0)
			{
				Camera.LookDown();
				SetView(game);
			}

			//Console.WriteLine(Camera);
		}
	}
}
