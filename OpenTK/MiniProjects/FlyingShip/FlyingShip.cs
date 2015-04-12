using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;
using OpenTK.Objects;

namespace OpenTK.MiniProjects.FlyingShip
{
	public class FlyingShip : GameProject
	{
		int YawAngle { get; set; }
		int RollAngle { get; set; }
		int PitchAngle { get; set; }

		List<Particle> Particles { get; set; }

		Random R { get; set; }

		public FlyingShip()
		{
			YawAngle = 0;
			RollAngle = 0;
			PitchAngle = 0;

			ClearColor = Color.Black;
		}

		public override void Init()
		{
			Particles = new List<Particle>();
			R = new Random(DateTime.Now.Millisecond);
		}

		public override void OnRenderFrame(FrameEventArgs e, GameWindow gw)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			var modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref modelview);

			//	Implement simple particle fountain

			Particles.RemoveAll(x => x.Dead);

			var particleVelocity = new Vector3(0, 0.2f, 0);

			var rollRadians = (float)(RollAngle * System.Math.PI / 180);
			var pitchRadians = (float)(PitchAngle * System.Math.PI / 180);

			particleVelocity = Vector3.Transform(particleVelocity, Matrix4.CreateRotationX(rollRadians) * Matrix4.CreateRotationZ(pitchRadians));

			Particles.Add(new Particle(particleVelocity, R));

			GL.PushMatrix();
				Shapes.Platform();
				GL.Rotate(RollAngle++, new Vector3(1, 0, 1));

				//	represents the engine/thruster.
				Shapes.DrawCube(0, 0, 0, 0.5f);
			GL.PopMatrix();

			Particles.ForEach(x=>x.Draw());

			gw.SwapBuffers();
		}
	}
}
