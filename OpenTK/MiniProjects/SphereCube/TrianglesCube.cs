using OpenTK.Graphics;
using OpenTK.Objects;

namespace OpenTK.MiniProjects.SphereCube
{
	public class TrianglesCube : GameProject
	{
		public const float LENGTH = 1f;

		public float CIRCLE_ROTATE_SPEED = 0.0f;
		public float CIRCLE_ANGLE = 0;

		public override void Init()
		{
			Initialized = true;
		}

		public override void OnRenderFrame(FrameEventArgs e, GameWindow gw)
		{
			//GL.Enable(EnableCap.DepthTest);
			//GL.DepthFunc(DepthFunction.Less);

			//GL.Fog(FogParameter.FogColor, new []{ 0.5f, 0.5f, 0.5f, 1.0f});
			//GL.Fog(FogParameter.FogMode, (int)FogMode.Exp);
			//GL.Fog(FogParameter.FogDensity, 0.35f);

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			var modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref modelview);

			CIRCLE_ANGLE += CIRCLE_ROTATE_SPEED;
			if (CIRCLE_ANGLE > 360) { CIRCLE_ANGLE -= 360; }
			else if (CIRCLE_ANGLE < 0) { CIRCLE_ANGLE += 360; }

			GL.Rotate(CIRCLE_ANGLE, new Vector3(0.0f, 0.1f, 0.1f));

			GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

			Shapes.DrawCube(0, 0, 0, LENGTH);

			gw.SwapBuffers();
		}

		public override void OnMouseWheel(Input.MouseWheelEventArgs mouseWheelEventArgs, GameWindow game)
		{
			CIRCLE_ROTATE_SPEED += mouseWheelEventArgs.Delta;
		}
	}
}
