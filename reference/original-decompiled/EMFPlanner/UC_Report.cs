using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.FileIO;

namespace EMFPlanner;

[DesignerGenerated]
public class UC_Report : UserControl
{
	[Serializable]
	public struct SerAlias
	{
		public string[,] ALS;
	}

	private IContainer components;

	private bool FlagOK;

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdWord1")]
	internal virtual Button cmdWord1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdWord1_Click;
			Button button = field;
			if (button != null)
			{
				button.Click -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdRef1")]
	internal virtual Button cmdRef1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdRef1_Click;
			Button button = field;
			if (button != null)
			{
				button.Click -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolTip1")]
	internal virtual ToolTip ToolTip1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("GroupBox2")]
	internal virtual GroupBox GroupBox2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdSelectFileTemplate")]
	internal virtual Button cmdSelectFileTemplate
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdSelectFileTemplate_Click;
			Button button = field;
			if (button != null)
			{
				button.Click -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("txtTemplateDOC")]
	internal virtual TextBox txtTemplateDOC
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = txtTemplateDOC_TextChanged;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.TextChanged -= value2;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.TextChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("GroupBox3")]
	internal virtual GroupBox GroupBox3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdRestoreAliases")]
	internal virtual Button cmdRestoreAliases
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdRestoreAliases_Click;
			Button button = field;
			if (button != null)
			{
				button.Click -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("DGV3")]
	internal virtual DataGridView DGV3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DataGridViewCellEventHandler value2 = DGV3_CellValueChanged;
			DataGridViewRowEventHandler value3 = DGV3_UserAddedRow;
			DataGridViewRowEventHandler value4 = DGV3_UserDeletedRow;
			DataGridView dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.CellValueChanged -= value2;
				dataGridView.UserAddedRow -= value3;
				dataGridView.UserDeletedRow -= value4;
			}
			field = value;
			dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.CellValueChanged += value2;
				dataGridView.UserAddedRow += value3;
				dataGridView.UserDeletedRow += value4;
			}
		}
	}

	[field: AccessedThroughProperty("cmdPasteAlias")]
	internal virtual Button cmdPasteAlias
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdPasteAlias_Click;
			Button button = field;
			if (button != null)
			{
				button.Click -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdCopyAlias")]
	internal virtual Button cmdCopyAlias
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdCopyAlias_Click;
			Button button = field;
			if (button != null)
			{
				button.Click -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("OFD1")]
	internal virtual OpenFileDialog OFD1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column4")]
	internal virtual DataGridViewTextBoxColumn Column4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column5")]
	internal virtual DataGridViewTextBoxColumn Column5
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public UC_Report()
	{
		Load += UC_Report_Load;
		FlagOK = false;
		InitializeComponent();
	}

	[DebuggerNonUserCode]
	protected override void Dispose(bool disposing)
	{
		try
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
		}
		finally
		{
			base.Dispose(disposing);
		}
	}

	[System.Diagnostics.DebuggerStepThrough]
	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.UC_Report));
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.GroupBox2 = new System.Windows.Forms.GroupBox();
		this.cmdSelectFileTemplate = new System.Windows.Forms.Button();
		this.txtTemplateDOC = new System.Windows.Forms.TextBox();
		this.GroupBox3 = new System.Windows.Forms.GroupBox();
		this.cmdRestoreAliases = new System.Windows.Forms.Button();
		this.DGV3 = new System.Windows.Forms.DataGridView();
		this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cmdPasteAlias = new System.Windows.Forms.Button();
		this.cmdCopyAlias = new System.Windows.Forms.Button();
		this.cmdRef1 = new System.Windows.Forms.Button();
		this.cmdWord1 = new System.Windows.Forms.Button();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.OFD1 = new System.Windows.Forms.OpenFileDialog();
		this.GroupBox1.SuspendLayout();
		this.GroupBox2.SuspendLayout();
		this.GroupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGV3).BeginInit();
		base.SuspendLayout();
		this.GroupBox1.Controls.Add(this.GroupBox2);
		this.GroupBox1.Controls.Add(this.GroupBox3);
		this.GroupBox1.Controls.Add(this.cmdRef1);
		this.GroupBox1.Controls.Add(this.cmdWord1);
		this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox1.Location = new System.Drawing.Point(0, 0);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(1000, 778);
		this.GroupBox1.TabIndex = 2;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "General Report by template";
		this.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox2.Controls.Add(this.cmdSelectFileTemplate);
		this.GroupBox2.Controls.Add(this.txtTemplateDOC);
		this.GroupBox2.Location = new System.Drawing.Point(8, 57);
		this.GroupBox2.Name = "GroupBox2";
		this.GroupBox2.Size = new System.Drawing.Size(986, 55);
		this.GroupBox2.TabIndex = 196;
		this.GroupBox2.TabStop = false;
		this.GroupBox2.Text = "Path to the report template file. If the template file is located in the folder with the project file, then you can just specify the file name with the extension";
		this.cmdSelectFileTemplate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdSelectFileTemplate.Image = (System.Drawing.Image)resources.GetObject("cmdSelectFileTemplate.Image");
		this.cmdSelectFileTemplate.Location = new System.Drawing.Point(948, 23);
		this.cmdSelectFileTemplate.Name = "cmdSelectFileTemplate";
		this.cmdSelectFileTemplate.Size = new System.Drawing.Size(25, 20);
		this.cmdSelectFileTemplate.TabIndex = 3;
		this.ToolTip1.SetToolTip(this.cmdSelectFileTemplate, "Select report template file");
		this.cmdSelectFileTemplate.UseVisualStyleBackColor = true;
		this.txtTemplateDOC.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtTemplateDOC.Location = new System.Drawing.Point(6, 23);
		this.txtTemplateDOC.Name = "txtTemplateDOC";
		this.txtTemplateDOC.Size = new System.Drawing.Size(928, 20);
		this.txtTemplateDOC.TabIndex = 0;
		this.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox3.Controls.Add(this.cmdRestoreAliases);
		this.GroupBox3.Controls.Add(this.DGV3);
		this.GroupBox3.Controls.Add(this.cmdPasteAlias);
		this.GroupBox3.Controls.Add(this.cmdCopyAlias);
		this.GroupBox3.Location = new System.Drawing.Point(8, 118);
		this.GroupBox3.Name = "GroupBox3";
		this.GroupBox3.Size = new System.Drawing.Size(986, 654);
		this.GroupBox3.TabIndex = 195;
		this.GroupBox3.TabStop = false;
		this.GroupBox3.Text = "Custom aliases";
		this.cmdRestoreAliases.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.cmdRestoreAliases.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdRestoreAliases.Image = (System.Drawing.Image)resources.GetObject("cmdRestoreAliases.Image");
		this.cmdRestoreAliases.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdRestoreAliases.Location = new System.Drawing.Point(66, 19);
		this.cmdRestoreAliases.Name = "cmdRestoreAliases";
		this.cmdRestoreAliases.Size = new System.Drawing.Size(24, 24);
		this.cmdRestoreAliases.TabIndex = 55;
		this.cmdRestoreAliases.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdRestoreAliases, "Restore the original set of custom aliases");
		this.cmdRestoreAliases.UseVisualStyleBackColor = false;
		this.DGV3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.DGV3.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
		this.DGV3.BackgroundColor = System.Drawing.SystemColors.Menu;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGV3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.DGV3.ColumnHeadersHeight = 22;
		this.DGV3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.DGV3.Columns.AddRange(this.Column4, this.Column5);
		this.DGV3.Location = new System.Drawing.Point(6, 49);
		this.DGV3.Name = "DGV3";
		this.DGV3.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.DGV3.Size = new System.Drawing.Size(974, 599);
		this.DGV3.TabIndex = 0;
		this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.Column4.HeaderText = "Alias name";
		this.Column4.Name = "Column4";
		this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column4.Width = 230;
		this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.Column5.DefaultCellStyle = dataGridViewCellStyle2;
		this.Column5.HeaderText = "Alias text";
		this.Column5.Name = "Column5";
		this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cmdPasteAlias.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.cmdPasteAlias.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdPasteAlias.Image = (System.Drawing.Image)resources.GetObject("cmdPasteAlias.Image");
		this.cmdPasteAlias.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdPasteAlias.Location = new System.Drawing.Point(41, 19);
		this.cmdPasteAlias.Name = "cmdPasteAlias";
		this.cmdPasteAlias.Size = new System.Drawing.Size(24, 24);
		this.cmdPasteAlias.TabIndex = 54;
		this.cmdPasteAlias.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdPasteAlias, "Paste the set of aliases from the clipboard");
		this.cmdPasteAlias.UseVisualStyleBackColor = false;
		this.cmdCopyAlias.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.cmdCopyAlias.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdCopyAlias.Image = (System.Drawing.Image)resources.GetObject("cmdCopyAlias.Image");
		this.cmdCopyAlias.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdCopyAlias.Location = new System.Drawing.Point(16, 19);
		this.cmdCopyAlias.Name = "cmdCopyAlias";
		this.cmdCopyAlias.Size = new System.Drawing.Size(24, 24);
		this.cmdCopyAlias.TabIndex = 53;
		this.cmdCopyAlias.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdCopyAlias, "Copy the alias set to clipboard");
		this.cmdCopyAlias.UseVisualStyleBackColor = false;
		this.cmdRef1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdRef1.Image = (System.Drawing.Image)resources.GetObject("cmdRef1.Image");
		this.cmdRef1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdRef1.Location = new System.Drawing.Point(49, 23);
		this.cmdRef1.Name = "cmdRef1";
		this.cmdRef1.Size = new System.Drawing.Size(26, 26);
		this.cmdRef1.TabIndex = 194;
		this.cmdRef1.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdRef1, "Show the list of standard aliases for a general report");
		this.cmdRef1.UseVisualStyleBackColor = true;
		this.cmdWord1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdWord1.Image = (System.Drawing.Image)resources.GetObject("cmdWord1.Image");
		this.cmdWord1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdWord1.Location = new System.Drawing.Point(22, 23);
		this.cmdWord1.Name = "cmdWord1";
		this.cmdWord1.Size = new System.Drawing.Size(26, 26);
		this.cmdWord1.TabIndex = 192;
		this.cmdWord1.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdWord1, "Create a general report from a Microsoft Word template");
		this.cmdWord1.UseVisualStyleBackColor = true;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.Controls.Add(this.GroupBox1);
		this.MinimumSize = new System.Drawing.Size(700, 0);
		base.Name = "UC_Report";
		base.Size = new System.Drawing.Size(1000, 778);
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox2.ResumeLayout(false);
		this.GroupBox2.PerformLayout();
		this.GroupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.DGV3).EndInit();
		base.ResumeLayout(false);
	}

	private void UC_Report_Load(object sender, EventArgs e)
	{
		FlagOK = false;
		FillDGV3();
		txtTemplateDOC.Text = Public2.PRJ.SETT.RepFileName;
		FlagOK = true;
	}

	public void FillDGV3()
	{
		DGV3.Rows.Clear();
		if (!Information.IsNothing(Public2.PRJ.SETT.ALS))
		{
			int upperBound = Public2.PRJ.SETT.ALS.GetUpperBound(1);
			for (int i = 0; i <= upperBound; i = checked(i + 1))
			{
				DGV3.Rows.Add(Public2.PRJ.SETT.ALS[0, i], Public2.PRJ.SETT.ALS[1, i]);
			}
		}
	}

	public void StoreDGV3()
	{
		if (!FlagOK)
		{
			return;
		}
		if (DGV3.RowCount < 2)
		{
			Public2.PRJ.SETT.ALS = null;
			return;
		}
		checked
		{
			Public2.PRJ.SETT.ALS = new string[2, DGV3.RowCount - 2 + 1];
			int num = DGV3.RowCount - 2;
			for (int i = 0; i <= num; i++)
			{
				Public2.PRJ.SETT.ALS[0, i] = Conversions.ToString(DGV3[0, i].Value);
				Public2.PRJ.SETT.ALS[1, i] = Conversions.ToString(DGV3[1, i].Value);
			}
		}
	}

	private void DGV3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		StoreDGV3();
	}

	private void DGV3_UserAddedRow(object sender, DataGridViewRowEventArgs e)
	{
		StoreDGV3();
	}

	private void DGV3_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
	{
		StoreDGV3();
	}

	private void cmdRestoreAliases_Click(object sender, EventArgs e)
	{
		Public2.NewAliases();
		FillDGV3();
	}

	private void cmdSelectFileTemplate_Click(object sender, EventArgs e)
	{
		OFD1.Filter = "MS Word Template Files (*.dot,*.dotx)|*.dot;*.dotx";
		if (Operators.CompareString(Public2.AppSetup.FolderProject, "", TextCompare: false) != 0)
		{
			OFD1.InitialDirectory = Public2.AppSetup.FolderProject;
		}
		if (OFD1.ShowDialog() == DialogResult.OK)
		{
			Public2.AppSetup.FolderProject = Microsoft.VisualBasic.FileIO.FileSystem.GetFileInfo(OFD1.FileName).DirectoryName;
			txtTemplateDOC.Text = OFD1.FileName;
			Public2.PRJ.SETT.RepFileName = OFD1.FileName;
		}
	}

	private void txtTemplateDOC_TextChanged(object sender, EventArgs e)
	{
		Public2.PRJ.SETT.RepFileName = txtTemplateDOC.Text;
	}

	private void cmdCopyAlias_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		checked
		{
			SerAlias serAlias = default;
			if (!Information.IsNothing(Public2.PRJ.SETT.ALS))
			{
				serAlias.ALS = new string[2, Public2.PRJ.SETT.ALS.GetUpperBound(1) + 1];
				int upperBound = Public2.PRJ.SETT.ALS.GetUpperBound(1);
				for (int i = 0; i <= upperBound; i++)
				{
					serAlias.ALS[0, i] = Public2.PRJ.SETT.ALS[0, i];
					serAlias.ALS[1, i] = Public2.PRJ.SETT.ALS[1, i];
				}
			}
			DataFormats.Format format = DataFormats.GetFormat(typeof(SerAlias).FullName);
			DataObject dataObject = new DataObject();
			((IDataObject)dataObject).SetData(format.Name, false, (object)serAlias);
			Clipboard.SetDataObject(dataObject, copy: true);
		}
	}

	private void cmdPasteAlias_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		IDataObject dataObject = Clipboard.GetDataObject();
		string fullName = typeof(SerAlias).FullName;
		SerAlias serAlias = default;
		if (dataObject.GetDataPresent(fullName))
		{
			object data = dataObject.GetData(fullName);
			serAlias = ((data != null) ? ((SerAlias)data) : default(SerAlias));
		}
		checked
		{
			if (!Information.IsNothing(serAlias.ALS))
			{
				Public2.PRJ.SETT.ALS = new string[2, serAlias.ALS.GetUpperBound(1) + 1];
				int upperBound = serAlias.ALS.GetUpperBound(1);
				for (int i = 0; i <= upperBound; i++)
				{
					Public2.PRJ.SETT.ALS[0, i] = serAlias.ALS[0, i];
					Public2.PRJ.SETT.ALS[1, i] = serAlias.ALS[1, i];
				}
				FillDGV3();
			}
		}
	}

	private void cmdWord1_Click(object sender, EventArgs e)
	{
		RepWordAlias.ReportAlias();
	}

	private void cmdRef1_Click(object sender, EventArgs e)
	{
		try
		{
			Process.Start(Public2.AppStartupPath + "\\Samples\\Aliases for a custom report.dot");
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Interaction.MsgBox(ex2.Message);
			ProjectData.ClearProjectError();
		}
	}
}
