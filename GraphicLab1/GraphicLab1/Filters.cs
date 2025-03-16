using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
using System.Security.Permissions;
using System.Drawing.Drawing2D;
using System.Configuration;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace GraphicLab1
{
    abstract class Filters
    {






        protected abstract Color calculateNewPixelColor(Bitmap sourceImage, int x, int y);





        public int Clamp(int val, int min, int max)
        {
            if (val < min) { return min; }
            if (val > max) { return max; }
            return val;
        }






        virtual public Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {

            // создаю изображение-результат
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            // обхожу каждый пиксель
            for (int i = 0; i < sourceImage.Width; i++)
            {
                // передаю информацию BackgroudWorker 
                // о состоянии обхода
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));

                // если пользователь нажал "отмена"
                if (worker.CancellationPending)
                {
                    return null;
                }

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }


            return resultImage;
        }


    }


    class InvertFilter : Filters
    {

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {


            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = Color.FromArgb(255 - sourceColor.R,
                                               255 - sourceColor.G,
                                               255 - sourceColor.B);
            return resultColor;
        }
    }



    class GrayFilter : Filters
    {
        /*protected static Color calculateGrayScaleColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            double intensity = 0.299 * sourceColor.R + 0.587 * sourceColor.G + 0.114 * sourceColor.B;
            Color resultColor = Color.FromArgb((int)intensity, (int)intensity, (int)intensity);
            return resultColor;
        }*/





        /*protected static Bitmap makeGrayScaleImage(Bitmap sourceImage)
        {
            // создаю изображение-результат
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            // обхожу каждый пиксель
            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateGrayScaleColor(sourceImage, i, j));
                }
            }
            return resultImage;
        }*/



        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            double intensity = 0.299 * sourceColor.R + 0.587 * sourceColor.G + 0.114 * sourceColor.B;
            Color resultColor = Color.FromArgb((int)intensity, (int)intensity, (int)intensity);
            return resultColor;
        }





        /*// контролирует выход за границы цветового диапазона
        protected static int Clamp(int val, int min, int max)
        {
            if (val < min) { return min; }
            if (val > max) { return max; }
            return val;
        }*/

        /*// переводит изображение в grayWorld
        protected static Bitmap grayWorld(Bitmap sourceImage)
        {

            // средние яркости по всем каналам
            float avgR = 0;
            float avgG = 0;
            float avgB = 0;

            // создаю изображение-результат
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            // обхожу каждый пиксель
            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    avgR += sourceImage.GetPixel(i, j).R;
                    avgG += sourceImage.GetPixel(i, j).G;
                    avgB += sourceImage.GetPixel(i, j).B;
                }
            }

            // кол-во пикселей
            int N = sourceImage.Height * sourceImage.Width;

            // досчитываю средние яркости каналов
            avgR /= N;
            avgG /= N;
            avgB /= N;

            // общая средняя яркость
            float avg = (avgR + avgG + avgB) / 3;


            float sourceR;
            float sourceG;
            float sourceB;

            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    sourceR = sourceImage.GetPixel(i, j).R;
                    sourceG = sourceImage.GetPixel(i, j).G;
                    sourceB = sourceImage.GetPixel(i, j).B;
                    float resultR = (sourceR * avg / avgR);
                    float resultG = (sourceG * avg / avgG);
                    float resultB = (sourceB * avg / avgB);

                    Color resColor;
                    resColor = Color.FromArgb(
                        Clamp((int)resultR, 0, 255),
                        Clamp((int)resultG, 0, 255),
                        Clamp((int)resultB, 0, 255));
                    resultImage.SetPixel(i, j, resColor);
                }
            }



            return resultImage;
        }*/
        /*public Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            return grayWorld(sourceImage);
        }*/

    }



    class SepiaFilter : Filters
    {

        protected static double calculateIntensity(Color sourceColor)
        {
            return 0.299 * sourceColor.R + 0.587 * sourceColor.G + 0.114 * sourceColor.B;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            double intensity = calculateIntensity(sourceColor);
            int k = 20;
            double R = intensity + 2 * k;
            double G = intensity + 0.5 * k;
            double B = intensity - k;
            R = Clamp((int)R, 0, 255);
            G = Clamp((int)G, 0, 255);
            B = Clamp((int)B, 0, 255);
            Color resultColor = Color.FromArgb((int)R, (int)G, (int)B);
            return resultColor;
        }
    }



    class HL20Filter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            int R = Clamp(sourceColor.R + 20, 0, 255);
            int G = Clamp(sourceColor.G + 20, 0, 255);
            int B = Clamp(sourceColor.B + 20, 0, 255);
            Color resultColor = Color.FromArgb(R, G, B);
            return resultColor;
        }
    }




    class Move50Filter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            return sourceImage.GetPixel(x, y);
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {

            // создаю изображение-результат
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);


            // обхожу каждый  начиная с 50 (смещение)
            for (int i = 50; i < sourceImage.Width; i++)
            {
                // передаю информацию BackgroudWorker 
                // о состоянии обхода
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));

                // если пользователь нажал "отмена"
                if (worker.CancellationPending)
                {
                    return null;
                }

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i - 50, j));
                }
            }
            return resultImage;
        }
    }



    class GrayWorldFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            return sourceImage.GetPixel(0, 0);
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            // средние яркости по всем каналам
            float avgR = 0;
            float avgG = 0;
            float avgB = 0;

            // создаю изображение-результат
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            // обхожу каждый пиксель
            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    avgR += sourceImage.GetPixel(i, j).R;
                    avgG += sourceImage.GetPixel(i, j).G;
                    avgB += sourceImage.GetPixel(i, j).B;
                }
            }

            // кол-во пикселей
            int N = sourceImage.Height * sourceImage.Width;

            // досчитываю средние яркости каналов
            avgR /= N;
            avgG /= N;
            avgB /= N;

            // общая средняя яркость
            float avg = (avgR + avgG + avgB) / 3;


            float sourceR;
            float sourceG;
            float sourceB;

            for (int i = 0; i < sourceImage.Width; i++)
            {
                // передаю информацию BackgroudWorker 
                // о состоянии обхода
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));

                // если пользователь нажал "отмена"
                if (worker.CancellationPending)
                {
                    return null;
                }


                for (int j = 0; j < sourceImage.Height; j++)
                {
                    sourceR = sourceImage.GetPixel(i, j).R;
                    sourceG = sourceImage.GetPixel(i, j).G;
                    sourceB = sourceImage.GetPixel(i, j).B;
                    float resultR = (sourceR * avg / avgR);
                    float resultG = (sourceG * avg / avgG);
                    float resultB = (sourceB * avg / avgB);

                    Color resColor;
                    resColor = Color.FromArgb(
                        Clamp((int)resultR, 0, 255),
                        Clamp((int)resultG, 0, 255),
                        Clamp((int)resultB, 0, 255));
                    resultImage.SetPixel(i, j, resColor);
                }
            }
            return resultImage;
        }
    }






    class AutolevelsFilter : Filters
    {

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            return sourceImage.GetPixel(0, 0);
        }

        protected static Color setMin(Color source, Color curr)
        {
            return Color.FromArgb(
                Math.Min(source.R, curr.R),
                Math.Min(source.G, curr.G),
                Math.Min(source.B, curr.B));
        }

        protected static Color setMax(Color source, Color curr)
        {
            return Color.FromArgb(
                Math.Max(source.R, curr.R),
                Math.Max(source.G, curr.G),
                Math.Max(source.B, curr.B));
        }


        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {

            // нахожу минимальный и максимальный цвет
            Color minColor = Color.FromArgb(255, 255, 255);
            Color maxColor = Color.FromArgb(0, 0, 0);

            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color sourceColor = sourceImage.GetPixel(i, j);
                    minColor = setMin(sourceColor, minColor);
                    maxColor = setMax(sourceColor, maxColor);
                }
            }

            // нахожу минимальные и макс интенсивности
            int Rmin = minColor.R;
            int Gmin = minColor.G;
            int Bmin = minColor.B;

            int Rmax = maxColor.R;
            int Gmax = maxColor.G;
            int Bmax = maxColor.B;



            for (int i = 0; i < sourceImage.Width; i++)
            {

                // передаю информацию BackgroudWorker 
                // о состоянии обхода
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));

                // если пользователь нажал "отмена"
                if (worker.CancellationPending)
                {
                    return null;
                }


                for (int j = 0; j < sourceImage.Height; j++)
                {
                    int R = sourceImage.GetPixel(i, j).R;
                    int G = sourceImage.GetPixel(i, j).G;
                    int B = sourceImage.GetPixel(i, j).B;

                    Color resColor = Color.FromArgb(
                    Clamp((int)((R - Rmin) * 255 / (Rmax - Rmin)), 0, 255),
                    Clamp((int)((G - Gmin) * 255 / (Gmax - Gmin)), 0, 255),
                    Clamp((int)((B - Bmin) * 255 / (Bmax - Bmin)), 0, 255)
                        );

                    resultImage.SetPixel(i, j, resColor);
                }
            }



            return resultImage;
        }
    }

    class PerfectReflectorFilter : Filters
    {

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            return sourceImage.GetPixel(0, 0);
        }

        protected static Color setMax(Color source, Color curr)
        {
            return Color.FromArgb(
                Math.Max(source.R, curr.R),
                Math.Max(source.G, curr.G),
                Math.Max(source.B, curr.B));
        }


        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {

            // нахожу максимальный цвет
            Color maxColor = Color.FromArgb(0, 0, 0);

            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color sourceColor = sourceImage.GetPixel(i, j);
                    maxColor = setMax(sourceColor, maxColor);
                }
            }

            // нахожу макс интенсивности

            int Rmax = maxColor.R;
            int Gmax = maxColor.G;
            int Bmax = maxColor.B;



            for (int i = 0; i < sourceImage.Width; i++)
            {

                // передаю информацию BackgroudWorker 
                // о состоянии обхода
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));

                // если пользователь нажал "отмена"
                if (worker.CancellationPending)
                {
                    return null;
                }

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    int R = sourceImage.GetPixel(i, j).R;
                    int G = sourceImage.GetPixel(i, j).G;
                    int B = sourceImage.GetPixel(i, j).B;

                    Color resColor = Color.FromArgb(
                    Clamp((int)(R * 255 / Rmax), 0, 255),
                    Clamp((int)(G * 255 / Gmax), 0, 255),
                    Clamp((int)(B * 255 / Bmax), 0, 255)
                        );

                    resultImage.SetPixel(i, j, resColor);
                }
            }



            return resultImage;
        }
    }




    class MatrixFilter : Filters
    {
        protected float[,] kernel = null;
        protected MatrixFilter() { }

        public MatrixFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }


        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;

            float resultR = 0;
            float resultG = 0;
            float resultB = 0;

            for (int l = -radiusY; l <= radiusX; l++)
            {
                for (int k = -radiusX; k <= radiusY; k++)
                {
                    // вычисляю координаты соседнего пикселя
                    int idx = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idy = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idx, idy);
                    resultR += neighborColor.R * kernel[k + radiusX, l + radiusY];
                    resultG += neighborColor.G * kernel[k + radiusX, l + radiusY];
                    resultB += neighborColor.B * kernel[k + radiusX, l + radiusY];
                }
            }
            return Color.FromArgb(
                Clamp((int)resultR, 0, 255),
                Clamp((int)resultG, 0, 255),
                Clamp((int)resultB, 0, 255));
        }


    }


    class TisnenieFilter : MatrixFilter
    {

        public TisnenieFilter()
        {
            kernel = new float[3, 3] {
            {0, 1, 0},
            {-1, 0, 1},
            {0, -1, 0}
            };
        }
    }



    class BlurFilter : MatrixFilter
    {
        public BlurFilter()
        {
            int sizeX = 3;
            int sizeY = 3;
            kernel = new float[sizeX, sizeY];
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    kernel[i, j] = 1.0f / (float)(sizeX * sizeY);
                }
            }
        }
    }


    class DilationFilter : MatrixFilter
    {
        public DilationFilter()
        {
            kernel = new float[3, 3] {
            {0, 1, 0},
            {1, 0, 1},
            {0, 1, 0}
            };
        }


        protected Color setMax(Color source, Color curr)
        {
            return Color.FromArgb(
                Math.Max(source.R, curr.R),
                Math.Max(source.G, curr.G),
                Math.Max(source.B, curr.B));
        }

        protected static int isOnImage(int val, int min, int max)
        {
            if (val < min) { return 0; }
            if (val > max) { return 0; }
            return 1;
        }

        protected Color maxColorInMask(Bitmap sourceImage, int x, int y, float[,] kernel)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;

            Color maxColor = sourceImage.GetPixel(x, y);

            for (int l = -radiusY; l <= radiusX; l++)
            {
                for (int k = -radiusX; k <= radiusY; k++)
                {
                    // вычисляю координаты соседнего пикселя
                    if (isOnImage(x + k, 0, sourceImage.Width - 1) == 1 &&
                        isOnImage(y + l, 0, sourceImage.Height - 1) == 1 &&
                        kernel[radiusX + k, radiusY + l] == 1)
                    {
                        int idx = Clamp(x + k, 0, sourceImage.Width - 1);
                        int idy = Clamp(y + l, 0, sourceImage.Height - 1);
                        Color neighborColor = sourceImage.GetPixel(idx, idy);

                        maxColor = setMax(maxColor, neighborColor);

                    }


                }
            }

            return maxColor;
        }


        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {

            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {

                // передаю информацию BackgroudWorker 
                // о состоянии обхода
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));

                // если пользователь нажал "отмена"
                if (worker.CancellationPending)
                {
                    return null;
                }

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color maxColor;
                    maxColor = maxColorInMask(sourceImage, i, j, kernel);
                    resultImage.SetPixel(i, j, maxColor);
                }
            }
            return resultImage;
        }

    }

    class ErosionFilter : MatrixFilter
    {

        public ErosionFilter()
        {
            kernel = new float[3, 3] {
            {0, 1, 0},
            {1, 1, 1},
            {0, 1, 0}
            };
        }

        protected static Color setMin(Color source, Color curr)
        {
            return Color.FromArgb(
                Math.Min(source.R, curr.R),
                Math.Min(source.G, curr.G),
                Math.Min(source.B, curr.B));
        }

        protected static int isOnImage(int val, int min, int max)
        {
            if (val < min) { return 0; }
            if (val > max) { return 0; }
            return 1;
        }



        protected Color maxColorInMask(Bitmap sourceImage, int x, int y, float[,] kernel)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;

            Color maxColor = sourceImage.GetPixel(x, y);

            for (int l = -radiusY; l <= radiusX; l++)
            {
                for (int k = -radiusX; k <= radiusY; k++)
                {
                    // вычисляю координаты соседнего пикселя
                    if (isOnImage(x + k, 0, sourceImage.Width - 1) == 1 &&
                        isOnImage(y + l, 0, sourceImage.Height - 1) == 1 &&
                        kernel[radiusX + k, radiusY + l] == 1)
                    {
                        int idx = Clamp(x + k, 0, sourceImage.Width - 1);
                        int idy = Clamp(y + l, 0, sourceImage.Height - 1);
                        Color neighborColor = sourceImage.GetPixel(idx, idy);

                        maxColor = setMin(maxColor, neighborColor);

                    }


                }
            }

            return maxColor;
        }


        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                // передаю информацию BackgroudWorker 
                // о состоянии обхода
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));

                // если пользователь нажал "отмена"
                if (worker.CancellationPending)
                {
                    return null;
                }

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color maxColor;
                    maxColor = maxColorInMask(sourceImage, i, j, kernel);
                    resultImage.SetPixel(i, j, maxColor);
                }
            }
            return resultImage;


        }

    }


    class MedianaFilter : MatrixFilter
    {
        public MedianaFilter()
        {
            kernel = new float[3, 3] {
            {1, 1, 1},
            {1, 1, 1},
            {1, 1, 1}
            };
        }

        protected Color avgColor(Bitmap sourceImage, int x, int y, int radius)
        {
            List<Color> colors = new List<Color>();

            // прохожу по всем цветам в окрестности 
            // и добавляю их в список
            for (int l = -radius; l <= radius; l++)
            {
                for (int k = -radius; k <= radius; k++)
                {
                    // вычисляю координаты соседнего пикселя
                    // проверка нахождения пикселя на изображении

                    int idx = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idy = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idx, idy);
                    colors.Add(neighborColor);




                }
            }

            // создаю отдельные массивы цветов R,G,B
            List<int> Rcolor = new List<int>();
            List<int> Gcolor = new List<int>();
            List<int> Bcolor = new List<int>();
            for (int i = 0; i < colors.Count; i++)
            {
                Rcolor.Add(colors[i].R);
                Gcolor.Add(colors[i].G);
                Bcolor.Add(colors[i].B);
            }

            // сортирую,чтобы найти медиану
            Rcolor.Sort();
            Gcolor.Sort();
            Bcolor.Sort();

            float R;
            float G;
            float B;

            // нахожу медианные цвета
            R = Rcolor[Rcolor.Count / 2];
            G = Gcolor[Gcolor.Count / 2];
            B = Bcolor[Bcolor.Count / 2];
            return Color.FromArgb((int)R, (int)G, (int)B);
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {

            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {

                // передаю информацию BackgroudWorker 
                // о состоянии обхода
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));

                // если пользователь нажал "отмена"
                if (worker.CancellationPending)
                {
                    return null;
                }

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color avgcolor;
                    avgcolor = avgColor(sourceImage, i, j, 1);
                    resultImage.SetPixel(i, j, avgcolor);
                }
            }
            return resultImage;
        }
    }



    class SobelFilter : MatrixFilter
    {

        protected float[,] kernelX = new float[3, 3] {
                    {-1, -2, -1},
                    {0, 0, 0},
                    {1, 2, 1}
               };

        protected float[,] kernelY = new float[3, 3] {
                    {-1, 0, 1},
                    {-2, 0, 2},
                    {-1, 0, 1}
               };

        public SobelFilter()
        {

        }




        protected Color calculateSobelColor(Bitmap sourceImage, int x, int y)
        {

            // радиусы матрицы по x и по y
            int radiusX = kernelX.GetLength(0) / 2;
            int radiusY = kernelX.GetLength(1) / 2;

            // результирующие цвета
            float resultRx = 0;
            float resultGx = 0;
            float resultBx = 0;

            float resultRy = 0;
            float resultGy = 0;
            float resultBy = 0;

            // прохожу по окрестности сверткой
            for (int l = -radiusY; l <= radiusY; l++)
            {
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idx = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idy = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idx, idy);

                    resultRx += neighborColor.R * kernelX[k + radiusX, l + radiusY];
                    resultGx += neighborColor.G * kernelX[k + radiusX, l + radiusY];
                    resultBx += neighborColor.B * kernelX[k + radiusX, l + radiusY];

                    resultRy += neighborColor.R * kernelY[k + radiusX, l + radiusY];
                    resultGy += neighborColor.G * kernelY[k + radiusX, l + radiusY];
                    resultBy += neighborColor.B * kernelY[k + radiusX, l + radiusY];
                }
            }


            int resultR = (int)Math.Sqrt(resultRx * resultRx + resultRy * resultRy);
            int resultG = (int)Math.Sqrt(resultGx * resultGx + resultGy * resultGy);
            int resultB = (int)Math.Sqrt(resultBx * resultBx + resultBy * resultBy);


            return Color.FromArgb(
                Clamp(resultR, 0, 255),
                Clamp(resultG, 0, 255),
                Clamp(resultB, 0, 255));
        }



        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            for (int i = 0; i < sourceImage.Width; i++)
            {

                // передаю информацию BackgroudWorker 
                // о состоянии обхода
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));

                // если пользователь нажал "отмена"
                if (worker.CancellationPending)
                {
                    return null;
                }

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateSobelColor(sourceImage, i, j));
                }
            }
            return resultImage;
        }
    }






    class ScharFiler : SobelFilter
    {
        public ScharFiler()
        {
            kernelX = new float[3, 3] {
                    {-3, -10, -3},
                    {0, 0, 0},
                    {3, 10, 3}
                };

            kernelY = new float[3, 3] {
                    {-3, 0, 3},
                    {-10, 0, 10},
                    {-3, 0, 3}
                };
        }
    }

}

