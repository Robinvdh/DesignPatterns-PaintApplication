namespace DesignPatterns_PaintApplication
{
    partial class Entity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Entity));
            topMenuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            btnNewFile = new ToolStripMenuItem();
            btnOpenFile = new ToolStripMenuItem();
            btnSaveFile = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            btnUndoToolStripMenuItem = new ToolStripMenuItem();
            btnRedoToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            btnAboutUsToolStripMenuItem = new ToolStripMenuItem();
            toolBar = new ToolStrip();
            btnSelect = new ToolStripButton();
            btnResize = new ToolStripButton();
            btnEllipse = new ToolStripButton();
            btnRectangle = new ToolStripButton();
            btnRemove = new ToolStripButton();
            btnAddGroup = new Button();
            treeView = new TreeView();
            paintPanel = new Panel();
            topMenuStrip.SuspendLayout();
            toolBar.SuspendLayout();
            SuspendLayout();
            // 
            // topMenuStrip
            // 
            topMenuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, toolStripMenuItem1, helpToolStripMenuItem });
            topMenuStrip.Location = new Point(0, 0);
            topMenuStrip.Name = "topMenuStrip";
            topMenuStrip.Size = new Size(800, 24);
            topMenuStrip.TabIndex = 0;
            topMenuStrip.Text = "topMenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { btnNewFile, btnOpenFile, btnSaveFile });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(61, 20);
            fileToolStripMenuItem.Text = "Bestand";
            // 
            // btnNewFile
            // 
            btnNewFile.Name = "btnNewFile";
            btnNewFile.ShortcutKeys = Keys.Control | Keys.N;
            btnNewFile.Size = new Size(180, 22);
            btnNewFile.Text = "Nieuw";
            btnNewFile.Click += btnNewFile_Click;
            // 
            // btnOpenFile
            // 
            btnOpenFile.Name = "btnOpenFile";
            btnOpenFile.ShortcutKeys = Keys.Control | Keys.O;
            btnOpenFile.Size = new Size(180, 22);
            btnOpenFile.Text = "Openen";
            btnOpenFile.Click += btnOpenFile_Click;
            // 
            // btnSaveFile
            // 
            btnSaveFile.Name = "btnSaveFile";
            btnSaveFile.Size = new Size(180, 22);
            btnSaveFile.Text = "Opslaan";
            btnSaveFile.Click += btnSaveFile_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { btnUndoToolStripMenuItem, btnRedoToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(70, 20);
            editToolStripMenuItem.Text = "Bewerken";
            // 
            // btnUndoToolStripMenuItem
            // 
            btnUndoToolStripMenuItem.Name = "btnUndoToolStripMenuItem";
            btnUndoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
            btnUndoToolStripMenuItem.Size = new Size(216, 22);
            btnUndoToolStripMenuItem.Text = "Ongedaan maken";
            btnUndoToolStripMenuItem.Click += btnUndoToolStripMenuItem_Click;
            // 
            // btnRedoToolStripMenuItem
            // 
            btnRedoToolStripMenuItem.Name = "btnRedoToolStripMenuItem";
            btnRedoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Y;
            btnRedoToolStripMenuItem.Size = new Size(216, 22);
            btnRedoToolStripMenuItem.Text = "Opnieuw uitvoeren";
            btnRedoToolStripMenuItem.Click += btnRedoToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(12, 20);
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { btnAboutUsToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // btnAboutUsToolStripMenuItem
            // 
            btnAboutUsToolStripMenuItem.Name = "btnAboutUsToolStripMenuItem";
            btnAboutUsToolStripMenuItem.Size = new Size(121, 22);
            btnAboutUsToolStripMenuItem.Text = "Over ons";
            btnAboutUsToolStripMenuItem.Click += btnAboutUsToolStripMenuItem_Click;
            // 
            // toolBar
            // 
            toolBar.GripMargin = new Padding(4);
            toolBar.ImageScalingSize = new Size(20, 20);
            toolBar.Items.AddRange(new ToolStripItem[] { btnSelect, btnResize, btnEllipse, btnRectangle, btnRemove });
            toolBar.Location = new Point(0, 24);
            toolBar.Name = "toolBar";
            toolBar.Size = new Size(800, 27);
            toolBar.TabIndex = 1;
            toolBar.Text = "toolBar";
            // 
            // btnSelect
            // 
            btnSelect.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSelect.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnSelect.Image = (Image)resources.GetObject("btnSelect.Image");
            btnSelect.ImageTransparentColor = Color.Magenta;
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(24, 24);
            btnSelect.Text = "btnSelect";
            btnSelect.Click += btnSelect_Click;
            // 
            // btnResize
            // 
            btnResize.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnResize.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnResize.Image = (Image)resources.GetObject("btnResize.Image");
            btnResize.ImageTransparentColor = Color.Magenta;
            btnResize.Name = "btnResize";
            btnResize.Size = new Size(24, 24);
            btnResize.Text = "btnResize";
            btnResize.Click += btnResize_Click;
            // 
            // btnEllipse
            // 
            btnEllipse.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnEllipse.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnEllipse.Image = (Image)resources.GetObject("btnEllipse.Image");
            btnEllipse.ImageTransparentColor = Color.Magenta;
            btnEllipse.Name = "btnEllipse";
            btnEllipse.Size = new Size(24, 24);
            btnEllipse.Text = "btnEllipse";
            btnEllipse.Click += btnEllipse_Click;
            // 
            // btnRectangle
            // 
            btnRectangle.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnRectangle.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnRectangle.Image = (Image)resources.GetObject("btnRectangle.Image");
            btnRectangle.ImageTransparentColor = Color.Magenta;
            btnRectangle.Name = "btnRectangle";
            btnRectangle.Size = new Size(24, 24);
            btnRectangle.Text = "btnRectangle";
            btnRectangle.Click += btnRectangle_Click;
            // 
            // btnRemove
            // 
            btnRemove.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnRemove.Image = (Image)resources.GetObject("btnRemove.Image");
            btnRemove.ImageTransparentColor = Color.Magenta;
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(24, 24);
            btnRemove.Text = "btnRemove";
            btnRemove.Click += btnRemove_Click;
            // 
            // btnAddGroup
            // 
            btnAddGroup.Location = new Point(637, 58);
            btnAddGroup.Name = "btnAddGroup";
            btnAddGroup.Size = new Size(151, 23);
            btnAddGroup.TabIndex = 5;
            btnAddGroup.Text = "Toevoegen";
            btnAddGroup.UseVisualStyleBackColor = true;
            btnAddGroup.Click += btnAddGroup_Click;
            // 
            // treeView
            // 
            treeView.Location = new Point(637, 87);
            treeView.Name = "treeView";
            treeView.Size = new Size(151, 351);
            treeView.TabIndex = 6;
            treeView.AfterLabelEdit += treeView_AfterLabelEdit;
            treeView.AfterSelect += treeView_AfterSelect;
            treeView.NodeMouseClick += treeView_NodeMouseClick;
            // 
            // paintPanel
            // 
            paintPanel.BorderStyle = BorderStyle.Fixed3D;
            paintPanel.Location = new Point(12, 58);
            paintPanel.Name = "paintPanel";
            paintPanel.Size = new Size(610, 380);
            paintPanel.TabIndex = 7;
            paintPanel.Paint += panel_Paint;
            paintPanel.MouseDown += paintPanel_MouseDown;
            paintPanel.MouseMove += paintPanel_MouseMove;
            paintPanel.MouseUp += paintPanel_MouseUp;
            // 
            // Entity
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(paintPanel);
            Controls.Add(treeView);
            Controls.Add(btnAddGroup);
            Controls.Add(toolBar);
            Controls.Add(topMenuStrip);
            MainMenuStrip = topMenuStrip;
            Name = "Entity";
            Text = "PaintApplication";
            topMenuStrip.ResumeLayout(false);
            topMenuStrip.PerformLayout();
            toolBar.ResumeLayout(false);
            toolBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip topMenuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem btnNewFile;
        private ToolStripMenuItem btnOpenFile;
        private ToolStripMenuItem btnSaveFile;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem btnUndoToolStripMenuItem;
        private ToolStripMenuItem btnRedoToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem btnAboutUsToolStripMenuItem;
        private ToolStrip toolBar;
        private ToolStripButton btnSelect;
        private ToolStripButton btnResize;
        private ToolStripButton btnEllipse;
        private ToolStripButton btnRectangle;
        private ToolStripButton btnRemove;
        private Button btnAddGroup;
        private TreeView treeView;
        private Panel paintPanel;
    }
}