using OpenTK.Graphics.OpenGL4;

namespace FizX.OpenTK;

public class VertexBufferLayout
{
    public List<VertexBufferLayoutElement> Elements = new List<VertexBufferLayoutElement>();
    
    public uint Stride { get; private set; }

    private void Push(VertexAttribPointerType type, uint count, bool normalized = false)
    {
        Elements.Add(new VertexBufferLayoutElement()
        {
            Count = count,
            Type = type,
            Normalized = normalized
        });
    }

    public void PushFloat(uint count, bool normalized = false)
    {
        Push(VertexAttribPointerType.Float, count, normalized);
        Stride += count * sizeof(float);
    }
}

public struct VertexBufferLayoutElement
{
    public uint Count;
    public VertexAttribPointerType Type;
    public bool Normalized;

    public uint GetSize()
    {
        uint typeSize = Type switch
        {
            VertexAttribPointerType.Float => 4,
            VertexAttribPointerType.UnsignedInt => 4,
            VertexAttribPointerType.UnsignedByte => 1,
            _ => throw new ArgumentOutOfRangeException()
        };

        return typeSize * Count;
    } 
}