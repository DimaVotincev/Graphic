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
using StbImageSharp;



namespace openTK_thing
{


    class Game : GameWindow
    {
        int width,height;


        float[] vertices = {
        -0.5f, 0.5f, 0f, // top left vertex - 0
        0.5f, 0.5f, 0f, // top right vertex - 1
        0.5f, -0.5f, 0f, // bottom right vertex - 2
        -0.5f, -0.5f, 0f // bottom left vertex - 3
        };

        // порядок отрисовки вершин
        uint[] indices =
            {
            0, 1, 2, //top triangle
            2, 3, 0 //bottom triangle
            };
        int EBO;


        int VAO, VBO;
        Shader shaderProgram = new Shader();

        int textureID;
        float[] texCoords =
        {
        0f, 1f,
        1f, 1f,
        1f, 0f,
        0f, 0f
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
            

            // создаю объект массива вершин (VAO) и буфер вершин (VBO)
            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();

            // прикрепляю VBO и копирую вершины в него
            GL.BindBuffer(BufferTarget.ArrayBuffer,VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length *
                sizeof(float), vertices, BufferUsageHint.StaticDraw);
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
            GL.BindVertexArray(0);










            // квадрат
            EBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length *
            sizeof(uint), indices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);










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
            ImageResult.FromStream(File.OpenRead("../../../Textures/kosmos.jpg"),
            ColorComponents.RedGreenBlueAlpha);
            GL.TexImage2D(TextureTarget.Texture2D, 0,
            PixelInternalFormat.Rgba, boxTexture.Width, boxTexture.Height, 0,
            PixelFormat.Rgba, PixelType.UnsignedByte, boxTexture.Data);
            //Unbind the texture
            GL.BindTexture(TextureTarget.Texture2D, 0);














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



            


            base.OnLoad();
        }

        protected override void OnUnload()
        {
            GL.DeleteBuffer(VAO);
            GL.DeleteBuffer(VBO);
            GL.DeleteBuffer(EBO);
            
            shaderProgram.DeleteShader();
            base.OnUnload();
        }

        // вызывается каждый кадр (рендеринг,связана с основной отрисовкой)
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(0.5f, 0.7f, 0.9f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            
            GL.UseProgram(shaderProgram.shaderHandle);
            GL.BindVertexArray(VAO);

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

            /*if (KeyboardState.IsKeyDown(Keys.F11) )
            {
                if (WindowState == WindowState.Normal)
                {
                    WindowState = WindowState.Fullscreen;                   
                }
                else
                {
                    WindowState = WindowState.Normal;
                }
                Thread.Sleep(1000);
            }*/
            
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
