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

        // квадрат
        /*float[] vertices = {
            -0.5f, 0.5f, 0f, // top left vertex - 0
            0.5f, 0.5f, 0f, // top right vertex - 1
            0.5f, -0.5f, 0f, // bottom right vertex - 2
            -0.5f, -0.5f, 0f // bottom left vertex - 3
        };*/

        // порядок отрисовки вершин у квадрата
        /*uint[] indices =
            {
                     
            0, 1, 2, //top triangle
            2, 3, 0 //bottom triangle 
            
            };*/




        int EBO;


        int VAO, VBO;
        Shader shaderProgram = new Shader();

        int textureID;
        /*float[] texCoords =
        {
        0f, 1f,
        1f, 1f,
        1f, 0f,
        0f, 0f
        };*/
        int textureVBO;


        // кубик

        List<Vector3> vertices1 = new List<Vector3>()
        {	
			//front face
			new Vector3(-0.5f,  0.5f, 0.5f), //top-left vertice
			new Vector3( 0.5f,  0.5f, 0.5f), //top-right vertice
			new Vector3( 0.5f, -0.5f, 0.5f), //bottom-right vertice
			new Vector3(-0.5f, -0.5f, 0.5f), //botom-left vertice
			//right face
			new Vector3( 0.5f,  0.5f, 0.5f), //top-left vertice
			new Vector3( 0.5f,  0.5f, -0.5f), //top-right vertice
			new Vector3( 0.5f, -0.5f, -0.5f), //bottom-right vertice
			new Vector3( 0.5f, -0.5f, 0.5f), //botom-left vertice
			//back face
			new Vector3(-0.5f,  0.5f, -0.5f), //top-left vertice
			new Vector3( 0.5f,  0.5f, -0.5f), //top-right vertice
			new Vector3( 0.5f, -0.5f, -0.5f), //bottom-right vertice
			new Vector3(-0.5f, -0.5f, -0.5f), //botom-left vertice
			//left face
			new Vector3( -0.5f,  0.5f, 0.5f), //top-left vertice
			new Vector3( -0.5f,  0.5f, -0.5f), //top-right vertice
			new Vector3( -0.5f, -0.5f, -0.5f), //bottom-right vertice
			new Vector3( -0.5f, -0.5f, 0.5f), //botom-left vertice
			// top face
			new Vector3(-0.5f,  0.5f, -0.5f), //top-left vertice
			new Vector3( 0.5f,  0.5f, -0.5f), //top-right vertice
			new Vector3( 0.5f, 0.5f, 0.5f), //bottom-right vertice
			new Vector3(-0.5f, 0.5f, 0.5f), //botom-left vertice
			//bottom face
			new Vector3(-0.5f,  -0.5f, -0.5f), //top-left vertice
			new Vector3( 0.5f,  -0.5f, -0.5f), //top-right vertice
			new Vector3( 0.5f, -0.5f, 0.5f), //bottom-right vertice
			new Vector3(-0.5f, -0.5f, 0.5f), //botom-left vertice
		};




        List<Vector2> texCoords1 = new List<Vector2>()
        {
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
        };

        uint[] indices1 =
        {
            0, 1, 2,	 // front face
			2, 3, 0,	

			4, 5, 6,     // right face 
            6, 7, 4,

            8, 9, 10,    // back face
            10, 11, 8,

            12, 13, 14,  // left face
            14, 15, 12,

            16, 17, 18,  // top face
            18, 19, 16,

            20, 21, 22,  // bottom face
            22, 23, 20
        };


        
        List<Vector3> vertices2 = new List<Vector3>()
        {	
			//front face
			new Vector3(-3f,  3f, 3f), //top-left vertice
			new Vector3( 3f,  3f, 3f), //top-right vertice
			new Vector3( 3f, -3f, 3f), //bottom-right vertice
			new Vector3(-3f, -3f, 3f), //botom-left vertice
        };


        List<Vector2> texCoords2 = new List<Vector2>()
        {
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f)
        };


        uint[] indices2 =
        {
            0, 1, 2,	 // front face
			2, 3, 0,
        };



        List<string> tex_paths = new List<string>();
        List<Data> all_vertices = new List<Data>();


        int[] VAOs, VBOs, EBOs, textureIDs;




        /*uint[] indices;
        List<Vector2> texCoords;
        List<Vector3> vertices;*/


        // not need camera for game

        Camera camera;

        All_Data ald = new All_Data();
        
        public Game(int width, int height) : base 
            (GameWindowSettings.Default, NativeWindowSettings.Default)
        {

            // это потом убрать
            /*all_vertices.Add(new Data(vertices1,texCoords1,indices1));
            tex_paths.Add("../../../Textures/dog.PNG");
            all_vertices.Add(new Data(vertices2, texCoords2, indices2));
            tex_paths.Add("../../../Textures/kosmos.jpg");*/


            // новый метод ( с использованием All_data)
            for (int i = 0; i < ald.all_data.Count; i++)
            {
                all_vertices.Add(ald.all_data[i]);
                tex_paths.Add(ald.tex_paths[i]);

            }
            treemoves = new List<Vector3>();
            
            for (int i = 0; i < ald.treecount; i++)
            {
                treemoves.Add(new Vector3(0,0,0));
            }

            Vector3 carMovement = new Vector3(0,0,0);


            VAOs = new int[all_vertices.Count];
            VBOs = new int[all_vertices.Count];
            EBOs = new int[all_vertices.Count];
            textureIDs = new int[all_vertices.Count];



            Vector3 pos = new Vector3(-0.005f, 5f, 6.5f); ;
            camera = new Camera(width, height, pos);


            CursorState = CursorState.Grabbed;


            //  Vector2i - двумерный массив интов
            this.CenterWindow(new Vector2i(width, height));
            this.height = height;
            this.width = width;
        }

        protected override void OnLoad()
        {



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



        List<Vector3> treemoves;
        Random rand = new Random();
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

                Console.WriteLine($"X: {pos.X}");
                Console.WriteLine($"Y: {pos.Y}");
                Console.WriteLine($"Z: {pos.Z}");



                GL.UseProgram(shaderProgram.shaderHandle);
                GL.BindTexture(TextureTarget.Texture2D, textureIDs[i]);
                GL.BindVertexArray(VAOs[i]);




                /*yRot += 0.0001f;
                if(yRot >= 360f)
                {
                    yRot = 0f;
                }*/
                


                //Transformation
                Matrix4 model = Matrix4.Identity;
                //Matrix4 model = Matrix4.CreateRotationY(yRot);
                /*model += Matrix4.CreateRotationZ(2f);
                model += Matrix4.CreateRotationX(1f);*/

                /*zoom += 0.0001f;
                if(zoom >= zcoeff)
                {
                    zoom = zcoeff*(-1f);
                }*/

                // начиная с этого индекса , остальны объекты - деревья


                //treemove += 0.001f;
                
                // если объект - машина (или её часть)
               if (i <=6)
                {
                    Matrix4 treeTranslation =
                        Matrix4.CreateTranslation(carMovement);
                    model *= treeTranslation;
                } else if (i >= first_tree_id) // если объект - дерево
                {
                    if (tree_part_loaded == 2)
                    {
                        treemoveid++;
                        tree_part_loaded = 0;
                    }


                    // хранить не флоты tremoves
                    // а вектора смещений Vector3 treemove3d
                    // и мой флот treemoves это будет просто treemove3d[2]
                    // а координату по X изменяю так же,но везде где noveX
                    // там treemove3d[0
                    


                    float treeZpos = ald.treePositions[treemoveid].Z + treemoves[treemoveid].Z;
                    

                    // телепорт(на заданное значение по Z) + смещение по Х рандомное
                    if (treeZpos >= 5f)
                    {
                        // нужно смещение -20f предположим
                        // то есть
                        float desiredZ = -37f;
                        float offsetX = (float)(rand.NextDouble() * -2f + 1f); // [-1, -11]

                        //float newX = ald.treePositions[treemoveid][0] - treemoves[treemoveid].X + offsetX;
                        float newX = 0;
                        treemoves[treemoveid]+= new Vector3(newX,0,desiredZ);
                        //moveX = ald.treePositions[treemoveid].X + (float)(rand.NextDouble() * -2f + 1f);

                    }



                    Matrix4 treeTranslation = 
                        Matrix4.CreateTranslation(treemoves[treemoveid]);
                    model *= treeTranslation;
                    treemoves[treemoveid] += new Vector3(0, 0, 0.07f);

                    tree_part_loaded++;

                    
                    

                    
                }
                



                /*if(i == 10 || i == 11)
                {
                    Matrix4 treeTranslation = Matrix4.CreateTranslation(0f, 0f, treemoves[0]);
                    model *= treeTranslation;
                    treemoves[0] += 0.01f;
                    if (treemoves[0] >= 5f)
                    {
                        treemoves[0] = -15f;
                    }
                }

                if (i == 12 || i == 13)
                {
                    Matrix4 treeTranslation = Matrix4.CreateTranslation(0f, 0f, treemoves[1]);
                    model *= treeTranslation;
                    treemoves[1] += 0.1f;
                    if (treemoves[1] >= 5f)
                    {
                        treemoves[1] = -15f;
                    }
                }*/


                /*if (i >= 10)
                {
                    // Деревья — смещаем их, допустим, по оси X
                    if (treemove <= -5f) // предел, когда дерево "исчезает"
                    {
                        treemove = 20f; // телепорт обратно
                    }

                    treemove -= 1f;
                    if (ald.treePositions[1].Z <= -5f)
                    {
                        
                        treemove = 20f;

                    } else
                    {
                        Matrix4 treeTranslation = Matrix4.CreateTranslation(0f, 0f, -treemove);
                        model *= treeTranslation;
                    }
                        
                }
                else
                {
                    
                    // Обычное смещение для остальных объектов
                    Matrix4 translation = Matrix4.CreateTranslation(0f, 0f, -2f + zoom);
                    model *= translation;
                }
                Console.WriteLine($"Movetr: {treemove}");*/


                /*Matrix4 translation = Matrix4.CreateTranslation(0f, 0f, -2f + zoom);
                model *= translation;*/



                //Matrix4 view = Matrix4.Identity;
                //Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60.0f), width / height, 0.1f, 100.0f);

                // not need camera for game
                Matrix4 view = camera.GetViewMatrix();
                Matrix4 projection = camera.GetProjection();

                //model = Matrix4.CreateTranslation(0f, 0f, -1f);

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
            
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            float range = 3.5f;
            if (KeyboardState.IsKeyDown(Keys.Left))
            {
                
                if (-range <= carMovement.X)
                {
                    carMovement += new Vector3(-0.1f, 0, 0);
                }
                
            } else if (KeyboardState.IsKeyDown(Keys.Right)) {

                if (carMovement.X <= range)
                {
                    carMovement += new Vector3(0.1f, 0, 0);
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
