﻿namespace ProjectLaunch
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            this.listviewOverview = new System.Windows.Forms.ListView();
            this.SolutionFile = new System.Windows.Forms.ColumnHeader();
            this.GitDirectory = new System.Windows.Forms.ColumnHeader();
            this.Branch = new System.Windows.Forms.ColumnHeader();
            this.GitStatus = new System.Windows.Forms.ColumnHeader();
            this.GitMessage = new System.Windows.Forms.ColumnHeader();
            this.GitAuthoer = new System.Windows.Forms.ColumnHeader();
            this.GitRemoteChanges = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStripRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pullWithGitExtentionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.launchVisualStudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fucktionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelInfo = new System.Windows.Forms.Label();
            this.contextMenuStripRightClick.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listviewOverview
            // 
            this.listviewOverview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listviewOverview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SolutionFile,
            this.GitDirectory,
            this.Branch,
            this.GitStatus,
            this.GitMessage,
            this.GitAuthoer,
            this.GitRemoteChanges});
            this.listviewOverview.ContextMenuStrip = this.contextMenuStripRightClick;
            this.listviewOverview.FullRowSelect = true;
            this.listviewOverview.Location = new System.Drawing.Point(14, 46);
            this.listviewOverview.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listviewOverview.Name = "listviewOverview";
            this.listviewOverview.Size = new System.Drawing.Size(1278, 620);
            this.listviewOverview.TabIndex = 0;
            this.listviewOverview.UseCompatibleStateImageBehavior = false;
            this.listviewOverview.View = System.Windows.Forms.View.Details;
            this.listviewOverview.DoubleClick += new System.EventHandler(this.listViewOverview_DoubleClick);
            // 
            // SolutionFile
            // 
            this.SolutionFile.Text = "Solution";
            this.SolutionFile.Width = 200;
            // 
            // GitDirectory
            // 
            this.GitDirectory.Text = "Git Directory";
            this.GitDirectory.Width = 300;
            // 
            // Branch
            // 
            this.Branch.Text = "Branch";
            this.Branch.Width = 150;
            // 
            // GitStatus
            // 
            this.GitStatus.Text = "Git status";
            this.GitStatus.Width = 175;
            // 
            // GitMessage
            // 
            this.GitMessage.Text = "GitMessage";
            this.GitMessage.Width = 250;
            // 
            // GitAuthoer
            // 
            this.GitAuthoer.Text = "GitAuthoer";
            // 
            // GitRemoteChanges
            // 
            this.GitRemoteChanges.Text = "GitRemoteChanges";
            // 
            // contextMenuStripRightClick
            // 
            this.contextMenuStripRightClick.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pullWithGitExtentionsToolStripMenuItem,
            this.launchVisualStudioToolStripMenuItem});
            this.contextMenuStripRightClick.Name = "contextMenuStrip1";
            this.contextMenuStripRightClick.Size = new System.Drawing.Size(229, 52);
            this.contextMenuStripRightClick.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // pullWithGitExtentionsToolStripMenuItem
            // 
            this.pullWithGitExtentionsToolStripMenuItem.Name = "pullWithGitExtentionsToolStripMenuItem";
            this.pullWithGitExtentionsToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.pullWithGitExtentionsToolStripMenuItem.Text = "Pull with git extentions";
            this.pullWithGitExtentionsToolStripMenuItem.Click += new System.EventHandler(this.pullWithGitExtentionsToolStripMenuItem_Click);
            // 
            // launchVisualStudioToolStripMenuItem
            // 
            this.launchVisualStudioToolStripMenuItem.Name = "launchVisualStudioToolStripMenuItem";
            this.launchVisualStudioToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.launchVisualStudioToolStripMenuItem.Text = "Launch Visual studio";
            this.launchVisualStudioToolStripMenuItem.Click += new System.EventHandler(this.launchVisualStudioToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.fucktionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1304, 30);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // fucktionsToolStripMenuItem
            // 
            this.fucktionsToolStripMenuItem.Name = "fucktionsToolStripMenuItem";
            this.fucktionsToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
            this.fucktionsToolStripMenuItem.Text = "Update";
            this.fucktionsToolStripMenuItem.Click += new System.EventHandler(this.functionsToolStripMenuItem_Click);
            // 
            // labelInfo
            // 
            this.labelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(14, 670);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(50, 20);
            this.labelInfo.TabIndex = 2;
            this.labelInfo.Text = "Ready";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1304, 699);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.listviewOverview);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormMain";
            this.Text = "Project Launcher";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.contextMenuStripRightClick.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView listviewOverview;
        private ColumnHeader SolutionFile;
        private ColumnHeader GitDirectory;
        private ColumnHeader Branch;
        private ColumnHeader GitStatus;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem fucktionsToolStripMenuItem;
        private ColumnHeader GitMessage;
        private ColumnHeader GitAuthoer;
        private ColumnHeader GitRemoteChanges;
        private ContextMenuStrip contextMenuStripRightClick;
        private ToolStripMenuItem pullWithGitExtentionsToolStripMenuItem;
        private ToolStripMenuItem launchVisualStudioToolStripMenuItem;
        private Label labelInfo;
    }
}