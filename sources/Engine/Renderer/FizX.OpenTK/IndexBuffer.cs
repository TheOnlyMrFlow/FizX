using OpenTK.Graphics.OpenGL4;

namespace FizX.OpenTK;

public class IndexBuffer : IDisposable
{
    private int _rendererId;
    public int Count { get; private set; }

    public IndexBuffer(uint[] data)
    {
        _rendererId = GL.GenBuffer();
        Count = data.Length;
        
        Bind();
        GL.BufferData(BufferTarget.ElementArrayBuffer, Count * sizeof(uint), data, BufferUsageHint.StaticDraw);
    }
    

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _rendererId);
    }

    public void Unbind()
    {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
    }

    public void Dispose()
    {
        GL.DeleteBuffers(1, ref _rendererId);
    }
}