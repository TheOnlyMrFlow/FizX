using System.Drawing;
using FizX.Core.Graphics;
using FizX.Core.Worlds;
using FizX.Renderer;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FizX.OpenTK;

public class OpenTkRenderer : IRenderer
{
    private readonly ConsoleRenderer _consoleRenderer = new();
    private readonly GameWindow _window;
    
    private readonly float[] _vertices =
    {
        -0.5f, -0.5f, 
        0.5f, -0.5f,
        0.5f,  0.5f,
        -0.5f, 0.5f
    };
    
    private readonly uint[] _verticesToDraw =
    {
        0, 1, 2, 2, 3, 0
    };
    
    private int _vertexBufferObject;

    private int _vertexArrayObject;
    
    private int _indexBufferObject;
    
    private Shader _shader;
    
    public OpenTkRenderer(GameWindow window)
    {
        _window = window;
    }

    public void Load()
    {
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        
        _vertexBufferObject = GL.GenBuffer();
       
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
        
        _vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(_vertexArrayObject);
        
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        _indexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indexBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, _verticesToDraw.Length * sizeof(uint), _verticesToDraw, BufferUsageHint.StaticDraw);
        
        
        _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");

        // Now, enable the shader.
        // Just like the VBO, this is global, so every function that uses a shader will modify this one until a new one is bound instead.
        _shader.Use();
    }

    public void Render(World world)
    {
        _consoleRenderer.Render(world);

        var actor1 = world.Actors.ElementAt(1); 
        _window.Title = world.Actors.ElementAt(1).Position.X.ToString();

        GL.Clear(ClearBufferMask.ColorBufferBit);
        
        _shader.Use();
        
        _vertices[0] = actor1.Position.X / 10000;
        _vertices[1] = actor1.Position.Y / 10000;
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.DynamicDraw); 
        
        //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, default);
        
        _window.SwapBuffers();
    }

    public void OnUpdateFrame(FrameEventArgs e)
    {
        var input = _window.KeyboardState;

        if (input.IsKeyDown(Keys.Escape))
        {
            _window.Close();
        }
    }

    public void OnResize(ResizeEventArgs e) 
    {
        GL.Viewport(0, 0, _window.Size.X, _window.Size.Y);
    }
    
    public void OnUnload()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindVertexArray(0);
        GL.UseProgram(0);
        
        GL.DeleteBuffer(_vertexBufferObject);
        GL.DeleteVertexArray(_vertexArrayObject);

        GL.DeleteProgram(_shader.Handle);
    }
}