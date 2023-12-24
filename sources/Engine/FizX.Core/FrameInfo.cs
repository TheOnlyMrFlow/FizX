namespace FizX.Core;

public struct FrameInfo
{
    public float DeltaTime { get; set; }
    
    public float Elapsed { get; set; }
    
    public ulong Index { get; set; }

    public override int GetHashCode()
    {
        return (int) (Index % int.MaxValue);
    }
}