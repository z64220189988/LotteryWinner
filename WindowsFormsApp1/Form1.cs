using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        
        private List<string> combinations = new List<string>();
        private System.Windows.Forms.Timer timer;

        // 在这里声明外部范围中需要使用的变量
       
        ComboBox comboBoxMonth = new ComboBox();
        ComboBox comboBoxDay = new ComboBox();
        ComboBox comboBox星座 = new ComboBox();
        ComboBox comboBox生肖 = new ComboBox();


        public Form1()
        {

            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;//讓視窗預設在正中央
            this.KeyDown += delete_KeyDown; // 添加KeyDown事件处理程序

            //===========================================================//時間顯示用

            // 初始化 Timer
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // 1000 毫秒 = 1 秒
            timer.Tick += Timer_Tick;

            // 啟動 Timer
            timer.Start();

            //===========================================================//多選用

            lboxOutput.SelectionMode = SelectionMode.MultiExtended;

            //===========================================================//點擊事件

            // 將 pnl1_38 中的 label 加入點擊事件
            foreach (Label label in pnl1_38.Controls.OfType<Label>())
            {
                label.Click += pnl1_38_Label_Click;
            }

            // 將 pnl1_8 中的 label 加入點擊事件
            foreach (Label label in pnl1_8.Controls.OfType<Label>())
            {
                label.Click += pnl1_8_Label_Click;
            }
            //=============================================================//
        }

        private void delete_KeyDown(object sender, KeyEventArgs e)
        {
            // 检查用户是否按下了Delete键并且lboxOutput具有焦点
            if (e.KeyCode == Keys.Delete)
            {
                btn刪除所選_Click(sender, e);
                
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 更新 lbl時間顯示 的 Text 屬性為現在的日期和時間
            lbl時間顯示.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        
        }
        //==================================隨機不重複10注===========================================//
        private void btnRamdom10_Click(object sender, EventArgs e)
        {
            // 用來存放已生成的組合
            List<List<int>> generatedCombinations = new List<List<int>>();

            // 生成10組號碼
            for (int i = 0; i < 10; i++)
            {
                List<int> numbers;
                int specialNumber;

                // 確保生成的組合不重複
                do
                {
                    numbers = GenerateUniqueNumbers(1, 38, 6);//min max count整數陣列
                    specialNumber = GenerateUniqueNumbers(1, 8, 1)[0];//整數

                    // 將數字排序
                    numbers.Sort();

                } while (IsCombinationDuplicate(generatedCombinations, numbers, specialNumber));

                // 加入特別號
                numbers.Add(specialNumber);

                // 轉換成字串格式，並輸出到ListBox
                string output = string.Join(",", numbers.Select(num => num.ToString("D2"))) + "," + specialNumber.ToString("D2");
                lboxOutput.Items.Add(output);
                lbl產生注數.Text = $"產生注數 : {lboxOutput.Items.Count} 注";
                lbl產生金額.Text = $"產生金額 : {lboxOutput.Items.Count * 100} 元";

                // 將已生成的組合加入列表
                generatedCombinations.Add(numbers);
            }
        }
        // 產生指定範圍內的指定數量不重複的數字
        private List<int> GenerateUniqueNumbers(int min, int max, int count)
        {
            List<int> numbers = new List<int>();
            Random random = new Random();

            while (numbers.Count < count)
            {
                int number = random.Next(min, max + 1);
                if (!numbers.Contains(number))//如果這個數字還沒有在列表出現
                {
                    numbers.Add(number);//加入這個數字
                }
            }

            return numbers ?? new List<int>(); // 明確指定非 null
        }
        // 檢查生成的組合是否已存在
        private bool IsCombinationDuplicate(List<List<int>> existingCombinations, List<int> numbers, int specialNumber)
        {
            foreach (var combination in existingCombinations)
            {
                // 使用 LINQ 檢查是否相等
                if (combination.Take(6).SequenceEqual(numbers) && combination.Last() == specialNumber)
                {
                    return true; // 已存在相同的組合
                }
            }
            return false; // 組合是唯一的
        }
        //===========================================小包牌==================================================//
        private void btn小包牌_Click(object sender, EventArgs e)
        {



            List<int> numbers = 隨機生成6個不重複的號碼List();


            // 生成 8 組數字串，每一組都搭配不同的特別號
            for (int specialNumber = 1; specialNumber <= 8; specialNumber++)
            {
                // 轉換成字串格式，並輸出到ListBox
                string output = string.Join(",", numbers.Select(num => num.ToString("D2")))
                    + "," + specialNumber.ToString("D2");
                lboxOutput.Items.Add(output);
                lbl產生注數.Text = $"產生注數 : {lboxOutput.Items.Count} 注";
                lbl產生金額.Text = $"產生金額 : {lboxOutput.Items.Count * 100} 元";
            }
        }
        //常用功能****
        List<int> 隨機生成6個不重複的號碼List()
        {
            // 隨機生成 6 個號碼，並排序
            List<int> numbers = GenerateUniqueNumbers(1, 38, 6);
            numbers.Sort();

            return numbers;
        }

        //常用功能****
        private string 隨機生成6個不重複的號碼string()
        {
            // 生成 6 個不重複的隨機數字，範圍在 01 到 38 之間
            Random random = new Random();
            HashSet<int> randomNumbers = new HashSet<int>();
            while (randomNumbers.Count < 6)
            {
                int randomNumber = random.Next(1, 39);
                randomNumbers.Add(randomNumber);
            }

            // 將隨機數字轉換為字串並合併成一個字串
            string randomInput = string.Join(",", randomNumbers);
            return randomInput;
        }
        //================================================小全餐==================================================//
        private void btn小全餐_Click(object sender, EventArgs e)
        {
            // 宣告 ZoneOne 和 ZoneTwo 陣列
            int[] originalZoneOne = Enumerable.Range(1, 38).ToArray();
            int[] ZoneTwo = Enumerable.Range(1, 8).ToArray();

            // 生成 8 組數字串
            for (int i = 0; i < 8; i++)
            {
                // 隨機排序 originalZoneOne 陣列
                int[] ZoneOne = originalZoneOne.OrderBy(x => Guid.NewGuid()).ToArray();

                // 從 ZoneOne 中取前 6 個數字
                int[] selectedNumbers = ZoneOne.Take(6).ToArray();

                // 將數字排序
                Array.Sort(selectedNumbers);

                // 從 ZoneTwo 中取第 i+1 個數字
                int specialNumber = ZoneTwo[i];

                // 轉換成字串格式，並輸出到ListBox
                string output = string.Join(",", selectedNumbers.Select(num => num.ToString("D2")))
                    + "," + specialNumber.ToString("D2");

                lboxOutput.Items.Add(output);
                lbl產生注數.Text = $"產生注數 : {lboxOutput.Items.Count} 注";
                lbl產生金額.Text = $"產生金額 : {lboxOutput.Items.Count * 100} 元";
            }
        }
        //=========================================連碰7=============================================//
        private void btn連碰7_Click(object sender, EventArgs e)
        {
            DialogResult randomInputResult = MessageBox.Show("您想要生成隨機數字嗎？", "隨機數字",MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            string userInput;

            if (randomInputResult == DialogResult.Yes)
            {
                // 生成隨機數字
                userInput = 隨機生成不重複的威力彩string7();
                // 檢查並處理使用者輸入
                string[] inputNumbers = userInput.Split(',');

                if (inputNumbers.Length == 7 && inputNumbers.All(IsNumberInRange))
                {
                    // 計算並輸出組合
                    CalculateCombinations(inputNumbers);
                    OutputCombinations();
                }
            }
            else
            {
                // 讓用戶輸入數字
                userInput = ShowInputDialog("輸入7個數字（01-38，用逗號分隔）：");
                if (!string.IsNullOrEmpty(userInput))
                {
                    // 檢查並處理使用者輸入
                    string[] inputNumbers = userInput.Split(',');

                    if (inputNumbers.Length == 7 && inputNumbers.All(IsNumberInRange))
                    {
                        // 計算並輸出組合
                        CalculateCombinations(inputNumbers);
                        OutputCombinations();
                    }
                    else
                    {
                        MessageBox.Show("請輸入7個數字，用逗號隔開。");
                    }
                }
            }

        }
        private string 隨機生成不重複的威力彩string7()
        {
            // 生成 6 個不重複的隨機數字，範圍在 1 到 38 之間
            Random random = new Random();
            HashSet<int> randomNumbers = new HashSet<int>();
            while (randomNumbers.Count < 6)
            {
                int randomNumber = random.Next(1, 39);
                randomNumbers.Add(randomNumber);
            }

            // 將隨機數字排序
            List<int> sortedNumbers = randomNumbers.OrderBy(num => num).ToList();

            // 生成 1 個 01 到 08 之間的隨機數字
            int specialNumber = random.Next(1, 9);

            // 將特別號補成 2 位數
            string specialNumberString = specialNumber.ToString("D2");

            // 將 6 個號碼轉換為 2 位數字串，並合併成一個字串
            string randomInput = string.Join(",", sortedNumbers.Select(num => num.ToString("D2")));

            // 將特別號添加到最後
            randomInput += "," + specialNumberString;

            return randomInput;
        }
        private bool IsNumberInRange(string number)
        {
            return int.TryParse(number, out int parsedNumber) && parsedNumber >= 1 && parsedNumber <= 38;
        }

        private void CalculateCombinations(string[] inputNumbers)
        {
            combinations.Clear(); // 清空結果

            // 第一區計算 輸入數字取6的組合
            var combinationsFirstArea = Combinations(inputNumbers, 6);

            // 第二區 8 選 1
            var numbersSecondArea = Enumerable.Range(1, 8).Select(x => x.ToString("D2")).ToArray();

            // 組合第一區和第二區
            foreach (var combinationFirstArea in combinationsFirstArea)
            {
                foreach (var numberSecondArea in numbersSecondArea)
                {
                    combinations.Add($"{string.Join(",", combinationFirstArea)},{numberSecondArea}");
                }
            }
        }
        private void OutputCombinations()
        {
            // 將結果輸出到 lboxOutput

            foreach (var combination in combinations)
            {
                lboxOutput.Items.Add(combination);
            }
            lbl產生注數.Text = $"產生注數 : {lboxOutput.Items.Count} 注";
            lbl產生金額.Text = $"產生金額 : {lboxOutput.Items.Count * 100} 元";
        }

        private IEnumerable<IEnumerable<T>> Combinations<T>(T[] elements, int k)
        {
            return k == 0 ? new[] { new T[0] } :
                elements.SelectMany((e, i) =>
                    Combinations(elements.Skip(i + 1).ToArray(), k - 1).Select(c => (new[] { e }).Concat(c)));
        }
        // 顯示輸入對話框
        private string ShowInputDialog(string prompt)
        {
            Form promptForm = new Form()
            {
                Width = 400,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = prompt,
                StartPosition = FormStartPosition.CenterScreen
            };

            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 300 };
            Button confirmation = new Button() { Text = "確定", Left = 250, Width = 100, Top = 70 };

            // 在 TextBox 的 KeyDown 事件中處理 Enter 鍵
            textBox.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    promptForm.Close();
                }
            };

            confirmation.Click += (sender, e) => { promptForm.Close(); };

            promptForm.Controls.Add(textBox);
            promptForm.Controls.Add(confirmation);

            // 強制切換輸入法為英文
            textBox.Enter += (sender, e) => { InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("en-US")); };

            promptForm.ShowDialog();

            string userInput = textBox.Text.Trim(); // 获取用户输入的内容并去除首尾空格


            if (!string.IsNullOrEmpty(userInput))
            {
                // 使用 LINQ 检查每个字符串是否是有效的数字
                bool isValidInput = userInput.Split(',').All(num => int.TryParse(num, out _));

                if (isValidInput)
                {
                    // 将每个数字转换为2位数
                    string[] inputNumbers = userInput.Split(',').Select(num => int.Parse(num).ToString("D2")).ToArray();
                    return string.Join(",", inputNumbers);
                }
                else
                {
                    MessageBox.Show("請輸入7位數字，用逗點隔開。");
                }
            }
            else
            {
                MessageBox.Show("未提供任何數字。");
            }

            return string.Empty; // 返回空字符串表示无效的输入
        }
        //=========================================連碰8=============================================//
        private void btn連碰8_Click(object sender, EventArgs e)
        {

            DialogResult randomInputResult = MessageBox.Show("您想要生成隨機數字嗎？", "隨機數字", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            string userInput;

            if (randomInputResult == DialogResult.Yes)
            {
                // 生成隨機數字
                userInput = 隨機生成不重複的威力彩string8();
                // 檢查並處理使用者輸入
                string[] inputNumbers = userInput.Split(',');

                if (inputNumbers.Length == 8 && inputNumbers.All(IsNumberInRange))
                {
                    // 計算並輸出組合
                    CalculateCombinations8(inputNumbers);
                    OutputCombinations();
                }
            }
            else
            {
                // 讓用戶輸入數字
                userInput = ShowInputDialog("輸入8個數字（01-38，用逗號分隔）：");
                if (!string.IsNullOrEmpty(userInput))
                {
                    // 檢查並處理使用者輸入
                    string[] inputNumbers = userInput.Split(',');

                    if (inputNumbers.Length == 8 && inputNumbers.All(IsNumberInRange))
                    {
                        // 計算並輸出組合
                        CalculateCombinations(inputNumbers);
                        OutputCombinations();
                    }
                    else
                    {
                        MessageBox.Show("請輸入8個數字，用逗號隔開。");
                    }
                }
            }

        }

        private string 隨機生成不重複的威力彩string8()
        {
            // 生成 7 個不重複的隨機數字，範圍在 1 到 38 之間
            Random random = new Random();
            HashSet<int> randomNumbers = new HashSet<int>();
            while (randomNumbers.Count < 7)
            {
                int randomNumber = random.Next(1, 39);
                randomNumbers.Add(randomNumber);
            }

            // 將隨機數字排序
            List<int> sortedNumbers = randomNumbers.OrderBy(num => num).ToList();

            // 生成 1 個 01 到 08 之間的隨機數字
            int specialNumber = random.Next(1, 9);

            // 將特別號補成 2 位數
            string specialNumberString = specialNumber.ToString("D2");

            // 將 6 個號碼轉換為 2 位數字串，並合併成一個字串
            string randomInput = string.Join(",", sortedNumbers.Select(num => num.ToString("D2")));

            // 將特別號添加到最後
            randomInput += "," + specialNumberString;

            return randomInput;
        }

        private void CalculateCombinations8(string[] inputNumbers)
        {
            combinations.Clear(); // 清空結果

            // 第一區計算 C8 取 6 的所有組合
            var combinationsFirstArea = Combinations(inputNumbers, 6);

            // 第二區 8 選 1
            var numbersSecondArea = Enumerable.Range(1, 8).Select(x => x.ToString("D2")).ToArray();

            // 組合第一區和第二區
            foreach (var combinationFirstArea in combinationsFirstArea)
            {
                foreach (var numberSecondArea in numbersSecondArea)
                {
                    combinations.Add($"{string.Join(",", combinationFirstArea)},{numberSecondArea}");
                }
            }
        }

        //=========================================加入兌獎器=============================================//

        private void btn加入兌獎器_Click(object sender, EventArgs e)
        {
            // 檢查 pnl1_8 中是否有選擇號碼
            bool isSpecialNumberSelected = pnl1_8.Controls.OfType<Label>().Any(label => label.BackColor == Color.LightPink);

            // 檢查 pnl1_38 中已點選的號碼數量是否為 6
            int selectedCount = pnl1_38.Controls.OfType<Label>().Count(label => label.BackColor == Color.LightGreen);

            // 判斷是否滿足條件
            if (!isSpecialNumberSelected && selectedCount != 6)
            {
                MessageBox.Show("第一區請點選6個號碼！\n第二區請點選1個號碼！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // 中止執行
            }

            if (!isSpecialNumberSelected)
            {
                MessageBox.Show("第二區請點選1個號碼！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // 中止執行
            }

            if (selectedCount != 6)
            {
                MessageBox.Show("第一區請點選6個號碼！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // 中止執行
            }
            // 讀取 pnl1_38 中被選擇的 label 的 text
            List<int> selectedNumbers = GetSelectedNumbers(pnl1_38, Color.LightGreen, 6);

            // 在 pnl1_8 中讀取被選擇的 label 的 text
            int specialNumber = GetSelectedNumbers(pnl1_8, Color.LightPink, 1).FirstOrDefault();

            // 排序已選擇的號碼
            selectedNumbers.Sort();

            // 輸出到 lbox待兌獎
            string outputString = $"{string.Join(",", selectedNumbers.Select(num => num.ToString("D2")))},{specialNumber:D2}";
            Console.WriteLine(outputString);
            lbox待兌獎.Items.Add(outputString);


            // 將所有已選號碼的顏色變回原色
            ResetSelectedNumbersColor(pnl1_38);
            ResetSelectedNumbersColor(pnl1_8);
        }

        private void ResetSelectedNumbersColor(Panel panel)
        {
            foreach (Label label in panel.Controls.OfType<Label>())
            {
                label.BackColor = panel == pnl1_38 ? Color.Green : Color.FromArgb(192, 0, 0);
                label.BorderStyle = BorderStyle.None;
                label.Font = new Font(label.Font, FontStyle.Regular);
            }
        }
        private List<int> GetSelectedNumbers(Panel panel, Color color, int count)
        {
            List<Label> selectedLabels = panel.Controls.OfType<Label>().Where(label => label.BackColor == color).ToList();
            List<int> selectedNumbers = selectedLabels.Select(label => int.Parse(label.Text)).OrderBy(num => num).ToList();
            return selectedNumbers;
        }

        private void InitializeLabels()//初始化顏色
        {
            foreach (Label label in pnl1_38.Controls.OfType<Label>())
            {
                label.Click += Label_Click;
                label.BackColor = Color.Green;
            }

            foreach (Label label in pnl1_8.Controls.OfType<Label>())
            {
                label.Click += Label_Click;
                label.BackColor = Color.FromArgb(192, 0, 0);
            }
        }
        private void Label_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            // 檢查已選擇的數字數量是否已達到上限
            int selectedCount = pnl1_38.Controls.OfType<Label>().Count(label => label.BackColor == Color.LightGreen)
                + pnl1_8.Controls.OfType<Label>().Count(label => label.BackColor == Color.LightPink);

            // 切換 label 的背景顏色
            if (selectedCount < 6 || (clickedLabel.Parent == pnl1_8 && selectedCount < 7))
            {
                if (clickedLabel.BackColor == Color.LightGreen)
                {
                    clickedLabel.BackColor = Color.Green;
                }
                else if (clickedLabel.BackColor == Color.LightPink)
                {
                    clickedLabel.BackColor = Color.FromArgb(192, 0, 0);
                }
                else
                {
                    clickedLabel.BackColor = clickedLabel.Parent == pnl1_38 ? Color.LightGreen : Color.LightPink;
                }
            }
        }
        private void pnl1_38_Label_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            // 檢查已選擇的數字數量是否已達到上限（6個）
            int selectedCount = pnl1_38.Controls.OfType<Label>().Count(label => label.BackColor == Color.LightGreen);

            // 切換 label 的背景顏色（淺綠色和綠色交替）
            if (selectedCount < 6)
            {
                if (clickedLabel.BackColor == Color.LightGreen)
                {
                    clickedLabel.BackColor = Color.Green; // 或其他你想設定的顏色
                }
                else
                {
                    clickedLabel.BackColor = Color.LightGreen;
                }
            }
        }
        private void pnl1_8_Label_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            // 檢查已選擇的數字數量是否已達到上限（1個）
            int selectedCount = pnl1_8.Controls.OfType<Label>().Count(label => label.BackColor == Color.LightPink);

            // 切換 label 的背景顏色（紅色和淺粉紅色交替）
            if (selectedCount < 1)
            {
                if (clickedLabel.BackColor == Color.LightPink)
                {
                    clickedLabel.BackColor = Color.FromArgb(192, 0, 0); // 或其他你想設定的顏色
                }
                else
                {
                    clickedLabel.BackColor = Color.LightPink;
                }
            }
            else
            {
                // 若已達到上限，點擊後變回原色
                clickedLabel.BackColor = Color.FromArgb(192, 0, 0); // 或其他你想設定的顏色
            }
        }
        //=========================================包牌匯出=============================================//
        private void btn包牌匯出_Click(object sender, EventArgs e)
        {
            // 將 lboxOutput 中的資料匯出到檔案
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "文字檔 (*.txt)|*.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 複製 lboxOutput 中的項目到一個新的 List<string>
                List<string> itemsToExport = lboxOutput.Items.Cast<string>().ToList();

                // 提示使用者匯出成功，詢問是否清空畫面
                DialogResult result = MessageBox.Show("匯出成功，是否清空目前數字組合？", "匯出確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // 清空 lboxOutput 中的所有項目
                    lboxOutput.Items.Clear();
                    lbl產生注數.Text = $"產生注數 : {lboxOutput.Items.Count} 注";
                    lbl產生金額.Text = $"產生金額 : {lboxOutput.Items.Count * 100} 元";

                    MessageBox.Show("已清空畫面！");
                }

                // 將複製的項目寫入檔案
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    foreach (string item in itemsToExport)
                    {
                        writer.WriteLine(item);
                    }
                }

                if (result == DialogResult.No)
                {
                    MessageBox.Show("已保留數字組合！");
                }
            }
        }
        //=========================================包牌匯入=============================================//

        private void btn包牌匯入_Click(object sender, EventArgs e)
        {
            // 創建一個自定義對話框以選擇要匯入的 ListBox
            using (var dialog = new Form())
            {
                dialog.Text = "選擇匯入到哪個表單";
                dialog.Size = new Size(300, 180);
                dialog.StartPosition = FormStartPosition.CenterScreen;

                var label = new Label();
                label.Text = "匯入到:";
                label.Location = new Point(50, 20);
                dialog.Controls.Add(label);

                var comboBox = new ComboBox();
                comboBox.Items.Add("多功能清單");
                comboBox.Items.Add("兌獎區清單");
                comboBox.Location = new Point(20, 50);
                dialog.Controls.Add(comboBox);

                var confirmButton = new Button();
                confirmButton.Text = "確定";
                confirmButton.Location = new Point(20, 90);
                confirmButton.Click += (s, ev) =>
                {
                    int selectedIndex = comboBox.SelectedIndex;

                    if (selectedIndex != -1)
                    {
                        string selectedListBoxName = comboBox.Items[selectedIndex].ToString();
                        ListBox selectedListBox = selectedListBoxName == "多功能清單" ? lboxOutput : lbox待兌獎;

                        // 從文件匯入數據
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Filter = "文字檔 (*.txt)|*.txt";

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            List<string> importedData = new List<string>();

                            using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                            {
                                string line;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    importedData.Add(line);
                                }
                            }

                            // 將匯入的數據添加到所選的 ListBox
                            selectedListBox.Items.AddRange(importedData.ToArray());

                            MessageBox.Show("匯入成功！");
                        }

                        dialog.Close(); // 關閉自定義對話框
                    }
                    else
                    {
                        MessageBox.Show("請選擇要匯入的 ListBox！");
                    }
                };
                dialog.Controls.Add(confirmButton);

                // 顯示自定義對話框
                dialog.ShowDialog();
            }
        }
        //===========================開獎號碼====================================/
        private void btn開獎號碼_Click(object sender, EventArgs e)
        {
            // 產生 1 到 38 的所有數字
            List<int> numbers = Enumerable.Range(1, 38).ToList();

            // 隨機排序數字
            Random random = new Random();
            numbers = numbers.OrderBy(num => random.Next()).ToList();

            // 取前 6 個數字並進行排序
            List<int> selectedNumbers = numbers.Take(6).OrderBy(num => num).ToList();

            // 設定 Label 獎號 1 到 6 的文字
            for (int i = 0; i < 6; i++)
            {
                string labelName = "label獎號" + (i + 1);
                Label label = Controls.Find(labelName, true).FirstOrDefault() as Label;

                if (label != null)
                {
                    label.Text = selectedNumbers[i].ToString("D2");
                }
            }

            // 產生 1 到 08 的特別號
            int specialNumber = random.Next(1, 9);
            label獎號S.Text = specialNumber.ToString("D2");
        }
        //===========================獎號歸零====================================/
        private void btn獎號歸零_Click(object sender, EventArgs e)
        {
            // 設定 Label 獎號 1 到 6 的文字為 "-"
            for (int i = 0; i < 6; i++)
            {
                string labelName = "label獎號" + (i + 1);
                Label label = Controls.Find(labelName, true).FirstOrDefault() as Label;

                if (label != null)
                {
                    label.Text = "0";
                }
            }

            // 設定 Label 獎號_特 的文字為 "-"
            label獎號S.Text = "0";
        }
        //===========================隨機選號====================================/
        private List<string> outputStrings = new List<string>(); // 新增一個 List 來存儲輸出字串

        private void btn隨機選號_Click(object sender, EventArgs e)
        {
            // 隨機挑選 panel1_38 中的 6 個 Label 變為藍色
            RandomSelectLabels(pnl1_38, Color.LightGreen, 6);

            // 隨機挑選 pnl1_8 中的 1 個 Label 變為橘色
            RandomSelectLabels(pnl1_8, Color.LightPink, 1);

            // 整理號碼並輸出到 lboxOutput
            OutputRandomSelection();
        }
        private void RandomSelectLabels(Panel panel, Color selectedColor, int count)
        {
            Random random = new Random();
            List<Label> labelList = panel.Controls.OfType<Label>().ToList();

            // 將所有 Label 恢復為原色，取消原有的外框
            foreach (Label label in labelList)
            {
                label.BackColor = panel == pnl1_38 ? Color.Green : Color.FromArgb(192, 0, 0);
                label.BorderStyle = BorderStyle.None;
                label.Font = new Font(label.Font, FontStyle.Regular); // 取消加粗
            }

            // 隨機挑選指定數量的 Label 變色
            for (int i = 0; i < count; i++)
            {
                int randomIndex = random.Next(labelList.Count);
                labelList[randomIndex].BackColor = selectedColor;
                labelList[randomIndex].BorderStyle = BorderStyle.FixedSingle;
                labelList[randomIndex].Font = new Font(labelList[randomIndex].Font, FontStyle.Bold); // 加粗 2pt
                labelList.RemoveAt(randomIndex);
            }
        }
        private void OutputRandomSelection()
        {
            // 從 panel1_38 中取得藍色 Label 的號碼
            string firstAreaNumbers = GetSelectedNumbers(pnl1_38, Color.LightGreen);

            // 從 pnl1_8 中取得橘色 Label 的號碼
            string secondAreaNumbers = GetSelectedNumbers(pnl1_8, Color.LightPink);

            // 輸出格式為 no1,no2,no3,no4,no5,no6 特別號 : X
            string outputString = $"{firstAreaNumbers},{secondAreaNumbers}";

            // 輸出到 lboxOutput
            outputStrings.Add(outputString);
            lboxOutput.Items.AddRange(outputStrings.ToArray());
            lbl產生注數.Text = $"產生注數 : {lboxOutput.Items.Count} 注";
            lbl產生金額.Text = $"產生金額 : {lboxOutput.Items.Count * 100} 元";
            outputStrings.Clear(); // 清空 List
        }
        private string GetSelectedNumbers(Panel panel, Color color)
        {
            List<Label> selectedLabels = panel.Controls.OfType<Label>().Where(label => label.BackColor == color).ToList();
            List<string> selectedNumbers = selectedLabels.Select(label =>
           {
            if (int.TryParse(label.Text, out int num))
            {
               return num.ToString("D2");
            }
            else
            {
               Console.WriteLine($"Failed to parse {label.Text} as an integer.");
               return null;
            }
           }
          )
            .Where(num => num != null)//Where方法过滤掉解析失败的文本
            .OrderBy(num => num)//OrderBy方法按升序对整数字符串进行排序
            .ToList();


            return string.Join(",", selectedNumbers);
        }

        //===========================還原選號====================================/
        private void btn還原選號_Click(object sender, EventArgs e)
        {
            // 將 panel1_38 中所有 Label 還原為綠色，取消外框
            RestoreSelection(pnl1_38);

            // 將 pnl1_8 中所有 Label 還原為紅色，取消外框
            RestoreSelection(pnl1_8);

            lbl產生注數.Text = $"產生注數 : {lboxOutput.Items.Count} 注";
            lbl產生金額.Text = $"產生金額 : {lboxOutput.Items.Count * 100} 元";
        }

        private void RestoreSelection(Panel panel)
        {
            List<Label> labelList = panel.Controls.OfType<Label>().ToList();

            // 將所有 Label 還原為原色，取消外框，並將字體設定為不是粗體的微軟正黑體
            foreach (Label label in labelList)
            {
                label.BackColor = panel == pnl1_38 ? Color.Green : Color.FromArgb(192, 0, 0);
                label.BorderStyle = BorderStyle.None;

                // 創建微軟正黑體字體，非粗體
                Font newFont = new Font("Microsoft JhengHei", label.Font.Size, FontStyle.Regular);
                label.Font = newFont;
            }
        }

        //==========================刪除所選==============================================/
        private void btn刪除所選_Click(object sender, EventArgs e)
        {
            
           

            // 確認是否有選中的項目
            if (lboxOutput.SelectedItems.Count > 0)
            {
                // 將被選中的項目從 lboxOutput 中移除
                foreach (var selectedItem in lboxOutput.SelectedItems.Cast<string>().ToList())
                {
                    lboxOutput.Items.Remove(selectedItem);
                }

                
            }
            else if (lbox待兌獎.SelectedItems.Count > 0)
            {
                // 在 lbox待兌獎 上执行删除操作
                foreach (var selectedItem in lbox待兌獎.SelectedItems.Cast<string>().ToList())
                {
                    lbox待兌獎.Items.Remove(selectedItem);
                }

               
            }
            else
            {
                MessageBox.Show("請先選擇要刪除的項目！");
            }
            lbl產生注數.Text = $"產生注數 : {lboxOutput.Items.Count} 注";
            lbl產生金額.Text = $"產生金額 : {lboxOutput.Items.Count * 100} 元";
        }

        //==========================全部刪除==============================================/
        private void btn全部刪除_Click(object sender, EventArgs e)
        {
            // 清空 lboxOutput 中的所有項目
            lboxOutput.Items.Clear();
            lbl產生注數.Text = $"產生注數 : {lboxOutput.Items.Count} 注";
            lbl產生金額.Text = $"產生金額 : {lboxOutput.Items.Count * 100} 元";
            
        }

        //==========================中獎率說明==============================================/
        private void btn中獎率說明_Click(object sender, EventArgs e)
        {

            lboxOutput.Items.Clear();
            lbl產生注數.Text = $"產生注數 : {lboxOutput.Items.Count} 注";
            lbl產生金額.Text = $"產生金額 : {lboxOutput.Items.Count * 100} 元";

            // 分割訊息為多行
            string[] messageLines =
            {
    "威力彩頭獎中獎率約1/2,209萬",
    "威力彩二獎中獎率約1/315萬",
    "", // 空行
    "威力彩三獎中獎率約1/11萬",
    "威力彩四獎中獎率約1/1.6萬",
    "威力彩五獎中獎率約1/3000",
    "威力彩六獎中獎率約1/424",
    "威力彩七獎中獎率約1/223",
    "威力彩八獎中獎率約1/41",
    "威力彩玖獎中獎率約1/32",
    "威力彩普獎中獎率約1/18",
    "", // 空行
    "總中獎率約1/9",
    "", // 空行
    "(獎金期望值 = 頭獎獎金 + 23000000)/2200000000",
    "此僅為約略估計！",
    "", // 空行
    "獎金未累積時頭獎僅2億，期望值僅10元！",
    "考慮購買成本$100元的話，期望值為-90",
    "購買前三思。"
};

            // 將每行添加到 ListBox
            foreach (var line in messageLines)
            {
                lboxOutput.Items.Add(line);
            }

        }
        //=============================扣稅說明===========================================/
        private void btn扣稅說明_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("單筆中獎獎金新台幣 5000 以上(含)\n需扣除 百分之二十(20%)的所得稅 及 千分之四(0.4%)的印花稅\n\n即實領金額 = 中獎金額 x 0.796 ");

            lboxOutput.Items.Clear();
            lbl產生注數.Text = $"產生注數 : {lboxOutput.Items.Count} 注";
            lbl產生金額.Text = $"產生金額 : {lboxOutput.Items.Count * 100} 元";


            // 分割訊息為多行
            string[] messageLines =
            {
    "單筆中獎獎金新台幣 5000 以上(含)",
    "",
    "需扣除",
    "百分之二十(20%)的所得稅",
    "及",
    "千分之四(0.4%)的印花稅",
    "", // 空行
    "即實領金額 = 中獎金額 x 0.796"
};

            // 將每行添加到 ListBox
            foreach (var line in messageLines)
            {
                lboxOutput.Items.Add(line);
            }

        }
        //=============================威力彩必勝口訣===========================================/
        private void btn威力彩必勝口訣_Click(object sender, EventArgs e)
        {

            try
            {
                // 設定檔案路徑，請替換成您的檔案名稱
                string filePath = Path.Combine(Application.StartupPath, "Resources", "不要買.txt");

                // 讀取文本文件中的所有行
                string[] lines = File.ReadAllLines(filePath);

                // 清空 ListBox 的現有項目
                lboxOutput.Items.Clear();
                lbl產生注數.Text = $"產生注數 : {lboxOutput.Items.Count} 注";
                lbl產生金額.Text = $"產生金額 : {lboxOutput.Items.Count * 100} 元";

                // 將每一行添加到 ListBox 中
                foreach (string line in lines)
                {
                    lboxOutput.Items.Add(line);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("讀取檔案時發生錯誤：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //============================包牌介紹============================================/
        private void btn包牌介紹_Click(object sender, EventArgs e)
        {

            lboxOutput.Items.Clear();
            lbl產生注數.Text = $"產生注數 : {lboxOutput.Items.Count} 注";
            lbl產生金額.Text = $"產生金額 : {lboxOutput.Items.Count * 100} 元";

            // 分割訊息為多行
            string[] messageLines =
            {
        "1.電選不重複10注 :",
        "",
        " 隨機選擇不重複的數字組合10注",
        " 售價 : 1000元。",
        "", // 空行
        "2.小包牌 :",
        "",
        " 第一區電腦選號一組固定",
        " 第二區特別號01~08都有",
        " 一組8注，售價 : 800元。",
        "", // 空行
        "3.小全餐 :",
        "",
        " 第一區電腦選號8組皆不重複",
        " 第二區特別號01~08都有",
        " 一組8注，售價 : 800元",
        "", // 空行
        "4.連碰7 :",
        "",
        " 第一區選取7顆，產生7取6的7種組合",
        " 第二區特別號01~08都有",
        " 一組56注，售價 5600 元",
        "", // 空行
        "5.連碰8 :",
        "",
        " 第一區選取8顆，產生8取6的28種組合",
        " 第二區特別號01~08都有",
        " 一組224注，售價 22400 元"
};

            // 將每行添加到 ListBox
            foreach (var line in messageLines)
            {
                lboxOutput.Items.Add(line);
            }

        }
        //========================================================================/
        private void lbox待兌獎_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //========================================================================/
        private void lboxOutput_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //==========================選擇困難器==============================================/
        private void btn選擇困難器_Click(object sender, EventArgs e)
        {
            // 取得系統時間的秒數
            int seconds = DateTime.Now.Second;

            // 計算餘數
            int remainder = seconds % 5;

            // 顯示方法名稱
            string methodName = "";

            // 根據餘數執行對應的方法
            switch (remainder)
            {
                case 0:
                    methodName = "電選不重複10注$1000";
                    // 顯示方法名稱
                    MessageBox.Show("替你選擇了：" + methodName, "選擇困難器");
                    btnRamdom10_Click(sender, e);
                    break;
                case 1:
                    methodName = "小包牌(固定+包特別號)$800";
                    // 顯示方法名稱
                    MessageBox.Show("替你選擇了：" + methodName, "選擇困難器");
                    btn小包牌_Click(sender, e);
                    break;
                case 2:
                    methodName = "小全餐(電選+包特別號)$800";
                    // 顯示方法名稱
                    MessageBox.Show("替你選擇了：" + methodName, "選擇困難器");
                    btn小全餐_Click(sender, e);
                    break;
                case 3:
                    methodName = "連碰7(選7+包特別號)$5600";
                    // 顯示方法名稱
                    MessageBox.Show("替你選擇了：" + methodName, "選擇困難器");
                    btn連碰7_Click(sender, e);
                    break;
                case 4:
                    methodName = "連碰8(選8+包特別號)$22400";
                    // 顯示方法名稱
                    MessageBox.Show("替你選擇了：" + methodName, "選擇困難器");
                    btn連碰8_Click(sender, e);
                    break;
                default:
                    // 如果需要，可以在這裡處理其他的情況
                    break;
            }


        }


        //=============================兌獎輸出===========================================/


        private void btn兌獎輸出_Click(object sender, EventArgs e)
        {
            lboxOutput.Items.Clear();
            OutputCombinations();

            // 檢查開獎號碼是否有效
            List<int> 開獎號碼 = 取得開獎號碼();
            bool containsElement = 開獎號碼.Contains(0);
            if (containsElement == true)
            {
                DialogResult result = MessageBox.Show("先按一下開獎號碼!\n\n是否要幫你按?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    btn開獎號碼_Click(sender, e);
                }

            }
            if (lbox待兌獎.Items.Count == 0)
            {
                MessageBox.Show("1.先點選號碼\n再點擊[選號加入下方兌獎器] 按鈕！\n\n或\n\n若已有包牌清單\n按右下方[本單加入兌獎器]按鈕\n將你的包牌組合加入兌獎器！");
            }
            else
            {



                // 接續執行後面的程式碼
                List<List<int>> 多組待兌獎號碼們 = 取得待兌獎號碼();
                List<Tuple<List<string>, List<long>>> 中獎結果 = 檢查是否中獎(多組待兌獎號碼們, 開獎號碼);

                // 將所有中獎獎名合併為一個列表
                List<string> 所有中獎獎名 = 中獎結果.SelectMany(t => t.Item1).ToList();

                List<long> 獎金金額 = 查詢獎金(所有中獎獎名);

                顯示中獎結果(中獎結果, 多組待兌獎號碼們);
            }
        }


        // 子方法1: 得到開獎號碼與待兌獎號碼

        private List<int> 取得開獎號碼()
        {
            // 處理開獎號碼
            List<int> 開獎號碼們 = new List<int>();

            try
            {
                int 獎號1 = int.Parse(label獎號1.Text);
                int 獎號2 = int.Parse(label獎號2.Text);
                int 獎號3 = int.Parse(label獎號3.Text);
                int 獎號4 = int.Parse(label獎號4.Text);
                int 獎號5 = int.Parse(label獎號5.Text);
                int 獎號6 = int.Parse(label獎號6.Text);

                // 將數字添加到開獎號碼列表
                開獎號碼們.Add(獎號1);
                開獎號碼們.Add(獎號2);
                開獎號碼們.Add(獎號3);
                開獎號碼們.Add(獎號4);
                開獎號碼們.Add(獎號5);
                開獎號碼們.Add(獎號6);

                // 排序開獎號碼
                開獎號碼們.Sort();

                int 獎號S = int.Parse(label獎號S.Text);

                // 將特別號添加到排序後的列表最後
                開獎號碼們.Add(獎號S);

                // 除錯輸出
                //Console.WriteLine("開獎號碼 : " + String.Join(", ", 開獎號碼們));
            }
            catch (FormatException)
            {
                MessageBox.Show("請先點選開獎號碼！");
                Console.WriteLine("無效的數字或符號，請檢查開獎號碼。");
                // 在這裡返回或者不執行後續程式碼，以確保不會再次引發例外狀況
                return new List<int>();
            }

            return 開獎號碼們;
        }


        public List<List<int>> 取得待兌獎號碼()
        {
            // 處理待兌獎號碼
            List<List<int>> 待兌獎號碼們 = new List<List<int>>();

            foreach (var item in lbox待兌獎.Items)
            {
                string 待兌獎號碼 = item.ToString();
                List<int> 單組待兌獎號碼 = new List<int>();

                // 分割字符串并解析为整数
                string[] 分割號碼 = 待兌獎號碼.Split(',');

                foreach (string 號碼 in 分割號碼)
                {
                    if (int.TryParse(號碼, out int 整數))
                    {
                        單組待兌獎號碼.Add(整數);
                    }
                    else
                    {
                        // 处理无法解析的情况，例如输入不是整数的情况
                        Console.WriteLine($"警告：無法解析的號碼 - {號碼}");
                    }
                }

                if (單組待兌獎號碼.Count > 0)
                {
                    int 特別號 = 單組待兌獎號碼.Last(); // 取得特別號
                    單組待兌獎號碼.RemoveAt(單組待兌獎號碼.Count - 1); // 移除特別號
                    單組待兌獎號碼.Sort();
                    單組待兌獎號碼.Add(特別號); // 將特別號添加到最後

                    待兌獎號碼們.Add(單組待兌獎號碼);

                    // 除錯輸出
                    Console.WriteLine("待兌獎號碼: " + String.Join(", ", 單組待兌獎號碼));
                }
            }
            return 待兌獎號碼們;
        }

        // 子方法2: 檢查是否中獎
        public List<Tuple<List<string>, List<long>>> 檢查是否中獎(List<List<int>> 多組待兌獎號碼們, List<int> 開獎號碼們)
        {
            List<Tuple<List<string>, List<long>>> 所有中獎結果 = new List<Tuple<List<string>, List<long>>>();

            foreach (List<int> 待兌獎號碼們 in 多組待兌獎號碼們)
            {

                List<string> str中獎獎名_list = new List<string>();
                List<int> 待兌獎號碼們_第一區 = 待兌獎號碼們.Take(6).ToList();
                int 待兌獎號碼_特別號;
                int 開獎號碼_特別號;

                // 驗證並轉換待兌獎號碼_特別號和開獎號碼_特別號
                if (!int.TryParse(待兌獎號碼們.Last().ToString(), out 待兌獎號碼_特別號) ||
                    !int.TryParse(開獎號碼們.Last().ToString(), out 開獎號碼_特別號))
                {
                    // 處理無法轉換的情況，例如顯示錯誤消息或跳過該筆數據
                    Console.WriteLine("無效的特別號碼，請檢查開獎號碼。");
                    continue;
                }

                // 比較 "待兌獎號碼們_第一區" 和 "開獎號碼們_第一區"。
                bool 第一區中獎 = (待兌獎號碼們.Take(6)).SequenceEqual(開獎號碼們.Take(6));

                // 比較 "待兌獎號碼_特別號" 和 "開獎號碼_特別號"。
                bool 特別號中獎 = 待兌獎號碼_特別號 == 開獎號碼_特別號;

                int 第一區中的相同數量 = 待兌獎號碼們_第一區.Intersect(開獎號碼們).Count();

                if (特別號中獎)
                {
                    if (第一區中的相同數量 == 6)
                    {
                        str中獎獎名_list.Add("頭獎");
                    }
                    else if (第一區中的相同數量 == 5)
                    {
                        str中獎獎名_list.Add("參獎");
                    }
                    else if (第一區中的相同數量 == 4)
                    {
                        str中獎獎名_list.Add("伍獎");
                    }
                    else if (第一區中的相同數量 == 3)
                    {
                        str中獎獎名_list.Add("柒獎");
                    }
                    else if (第一區中的相同數量 == 2)
                    {
                        str中獎獎名_list.Add("捌獎");
                    }
                    else if (第一區中的相同數量 == 1)
                    {
                        str中獎獎名_list.Add("普獎");
                    }
                }
                else
                {
                    if (第一區中的相同數量 == 6)
                    {
                        str中獎獎名_list.Add("貳獎");
                    }
                    else if (第一區中的相同數量 == 5)
                    {
                        str中獎獎名_list.Add("肆獎");
                    }
                    else if (第一區中的相同數量 == 4)
                    {
                        str中獎獎名_list.Add("陸獎");
                    }
                    else if (第一區中的相同數量 == 3)
                    {
                        str中獎獎名_list.Add("玖獎");
                    }
                    else
                    {
                        str中獎獎名_list.Add("未中獎");
                    }
                }


                List<long> 獎金金額 = 查詢獎金(str中獎獎名_list);
                所有中獎結果.Add(new Tuple<List<string>, List<long>>(str中獎獎名_list, 獎金金額));

                // 此處可以加入 Console.WriteLine 輸出變數用於除錯
                Console.WriteLine($"第一區中獎: {第一區中獎}");
                Console.WriteLine($"特別號中獎: {特別號中獎}");
                Console.WriteLine($"中獎獎項: {string.Join(", ", str中獎獎名_list)}");

            }
            return 所有中獎結果;
        }


        // 子方法3: 查詢獎金
        public List<long> 查詢獎金(List<string> str中獎獎名_list)
        {


            List<long> 獎金金額 = new List<long>();
            // 建立獎金字典，對應不同獎項的獎金。
            Dictionary<string, long> 獎金字典 = new Dictionary<string, long>
            {
                { "頭獎", 200000000 },
                { "貳獎", 20000000 },
                { "參獎", 150000 },
                { "肆獎", 20000 },
                { "伍獎", 4000 },
                { "陸獎", 800 },
                { "柒獎", 400 },
                { "捌獎",200},
                { "玖獎",100},
                { "普獎", 100 },
                { "未中獎",0}
            };

            foreach (var 獎名 in str中獎獎名_list)
            {



                // 若中了頭獎或貳獎，回傳-1表示獎金不固定需上網查詢。
                if (獎名 == "頭獎" || 獎名 == "貳獎")
                {

                    獎金金額.Add(-1);
                }
                else if (獎金字典.ContainsKey(獎名))
                {
                    // 否則根據獎項在字典中查詢獎金。
                    獎金金額.Add(獎金字典[獎名]);

                }
                else
                {
                    獎金金額.Add(0);
                }

            }
            return 獎金金額;

        }



        // 子方法4: 顯示中獎結果
        // 子方法4: 顯示中獎結果
        public void 顯示中獎結果(List<Tuple<List<string>, List<long>>> 中獎結果, List<List<int>> 多組待兌獎號碼們)
        {
            List<string> 所有中獎獎名 = new List<string>();
            List<long> 所有獎金 = new List<long>();
            lboxOutput.Items.Clear();
            lbl產生注數.Text = $"產生注數 : {lboxOutput.Items.Count} 注";
            lbl產生金額.Text = $"產生金額 : {lboxOutput.Items.Count * 100} 元";


            for (int resultIndex = 0; resultIndex < 中獎結果.Count; resultIndex++)
            {
                List<string> str中獎獎名 = 中獎結果[resultIndex].Item1;
                List<long> 獎金金額 = 中獎結果[resultIndex].Item2;
                List<int> 待兌獎號碼們 = 多組待兌獎號碼們[resultIndex]; // 取得對應的待兌獎號碼

                for (int i = 0; i < str中獎獎名.Count; i++)
                {
                    string 獎名 = str中獎獎名[i];
                    long 獎金 = 獎金金額[i];

                    if (獎金 == -1)
                    {
                        string 顯示結果 = $"你中了{獎名}!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!";
                        string 顯示結果2 =  $"對中的號碼組合是 {string.Join(", ", 待兌獎號碼們.Select(num => num.ToString("D2")))}";

                        lboxOutput.Items.Add(顯示結果);
                        lboxOutput.Items.Add(顯示結果2);
                        lboxOutput.Items.Add("實際獎金請洽官網。");
                        lboxOutput.Items.Add("");
                    }
                    else if (獎金 >= 5000 && 獎金 <= 150000)
                    {
                        long 實領金額 = Convert.ToInt64(獎金 * 0.796);
                        所有中獎獎名.Add($"{獎名} (金額:{獎金.ToString("D2")}, 實領金額:{實領金額.ToString("D2")})");
                        所有獎金.Add(實領金額);

                        string 顯示結果 = $"你中了{獎名}!!!!!!!!!!!!!!!!!!!!!!!!!";
                        string 顯示結果2 = $"對中的號碼組合是 {string.Join(", ", 待兌獎號碼們.Select(num => num.ToString("D2")))}";
                        string 顯示結果3 = $"中獎的金額是:{獎金.ToString("D2")}, 實領的金額是:{實領金額.ToString("D2")}，請參考下方扣稅說明";

                        lboxOutput.Items.Add(顯示結果);
                        lboxOutput.Items.Add(顯示結果2);
                        lboxOutput.Items.Add(顯示結果3);
                        lboxOutput.Items.Add("");
                    }
                    else if (獎金 > 0 && 獎金 < 5000)
                    {
                        所有中獎獎名.Add($"{獎名} (金額:{獎金.ToString("D2")})");
                        所有獎金.Add(獎金);

                        string 顯示結果 = $"你中了{獎名}";
                        string 顯示結果2 = $"對中的號碼組合是 {string.Join(", ", 待兌獎號碼們.Select(num => num.ToString("D2")))}";
                        string 顯示結果3 = $"獎金是:{獎金.ToString("D2")}";

                        lboxOutput.Items.Add(顯示結果);
                        lboxOutput.Items.Add(顯示結果2);
                        lboxOutput.Items.Add(顯示結果3);
                        lboxOutput.Items.Add("");
                    }
                }
            }
        }




        //==============================兌獎清空==========================================/
        private void btn兌獎清空_Click(object sender, EventArgs e)
        {
            // 清空 lbox待兌獎
            lbox待兌獎.Items.Clear();
           
        }

        //===========================清單加入兌獎器=============================================/
        private void btn清單加入兌獎器_Click(object sender, EventArgs e)
        {
            // 遍歷 lboxOutput 中的每個項目
            foreach (var item in lboxOutput.Items)
            {
                string text = item.ToString(); // 獲取項目的文字

                // 檢查每個項目的文字是否包含非數字和非逗號的字符
                if (text.Any(c => !char.IsDigit(c) && c != ','))
                {
                    DialogResult result = MessageBox.Show("加入兌獎器時畫面只能有數字！\n\n即將清空表單。", "號碼組合清單異常");

                    
                   return; // 如果有無效字符，不執行加入操作
                    
                   

                }

                // 如果項目的文字符合要求，就將該項目添加到 lbox待兌獎 中
                lbox待兌獎.Items.Add(item);
            }

            // 調用兌獎輸出的邏輯
            btn兌獎輸出_Click(sender, e);
        }

        //========================================================================/
        private void pnlCheckNumber_Paint(object sender, PaintEventArgs e)
        {

        }
        //========================功能按鈕介紹================================================/

        private void btn功能按鈕介紹_Click(object sender, EventArgs e)
        {
            
                try
                {
                    // 設定檔案路徑，請替換成您的檔案名稱
                    string filePath = Path.Combine(Application.StartupPath, "Resources", "showfunction.txt");

                    // 讀取文本文件中的所有行
                    string[] lines = File.ReadAllLines(filePath);

                    // 清空 ListBox 的現有項目
                    lboxOutput.Items.Clear();
                lbl產生注數.Text = $"產生注數 : {lboxOutput.Items.Count} 注";
                lbl產生金額.Text = $"產生金額 : {lboxOutput.Items.Count * 100} 元";

                // 將每一行添加到 ListBox 中
                foreach (string line in lines)
                    {
                        lboxOutput.Items.Add(line);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("讀取檔案時發生錯誤：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }

        private void pnlFuction_Paint(object sender, PaintEventArgs e)
        {

        }

        //===============================個資選號=========================================/
        private void btn個資選號_Click(object sender, EventArgs e)
        {
            lboxOutput.Items.Clear();
            lbl產生注數.Text = $"產生注數 : {lboxOutput.Items.Count} 注";
            lbl產生金額.Text = $"產生金額 : {lboxOutput.Items.Count * 100} 元";

            // 创建一个新的对话框窗口
            using (var dialog = new Form())
            {
                dialog.Text = "選擇：生日（西元年、月、日）、星座、生肖";
                dialog.Size = new System.Drawing.Size(430, 130);
                dialog.BackColor = Color.Gainsboro;
                

                // 设置对话框的初始位置为屏幕的居中
                dialog.StartPosition = FormStartPosition.CenterScreen;

                // 在对话框内创建并添加各个控件
                var comboBoxYear = new ComboBox();
                var comboBoxMonth = new ComboBox();
                var comboBoxDay = new ComboBox();
                var comboBox星座 = new ComboBox();
                var comboBox生肖 = new ComboBox();

                // 创建和添加 "確定" 按钮
                Button buttonConfirm = new Button();

                buttonConfirm.Text = "確定";
                buttonConfirm.Location = new System.Drawing.Point(320, 50);
                buttonConfirm.UseVisualStyleBackColor = true; // 设置按钮使用默认样式
                buttonConfirm.Click += (s, ev) =>
                {
                    string selectedYear = comboBoxYear.SelectedItem.ToString();
                    string selectedMonth = comboBoxMonth.SelectedItem.ToString();
                    string selectedDay = comboBoxDay.SelectedItem.ToString();
                    string selected星座 = comboBox星座.SelectedItem.ToString();
                    string selected生肖 = comboBox生肖.SelectedItem.ToString();

                    bool isValidMonth = IsValidMonth(selectedMonth);
                    bool isValidDay = IsValidDay(selectedDay);

                    if (isValidMonth && isValidDay)
                    {
                        // 在此处处理用户选择的数据
                        CalculateAndGenerateNumbers(selectedYear, selectedMonth, selectedDay, selected星座, selected生肖);
                        // 可以将结果添加到 lboxOutput 中
                        string result = $"西元{selectedYear}年，{selectedMonth}月，{selectedDay}日，星座是{selected星座}，生肖是{selected生肖}";
                        lboxOutput.Items.Add(result);

                        dialog.Close(); // 关闭对话框
                    }
                    else
                    {
                        MessageBox.Show("請輸入有效的月份和日期！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                };

                // 创建和添加西元年的下拉式选单

                comboBoxYear.Items.AddRange(Enumerable.Range(1900, 151).Select(year => year.ToString()).ToArray()); // 1900年到2050年
                comboBoxYear.SelectedIndex = 0;
                comboBoxYear.Location = new System.Drawing.Point(65, 20);
                comboBoxYear.DropDownStyle = ComboBoxStyle.DropDownList; // 設定為只能從下拉選單選取
                comboBoxYear.Size = new System.Drawing.Size(50, 20); // 设置标签的宽度 labelYear.Size = new System.Drawing.Size(60, 20); // 设置标签的宽度
                dialog.Controls.Add(comboBoxYear);

                // 创建和添加西元年的下拉式选单标签
                Label labelYear = new Label();
                labelYear.Text = "西元年：";
                labelYear.Location = new System.Drawing.Point(5, 24);
                labelYear.Size = new System.Drawing.Size(60, 20); // 设置标签的宽度
                dialog.Controls.Add(labelYear);

                // 创建和添加月份的下拉式选单
                comboBoxMonth.Items.AddRange(new string[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" });
                comboBoxMonth.SelectedIndex = 0;
                comboBoxMonth.Location = new System.Drawing.Point(165, 20);
                comboBoxMonth.DropDownStyle = ComboBoxStyle.DropDownList; // 設定為只能從下拉選單選取
                comboBoxMonth.Size = new System.Drawing.Size(40, 20);
                dialog.Controls.Add(comboBoxMonth);

                // 创建和添加月份的下拉式选单标签
                Label labelMonth = new Label();
                labelMonth.Text = "月：";
                labelMonth.Location = new System.Drawing.Point(130, 24);
                labelMonth.Size = new System.Drawing.Size(30, 20); // 设置标签的宽度
                dialog.Controls.Add(labelMonth);

                // 创建和添加日期的下拉式选单
                comboBoxDay.Items.AddRange(new string[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" });
                comboBoxDay.SelectedIndex = 0;
                comboBoxDay.Location = new System.Drawing.Point(260, 20);
                comboBoxDay.DropDownStyle = ComboBoxStyle.DropDownList; // 設定為只能從下拉選單選取
                comboBoxDay.Size = new System.Drawing.Size(40, 20);
                dialog.Controls.Add(comboBoxDay);

                // 创建和添加日期的下拉式选单标签
                Label labelDay = new Label();
                labelDay.Text = "日：";
                labelDay.Location = new System.Drawing.Point(220, 24);
                labelDay.Size = new System.Drawing.Size(40, 20); // 设置标签的宽度
                dialog.Controls.Add(labelDay);

                // 创建和添加星座的下拉式选单
                comboBox星座.Items.AddRange(new string[] { "牡羊座", "金牛座", "雙子座", "巨蟹座", "獅子座", "處女座", "天秤座", "天蠍座", "射手座", "魔羯座", "水瓶座", "雙魚座" });
                comboBox星座.SelectedIndex = 0;
                comboBox星座.Location = new System.Drawing.Point(65, 50);
                comboBox星座.DropDownStyle = ComboBoxStyle.DropDownList; // 設定為只能從下拉選單選取
                comboBox星座.Size = new System.Drawing.Size(60, 20);
                dialog.Controls.Add(comboBox星座);

                // 创建和添加星座的下拉式选单标签
                Label label星座 = new Label();
                label星座.Text = "星座：";
                label星座.Location = new System.Drawing.Point(20, 54);
                label星座.Size = new System.Drawing.Size(45, 20); // 设置标签的宽度
                dialog.Controls.Add(label星座);

                // 创建和添加生肖的下拉式选单
                comboBox生肖.Items.AddRange(new string[] { "鼠", "牛", "虎", "兔", "龍", "蛇", "馬", "羊", "猴", "雞", "狗", "豬" });
                comboBox生肖.SelectedIndex = 0;
                comboBox生肖.Location = new System.Drawing.Point(185, 50);
                comboBox生肖.DropDownStyle = ComboBoxStyle.DropDownList; // 設定為只能從下拉選單選取
                comboBox生肖.Size = new System.Drawing.Size(70, 20);
                dialog.Controls.Add(comboBox生肖);

                // 创建和添加生肖的下拉式选单标签
                Label label生肖 = new Label();
                label生肖.Text = "生肖：";
                label生肖.Location = new System.Drawing.Point(140, 54);
                label生肖.Size = new System.Drawing.Size(45, 20); // 设置标签的宽度
                dialog.Controls.Add(label生肖);

                dialog.Controls.Add(buttonConfirm);

                // 添加控件到对话框
                dialog.Controls.Add(comboBoxYear);
                dialog.Controls.Add(comboBoxMonth);
                dialog.Controls.Add(comboBoxDay);
                dialog.Controls.Add(comboBox星座);
                dialog.Controls.Add(comboBox生肖);
                dialog.Controls.Add(buttonConfirm);

                // 显示对话框窗口
                dialog.ShowDialog();
            }
        }

        bool IsValidMonth(string month)
        {
            // 在此處檢查月份的有效性，例如檢查是否在 "01" 到 "12" 的範圍內
            // 如果有效，返回 true，否則返回 false

            int monthNumber;
            if (int.TryParse(month, out monthNumber))
            {
                return (monthNumber >= 1 && monthNumber <= 12);
            }
            return false;
        }

        bool IsValidDay(string day)
        {
            // 在此處檢查日期的有效性，例如檢查是否在 "01" 到 "31" 的範圍內
            // 如果有效，返回 true，否則返回 false
            int dayNumber;
            if (int.TryParse(day, out dayNumber))
            {
                return (dayNumber >= 1 && dayNumber <= 31);
            }
            return false;
        }
        private void CalculateAndGenerateNumbers(string selectedYear, string selectedMonth, string selectedDay, string selected星座, string selected生肖)
        {
            // 获取用户选择的生肖对应的数字
            string animalNumber = GetAnimalNumber(selected生肖);

            // 获取用户选择的星座对应的数字
            string constellationNumber = GetConstellationNumber(selected星座);

            // 获取用户选择的西元年、月、日对应的数字

            // 将所有数字连接成一个数字串
            string numberString = selectedYear.PadLeft(4,'0') +
                                  selectedMonth.PadLeft(2, '0') +
                                  selectedDay.PadLeft(2, '0') +
                                  animalNumber.PadLeft(2,'0') +
                                  constellationNumber;

            // 随机切割数字串，选出6个介于01和38之间的2位数，并排序
            List<int> numbers = RandomSelectAndSortNumbers(numberString, 6);

            // 计算数字列表的和，略过0
            int sum = numbers.Where(n => n != 0).Sum();

            // 计算和除以8的余数，然后+1并转换成2位数字字符串
            int remainder = (sum % 8) + 1;
            string remainderString = remainder.ToString("D2");

            // 将余数字符串添加到数字列表的末尾
            numbers.Add(remainder);

            // 显示结果在名为lboxOutput的消息框中
            List<string> formattedNumbers = numbers.Select(n => n.ToString("D2")).ToList();
            string result = string.Join(", ", formattedNumbers);
            lboxOutput.Items.Add($"你的專屬幸運號碼：{result}");
        }

        // 获取生肖对应的数字
        private string GetAnimalNumber(string animal)
        {
            switch (animal)
            {
                case "鼠": return "1";
                case "牛": return "2";
                case "虎": return "3";
                case "兔": return "4";
                case "龍": return "5";
                case "蛇": return "6";
                case "馬": return "7";
                case "羊": return "8";
                case "猴": return "9";
                case "雞": return "10";
                case "狗": return "11";
                case "豬": return "12";
                default: return "";
            }
        }
        // 获取星座对应的数字
        private string GetConstellationNumber(string constellation)
        {
            // 在这里添加星座到数字的映射
            switch (constellation)
            {
                case "牡羊座": return "03210419";
                case "金牛座": return "04200520";
                case "雙子座": return "05210621";
                case "巨蟹座": return "06220722";
                case "獅子座": return "07230822";
                case "處女座": return "08230922";
                case "天秤座": return "09231023";
                case "天蠍座": return "10241122";
                case "射手座": return "11231221";
                case "魔羯座": return "12220119";
                case "水瓶座": return "01200218";
                case "雙魚座": return "02190320";
                default: return "";
            }
        }

        // 随机选择和排序数字
        private List<int> RandomSelectAndSortNumbers(string numberString, int count)
        {
            HashSet<int> uniqueNumbers = new HashSet<int>();
            Random random = new Random();

            // 将数字串拆分成单个数字
            List<int> singleDigits = numberString.Select(c => c - '0').ToList();

            int attempts = 0;
            int maxAttempts = 1000; // 设置一个尝试的最大次数

            while (uniqueNumbers.Count < count && attempts < maxAttempts)
            {
                // 随机选择两个数字并组合成两位数
                int num1 = singleDigits[random.Next(singleDigits.Count)];
                int num2 = singleDigits[random.Next(singleDigits.Count)];
                int combinedNumber = num1 * 10 + num2;

                // 检查数字是否在01到38之间
                if (combinedNumber >= 1 && combinedNumber <= 38)
                {
                    uniqueNumbers.Add(combinedNumber);
                }

                attempts++;
            }

            if (uniqueNumbers.Count < count)
            {
                // 无法生成足够的唯一数字，可以在这里处理错误
                // 例如，返回一个空列表或抛出异常
                return new List<int>(); // 或 throw new Exception("无法生成足够的唯一数字。");
            }

            // 将结果排序
            List<int> sortedNumbers = uniqueNumbers.ToList();
            sortedNumbers.Sort();

            return sortedNumbers;
        }
        //======================================兌獎匯出======================================================
        private void btn兌獎匯出_Click(object sender, EventArgs e)
        {
            // 將 lboxOutput 中的資料匯出到檔案
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "文字檔 (*.txt)|*.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 複製 lboxOutput 中的項目到一個新的 List<string>
                List<string> itemsToExport = lbox待兌獎.Items.Cast<string>().ToList();

                // 提示使用者匯出成功，詢問是否清空畫面
                DialogResult result = MessageBox.Show("匯出成功，是否清空目前數字組合？", "匯出確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // 清空 lbox待兌獎 中的所有項目
                    lbox待兌獎.Items.Clear();
                    MessageBox.Show("已清空畫面！");
                }

                // 將複製的項目寫入檔案
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    foreach (string item in itemsToExport)
                    {
                        writer.WriteLine(item);
                    }
                }

                if (result == DialogResult.No)
                {
                    MessageBox.Show("已保留數字組合！");
                }
            }
        }

    }
} 



