using System;
using System.Collections.Generic;
using OpenTK.Graphics;
using OpenTK.Objects;

namespace OpenTK.MiniProjects.SphereCube
{
	public class Sphere : GameProject
	{
		private List<float> x, y, z;
		private const float RADIUS = 1;
		private const int FULL_CIRCLE = 360;
		private const int NUM_POINTS = 20;
		private const float POINT_SQUARE_SIZE = 0.01f;

		private const float CIRCLE_ROTATE_SPEED = 0.5f;
		private float CIRCLE_ANGLE = 0.0f;

		public Sphere()
		{
			x = new List<float>();
			y = new List<float>();
			z = new List<float>();
		}

		public override void Init()
		{
			CreatePointsTable();

			Initialized = true;
		}

		void CreatePointsTable()
		{
			const double baseCalc = System.Math.PI * FULL_CIRCLE / NUM_POINTS / 180;

			for (var i = 0; i < NUM_POINTS; i++)
			{
				var a = baseCalc * i;

				for (var t = 0; t < NUM_POINTS; t++)
				{
					var b = baseCalc * t;

					x.Add(RADIUS * (float)(System.Math.Cos(a) * System.Math.Sin(b)));
					y.Add(RADIUS * (float)(System.Math.Sin(a) * System.Math.Sin(b)));
					z.Add(RADIUS * (float)(System.Math.Cos(b)));
				}
			}
		}

		public override void OnRenderFrame(FrameEventArgs e, GameWindow gw)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref modelview);

			CIRCLE_ANGLE += CIRCLE_ROTATE_SPEED;

			GL.Translate(0.0f, 0.0f, 4.0f);
			GL.Rotate(CIRCLE_ANGLE, new Vector3(0.0f, 0.1f, 0.1f));
			GL.Translate(0.0f, 0.0f, -4.0f);

			for (int i = 0; i < x.Count; i++)
			{
				Shapes.DrawCube(x[i], y[i], z[i], POINT_SQUARE_SIZE);
			}

			gw.SwapBuffers();
		}
	}
}
