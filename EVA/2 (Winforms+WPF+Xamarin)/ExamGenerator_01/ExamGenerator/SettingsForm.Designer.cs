﻿namespace ELTE.Forms.ExamGenerator
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._buttonOk = new System.Windows.Forms.Button();
            this._numericQuestionCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this._numericPeriodLength = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this._checkedListBox = new System.Windows.Forms.CheckedListBox();
            this._buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._numericQuestionCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._numericPeriodLength)).BeginInit();
            this.SuspendLayout();
            // 
            // _buttonOk
            // 
            this._buttonOk.Location = new System.Drawing.Point(94, 155);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(75, 23);
            this._buttonOk.TabIndex = 1;
            this._buttonOk.Text = "OK";
            this._buttonOk.UseVisualStyleBackColor = true;
            this._buttonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // _numericQuestionCount
            // 
            this._numericQuestionCount.Location = new System.Drawing.Point(94, 3);
            this._numericQuestionCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numericQuestionCount.Name = "_numericQuestionCount";
            this._numericQuestionCount.Size = new System.Drawing.Size(75, 20);
            this._numericQuestionCount.TabIndex = 2;
            this._numericQuestionCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tételek száma:";
            // 
            // _numericPeriodLength
            // 
            this._numericPeriodLength.Location = new System.Drawing.Point(94, 29);
            this._numericPeriodLength.Name = "_numericPeriodLength";
            this._numericPeriodLength.Size = new System.Drawing.Size(75, 20);
            this._numericPeriodLength.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Periódus:";
            // 
            // _checkedListBox
            // 
            this._checkedListBox.FormattingEnabled = true;
            this._checkedListBox.Location = new System.Drawing.Point(12, 55);
            this._checkedListBox.Name = "_checkedListBox";
            this._checkedListBox.Size = new System.Drawing.Size(157, 94);
            this._checkedListBox.TabIndex = 6;
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Location = new System.Drawing.Point(12, 155);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 23);
            this._buttonCancel.TabIndex = 7;
            this._buttonCancel.Text = "Mégse";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(176, 182);
            this.ControlBox = false;
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._checkedListBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._numericPeriodLength);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._numericQuestionCount);
            this.Controls.Add(this._buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Beállítások";
            ((System.ComponentModel.ISupportInitialize)(this._numericQuestionCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._numericPeriodLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _buttonOk;
        private System.Windows.Forms.NumericUpDown _numericQuestionCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown _numericPeriodLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox _checkedListBox;
        private System.Windows.Forms.Button _buttonCancel;
    }
}