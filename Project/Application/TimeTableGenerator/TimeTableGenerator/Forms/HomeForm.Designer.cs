
namespace TImeTableGenerator.Forms
{
    partial class HomeForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslblDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.tsbProgram = new System.Windows.Forms.ToolStripButton();
            this.tsbSession = new System.Windows.Forms.ToolStripButton();
            this.tsbSubject = new System.Windows.Forms.ToolStripButton();
            this.tssdLecture = new System.Windows.Forms.ToolStripDropDownButton();
            this.newLectureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assignSubjectsToLectureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tssdRoomLab = new System.Windows.Forms.ToolStripDropDownButton();
            this.addRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addLabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tssdSemesters = new System.Windows.Forms.ToolStripDropDownButton();
            this.newSemestersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSemesterSectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assignSemesterToProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assignSubjectToSemesterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tssdDays = new System.Windows.Forms.ToolStripDropDownButton();
            this.addDayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dayTimeSlotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbGenerate = new System.Windows.Forms.ToolStripButton();
            this.tssdPrint = new System.Windows.Forms.ToolStripDropDownButton();
            this.printAllTimeTablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printAllTeacherTimeTablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printAllDayWiseTimeTablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbProgram,
            this.tsbSession,
            this.tsbSubject,
            this.tssdLecture,
            this.tssdRoomLab,
            this.tssdSemesters,
            this.tssdDays,
            this.tsbGenerate,
            this.tssdPrint});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1624, 75);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(94, 20);
            this.toolStripStatusLabel1.Text = "        Ready....";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tsslblDateTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 739);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1624, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = " .   Ready ........";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // tsslblDateTime
            // 
            this.tsslblDateTime.Name = "tsslblDateTime";
            this.tsslblDateTime.Size = new System.Drawing.Size(1515, 20);
            this.tsslblDateTime.Spring = true;
            this.tsslblDateTime.Text = "toolStripStatusLabel2";
            this.tsslblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelHeader
            // 
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHeader.Location = new System.Drawing.Point(0, 75);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1624, 664);
            this.panelHeader.TabIndex = 2;
            // 
            // tsbProgram
            // 
            this.tsbProgram.Image = global::TImeTableGenerator.Properties.Resources.programicon;
            this.tsbProgram.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbProgram.Name = "tsbProgram";
            this.tsbProgram.Size = new System.Drawing.Size(70, 72);
            this.tsbProgram.Text = "Program";
            this.tsbProgram.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbProgram.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsbSession
            // 
            this.tsbSession.Image = global::TImeTableGenerator.Properties.Resources.sessionicon;
            this.tsbSession.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSession.Name = "tsbSession";
            this.tsbSession.Size = new System.Drawing.Size(62, 72);
            this.tsbSession.Text = "Session";
            this.tsbSession.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSession.Click += new System.EventHandler(this.tsbSession_Click);
            // 
            // tsbSubject
            // 
            this.tsbSubject.Image = global::TImeTableGenerator.Properties.Resources.subjecticon;
            this.tsbSubject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSubject.Name = "tsbSubject";
            this.tsbSubject.Size = new System.Drawing.Size(62, 72);
            this.tsbSubject.Text = "Subject";
            this.tsbSubject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSubject.Click += new System.EventHandler(this.tsbSubject_Click);
            // 
            // tssdLecture
            // 
            this.tssdLecture.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newLectureToolStripMenuItem,
            this.assignSubjectsToLectureToolStripMenuItem});
            this.tssdLecture.Image = global::TImeTableGenerator.Properties.Resources.teachericon;
            this.tssdLecture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssdLecture.Name = "tssdLecture";
            this.tssdLecture.Size = new System.Drawing.Size(77, 72);
            this.tssdLecture.Text = "Lectures";
            this.tssdLecture.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // newLectureToolStripMenuItem
            // 
            this.newLectureToolStripMenuItem.Name = "newLectureToolStripMenuItem";
            this.newLectureToolStripMenuItem.Size = new System.Drawing.Size(266, 26);
            this.newLectureToolStripMenuItem.Text = "New Lecture";
            this.newLectureToolStripMenuItem.Click += new System.EventHandler(this.newLectureToolStripMenuItem_Click_1);
            // 
            // assignSubjectsToLectureToolStripMenuItem
            // 
            this.assignSubjectsToLectureToolStripMenuItem.Name = "assignSubjectsToLectureToolStripMenuItem";
            this.assignSubjectsToLectureToolStripMenuItem.Size = new System.Drawing.Size(266, 26);
            this.assignSubjectsToLectureToolStripMenuItem.Text = "Assign Subjects To Lecture";
            this.assignSubjectsToLectureToolStripMenuItem.Click += new System.EventHandler(this.assignSubjectsToLectureToolStripMenuItem_Click);
            // 
            // tssdRoomLab
            // 
            this.tssdRoomLab.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRoomToolStripMenuItem,
            this.addLabToolStripMenuItem});
            this.tssdRoomLab.Image = global::TImeTableGenerator.Properties.Resources.roomsicon;
            this.tssdRoomLab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssdRoomLab.Name = "tssdRoomLab";
            this.tssdRoomLab.Size = new System.Drawing.Size(132, 72);
            this.tssdRoomLab.Text = "Rooms and Labs";
            this.tssdRoomLab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // addRoomToolStripMenuItem
            // 
            this.addRoomToolStripMenuItem.Name = "addRoomToolStripMenuItem";
            this.addRoomToolStripMenuItem.Size = new System.Drawing.Size(164, 26);
            this.addRoomToolStripMenuItem.Text = "Add Room";
            this.addRoomToolStripMenuItem.Click += new System.EventHandler(this.addRoomToolStripMenuItem_Click);
            // 
            // addLabToolStripMenuItem
            // 
            this.addLabToolStripMenuItem.Name = "addLabToolStripMenuItem";
            this.addLabToolStripMenuItem.Size = new System.Drawing.Size(164, 26);
            this.addLabToolStripMenuItem.Text = "Add Lab";
            this.addLabToolStripMenuItem.Click += new System.EventHandler(this.addLabToolStripMenuItem_Click);
            // 
            // tssdSemesters
            // 
            this.tssdSemesters.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newSemestersToolStripMenuItem,
            this.addSemesterSectionsToolStripMenuItem,
            this.assignSemesterToProgramToolStripMenuItem,
            this.assignSubjectToSemesterToolStripMenuItem});
            this.tssdSemesters.Image = global::TImeTableGenerator.Properties.Resources.semestericon;
            this.tssdSemesters.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssdSemesters.Name = "tssdSemesters";
            this.tssdSemesters.Size = new System.Drawing.Size(90, 72);
            this.tssdSemesters.Text = "Semesters";
            this.tssdSemesters.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // newSemestersToolStripMenuItem
            // 
            this.newSemestersToolStripMenuItem.Name = "newSemestersToolStripMenuItem";
            this.newSemestersToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.newSemestersToolStripMenuItem.Text = "New Semesters";
            this.newSemestersToolStripMenuItem.Click += new System.EventHandler(this.newSemestersToolStripMenuItem_Click);
            // 
            // addSemesterSectionsToolStripMenuItem
            // 
            this.addSemesterSectionsToolStripMenuItem.Name = "addSemesterSectionsToolStripMenuItem";
            this.addSemesterSectionsToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.addSemesterSectionsToolStripMenuItem.Text = "Add Semester Sections";
            this.addSemesterSectionsToolStripMenuItem.Click += new System.EventHandler(this.addSemesterSectionsToolStripMenuItem_Click);
            // 
            // assignSemesterToProgramToolStripMenuItem
            // 
            this.assignSemesterToProgramToolStripMenuItem.Name = "assignSemesterToProgramToolStripMenuItem";
            this.assignSemesterToProgramToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.assignSemesterToProgramToolStripMenuItem.Text = "Assign Semester to Program";
            this.assignSemesterToProgramToolStripMenuItem.Click += new System.EventHandler(this.assignSemesterToProgramToolStripMenuItem_Click);
            // 
            // assignSubjectToSemesterToolStripMenuItem
            // 
            this.assignSubjectToSemesterToolStripMenuItem.Name = "assignSubjectToSemesterToolStripMenuItem";
            this.assignSubjectToSemesterToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.assignSubjectToSemesterToolStripMenuItem.Text = "Assign Subject to Semester";
            this.assignSubjectToSemesterToolStripMenuItem.Click += new System.EventHandler(this.assignSubjectToSemesterToolStripMenuItem_Click);
            // 
            // tssdDays
            // 
            this.tssdDays.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDayToolStripMenuItem,
            this.dayTimeSlotToolStripMenuItem});
            this.tssdDays.Image = global::TImeTableGenerator.Properties.Resources.daysicon;
            this.tssdDays.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssdDays.Name = "tssdDays";
            this.tssdDays.Size = new System.Drawing.Size(62, 72);
            this.tssdDays.Text = "Days";
            this.tssdDays.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // addDayToolStripMenuItem
            // 
            this.addDayToolStripMenuItem.Name = "addDayToolStripMenuItem";
            this.addDayToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.addDayToolStripMenuItem.Text = "Add Day";
            this.addDayToolStripMenuItem.Click += new System.EventHandler(this.addDayToolStripMenuItem_Click);
            // 
            // dayTimeSlotToolStripMenuItem
            // 
            this.dayTimeSlotToolStripMenuItem.Name = "dayTimeSlotToolStripMenuItem";
            this.dayTimeSlotToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.dayTimeSlotToolStripMenuItem.Text = "Day Time Slot";
            this.dayTimeSlotToolStripMenuItem.Click += new System.EventHandler(this.dayTimeSlotToolStripMenuItem_Click);
            // 
            // tsbGenerate
            // 
            this.tsbGenerate.Image = global::TImeTableGenerator.Properties.Resources.timetableicon;
            this.tsbGenerate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGenerate.Name = "tsbGenerate";
            this.tsbGenerate.Size = new System.Drawing.Size(149, 72);
            this.tsbGenerate.Text = "Generate Time Table";
            this.tsbGenerate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbGenerate.Click += new System.EventHandler(this.tsbGenerate_Click);
            // 
            // tssdPrint
            // 
            this.tssdPrint.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printAllTimeTablesToolStripMenuItem,
            this.printAllTeacherTimeTablesToolStripMenuItem,
            this.printAllDayWiseTimeTablesToolStripMenuItem});
            this.tssdPrint.Image = global::TImeTableGenerator.Properties.Resources.printsicon;
            this.tssdPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssdPrint.Name = "tssdPrint";
            this.tssdPrint.Size = new System.Drawing.Size(135, 72);
            this.tssdPrint.Text = "Print TIme Tables";
            this.tssdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // printAllTimeTablesToolStripMenuItem
            // 
            this.printAllTimeTablesToolStripMenuItem.Name = "printAllTimeTablesToolStripMenuItem";
            this.printAllTimeTablesToolStripMenuItem.Size = new System.Drawing.Size(292, 26);
            this.printAllTimeTablesToolStripMenuItem.Text = "Print All Time Tables";
            this.printAllTimeTablesToolStripMenuItem.Click += new System.EventHandler(this.printAllTimeTablesToolStripMenuItem_Click);
            // 
            // printAllTeacherTimeTablesToolStripMenuItem
            // 
            this.printAllTeacherTimeTablesToolStripMenuItem.Name = "printAllTeacherTimeTablesToolStripMenuItem";
            this.printAllTeacherTimeTablesToolStripMenuItem.Size = new System.Drawing.Size(292, 26);
            this.printAllTeacherTimeTablesToolStripMenuItem.Text = "Print All Teacher Time Tables";
            this.printAllTeacherTimeTablesToolStripMenuItem.Click += new System.EventHandler(this.printAllTeacherTimeTablesToolStripMenuItem_Click);
            // 
            // printAllDayWiseTimeTablesToolStripMenuItem
            // 
            this.printAllDayWiseTimeTablesToolStripMenuItem.Name = "printAllDayWiseTimeTablesToolStripMenuItem";
            this.printAllDayWiseTimeTablesToolStripMenuItem.Size = new System.Drawing.Size(292, 26);
            this.printAllDayWiseTimeTablesToolStripMenuItem.Text = "Print All Day Wise Time Tables";
            this.printAllDayWiseTimeTablesToolStripMenuItem.Click += new System.EventHandler(this.printAllDayWiseTimeTablesToolStripMenuItem_Click);
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1624, 765);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "HomeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HomeForm";
            this.Load += new System.EventHandler(this.HomeForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslblDateTime;
        private System.Windows.Forms.ToolStripButton tsbProgram;
        private System.Windows.Forms.ToolStripButton tsbSession;
        private System.Windows.Forms.ToolStripButton tsbSubject;
        private System.Windows.Forms.ToolStripDropDownButton tssdLecture;
        private System.Windows.Forms.ToolStripMenuItem newLectureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assignSubjectsToLectureToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton tssdRoomLab;
        private System.Windows.Forms.ToolStripMenuItem addRoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addLabToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton tssdSemesters;
        private System.Windows.Forms.ToolStripMenuItem newSemestersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSemesterSectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assignSemesterToProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assignSubjectToSemesterToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton tssdDays;
        private System.Windows.Forms.ToolStripMenuItem addDayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dayTimeSlotToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbGenerate;
        private System.Windows.Forms.ToolStripDropDownButton tssdPrint;
        private System.Windows.Forms.ToolStripMenuItem printAllTimeTablesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printAllTeacherTimeTablesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printAllDayWiseTimeTablesToolStripMenuItem;
        private System.Windows.Forms.Panel panelHeader;
    }
}