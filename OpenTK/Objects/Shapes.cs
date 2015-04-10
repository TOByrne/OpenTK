using OpenTK.Graphics;

namespace OpenTK.Objects
{
	public static class Shapes
	{

		public static void DrawSquare(float x, float y, float z, float length)
		{
			GL.Begin(BeginMode.Quads);

			GL.Color3(1.0f, 1.0f, 1.0f);

			GL.Vertex3(x - length, y - length, z);
			GL.Vertex3(x + length, y - length, z);
			GL.Vertex3(x + length, y + length, z);
			GL.Vertex3(x - length, y + length, z);

			GL.End();
		}

		public static void DrawCube(float x, float y, float z, float length, float r = 1.0f, float g = 1.0f, float b = 1.0f)
		{
			var l = length/2;
			GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

			GL.Begin(BeginMode.Triangles);

			GL.Color3(r, g, b);

			GL.Vertex3(x - l, y - l, z - l); GL.Vertex3(x - l, y - l, z + l); GL.Vertex3(x - l, y + l, z + l);
			GL.Vertex3(x + l, y + l, z - l); GL.Vertex3(x - l, y - l, z - l); GL.Vertex3(x - l, y + l, z - l);
			GL.Vertex3(x + l, y - l, z + l); GL.Vertex3(x - l, y - l, z - l); GL.Vertex3(x + l, y - l, z - l);
			GL.Vertex3(x + l, y + l, z - l); GL.Vertex3(x + l, y - l, z - l); GL.Vertex3(x - l, y - l, z - l);
			GL.Vertex3(x - l, y - l, z - l); GL.Vertex3(x - l, y + l, z + l); GL.Vertex3(x - l, y + l, z - l);
			GL.Vertex3(x + l, y - l, z + l); GL.Vertex3(x - l, y - l, z + l); GL.Vertex3(x - l, y - l, z - l);
			GL.Vertex3(x - l, y + l, z + l); GL.Vertex3(x - l, y - l, z + l); GL.Vertex3(x + l, y - l, z + l);
			GL.Vertex3(x + l, y + l, z + l); GL.Vertex3(x + l, y - l, z - l); GL.Vertex3(x + l, y + l, z - l);
			GL.Vertex3(x + l, y - l, z - l); GL.Vertex3(x + l, y + l, z + l); GL.Vertex3(x + l, y - l, z + l);
			GL.Vertex3(x + l, y + l, z + l); GL.Vertex3(x + l, y + l, z - l); GL.Vertex3(x - l, y + l, z - l);
			GL.Vertex3(x + l, y + l, z + l); GL.Vertex3(x - l, y + l, z - l); GL.Vertex3(x - l, y + l, z + l);
			GL.Vertex3(x + l, y + l, z + l); GL.Vertex3(x - l, y + l, z + l); GL.Vertex3(x + l, y - l, z + l);

			GL.End();
		}

		public static void Platform()
		{
			GL.Begin(BeginMode.Lines);

			float
				r = 0.1f,
				g = 0.1f,
				b = 0.1f;

			for (float z = -10; z <= 10; z += 0.2f)
			{
				if (z > -0.1 && z < 0.1)
					GL.Color3(0.3, 0, 0);
				else
					GL.Color3(r, g, b);

				GL.Vertex3(-10.0f, -1.0f, z);
				GL.Vertex3(+10.0f, -1.0f, z);
			}

			for (float x = -10; x <= 10; x += 0.2f)
			{
				if (x > -0.1 && x < 0.1)
					GL.Color3(0, 0.3, 0);
				else
					GL.Color3(r, g, b);

				GL.Vertex3(x, -1.0f, -10.0f);
				GL.Vertex3(x, -1.0f, +10.0f);
			}

			//Console.WriteLine(r + " " + g + " " + b);

			GL.End();
		}
	}
}
