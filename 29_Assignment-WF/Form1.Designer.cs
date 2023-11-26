namespace _29_Assignment_WF
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
            IntergralOnlyCheckBox = new CheckBox();
            SuspendLayout();
            // 
            // IntergralOnlyCheckBox
            // 
            IntergralOnlyCheckBox.AutoSize = true;
            IntergralOnlyCheckBox.ImageAlign = ContentAlignment.MiddleLeft;
            IntergralOnlyCheckBox.Location = new Point(73, 302);
            IntergralOnlyCheckBox.Name = "IntergralOnlyCheckBox";
            IntergralOnlyCheckBox.Size = new Size(103, 19);
            IntergralOnlyCheckBox.TabIndex = 0;
            IntergralOnlyCheckBox.Text = "Intergral Only?";
            IntergralOnlyCheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(IntergralOnlyCheckBox);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox IntergralOnlyCheckBox;
    }
}