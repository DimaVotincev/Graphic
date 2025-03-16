namespace GraphicLab1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            открытьToolStripMenuItem = new ToolStripMenuItem();
            фильтрыToolStripMenuItem = new ToolStripMenuItem();
            точечныеToolStripMenuItem = new ToolStripMenuItem();
            инверсияToolStripMenuItem = new ToolStripMenuItem();
            оттенкиСерогоToolStripMenuItem = new ToolStripMenuItem();
            сепияToolStripMenuItem = new ToolStripMenuItem();
            увеличениеЯркостиНа20ToolStripMenuItem = new ToolStripMenuItem();
            сдвигНа50ToolStripMenuItem = new ToolStripMenuItem();
            серыйМирToolStripMenuItem = new ToolStripMenuItem();
            autolevelsToolStripMenuItem = new ToolStripMenuItem();
            идеальныйОтражательToolStripMenuItem = new ToolStripMenuItem();
            матричныеToolStripMenuItem = new ToolStripMenuItem();
            тиснениеToolStripMenuItem = new ToolStripMenuItem();
            blurToolStripMenuItem = new ToolStripMenuItem();
            расширениеToolStripMenuItem = new ToolStripMenuItem();
            сужениеToolStripMenuItem = new ToolStripMenuItem();
            медианаToolStripMenuItem = new ToolStripMenuItem();
            фильтрСобеляToolStripMenuItem = new ToolStripMenuItem();
            фильтрЩарраToolStripMenuItem = new ToolStripMenuItem();
            сохранитьToolStripMenuItem = new ToolStripMenuItem();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            progressBar1 = new ProgressBar();
            button1 = new Button();
            применитьСлучайныйToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(0, 31);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(907, 643);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, фильтрыToolStripMenuItem, сохранитьToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(907, 28);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { открытьToolStripMenuItem });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(59, 24);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьToolStripMenuItem
            // 
            открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            открытьToolStripMenuItem.Size = new Size(150, 26);
            открытьToolStripMenuItem.Text = "Открыть";
            открытьToolStripMenuItem.Click += открытьToolStripMenuItem_Click;
            // 
            // фильтрыToolStripMenuItem
            // 
            фильтрыToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { точечныеToolStripMenuItem, матричныеToolStripMenuItem, применитьСлучайныйToolStripMenuItem });
            фильтрыToolStripMenuItem.Name = "фильтрыToolStripMenuItem";
            фильтрыToolStripMenuItem.Size = new Size(85, 24);
            фильтрыToolStripMenuItem.Text = "Фильтры";
            // 
            // точечныеToolStripMenuItem
            // 
            точечныеToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { инверсияToolStripMenuItem, оттенкиСерогоToolStripMenuItem, сепияToolStripMenuItem, увеличениеЯркостиНа20ToolStripMenuItem, сдвигНа50ToolStripMenuItem, серыйМирToolStripMenuItem, autolevelsToolStripMenuItem, идеальныйОтражательToolStripMenuItem });
            точечныеToolStripMenuItem.Name = "точечныеToolStripMenuItem";
            точечныеToolStripMenuItem.Size = new Size(252, 26);
            точечныеToolStripMenuItem.Text = "Точечные";
            // 
            // инверсияToolStripMenuItem
            // 
            инверсияToolStripMenuItem.Name = "инверсияToolStripMenuItem";
            инверсияToolStripMenuItem.Size = new Size(276, 26);
            инверсияToolStripMenuItem.Text = "Инверсия";
            инверсияToolStripMenuItem.Click += инверсияToolStripMenuItem_Click;
            // 
            // оттенкиСерогоToolStripMenuItem
            // 
            оттенкиСерогоToolStripMenuItem.Name = "оттенкиСерогоToolStripMenuItem";
            оттенкиСерогоToolStripMenuItem.Size = new Size(276, 26);
            оттенкиСерогоToolStripMenuItem.Text = "Оттенки серого";
            оттенкиСерогоToolStripMenuItem.Click += оттенкиСерогоToolStripMenuItem_Click;
            // 
            // сепияToolStripMenuItem
            // 
            сепияToolStripMenuItem.Name = "сепияToolStripMenuItem";
            сепияToolStripMenuItem.Size = new Size(276, 26);
            сепияToolStripMenuItem.Text = "Сепия";
            сепияToolStripMenuItem.Click += сепияToolStripMenuItem_Click;
            // 
            // увеличениеЯркостиНа20ToolStripMenuItem
            // 
            увеличениеЯркостиНа20ToolStripMenuItem.Name = "увеличениеЯркостиНа20ToolStripMenuItem";
            увеличениеЯркостиНа20ToolStripMenuItem.Size = new Size(276, 26);
            увеличениеЯркостиНа20ToolStripMenuItem.Text = "Увеличение яркости на 20";
            увеличениеЯркостиНа20ToolStripMenuItem.Click += увеличениеЯркостиНа20ToolStripMenuItem_Click;
            // 
            // сдвигНа50ToolStripMenuItem
            // 
            сдвигНа50ToolStripMenuItem.Name = "сдвигНа50ToolStripMenuItem";
            сдвигНа50ToolStripMenuItem.Size = new Size(276, 26);
            сдвигНа50ToolStripMenuItem.Text = "Сдвиг на 50";
            сдвигНа50ToolStripMenuItem.Click += сдвигНа50ToolStripMenuItem_Click;
            // 
            // серыйМирToolStripMenuItem
            // 
            серыйМирToolStripMenuItem.Name = "серыйМирToolStripMenuItem";
            серыйМирToolStripMenuItem.Size = new Size(276, 26);
            серыйМирToolStripMenuItem.Text = "Серый мир";
            серыйМирToolStripMenuItem.Click += серыйМирToolStripMenuItem_Click;
            // 
            // autolevelsToolStripMenuItem
            // 
            autolevelsToolStripMenuItem.Name = "autolevelsToolStripMenuItem";
            autolevelsToolStripMenuItem.Size = new Size(276, 26);
            autolevelsToolStripMenuItem.Text = "Autolevels";
            autolevelsToolStripMenuItem.Click += autolevelsToolStripMenuItem_Click;
            // 
            // идеальныйОтражательToolStripMenuItem
            // 
            идеальныйОтражательToolStripMenuItem.Name = "идеальныйОтражательToolStripMenuItem";
            идеальныйОтражательToolStripMenuItem.Size = new Size(276, 26);
            идеальныйОтражательToolStripMenuItem.Text = "Идеальный отражатель";
            идеальныйОтражательToolStripMenuItem.Click += идеальныйОтражательToolStripMenuItem_Click;
            // 
            // матричныеToolStripMenuItem
            // 
            матричныеToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { тиснениеToolStripMenuItem, blurToolStripMenuItem, расширениеToolStripMenuItem, сужениеToolStripMenuItem, медианаToolStripMenuItem, фильтрСобеляToolStripMenuItem, фильтрЩарраToolStripMenuItem });
            матричныеToolStripMenuItem.Name = "матричныеToolStripMenuItem";
            матричныеToolStripMenuItem.Size = new Size(252, 26);
            матричныеToolStripMenuItem.Text = "Матричные";
            // 
            // тиснениеToolStripMenuItem
            // 
            тиснениеToolStripMenuItem.Name = "тиснениеToolStripMenuItem";
            тиснениеToolStripMenuItem.Size = new Size(198, 26);
            тиснениеToolStripMenuItem.Text = "Тиснение";
            тиснениеToolStripMenuItem.Click += тиснениеToolStripMenuItem_Click;
            // 
            // blurToolStripMenuItem
            // 
            blurToolStripMenuItem.Name = "blurToolStripMenuItem";
            blurToolStripMenuItem.Size = new Size(198, 26);
            blurToolStripMenuItem.Text = "Blur";
            blurToolStripMenuItem.Click += blurToolStripMenuItem_Click;
            // 
            // расширениеToolStripMenuItem
            // 
            расширениеToolStripMenuItem.Name = "расширениеToolStripMenuItem";
            расширениеToolStripMenuItem.Size = new Size(198, 26);
            расширениеToolStripMenuItem.Text = "Расширение";
            расширениеToolStripMenuItem.Click += расширениеToolStripMenuItem_Click;
            // 
            // сужениеToolStripMenuItem
            // 
            сужениеToolStripMenuItem.Name = "сужениеToolStripMenuItem";
            сужениеToolStripMenuItem.Size = new Size(198, 26);
            сужениеToolStripMenuItem.Text = "Сужение";
            сужениеToolStripMenuItem.Click += сужениеToolStripMenuItem_Click;
            // 
            // медианаToolStripMenuItem
            // 
            медианаToolStripMenuItem.Name = "медианаToolStripMenuItem";
            медианаToolStripMenuItem.Size = new Size(198, 26);
            медианаToolStripMenuItem.Text = "Медиана";
            медианаToolStripMenuItem.Click += медианаToolStripMenuItem_Click;
            // 
            // фильтрСобеляToolStripMenuItem
            // 
            фильтрСобеляToolStripMenuItem.Name = "фильтрСобеляToolStripMenuItem";
            фильтрСобеляToolStripMenuItem.Size = new Size(198, 26);
            фильтрСобеляToolStripMenuItem.Text = "Фильтр Собеля";
            фильтрСобеляToolStripMenuItem.Click += фильтрСобеляToolStripMenuItem_Click;
            // 
            // фильтрЩарраToolStripMenuItem
            // 
            фильтрЩарраToolStripMenuItem.Name = "фильтрЩарраToolStripMenuItem";
            фильтрЩарраToolStripMenuItem.Size = new Size(198, 26);
            фильтрЩарраToolStripMenuItem.Text = "Фильтр Щарра";
            фильтрЩарраToolStripMenuItem.Click += фильтрЩарраToolStripMenuItem_Click;
            // 
            // сохранитьToolStripMenuItem
            // 
            сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            сохранитьToolStripMenuItem.Size = new Size(97, 24);
            сохранитьToolStripMenuItem.Text = "Сохранить";
            сохранитьToolStripMenuItem.Click += сохранитьToolStripMenuItem_Click;
            // 
            // backgroundWorker1
            // 
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 692);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(790, 17);
            progressBar1.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new Point(808, 680);
            button1.Name = "button1";
            button1.Size = new Size(94, 38);
            button1.TabIndex = 4;
            button1.Text = "отмена";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // применитьСлучайныйToolStripMenuItem
            // 
            применитьСлучайныйToolStripMenuItem.Name = "применитьСлучайныйToolStripMenuItem";
            применитьСлучайныйToolStripMenuItem.Size = new Size(252, 26);
            применитьСлучайныйToolStripMenuItem.Text = "Применить случайный";
            применитьСлучайныйToolStripMenuItem.Click += применитьСлучайныйToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(907, 721);
            Controls.Add(button1);
            Controls.Add(progressBar1);
            Controls.Add(pictureBox1);
            Controls.Add(menuStrip1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem открытьToolStripMenuItem;
        private ToolStripMenuItem фильтрыToolStripMenuItem;
        private ToolStripMenuItem точечныеToolStripMenuItem;
        private ToolStripMenuItem инверсияToolStripMenuItem;
        private ToolStripMenuItem матричныеToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private ProgressBar progressBar1;
        private Button button1;
        private ToolStripMenuItem тиснениеToolStripMenuItem;
        private ToolStripMenuItem blurToolStripMenuItem;
        private ToolStripMenuItem сохранитьToolStripMenuItem;
        private ToolStripMenuItem сепияToolStripMenuItem;
        private ToolStripMenuItem увеличениеЯркостиНа20ToolStripMenuItem;
        private ToolStripMenuItem сдвигНа50ToolStripMenuItem;
        private ToolStripMenuItem оттенкиСерогоToolStripMenuItem;
        private ToolStripMenuItem серыйМирToolStripMenuItem;
        private ToolStripMenuItem autolevelsToolStripMenuItem;
        private ToolStripMenuItem идеальныйОтражательToolStripMenuItem;
        private ToolStripMenuItem расширениеToolStripMenuItem;
        private ToolStripMenuItem сужениеToolStripMenuItem;
        private ToolStripMenuItem медианаToolStripMenuItem;
        private ToolStripMenuItem фильтрСобеляToolStripMenuItem;
        private ToolStripMenuItem фильтрЩарраToolStripMenuItem;
        private ToolStripMenuItem применитьСлучайныйToolStripMenuItem;
    }
}
