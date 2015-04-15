namespace OpenTK.MiniProjects.Shared
{
	public interface IDrawable
	{
		Vector3 Position { get; set; }
		Vector3 Velocity { get; set; }
		bool Dead { get; }

		void Draw();
		void UpdateFrame();
	}
}
