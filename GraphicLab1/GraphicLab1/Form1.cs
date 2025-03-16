using System.Drawing.Imaging;

namespace GraphicLab1
{
    public partial class Form1 : Form
    {

        Bitmap image;



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // создаю диалог для открытия файла
            OpenFileDialog dialog = new OpenFileDialog();

            // и фильтрую,чтобы открывались
            // только изображения        
            dialog.Filter = "Image files|*.png;*.jpg;*.bmp|All files(*.*)|*.*";



            // если пользователь выбрал файл
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // image = изображение_выбранное
                image = new Bitmap(dialog.FileName);

                // отображение картинки
                pictureBox1.Image = image;
                pictureBox1.Refresh();

            }
        }

        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new InvertFilter();
            backgroundWorker1.RunWorkerAsync(filter);


            // функция до появление progressBar
            // InvertFilter filter = new InvertFilter();
            // Bitmap resultImage = filter.processImage(image,???);  
            //
            // второго параметра у procesImage не было
            // если передать просто backgroundWorker1
            // то ничего не будет работать
            // ( нет синхронизации )
            //
            // pictureBox1.Image = resultImage;
            // pictureBox1.Refresh();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //  остановление выполнения фильтра
            backgroundWorker1.CancelAsync();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);
            if (backgroundWorker1.CancellationPending != true)
            {
                image = newImage;
            }
        }


        // изменение цвета progressBar
        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }


        //  визуализирует изображение
        //  и обнуляет полоску progressBar
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            progressBar1.Value = 0;


        }

        private void тиснениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new TisnenieFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void blurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }




        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Нет изображения для сохранения.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPEG Image (*.jpg)|*.jpg|PNG Image (*.png)|*.png|BMP Image (*.bmp)|*.bmp|GIF Image (*.gif)|*.gif";
            saveFileDialog.Title = "Сохранить изображение";
            saveFileDialog.DefaultExt = "jpg";
            saveFileDialog.AddExtension = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ImageFormat format = ImageFormat.Jpeg; // Default
                    switch (saveFileDialog.FilterIndex)
                    {
                        case 2:
                            format = ImageFormat.Png;
                            break;
                        case 3:
                            format = ImageFormat.Bmp;
                            break;
                        case 4:
                            format = ImageFormat.Gif;
                            break;
                    }

                    pictureBox1.Image.Save(saveFileDialog.FileName, format);
                    MessageBox.Show("Изображение успешно сохранено.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении изображения: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }






        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SepiaFilter filter = new SepiaFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void увеличениеЯркостиНа20ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HL20Filter filter = new HL20Filter();
            backgroundWorker1.RunWorkerAsync(filter);
        }



        private void сдвигНа50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Move50Filter filter = new Move50Filter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void оттенкиСерогоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GrayFilter filter = new GrayFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void серыйМирToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GrayWorldFilter filter = new GrayWorldFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void autolevelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutolevelsFilter filter = new AutolevelsFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void идеальныйОтражательToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerfectReflectorFilter filter = new PerfectReflectorFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void расширениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DilationFilter filter = new DilationFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сужениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ErosionFilter filter = new ErosionFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void медианаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MedianaFilter filter = new MedianaFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void фильтрСобеляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SobelFilter filter = new SobelFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void фильтрЩарраToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScharFiler filter = new ScharFiler();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void применитьСлучайныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random random = new Random(); 

            int rm = random.Next(16, 256);
            rm /= 16;
            
           

            switch (rm) {
                case 1:
                    InvertFilter filter1 = new InvertFilter();
                    backgroundWorker1.RunWorkerAsync(filter1);
                    break;
                case 2:
                    GrayFilter filter2 = new GrayFilter();
                    backgroundWorker1.RunWorkerAsync(filter2);
                    break;
                case 3:
                    SepiaFilter filter3 = new SepiaFilter();
                    backgroundWorker1.RunWorkerAsync(filter3);
                    break;
                case 4:
                    HL20Filter filter4 = new HL20Filter();
                    backgroundWorker1.RunWorkerAsync(filter4);
                    break;
                case 5:
                    // не интересно
                    //Move50Filter filter5 = new Move50Filter();
                    //backgroundWorker1.RunWorkerAsync(filter5);
                    break;
                case 6:
                    TisnenieFilter filter6 = new TisnenieFilter();
                    backgroundWorker1.RunWorkerAsync(filter6);
                    break;
                case 7:
                    BlurFilter filter7 = new BlurFilter();
                    backgroundWorker1.RunWorkerAsync(filter7);
                    break;
                case 8:
                    GrayWorldFilter filter8 = new GrayWorldFilter();
                    backgroundWorker1.RunWorkerAsync(filter8);
                    break;
                case 9:
                    AutolevelsFilter filter9 = new AutolevelsFilter();
                    backgroundWorker1.RunWorkerAsync(filter9);
                    break;
                case 10:
                    PerfectReflectorFilter filter10 = new PerfectReflectorFilter();
                    backgroundWorker1.RunWorkerAsync(filter10);
                    break;
                case 11:
                    DilationFilter filter11 = new DilationFilter();
                    backgroundWorker1.RunWorkerAsync(filter11);
                    break;
                case 12:
                    ErosionFilter filter = new ErosionFilter();
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;
                case 13:
                    MedianaFilter filter13 = new MedianaFilter();
                    backgroundWorker1.RunWorkerAsync(filter13);
                    return;
                case 14:
                    SobelFilter filter14 = new SobelFilter();                  
                    backgroundWorker1.RunWorkerAsync(filter14);
                    break;
                case 15:
                    ScharFiler filter15 = new ScharFiler();
                    backgroundWorker1.RunWorkerAsync(filter15);
                    break;
                default:
                    break;
            }
        }




    }
}
