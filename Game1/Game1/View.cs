using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class View
    {
        











        void InitShaders()
        {
            BasicProgramID = GL.CreateProgram();

            loadShader("..\\..\\raytracing.vert",ShaderType.VertexShader, BasicProgramID,
                out BasicVertexShader);
            loadShader("..\\..\\raytracing.frag", ShaderType.FragmentShader, BasicProgramID,
                out BasicVertexShader);
            GL.LinkProgram(BasicProgramID);

            int status = 0;
            GL.GetProgram(BasicProgramID, GetProgramParameterName.LinkStatus,out status);
            Console.WriteLine(GL.GetProgramInfoLog(BasicProgramID));

        }

        void loadShader(string filename, shadertype type, uint program, out uint address)
        {
            // создаю объект шейдера типа type
            address = GL.CreateShader(type);
            // функция возвращает дескриптор этого объекта

            using (System.IO.StreamReader sr = new System.IO.StreamReader(filename))
            {
                // загружаем исходный код в шейдерный объект
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            // компилирую шейдер address
            GL.CompileShader(address);

            // компоную шейдерную программу
            GL.AttachShader(program, address);



            Console.WriteLine(GL.GetShaderInfoLog(address));

        }




    }
}
