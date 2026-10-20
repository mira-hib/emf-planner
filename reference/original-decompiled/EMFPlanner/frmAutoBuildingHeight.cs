using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class frmAutoBuildingHeight : Form
{
	private IContainer components;

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblMeter3")]
	internal virtual Label lblMeter3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblMeter2")]
	internal virtual Label lblMeter2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblMeter1")]
	internal virtual Label lblMeter1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label4")]
	internal virtual Label Label4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtFundationHeight")]
	internal virtual TextBox txtFundationHeight
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtFundationHeight_Validating;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.Validating -= value2;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.Validating += value2;
			}
		}
	}

	[field: AccessedThroughProperty("Label3")]
	internal virtual Label Label3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtHgtFloor")]
	internal virtual TextBox txtHgtFloor
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtHgtFloor_Validating;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.Validating -= value2;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.Validating += value2;
			}
		}
	}

	[field: AccessedThroughProperty("Label2")]
	internal virtual Label Label2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtHgtBuilding")]
	internal virtual TextBox txtHgtBuilding
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtHgtBuilding_Validating;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.Validating -= value2;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.Validating += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdOK")]
	internal virtual Button cmdOK
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdOK_Click;
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

	[field: AccessedThroughProperty("cmdCANCEL")]
	internal virtual Button cmdCANCEL
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public frmAutoBuildingHeight()
	{
		Load += FrmAutoBuildingHeight_Load;
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
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.lblMeter3 = new System.Windows.Forms.Label();
		this.lblMeter2 = new System.Windows.Forms.Label();
		this.lblMeter1 = new System.Windows.Forms.Label();
		this.Label4 = new System.Windows.Forms.Label();
		this.txtFundationHeight = new System.Windows.Forms.TextBox();
		this.Label3 = new System.Windows.Forms.Label();
		this.txtHgtFloor = new System.Windows.Forms.TextBox();
		this.Label2 = new System.Windows.Forms.Label();
		this.txtHgtBuilding = new System.Windows.Forms.TextBox();
		this.cmdOK = new System.Windows.Forms.Button();
		this.cmdCANCEL = new System.Windows.Forms.Button();
		this.GroupBox1.SuspendLayout();
		base.SuspendLayout();
		this.GroupBox1.Controls.Add(this.lblMeter3);
		this.GroupBox1.Controls.Add(this.lblMeter2);
		this.GroupBox1.Controls.Add(this.lblMeter1);
		this.GroupBox1.Controls.Add(this.Label4);
		this.GroupBox1.Controls.Add(this.txtFundationHeight);
		this.GroupBox1.Controls.Add(this.Label3);
		this.GroupBox1.Controls.Add(this.txtHgtFloor);
		this.GroupBox1.Controls.Add(this.Label2);
		this.GroupBox1.Controls.Add(this.txtHgtBuilding);
		this.GroupBox1.Location = new System.Drawing.Point(6, 12);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(447, 152);
		this.GroupBox1.TabIndex = 6;
		this.GroupBox1.TabStop = false;
		this.lblMeter3.AutoSize = true;
		this.lblMeter3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.lblMeter3.Location = new System.Drawing.Point(428, 115);
		this.lblMeter3.Name = "lblMeter3";
		this.lblMeter3.Size = new System.Drawing.Size(15, 13);
		this.lblMeter3.TabIndex = 93;
		this.lblMeter3.Text = "m";
		this.lblMeter2.AutoSize = true;
		this.lblMeter2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.lblMeter2.Location = new System.Drawing.Point(428, 73);
		this.lblMeter2.Name = "lblMeter2";
		this.lblMeter2.Size = new System.Drawing.Size(15, 13);
		this.lblMeter2.TabIndex = 92;
		this.lblMeter2.Text = "m";
		this.lblMeter1.AutoSize = true;
		this.lblMeter1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.lblMeter1.Location = new System.Drawing.Point(428, 27);
		this.lblMeter1.Name = "lblMeter1";
		this.lblMeter1.Size = new System.Drawing.Size(15, 13);
		this.lblMeter1.TabIndex = 91;
		this.lblMeter1.Text = "m";
		this.Label4.AutoSize = true;
		this.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Label4.Location = new System.Drawing.Point(253, 115);
		this.Label4.Name = "Label4";
		this.Label4.Size = new System.Drawing.Size(116, 13);
		this.Label4.TabIndex = 6;
		this.Label4.Text = "Building base elevation";
		this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtFundationHeight.Location = new System.Drawing.Point(377, 112);
		this.txtFundationHeight.Name = "txtFundationHeight";
		this.txtFundationHeight.Size = new System.Drawing.Size(47, 20);
		this.txtFundationHeight.TabIndex = 5;
		this.txtFundationHeight.Text = "0";
		this.txtFundationHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Label3.Location = new System.Drawing.Point(10, 62);
		this.Label3.Name = "Label3";
		this.Label3.Size = new System.Drawing.Size(359, 30);
		this.Label3.TabIndex = 3;
		this.Label3.Text = "Floor height of a building, if for this building the OSM database does not contain the height of the building, but contains the number of floors";
		this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtHgtFloor.Location = new System.Drawing.Point(377, 70);
		this.txtHgtFloor.Name = "txtHgtFloor";
		this.txtHgtFloor.Size = new System.Drawing.Size(47, 20);
		this.txtHgtFloor.TabIndex = 2;
		this.txtHgtFloor.Text = "3";
		this.txtHgtFloor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Label2.Location = new System.Drawing.Point(13, 18);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(356, 30);
		this.Label2.TabIndex = 1;
		this.Label2.Text = "Height of the building, in case the OSM database for this building does not contain the height and number of floors";
		this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtHgtBuilding.Location = new System.Drawing.Point(377, 24);
		this.txtHgtBuilding.Name = "txtHgtBuilding";
		this.txtHgtBuilding.Size = new System.Drawing.Size(47, 20);
		this.txtHgtBuilding.TabIndex = 0;
		this.txtHgtBuilding.Text = "25";
		this.txtHgtBuilding.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdOK.Location = new System.Drawing.Point(347, 182);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.Size = new System.Drawing.Size(106, 23);
		this.cmdOK.TabIndex = 7;
		this.cmdOK.Text = "OK";
		this.cmdOK.UseVisualStyleBackColor = true;
		this.cmdCANCEL.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdCANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCANCEL.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdCANCEL.Location = new System.Drawing.Point(235, 182);
		this.cmdCANCEL.Name = "cmdCANCEL";
		this.cmdCANCEL.Size = new System.Drawing.Size(106, 23);
		this.cmdCANCEL.TabIndex = 8;
		this.cmdCANCEL.Text = "Cancel";
		this.cmdCANCEL.UseVisualStyleBackColor = true;
		base.AcceptButton = this.cmdOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdCANCEL;
		base.ClientSize = new System.Drawing.Size(462, 217);
		base.Controls.Add(this.cmdOK);
		base.Controls.Add(this.cmdCANCEL);
		base.Controls.Add(this.GroupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmAutoBuildingHeight";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Building height selection options";
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox1.PerformLayout();
		base.ResumeLayout(false);
	}

	private void FrmAutoBuildingHeight_Load(object sender, EventArgs e)
	{
		txtHgtBuilding.Text = Conversions.ToString(Form1_Public.HgtBuilding);
		txtHgtFloor.Text = Conversions.ToString(Form1_Public.HgtFloor);
		txtFundationHeight.Text = Conversions.ToString(Form1_Public.FundationHeight);
		Form1_Public.AutoBuildingHeightOK = false;
	}

	private void CmdOK_Click(object sender, EventArgs e)
	{
		Form1_Public.AutoBuildingHeightOK = true;
		Close();
	}

	private void txtHgtBuilding_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtHgtBuilding;
		string TextProperty = textBox.Text;
		double DblVariable = Form1_Public.HgtBuilding;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 0f, 1000f, IncludeMin: true, IncludeMax: true, Message: true);
		Form1_Public.HgtBuilding = (float)DblVariable;
		textBox.Text = TextProperty;
	}

	private void txtHgtFloor_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtHgtFloor;
		string TextProperty = textBox.Text;
		double DblVariable = Form1_Public.HgtFloor;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 0f, 30f, IncludeMin: false, IncludeMax: true, Message: true);
		Form1_Public.HgtFloor = (float)DblVariable;
		textBox.Text = TextProperty;
	}

	private void txtFundationHeight_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtFundationHeight;
		string TextProperty = textBox.Text;
		double DblVariable = Form1_Public.FundationHeight;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: false, 0f, 1000f, IncludeMin: true, IncludeMax: true, Message: true);
		Form1_Public.FundationHeight = (float)DblVariable;
		textBox.Text = TextProperty;
	}
}
