
using OpenTK.Graphics;

namespace OpenTK.Objects
{
	public static class Shapes
	{

		public static void DrawSquare(float x, float y, float z, float length)
		{
			GL.Begin(BeginMode.Quads);

			GL.Color3(1.0f, 1.0f, 1.0f);

			GL.Vertex3(x - length, y - length, 4.0f + z);
			GL.Vertex3(x + length, y - length, 4.0f + z);
			GL.Vertex3(x + length, y + length, 4.0f + z);
			GL.Vertex3(x - length, y + length, 4.0f + z);

			GL.End();
		}

		public static void DrawCube(float x, float y, float z, float length)
		{
			var l = length/2;
			GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

			GL.Begin(BeginMode.Triangles);

			GL.Color3(1.0f, 1.0f, 1.0f);

			GL.Vertex3(x - l, y - l, z + 4 - l); GL.Vertex3(x - l, y - l, z + 4 + l); GL.Vertex3(x - l, y + l, z + 4 + l);
			GL.Vertex3(x + l, y + l, z + 4 - l); GL.Vertex3(x - l, y - l, z + 4 - l); GL.Vertex3(x - l, y + l, z + 4 - l);
			GL.Vertex3(x + l, y - l, z + 4 + l); GL.Vertex3(x - l, y - l, z + 4 - l); GL.Vertex3(x + l, y - l, z + 4 - l);
			GL.Vertex3(x + l, y + l, z + 4 - l); GL.Vertex3(x + l, y - l, z + 4 - l); GL.Vertex3(x - l, y - l, z + 4 - l);
			GL.Vertex3(x - l, y - l, z + 4 - l); GL.Vertex3(x - l, y + l, z + 4 + l); GL.Vertex3(x - l, y + l, z + 4 - l);
			GL.Vertex3(x + l, y - l, z + 4 + l); GL.Vertex3(x - l, y - l, z + 4 + l); GL.Vertex3(x - l, y - l, z + 4 - l);
			GL.Vertex3(x - l, y + l, z + 4 + l); GL.Vertex3(x - l, y - l, z + 4 + l); GL.Vertex3(x + l, y - l, z + 4 + l);
			GL.Vertex3(x + l, y + l, z + 4 + l); GL.Vertex3(x + l, y - l, z + 4 - l); GL.Vertex3(x + l, y + l, z + 4 - l);
			GL.Vertex3(x + l, y - l, z + 4 - l); GL.Vertex3(x + l, y + l, z + 4 + l); GL.Vertex3(x + l, y - l, z + 4 + l);
			GL.Vertex3(x + l, y + l, z + 4 + l); GL.Vertex3(x + l, y + l, z + 4 - l); GL.Vertex3(x - l, y + l, z + 4 - l);
			GL.Vertex3(x + l, y + l, z + 4 + l); GL.Vertex3(x - l, y + l, z + 4 - l); GL.Vertex3(x - l, y + l, z + 4 + l);
			GL.Vertex3(x + l, y + l, z + 4 + l); GL.Vertex3(x - l, y + l, z + 4 + l); GL.Vertex3(x + l, y - l, z + 4 + l);

			GL.End();
		}

	}
}
