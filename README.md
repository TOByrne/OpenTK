# OpenTK

## Getting Started
You'll need to download OpenTK before any of this will work.  You can download from the OpenTK site here: http://www.opentk.com/

Once you've installed the ToolKit, you can either repoint the OpenTK references in the solution or, as I did, copy the dlls into an easier-to-remember directory: 
C:\Projects\Libraries\OpenTK\*

## Examples
I've got a few examples of some simple stuff.  Check them out just by changing the static readonly GameProject property to something else that inherits from GameProject.  For example:

```csharp
namespace OpenTK
{
	class Game : GameWindow
	{
		static readonly GameProject Project = new SphereCube();
```

may become

```csharp
namespace OpenTK
{
	class Game : GameWindow
	{
		static readonly GameProject Project = new Cube();
```

## In Progress
SphereCube.cs is supposed to start at either a sphere or a cube and morph into the other.  The function, `void CalculateDeltas(int i)` was inspired by [something on StackExchange](http://gamedev.stackexchange.com/questions/43741/how-do-you-turn-a-cube-into-a-sphere).  It currently does something, but not the something I'd like it to do.
