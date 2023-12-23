using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace FizX.OpenTK;

 public class Shader
 {
     public readonly int _rendererId;

     private readonly Dictionary<string, int> _uniformLocations;
     
     public Shader(string vertPath, string fragPath)
     {
         var vertexShaderSource = File.ReadAllText(vertPath);
         var vertexShader = GL.CreateShader(ShaderType.VertexShader);
         GL.ShaderSource(vertexShader, vertexShaderSource);
         CompileShader(vertexShader);
         
         var fragmentShaderSource = File.ReadAllText(fragPath);
         var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
         GL.ShaderSource(fragmentShader, fragmentShaderSource);
         CompileShader(fragmentShader);
         
         _rendererId = GL.CreateProgram();
         
         GL.AttachShader(_rendererId, vertexShader);
         GL.AttachShader(_rendererId, fragmentShader);
         
         LinkProgram(_rendererId);
         
         GL.DetachShader(_rendererId, vertexShader);
         GL.DetachShader(_rendererId, fragmentShader);
         GL.DeleteShader(fragmentShader);
         GL.DeleteShader(vertexShader);
         
         GL.GetProgram(_rendererId, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);
         
         _uniformLocations = new Dictionary<string, int>();

         // Loop over all the uniforms,
         for (var i = 0; i < numberOfUniforms; i++)
         {
             // get the name of this uniform,
             var key = GL.GetActiveUniform(_rendererId, i, out _, out _);

             // get the location,
             var location = GL.GetUniformLocation(_rendererId, key);

             // and then add it to the dictionary.
             _uniformLocations.Add(key, location);
         }
     }

     private static void CompileShader(int shader)
     {
         // Try to compile the shader
         GL.CompileShader(shader);

         // Check for compilation errors
         GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
         if (code != (int)All.True)
         {
             // We can use `GL.GetShaderInfoLog(shader)` to get information about the error.
             var infoLog = GL.GetShaderInfoLog(shader);
             throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
         }
     }

     private static void LinkProgram(int program)
     {
         // We link the program
         GL.LinkProgram(program);

         // Check for linking errors
         GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
         if (code != (int)All.True)
         {
             // We can use `GL.GetProgramInfoLog(program)` to get information about the error.
             throw new Exception($"Error occurred whilst linking Program({program})");
         }
     }
     
     public void Use()
     {
         GL.UseProgram(_rendererId);
     }
     
     public int GetAttribLocation(string attribName)
     {
         return GL.GetAttribLocation(_rendererId, attribName);
     }
     
     public void SetUniformInt(string name, int data)
     {
         GL.UseProgram(_rendererId);
         if (!_uniformLocations.TryGetValue(name, out var location))
             return;
             
         GL.Uniform1(location, data);
     }

     public void SetUniformFloat(string name, float data)
     {
         GL.UseProgram(_rendererId);
         if (!_uniformLocations.TryGetValue(name, out var location))
             return;
             
         GL.Uniform1(location, data);
     }
     
     public void SetUniformMatrix4(string name, Matrix4 data, bool transpose = false)
     {
         GL.UseProgram(_rendererId);
         if (!_uniformLocations.TryGetValue(name, out var location))
             return;
             
         GL.UniformMatrix4(location, transpose, ref data);
     }
     
     public void SetUniformVec3(string name, Vector3 data)
     {
         GL.UseProgram(_rendererId);
         if (!_uniformLocations.TryGetValue(name, out var location))
             return;
             
         GL.Uniform3(location, data);
     }
     
     public void SetUniformVec4(string name, Vector4 data)
     {
         GL.UseProgram(_rendererId);
         if (!_uniformLocations.TryGetValue(name, out var location))
             return;
             
         GL.Uniform4(location, data);
     }
 }