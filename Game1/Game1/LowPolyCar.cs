using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;
using SharpGLTF.Schema2;

namespace Game1
{



    public static class Extensions
    {
        public static Vector3 ToOpenTK(this System.Numerics.Vector3 v)
            => new Vector3(v.X, v.Y, v.Z);

        public static Vector2 ToOpenTK(this System.Numerics.Vector2 v)
            => new Vector2(v.X, v.Y);

        public static Matrix4 ToOpenTK(this System.Numerics.Matrix4x4 m)
        {
            return new Matrix4(
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44
            );
        }
    }




    public static class ModelLoader
    {

        public static void LoadModel(string path, out List<Vector3> vertices, out List<Vector2> texCoords, out uint[] indices)
        {
            var model = ModelRoot.Load(path);

            vertices = new();
            texCoords = new();
            List<uint> indicesList = new();

            foreach (var node in model.LogicalNodes)
            {
                var mesh = node.Mesh;
                if (mesh == null) continue;

                var transform = node.WorldMatrix;

                foreach (var prim in mesh.Primitives)
                {
                    var posAccessor = prim.GetVertexAccessor("POSITION");
                    var uvAccessor = prim.GetVertexAccessor("TEXCOORD_0");

                    var localVerts = posAccessor.AsVector3Array();
                    var localUVs = uvAccessor?.AsVector2Array();

                    uint baseIndex = (uint)vertices.Count;

                    foreach (var v in localVerts)
                        vertices.Add(Vector3.TransformPosition(v.ToOpenTK(), transform.ToOpenTK()));

                    if (localUVs != null)
                        texCoords.AddRange(localUVs.Select(uv => uv.ToOpenTK()));
                    else
                        texCoords.AddRange(Enumerable.Repeat(Vector2.Zero, localVerts.Count()));

                    foreach (var i in prim.IndexAccessor.AsIndicesArray())
                        indicesList.Add(baseIndex + (uint)i);
                }
            }

            indices = indicesList.ToArray();
        }
    }
}