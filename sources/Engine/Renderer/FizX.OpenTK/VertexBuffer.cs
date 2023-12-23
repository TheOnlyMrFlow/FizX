using OpenTK.Graphics.OpenGL4;

namespace FizX.OpenTK;

public abstract class VertexBuffer : IDisposable
{
    private int _rendererId;

    public VertexBuffer()
    {
        _rendererId = GL.GenBuffer();
    }

    public static void BufferData<T>(T[] data, int size) where T : struct
    {
        GL.BufferData(BufferTarget.ArrayBuffer, size, data, BufferUsageHint.StaticDraw);
    }

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, _rendererId);
    }

    public void Unbind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }

    public void Dispose()
    {
        GL.DeleteBuffers(1, ref _rendererId);
    }
}

public class VertexBuffer<T> : VertexBuffer where T : struct
{
    public VertexBuffer() : base()
    {
        
    }
    
    public VertexBuffer(T[] data, int size) : base()
    {
        BufferData(data, size);
    }

    public void BufferData(T[] data, int size)
    {
        VertexBuffer.BufferData(data, size);
    }
}