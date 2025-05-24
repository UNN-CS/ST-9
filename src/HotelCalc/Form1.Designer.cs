using System;
using System.Windows.Forms;

namespace HotelCalc
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private System.Windows.Forms.TextBox daysTextBox;
        private System.Windows.Forms.TextBox categoryTextBox;
        private System.Windows.Forms.TextBox capacityTextBox;
        private System.Windows.Forms.TextBox safeTextBox;
        private System.Windows.Forms.TextBox breakfastTextBox;
        private System.Windows.Forms.TextBox resultTextBox;
        private System.Windows.Forms.Button calculateButton;

        private void InitializeComponent()
        {
            this.daysTextBox = new System.Windows.Forms.TextBox();
            this.categoryTextBox = new System.Windows.Forms.TextBox();
            this.capacityTextBox = new System.Windows.Forms.TextBox();
            this.safeTextBox = new System.Windows.Forms.TextBox();
            this.breakfastTextBox = new System.Windows.Forms.TextBox();
            this.resultTextBox = new System.Windows.Forms.TextBox();
            this.calculateButton = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // Кол-во дней
            Label label1 = new Label();
            label1.Text = "Количество дней проживания";
            label1.Location = new System.Drawing.Point(10, 10);
            label1.AutoSize = true;
            this.Controls.Add(label1);
            daysTextBox.Name = "daysTextBox";
            daysTextBox.Location = new System.Drawing.Point(250, 10);
            this.Controls.Add(daysTextBox);

            // Категория номера
            Label label2 = new Label();
            label2.Text = "Категория номера (1,2,3)";
            label2.Location = new System.Drawing.Point(10, 40);
            label2.AutoSize = true;
            this.Controls.Add(label2);
            categoryTextBox.Name = "categoryTextBox";
            categoryTextBox.Location = new System.Drawing.Point(250, 40);
            this.Controls.Add(categoryTextBox);

            // Вместимость номера
            Label label3 = new Label();
            label3.Text = "Вместимость номера (1,2,3)";
            label3.Location = new System.Drawing.Point(10, 70);
            label3.AutoSize = true;
            this.Controls.Add(label3);
            capacityTextBox.Name = "capacityTextBox";
            capacityTextBox.Location = new System.Drawing.Point(250, 70);
            this.Controls.Add(capacityTextBox);

            // Сейф
            Label label4 = new Label();
            label4.Text = "Сейф (да/нет)";
            label4.Location = new System.Drawing.Point(10, 100);
            label4.AutoSize = true;
            this.Controls.Add(label4);
            safeTextBox.Name = "safeTextBox";
            safeTextBox.Location = new System.Drawing.Point(250, 100);
            this.Controls.Add(safeTextBox);

            // Завтрак
            Label label5 = new Label();
            label5.Text = "Завтрак (да/нет)";
            label5.Location = new System.Drawing.Point(10, 130);
            label5.AutoSize = true;
            this.Controls.Add(label5);
            breakfastTextBox.Name = "breakfastTextBox";
            breakfastTextBox.Location = new System.Drawing.Point(250, 130);
            this.Controls.Add(breakfastTextBox);

            // Сумма
            Label label6 = new Label();
            label6.Text = "Сумма";
            label6.Location = new System.Drawing.Point(10, 160);
            label6.AutoSize = true;
            this.Controls.Add(label6);
            resultTextBox.Name = "resultTextBox";
            resultTextBox.Location = new System.Drawing.Point(250, 160);
            resultTextBox.ReadOnly = true;
            this.Controls.Add(resultTextBox);

            // Кнопка "Рассчитать"
            calculateButton.Name = "calculateButton";
            calculateButton.Text = "Рассчитать";
            calculateButton.Location = new System.Drawing.Point(250, 190);
            calculateButton.Click += new System.EventHandler(this.calculateButton_Click);
            this.Controls.Add(calculateButton);

            // Настройки формы
            this.ClientSize = new System.Drawing.Size(450, 240);
            this.Text = "Гостиничный калькулятор";
            this.ResumeLayout(false);
            this.PerformLayout();
        }




        #endregion
    }
}

