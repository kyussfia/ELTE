namespace LightDuel_WinForms.View
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.újJátékToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kicsiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.közepesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nagyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clockLabel = new System.Windows.Forms.Label();
            this.Pause = new System.Windows.Forms.Button();
            this.programBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.programBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.újJátékToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(2, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(942, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip";
            // 
            // újJátékToolStripMenuItem
            // 
            this.újJátékToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kicsiToolStripMenuItem,
            this.közepesToolStripMenuItem,
            this.nagyToolStripMenuItem});
            this.újJátékToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.újJátékToolStripMenuItem.Name = "újJátékToolStripMenuItem";
            this.újJátékToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.újJátékToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.újJátékToolStripMenuItem.Text = "Új játék";
            this.újJátékToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // kicsiToolStripMenuItem
            // 
            this.kicsiToolStripMenuItem.Name = "kicsiToolStripMenuItem";
            this.kicsiToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.kicsiToolStripMenuItem.Text = "Kicsi (12)";
            this.kicsiToolStripMenuItem.Click += new System.EventHandler(this.kicsiToolStripMenuItem_Click);
            // 
            // közepesToolStripMenuItem
            // 
            this.közepesToolStripMenuItem.Name = "közepesToolStripMenuItem";
            this.közepesToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.közepesToolStripMenuItem.Text = "Közepes (24)";
            this.közepesToolStripMenuItem.Click += new System.EventHandler(this.közepesToolStripMenuItem_Click);
            // 
            // nagyToolStripMenuItem
            // 
            this.nagyToolStripMenuItem.Name = "nagyToolStripMenuItem";
            this.nagyToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.nagyToolStripMenuItem.Text = "Nagy (36)";
            this.nagyToolStripMenuItem.Click += new System.EventHandler(this.nagyToolStripMenuItem_Click);
            // 
            // clockLabel
            // 
            this.clockLabel.AutoSize = true;
            this.clockLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.clockLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clockLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.clockLabel.Location = new System.Drawing.Point(2, 24);
            this.clockLabel.MaximumSize = new System.Drawing.Size(500, 50);
            this.clockLabel.MinimumSize = new System.Drawing.Size(940, 0);
            this.clockLabel.Name = "clockLabel";
            this.clockLabel.Size = new System.Drawing.Size(940, 41);
            this.clockLabel.TabIndex = 1;
            this.clockLabel.Text = "00:00:00";
            this.clockLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Pause
            // 
            this.Pause.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.Pause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Pause.Location = new System.Drawing.Point(398, 68);
            this.Pause.Name = "Pause";
            this.Pause.Size = new System.Drawing.Size(145, 22);
            this.Pause.TabIndex = 3;
            this.Pause.TabStop = false;
            this.Pause.Text = "Pause";
            this.Pause.UseVisualStyleBackColor = false;
            // 
            // programBindingSource
            // 
            this.programBindingSource.DataSource = typeof(LightDuel_WinForms.View.Program);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 948);
            this.Controls.Add(this.Pause);
            this.Controls.Add(this.clockLabel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(960, 987);
            this.MinimumSize = new System.Drawing.Size(960, 987);
            this.Name = "MainWindow";
            this.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.Text = "Light-Duel";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.programBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem újJátékToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kicsiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem közepesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nagyToolStripMenuItem;
        private System.Windows.Forms.Label clockLabel;
        private System.Windows.Forms.BindingSource programBindingSource;
        private System.Windows.Forms.Button Pause;
    }
}

