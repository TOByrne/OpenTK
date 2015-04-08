using System.Collections.Generic;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Objects;

namespace OpenTK.MiniProjects.SphereCube
{
	//	http://gamedev.stackexchange.com/questions/43741/how-do-you-turn-a-cube-into-a-sphere
	public class SphereCube : GameProject
	{
		private List<float> x, y, z;
		private const float RADIUS = 1;
		private const int FULL_CIRCLE = 360;
		private const int NUM_CUBE_POINTS = 20;
		private const int NUM_SPHERE_POINTS = 50;
		private const float POINT_SQUARE_SIZE = 0.01f;
		private const float SIDE_LENGTH = 2;

		private const float CIRCLE_ROTATE_SPEED = 0.5f;
		private float CIRCLE_ANGLE = 0.0f;

		public bool Transform { get; set; }

		public SphereCube()
		{
			Transform = false;

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
			CreateSpherePoints();
			CreateCubePoints();
		}

		private void CreateCubePoints()
		{
			const float incAmt = SIDE_LENGTH / NUM_CUBE_POINTS;

			//	Do this for every side.

			const float min = SIDE_LENGTH/-2;
			const float max = SIDE_LENGTH/2;

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

		private void CreateSpherePoints()
		{
			const double baseCalc = System.Math.PI * FULL_CIRCLE / NUM_SPHERE_POINTS / 180;

			for (var i = 0; i < NUM_SPHERE_POINTS; i++)
			{
				var a = baseCalc*i;

				for (var t = 0; t < NUM_SPHERE_POINTS; t++)
				{
					var b = baseCalc*t;

					x.Add(RADIUS*(float) (System.Math.Cos(a)*System.Math.Sin(b)));
					y.Add(RADIUS*(float) (System.Math.Sin(a)*System.Math.Sin(b)));
					z.Add(RADIUS*(float) (System.Math.Cos(b)));
				}
			}
		}

		public override void OnRenderFrame(FrameEventArgs e, GameWindow gw)
		{
			gw.KeyDown += TransformerKeyboard;

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			var modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref modelview);

			CIRCLE_ANGLE += CIRCLE_ROTATE_SPEED;

			GL.Rotate(CIRCLE_ANGLE, new Vector3(0.0f, 0.1f, 0.1f));

			for (var i = 0; i < x.Count; i++)
			{
				if (Transform)
				{
					CalculateDeltas(i);
				}

				Shapes.DrawSquare(x[i], y[i], z[i], POINT_SQUARE_SIZE);
			}

			gw.SwapBuffers();
		}

		private void TransformerKeyboard(object sender, KeyboardKeyEventArgs keyboardKeyEventArgs)
		{
			switch (keyboardKeyEventArgs.Key)
			{
				case Key.Space:
					Transform = !Transform;
					break;
			}
		}

		void CalculateDeltas(int i)
		{
			var x = this.x[i];
			var y = this.y[i];
			var z = this.z[i];

			float hsx = HalfSquare(x), hsy = HalfSquare(y), hsz = HalfSquare(z);
			float tsyz = ThirdSquareOthers(y, z), tszx = ThirdSquareOthers(z, x), tsxy = ThirdSquareOthers(x, y);

			var dx = x * (float)(System.Math.Sqrt(1.0f - hsy - hsz + tsyz));
			var dy = y * (float)(System.Math.Sqrt(1.0f - hsz - hsz + tszx));
			var dz = z * (float)(System.Math.Sqrt(1.0f - hsx - hsy + tsxy));

			this.x[i] = dx;
			this.y[i] = dy;
			this.z[i] = dz;
		}

		static float HalfSquare(float sq) { return (sq*sq)/2; }
		static float ThirdSquareOthers(float sq1, float sq2) { return (sq1*sq1*sq2*sq2)/3; }
	}
}
