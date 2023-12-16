using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FizX.Renderer.OpenTK;

public class FizXGameWindow : GameWindow
{
    // Create the vertices for our triangle. These are listed in normalized device coordinates (NDC)
    // In NDC, (0, 0) is the center of the screen.
    // Negative X coordinates move to the left, positive X move to the right.
    // Negative Y coordinates move to the bottom, positive Y move to the top.
    // OpenGL only supports rendering in 3D, so to create a flat triangle, the Z coordinate will be kept as 0.
    private readonly float[] _vertices =
    {
        -0.5f, -0.5f, 0.0f, // Bottom-left vertex
        0.5f, -0.5f, 0.0f, // Bottom-right vertex
        0.0f,  0.5f, 0.0f  // Top vertex
    };

    // These are the handles to OpenGL objects. A handle is an integer representing where the object lives on the
    // graphics card. Consider them sort of like a pointer; we can't do anything with them directly, but we can
    // send them to OpenGL functions that need them.

    // What these objects are will be explained in OnLoad.
    private int _vertexBufferObject;

    private int _vertexArrayObject;

    // This class is a wrapper around a shader, which helps us manage it.
    // The shader class's code is in the Common project.
    // What shaders are and what they're used for will be explained later in this tutorial.
    private Shader _shader;

    
    // A simple constructor to let us set properties like window size, title, FPS, etc. on the window.
    public FizXGameWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {
    }

    // This function runs on every update frame.
    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        // Check if the Escape button is currently being pressed.
        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            // If it is, close the window.
            Close();
        }

        base.OnUpdateFrame(e);
    }
}