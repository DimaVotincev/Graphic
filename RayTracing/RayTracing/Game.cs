using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Audio.OpenAL;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RayTracing;
using StbImageSharp;



namespace RayTracing
{


    class Game : GameWindow
    {




        int width, height;
        int vbo_position;
        Vector3[] vertdata = new Vector3[] {
        new Vector3(-1f, -1f, 0f),
        new Vector3( 1f, -1f, 0f),
        new Vector3( 1f, 1f, 0f),
        new Vector3(-1f, 1f, 0f) };

        /*Vector3[] vertdata = new Vector3[] {
        new Vector3(0,0,0),
        new Vector3(0,0,0),
        new Vector3(0,0,0),
        new Vector3(0,0,0) };

       
        Vector3[] vertdata = new Vector3[] {
        new Vector3(-0.5f,-0.5f,0),
        new Vector3(0.5f,-0.5f,0),
        new Vector3(0.5f,0.5f,0),
        new Vector3(-0.5f,0.5f,0)};
         */







        int EBO;
        int VAO, VBO;
        Shader shaderProgram = new Shader();
        int textureID;
        int textureVBO;

        List<Vector2> texCoords = new List<Vector2>()
        {
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f)

            
        };

        uint[] indices =
        {
            0, 1, 2,	 // front face
			2, 3, 0
          
        };


        public Game(int width, int height) : base
            (GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            
            //  Vector2i - двумерный массив интов
            this.CenterWindow(new Vector2i(width, height));
            this.height = height;
            this.width = width;
        }

        protected override void OnLoad()
        {
            /*GL.GenBuffers(1, out vbo_position);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);

            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length *
             Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float, false, 0,
            0);
            GL.Uniform3(uniform_pos, campos);
            GL.Uniform1(uniform_aspect, aspect);

            GL.UseProgram(BasicProgramID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);*/






            // создаю объект массива вершин (VAO) и буфер вершин (VBO)
            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();

            // прикрепляю VBO и копирую вершины в него
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertdata.Length * Vector3.SizeInBytes *
                sizeof(float), vertdata.ToArray(), BufferUsageHint.StaticDraw);
            //  _--------------------------------------------^ 
            // |StaticDraw: the data will most likely not change at all or very rarely.
            // |DynamicDraw: the data is likely to change a lot.
            // |StreamDraw: the data will change every time it is drawn.


            // прикрепляю VAO 
            GL.BindVertexArray(VAO);
            // bind a slot number 0
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

            // enable the slot
            GL.EnableVertexArrayAttrib(VAO, 0);

            // unbind
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);











            // квадрат
            EBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length *
            sizeof(uint), indices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            //Create, bind texture
            textureVBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, textureVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, texCoords.Count * Vector3.SizeInBytes *
            sizeof(float), texCoords.ToArray(), BufferUsageHint.StaticDraw);
            //Point a slot number 1
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false,
            0, 0);
            //Enable the slot
            GL.EnableVertexArrayAttrib(VAO, 1);



            // Texture Loading
            textureID = GL.GenTexture(); //Generate empty texture
            GL.ActiveTexture(TextureUnit.Texture0); //Activate the texture in the unit
            GL.BindTexture(TextureTarget.Texture2D, textureID); //Bind texture
                                                                //Texture parameters
            GL.TexParameter(TextureTarget.Texture2D,
            TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D,
            TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D,
            TextureParameterName.TextureMinFilter,
            (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D,
            TextureParameterName.TextureMagFilter,
            (int)TextureMagFilter.Nearest);
            //Load image
            StbImage.stbi_set_flip_vertically_on_load(1);
            ImageResult boxTexture =
            ImageResult.FromStream(File.OpenRead("../../../Textures/dog.PNG"),
            ColorComponents.RedGreenBlueAlpha);
            GL.TexImage2D(TextureTarget.Texture2D, 0,
            PixelInternalFormat.Rgba, boxTexture.Width, boxTexture.Height, 0,
            PixelFormat.Rgba, PixelType.UnsignedByte, boxTexture.Data);
            //Unbind the texture
            GL.BindTexture(TextureTarget.Texture2D, 0);









            GL.BindVertexArray(0);




            shaderProgram.LoadShader();
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, LoadShaderSource("shader.vert"));
            GL.CompileShader(vertexShader);
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, LoadShaderSource("shader.frag"));
            GL.CompileShader(fragmentShader);
            GL.AttachShader(shaderProgram.shaderHandle, vertexShader);
            GL.AttachShader(shaderProgram.shaderHandle, fragmentShader);
            GL.LinkProgram(shaderProgram.shaderHandle);

            //GL.DeleteProgram(shaderProgram);
            // перенесена в OnUnload

            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int success1);
            if (success1 == 0)
            {
                string infoLog = GL.GetShaderInfoLog(vertexShader);
                Console.WriteLine(infoLog);
            }
            GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out int success2);
            if (success2 == 0)
            {
                string infoLog = GL.GetShaderInfoLog(fragmentShader);
                Console.WriteLine(infoLog);
            }




            GL.Enable(EnableCap.DepthTest);

            base.OnLoad();
        }

        protected override void OnUnload()
        {
            GL.DeleteBuffer(VAO);
            GL.DeleteBuffer(VBO);
            GL.DeleteBuffer(EBO);
            GL.DeleteTexture(textureID);

            shaderProgram.DeleteShader();
            base.OnUnload();
        }



        float yRot = 0f;

        // вызывается каждый кадр (рендеринг,связана с основной отрисовкой)
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(0.5f, 0.7f, 0.9f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(shaderProgram.shaderHandle);
            GL.BindTexture(TextureTarget.Texture2D, textureID); //
            GL.BindVertexArray(VAO);


            yRot += 0.001f;
            if (yRot >= 360f)
            {
                yRot = 0f;
            }

            //Transformation
            //Matrix4 model = Matrix4.Identity;
            Matrix4 model = Matrix4.CreateRotationY(yRot);
            /*model += Matrix4.CreateRotationZ(2f);
            model += Matrix4.CreateRotationX(1f);*/

            float zoom = 0;

            Matrix4 translation = Matrix4.CreateTranslation(0f, 0f, -3f + zoom);
            model *= translation;
            Matrix4 view = Matrix4.Identity;
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60.0f), width / height, 0.1f, 100.0f);

            //model = Matrix4.CreateTranslation(0f, 0f, -1f);

            // отправляем на GPU
            int modelLocation =
            GL.GetUniformLocation(shaderProgram.shaderHandle, "model");
            int viewLocation =
            GL.GetUniformLocation(shaderProgram.shaderHandle, "view");
            int projectionLocation =
            GL.GetUniformLocation(shaderProgram.shaderHandle, "projection");



            // отправляем в uniforms
            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.UniformMatrix4(viewLocation, true, ref view);
            GL.UniformMatrix4(projectionLocation, true, ref projection);





            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length,
            DrawElementsType.UnsignedInt, 0);




            Context.SwapBuffers();


            base.OnRenderFrame(args);
        }








        // тоже вызывается каждый кадр и обновляет окно, когда оно готово
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }

        }


        // OpenTK - обертка OpenGL
        // когда мы меняем размер окна - об этом  нужно сообщить OpenGL
        // мы это делаем через Viewport
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            this.width = e.Width;
            this.height = e.Height;
        }

        public static string LoadShaderSource(string filepath)
        {
            string shaderSource = "";
            try
            {
                using (StreamReader reader = new
                StreamReader("../../../Shaders/" + filepath))
                {
                    shaderSource = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to load shader source file: " + e.Message);
            }
            return shaderSource;
        }
    }
}