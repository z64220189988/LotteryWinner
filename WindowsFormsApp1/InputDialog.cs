using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApp1
{
    public class InputDialog : Form
    {
        private TextBox txtNumbers;
        private NumericUpDown numSpecialNumber;
        private Button btnOK;
        private Button btnCancel;


        public InputDialog()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            txtNumbers = new TextBox();
            numSpecialNumber = new NumericUpDown();
            btnOK = new Button();
            btnCancel = new Button();

            // 設定控制元件屬性和事件
            // ...

            // 這裡需要設定控制元件的屬性和事件，以構建您的對話框。

            // 以下是示例：
            txtNumbers.Location = new Point(10, 10);
            txtNumbers.Size = new Size(150, 20);

            numSpecialNumber.Location = new Point(10, 40);

            btnOK.Location = new Point(10, 70);
            btnOK.Click += BtnOK_Click;

            btnCancel.Location = new Point(90, 70);
            btnCancel.Click += BtnCancel_Click;

            // 將控制元件添加到對話框
            Controls.Add(txtNumbers);
            Controls.Add(numSpecialNumber);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            // 在此處處理按下 OK 按鈕的邏輯
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            // 在此處處理按下 Cancel 按鈕的邏輯
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public string GetInputNumbers()
        {
            return txtNumbers.Text;
        }

        public int GetInputSpecialNumber()
        {
            return (int)numSpecialNumber.Value;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // InputDialog
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "InputDialog";
            this.Load += new System.EventHandler(this.InputDialog_Load);
            this.ResumeLayout(false);

        }

        private void InputDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
