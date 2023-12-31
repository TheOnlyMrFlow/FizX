using FizX.Core.Serialization;

namespace FizX.Core.Graphics;

public class SpriteRendererComponent : RendererComponent
{
    [FSerializable]
    public string TextureFilePath { get; init; }
}