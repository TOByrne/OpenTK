using System.Collections.Generic;
using OpenTK.Graphics;
using OpenTK.Objects;

namespace OpenTK.MiniProjects.SphereCube
{
	public class Cube : GameProject
	{
		private List<float> x, y, z;
		private const float SIDE_LENGTH = 1;
		private const int NUM_POINTS = 10;
		private const float POINT_SQUARE_SIZE = 0.01f;

		private const float CUBE_ROTATE_SPEED = 0.5f;
		private float CUBE_ANGLE = 0.0f;

		bool Initialized { get; set; }

		public Cube()
		{
			//sin = new float[NUM_POINTS];
			//cos = new float[NUM_POINTS];

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
			const float incAmt = SIDE_LENGTH/NUM_POINTS;

			//	Do this for every side.

			const float min = SIDE_LENGTH / -2;
			const float max = SIDE_LENGTH / 2;

			for (var i = min; i < max; i += incAmt)
			{
				for (var j = min; j < max; j += incAmt)
				{
					for (var k = min; k < max; k += incAmt)
					{
						if (i == min || j == min || k == min || i >= (max - incAmt)
							|| j >= (max - incAmt) || k >= (max - incAmt))
						{
							x.Add(i);
							y.Add(j);
							z.Add(k);
						}
					}
				}
			}
		}

		public override void OnRenderFrame(FrameEventArgs e, GameWindow gw)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			var modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref modelview);

			CUBE_ANGLE += CUBE_ROTATE_SPEED;

			GL.Rotate(CUBE_ANGLE, new Vector3(0.0f, 0.1f, 0.1f));

			for (var i = 0; i < x.Count; i++)
			{
				Shapes.DrawSquare(x[i], y[i], z[i], POINT_SQUARE_SIZE);
			}

			gw.SwapBuffers();
		}
	}
}
