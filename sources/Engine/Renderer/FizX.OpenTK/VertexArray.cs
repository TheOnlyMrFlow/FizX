using OpenTK.Audio.OpenAL;
using OpenTK.Graphics.OpenGL4;

namespace FizX.OpenTK;

public class VertexArray : IDisposable
{
    private int _rendererId;
    
    public VertexArray()
    {
        _rendererId = GL.GenVertexArray();
    }

    public void Bind()
    {
        GL.BindVertexArray(_rendererId);
    }

    public void Unbind()
    {
        GL.BindVertexArray(0);
    }
    
    public void AddBuffer(VertexBuffer vb, VertexBufferLayout layout)
    {
        Bind();
        
        vb.Bind();

        int offset = 0;
        for (var i = 0; i < layout.Elements.Count; i++)
        {
            var element = layout.Elements[i];
            GL.EnableVertexAttribArray(i);
            GL.VertexAttribPointer(i, (int)element.Count, element.Type, element.Normalized, (int) layout.Stride, offset);
            offset += (int)element.GetSize();
            
        }
    }

    public void Dispose()
    {
        GL.DeleteVertexArray(_rendererId);
    }
}