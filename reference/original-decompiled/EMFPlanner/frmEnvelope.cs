using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class frmEnvelope : Form
{
	private IContainer components;

	private bool FlagProcessing;

	private List<ClassDN.AntennaPatternEnvelop> PatEnvList;

	[field: AccessedThroughProperty("cmdFolder")]
	internal virtual Button cmdFolder
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdFolder_Click;
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

	[field: AccessedThroughProperty("chkUseGain")]
	internal virtual CheckBox chkUseGain
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = chkUseGain_CheckedChanged;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("gbH")]
	internal virtual GroupBox gbH
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("DGV1")]
	internal virtual DataGridView DGV1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DataGridViewCellValidatingEventHandler value2 = DGV1_CellValidating;
			DataGridViewCellMouseEventHandler value3 = DGV1_CellMouseDown;
			DataGridViewCellEventHandler value4 = DGV1_CellValueChanged;
			KeyEventHandler value5 = DGV1_KeyUp;
			DataGridView dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.CellValidating -= value2;
				dataGridView.CellMouseDown -= value3;
				dataGridView.CellValueChanged -= value4;
				dataGridView.KeyUp -= value5;
			}
			field = value;
			dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.CellValidating += value2;
				dataGridView.CellMouseDown += value3;
				dataGridView.CellValueChanged += value4;
				dataGridView.KeyUp += value5;
			}
		}
	}

	[field: AccessedThroughProperty("Column1")]
	internal virtual DataGridViewCheckBoxColumn Column1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column2")]
	internal virtual DataGridViewTextBoxColumn Column2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column3")]
	internal virtual DataGridViewTextBoxColumn Column3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolTip1")]
	internal virtual ToolTip ToolTip1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("FBD1")]
	internal virtual FolderBrowserDialog FBD1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdMakeEnvelope")]
	internal virtual Button cmdMakeEnvelope
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdMakeEnvelope_Click;
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

	[field: AccessedThroughProperty("cmdSelectDeselectAll")]
	internal virtual Button cmdSelectDeselectAll
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdSelectDeselectAll_Click;
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

	[field: AccessedThroughProperty("cmdCancel")]
	internal virtual Button cmdCancel
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public frmEnvelope()
	{
		Load += UC_Envelop_Load;
		PatEnvList = new List<ClassDN.AntennaPatternEnvelop>();
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.frmEnvelope));
		this.chkUseGain = new System.Windows.Forms.CheckBox();
		this.gbH = new System.Windows.Forms.GroupBox();
		this.DGV1 = new System.Windows.Forms.DataGridView();
		this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.cmdCancel = new System.Windows.Forms.Button();
		this.cmdSelectDeselectAll = new System.Windows.Forms.Button();
		this.cmdFolder = new System.Windows.Forms.Button();
		this.cmdMakeEnvelope = new System.Windows.Forms.Button();
		this.FBD1 = new System.Windows.Forms.FolderBrowserDialog();
		this.gbH.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGV1).BeginInit();
		base.SuspendLayout();
		this.chkUseGain.AutoSize = true;
		this.chkUseGain.Location = new System.Drawing.Point(178, 18);
		this.chkUseGain.Name = "chkUseGain";
		this.chkUseGain.Size = new System.Drawing.Size(110, 17);
		this.chkUseGain.TabIndex = 194;
		this.chkUseGain.Text = "Use antenna gain";
		this.chkUseGain.UseVisualStyleBackColor = true;
		this.gbH.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gbH.Controls.Add(this.DGV1);
		this.gbH.Location = new System.Drawing.Point(7, 45);
		this.gbH.Name = "gbH";
		this.gbH.Size = new System.Drawing.Size(399, 510);
		this.gbH.TabIndex = 191;
		this.gbH.TabStop = false;
		this.gbH.Text = "Selected antenna patterns";
		this.DGV1.AllowUserToAddRows = false;
		this.DGV1.AllowUserToDeleteRows = false;
		this.DGV1.AllowUserToResizeColumns = false;
		this.DGV1.AllowUserToResizeRows = false;
		this.DGV1.BackgroundColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGV1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.DGV1.ColumnHeadersHeight = 25;
		this.DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.DGV1.Columns.AddRange(this.Column1, this.Column2, this.Column3);
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.DGV1.DefaultCellStyle = dataGridViewCellStyle2;
		this.DGV1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.DGV1.Location = new System.Drawing.Point(3, 16);
		this.DGV1.Name = "DGV1";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGV1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.DGV1.RowTemplate.Height = 18;
		this.DGV1.Size = new System.Drawing.Size(393, 491);
		this.DGV1.TabIndex = 1;
		this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.Column1.HeaderText = "";
		this.Column1.Name = "Column1";
		this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.Column1.Width = 30;
		this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.Column2.DefaultCellStyle = dataGridViewCellStyle4;
		this.Column2.HeaderText = "Name";
		this.Column2.Name = "Column2";
		this.Column2.ReadOnly = true;
		this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column3.DefaultCellStyle = dataGridViewCellStyle5;
		this.Column3.HeaderText = "Gain, dB";
		this.Column3.Name = "Column3";
		this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column3.Width = 60;
		this.ToolTip1.AutoPopDelay = 5000;
		this.ToolTip1.InitialDelay = 500;
		this.ToolTip1.ReshowDelay = 100;
		this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		this.cmdCancel.Image = (System.Drawing.Image)resources.GetObject("cmdCancel.Image");
		this.cmdCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdCancel.Location = new System.Drawing.Point(88, 12);
		this.cmdCancel.Name = "cmdCancel";
		this.cmdCancel.Size = new System.Drawing.Size(26, 26);
		this.cmdCancel.TabIndex = 199;
		this.ToolTip1.SetToolTip(this.cmdCancel, "Close");
		this.cmdCancel.UseVisualStyleBackColor = true;
		this.cmdSelectDeselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdSelectDeselectAll.Image = (System.Drawing.Image)resources.GetObject("cmdSelectDeselectAll.Image");
		this.cmdSelectDeselectAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdSelectDeselectAll.Location = new System.Drawing.Point(34, 12);
		this.cmdSelectDeselectAll.Name = "cmdSelectDeselectAll";
		this.cmdSelectDeselectAll.Size = new System.Drawing.Size(26, 26);
		this.cmdSelectDeselectAll.TabIndex = 197;
		this.cmdSelectDeselectAll.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdSelectDeselectAll, "Select/deselect all antenna patterns");
		this.cmdSelectDeselectAll.UseVisualStyleBackColor = true;
		this.cmdFolder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
		this.cmdFolder.Image = (System.Drawing.Image)resources.GetObject("cmdFolder.Image");
		this.cmdFolder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdFolder.Location = new System.Drawing.Point(7, 12);
		this.cmdFolder.Name = "cmdFolder";
		this.cmdFolder.Size = new System.Drawing.Size(26, 26);
		this.cmdFolder.TabIndex = 195;
		this.ToolTip1.SetToolTip(this.cmdFolder, "Load antenna pattern files");
		this.cmdFolder.UseVisualStyleBackColor = true;
		this.cmdMakeEnvelope.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdMakeEnvelope.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		this.cmdMakeEnvelope.Image = (System.Drawing.Image)resources.GetObject("cmdMakeEnvelope.Image");
		this.cmdMakeEnvelope.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdMakeEnvelope.Location = new System.Drawing.Point(61, 12);
		this.cmdMakeEnvelope.Name = "cmdMakeEnvelope";
		this.cmdMakeEnvelope.Size = new System.Drawing.Size(26, 26);
		this.cmdMakeEnvelope.TabIndex = 192;
		this.ToolTip1.SetToolTip(this.cmdMakeEnvelope, "Create envelope of selected antenna patterns");
		this.cmdMakeEnvelope.UseVisualStyleBackColor = true;
		this.FBD1.ShowNewFolderButton = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdCancel;
		base.ClientSize = new System.Drawing.Size(413, 561);
		base.Controls.Add(this.cmdCancel);
		base.Controls.Add(this.cmdSelectDeselectAll);
		base.Controls.Add(this.cmdFolder);
		base.Controls.Add(this.chkUseGain);
		base.Controls.Add(this.cmdMakeEnvelope);
		base.Controls.Add(this.gbH);
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmEnvelope";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Envelope";
		this.gbH.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.DGV1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void UC_Envelop_Load(object sender, EventArgs e)
	{
		gbH.Select();
		Form1_Public.ClickOK = false;
		Form1_Public.PatEnv = null;
		FillDGV();
		FlagProcessing = true;
		chkUseGain.Checked = Public2.AppSetup.UseGainForEnvelop;
		if (Public2.AppSetup.UseGainForEnvelop)
		{
			DGV1.Columns[2].Visible = true;
		}
		else
		{
			DGV1.Columns[2].Visible = false;
		}
		FlagProcessing = false;
	}

	public void FillDGV()
	{
		FlagProcessing = true;
		DGV1.Rows.Clear();
		foreach (ClassDN.AntennaPatternEnvelop patEnv in PatEnvList)
		{
			DGV1.Rows.Add(patEnv.Active, patEnv.Name, patEnv.GainDBi);
		}
		DGV1.ClearSelection();
		gbH.Select();
		FlagProcessing = false;
	}

	private void DGV1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
	{
		if (!FlagProcessing && e.ColumnIndex == 2)
		{
			if (Versioned.IsNumeric(RuntimeHelpers.GetObjectValue(e.FormattedValue)))
			{
				PatEnvList[e.RowIndex].GainDBi = Module1.QSNG(Conversions.ToString(e.FormattedValue));
				DGV1.Rows[e.RowIndex].Cells[2].Value = PatEnvList[e.RowIndex].GainDBi;
			}
			else
			{
				e.Cancel = true;
			}
		}
	}

	private void DGV1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
	{
		checked
		{
			if (e.ColumnIndex == 0 && DGV1.SelectedCells.Count > 1)
			{
				bool flag = Conversions.ToBoolean(Operators.NotObject(DGV1.Rows[e.RowIndex].Cells[0].Value));
				int num = DGV1.SelectedCells.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					PatEnvList[DGV1.SelectedCells[i].RowIndex].Active = flag;
					DGV1.Rows[DGV1.SelectedCells[i].RowIndex].Cells[0].Value = flag;
				}
				DGV1.Rows[e.RowIndex].Cells[0].Value = Operators.NotObject(DGV1.Rows[e.RowIndex].Cells[0].Value);
				PatEnvList[e.RowIndex].Active = Conversions.ToBoolean(Operators.NotObject(DGV1.Rows[e.RowIndex].Cells[0].Value));
				DGV1.ClearSelection();
			}
		}
	}

	private void DGV1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if (!FlagProcessing && e.ColumnIndex == 0 && DGV1.SelectedCells.Count == 1)
		{
			PatEnvList[e.RowIndex].Active = Conversions.ToBoolean(DGV1.Rows[e.RowIndex].Cells[0].Value);
			DGV1.ClearSelection();
		}
	}

	private void DGV1_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Delete || DGV1.SelectedRows.Count == 0)
		{
			return;
		}
		List<short> list = new List<short>();
		checked
		{
			int num = DGV1.SelectedRows.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				if (DGV1.SelectedRows[i].Index < PatEnvList.Count)
				{
					list.Add((short)DGV1.SelectedRows[i].Index);
				}
			}
			list.Sort();
			for (int j = list.Count - 1; j >= 0; j += -1)
			{
				PatEnvList.RemoveAt(list[j]);
			}
			FillDGV();
		}
	}

	private void chkUseGain_CheckedChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			Public2.AppSetup.UseGainForEnvelop = chkUseGain.Checked;
			if (Public2.AppSetup.UseGainForEnvelop)
			{
				DGV1.Columns[2].Visible = true;
			}
			else
			{
				DGV1.Columns[2].Visible = false;
			}
		}
	}

	private void cmdFolder_Click(object sender, EventArgs e)
	{
		gbH.Select();
		FBD1.Description = "Select antenna pattern folder";
		if (FBD1.ShowDialog() == DialogResult.Cancel)
		{
			return;
		}
		PatEnvList.Clear();
		string[] files = Directory.GetFiles(FBD1.SelectedPath, "*.*", SearchOption.TopDirectoryOnly);
		foreach (string text in files)
		{
			ClassDN.AntennaPatternClass antennaPatternClass = ClassDN.LoadAntennaFile(text);
			if ((!antennaPatternClass.VertPatternFormat & !antennaPatternClass.GroupFormat) && !((antennaPatternClass.GR[0].HZ.Count < 3) | (antennaPatternClass.GR[0].VR.Count < 3)))
			{
				PatEnvList.Add(new ClassDN.AntennaPatternEnvelop(Path.GetFileNameWithoutExtension(text), Module1.RSNG(antennaPatternClass.GainDBi), antennaPatternClass.GR[0].HZ, antennaPatternClass.GR[0].VR));
			}
		}
		FillDGV();
	}

	private void cmdSelectDeselectAll_Click(object sender, EventArgs e)
	{
		gbH.Select();
		if (PatEnvList.Count == 0)
		{
			return;
		}
		bool active = !PatEnvList.First().Active;
		foreach (ClassDN.AntennaPatternEnvelop patEnv in PatEnvList)
		{
			patEnv.Active = active;
		}
		FillDGV();
	}

	private void cmdMakeEnvelope_Click(object sender, EventArgs e)
	{
		gbH.Select();
		bool flag = true;
		if (PatEnvList.Count > 0)
		{
			foreach (ClassDN.AntennaPatternEnvelop patEnv in PatEnvList)
			{
				if (patEnv.Active)
				{
					flag = false;
					break;
				}
			}
		}
		if (flag)
		{
			Interaction.MsgBox(Localization.LocMSG(110));
			return;
		}
		Form1_Public.ClickOK = true;
		string text = "";
		Form1_Public.PatEnv = new ClassDN.AntennaPatternClass(_VertPatternFornmat: false, _GroupFormat: false);
		Form1_Public.PatEnv.GR[0].HZ.Clear();
		Form1_Public.PatEnv.GR[0].VR.Clear();
		int num = 0;
		checked
		{
			do
			{
				Form1_Public.PatEnv.GR[0].HZ.Add(new ClassDN.PatternPoint(num, -60f));
				Form1_Public.PatEnv.GR[0].VR.Add(new ClassDN.PatternPoint(num, -60f));
				num++;
			}
			while (num <= 359);
			foreach (ClassDN.AntennaPatternEnvelop patEnv2 in PatEnvList)
			{
				if (!patEnv2.Active)
				{
					continue;
				}
				if (Operators.CompareString(text, "", TextCompare: false) == 0)
				{
					text = patEnv2.Name;
				}
				List<ClassDN.PatternPoint> DN = Module1.DeepClone(patEnv2.HZ);
				List<ClassDN.PatternPoint> DN2 = Module1.DeepClone(patEnv2.VR);
				ClassDN.Interpol1deg360points(ref DN);
				ClassDN.Interpol1deg360points(ref DN2);
				ClassDN.PatternNormalize(ref DN);
				ClassDN.PatternNormalize(ref DN2);
				int num2 = 0;
				do
				{
					if (Public2.AppSetup.UseGainForEnvelop)
					{
						DN[num2].G = DN[num2].G + patEnv2.GainDBi;
						DN2[num2].G = DN2[num2].G + patEnv2.GainDBi;
					}
					if (DN[num2].G > Form1_Public.PatEnv.GR[0].HZ[num2].G)
					{
						Form1_Public.PatEnv.GR[0].HZ[num2].G = DN[num2].G;
					}
					if (DN2[num2].G > Form1_Public.PatEnv.GR[0].VR[num2].G)
					{
						Form1_Public.PatEnv.GR[0].VR[num2].G = DN2[num2].G;
					}
					num2++;
				}
				while (num2 <= 359);
			}
			ClassDN.PatternNormalize(ref Form1_Public.PatEnv.GR[0].HZ);
			ClassDN.PatternNormalize(ref Form1_Public.PatEnv.GR[0].VR);
			if (text.Length > 16)
			{
				Form1_Public.PatEnv.Name = "Envelope " + text.Substring(0, 16);
			}
			else
			{
				Form1_Public.PatEnv.Name = "Envelope " + text;
			}
			Close();
		}
	}
}
