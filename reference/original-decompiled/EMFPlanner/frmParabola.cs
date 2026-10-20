using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class frmParabola : Form
{
	private IContainer components;

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label8")]
	internal virtual Label Label8
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtAntennaDiameter699")]
	internal virtual TextBox txtAntennaDiameter699
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label9")]
	internal virtual Label Label9
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtAntennaGain699")]
	internal virtual TextBox txtAntennaGain699
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label10")]
	internal virtual Label Label10
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtFrequency699")]
	internal virtual TextBox txtFrequency699
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdCancel")]
	internal virtual Button cmdCancel
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdMakePattern")]
	internal virtual Button cmdMakePattern
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdMakePattern_Click;
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

	public frmParabola()
	{
		Load += frmParabola_Load;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.frmParabola));
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.Label8 = new System.Windows.Forms.Label();
		this.txtAntennaDiameter699 = new System.Windows.Forms.TextBox();
		this.Label9 = new System.Windows.Forms.Label();
		this.txtAntennaGain699 = new System.Windows.Forms.TextBox();
		this.Label10 = new System.Windows.Forms.Label();
		this.txtFrequency699 = new System.Windows.Forms.TextBox();
		this.cmdCancel = new System.Windows.Forms.Button();
		this.cmdMakePattern = new System.Windows.Forms.Button();
		this.GroupBox1.SuspendLayout();
		base.SuspendLayout();
		this.GroupBox1.Controls.Add(this.Label8);
		this.GroupBox1.Controls.Add(this.txtAntennaDiameter699);
		this.GroupBox1.Controls.Add(this.Label9);
		this.GroupBox1.Controls.Add(this.txtAntennaGain699);
		this.GroupBox1.Controls.Add(this.Label10);
		this.GroupBox1.Controls.Add(this.txtFrequency699);
		this.GroupBox1.Location = new System.Drawing.Point(12, 47);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(299, 126);
		this.GroupBox1.TabIndex = 39;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "Create pattern for rotationally symmetrical antenna using Rec. ITU-R F.699-8";
		this.Label8.AutoSize = true;
		this.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Label8.Location = new System.Drawing.Point(17, 92);
		this.Label8.Name = "Label8";
		this.Label8.Size = new System.Drawing.Size(107, 13);
		this.Label8.TabIndex = 5;
		this.Label8.Text = "Antenna diameter (m)";
		this.txtAntennaDiameter699.Location = new System.Drawing.Point(138, 89);
		this.txtAntennaDiameter699.Name = "txtAntennaDiameter699";
		this.txtAntennaDiameter699.Size = new System.Drawing.Size(43, 20);
		this.txtAntennaDiameter699.TabIndex = 4;
		this.txtAntennaDiameter699.Text = "0.6";
		this.Label9.AutoSize = true;
		this.Label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Label9.Location = new System.Drawing.Point(29, 66);
		this.Label9.Name = "Label9";
		this.Label9.Size = new System.Drawing.Size(94, 13);
		this.Label9.TabIndex = 3;
		this.Label9.Text = "Antenna gain (dBi)";
		this.txtAntennaGain699.Location = new System.Drawing.Point(138, 63);
		this.txtAntennaGain699.Name = "txtAntennaGain699";
		this.txtAntennaGain699.Size = new System.Drawing.Size(43, 20);
		this.txtAntennaGain699.TabIndex = 2;
		this.txtAntennaGain699.Text = "31";
		this.Label10.AutoSize = true;
		this.Label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Label10.Location = new System.Drawing.Point(35, 40);
		this.Label10.Name = "Label10";
		this.Label10.Size = new System.Drawing.Size(88, 13);
		this.Label10.TabIndex = 1;
		this.Label10.Text = "Frequency (MHz)";
		this.txtFrequency699.Location = new System.Drawing.Point(138, 37);
		this.txtFrequency699.Name = "txtFrequency699";
		this.txtFrequency699.Size = new System.Drawing.Size(43, 20);
		this.txtFrequency699.TabIndex = 0;
		this.txtFrequency699.Text = "7000";
		this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		this.cmdCancel.Image = (System.Drawing.Image)resources.GetObject("cmdCancel.Image");
		this.cmdCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdCancel.Location = new System.Drawing.Point(39, 12);
		this.cmdCancel.Name = "cmdCancel";
		this.cmdCancel.Size = new System.Drawing.Size(26, 26);
		this.cmdCancel.TabIndex = 203;
		this.cmdCancel.UseVisualStyleBackColor = true;
		this.cmdMakePattern.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdMakePattern.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		this.cmdMakePattern.Image = (System.Drawing.Image)resources.GetObject("cmdMakePattern.Image");
		this.cmdMakePattern.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdMakePattern.Location = new System.Drawing.Point(12, 12);
		this.cmdMakePattern.Name = "cmdMakePattern";
		this.cmdMakePattern.Size = new System.Drawing.Size(26, 26);
		this.cmdMakePattern.TabIndex = 200;
		this.cmdMakePattern.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdCancel;
		base.ClientSize = new System.Drawing.Size(326, 182);
		base.Controls.Add(this.cmdCancel);
		base.Controls.Add(this.cmdMakePattern);
		base.Controls.Add(this.GroupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmParabola";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Create antenna pattern";
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox1.PerformLayout();
		base.ResumeLayout(false);
	}

	private void frmParabola_Load(object sender, EventArgs e)
	{
		GroupBox1.Select();
		Form1_Public.ClickOK = false;
		Form1_Public.PatEnv = null;
	}

	private void cmdMakePattern_Click(object sender, EventArgs e)
	{
		GroupBox1.Select();
		if (!Module1.ValidText(txtFrequency699.Text, CheckLimits: true, 1f, 70000f, IncludeMin: true, IncludeMax: true, Message: true))
		{
			return;
		}
		txtFrequency699.Text = Conversions.ToString(Conversions.ToSingle(txtFrequency699.Text));
		float num = Conversions.ToSingle(txtFrequency699.Text);
		if (!Module1.ValidText(txtAntennaGain699.Text, CheckLimits: true, 0f, 60f, IncludeMin: true, IncludeMax: true, Message: true))
		{
			return;
		}
		txtAntennaGain699.Text = Conversions.ToString(Conversions.ToSingle(txtAntennaGain699.Text));
		float num2 = Conversions.ToSingle(txtAntennaGain699.Text);
		checked
		{
			if (Module1.ValidText(txtAntennaDiameter699.Text, CheckLimits: true, 0.1f, 20f, IncludeMin: true, IncludeMax: true, Message: true))
			{
				txtAntennaDiameter699.Text = Conversions.ToString(Conversions.ToSingle(txtAntennaDiameter699.Text));
				float num3 = Conversions.ToSingle(txtAntennaDiameter699.Text);
				Form1_Public.ClickOK = true;
				Form1_Public.PatEnv = new ClassDN.AntennaPatternClass(_VertPatternFornmat: false, _GroupFormat: false);
				Form1_Public.PatEnv.GR[0].HZ.Clear();
				Form1_Public.PatEnv.GR[0].VR.Clear();
				Form1_Public.PatEnv.GainDBi = Conversions.ToString(num2);
				Form1_Public.PatEnv.Name = Conversions.ToString(num / 1000f) + " GHz " + Conversions.ToString(num3) + " m";
				float num4 = 300f / num;
				float num5 = (float)(2.0 + 15.0 * Math.Log10(num3 / num4));
				float fiM = (float)((double)(20f * num4) * Math.Sqrt(num2 - num5) / (double)num3);
				float fiR = (float)(15.85 * Math.Pow(num3 / num4, -0.6));
				int num6 = 0;
				do
				{
					float num7 = ((!((num6 >= 0) & (num6 <= 180))) ? ((float)Math.Round(Math.Abs(Gq((short)(360 - num6), num4, num3, num2, fiM, fiR, num5)), 1)) : ((float)Math.Round(Math.Abs(Gq((short)num6, num4, num3, num2, fiM, fiR, num5)), 1)));
					Form1_Public.PatEnv.GR[0].HZ.Add(new ClassDN.PatternPoint(num6, 0f - num7));
					Form1_Public.PatEnv.GR[0].VR.Add(new ClassDN.PatternPoint(num6, 0f - num7));
					num6++;
				}
				while (num6 <= 359);
				Close();
			}
		}
	}

	public float Gq(short Fi, float Lambda, float Ddn, float Gdn, float FiM, float FiR, float G1)
	{
		if (Ddn / Lambda > 100f)
		{
			if ((Fi >= 0) & ((float)Fi < FiM))
			{
				return (float)(-0.0025 * Math.Pow(Ddn * (float)Fi / Lambda, 2.0));
			}
			if (((float)Fi >= FiM) & ((float)Fi < FiR))
			{
				return G1 - Gdn;
			}
			if (((float)Fi >= FiR) & (Fi < 48))
			{
				return (float)(32.0 - 25.0 * Math.Log10(Fi) - (double)Gdn);
			}
			return -10f - Gdn;
		}
		if ((Fi >= 0) & ((float)Fi < FiM))
		{
			return (float)(-0.0025 * Math.Pow(Ddn * (float)Fi / Lambda, 2.0));
		}
		if (((float)Fi >= FiM) & ((float)Fi < 100f * Lambda / Ddn))
		{
			return G1 - Gdn;
		}
		if (((float)Fi >= 100f * Lambda / Ddn) & (Fi < 48))
		{
			return (float)(52.0 - 10.0 * Math.Log10(Ddn / Lambda) - 25.0 * Math.Log10(Fi) - (double)Gdn);
		}
		return (float)(10.0 - 10.0 * Math.Log10(Ddn / Lambda) - (double)Gdn);
	}
}
