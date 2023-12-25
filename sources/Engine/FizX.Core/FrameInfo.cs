namespace FizX.Core;

public struct FrameInfo
{
    public float DeltaTimeMs { get; set; }
    
    public float ElapsedMs { get; set; }
    
    public ulong Index { get; set; }

    public override int GetHashCode()
    {
        return (int) (Index % int.MaxValue);
    }
}