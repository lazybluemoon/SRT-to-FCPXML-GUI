namespace SRT_to_FCPXML_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();

            string res = "";
            foreach(string file in openFileDialog1.FileNames)
            {
                textBox1.AppendText(file + Environment.NewLine + Environment.NewLine);
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string list_of_urls = textBox1.Text;
            string[] url_list = list_of_urls.Split(Environment.NewLine + Environment.NewLine);

            foreach(string url in url_list)
            {
                if(url != "")
                {
                    ConversionManager cm = new ConversionManager(url, textBox2.Text);
                    cm.printFile();
                }
            }
            MessageBox.Show("All files are done!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    textBox2.Text = fbd.SelectedPath;
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}