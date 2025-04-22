using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1;
using OpenTK.Audio.OpenAL;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using StbImageSharp;



namespace Game1
{
    struct Data
    {
        public List<Vector3> vertices;
        public List<Vector2> texCoord;
        public uint[] indices;
        public Data(List<Vector3> vertices,
        List<Vector2> texCoord,
        uint[] indices)
        {
            this.vertices = vertices;
            this.texCoord = texCoord;
            this.indices = indices;
        }
    };
        

    class Game : GameWindow
    {
        int width,height;

        Shader shaderProgram = new Shader();
        int textureVBO;

        int vbo_position;
        Vector3[] vertdata = new Vector3[] {
            new Vector3(-1f,-1f,0f),
            new Vector3(1f,-1f,0f),
            new Vector3(1f,1f,0f),
            new Vector3(-1f,1f,0f)
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


            GL.GenBuffers(1, out vbo_position);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer,
                (IntPtr)(vertdata.Length*Vector3.SizeInBytes),vertdata,
                BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.Uniform3(uniform_pos, campos);
            GL.Uniform1(uniform_aspect,aspect);
            GL.UseProgram(BasicProgramID);
            GL.BindBuffer(BufferTarget.ArrayBuffer,0);

            // ???? куда это писать??





            /*
            // прохожусь по всем объектам
            for (int i = 0; i < all_vertices.Count; i++)
            {

                List<Vector3> vertices = all_vertices[i].vertices;
                List<Vector2> texCoords = all_vertices[i].texCoord;
                uint[] indices = all_vertices[i].indices;

                // создаю объект массива вершин (VAO) и буфер вершин (VBO)
                VAOs[i] = GL.GenVertexArray();
                VBOs[i] = GL.GenBuffer();

                // прикрепляю VBO и копирую вершины в него
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBOs[i]);
                GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * Vector3.SizeInBytes,
                    vertices.ToArray(), BufferUsageHint.StaticDraw);

                // прикрепляю VAO 
                GL.BindVertexArray(VAOs[i]);
                // bind a slot number 0
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

                // enable the slot
                GL.EnableVertexArrayAttrib(VAOs[i], 0);

                // unbind
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

                // квадрат
                EBOs[i] = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBOs[i]);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length *
                sizeof(uint), indices, BufferUsageHint.StaticDraw);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

                //Create, bind texture
                textureVBO = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, textureVBO);
                GL.BufferData(BufferTarget.ArrayBuffer,
                    texCoords.Count * Vector2.SizeInBytes,
                    texCoords.ToArray(), BufferUsageHint.StaticDraw);

                //Point a slot number 1
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false,
                0, 0);
                //Enable the slot
                GL.EnableVertexArrayAttrib(VAOs[i], 1);

                // Texture Loading
                textureIDs[i] = GL.GenTexture(); //Generate empty texture
                GL.ActiveTexture(TextureUnit.Texture0); //Activate the texture in the unit
                GL.BindTexture(TextureTarget.Texture2D, textureIDs[i]); //Bind texture
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

                ImageResult boxTexture;
                boxTexture =
                    ImageResult.FromStream(File.OpenRead(tex_paths[i]),
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

            }
            */
            GL.Enable(EnableCap.DepthTest);
            base.OnLoad();
        }

        protected override void OnUnload()
        {
            Console.WriteLine("----Thank you for playing!----");
            for (int i = 0; i < VAOs.Length; i++)
            {
                GL.DeleteBuffer(VAOs[i]);
            }

            for (int i = 0; i < VBOs.Length; i++)
            {
                GL.DeleteBuffer(VBOs[i]);
            }

            for (int i = 0; i < EBOs.Length; i++)
            {
                GL.DeleteBuffer(EBOs[i]);
            }

            for (int i = 0; i < textureIDs.Length; i++)
            {
                GL.DeleteBuffer(textureIDs[i]);
            }

            shaderProgram.DeleteShader();
            base.OnUnload();
        }



        List<Vector3> treemoves;
        Random rand = new Random();
        int is_gamestarted = 0;
        // вызывается каждый кадр (рендеринг,связана с основной отрисовкой)
        protected override void OnRenderFrame(FrameEventArgs args)
        {


            GL.ClearColor(0.5f, 0.7f, 0.9f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // сначала стоит на первом дереве
            int first_tree_id = ald.all_data.Count - ald.treecount * 2;
            int tree_part_loaded = 0;
            int treemoveid = 0;
            
            for (int i = 0; i < all_vertices.Count; i++)
            {
                Vector3 pos = camera.position;

                



                GL.UseProgram(shaderProgram.shaderHandle);
                GL.BindTexture(TextureTarget.Texture2D, textureIDs[i]);
                GL.BindVertexArray(VAOs[i]);



                //Transformation
                Matrix4 model = Matrix4.Identity;


                if(is_gamestarted == 1)
                {
                    // если объект - машина (или её часть)
                    if (i <= 6)
                    {
                        Matrix4 treeTranslation =
                            Matrix4.CreateTranslation(carMovement);
                        model *= treeTranslation;
                    }
                    else if (i >= first_tree_id) // если объект - дерево
                    {
                        if (tree_part_loaded == 2)
                        {
                            treemoveid++;
                            tree_part_loaded = 0;
                        }


                        float treeZpos = ald.treePositions[treemoveid].Z + treemoves[treemoveid].Z;


                        // телепорт(на заданное значение по Z) + смещение по Х рандомное
                        if (treeZpos >= 5f)
                        {
                            float desiredZ = -37f;
                            float offsetX = (float)(rand.NextDouble() * -2f + 1f); // [-1, -11]
                            float newX = 0;
                            treemoves[treemoveid] += new Vector3(newX, 0, desiredZ);


                        }

                        Matrix4 treeTranslation =
                            Matrix4.CreateTranslation(treemoves[treemoveid]);
                        model *= treeTranslation;
                        treemoves[treemoveid] += new Vector3(0, 0, 0.007f);

                        tree_part_loaded++;
                    }
                }
                
                
                
                Matrix4 view = camera.GetViewMatrix();
                Matrix4 projection = camera.GetProjection();

                // отправляем на GPU
                int modelLocation = GL.GetUniformLocation(shaderProgram.shaderHandle, "model");
                int viewLocation = GL.GetUniformLocation(shaderProgram.shaderHandle, "view");
                int projectionLocation = GL.GetUniformLocation(shaderProgram.shaderHandle, "projection");



                // отправляем в uniforms
                GL.UniformMatrix4(modelLocation, true, ref model);
                GL.UniformMatrix4(viewLocation, true, ref view);
                GL.UniformMatrix4(projectionLocation, true, ref projection);

                uint[] indices = all_vertices[i].indices;

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBOs[i]);
                GL.DrawElements(PrimitiveType.Triangles, indices.Length,
                DrawElementsType.UnsignedInt, 0);

                
            }

            
            Context.SwapBuffers();

            base.OnRenderFrame(args);
        }



        Vector3 carMovement;
        // тоже вызывается каждый кадр и обновляет окно, когда оно готово
        protected override void OnUpdateFrame(FrameEventArgs args)
        {

            if (KeyboardState.IsKeyDown(Keys.F11))
            {
                if (WindowState != WindowState.Fullscreen)
                {
                    WindowState = WindowState.Fullscreen;
                }
            }





            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            if (KeyboardState.IsKeyDown(Keys.Up))
            {
                is_gamestarted = 1;
            }


            float range = 3.5f;
            if (is_gamestarted ==1)
            {
                if (KeyboardState.IsKeyDown(Keys.Left))
                {

                    if (-range <= carMovement.X)
                    {
                        carMovement += new Vector3(-0.02f, 0, 0);
                    }

                }
                else if (KeyboardState.IsKeyDown(Keys.Right))
                {

                    if (carMovement.X <= range)
                    {
                        carMovement += new Vector3(0.02f, 0, 0);
                    }
                }
            }
            

                MouseState mouse = MouseState;
            KeyboardState input = KeyboardState;
            base.OnUpdateFrame(args);

            // not need camera for game
            //camera.Update(input, mouse, args);

            base.OnUpdateFrame(args);
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
