using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using EMFPlanner.My;
using EMFPlanner.My.Resources;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class frmQuickVertPlane : Form
{
	private IContainer components;

	private short MarginUP;

	private short MarginDOWN;

	private short MarginLEFT;

	private short MarginRIGHT;

	private Pen PenBlack2;

	private Pen PenRed1;

	private Font Font8;

	private SolidBrush drawBrushBlack;

	private SizeF stringSize;

	[field: AccessedThroughProperty("cmdClose")]
	internal virtual Button cmdClose
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PB")]
	internal virtual PictureBox PB
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			PaintEventHandler value2 = PB_Paint;
			EventHandler value3 = PB_Resize;
			PictureBox pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Paint -= value2;
				pictureBox.Resize -= value3;
			}
			field = value;
			pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Paint += value2;
				pictureBox.Resize += value3;
			}
		}
	}

	[field: AccessedThroughProperty("cmdToVertPlane")]
	internal virtual Button cmdToVertPlane
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdToVertPlane_Click;
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

	[field: AccessedThroughProperty("cmdDelVertPlane")]
	internal virtual Button cmdDelVertPlane
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdDelVertPlane_Click;
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

	public frmQuickVertPlane()
	{
		Load += FrmQuickVertPlane_Load;
		MarginUP = 25;
		MarginDOWN = 20;
		MarginLEFT = 30;
		MarginRIGHT = 35;
		PenBlack2 = new Pen(Color.Black, 2f);
		PenRed1 = new Pen(Color.Red, 1f);
		Font8 = new Font("Arial", 8f, FontStyle.Regular);
		drawBrushBlack = new SolidBrush(Color.Black);
		stringSize = default;
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
		this.cmdClose = new System.Windows.Forms.Button();
		this.PB = new System.Windows.Forms.PictureBox();
		this.cmdToVertPlane = new System.Windows.Forms.Button();
		this.cmdDelVertPlane = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.PB).BeginInit();
		base.SuspendLayout();
		this.cmdClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdClose.Location = new System.Drawing.Point(419, 440);
		this.cmdClose.Name = "cmdClose";
		this.cmdClose.Size = new System.Drawing.Size(90, 23);
		this.cmdClose.TabIndex = 0;
		this.cmdClose.Text = "Cancel";
		this.cmdClose.UseVisualStyleBackColor = true;
		this.PB.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.PB.BackColor = System.Drawing.SystemColors.Window;
		this.PB.Location = new System.Drawing.Point(0, 1);
		this.PB.Name = "PB";
		this.PB.Size = new System.Drawing.Size(764, 429);
		this.PB.TabIndex = 1;
		this.PB.TabStop = false;
		this.cmdToVertPlane.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdToVertPlane.Location = new System.Drawing.Point(643, 440);
		this.cmdToVertPlane.Name = "cmdToVertPlane";
		this.cmdToVertPlane.Size = new System.Drawing.Size(114, 23);
		this.cmdToVertPlane.TabIndex = 2;
		this.cmdToVertPlane.Text = "Show cut";
		this.cmdToVertPlane.UseVisualStyleBackColor = true;
		this.cmdDelVertPlane.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdDelVertPlane.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdDelVertPlane.Location = new System.Drawing.Point(515, 440);
		this.cmdDelVertPlane.Name = "cmdDelVertPlane";
		this.cmdDelVertPlane.Size = new System.Drawing.Size(122, 23);
		this.cmdDelVertPlane.TabIndex = 3;
		this.cmdDelVertPlane.Text = "Remove cut";
		this.cmdDelVertPlane.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdClose;
		base.ClientSize = new System.Drawing.Size(764, 473);
		base.Controls.Add(this.cmdDelVertPlane);
		base.Controls.Add(this.cmdToVertPlane);
		base.Controls.Add(this.PB);
		base.Controls.Add(this.cmdClose);
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmQuickVertPlane";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Vertical cut along the azimuth";
		((System.ComponentModel.ISupportInitialize)this.PB).EndInit();
		base.ResumeLayout(false);
	}

	private void FrmQuickVertPlane_Load(object sender, EventArgs e)
	{
		if (MyProject.Forms.Form1.IndexVertPlane >= 0)
		{
			Text = Localization.LocMSG(93) + " " + Conversions.ToString(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Azimuth) + "° (" + Localization.LocMSG(94) + " " + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Name.Replace("\r\n", "; ") + ")";
		}
	}

	private void PB_Paint(object sender, PaintEventArgs e)
	{
		e.Graphics.InterpolationMode = InterpolationMode.High;
		if (Information.IsNothing(Public2.PRJ.VP) || MyProject.Forms.Form1.IndexVertPlane > Public2.PRJ.VP.GetUpperBound(0) || Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastL1 == 0f)
		{
			return;
		}
		Pen pen = new Pen(Color.Black, 1f);
		pen.DashStyle = DashStyle.Dash;
		Pen pen2 = new Pen(Color.Red, 1f);
		pen2.DashStyle = DashStyle.Dash;
		double num = Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastL1 + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastL2;
		checked
		{
			short num2 = (short)(PB.Width - MarginLEFT - MarginRIGHT);
			short num3 = (short)(PB.Height - MarginUP - MarginDOWN);
			double num4 = (double)num2 / num;
			double num5 = (float)num3 / (Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastH1 + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastH2);
			if (!Information.IsNothing(Public2.PRJ.TXT))
			{
				int upperBound = Public2.PRJ.TXT.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if (!(Form1_Public.MastDimension(Public2.PRJ.TXT[i].Text) > 0f))
					{
						continue;
					}
					Public2.CoordMeters coordMeters = CalcCoords.ConvWebMercMetersToMetersRound(Public2.PRJ.TXT[i].CoordWM);
					float num6 = (float)(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Coord1M.Xm - coordMeters.Xm);
					float num7 = (float)(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Coord1M.Ym - coordMeters.Ym);
					float num8 = (float)Math.Sqrt(num6 * num6 + num7 * num7);
					float num9 = Form1_Public.MastDimension(Public2.PRJ.TXT[i].Text);
					if ((num8 < num9) | Module1.PointLineD(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Coord1M.Xm, Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Coord1M.Ym, Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Coord2M.Xm, Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Coord2M.Ym, coordMeters.Xm, coordMeters.Ym, num9))
					{
						float xo = (float)((double)MarginLEFT + (double)num8 * num4);
						float yo = (float)((double)(short)unchecked(MarginUP + num3) - (double)(Triangul.FindElevWithBldPlusHbld(Public2.PRJ.TXT[i].CoordWM) - Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev0 + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastH2) * num5);
						if (num8 < num9)
						{
							xo = MarginLEFT;
						}
						Form1_Public.DrawMast2D(Public2.PRJ.TXT[i].Text, xo, yo, num4, num5, e);
					}
				}
				e.Graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, MarginLEFT, PB.Height);
			}
			if (!Information.IsNothing(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev))
			{
				double num10 = (double)num2 / (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev.GetUpperBound(0);
				Point[] array = new Point[Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev.GetUpperBound(0) + 3 + 1];
				int upperBound2 = Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					array[j].X = (int)Math.Round((double)MarginLEFT + (double)j * num10);
					float num11 = (float)((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev[j] - Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev0 + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastH2) * num5);
					if (num11 < (float)(short)unchecked(MarginUP + num3))
					{
						array[j].Y = (int)Math.Round(num11);
					}
					else
					{
						array[j].Y = (short)unchecked(MarginUP + num3);
					}
				}
				if (!Information.IsNothing(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld))
				{
					int upperBound3 = Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld.GetUpperBound(0);
					for (int k = 0; k <= upperBound3; k++)
					{
						short num12 = (short)Math.Floor((double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[k].L1 / num * (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev.GetUpperBound(0));
						short num13 = (short)Math.Ceiling((double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[k].L2 / num * (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev.GetUpperBound(0));
						short num14 = num12;
						short num15 = num13;
						for (short num16 = num14; num16 <= num15; num16 = (short)unchecked(num16 + 1))
						{
							array[num16].Y = (int)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[k].Fundation - Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev0 + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastH2) * num5);
						}
						array[num12].X = (int)Math.Round((double)MarginLEFT + (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[k].L1 / num * (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev.GetUpperBound(0) * num10);
						array[num13].X = (int)Math.Round((double)MarginLEFT + (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[k].L2 / num * (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev.GetUpperBound(0) * num10);
					}
				}
				array[array.GetUpperBound(0) - 2].X = array[array.GetUpperBound(0) - 3].X;
				array[array.GetUpperBound(0) - 2].Y = (short)unchecked(MarginUP + num3);
				array[array.GetUpperBound(0) - 1].X = MarginLEFT;
				array[array.GetUpperBound(0) - 1].Y = (short)unchecked(MarginUP + num3);
				array[array.GetUpperBound(0)].X = array[0].X;
				array[array.GetUpperBound(0)].Y = array[0].Y;
				e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(220, 220, 220)), array);
				e.Graphics.DrawLines(PenBlack2, array);
				if (Public2.PRJ.SETT.ShowPlus2m)
				{
					Pen pen3 = new Pen(Color.Red, 1f);
					pen3.DashStyle = DashStyle.Custom;
					pen3.DashPattern = new float[2] { 8f, 4f };
					array = new Point[Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev.GetUpperBound(0) + 1];
					int upperBound4 = Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev.GetUpperBound(0);
					for (int l = 0; l <= upperBound4; l++)
					{
						array[l].X = (int)Math.Round((double)MarginLEFT + (double)l * num10);
						float num17 = (float)((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev[l] - Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev0 + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastH2 + 2f) * num5);
						if (num17 < (float)(short)unchecked(MarginUP + num3))
						{
							array[l].Y = (int)Math.Round(num17);
						}
						else
						{
							array[l].Y = (short)unchecked(MarginUP + num3);
						}
					}
					if (!Information.IsNothing(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld))
					{
						int upperBound5 = Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld.GetUpperBound(0);
						for (int m = 0; m <= upperBound5; m++)
						{
							short num18 = (short)Math.Floor((double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[m].L1 / num * (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev.GetUpperBound(0));
							short num19 = (short)Math.Ceiling((double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[m].L2 / num * (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev.GetUpperBound(0));
							short num20 = num18;
							short num21 = num19;
							for (short num22 = num20; num22 <= num21; num22 = (short)unchecked(num22 + 1))
							{
								array[num22].Y = (int)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[m].Fundation - Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev0 + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastH2) * num5);
							}
							array[num18].X = (int)Math.Round((double)MarginLEFT + (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[m].L1 / num * (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev.GetUpperBound(0) * num10);
							array[num19].X = (int)Math.Round((double)MarginLEFT + (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[m].L2 / num * (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev.GetUpperBound(0) * num10);
						}
					}
					e.Graphics.DrawLines(pen3, array);
				}
			}
			if (!Information.IsNothing(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld))
			{
				Point[] array2 = new Point[5];
				int upperBound6 = Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld.GetUpperBound(0);
				for (int n = 0; n <= upperBound6; n++)
				{
					if ((short)Math.Round((double)(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[n].Fundation + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[n].H - Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev0 + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastH2) * num5) > 0)
					{
						array2[0].X = (int)Math.Round((double)MarginLEFT + (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[n].L1 * num4);
						array2[0].Y = (int)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[n].Fundation - Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev0 + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastH2) * num5);
						array2[1].X = array2[0].X;
						array2[1].Y = (int)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[n].Fundation + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[n].H - Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev0 + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastH2) * num5);
						array2[2].X = (int)Math.Round((double)MarginLEFT + (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[n].L2 * num4);
						array2[2].Y = array2[1].Y;
						array2[3].X = array2[2].X;
						array2[3].Y = array2[0].Y;
						array2[4].X = array2[0].X;
						array2[4].Y = array2[0].Y;
						e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(180, 180, 180)), array2);
						e.Graphics.DrawLines(PenBlack2, array2);
						string text = "";
						if (Public2.PRJ.SETT.ShowBuildingText)
						{
							text = text + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[n].Text + "\r\n";
						}
						if (Public2.PRJ.SETT.ShowBuildingHeight)
						{
							text = text + Conversions.ToString(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[n].H) + "m\r\n";
						}
						if (Public2.PRJ.SETT.ShowBuildingFoundation)
						{
							text = text + Conversions.ToString(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[n].Fundation) + "m\r\n";
						}
						if (text.Length > 0)
						{
							text = text.Substring(0, text.Length - 1);
							stringSize = e.Graphics.MeasureString(text, Font8);
							e.Graphics.DrawString(text, Font8, Brushes.Black, (float)((double)(array2[0].X + array2[2].X) / 2.0 - (double)(stringSize.Width / 2f)), (float)((double)(array2[0].Y + array2[1].Y) / 2.0 - (double)(stringSize.Height / 2f)));
						}
					}
				}
			}
			if (!Information.IsNothing(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].PLNs))
			{
				int num23 = 0;
				do
				{
					if (Public2.PRJ.SETT.VertCutLimits[num23] && !Information.IsNothing(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].PLNs[num23]))
					{
						double num24 = (double)num2 / (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Size;
						double num25 = (double)num3 / (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Size;
						int upperBound7 = Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].PLNs[num23].GetUpperBound(0);
						for (int num26 = 0; num26 <= upperBound7; num26++)
						{
							Point[] array3 = new Point[Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].PLNs[num23][num26].GetUpperBound(0) + 1];
							int upperBound8 = Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].PLNs[num23][num26].GetUpperBound(0);
							for (int num27 = 0; num27 <= upperBound8; num27++)
							{
								array3[num27].X = (int)Math.Round((double)MarginLEFT + (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].PLNs[num23][num26][num27].X * num24);
								array3[num27].Y = (int)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].PLNs[num23][num26][num27].Y * num25);
							}
							Pen pen4 = new Pen(Public2.PRJ.SETT.LimitColor[num23], 3f);
							pen4.LineJoin = LineJoin.Bevel;
							e.Graphics.DrawLines(pen4, array3);
						}
					}
					num23++;
				}
				while (num23 <= 3);
			}
			float xmin = 0f - Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastL2;
			float lastL = Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastL1;
			short num28 = default;
			int N = num28;
			float dX = default;
			float X = default;
			Module1.RazmetkaOSI(xmin, lastL, 8, 13, ref N, ref dX, ref X);
			num28 = (short)N;
			N = num28 - 1;
			for (int num29 = 0; num29 <= N; num29++)
			{
				short num30 = (short)(int)Math.Round((double)MarginLEFT + (double)(X + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastL2) * num4);
				e.Graphics.DrawLine(pen, num30, MarginUP, num30, (short)unchecked(MarginUP + num3));
				stringSize = e.Graphics.MeasureString(Conversions.ToString(X), Font8);
				e.Graphics.DrawString(Conversions.ToString(X), Font8, drawBrushBlack, (float)num30 - stringSize.Width / 2f, (short)unchecked(MarginUP + num3) + 2);
				X += dX;
			}
			e.Graphics.DrawString("R, m", Font8, drawBrushBlack, (short)unchecked(MarginLEFT + num2) + 3, (short)unchecked(MarginUP + num3) - 10);
			float xmin2 = 0f - Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastH2;
			float lastH = Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastH1;
			int N2 = num28;
			Module1.RazmetkaOSI(xmin2, lastH, 8, 13, ref N2, ref dX, ref X);
			num28 = (short)N2;
			N2 = num28 - 1;
			for (int num31 = 0; num31 <= N2; num31++)
			{
				short num30 = (short)(int)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)(X + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastH2) * num5);
				e.Graphics.DrawLine(pen, MarginLEFT, num30, (short)unchecked(MarginLEFT + num2), num30);
				stringSize = e.Graphics.MeasureString(Conversions.ToString(X), Font8);
				e.Graphics.DrawString(Conversions.ToString(X), Font8, drawBrushBlack, (float)MarginLEFT - stringSize.Width - 2f, num30 - 6);
				X += dX;
			}
			e.Graphics.DrawString("H, m", Font8, drawBrushBlack, 5f, MarginUP - 22);
			e.Graphics.DrawLine(new Pen(Color.Black, 2f), MarginLEFT, MarginUP, (short)unchecked(MarginLEFT + num2), MarginUP);
			e.Graphics.DrawLine(new Pen(Color.Black, 2f), (short)unchecked(MarginLEFT + num2), MarginUP, (short)unchecked(MarginLEFT + num2), (short)unchecked(MarginUP + num3));
			e.Graphics.DrawLine(new Pen(Color.Black, 2f), (short)unchecked(MarginLEFT + num2), (short)unchecked(MarginUP + num3), MarginLEFT, (short)unchecked(MarginUP + num3));
			e.Graphics.DrawLine(new Pen(Color.Black, 2f), MarginLEFT, (short)unchecked(MarginUP + num3), MarginLEFT, MarginUP);
			if (Public2.PRJ.SETT.ShowKPonVert && !Information.IsNothing(Public2.PRJ.CPG))
			{
				int upperBound9 = Public2.PRJ.CPG.GetUpperBound(0);
				for (int num32 = 0; num32 <= upperBound9; num32++)
				{
					if (!Public2.PRJ.CPG[num32].Active || Information.IsNothing(Public2.PRJ.CPG[num32].CP))
					{
						continue;
					}
					int upperBound10 = Public2.PRJ.CPG[num32].CP.GetUpperBound(0);
					for (int num33 = 0; num33 <= upperBound10; num33++)
					{
						if (!Module1.PointLineD(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Coord1M.Xm, Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Coord1M.Ym, Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Coord2M.Xm, Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Coord2M.Ym, Public2.PRJ.CPG[num32].CP[num33].CoordM.Xm, Public2.PRJ.CPG[num32].CP[num33].CoordM.Ym, 1.0))
						{
							continue;
						}
						float num34 = (float)Math.Sqrt(Math.Pow(Public2.PRJ.CPG[num32].CP[num33].CoordM.Xm - Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Coord1M.Xm, 2.0) + Math.Pow(Public2.PRJ.CPG[num32].CP[num33].CoordM.Ym - Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Coord1M.Ym, 2.0));
						short num35 = (short)Math.Round((double)MarginLEFT + (double)num34 * num4 - 7.0);
						short num36 = (short)Math.Round((double)num34 / num * (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev.GetUpperBound(0));
						if (!((num36 >= 0) & (num36 <= Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev.GetUpperBound(0))))
						{
							continue;
						}
						float num37 = Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev[(int)Math.Round((double)num34 / num * (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev.GetUpperBound(0))];
						if (Operators.CompareString(Public2.PRJ.CPG[num32].CP[num33].Location, "Building", TextCompare: false) == 0 && !Information.IsNothing(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld))
						{
							int upperBound11 = Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld.GetUpperBound(0);
							for (int num38 = 0; num38 <= upperBound11; num38++)
							{
								if ((num34 > Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[num38].L1) & (num34 < Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[num38].L2))
								{
									num37 = Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Bld[num38].Fundation;
									break;
								}
							}
						}
						int upperBound12 = Public2.PRJ.CPG[num32].CP[num33].H.GetUpperBound(0);
						for (int num39 = 0; num39 <= upperBound12; num39++)
						{
							short num40 = (short)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.CPG[num32].CP[num33].H[num39].H + num37 - Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].Elev0 + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastH2) * num5 - 7.0);
							e.Graphics.DrawImage(Resources.ring8x8, num35, num40);
							string text2 = Public2.PRJ.SETT.ControlPointPrefix;
							if (Public2.PRJ.SETT.ShowControlPointGroups)
							{
								text2 = text2 + Conversions.ToString(num32 + 1) + ".";
							}
							text2 += Conversions.ToString(num33 + 1);
							if (Public2.PRJ.SETT.ShowControlPointHeight)
							{
								text2 = text2 + " (" + Conversions.ToString(Public2.PRJ.CPG[num32].CP[num33].H[num39].H) + "m)";
							}
							e.Graphics.DrawString(text2, Font8, Brushes.Blue, num35 + 8, num40 - 8);
						}
					}
				}
			}
			if (Public2.PRJ.SETT.ShowManualPointsZO && !Information.IsNothing(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].ManualPointZO))
			{
				int upperBound13 = Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].ManualPointZO.GetUpperBound(0);
				for (int num41 = 0; num41 <= upperBound13; num41++)
				{
					short num42 = (short)Math.Round((double)MarginLEFT + (double)(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].ManualPointZO[num41].R + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastL2) * num4);
					short num43 = (short)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].ManualPointZO[num41].H + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastH2) * num5);
					e.Graphics.FillEllipse(Brushes.Red, num42 - 3, num43 - 3, 6, 6);
					e.Graphics.DrawLine(new Pen(Color.Red, 1f), (int)Math.Round((double)MarginLEFT + (double)Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastL2 * num4), num43, num42, num43);
					e.Graphics.DrawLine(new Pen(Color.Red, 1f), num42, MarginUP, num42, num43);
					string s = "R=" + Conversions.ToString(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].ManualPointZO[num41].R) + "m\r\nH=" + Conversions.ToString(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].ManualPointZO[num41].H) + "m";
					e.Graphics.DrawString(s, Font8, Brushes.Red, num42 + 2, num43 - 26);
				}
			}
			if (!Information.IsNothing(Public2.PRJ.VP))
			{
				if (Public2.PRJ.SETT.ShowLengthZO & (Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LenghtZO != 9999f))
				{
					short num44 = (short)Math.Round((double)MarginLEFT + (double)(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LenghtZO + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastL2) * num4);
					e.Graphics.DrawLine(pen2, num44, MarginUP, num44, (short)unchecked(MarginUP + num3));
					string s2 = Conversions.ToString(Math.Round(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LenghtZO, 1));
					stringSize = e.Graphics.MeasureString(s2, Font8);
					e.Graphics.DrawString(s2, Font8, Brushes.Red, (float)num44 - stringSize.Width / 2f, MarginUP - 15);
				}
				if (Public2.PRJ.SETT.ShowHgtMaxZO & (Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].HeightMaxZO != 9999f))
				{
					short num45 = (short)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].HeightMaxZO + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastH2) * num5);
					e.Graphics.DrawLine(pen2, MarginLEFT, num45, (short)unchecked(MarginLEFT + num2), num45);
					string s3 = Conversions.ToString(Math.Round(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].HeightMaxZO, 1));
					stringSize = e.Graphics.MeasureString(s3, Font8);
					e.Graphics.DrawString(s3, Font8, Brushes.Red, (short)unchecked(MarginLEFT + num2) + 3, (float)num45 - stringSize.Height / 2f);
				}
				if (Public2.PRJ.SETT.ShowHgtBottomZO & (Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].HeightBottomZO != 9999f))
				{
					short num46 = (short)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].HeightBottomZO + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastH2) * num5);
					e.Graphics.DrawLine(pen2, MarginLEFT, num46, (short)unchecked(MarginLEFT + num2), num46);
					string s4 = Conversions.ToString(Math.Round(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].HeightBottomZO, 1));
					stringSize = e.Graphics.MeasureString(s4, Font8);
					e.Graphics.DrawString(s4, Font8, Brushes.Red, (short)unchecked(MarginLEFT + num2) + 3, (float)num46 - stringSize.Height / 2f);
				}
				if (Public2.PRJ.SETT.ShowHgtTopZO & (Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].HeightTopZO != 9999f))
				{
					short num47 = (short)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].HeightTopZO + Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].LastH2) * num5);
					e.Graphics.DrawLine(pen2, MarginLEFT, num47, (short)unchecked(MarginLEFT + num2), num47);
					string s5 = Conversions.ToString(Math.Round(Public2.PRJ.VP[MyProject.Forms.Form1.IndexVertPlane].HeightTopZO, 1));
					stringSize = e.Graphics.MeasureString(s5, Font8);
					e.Graphics.DrawString(s5, Font8, Brushes.Red, (short)unchecked(MarginLEFT + num2) + 3, (float)num47 - stringSize.Height / 2f);
				}
			}
		}
	}

	private void PB_Resize(object sender, EventArgs e)
	{
		PB.Refresh();
	}

	private void CmdToVertPlane_Click(object sender, EventArgs e)
	{
		MyProject.Forms.Form1.TV.SelectedNode = MyProject.Forms.Form1.TV.Nodes["VertPlanes"];
		MyProject.Forms.Form1.TV.Focus();
		MyProject.Forms.Form1.UC_VertPlanes1.DGV1.CurrentCell = MyProject.Forms.Form1.UC_VertPlanes1.DGV1[0, MyProject.Forms.Form1.IndexVertPlane];
		MyProject.Forms.Form1.UC_VertPlanes1.DGV1.Rows[MyProject.Forms.Form1.IndexVertPlane].Selected = true;
		Close();
	}

	private void CmdDelVertPlane_Click(object sender, EventArgs e)
	{
		checked
		{
			if (Information.IsNothing(Public2.PRJ.VP))
			{
				Close();
			}
			else if (Public2.PRJ.VP.GetUpperBound(0) <= 0)
			{
				Public2.PRJ.VP = null;
			}
			else
			{
				int indexVertPlane = MyProject.Forms.Form1.IndexVertPlane;
				int num = Public2.PRJ.VP.GetUpperBound(0) - 1;
				for (int i = indexVertPlane; i <= num; i++)
				{
					Public2.PRJ.VP[i] = Module1.DeepClone(Public2.PRJ.VP[i + 1]);
				}
				ref Public2.StructVertPlane[] vP = ref Public2.PRJ.VP;
				vP = (Public2.StructVertPlane[])Utils.CopyArray(vP, new Public2.StructVertPlane[Public2.PRJ.VP.GetUpperBound(0) - 1 + 1]);
			}
			MyProject.Forms.Form1.PB.Refresh();
			Close();
		}
	}
}
