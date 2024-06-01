using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NumberSelector
{
    public partial class MainForm : Form
    {
        private Panel panel1_38;

        public MainForm()
        {
            
            InitializePanel();
            InitializeButtons();
        }

        private void panel1_38_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void InitializePanel()
        {
            panel1_38 = new Panel();
            panel1_38.Size = new Size(400, 200);
            panel1_38.Location = new Point(50, 50);
            panel1_38.BorderStyle = BorderStyle.FixedSingle;

            Controls.Add(panel1_38);
        }

        private void InitializeButtons()
        {
            for (int i = 1; i <= 38; i++)
            {
                Button button = new Button();
                button.Size = new Size(50, 50);
                button.Name = "button" + i.ToString("D2");
                button.Text = i.ToString("D2");
                button.ForeColor = Color.Black;
                button.Font = new Font("Microsoft JhengHei", 10, FontStyle.Bold);
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.BackColor = Color.Green;
                button.Click += Button_Click;

                // 設定按鈕的橢圓形外觀
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(0, 0, button.Width, button.Height);
                    button.Region = new Region(path);
                }

                panel1_38.Controls.Add(button);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // 切換按鈕的顏色
            clickedButton.BackColor = (clickedButton.BackColor == Color.Green) ? Color.Red : Color.Green;
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
