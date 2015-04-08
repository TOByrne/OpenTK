using System.Linq;
using System;
using OpenTK.Input;
using OpenTK.MiniProjects;
using OpenTK.MiniProjects.ParticleFountain;

//	http://choorucode.com/2013/06/07/how-to-get-started-with-opentk-using-c-and-visual-studio/

namespace OpenTK
{
	class Game : GameWindow
	{
		static readonly GameProject Project = new Fountain();

		public Game Instance { get { return this; } }

		public Game()
			: base(Project.WIDTH, Project.HEIGHT, Project.MODE, Project.TITLE)
		{
			VSync = VSyncMode.On;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			Project.OnLoad(e);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			Project.OnResize(e, this);
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);

			Project.OnUpdateFrame(e, this);
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			base.OnRenderFrame(e);

			Project.OnRenderFrame(e, this);
		}

		protected override void OnMouseWheel(MouseWheelEventArgs e)
		{
			Project.OnMouseWheel(e, this);
		}

		protected override void OnMouseMove(MouseMoveEventArgs e)
		{
			Project.OnMouseMove(e, this);
		}

		[STAThread]
		static void Main()
		{
			DeviceInfo();

			if (!Project.Initialized)
			{
				Project.Init();
			}

			using (var game = new Game())
			{
				game.Run(30.0);
			}
		}

		private static void DeviceInfo()
		{
			var devices = Enum.GetValues(typeof (DisplayIndex))
				.Cast<DisplayIndex>()
				.Select(DisplayDevice.GetDisplay)
				.Where(device => device != null).ToList();

			Console.WriteLine(string.Format("There {0} {1} device{2}.",
				devices.Count > 1 ? "are" : "is",
				devices.Count,
				devices.Count > 1 ? "s" : ""));

			foreach (var device in devices)
			{
				Console.WriteLine(device.IsPrimary);
				Console.WriteLine(device.Bounds);
				Console.WriteLine(device.RefreshRate);
				Console.WriteLine(device.BitsPerPixel);

				foreach (var res in device.AvailableResolutions)
				{
					Console.WriteLine(res);
				}
			}
		}
	}
}
