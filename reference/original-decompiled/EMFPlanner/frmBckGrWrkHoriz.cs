using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EMFPlanner.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class frmBckGrWrkHoriz : Form
{
	private IContainer components;

	private long TIME;

	private short counter;

	private object CountLock;

	[field: AccessedThroughProperty("Label3")]
	internal virtual Label Label3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label1")]
	internal virtual Label Label1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ProgressBar1")]
	internal virtual ProgressBar ProgressBar1
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
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdCancel_Click;
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

	[field: AccessedThroughProperty("BackgroundWorker1")]
	internal virtual BackgroundWorker BackgroundWorker1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DoWorkEventHandler value2 = BackgroundWorker1_DoWork;
			ProgressChangedEventHandler value3 = BackgroundWorker1_ProgressChanged;
			RunWorkerCompletedEventHandler value4 = BackgroundWorker1_RunWorkerCompleted;
			BackgroundWorker backgroundWorker = field;
			if (backgroundWorker != null)
			{
				backgroundWorker.DoWork -= value2;
				backgroundWorker.ProgressChanged -= value3;
				backgroundWorker.RunWorkerCompleted -= value4;
			}
			field = value;
			backgroundWorker = field;
			if (backgroundWorker != null)
			{
				backgroundWorker.DoWork += value2;
				backgroundWorker.ProgressChanged += value3;
				backgroundWorker.RunWorkerCompleted += value4;
			}
		}
	}

	public frmBckGrWrkHoriz()
	{
		Load += frmBackgroundWorker_Load;
		FormClosed += frmBackgroundWorker_FormClosed;
		CountLock = RuntimeHelpers.GetObjectValue(new object());
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.frmBckGrWrkHoriz));
		this.Label3 = new System.Windows.Forms.Label();
		this.Label1 = new System.Windows.Forms.Label();
		this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
		this.cmdCancel = new System.Windows.Forms.Button();
		this.BackgroundWorker1 = new System.ComponentModel.BackgroundWorker();
		base.SuspendLayout();
		resources.ApplyResources(this.Label3, "Label3");
		this.Label3.Name = "Label3";
		resources.ApplyResources(this.Label1, "Label1");
		this.Label1.Name = "Label1";
		resources.ApplyResources(this.ProgressBar1, "ProgressBar1");
		this.ProgressBar1.Name = "ProgressBar1";
		resources.ApplyResources(this.cmdCancel, "cmdCancel");
		this.cmdCancel.Name = "cmdCancel";
		this.cmdCancel.UseVisualStyleBackColor = true;
		this.BackgroundWorker1.WorkerReportsProgress = true;
		this.BackgroundWorker1.WorkerSupportsCancellation = true;
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.Label3);
		base.Controls.Add(this.Label1);
		base.Controls.Add(this.ProgressBar1);
		base.Controls.Add(this.cmdCancel);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmBckGrWrkHoriz";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void frmBackgroundWorker_Load(object sender, EventArgs e)
	{
		int subKeyCount = MyProject.Computer.Registry.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\CentralProcessor", writable: false).SubKeyCount;
		Label3.Text = "Calculation of EMF exposure value using " + subKeyCount + " helper-processes";
		TIME = checked((long)Math.Round(DateTime.Now.Subtract(DateTime.MinValue.AddYears(2000)).TotalMilliseconds));
		CalcSZZ.FlagStopCalc = false;
		counter = -1;
		ProgressBar1.Maximum = CalcSZZ.ListHVP.GetUpperBound(0);
		ProgressBar1.Value = 0;
		Label1.Text = "";
		if (!BackgroundWorker1.IsBusy)
		{
			BackgroundWorker1.RunWorkerAsync();
		}
	}

	private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
	{
		Parallel.ForEach(CalcSZZ.ListHVP, checked([SpecialName] (int item, ParallelLoopState LoopeState) =>
		{
			CalcSZZ.CalcOneHorizPlane((short)item);
			object countLock = CountLock;
			ObjectFlowControl.CheckForSyncLockOnValueType(countLock);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(countLock, ref lockTaken);
				counter++;
				BackgroundWorker1.ReportProgress(counter);
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(countLock);
				}
			}
			if (BackgroundWorker1.CancellationPending)
			{
				LoopeState.Stop();
			}
		}));
	}

	private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
	{
		ProgressBar1.Value = e.ProgressPercentage;
		Label1.Text = "Elapsed time: " + Strings.Format((DateTime.Now.Subtract(DateTime.MinValue.AddYears(2000)).TotalMilliseconds - (double)TIME) / 1000.0, "0.0") + " sec.";
	}

	private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		Close();
	}

	private void cmdCancel_Click(object sender, EventArgs e)
	{
		BackgroundWorker1.CancelAsync();
		CalcSZZ.FlagStopCalc = true;
	}

	private void frmBackgroundWorker_FormClosed(object sender, FormClosedEventArgs e)
	{
		BackgroundWorker1.CancelAsync();
	}
}
