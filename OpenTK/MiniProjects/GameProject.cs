using OpenTK.Graphics;
using System;
using OpenTK.Input;

namespace OpenTK.MiniProjects
{
	public abstract class GameProject
	{
		public virtual int WIDTH { get { return 800; } }
		public virtual int HEIGHT { get { return 600; } }
		public virtual GraphicsMode MODE { get { return GraphicsMode.Default; } }
		public virtual string TITLE { get { return "OpenTK Quick Start Sample"; } }

		public virtual bool Initialized { get; set; }

		public abstract void Init();

		protected GameProject()
		{
			Initialized = false;
		}

		public virtual void OnLoad(EventArgs e)
		{
			GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f);
			GL.Enable(EnableCap.DepthTest);
		}

		public virtual void OnResize(EventArgs e, GameWindow gw)
		{
			GL.Viewport(gw.ClientRectangle.X, gw.ClientRectangle.Y, gw.ClientRectangle.Width, gw.ClientRectangle.Height);

			Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)System.Math.PI / 4, gw.Width / (float)gw.Height, 1.0f, 64.0f);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref projection);
		}

		public virtual void OnUpdateFrame(FrameEventArgs e, GameWindow gw)
		{
			if (gw.Keyboard[Key.Escape])
				gw.Exit();
		}
		public abstract void OnRenderFrame(FrameEventArgs e, GameWindow gw);

	}
}
