using System.Drawing;
using System.Numerics;
using FizX.Core.Graphics;
using FizX.Core.Timing;
using FizX.Core.Worlds;
using FizX.Renderer;
using ImGuiNET;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Vector4 = OpenTK.Mathematics.Vector4;

namespace FizX.OpenTK;

public class OpenTkRenderingEngine : IRenderingEngine
{
    private readonly ConsoleRenderingEngine _consoleRenderingEngine = new();
    private readonly GameWindow _window;
    
    private readonly ImGuiController _imgUicontroller;
    
    private readonly float[] _vertices =
    {
        -50f, -50f, 0.0f, 0.0f, 
        50f, -50f, 1.0f, 0.0f,
        50f,  50f, 1.0f, 1.0f,
        -50f, 50f, 0.0f, 1.0f
    };
    
    private readonly uint[] _verticesToDraw =
    {
        0, 1, 2, 2, 3, 0
    };
    
    private VertexBuffer<float> _vertexBuffer;

    private VertexArray _vertexArray;
    
    private IndexBuffer _indexBuffer;
    
    private Shader _shader;

    private Renderer _renderer = new Renderer();

    private Dictionary<int, Texture> _sprites = new ();
    
    public OpenTkRenderingEngine(GameWindow window)
    {
        _window = window;
        _imgUicontroller = new ImGuiController(window.ClientSize.X, window.ClientSize.Y);
    }

    public void Load()
    {
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        GL.Enable(EnableCap.Blend);

        _vertexBuffer = new VertexBuffer<float>();
        _vertexBuffer.Bind();
        VertexBuffer.BufferData(_vertices, _vertices.Length * sizeof(float));

        _vertexArray = new VertexArray();

        var layout = new VertexBufferLayout();
        layout.PushFloat(2);
        layout.PushFloat(2);
        
        _vertexArray.AddBuffer(_vertexBuffer, layout);

        _indexBuffer = new IndexBuffer(_verticesToDraw);

        
        _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
        
        _shader.Use();
        
        var texture = new Texture("Resources/cat.png");
        texture.Use(0);
        
        _shader.SetUniformInt("u_Texture", 0);
        
    }

    public void RenderWorld(World world)
    {
        _consoleRenderingEngine.RenderWorld(world);

        //var actor1 = world.Actors.ElementAt(1); 
        //_window.Title = world.Actors.ElementAt(1).Position.X.ToString();

        _renderer.Clear();
        
        _shader.Use();
        _shader.SetUniformVec4("u_Color", new Vector4(0.5f, 1, 1, 1));
        
        var proj = Matrix4.CreateOrthographicOffCenter(0, 960, 0, 540f, -1f, 1f);
        var view = Matrix4.CreateTranslation(0, 0, 0);
        
        foreach (var actor in world.Actors)
        {
            var spriteRenderers = actor.GetComponents<SpriteRendererComponent>();
            foreach (var spriteRendererComponent in spriteRenderers)
            {
                if (!_sprites.TryGetValue(spriteRendererComponent.Id, out var texture))
                {
                    texture = new Texture(spriteRendererComponent.TextureFilePath);
                    _sprites.Add(spriteRendererComponent.Id, texture);
                }
                texture.Use(0);
                _shader.SetUniformInt("u_Texture", 0);
                var transform = actor.Transform;
                var model = Matrix4.CreateTranslation(transform.Position.X, transform.Position.Y, 0);
                var mvp = model * view * proj;
                _shader.SetUniformMatrix4("u_MVP", mvp);
                _renderer.Draw(ref _vertexArray, ref _indexBuffer, ref _shader);
                
            }
        }
        
        // _vertices[0] = actor1.Position.X / 10000;
        // _vertices[1] = actor1.Position.Y / 10000;
        // GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.DynamicDraw);
        
        //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        
        
        //ImGui.DockSpaceOverViewport();

        //ImGui.ShowDemoWindow();
        
        _imgUicontroller.Update(_window, (float)0.16f);

        ImGui.Text("Hello world");
        foreach (var timeline in Time.GetAllTimeLines().Take(2))
        {
            ImGui.Text($"Timeline {timeline.Index}");
            var isRecording = timeline.IsRecording;
            var isRewinding = timeline.IsRewinding;
            ImGui.Checkbox($"Recording", ref isRecording);
            ImGui.Checkbox($"Rewinding", ref isRewinding);
        }
        _imgUicontroller.Render();
        
        ImGuiController.CheckGLError("End of frame");
        
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
        _imgUicontroller.WindowResized(_window.ClientSize.X, _window.ClientSize.Y);

    }
    
    public void OnUnload()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindVertexArray(0);
        GL.UseProgram(0);
        
        _indexBuffer.Dispose();
        _vertexBuffer.Dispose();
        _vertexArray.Dispose();

        GL.DeleteProgram(_shader._rendererId);
    }
}