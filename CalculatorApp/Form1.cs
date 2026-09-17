using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class Form1 : Form
    {
        private TextBox? displayBox;
        private int currentResult = 0;
        private string currentOperation = "";
        private bool isNewInput = true;

        public Form1()
        {
            InitializeComponent();
            displayBox = new TextBox();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Integer Calculator";
            this.Size = new Size(300, 400);

            displayBox!.Location = new Point(20, 20);
            displayBox!.Size = new Size(240, 40);
            displayBox!.Font = new Font("Arial", 20);
            displayBox!.ReadOnly = true;
            displayBox!.TextAlign = HorizontalAlignment.Right;
            displayBox!.Text = "0";
            this.Controls.Add(displayBox);

            string[] buttons = { "7", "8", "9", "+", "4", "5", "6", "-", "1", "2", "3", "*", "C", "0", "=", "" };

            int x = 20;
            int y = 70;
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i] == "") continue;

                Button btn = new Button();
                btn.Text = buttons[i];
                btn.Size = new Size(50, 50);
                btn.Location = new Point(x, y);
                btn.Font = new Font("Arial", 16);

                if (buttons[i] == "C")
                    btn.Click += ClearButton_Click;
                else if (buttons[i] == "=")
                    btn.Click += EqualsButton_Click;
                else if (buttons[i] == "+" || buttons[i] == "-" || buttons[i] == "*")
                    btn.Click += OperatorButton_Click;
                else
                    btn.Click += DigitButton_Click;

                this.Controls.Add(btn);

                x += 60;
                if ((i + 1) % 4 == 0)
                {
                    x = 20;
                    y += 60;
                }
            }
        }

        private void DigitButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                if (isNewInput)
                {
                    displayBox!.Text = btn.Text;
                    isNewInput = false;
                }
                else
                {
                    displayBox!.Text += btn.Text;
                }
            }
        }

        private void OperatorButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                if (!isNewInput)
                {
                    CalculateResult();
                }

                if (int.TryParse(displayBox!.Text, out int result))
                {
                    currentResult = result;
                }

                currentOperation = btn.Text;
                isNewInput = true;
            }
        }

        private void EqualsButton_Click(object? sender, EventArgs e)
        {
            CalculateResult();
            currentOperation = "";
            isNewInput = true;
        }

        private void ClearButton_Click(object? sender, EventArgs e)
        {
            displayBox!.Text = "0";
            currentResult = 0;
            currentOperation = "";
            isNewInput = true;
        }

        private void CalculateResult()
        {
            if (int.TryParse(displayBox!.Text, out int newValue))
            {
                switch (currentOperation)
                {
                    case "+":
                        currentResult += newValue;
                        break;
                    case "-":
                        currentResult -= newValue;
                        break;
                    case "*":
                        currentResult *= newValue;
                        break;
                    case "":
                        currentResult = newValue;
                        break;
                }
                displayBox.Text = currentResult.ToString();
            }
        }
    }
}
