namespace iisuEmulator
{
    partial class EmulatorView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmulatorView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bToolBox = new System.Windows.Forms.Button();
            this.cbPlay = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbStartStop = new System.Windows.Forms.ComboBox();
            this.dgMappings = new System.Windows.Forms.DataGridView();
            this.mappingEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IIDOutput = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Emulator = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DataSource = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.bBrowseIID = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbIIDProject = new System.Windows.Forms.TextBox();
            this.bOpenIIDProject = new System.Windows.Forms.Button();
            this.bNew = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.bOpen = new System.Windows.Forms.Button();
            this.skLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgMappings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.skLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // bToolBox
            // 
            this.bToolBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bToolBox.Location = new System.Drawing.Point(50, 63);
            this.bToolBox.Name = "bToolBox";
            this.bToolBox.Size = new System.Drawing.Size(79, 28);
            this.bToolBox.TabIndex = 2;
            this.bToolBox.Text = "ToolBox";
            this.bToolBox.UseVisualStyleBackColor = true;
            this.bToolBox.Visible = false;
            this.bToolBox.Click += new System.EventHandler(this.bToolBox_Click);
            // 
            // cbPlay
            // 
            this.cbPlay.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(62)))), ((int)(((byte)(60)))));
            this.cbPlay.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cbPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(96)))), ((int)(((byte)(94)))));
            this.cbPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbPlay.Image = ((System.Drawing.Image)(resources.GetObject("cbPlay.Image")));
            this.cbPlay.Location = new System.Drawing.Point(16, 63);
            this.cbPlay.Name = "cbPlay";
            this.cbPlay.Size = new System.Drawing.Size(28, 28);
            this.cbPlay.TabIndex = 1;
            this.cbPlay.UseVisualStyleBackColor = false;
            this.cbPlay.CheckedChanged += new System.EventHandler(this.cbPlay_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(196)))), ((int)(((byte)(195)))));
            this.label2.Location = new System.Drawing.Point(13, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Start/Stop emulation:";
            // 
            // cbStartStop
            // 
            this.cbStartStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(72)))), ((int)(((byte)(70)))));
            this.cbStartStop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStartStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbStartStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStartStop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.cbStartStop.FormattingEnabled = true;
            this.cbStartStop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbStartStop.ItemHeight = 15;
            this.cbStartStop.Location = new System.Drawing.Point(144, 171);
            this.cbStartStop.Name = "cbStartStop";
            this.cbStartStop.Size = new System.Drawing.Size(405, 23);
            this.cbStartStop.TabIndex = 4;
            this.cbStartStop.SelectedIndexChanged += new System.EventHandler(this.cbStartStop_SelectedIndexChanged);
            // 
            // dgMappings
            // 
            this.dgMappings.AllowUserToAddRows = false;
            this.dgMappings.AllowUserToDeleteRows = false;
            this.dgMappings.AllowUserToOrderColumns = true;
            this.dgMappings.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgMappings.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgMappings.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(72)))), ((int)(((byte)(70)))));
            this.dgMappings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgMappings.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgMappings.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(96)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(133)))), ((int)(((byte)(131)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMappings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgMappings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMappings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mappingEnabled,
            this.IIDOutput,
            this.DataType,
            this.Emulator,
            this.DataSource});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMappings.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgMappings.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgMappings.EnableHeadersVisualStyles = false;
            this.dgMappings.GridColor = System.Drawing.Color.Black;
            this.dgMappings.Location = new System.Drawing.Point(16, 237);
            this.dgMappings.MultiSelect = false;
            this.dgMappings.Name = "dgMappings";
            this.dgMappings.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgMappings.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgMappings.ShowEditingIcon = false;
            this.dgMappings.Size = new System.Drawing.Size(951, 150);
            this.dgMappings.TabIndex = 3;
            this.dgMappings.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMappings_CellValueChanged);
            this.dgMappings.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgMappings_CurrentCellDirtyStateChanged);
            // 
            // mappingEnabled
            // 
            this.mappingEnabled.HeaderText = "";
            this.mappingEnabled.Name = "mappingEnabled";
            this.mappingEnabled.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.mappingEnabled.Width = 35;
            // 
            // IIDOutput
            // 
            this.IIDOutput.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IIDOutput.HeaderText = "IID Output";
            this.IIDOutput.Name = "IIDOutput";
            this.IIDOutput.ReadOnly = true;
            // 
            // DataType
            // 
            this.DataType.HeaderText = "Data type";
            this.DataType.Name = "DataType";
            this.DataType.ReadOnly = true;
            // 
            // Emulator
            // 
            this.Emulator.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(96)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.Emulator.DefaultCellStyle = dataGridViewCellStyle3;
            this.Emulator.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.Emulator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Emulator.HeaderText = "Emulator";
            this.Emulator.Name = "Emulator";
            this.Emulator.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Emulator.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // DataSource
            // 
            this.DataSource.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DataSource.HeaderText = "Emulated source";
            this.DataSource.Name = "DataSource";
            // 
            // bBrowseIID
            // 
            this.bBrowseIID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(62)))), ((int)(((byte)(60)))));
            this.bBrowseIID.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.bBrowseIID.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(96)))), ((int)(((byte)(94)))));
            this.bBrowseIID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bBrowseIID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.bBrowseIID.Location = new System.Drawing.Point(558, 126);
            this.bBrowseIID.Margin = new System.Windows.Forms.Padding(0);
            this.bBrowseIID.Name = "bBrowseIID";
            this.bBrowseIID.Size = new System.Drawing.Size(28, 28);
            this.bBrowseIID.TabIndex = 2;
            this.bBrowseIID.Text = "...";
            this.bBrowseIID.UseVisualStyleBackColor = false;
            this.bBrowseIID.Click += new System.EventHandler(this.bBrowseIID_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(196)))), ((int)(((byte)(195)))));
            this.label1.Location = new System.Drawing.Point(13, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "IID Project:";
            // 
            // tbIIDProject
            // 
            this.tbIIDProject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(72)))), ((int)(((byte)(70)))));
            this.tbIIDProject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbIIDProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbIIDProject.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.tbIIDProject.Location = new System.Drawing.Point(144, 126);
            this.tbIIDProject.Multiline = true;
            this.tbIIDProject.Name = "tbIIDProject";
            this.tbIIDProject.Size = new System.Drawing.Size(405, 28);
            this.tbIIDProject.TabIndex = 0;
            this.tbIIDProject.WordWrap = false;
            // 
            // bOpenIIDProject
            // 
            this.bOpenIIDProject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(62)))), ((int)(((byte)(60)))));
            this.bOpenIIDProject.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.bOpenIIDProject.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(96)))), ((int)(((byte)(94)))));
            this.bOpenIIDProject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOpenIIDProject.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bOpenIIDProject.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.bOpenIIDProject.Location = new System.Drawing.Point(595, 126);
            this.bOpenIIDProject.Margin = new System.Windows.Forms.Padding(0);
            this.bOpenIIDProject.Name = "bOpenIIDProject";
            this.bOpenIIDProject.Size = new System.Drawing.Size(104, 28);
            this.bOpenIIDProject.TabIndex = 6;
            this.bOpenIIDProject.Text = "Open in IID";
            this.bOpenIIDProject.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.bOpenIIDProject.UseVisualStyleBackColor = false;
            this.bOpenIIDProject.Click += new System.EventHandler(this.bOpenIIDProject_Click);
            // 
            // bNew
            // 
            this.bNew.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(54)))), ((int)(((byte)(53)))));
            this.bNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(96)))), ((int)(((byte)(94)))));
            this.bNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bNew.Image = ((System.Drawing.Image)(resources.GetObject("bNew.Image")));
            this.bNew.Location = new System.Drawing.Point(10, 10);
            this.bNew.Name = "bNew";
            this.bNew.Size = new System.Drawing.Size(31, 29);
            this.bNew.TabIndex = 8;
            this.bNew.UseVisualStyleBackColor = true;
            this.bNew.Click += new System.EventHandler(this.bNew_Click);
            // 
            // bSave
            // 
            this.bSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(54)))), ((int)(((byte)(53)))));
            this.bSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(96)))), ((int)(((byte)(94)))));
            this.bSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSave.Image = ((System.Drawing.Image)(resources.GetObject("bSave.Image")));
            this.bSave.Location = new System.Drawing.Point(45, 10);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(31, 29);
            this.bSave.TabIndex = 9;
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bOpen
            // 
            this.bOpen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(54)))), ((int)(((byte)(53)))));
            this.bOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(96)))), ((int)(((byte)(94)))));
            this.bOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOpen.Image = ((System.Drawing.Image)(resources.GetObject("bOpen.Image")));
            this.bOpen.Location = new System.Drawing.Point(82, 10);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(31, 29);
            this.bOpen.TabIndex = 10;
            this.bOpen.UseVisualStyleBackColor = true;
            this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
            // 
            // skLogo
            // 
            this.skLogo.Image = ((System.Drawing.Image)(resources.GetObject("skLogo.Image")));
            this.skLogo.Location = new System.Drawing.Point(749, 10);
            this.skLogo.Name = "skLogo";
            this.skLogo.Size = new System.Drawing.Size(218, 49);
            this.skLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.skLogo.TabIndex = 11;
            this.skLogo.TabStop = false;
            // 
            // EmulatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(54)))), ((int)(((byte)(53)))));
            this.ClientSize = new System.Drawing.Size(980, 398);
            this.Controls.Add(this.skLogo);
            this.Controls.Add(this.bOpen);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.bNew);
            this.Controls.Add(this.bOpenIIDProject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bToolBox);
            this.Controls.Add(this.cbStartStop);
            this.Controls.Add(this.cbPlay);
            this.Controls.Add(this.dgMappings);
            this.Controls.Add(this.bBrowseIID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbIIDProject);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(730, 326);
            this.Name = "EmulatorView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "iisu Emulator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EmulatorView_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.EmulatorView_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgMappings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.skLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbPlay;
        private System.Windows.Forms.TextBox tbIIDProject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bBrowseIID;
        private System.Windows.Forms.DataGridView dgMappings;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbStartStop;
        private System.Windows.Forms.Button bToolBox;
        private System.Windows.Forms.Button bOpenIIDProject;
        private System.Windows.Forms.DataGridViewCheckBoxColumn mappingEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn IIDOutput;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataType;
        private System.Windows.Forms.DataGridViewComboBoxColumn Emulator;
        private System.Windows.Forms.DataGridViewComboBoxColumn DataSource;
        private System.Windows.Forms.Button bNew;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bOpen;
        private System.Windows.Forms.PictureBox skLogo;
    }
}

