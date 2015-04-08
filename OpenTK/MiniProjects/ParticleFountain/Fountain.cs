using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Objects;

namespace OpenTK.MiniProjects.ParticleFountain
{
	public class Fountain : GameProject
	{
		private Random R { get; set; }

		public const int NUM_PARTICLES = 100000;

		List<Particle> Particles { get; set; }

		public Fountain()
		{
			R = new Random(DateTime.Now.Millisecond);

			Particles = new List<Particle>(NUM_PARTICLES);

			ClearColor = Color.Black;
		}

		public override void Init()
		{
			Initialized = true;
		}

		public override void OnRenderFrame(FrameEventArgs e, GameWindow gw)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref modelview);

			Particles.RemoveAll(x => x.ParticleExpired);

			if (Particles.Count < Particles.Capacity)
			{
				Particles.Add(new Particle(0, -1, 0, R));
			}

			foreach (var particle in Particles)
			{
				particle.Move();
				particle.Draw();
			}

			Shapes.Platform();

			gw.SwapBuffers();
		}

		public override void OnMouseWheel(MouseWheelEventArgs mouseWheelEventArgs, GameWindow game)
		{
			var currentCapacity = Particles.Capacity;
			var newCapacity = currentCapacity + (10*mouseWheelEventArgs.Delta);

			if (newCapacity <= 0) { return; }

			Particles.Capacity = newCapacity;
		}
	}
}
