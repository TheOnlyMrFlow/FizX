using OpenTK.Graphics.OpenGL4;

namespace FizX.OpenTK;

public class Renderer
{
    public void Clear()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);
    }
    
    public void Draw(ref VertexArray va, ref IndexBuffer ib, ref Shader shader)
    {
        shader.Use();
        va.Bind();
        ib.Bind();
        
        GL.DrawElements(PrimitiveType.Triangles, ib.Count, DrawElementsType.UnsignedInt, default);
    }
}