namespace eRepublik_MU_Stats
{
    partial class frmMain
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
            this.lvMembers = new System.Windows.Forms.ListView();
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colProfileLink = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStrength = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStrengthGained = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRank = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRankPoints = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRankPointsGained = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colInfluence = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExperience = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExperienceGained = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNationalRank = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Log = new System.Windows.Forms.TextBox();
            this.btnAbout = new eRepublik_MU_Stats.CustomButton();
            this.btnHelp = new eRepublik_MU_Stats.CustomButton();
            this.btnWriteArticle = new eRepublik_MU_Stats.CustomButton();
            this.btnScan = new eRepublik_MU_Stats.CustomButton();
            this.SuspendLayout();
            // 
            // lvMembers
            // 
            this.lvMembers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colID,
            this.colName,
            this.colProfileLink,
            this.colStrength,
            this.colStrengthGained,
            this.colRank,
            this.colRankPoints,
            this.colRankPointsGained,
            this.colInfluence,
            this.colHit,
            this.colExperience,
            this.colExperienceGained,
            this.colLevel,
            this.colNationalRank});
            this.lvMembers.FullRowSelect = true;
            this.lvMembers.GridLines = true;
            this.lvMembers.HideSelection = false;
            this.lvMembers.Location = new System.Drawing.Point(12, 12);
            this.lvMembers.Name = "lvMembers";
            this.lvMembers.Size = new System.Drawing.Size(1105, 293);
            this.lvMembers.TabIndex = 0;
            this.lvMembers.UseCompatibleStateImageBehavior = false;
            this.lvMembers.View = System.Windows.Forms.View.Details;
            // 
            // colID
            // 
            this.colID.Text = "ID";
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 110;
            // 
            // colProfileLink
            // 
            this.colProfileLink.Text = "Profile Link";
            this.colProfileLink.Width = 85;
            // 
            // colStrength
            // 
            this.colStrength.Text = "Strength";
            this.colStrength.Width = 62;
            // 
            // colStrengthGained
            // 
            this.colStrengthGained.Text = "Strength +";
            this.colStrengthGained.Width = 69;
            // 
            // colRank
            // 
            this.colRank.Text = "Rank";
            this.colRank.Width = 200;
            // 
            // colRankPoints
            // 
            this.colRankPoints.Text = "Rank Pts";
            this.colRankPoints.Width = 78;
            // 
            // colRankPointsGained
            // 
            this.colRankPointsGained.Text = "Rank Pts +";
            this.colRankPointsGained.Width = 85;
            // 
            // colInfluence
            // 
            this.colInfluence.Text = "Influence";
            // 
            // colHit
            // 
            this.colHit.Text = "Hit";
            // 
            // colExperience
            // 
            this.colExperience.Text = "Xp";
            // 
            // colExperienceGained
            // 
            this.colExperienceGained.Text = "Xp +";
            // 
            // colLevel
            // 
            this.colLevel.Text = "LvL";
            // 
            // colNationalRank
            // 
            this.colNationalRank.Text = "Nat Rank";
            // 
            // Log
            // 
            this.Log.Location = new System.Drawing.Point(1123, 236);
            this.Log.Multiline = true;
            this.Log.Name = "Log";
            this.Log.ReadOnly = true;
            this.Log.Size = new System.Drawing.Size(215, 69);
            this.Log.TabIndex = 2;
            // 
            // btnAbout
            // 
            this.btnAbout.BackColor = System.Drawing.Color.Transparent;
            this.btnAbout.FlatAppearance.BorderSize = 0;
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbout.Font = new System.Drawing.Font("Arial Narrow", 21F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbout.ForeColor = System.Drawing.Color.White;
            this.btnAbout.Location = new System.Drawing.Point(1123, 180);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.ShadowColor = System.Drawing.Color.Black;
            this.btnAbout.Size = new System.Drawing.Size(215, 50);
            this.btnAbout.TabIndex = 6;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = false;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.BackColor = System.Drawing.Color.Transparent;
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Font = new System.Drawing.Font("Arial Narrow", 21F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnHelp.ForeColor = System.Drawing.Color.White;
            this.btnHelp.Location = new System.Drawing.Point(1123, 124);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.ShadowColor = System.Drawing.Color.Black;
            this.btnHelp.Size = new System.Drawing.Size(215, 50);
            this.btnHelp.TabIndex = 5;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnWriteArticle
            // 
            this.btnWriteArticle.BackColor = System.Drawing.Color.Transparent;
            this.btnWriteArticle.FlatAppearance.BorderSize = 0;
            this.btnWriteArticle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWriteArticle.Font = new System.Drawing.Font("Arial Narrow", 21F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWriteArticle.ForeColor = System.Drawing.Color.White;
            this.btnWriteArticle.Location = new System.Drawing.Point(1123, 68);
            this.btnWriteArticle.Name = "btnWriteArticle";
            this.btnWriteArticle.ShadowColor = System.Drawing.Color.Black;
            this.btnWriteArticle.Size = new System.Drawing.Size(215, 50);
            this.btnWriteArticle.TabIndex = 4;
            this.btnWriteArticle.Text = "Write Article";
            this.btnWriteArticle.UseVisualStyleBackColor = false;
            this.btnWriteArticle.Click += new System.EventHandler(this.btnWriteArticle_Click);
            // 
            // btnScan
            // 
            this.btnScan.BackColor = System.Drawing.Color.Transparent;
            this.btnScan.FlatAppearance.BorderSize = 0;
            this.btnScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScan.Font = new System.Drawing.Font("Arial Narrow", 21F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScan.ForeColor = System.Drawing.Color.White;
            this.btnScan.Location = new System.Drawing.Point(1123, 12);
            this.btnScan.Name = "btnScan";
            this.btnScan.ShadowColor = System.Drawing.Color.Black;
            this.btnScan.Size = new System.Drawing.Size(215, 50);
            this.btnScan.TabIndex = 3;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = false;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 317);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnWriteArticle);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.Log);
            this.Controls.Add(this.lvMembers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "eRepublik MU Stats";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvMembers;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colProfileLink;
        private System.Windows.Forms.ColumnHeader colStrength;
        private System.Windows.Forms.ColumnHeader colStrengthGained;
        private System.Windows.Forms.ColumnHeader colRank;
        private System.Windows.Forms.ColumnHeader colRankPoints;
        private System.Windows.Forms.ColumnHeader colRankPointsGained;
        private System.Windows.Forms.ColumnHeader colInfluence;
        private System.Windows.Forms.ColumnHeader colHit;
        private System.Windows.Forms.ColumnHeader colExperience;
        private System.Windows.Forms.ColumnHeader colExperienceGained;
        private System.Windows.Forms.ColumnHeader colLevel;
        private System.Windows.Forms.ColumnHeader colNationalRank;
        private System.Windows.Forms.TextBox Log;
        private CustomButton btnScan;
        private CustomButton btnWriteArticle;
        private CustomButton btnHelp;
        private CustomButton btnAbout;
    }
}

