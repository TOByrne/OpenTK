using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;
using OpenTK.Objects;

namespace OpenTK.MiniProjects.MultipeShapes
{
	public class MultipleCubes : GameProject
	{
		private int CubeAngle { get; set; }
		private int RotateSpeed { get; set; }

		public MultipleCubes()
		{
			CubeAngle = 0;
			RotateSpeed = 1;

			ClearColor = Color.Black;
		}

		public override void Init()
		{
			Initialized = true;
		}

		public override void OnRenderFrame(FrameEventArgs e, GameWindow gw)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			var modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref modelview);

			//	This is just for the "Ground"
			GL.PushMatrix();
				GL.Translate(Vector3.Zero);
				Shapes.Platform();
			GL.PopMatrix();

			//	Method 1
			RotateTheWrongWay();

			//	Method 2
			RotateTheCorrectWay();

			gw.SwapBuffers();

			CubeAngle += RotateSpeed;
			if (CubeAngle > 360) { CubeAngle -= 360; }
			else if (CubeAngle < 0) { CubeAngle += 360; }
		}

		private void RotateTheWrongWay()
		{
			//	Forget about this for now - I'm using it just to demonstrate
			//	the differences between the right way and the wrong way.
			GL.PushMatrix();

				//	These next few lines are what someone new to this might
				//	typically try to do.

				//	Rotate both cubes around the origin
				GL.Rotate(CubeAngle, Vector3.UnitZ);

				//	Draw cubes at their specified positions.
				Shapes.DrawCube(2.5f, 0f, 0f, 1f);
				Shapes.DrawCube(-2.5f, 0f, 0f, 1f);

				//	But how to rotate the cubes independently of each other?
				//	Check out the right way.

			//	Forget about this for now - I'm using it just to demonstrate
			//	the differences between the right way and the wrong way.
			GL.PopMatrix();
		}

		private void RotateTheCorrectWay()
		{
			//	Save the drawing matrix
			//	It's this PushMatrix and PopMatrix below that are the magic.
			//	Once we Push the matrix
			GL.PushMatrix();
				//	We move the origin to the intended position of the object
				GL.Translate(new Vector3(1f, 0, 0));

				//	we then rotate the local coordinates
				GL.Rotate(CubeAngle, Vector3.UnitY);

				//	and finally draw the object
				Shapes.DrawCube(0, 0, 0, 1);
			//	Restore the drawing matrix
			GL.PopMatrix();

			//	Save the drawing matrix
			GL.PushMatrix();
				//	Move the origin to the position of the object
				GL.Translate(new Vector3(-1f, 0, 0));

				//	Rotate the local coordinates (and twice as fast as the previous cube.)
				GL.Rotate(CubeAngle * 2, Vector3.UnitX);

				//	Draw the object
				Shapes.DrawCube(0, 0, 0, 1);
			//	Restore the drawing matrix
			GL.PopMatrix();
		}
	}
}
