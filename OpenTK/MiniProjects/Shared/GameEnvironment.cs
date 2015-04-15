using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;

namespace OpenTK.MiniProjects.Shared
{
	public class GameEnvironment
	{
		List<IDrawable> WorldObjects { get; set; }

		public GameEnvironment()
		{
			WorldObjects = new List<IDrawable>();
		}

		public void Render()
		{
			if (WorldObjects != null)
			{
				WorldObjects.RemoveAll(o => o.Dead);
				WorldObjects.ForEach(o => o.Draw());
			}
		}

		public void AddWorldObject(IDrawable worldObject)
		{
			WorldObjects.Add(worldObject);
		}

		public void Update()
		{
		}
	}
}
