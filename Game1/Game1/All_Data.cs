using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using OpenTK.Mathematics;

namespace Game1
{
    class All_Data
    {
        public List<Data> all_data = new List<Data>();
        public List<string> tex_paths = new List<string>();
        public int treecount = 0;


        // примерные
        // x от [-18,-5] , [5,18]
        // y =0
        // z от -7f до -45f

        public List<Vector3> treePositions = TreePositionGenerator.Generate(40);




        public List<Tree> trees = new List<Tree>();

       

        public All_Data()
        {

            
            // -------car details (7 items)
            all_data.Add(new Body().body);
            tex_paths.Add("../../../Textures/RED.jpg");

            all_data.Add(new FrontLights().lights);
            tex_paths.Add("../../../Textures/WHITE.jpg");

            all_data.Add(new RearLights().lights);
            tex_paths.Add("../../../Textures/WHITE.jpg");

            all_data.Add(new WheelFL().wheel);
            tex_paths.Add("../../../Textures/BLACK.jpg");

            all_data.Add(new WheelFR().wheel);
            tex_paths.Add("../../../Textures/BLACK.jpg");

            all_data.Add(new WheelRL().wheel);
            tex_paths.Add("../../../Textures/BLACK.jpg");

            all_data.Add(new WheelRR().wheel);
            tex_paths.Add("../../../Textures/BLACK.jpg");
            // -------car details


            // -------around details
            all_data.Add(new Road().road);
            tex_paths.Add("../../../Textures/ROAD.jpg");

            all_data.Add(new Ground().ground);
            tex_paths.Add("../../../Textures/GRASS.jpg");

            all_data.Add(new SkyBackground().sky);
            tex_paths.Add("../../../Textures/SKY.jpg");
            // -------around details



            // ------- T R E E S

            Random rand = new Random();
            for (int i = 0; i < treePositions.Count; i++)
            {
                var pos = treePositions[i];
                // Ствол
                var trunk = new TreeTrunk(pos).trunk;
                all_data.Add(trunk);
                tex_paths.Add("../../../Textures/BROWN.jpg");

                // Листва (немного выше)
                var leaves = new TreeLeaves(pos + new Vector3(0, 2f, 0)).leaves;
                all_data.Add(leaves);
                
                
                int rn = rand.Next(0, 5);
                switch (rn)
                {
                    case 0:
                        tex_paths.Add("../../../Textures/GREEN_TREE.jpg");
                        break;
                    case 1:
                        tex_paths.Add("../../../Textures/GREEN_TREE2.jpg");
                        break;
                    case 2:
                        tex_paths.Add("../../../Textures/GR_TREE.jpg");
                        break;
                    case 3:
                        tex_paths.Add("../../../Textures/RED_TREE.jpg");
                        break;
                    case 4:
                        tex_paths.Add("../../../Textures/YELLOW_TREE.jpg");
                        break;
                    default:
                        // На всякий случай, если что-то пойдёт не так
                        tex_paths.Add("../../../Textures/GREEN_TREE.jpg");
                        break;
                }

            }
            treecount = treePositions.Count;
            return;
        }
    }

 
    class Body
    {
        List<Vector3> vertices = new List<Vector3>()
        {
            new Vector3(-1, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 0, -1), new Vector3(-1, 0, -1),
            new Vector3(-1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, -1), new Vector3(-1, 1, -1)
        };

        List<Vector2> texCoords = new List<Vector2>()
        {
            new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
            new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1)
        };

        uint[] indices = {
            0, 1, 2, 2, 3, 0,
            4, 5, 6, 6, 7, 4,
            0, 1, 5, 5, 4, 0,
            1, 2, 6, 6, 5, 1,
            2, 3, 7, 7, 6, 2,
            3, 0, 4, 4, 7, 3
        };

        public Data body;

        public Body()
        {
            body = new Data
            {
                vertices = vertices,
                texCoord = texCoords,
                indices = indices
            };
        }
    }



    class FrontLights
    {
        public Data lights;

        public FrontLights()
        {

            lights = new Data();
            lights.vertices = new List<Vector3>();
            lights.texCoord = new List<Vector2>();


            // Размер фары
            float width = 0.5f;
            float height = 0.3f; // в высоту машинки
            float depth = 0.4f;

            // Центры фар
            Vector3 leftCenter = new Vector3(-0.45f, 0.7f, 1f);
            Vector3 rightCenter = new Vector3(0.45f, 0.7f, 1f);

            List<Vector3> MakeCube(Vector3 center)
            {
                float x = center.X;
                float y = center.Y;
                float z = center.Z;

                float w = width / 2f;
                float h = height / 2f;
                float d = depth / 2f;

                return new List<Vector3>
            {
                // Front face
                new Vector3(x - w, y + h, z + d), new Vector3(x + w, y + h, z + d),
                new Vector3(x + w, y - h, z + d), new Vector3(x - w, y - h, z + d),

                // Back face
                new Vector3(x - w, y + h, z - d), new Vector3(x + w, y + h, z - d),
                new Vector3(x + w, y - h, z - d), new Vector3(x - w, y - h, z - d),

                // Left face
                new Vector3(x - w, y + h, z - d), new Vector3(x - w, y + h, z + d),
                new Vector3(x - w, y - h, z + d), new Vector3(x - w, y - h, z - d),

                // Right face
                new Vector3(x + w, y + h, z + d), new Vector3(x + w, y + h, z - d),
                new Vector3(x + w, y - h, z - d), new Vector3(x + w, y - h, z + d),

                // Top face
                new Vector3(x - w, y + h, z - d), new Vector3(x + w, y + h, z - d),
                new Vector3(x + w, y + h, z + d), new Vector3(x - w, y + h, z + d),

                // Bottom face
                new Vector3(x - w, y - h, z + d), new Vector3(x + w, y - h, z + d),
                new Vector3(x + w, y - h, z - d), new Vector3(x - w, y - h, z - d)
            };
            }

            uint[] MakeCubeIndices(uint startIndex)
            {
                return new uint[]
                {
                // Front face
                startIndex, startIndex + 1, startIndex + 2, startIndex + 2, startIndex + 3, startIndex,
                // Back face
                startIndex + 4, startIndex + 5, startIndex + 6, startIndex + 6, startIndex + 7, startIndex + 4,
                // Left face
                startIndex + 8, startIndex + 9, startIndex +10, startIndex +10, startIndex +11, startIndex + 8,
                // Right face
                startIndex +12, startIndex +13, startIndex +14, startIndex +14, startIndex +15, startIndex +12,
                // Top face
                startIndex +16, startIndex +17, startIndex +18, startIndex +18, startIndex +19, startIndex +16,
                // Bottom face
                startIndex +20, startIndex +21, startIndex +22, startIndex +22, startIndex +23, startIndex +20
                };
            }

            // Добавляем левую и правую фары
            var leftVertices = MakeCube(leftCenter);
            var rightVertices = MakeCube(rightCenter);

            lights.vertices.AddRange(leftVertices);
            lights.vertices.AddRange(rightVertices);

            // Заглушки UV (можно потом заменить)
            for (int i = 0; i < lights.vertices.Count; i++)
                lights.texCoord.Add(new Vector2(0, 0));

            // Индексы
            lights.indices = MakeCubeIndices(0)
                .Concat(MakeCubeIndices(24)).ToArray(); // вторая фара начинается с вершины 24
        }
    }




    class RearLights
    {
        public Data lights;

        public RearLights()
        {

            lights = new Data();
            lights.vertices = new List<Vector3>();
            lights.texCoord = new List<Vector2>();


            // Размер фары
            float width = 0.5f;
            float height = 0.3f; // в высоту машинки
            float depth = 0.4f;

            // Центры фар
            Vector3 leftCenter = new Vector3(-0.45f, 0.7f, -1f);
            Vector3 rightCenter = new Vector3(0.45f, 0.7f, -1f);

            List<Vector3> MakeCube(Vector3 center)
            {
                float x = center.X;
                float y = center.Y;
                float z = center.Z;

                float w = width / 2f;
                float h = height / 2f;
                float d = depth / 2f;

                return new List<Vector3>
            {
                // Front face
                new Vector3(x - w, y + h, z + d), new Vector3(x + w, y + h, z + d),
                new Vector3(x + w, y - h, z + d), new Vector3(x - w, y - h, z + d),

                // Back face
                new Vector3(x - w, y + h, z - d), new Vector3(x + w, y + h, z - d),
                new Vector3(x + w, y - h, z - d), new Vector3(x - w, y - h, z - d),

                // Left face
                new Vector3(x - w, y + h, z - d), new Vector3(x - w, y + h, z + d),
                new Vector3(x - w, y - h, z + d), new Vector3(x - w, y - h, z - d),

                // Right face
                new Vector3(x + w, y + h, z + d), new Vector3(x + w, y + h, z - d),
                new Vector3(x + w, y - h, z - d), new Vector3(x + w, y - h, z + d),

                // Top face
                new Vector3(x - w, y + h, z - d), new Vector3(x + w, y + h, z - d),
                new Vector3(x + w, y + h, z + d), new Vector3(x - w, y + h, z + d),

                // Bottom face
                new Vector3(x - w, y - h, z + d), new Vector3(x + w, y - h, z + d),
                new Vector3(x + w, y - h, z - d), new Vector3(x - w, y - h, z - d)
            };
            }

            uint[] MakeCubeIndices(uint startIndex)
            {
                return new uint[]
                {
                // Front face
                startIndex, startIndex + 1, startIndex + 2, startIndex + 2, startIndex + 3, startIndex,
                // Back face
                startIndex + 4, startIndex + 5, startIndex + 6, startIndex + 6, startIndex + 7, startIndex + 4,
                // Left face
                startIndex + 8, startIndex + 9, startIndex +10, startIndex +10, startIndex +11, startIndex + 8,
                // Right face
                startIndex +12, startIndex +13, startIndex +14, startIndex +14, startIndex +15, startIndex +12,
                // Top face
                startIndex +16, startIndex +17, startIndex +18, startIndex +18, startIndex +19, startIndex +16,
                // Bottom face
                startIndex +20, startIndex +21, startIndex +22, startIndex +22, startIndex +23, startIndex +20
                };
            }

            // Добавляем левую и правую фары
            var leftVertices = MakeCube(leftCenter);
            var rightVertices = MakeCube(rightCenter);

            lights.vertices.AddRange(leftVertices);
            lights.vertices.AddRange(rightVertices);

            // Заглушки UV (можно потом заменить)
            for (int i = 0; i < lights.vertices.Count; i++)
                lights.texCoord.Add(new Vector2(0, 0));

            // Индексы
            lights.indices = MakeCubeIndices(0)
                .Concat(MakeCubeIndices(24)).ToArray(); // вторая фара начинается с вершины 24
        }
    }



    abstract class WheelBase
    {
        protected Data GenerateWheel(float xOffset, float zOffset)
        {
            Data w = new Data();
            float radius = 0.3f;
            float thickness = 0.1f;
            int segments = 16;

            w.vertices = new List<Vector3>();
            w.texCoord = new List<Vector2>();
            List<uint> inds = new List<uint>();

            // Два круга: передний и задний
            for (int side = 0; side <= 1; side++)
            {
                float x = xOffset + (side == 0 ? 0 : thickness);

                for (int i = 0; i < segments; i++)
                {
                    float angle = i * MathF.PI * 2f / segments;
                    float z = MathF.Cos(angle) * radius + zOffset;
                    float y = MathF.Sin(angle) * radius + 0.3f;
                    w.vertices.Add(new Vector3(x, y, z));
                    w.texCoord.Add(new Vector2(i / (float)segments, side));
                }
            }

            int offset = 0;

            // Передняя крышка
            for (int i = 1; i < segments - 1; i++)
            {
                inds.Add((uint)offset);
                inds.Add((uint)(offset + i));
                inds.Add((uint)(offset + i + 1));
            }

            offset = segments;

            // Задняя крышка
            for (int i = 1; i < segments - 1; i++)
            {
                inds.Add((uint)offset);
                inds.Add((uint)(offset + i + 1));
                inds.Add((uint)(offset + i));
            }

            // Боковые стенки
            for (int i = 0; i < segments; i++)
            {
                int next = (i + 1) % segments;
                int i0 = i;
                int i1 = next;
                int i2 = segments + next;
                int i3 = segments + i;

                inds.Add((uint)i0);
                inds.Add((uint)i1);
                inds.Add((uint)i2);

                inds.Add((uint)i2);
                inds.Add((uint)i3);
                inds.Add((uint)i0);
            }

            w.indices = inds.ToArray();
            return w;
        }
    }



    class WheelFL : WheelBase // Front Left
    {
        public Data wheel;

        public WheelFL()
        {
            wheel = GenerateWheel(-1.1f, 1.1f);
        }
    }

    class WheelFR : WheelBase // Front Right
    {
        public Data wheel;

        public WheelFR()
        {
            wheel = GenerateWheel(1.1f, 1.1f);
        }
    }

    class WheelRL : WheelBase // Rear Left
    {
        public Data wheel;

        public WheelRL()
        {
            wheel = GenerateWheel(-1.1f, -1.1f);
        }
    }

    class WheelRR : WheelBase // Rear Right
    {
        public Data wheel;

        public WheelRR()
        {
            wheel = GenerateWheel(1.1f, -1.1f);
        }
    }



    class Road
    {
        public Data road;

        public Road()
        {
            float width = 12f;   // Ширина дороги по оси X
            float length = 100f;  // Длина дороги по оси Z


            road = new Data();
            road.vertices = new List<Vector3>()
        {
            new Vector3(-width / 2f, 0f,  length / 2f),
            new Vector3( width / 2f, 0f,  length / 2f),
            new Vector3( width / 2f, 0f, -length / 2f),
            new Vector3(-width / 2f, 0f, -length / 2f)
        };

            road.texCoord = new List<Vector2>()
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1)
        };

            road.indices = new uint[]
            {
            0, 1, 2,
            2, 3, 0
            };
        }
    }


    class Ground
    {
        public Data ground;

        public Ground()
        {
            float size = 40f;
            float yCor = -000.1f;
            ground = new Data();
            ground.vertices = new List<Vector3>()
        {
            new Vector3(-size, yCor,  size),
            new Vector3( size, yCor,  size),
            new Vector3( size, yCor, -size),
            new Vector3(-size, yCor, -size)
        };

            ground.texCoord = new List<Vector2>()
        {
            new Vector2(0, 0),
            new Vector2(10, 0),
            new Vector2(10, 10),
            new Vector2(0, 10)
        };

            ground.indices = new uint[]
            {
            0, 1, 2,
            2, 3, 0
            };
        }
    }




    class TreeTrunk
    {
        public Data trunk;

        public TreeTrunk(Vector3 center, float height = 1.5f, float radius = 0.5f, int segments = 16)
        {
            trunk = new Data();
            trunk.vertices = new List<Vector3>();
            trunk.texCoord = new List<Vector2>();
            List<uint> indices = new List<uint>();

            for (int i = 0; i < segments; i++)
            {
                float angle = 2f * MathF.PI * i / segments;
                float x = MathF.Cos(angle) * radius;
                float z = MathF.Sin(angle) * radius;

                trunk.vertices.Add(new Vector3(center.X + x, center.Y, center.Z + z));             // bottom
                trunk.vertices.Add(new Vector3(center.X + x, center.Y + height, center.Z + z));    // top

                trunk.texCoord.Add(new Vector2(i / (float)segments, 0));
                trunk.texCoord.Add(new Vector2(i / (float)segments, 1));
            }

            for (int i = 0; i < segments; i++)
            {
                int next = (i + 1) % segments;

                indices.Add((uint)(i * 2));
                indices.Add((uint)(next * 2));
                indices.Add((uint)(i * 2 + 1));

                indices.Add((uint)(i * 2 + 1));
                indices.Add((uint)(next * 2));
                indices.Add((uint)(next * 2 + 1));
            }

            trunk.indices = indices.ToArray();
        }
    }




    class TreeLeaves
    {
        public Data leaves;

        public TreeLeaves(Vector3 center, float radius = 1f, int segments = 16)
        {
            leaves = new Data();
            leaves.vertices = new List<Vector3>();
            leaves.texCoord = new List<Vector2>();
            List<uint> indices = new List<uint>();

            for (int y = 0; y <= segments; y++)
            {
                float theta = y * MathF.PI / segments;
                for (int x = 0; x <= segments; x++)
                {
                    float phi = x * 2f * MathF.PI / segments;

                    float dx = MathF.Sin(theta) * MathF.Cos(phi);
                    float dy = MathF.Cos(theta);
                    float dz = MathF.Sin(theta) * MathF.Sin(phi);

                    leaves.vertices.Add(center + new Vector3(dx, dy, dz) * radius);
                    leaves.texCoord.Add(new Vector2(x / (float)segments, y / (float)segments));
                }
            }

            for (int y = 0; y < segments; y++)
            {
                for (int x = 0; x < segments; x++)
                {
                    int i0 = y * (segments + 1) + x;
                    int i1 = i0 + 1;
                    int i2 = i0 + segments + 1;
                    int i3 = i2 + 1;

                    indices.Add((uint)i0);
                    indices.Add((uint)i2);
                    indices.Add((uint)i1);

                    indices.Add((uint)i1);
                    indices.Add((uint)i2);
                    indices.Add((uint)i3);
                }
            }

            leaves.indices = indices.ToArray();
        }
    }

    class Tree
    {
        Data dataTrunk;
        Data dataLeafs;

        public Tree(Data trunk, Data leaves)
        {
            dataTrunk = trunk;
            dataLeafs = leaves;
        }
        /*var trunk = new TreeTrunk(pos).trunk;
                all_data.Add(trunk);
                tex_paths.Add("../../../Textures/BROWN.jpg");

                // Листва (немного выше)
                var leaves = new TreeLeaves(pos + new Vector3(0, 2f, 0)).leaves;
                all_data.Add(leaves);
                tex_paths.Add("../../../Textures/GREEN_TREE.jpg");

                trees.Add(Tree(trunk,leaves));*/

    }

    class SkyBackground
    {
        public Data sky;

        public SkyBackground()
        {

            // 4928 x 1696
            float coeff = 0.005f * 5;
            float width = 4928f*coeff; 
            float height = 1696f*coeff;
            float z = -40f;  // Поставим задний фон чуть дальше, чем конец дороги

            sky = new Data();
            float center = height- height/10;
            sky.vertices = new List<Vector3>()
        {
            new Vector3(-width / 2f, center + height, z),
            new Vector3(width / 2f, center+ height, z),
            new Vector3(width / 2f, center - height, z),
            new Vector3(-width / 2f, center - height, z)
        };

            sky.texCoord = new List<Vector2>()
        {
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),
            new Vector2(0, 0)
        };

            sky.indices = new uint[]
            {
            0, 1, 2,
            2, 3, 0
            };
        }
    }






    class TreePositionGenerator
    {
        private static Random rand = new Random();

        public static List<Vector3> Generate(int count)
        {
            List<Vector3> positions = new List<Vector3>();
            int safetyLimit = 10000;

            while (positions.Count < count && safetyLimit-- > 0)
            {
                float x = rand.NextDouble() < 0.5
                    ? RandomRange(-30f, -7f)
                    : RandomRange(7f, 30f);

                float z = RandomRange(-45f, -2f);
                x = (float)Math.Round(x * 2f) / 2f;
                z = (float)Math.Round(z * 2f) / 2f;

                Vector3 candidate = new Vector3(x, 0, z);

                bool isValid = true;
                foreach (var existing in positions)
                {
                    if (Vector3.Distance(candidate, existing) < 5f)
                    {
                        isValid = false;
                        break;
                    }

                    // Одна линия и слишком близко
                    if (Math.Abs(candidate.X - existing.X) < 0.5f &&
                        Math.Abs(candidate.Z - existing.Z) < 0.5f)
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid)
                    positions.Add(candidate);
            }

            if (positions.Count < count)
            {
                Console.WriteLine($" Не удалось сгенерировать {count} позиций. Итог: {positions.Count}");
            }

            return positions;
        }

        private static float RandomRange(float min, float max)
        {
            return (float)(rand.NextDouble() * (max - min) + min);
        }

        
    }








}
