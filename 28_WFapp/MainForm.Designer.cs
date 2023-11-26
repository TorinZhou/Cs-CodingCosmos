namespace _28_WFapp
{
    partial class MainForm
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
            CounterLabel = new Label();
            IncreaseCounterButton = new Button();
            HideButtonCheckBox = new CheckBox();
            YearOfBirthTextBox = new TextBox();
            SuspendLayout();
            // 
            // CounterLabel
            // 
            CounterLabel.AutoSize = true;
            CounterLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            CounterLabel.Location = new Point(77, 185);
            CounterLabel.Name = "CounterLabel";
            CounterLabel.Size = new Size(33, 37);
            CounterLabel.TabIndex = 0;
            CounterLabel.Text = "0";
            // 
            // IncreaseCounterButton
            // 
            IncreaseCounterButton.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            IncreaseCounterButton.Location = new Point(207, 174);
            IncreaseCounterButton.Name = "IncreaseCounterButton";
            IncreaseCounterButton.Size = new Size(222, 58);
            IncreaseCounterButton.TabIndex = 1;
            IncreaseCounterButton.Text = "Click";
            IncreaseCounterButton.UseVisualStyleBackColor = true;
            IncreaseCounterButton.Click += IncreaseCounterButton_Click;
            // 
            // HideButtonCheckBox
            // 
            HideButtonCheckBox.AutoSize = true;
            HideButtonCheckBox.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            HideButtonCheckBox.Location = new Point(235, 238);
            HideButtonCheckBox.Name = "HideButtonCheckBox";
            HideButtonCheckBox.Size = new Size(163, 29);
            HideButtonCheckBox.TabIndex = 2;
            HideButtonCheckBox.Text = "Hide the Button";
            HideButtonCheckBox.UseVisualStyleBackColor = true;
            HideButtonCheckBox.CheckedChanged += HideButtonCheckBox_CheckedChanged;
            // 
            // YearOfBirthTextBox
            // 
            YearOfBirthTextBox.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            YearOfBirthTextBox.Location = new Point(207, 337);
            YearOfBirthTextBox.Name = "YearOfBirthTextBox";
            YearOfBirthTextBox.Size = new Size(222, 43);
            YearOfBirthTextBox.TabIndex = 3;
            YearOfBirthTextBox.KeyPress += YearOfBirthTextBox_KeyPress;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(YearOfBirthTextBox);
            Controls.Add(HideButtonCheckBox);
            Controls.Add(IncreaseCounterButton);
            Controls.Add(CounterLabel);
            Name = "MainForm";
            Text = "Our First App";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label CounterLabel;
        private Button IncreaseCounterButton;
        private CheckBox HideButtonCheckBox;
        private TextBox YearOfBirthTextBox;
    }
}