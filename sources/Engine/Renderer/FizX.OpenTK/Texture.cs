using OpenTK.Graphics.OpenGL4;
using StbImageSharp;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;

namespace FizX.OpenTK; 


 public class Texture : IDisposable
 {
     private readonly int _rendererId;

     public int Width { get; private set; }
     public int Height { get; private set; }

     public Texture(string path)
     {
         _rendererId = GL.GenTexture();
         
         GL.ActiveTexture(TextureUnit.Texture0);
         GL.BindTexture(TextureTarget.Texture2D, _rendererId);

         StbImage.stbi_set_flip_vertically_on_load(1);
         
         using (Stream stream = File.OpenRead(path))
         {
             ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
             Width = image.Width;
             Height = image.Height;
             
             GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
         }

         GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new[] { (uint)TextureMinFilter.Linear });
         GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new[] { (uint)TextureMagFilter.Linear });
         
         GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, new[] { (uint)TextureWrapMode.ClampToEdge });
         GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, new[] { (uint)TextureWrapMode.ClampToEdge });
         
         GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
     }
     
     
     public void Use(TextureUnit unit)
     {
         GL.ActiveTexture(unit);
         GL.BindTexture(TextureTarget.Texture2D, _rendererId);
     }
     
     public void UnUse(TextureUnit unit)
     {
         GL.BindTexture(TextureTarget.Texture2D, 0);
     }

     public void Dispose()
     {
         GL.DeleteTexture(_rendererId);
     }
 }