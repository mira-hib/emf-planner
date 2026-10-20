using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using EMFPlanner.My;
using EMFPlanner.My.Resources;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.FileIO;

namespace EMFPlanner;

[DesignerGenerated]
public class Form1 : Form
{
	public struct RequestParam
	{
		public string NameTileURL;

		public string NameTileCache;

		public int X;

		public int Y;

		public int Z;

		public RequestParam(string NameTileURL, string NameTileCache, int X, int Y, int Z)
		{
			this = default;
			this.NameTileURL = NameTileURL;
			this.NameTileCache = NameTileCache;
			this.X = X;
			this.Y = Y;
			this.Z = Z;
		}
	}

	public delegate void Del1(string NameTileCache, int X, int Y);

	private bool FlagProcessing;

	private object FlagMSG;

	internal UC_PRTO UC_PRTO1;

	internal UC_Limits UC_Limits1;

	internal UC_Origin UC_Origin1;

	internal UC_Buildings UC_Buildings1;

	internal UC_Relief UC_Relief1;

	internal UC_Lines UC_Lines1;

	internal UC_Antennas UC_Antennas1;

	internal UC_ControlPoints UC_ControlPoints1;

	internal UC_HorizPlanes UC_HorizPlanes1;

	internal UC_VertPlanes UC_VertPlanes1;

	internal UC_Tables UC_Tables1;

	internal UC_Report UC_Report1;

	internal UC_CAD UC_CAD1;

	internal UC_3D UC_3D1;

	internal UC_Raster UC_Raster1;

	internal UC_Setup UC_Setup1;

	public string NameInCacheTileServer;

	public string ExtentionTileServer;

	public string UrlTileServer;

	private double ShirCursor;

	private double DolgCursor;

	public int TileX;

	public int TileY;

	public int PointX;

	public int PointY;

	private bool FlagRedrawMap;

	private bool MapMoving;

	private int MouseDownX;

	private int MouseDownY;

	private string[,] ShowingTile;

	private string[] StackTiles;

	private Semaphore SemStackTiles;

	private bool FlagMouseInPB;

	private bool FlagShowingTiles;

	private Graphics G;

	private short Xmap;

	private short Ymap;

	private int WHEEL;

	private SizeF stringSize;

	private Font Font8;

	private Brush BrushBlack;

	private Brush BrushDarkBlue;

	private Brush BrushBlue;

	private Font Font10;

	private double tmpTileX1;

	private double tmpTileY1;

	private long LineX1;

	private long LineY1;

	private long LineX2;

	private long LineY2;

	private Pen PenBlue1;

	private Pen PenMagenta2;

	private Pen PenBlack1;

	private Pen PenBlack2;

	private Pen PenBrown1;

	private Pen PenYellow1;

	private bool FlagPick;

	private Public2.CoordPix PickPoint;

	private bool FlagOriginPick;

	private int IndexRPL;

	private short IndexTX1;

	private short IndexGR;

	private short IndexCP;

	private short IndexHorAreaX;

	private short IndexHorAreaY;

	public short IndexBLD;

	public short IndexPNT;

	private Public2.StructBuilding PickBLD;

	private bool DragStart;

	public short IndexRoad;

	public short IndexFence;

	public short IndexText;

	private Public2.StructRoad PickROAD;

	private Public2.StructFence PickFence;

	private Public2.StructText PickText;

	public short IndexElevLine;

	public short IndexElevPoint;

	private Public2.StructElevLine PickElevLine;

	private bool FlagRulerPick;

	private int IndexRuler;

	public int IndexVertPlane;

	private IContainer components;

	[field: AccessedThroughProperty("cmdFile")]
	internal virtual ToolStripDropDownButton cmdFile
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdNEW")]
	internal virtual ToolStripMenuItem cmdNEW
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdNEW_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdOPEN")]
	internal virtual ToolStripMenuItem cmdOPEN
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdOPEN_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdSAVE")]
	internal virtual ToolStripMenuItem cmdSAVE
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdSAVE_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdSaveAS")]
	internal virtual ToolStripMenuItem cmdSaveAS
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdSaveAS_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStripSeparator1")]
	internal virtual ToolStripSeparator ToolStripSeparator1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("mnuLastFile1")]
	internal virtual ToolStripMenuItem mnuLastFile1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuLastFile1_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuLastFile2")]
	internal virtual ToolStripMenuItem mnuLastFile2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuLastFile2_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("mnuLastFile3")]
	internal virtual ToolStripMenuItem mnuLastFile3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = mnuLastFile3_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdSavePanel")]
	internal virtual ToolStripButton cmdSavePanel
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdSAVE_Click;
			ToolStripButton toolStripButton = field;
			if (toolStripButton != null)
			{
				toolStripButton.Click -= value2;
			}
			field = value;
			toolStripButton = field;
			if (toolStripButton != null)
			{
				toolStripButton.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStripSeparator10")]
	internal virtual ToolStripSeparator ToolStripSeparator10
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolStripDropDownButton3")]
	internal virtual ToolStripDropDownButton ToolStripDropDownButton3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdAbout")]
	internal virtual ToolStripMenuItem cmdAbout
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdAbout_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdUserManual")]
	internal virtual ToolStripMenuItem cmdUserManual
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdUserManual_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdCheckForUpdates")]
	internal virtual ToolStripMenuItem cmdCheckForUpdates
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdCheckForUpdates_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStripSeparator9")]
	internal virtual ToolStripSeparator ToolStripSeparator9
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("MenuStrip1")]
	internal virtual MenuStrip MenuStrip1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("StatusStrip1")]
	internal virtual StatusStrip StatusStrip1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("SplitContainer1")]
	internal virtual SplitContainer SplitContainer1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TV")]
	internal virtual TreeView TV
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			TreeViewEventHandler value2 = TV_AfterSelect;
			TreeView treeView = field;
			if (treeView != null)
			{
				treeView.AfterSelect -= value2;
			}
			field = value;
			treeView = field;
			if (treeView != null)
			{
				treeView.AfterSelect += value2;
			}
		}
	}

	[field: AccessedThroughProperty("SplitContainer2")]
	internal virtual SplitContainer SplitContainer2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("SplitContainer3")]
	internal virtual SplitContainer SplitContainer3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ImageList1")]
	internal virtual ImageList ImageList1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("MenuStrip2")]
	internal virtual MenuStrip MenuStrip2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TScmbZOOM")]
	internal virtual ToolStripComboBox TScmbZOOM
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TScmbZOOM_SelectedIndexChanged;
			ToolStripComboBox toolStripComboBox = field;
			if (toolStripComboBox != null)
			{
				toolStripComboBox.SelectedIndexChanged -= value2;
			}
			field = value;
			toolStripComboBox = field;
			if (toolStripComboBox != null)
			{
				toolStripComboBox.SelectedIndexChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TScmbURL")]
	internal virtual ToolStripComboBox TScmbURL
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TScmbURL_SelectedIndexChanged;
			ToolStripComboBox toolStripComboBox = field;
			if (toolStripComboBox != null)
			{
				toolStripComboBox.SelectedIndexChanged -= value2;
			}
			field = value;
			toolStripComboBox = field;
			if (toolStripComboBox != null)
			{
				toolStripComboBox.SelectedIndexChanged += value2;
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

	[field: AccessedThroughProperty("OFD2")]
	internal virtual OpenFileDialog OFD2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("SFD1")]
	internal virtual SaveFileDialog SFD1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("pbBM")]
	internal virtual PictureBox pbBM
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Panel1")]
	internal virtual Panel Panel1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = Panel1_Resize;
			Panel panel = field;
			if (panel != null)
			{
				panel.Resize -= value2;
			}
			field = value;
			panel = field;
			if (panel != null)
			{
				panel.Resize += value2;
			}
		}
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
			MouseEventHandler value3 = PB_MouseDown;
			MouseEventHandler value4 = PB_MouseMove;
			MouseEventHandler value5 = PB_MouseUp;
			EventHandler value6 = PB_MouseEnter;
			EventHandler value7 = PB_MouseLeave;
			PictureBox pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Paint -= value2;
				pictureBox.MouseDown -= value3;
				pictureBox.MouseMove -= value4;
				pictureBox.MouseUp -= value5;
				pictureBox.MouseEnter -= value6;
				pictureBox.MouseLeave -= value7;
			}
			field = value;
			pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Paint += value2;
				pictureBox.MouseDown += value3;
				pictureBox.MouseMove += value4;
				pictureBox.MouseUp += value5;
				pictureBox.MouseEnter += value6;
				pictureBox.MouseLeave += value7;
			}
		}
	}

	[field: AccessedThroughProperty("lblCOORD")]
	internal virtual ToolStripStatusLabel lblCOORD
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Timer1")]
	internal virtual System.Windows.Forms.Timer Timer1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = Timer1_Tick;
			System.Windows.Forms.Timer timer = field;
			if (timer != null)
			{
				timer.Tick -= value2;
			}
			field = value;
			timer = field;
			if (timer != null)
			{
				timer.Tick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("lblHeight")]
	internal virtual ToolStripStatusLabel lblHeight
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblCoordFromOrigin")]
	internal virtual ToolStripStatusLabel lblCoordFromOrigin
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

	[field: AccessedThroughProperty("ilICO")]
	internal virtual ImageList ilICO
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblTime")]
	internal virtual ToolStripStatusLabel lblTime
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtMapBrightness")]
	internal virtual NumericUpDown txtMapBrightness
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = txtMapBrightness_MouseUp;
			MouseEventHandler value3 = txtMapBrightness_MouseUp;
			NumericUpDown numericUpDown = field;
			if (numericUpDown != null)
			{
				numericUpDown.MouseUp -= value2;
				numericUpDown.MouseDown -= value3;
			}
			field = value;
			numericUpDown = field;
			if (numericUpDown != null)
			{
				numericUpDown.MouseUp += value2;
				numericUpDown.MouseDown += value3;
			}
		}
	}

	[field: AccessedThroughProperty("chkShowMap")]
	internal virtual CheckBox chkShowMap
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowMap_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ProgressBar")]
	internal virtual ToolStripProgressBar ProgressBar
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdRuler")]
	internal virtual ToolStripMenuItem cmdRuler
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdRuler_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStripSeparator3")]
	internal virtual ToolStripSeparator ToolStripSeparator3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdEXIT")]
	internal virtual ToolStripMenuItem cmdEXIT
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdEXIT_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ContCP")]
	internal virtual ContextMenuStrip ContCP
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdNewCP")]
	internal virtual ToolStripMenuItem cmdNewCP
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdNewCP_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdMoveCP")]
	internal virtual ToolStripMenuItem cmdMoveCP
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdMoveCP_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ContANT")]
	internal virtual ContextMenuStrip ContANT
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdMoveANT")]
	internal virtual ToolStripMenuItem cmdMoveANT
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdMoveANT_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("SplitContainer4")]
	internal virtual SplitContainer SplitContainer4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("chkShowOrigin")]
	internal virtual CheckBox chkShowOrigin
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowOrigin_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("GroupBox2")]
	internal virtual GroupBox GroupBox2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("chkShowAllBuildings")]
	internal virtual CheckBox chkShowAllBuildings
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowAllBuildings_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkShowBuildingText")]
	internal virtual CheckBox chkShowBuildingText
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowBuildingText_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkShowBuildingFoundation")]
	internal virtual CheckBox chkShowBuildingFoundation
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowBuildingFoundation_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkShowBuildingHeight")]
	internal virtual CheckBox chkShowBuildingHeight
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowBuildingHeight_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkShowRelief")]
	internal virtual CheckBox chkShowRelief
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowRelief_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
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

	[field: AccessedThroughProperty("chkShowFences")]
	internal virtual CheckBox chkShowFences
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowFences_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkShowRoads")]
	internal virtual CheckBox chkShowRoads
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowRoads_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkShowText")]
	internal virtual CheckBox chkShowText
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowText_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("GroupBox4")]
	internal virtual GroupBox GroupBox4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("chkShowLongAzimuth")]
	internal virtual CheckBox chkShowLongAzimuth
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowLongAzimuth_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkShowAntennas")]
	internal virtual CheckBox chkShowAntennas
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowAntennas_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkShowAntennaAzimuth")]
	internal virtual CheckBox chkShowAntennaAzimuth
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowAntennaAzimuth_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkShowAntennaLabels")]
	internal virtual CheckBox chkShowAntennaLabels
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowAntennaLabels_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("GroupBox5")]
	internal virtual GroupBox GroupBox5
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("GroupBox6")]
	internal virtual GroupBox GroupBox6
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("chkShowControlPointHeight")]
	internal virtual CheckBox chkShowControlPointHeight
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowControlPointHeight_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkShowControlPoints")]
	internal virtual CheckBox chkShowControlPoints
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowControlPoints_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkShowControlPointGroups")]
	internal virtual CheckBox chkShowControlPointGroups
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowControlPointGroups_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkShowHorizPlaneScale")]
	internal virtual CheckBox chkShowHorizPlaneScale
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowHorizPlaneScale_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkShowConturZO")]
	internal virtual CheckBox chkShowConturZO
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowConturZO_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("GroupBox7")]
	internal virtual GroupBox GroupBox7
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

	[field: AccessedThroughProperty("chkShowVertPlaneLines")]
	internal virtual CheckBox chkShowVertPlaneLines
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowVertPlaneLines_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStripSeparator4")]
	internal virtual ToolStripSeparator ToolStripSeparator4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolStripDropDownButton1")]
	internal virtual ToolStripDropDownButton ToolStripDropDownButton1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdCleanOutsideScreen")]
	internal virtual ToolStripMenuItem cmdCleanOutsideScreen
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdCleanOutside_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdCleanOutsideArea")]
	internal virtual ToolStripMenuItem cmdCleanOutsideArea
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdCleanOutsideArea_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ContOrigin")]
	internal virtual ContextMenuStrip ContOrigin
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdSetOrigin")]
	internal virtual ToolStripMenuItem cmdSetOrigin
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdSetOrigin_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdPurchase")]
	internal virtual ToolStripMenuItem cmdPurchase
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdPurchase_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdActivation")]
	internal virtual ToolStripMenuItem cmdActivation
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdActivation_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("GroupBox8")]
	internal virtual GroupBox GroupBox8
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label2")]
	internal virtual Label Label2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtCircleStep")]
	internal virtual NumericUpDown txtCircleStep
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = txtCircleStep_ValueChanged;
			NumericUpDown numericUpDown = field;
			if (numericUpDown != null)
			{
				numericUpDown.ValueChanged -= value2;
			}
			field = value;
			numericUpDown = field;
			if (numericUpDown != null)
			{
				numericUpDown.ValueChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkShowCircles")]
	internal virtual CheckBox chkShowCircles
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowCircles_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	public Form1()
	{
		Load += Form1_Load;
		FormClosing += Form1_FormClosing;
		MouseWheel += Form1_MouseWheel;
		FlagProcessing = true;
		FlagRedrawMap = false;
		MapMoving = false;
		ShowingTile = checked(new string[(int)Math.Ceiling((double)(Screen.PrimaryScreen.Bounds.Width - 256) / 256.0) + 1, (int)Math.Ceiling((double)(Screen.PrimaryScreen.Bounds.Height - 256) / 256.0) + 1]);
		StackTiles = new string[1];
		SemStackTiles = new Semaphore(1, 1000);
		FlagMouseInPB = false;
		FlagShowingTiles = false;
		WHEEL = 0;
		stringSize = default;
		Font8 = new Font("Arial", 8f, FontStyle.Regular);
		BrushBlack = new SolidBrush(Color.Black);
		BrushDarkBlue = new SolidBrush(Color.DarkBlue);
		BrushBlue = new SolidBrush(Color.Blue);
		Font10 = new Font("Arial", 10f, FontStyle.Regular);
		PenBlue1 = new Pen(Color.DarkBlue, 1f);
		PenMagenta2 = new Pen(Color.DarkBlue, 2f);
		PenBlack1 = new Pen(Color.Black, 1f);
		PenBlack2 = new Pen(Color.Black, 2f);
		PenBrown1 = new Pen(Color.Brown, 2f);
		PenYellow1 = new Pen(Color.YellowGreen, 1f);
		FlagPick = false;
		FlagOriginPick = false;
		IndexRPL = -1;
		IndexTX1 = -1;
		IndexGR = -1;
		IndexCP = -1;
		IndexHorAreaX = -1;
		IndexHorAreaY = -1;
		IndexBLD = -1;
		IndexPNT = -1;
		IndexRoad = -1;
		IndexFence = -1;
		IndexText = -1;
		IndexElevLine = -1;
		IndexElevPoint = -1;
		FlagRulerPick = false;
		IndexRuler = -1;
		IndexVertPlane = -1;
		InitializeComponent();
	}

	private void Form1_Load(object sender, EventArgs e)
	{
		new Thread(GetServerName).Start();
		WindowState = FormWindowState.Maximized;
		SplitContainer4.Panel2Collapsed = true;
		DrawTV();
		LoadTScmbURL();
		TScmbURL.Text = Public2.PRJ.SETT.NameTileServer;
		TScmbZOOM.Text = Conversions.ToString(Public2.PRJ.SETT.ZOOM);
		ShowCheckBoxes();
		string nameTileServer = Public2.PRJ.SETT.NameTileServer;
		ref byte projection = ref Public2.PRJ.SETT.Projection;
		ref byte reference = ref projection;
		int PROJECTION = projection;
		CalcTileWeb.LoadTileServer(nameTileServer, ref PROJECTION, ref NameInCacheTileServer, ref ExtentionTileServer, ref UrlTileServer);
		checked
		{
			reference = (byte)PROJECTION;
			FlagRedrawMap = true;
			TileX = Public2.PRJ.SETT.TileX;
			TileY = Public2.PRJ.SETT.TileY;
			PointX = Public2.PRJ.SETT.PointX;
			PointY = Public2.PRJ.SETT.PointY;
			DrawPIC();
			Form1_Public.SetScreenBorders();
			mnuLastFile1.Visible = false;
			mnuLastFile2.Visible = false;
			mnuLastFile3.Visible = false;
			mnuLastFile1.Text = "";
			mnuLastFile2.Text = "";
			mnuLastFile3.Text = "";
			if (MyProject.Computer.FileSystem.FileExists(Public2.AppDataFolder + "\\LastFiles.tmp"))
			{
				StreamReader streamReader = new StreamReader(Public2.AppDataFolder + "\\LastFiles.tmp", Encoding.GetEncoding(1251));
				mnuLastFile1.Text = streamReader.ReadLine();
				mnuLastFile2.Text = streamReader.ReadLine();
				mnuLastFile3.Text = streamReader.ReadLine();
				if (Operators.CompareString(mnuLastFile1.Text, "", TextCompare: false) != 0)
				{
					mnuLastFile1.Visible = true;
				}
				if (Operators.CompareString(mnuLastFile2.Text, "", TextCompare: false) != 0)
				{
					mnuLastFile2.Visible = true;
				}
				if (Operators.CompareString(mnuLastFile3.Text, "", TextCompare: false) != 0)
				{
					mnuLastFile3.Visible = true;
				}
				streamReader.Close();
			}
			if (Strings.Len(Interaction.Command()) > 5)
			{
				string text = Strings.Replace(Interaction.Command(), "\"", "");
				text = text.Trim();
				if ((Operators.CompareString(text.Substring(Strings.Len(text) - 4), "xemf", TextCompare: false) == 0) | (Operators.CompareString(text.Substring(Strings.Len(text) - 4), "XEMF", TextCompare: false) == 0))
				{
					Public2.ProjectFileName = text;
					Public2.AppSetup.FolderProject = Microsoft.VisualBasic.FileIO.FileSystem.GetFileInfo(Public2.ProjectFileName).DirectoryName;
					Form1_Public.OpenProject(Public2.ProjectFileName);
					Text = "EMF Planner 7.1 Trial " + Public2.ProjectFileName;
					if (Operators.CompareString(Form1_Public.NodeName, "PRTO", TextCompare: false) == 0)
					{
						if (!Information.IsNothing(UC_PRTO1))
						{
							UC_PRTO1.FillDGV1();
						}
					}
					else
					{
						TV.SelectedNode = TV.Nodes["PRTO"];
					}
					Form1_Public.IndexTX = 0;
					UC_PRTO1.SelRow = 0;
					UC_PRTO1.RefreshRightPanel();
					FlagRedrawMap = false;
					TScmbURL.Text = Public2.PRJ.SETT.NameTileServer;
					TScmbZOOM.Text = Conversions.ToString(Public2.PRJ.SETT.ZOOM);
					ShowCheckBoxes();
					string nameTileServer2 = Public2.PRJ.SETT.NameTileServer;
					ref byte projection2 = ref Public2.PRJ.SETT.Projection;
					reference = ref projection2;
					PROJECTION = projection2;
					CalcTileWeb.LoadTileServer(nameTileServer2, ref PROJECTION, ref NameInCacheTileServer, ref ExtentionTileServer, ref UrlTileServer);
					reference = (byte)PROJECTION;
					FlagRedrawMap = true;
					TileX = Public2.PRJ.SETT.TileX;
					TileY = Public2.PRJ.SETT.TileY;
					PointX = Public2.PRJ.SETT.PointX;
					PointY = Public2.PRJ.SETT.PointY;
					DrawPIC();
					Triangul.CalcTriangulate();
					Form1_Public.VPSelRow = 0;
					Form1_Public.CPGSelRow = 0;
					SortLastFile();
				}
			}
			if (!Public2.AppSetup.DontCheckNewVersionEveryAppStart)
			{
				UpdateMessage.StartUpdateMessage();
			}
			ShowCheckBoxes();
			Form1_Public.SaveProject(Module1.FolderTMP + "\\file1.tmp");
		}
	}

	private void Form1_FormClosing(object sender, FormClosingEventArgs e)
	{
		UC_Validation();
		Public2.AppSetup.TileX = TileX;
		Public2.AppSetup.TileY = TileY;
		Public2.AppSetup.PointX = PointX;
		Public2.AppSetup.PointY = PointY;
		if (ZaprosNaSohr())
		{
			FlagMSG = Interaction.MsgBox(Localization.LocMSG(101), MsgBoxStyle.YesNoCancel);
			if (Operators.ConditionalCompareObjectEqual(FlagMSG, MsgBoxResult.Yes, TextCompare: false))
			{
				cmdSAVE.PerformClick();
			}
			else if (Operators.ConditionalCompareObjectEqual(FlagMSG, MsgBoxResult.Cancel, TextCompare: false))
			{
				e.Cancel = true;
			}
		}
	}

	public void DrawTV()
	{
		TV.Nodes.Add("PRTO", "Radiating Sources", 11, 11);
		TV.Nodes.Add("Limits", "EMF Limits", 4, 4);
		TV.Nodes.Add("Origin", "Origin and Grid", 8, 8);
		TV.Nodes.Add("Antennas", "Antenna Locations", 1, 1);
		TV.Nodes.Add("Buildings", "Buildings", 5, 5);
		TV.Nodes.Add("Relief", "Terrain", 10, 10);
		TV.Nodes.Add("Lines", "Lines and Labels", 7, 7);
		TV.Nodes.Add("ControlPoints", "Point Calculations", 6, 6);
		TV.Nodes.Add("HorizPlanes", "Horizontal Cuts", 3, 3);
		TV.Nodes.Add("VertPlanes", "Vertical Cuts", 2, 2);
		TV.Nodes.Add("Tables", "Tables and Graphs", 12, 12);
		TV.Nodes.Add("Report", "Report by template", 13, 13);
		TV.Nodes.Add("CAD", "CAD", 16, 16);
		TV.Nodes.Add("3D", "3D", 17, 17);
		TV.Nodes.Add("Raster", "Calibrate Map Image", 9, 9);
		TV.Nodes.Add("Setup", "Settings", 0, 0);
		TV.ExpandAll();
	}

	private void ShowPanels()
	{
		checked
		{
			int num = SplitContainer2.Panel1.Controls.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				SplitContainer2.Panel1.Controls[i].Dispose();
			}
			int num2 = SplitContainer3.Panel1.Controls.Count - 1;
			for (int j = 0; j <= num2; j++)
			{
				SplitContainer3.Panel1.Controls[j].Dispose();
			}
			IndexBLD = -1;
			IndexTX1 = -1;
			IndexGR = -1;
			IndexCP = -1;
			IndexRoad = -1;
			IndexFence = -1;
			IndexText = -1;
			IndexElevLine = -1;
			IndexElevPoint = -1;
			TreeNode selectedNode = TV.SelectedNode;
			if (selectedNode == null)
			{
				return;
			}
			if ((Operators.CompareString(Form1_Public.NodeName, "Relief", TextCompare: false) == 0) & (Operators.CompareString(selectedNode.Name, "Relief", TextCompare: false) != 0))
			{
				Triangul.CalcTriangulate();
			}
			else if ((Operators.CompareString(Form1_Public.NodeName, "Origin", TextCompare: false) == 0) & (Operators.CompareString(selectedNode.Name, "Origin", TextCompare: false) != 0))
			{
				CalcCoords.ConvScaleWebMercMetersToMeters();
				CalcCoords.ConvAllTxWebMercMetersToMeters();
				CalcCoords.ConvAllControlPointsWebMercMetersToMeters();
				CalcCoords.ConvAllVertPlanesWebMercMetersToMeters();
			}
			else if ((Operators.CompareString(Form1_Public.NodeName, "Setup", TextCompare: false) == 0) & (Operators.CompareString(selectedNode.Name, "Setup", TextCompare: false) != 0))
			{
				if (!Information.IsNothing(UC_Setup1))
				{
					UC_Setup1.GroupBox1.Focus();
				}
			}
			else if ((Operators.CompareString(Form1_Public.NodeName, "ControlPoints", TextCompare: false) == 0) & (Operators.CompareString(selectedNode.Name, "ControlPoints", TextCompare: false) != 0))
			{
				Form1_Public.CPGSelRow = UC_ControlPoints1.SelRowDGV1;
			}
			Form1_Public.NodeName = selectedNode.Name;
			if (Operators.CompareString(selectedNode.Name, "PRTO", TextCompare: false) == 0)
			{
				ShowPanelLeft(Show: false, 0);
				ShowPanelUp(Show: true, 500);
				ShowPanelMAP(Show: false);
				UC_PRTO1 = new UC_PRTO();
				UC_PRTO uC_PRTO = UC_PRTO1;
				SplitContainer3.Panel1.Controls.Add(UC_PRTO1);
				uC_PRTO.Dock = DockStyle.Fill;
				_ = null;
				SplitContainer4.Panel2Collapsed = true;
			}
			else if (Operators.CompareString(selectedNode.Name, "Limits", TextCompare: false) == 0)
			{
				ShowPanelLeft(Show: true, 1300);
				ShowPanelUp(Show: false, 0);
				ShowPanelMAP(Show: false);
				UC_Limits1 = new UC_Limits();
				UC_Limits uC_Limits = UC_Limits1;
				SplitContainer2.Panel1.Controls.Add(UC_Limits1);
				uC_Limits.Dock = DockStyle.Fill;
				_ = null;
				SplitContainer4.Panel2Collapsed = true;
			}
			else if (Operators.CompareString(selectedNode.Name, "Raster", TextCompare: false) == 0)
			{
				ShowPanelLeft(Show: true, 700);
				ShowPanelUp(Show: false, 0);
				ShowPanelMAP(Show: true);
				UC_Raster1 = new UC_Raster();
				UC_Raster uC_Raster = UC_Raster1;
				SplitContainer2.Panel1.Controls.Add(UC_Raster1);
				uC_Raster.Dock = DockStyle.Fill;
				_ = null;
				SplitContainer4.Panel2Collapsed = false;
			}
			else if (Operators.CompareString(selectedNode.Name, "Origin", TextCompare: false) == 0)
			{
				ShowPanelLeft(Show: true, 300);
				ShowPanelUp(Show: false, 0);
				ShowPanelMAP(Show: true);
				UC_Origin1 = new UC_Origin();
				UC_Origin uC_Origin = UC_Origin1;
				SplitContainer2.Panel1.Controls.Add(UC_Origin1);
				uC_Origin.Dock = DockStyle.Fill;
				_ = null;
				SplitContainer4.Panel2Collapsed = false;
			}
			else if (Operators.CompareString(selectedNode.Name, "Buildings", TextCompare: false) == 0)
			{
				ShowPanelLeft(Show: true, 300);
				ShowPanelUp(Show: false, 0);
				ShowPanelMAP(Show: true);
				UC_Buildings1 = new UC_Buildings();
				UC_Buildings uC_Buildings = UC_Buildings1;
				SplitContainer2.Panel1.Controls.Add(UC_Buildings1);
				uC_Buildings.Dock = DockStyle.Fill;
				_ = null;
				SplitContainer4.Panel2Collapsed = false;
			}
			else if (Operators.CompareString(selectedNode.Name, "Relief", TextCompare: false) == 0)
			{
				ShowPanelLeft(Show: true, 300);
				ShowPanelUp(Show: false, 0);
				ShowPanelMAP(Show: true);
				UC_Relief1 = new UC_Relief();
				UC_Relief uC_Relief = UC_Relief1;
				SplitContainer2.Panel1.Controls.Add(UC_Relief1);
				uC_Relief.Dock = DockStyle.Fill;
				_ = null;
				SplitContainer4.Panel2Collapsed = false;
			}
			else if (Operators.CompareString(selectedNode.Name, "Lines", TextCompare: false) == 0)
			{
				ShowPanelLeft(Show: true, 300);
				ShowPanelUp(Show: false, 0);
				ShowPanelMAP(Show: true);
				UC_Lines1 = new UC_Lines();
				UC_Lines uC_Lines = UC_Lines1;
				SplitContainer2.Panel1.Controls.Add(UC_Lines1);
				uC_Lines.Dock = DockStyle.Fill;
				_ = null;
				SplitContainer4.Panel2Collapsed = false;
			}
			else if (Operators.CompareString(selectedNode.Name, "Antennas", TextCompare: false) == 0)
			{
				ShowPanelLeft(Show: true, 500);
				ShowPanelUp(Show: false, 0);
				ShowPanelMAP(Show: true);
				UC_Antennas1 = new UC_Antennas();
				UC_Antennas uC_Antennas = UC_Antennas1;
				SplitContainer2.Panel1.Controls.Add(UC_Antennas1);
				uC_Antennas.Dock = DockStyle.Fill;
				_ = null;
				SplitContainer4.Panel2Collapsed = false;
			}
			else if (Operators.CompareString(selectedNode.Name, "ControlPoints", TextCompare: false) == 0)
			{
				ShowPanelLeft(Show: true, 500);
				ShowPanelUp(Show: false, 0);
				ShowPanelMAP(Show: true);
				UC_ControlPoints1 = new UC_ControlPoints();
				UC_ControlPoints uC_ControlPoints = UC_ControlPoints1;
				SplitContainer2.Panel1.Controls.Add(UC_ControlPoints1);
				uC_ControlPoints.Dock = DockStyle.Fill;
				_ = null;
				SplitContainer4.Panel2Collapsed = false;
			}
			else if (Operators.CompareString(selectedNode.Name, "HorizPlanes", TextCompare: false) == 0)
			{
				ShowPanelLeft(Show: true, 350);
				ShowPanelUp(Show: false, 0);
				ShowPanelMAP(Show: true);
				UC_HorizPlanes1 = new UC_HorizPlanes();
				UC_HorizPlanes uC_HorizPlanes = UC_HorizPlanes1;
				SplitContainer2.Panel1.Controls.Add(UC_HorizPlanes1);
				uC_HorizPlanes.Dock = DockStyle.Fill;
				_ = null;
				SplitContainer4.Panel2Collapsed = false;
			}
			else if (Operators.CompareString(selectedNode.Name, "VertPlanes", TextCompare: false) == 0)
			{
				ShowPanelLeft(Show: true, 1600);
				ShowPanelUp(Show: false, 0);
				ShowPanelMAP(Show: false);
				UC_VertPlanes1 = new UC_VertPlanes();
				UC_VertPlanes uC_VertPlanes = UC_VertPlanes1;
				SplitContainer2.Panel1.Controls.Add(UC_VertPlanes1);
				uC_VertPlanes.Dock = DockStyle.Fill;
				_ = null;
				SplitContainer4.Panel2Collapsed = true;
			}
			else if (Operators.CompareString(selectedNode.Name, "Tables", TextCompare: false) == 0)
			{
				ShowPanelLeft(Show: true, 700);
				ShowPanelUp(Show: false, 0);
				ShowPanelMAP(Show: false);
				UC_Tables1 = new UC_Tables();
				UC_Tables uC_Tables = UC_Tables1;
				SplitContainer2.Panel1.Controls.Add(UC_Tables1);
				uC_Tables.Dock = DockStyle.Fill;
				_ = null;
				SplitContainer4.Panel2Collapsed = true;
			}
			else if (Operators.CompareString(selectedNode.Name, "Report", TextCompare: false) == 0)
			{
				ShowPanelLeft(Show: true, 1000);
				ShowPanelUp(Show: false, 0);
				ShowPanelMAP(Show: false);
				UC_Report1 = new UC_Report();
				UC_Report uC_Report = UC_Report1;
				SplitContainer2.Panel1.Controls.Add(UC_Report1);
				uC_Report.Dock = DockStyle.Fill;
				_ = null;
				SplitContainer4.Panel2Collapsed = true;
			}
			else if (Operators.CompareString(selectedNode.Name, "Setup", TextCompare: false) == 0)
			{
				ShowPanelLeft(Show: true, 800);
				ShowPanelUp(Show: false, 0);
				ShowPanelMAP(Show: false);
				UC_Setup1 = new UC_Setup();
				UC_Setup uC_Setup = UC_Setup1;
				SplitContainer2.Panel1.Controls.Add(UC_Setup1);
				uC_Setup.Dock = DockStyle.Fill;
				_ = null;
				SplitContainer4.Panel2Collapsed = true;
			}
			else if (Operators.CompareString(selectedNode.Name, "CAD", TextCompare: false) == 0)
			{
				ShowPanelLeft(Show: true, 1600);
				ShowPanelUp(Show: false, 0);
				ShowPanelMAP(Show: false);
				UC_CAD1 = new UC_CAD();
				UC_CAD uC_CAD = UC_CAD1;
				SplitContainer2.Panel1.Controls.Add(UC_CAD1);
				uC_CAD.Dock = DockStyle.Fill;
				_ = null;
				SplitContainer4.Panel2Collapsed = true;
			}
			else if (Operators.CompareString(selectedNode.Name, "3D", TextCompare: false) == 0)
			{
				ShowPanelLeft(Show: true, 1600);
				ShowPanelUp(Show: false, 0);
				ShowPanelMAP(Show: false);
				UC_3D1 = new UC_3D();
				UC_3D uC_3D = UC_3D1;
				SplitContainer2.Panel1.Controls.Add(UC_3D1);
				uC_3D.Dock = DockStyle.Fill;
				_ = null;
				SplitContainer4.Panel2Collapsed = true;
			}
			else
			{
				ShowPanelLeft(Show: false, 0);
				ShowPanelUp(Show: false, 0);
				ShowPanelMAP(Show: true);
				SplitContainer4.Panel2Collapsed = true;
			}
		}
	}

	private void TV_AfterSelect(object sender, TreeViewEventArgs e)
	{
		ShowPanels();
	}

	public void ShowPanelLeft(bool Show, short Width)
	{
		if (Show)
		{
			if (Width != 1600)
			{
				SplitContainer2.Panel1Collapsed = false;
				SplitContainer2.Panel1.Controls.Clear();
				SplitContainer2.SplitterDistance = Width;
				SplitContainer2.Panel2Collapsed = false;
			}
			else
			{
				SplitContainer2.Panel1Collapsed = false;
				SplitContainer2.Panel1.Controls.Clear();
				SplitContainer2.Panel2Collapsed = true;
			}
		}
		else
		{
			SplitContainer2.Panel1Collapsed = true;
			SplitContainer2.Panel1.Controls.Clear();
		}
	}

	public void ShowPanelUp(bool Show, short Width)
	{
		if (Show)
		{
			SplitContainer3.Panel1Collapsed = false;
			SplitContainer3.Panel1.Controls.Clear();
			SplitContainer3.SplitterDistance = Width;
		}
		else
		{
			SplitContainer3.Panel1Collapsed = true;
			SplitContainer3.Panel1.Controls.Clear();
		}
	}

	public void ShowPanelMAP(bool Show)
	{
		if (Show)
		{
			SplitContainer3.Panel2Collapsed = false;
		}
		else
		{
			SplitContainer3.Panel2Collapsed = true;
		}
	}

	private void cmdNEW_Click(object sender, EventArgs e)
	{
		UC_Validation();
		Form1_Public.RulerOn = false;
		cmdRuler.BackColor = SystemColors.Control;
		if (Interaction.MsgBox(Localization.LocMSG(96), MsgBoxStyle.YesNo) != MsgBoxResult.Yes)
		{
			return;
		}
		Public2.NewPrj();
		Public2.ProjectFileName = "";
		Text = "EMF Planner 7.1 Trial " + Public2.ProjectFileName;
		if (Operators.CompareString(Form1_Public.NodeName, "PRTO", TextCompare: false) == 0)
		{
			if (!Information.IsNothing(UC_PRTO1))
			{
				UC_PRTO1.FillDGV1();
			}
		}
		else
		{
			TV.SelectedNode = TV.Nodes["PRTO"];
		}
		Form1_Public.IndexTX = 0;
		UC_PRTO1.SelRow = 0;
		UC_PRTO1.RefreshRightPanel();
		FlagRedrawMap = false;
		TScmbURL.Text = Public2.PRJ.SETT.NameTileServer;
		TScmbZOOM.Text = Conversions.ToString(Public2.PRJ.SETT.ZOOM);
		ShowCheckBoxes();
		string nameTileServer = Public2.PRJ.SETT.NameTileServer;
		ref byte projection = ref Public2.PRJ.SETT.Projection;
		int PROJECTION = projection;
		CalcTileWeb.LoadTileServer(nameTileServer, ref PROJECTION, ref NameInCacheTileServer, ref ExtentionTileServer, ref UrlTileServer);
		projection = checked((byte)PROJECTION);
		FlagRedrawMap = true;
		TileX = Public2.PRJ.SETT.TileX;
		TileY = Public2.PRJ.SETT.TileY;
		PointX = Public2.PRJ.SETT.PointX;
		PointY = Public2.PRJ.SETT.PointY;
		DrawPIC();
		Form1_Public.SaveProject(Module1.FolderTMP + "\\file1.tmp");
	}

	private void cmdSaveAS_Click(object sender, EventArgs e)
	{
		UC_Validation();
		SFD1.Filter = "Project files (*.xemf)|*.xemf";
		if (Operators.CompareString(Public2.AppSetup.FolderProject, "", TextCompare: false) != 0)
		{
			SFD1.InitialDirectory = Public2.AppSetup.FolderProject;
		}
		if (SFD1.ShowDialog() == DialogResult.OK)
		{
			Public2.AppSetup.FolderProject = Microsoft.VisualBasic.FileIO.FileSystem.GetFileInfo(SFD1.FileName).DirectoryName;
			Public2.PRJ.SETT.TileX = TileX;
			Public2.PRJ.SETT.TileY = TileY;
			Public2.PRJ.SETT.PointX = PointX;
			Public2.PRJ.SETT.PointY = PointY;
			try
			{
				Public2.ProjectFileName = SFD1.FileName;
				Form1_Public.SaveProject(Public2.ProjectFileName);
				Text = "EMF Planner 7.1 Trial " + Public2.ProjectFileName;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Interaction.MsgBox(ex2.Message);
				ProjectData.ClearProjectError();
			}
			SortLastFile();
			Form1_Public.SaveProject(Module1.FolderTMP + "\\file1.tmp");
		}
	}

	private void cmdSAVE_Click(object sender, EventArgs e)
	{
		UC_Validation();
		if (Information.IsNothing(Public2.ProjectFileName) | (Operators.CompareString(Public2.ProjectFileName, "", TextCompare: false) == 0))
		{
			cmdSaveAS.PerformClick();
			return;
		}
		Public2.PRJ.SETT.TileX = TileX;
		Public2.PRJ.SETT.TileY = TileY;
		Public2.PRJ.SETT.PointX = PointX;
		Public2.PRJ.SETT.PointY = PointY;
		try
		{
			Form1_Public.SaveProject(Public2.ProjectFileName);
			Text = "EMF Planner 7.1 Trial " + Public2.ProjectFileName;
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Interaction.MsgBox(ex2.Message);
			ProjectData.ClearProjectError();
		}
		Form1_Public.SaveProject(Module1.FolderTMP + "\\file1.tmp");
	}

	private void cmdOPEN_Click(object sender, EventArgs e)
	{
		UC_Validation();
		Form1_Public.RulerOn = false;
		cmdRuler.BackColor = SystemColors.Control;
		if (ZaprosNaSohr())
		{
			FlagMSG = Interaction.MsgBox(Localization.LocMSG(101), MsgBoxStyle.YesNoCancel);
			if (Operators.ConditionalCompareObjectEqual(FlagMSG, MsgBoxResult.Yes, TextCompare: false))
			{
				cmdSAVE.PerformClick();
			}
			else if (Operators.ConditionalCompareObjectEqual(FlagMSG, MsgBoxResult.Cancel, TextCompare: false))
			{
				return;
			}
		}
		OFD1.Filter = "Project files (*.xemf)|*.xemf";
		if (Operators.CompareString(Public2.AppSetup.FolderProject, "", TextCompare: false) != 0)
		{
			OFD1.InitialDirectory = Public2.AppSetup.FolderProject;
		}
		if (OFD1.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		Public2.AppSetup.FolderProject = Microsoft.VisualBasic.FileIO.FileSystem.GetFileInfo(OFD1.FileName).DirectoryName;
		try
		{
			Public2.ProjectFileName = OFD1.FileName;
			Form1_Public.OpenProject(Public2.ProjectFileName);
			Text = "EMF Planner 7.1 Trial " + Public2.ProjectFileName;
			if (Operators.CompareString(Form1_Public.NodeName, "PRTO", TextCompare: false) == 0)
			{
				if (!Information.IsNothing(UC_PRTO1))
				{
					UC_PRTO1.FillDGV1();
				}
			}
			else
			{
				TV.SelectedNode = TV.Nodes["PRTO"];
			}
			Form1_Public.IndexTX = 0;
			UC_PRTO1.SelRow = 0;
			UC_PRTO1.RefreshRightPanel();
			FlagRedrawMap = false;
			TScmbURL.Text = Public2.PRJ.SETT.NameTileServer;
			TScmbZOOM.Text = Conversions.ToString(Public2.PRJ.SETT.ZOOM);
			ShowCheckBoxes();
			string nameTileServer = Public2.PRJ.SETT.NameTileServer;
			ref byte projection = ref Public2.PRJ.SETT.Projection;
			int PROJECTION = projection;
			CalcTileWeb.LoadTileServer(nameTileServer, ref PROJECTION, ref NameInCacheTileServer, ref ExtentionTileServer, ref UrlTileServer);
			projection = checked((byte)PROJECTION);
			FlagRedrawMap = true;
			TileX = Public2.PRJ.SETT.TileX;
			TileY = Public2.PRJ.SETT.TileY;
			PointX = Public2.PRJ.SETT.PointX;
			PointY = Public2.PRJ.SETT.PointY;
			DrawPIC();
			Triangul.CalcTriangulate();
			Form1_Public.VPSelRow = 0;
			Form1_Public.CPGSelRow = 0;
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Interaction.MsgBox(ex2.Message);
			ProjectData.ClearProjectError();
		}
		SortLastFile();
		Form1_Public.SaveProject(Module1.FolderTMP + "\\file1.tmp");
	}

	private void cmdEXIT_Click(object sender, EventArgs e)
	{
		Close();
	}

	public void SortLastFile()
	{
		if (Operators.CompareString(mnuLastFile1.Text, Public2.ProjectFileName, TextCompare: false) != 0)
		{
			if (Operators.CompareString(mnuLastFile2.Text, Public2.ProjectFileName, TextCompare: false) == 0)
			{
				mnuLastFile2.Text = mnuLastFile1.Text;
				mnuLastFile1.Text = Public2.ProjectFileName;
			}
			else if (Operators.CompareString(mnuLastFile3.Text, Public2.ProjectFileName, TextCompare: false) == 0)
			{
				mnuLastFile3.Text = mnuLastFile2.Text;
				mnuLastFile2.Text = mnuLastFile1.Text;
				mnuLastFile1.Text = Public2.ProjectFileName;
			}
			else if (Operators.CompareString(mnuLastFile1.Text, "", TextCompare: false) == 0)
			{
				mnuLastFile1.Text = Public2.ProjectFileName;
				mnuLastFile1.Visible = true;
			}
			else if (Operators.CompareString(mnuLastFile2.Text, "", TextCompare: false) == 0)
			{
				mnuLastFile2.Text = mnuLastFile1.Text;
				mnuLastFile1.Text = Public2.ProjectFileName;
				mnuLastFile2.Visible = true;
			}
			else if (Operators.CompareString(mnuLastFile3.Text, "", TextCompare: false) == 0)
			{
				mnuLastFile3.Text = mnuLastFile2.Text;
				mnuLastFile2.Text = mnuLastFile1.Text;
				mnuLastFile1.Text = Public2.ProjectFileName;
				mnuLastFile3.Visible = true;
			}
			else
			{
				mnuLastFile3.Text = mnuLastFile2.Text;
				mnuLastFile2.Text = mnuLastFile1.Text;
				mnuLastFile1.Text = Public2.ProjectFileName;
			}
		}
		StreamWriter streamWriter = new StreamWriter(Public2.AppDataFolder + "\\LastFiles.tmp", append: false, Encoding.GetEncoding(1251));
		streamWriter.WriteLine(mnuLastFile1.Text);
		streamWriter.WriteLine(mnuLastFile2.Text);
		streamWriter.WriteLine(mnuLastFile3.Text);
		streamWriter.Close();
	}

	public bool ZaprosNaSohr()
	{
		Form1_Public.SaveProject(Module1.FolderTMP + "\\file2.tmp");
		return !Module1.FileCompare(Module1.FolderTMP + "\\file1.tmp", Module1.FolderTMP + "\\file2.tmp");
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	private void mnuLastFile1_Click(object sender, EventArgs e)
	{
		UC_Validation();
		Form1_Public.RulerOn = false;
		cmdRuler.BackColor = SystemColors.Control;
		if (Operators.CompareString(mnuLastFile1.Text, "", TextCompare: false) == 0)
		{
			Interaction.MsgBox(Localization.LocMSG(97), MsgBoxStyle.Exclamation);
			return;
		}
		if (Operators.CompareString(Microsoft.VisualBasic.FileSystem.Dir(mnuLastFile1.Text), "", TextCompare: false) == 0)
		{
			Interaction.MsgBox(Localization.LocMSG(97), MsgBoxStyle.Exclamation);
			return;
		}
		if (ZaprosNaSohr())
		{
			FlagMSG = Interaction.MsgBox(Localization.LocMSG(101), MsgBoxStyle.YesNoCancel);
			if (Operators.ConditionalCompareObjectEqual(FlagMSG, MsgBoxResult.Yes, TextCompare: false))
			{
				cmdSAVE.PerformClick();
			}
			else if (Operators.ConditionalCompareObjectEqual(FlagMSG, MsgBoxResult.Cancel, TextCompare: false))
			{
				return;
			}
		}
		Public2.ProjectFileName = mnuLastFile1.Text;
		Public2.AppSetup.FolderProject = Microsoft.VisualBasic.FileIO.FileSystem.GetFileInfo(Public2.ProjectFileName).DirectoryName;
		Public2.NewPrj();
		Form1_Public.OpenProject(Public2.ProjectFileName);
		Text = "EMF Planner 7.1 Trial " + Public2.ProjectFileName;
		if (Operators.CompareString(Form1_Public.NodeName, "PRTO", TextCompare: false) == 0)
		{
			if (!Information.IsNothing(UC_PRTO1))
			{
				UC_PRTO1.FillDGV1();
			}
		}
		else
		{
			TV.SelectedNode = TV.Nodes["PRTO"];
		}
		Form1_Public.IndexTX = 0;
		UC_PRTO1.SelRow = 0;
		UC_PRTO1.RefreshRightPanel();
		FlagRedrawMap = false;
		TScmbURL.Text = Public2.PRJ.SETT.NameTileServer;
		TScmbZOOM.Text = Conversions.ToString(Public2.PRJ.SETT.ZOOM);
		ShowCheckBoxes();
		string nameTileServer = Public2.PRJ.SETT.NameTileServer;
		ref byte projection = ref Public2.PRJ.SETT.Projection;
		int PROJECTION = projection;
		CalcTileWeb.LoadTileServer(nameTileServer, ref PROJECTION, ref NameInCacheTileServer, ref ExtentionTileServer, ref UrlTileServer);
		projection = checked((byte)PROJECTION);
		FlagRedrawMap = true;
		TileX = Public2.PRJ.SETT.TileX;
		TileY = Public2.PRJ.SETT.TileY;
		PointX = Public2.PRJ.SETT.PointX;
		PointY = Public2.PRJ.SETT.PointY;
		DrawPIC();
		Triangul.CalcTriangulate();
		Form1_Public.VPSelRow = 0;
		Form1_Public.CPGSelRow = 0;
		SortLastFile();
		Form1_Public.SaveProject(Module1.FolderTMP + "\\file1.tmp");
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	private void mnuLastFile2_Click(object sender, EventArgs e)
	{
		UC_Validation();
		Form1_Public.RulerOn = false;
		cmdRuler.BackColor = SystemColors.Control;
		if (Operators.CompareString(mnuLastFile2.Text, "", TextCompare: false) == 0)
		{
			Interaction.MsgBox(Localization.LocMSG(97), MsgBoxStyle.Exclamation);
			return;
		}
		if (Operators.CompareString(Microsoft.VisualBasic.FileSystem.Dir(mnuLastFile2.Text), "", TextCompare: false) == 0)
		{
			Interaction.MsgBox(Localization.LocMSG(97), MsgBoxStyle.Exclamation);
			return;
		}
		if (ZaprosNaSohr())
		{
			FlagMSG = Interaction.MsgBox(Localization.LocMSG(101), MsgBoxStyle.YesNoCancel);
			if (Operators.ConditionalCompareObjectEqual(FlagMSG, MsgBoxResult.Yes, TextCompare: false))
			{
				cmdSAVE.PerformClick();
			}
			else if (Operators.ConditionalCompareObjectEqual(FlagMSG, MsgBoxResult.Cancel, TextCompare: false))
			{
				return;
			}
		}
		Public2.ProjectFileName = mnuLastFile2.Text;
		Public2.AppSetup.FolderProject = Microsoft.VisualBasic.FileIO.FileSystem.GetFileInfo(Public2.ProjectFileName).DirectoryName;
		Public2.NewPrj();
		Form1_Public.OpenProject(Public2.ProjectFileName);
		Text = "EMF Planner 7.1 Trial " + Public2.ProjectFileName;
		if (Operators.CompareString(Form1_Public.NodeName, "PRTO", TextCompare: false) == 0)
		{
			if (!Information.IsNothing(UC_PRTO1))
			{
				UC_PRTO1.FillDGV1();
			}
		}
		else
		{
			TV.SelectedNode = TV.Nodes["PRTO"];
		}
		Form1_Public.IndexTX = 0;
		UC_PRTO1.SelRow = 0;
		UC_PRTO1.RefreshRightPanel();
		FlagRedrawMap = false;
		TScmbURL.Text = Public2.PRJ.SETT.NameTileServer;
		TScmbZOOM.Text = Conversions.ToString(Public2.PRJ.SETT.ZOOM);
		ShowCheckBoxes();
		string nameTileServer = Public2.PRJ.SETT.NameTileServer;
		ref byte projection = ref Public2.PRJ.SETT.Projection;
		int PROJECTION = projection;
		CalcTileWeb.LoadTileServer(nameTileServer, ref PROJECTION, ref NameInCacheTileServer, ref ExtentionTileServer, ref UrlTileServer);
		projection = checked((byte)PROJECTION);
		FlagRedrawMap = true;
		TileX = Public2.PRJ.SETT.TileX;
		TileY = Public2.PRJ.SETT.TileY;
		PointX = Public2.PRJ.SETT.PointX;
		PointY = Public2.PRJ.SETT.PointY;
		DrawPIC();
		Triangul.CalcTriangulate();
		Form1_Public.VPSelRow = 0;
		Form1_Public.CPGSelRow = 0;
		SortLastFile();
		Form1_Public.SaveProject(Module1.FolderTMP + "\\file1.tmp");
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	private void mnuLastFile3_Click(object sender, EventArgs e)
	{
		UC_Validation();
		Form1_Public.RulerOn = false;
		cmdRuler.BackColor = SystemColors.Control;
		if (Operators.CompareString(mnuLastFile3.Text, "", TextCompare: false) == 0)
		{
			Interaction.MsgBox(Localization.LocMSG(97), MsgBoxStyle.Exclamation);
			return;
		}
		if (Operators.CompareString(Microsoft.VisualBasic.FileSystem.Dir(mnuLastFile3.Text), "", TextCompare: false) == 0)
		{
			Interaction.MsgBox(Localization.LocMSG(97), MsgBoxStyle.Exclamation);
			return;
		}
		if (ZaprosNaSohr())
		{
			FlagMSG = Interaction.MsgBox(Localization.LocMSG(101), MsgBoxStyle.YesNoCancel);
			if (Operators.ConditionalCompareObjectEqual(FlagMSG, MsgBoxResult.Yes, TextCompare: false))
			{
				cmdSAVE.PerformClick();
			}
			else if (Operators.ConditionalCompareObjectEqual(FlagMSG, MsgBoxResult.Cancel, TextCompare: false))
			{
				return;
			}
		}
		Public2.ProjectFileName = mnuLastFile3.Text;
		Public2.AppSetup.FolderProject = Microsoft.VisualBasic.FileIO.FileSystem.GetFileInfo(Public2.ProjectFileName).DirectoryName;
		Public2.NewPrj();
		Form1_Public.OpenProject(Public2.ProjectFileName);
		Text = "EMF Planner 7.1 Trial " + Public2.ProjectFileName;
		if (Operators.CompareString(Form1_Public.NodeName, "PRTO", TextCompare: false) == 0)
		{
			if (!Information.IsNothing(UC_PRTO1))
			{
				UC_PRTO1.FillDGV1();
			}
		}
		else
		{
			TV.SelectedNode = TV.Nodes["PRTO"];
		}
		Form1_Public.IndexTX = 0;
		UC_PRTO1.SelRow = 0;
		UC_PRTO1.RefreshRightPanel();
		FlagRedrawMap = false;
		TScmbURL.Text = Public2.PRJ.SETT.NameTileServer;
		TScmbZOOM.Text = Conversions.ToString(Public2.PRJ.SETT.ZOOM);
		ShowCheckBoxes();
		string nameTileServer = Public2.PRJ.SETT.NameTileServer;
		ref byte projection = ref Public2.PRJ.SETT.Projection;
		int PROJECTION = projection;
		CalcTileWeb.LoadTileServer(nameTileServer, ref PROJECTION, ref NameInCacheTileServer, ref ExtentionTileServer, ref UrlTileServer);
		projection = checked((byte)PROJECTION);
		FlagRedrawMap = true;
		TileX = Public2.PRJ.SETT.TileX;
		TileY = Public2.PRJ.SETT.TileY;
		PointX = Public2.PRJ.SETT.PointX;
		PointY = Public2.PRJ.SETT.PointY;
		DrawPIC();
		Triangul.CalcTriangulate();
		Form1_Public.VPSelRow = 0;
		Form1_Public.CPGSelRow = 0;
		SortLastFile();
		Form1_Public.SaveProject(Module1.FolderTMP + "\\file1.tmp");
	}

	public void UC_Validation()
	{
		try
		{
			if (!Information.IsNothing(UC_PRTO1))
			{
				UC_PRTO1.gb1.Focus();
			}
			if (!Information.IsNothing(UC_ControlPoints1))
			{
				UC_ControlPoints1.GroupBox1.Focus();
			}
			if (!Information.IsNothing(UC_Setup1))
			{
				UC_Setup1.GroupBox1.Focus();
			}
			if (!Information.IsNothing(UC_Antennas1))
			{
				UC_Antennas1.GroupBox1.Focus();
			}
			if (!Information.IsNothing(UC_VertPlanes1))
			{
				UC_VertPlanes1.GroupBox1.Focus();
			}
			if (!Information.IsNothing(UC_HorizPlanes1))
			{
				UC_HorizPlanes1.GroupBox1.Focus();
			}
			if (!Information.IsNothing(UC_Tables1))
			{
				UC_Tables1.GroupBox1.Focus();
			}
			if (!Information.IsNothing(UC_Report1))
			{
				UC_Report1.GroupBox1.Focus();
			}
			if (!Information.IsNothing(UC_Limits1))
			{
				UC_Limits1.GroupBox1.Focus();
			}
			if (!Information.IsNothing(UC_Origin1))
			{
				UC_Origin1.GroupBox1.Focus();
			}
			if (!Information.IsNothing(UC_Buildings1))
			{
				UC_Buildings1.GroupBox1.Focus();
			}
			if (!Information.IsNothing(UC_Relief1))
			{
				UC_Relief1.GroupBox1.Focus();
			}
			if (!Information.IsNothing(UC_Lines1))
			{
				UC_Lines1.GroupBox1.Focus();
			}
			if (!Information.IsNothing(UC_Raster1))
			{
				UC_Raster1.GroupBox1.Focus();
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}

	private void cmdAbout_Click(object sender, EventArgs e)
	{
		MyProject.Forms.frmAbout.ShowDialog();
	}

	private void cmdUserManual_Click(object sender, EventArgs e)
	{
		Process.Start("https://www.wireless-planning.com/emfplanner");
	}

	private void cmdCheckForUpdates_Click(object sender, EventArgs e)
	{
		UpdateMessage.LoadUpdateMessage1();
	}

	private void cmdPurchase_Click(object sender, EventArgs e)
	{
		Process.Start("https://www.puglieseprogettazioni.it");
	}

	private void cmdActivation_Click(object sender, EventArgs e)
	{
	}

	private void cmdRuler_Click(object sender, EventArgs e)
	{
		if (Form1_Public.RulerOn)
		{
			Form1_Public.RulerOn = false;
			cmdRuler.BackColor = SystemColors.Control;
		}
		else
		{
			Form1_Public.RulerOn = true;
			cmdRuler.BackColor = SystemColors.ControlDark;
			Public2.CoordWebMerc screenCenterWM = Form1_Public.GetScreenCenterWM();
			float num = (float)(25.0 / CalcCoords.CalcMashtWMtoMeters());
			Form1_Public.Ruler[0].Xwm = screenCenterWM.Xwm - (double)num;
			Form1_Public.Ruler[0].Ywm = screenCenterWM.Ywm;
			Form1_Public.Ruler[1].Xwm = screenCenterWM.Xwm + (double)num;
			Form1_Public.Ruler[1].Ywm = screenCenterWM.Ywm;
		}
		PB.Refresh();
	}

	private void cmdCleanOutside_Click(object sender, EventArgs e)
	{
		if (Interaction.MsgBox(Localization.LocMSG(98), MsgBoxStyle.YesNo) != MsgBoxResult.Yes)
		{
			return;
		}
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.BLD))
			{
				for (int i = Public2.PRJ.BLD.GetUpperBound(0); i >= 0; i += -1)
				{
					bool flag = true;
					int upperBound = Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0);
					for (int j = 0; j <= upperBound; j++)
					{
						if (Form1_Public.ShowPointOnMap(Public2.PRJ.BLD[i].CoordWM[j]))
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						eRemove(ref Public2.PRJ.BLD, i);
					}
				}
				if (Public2.PRJ.BLD.Length == 0)
				{
					Public2.PRJ.BLD = null;
				}
			}
			if (!Information.IsNothing(Public2.PRJ.ROAD))
			{
				for (int k = Public2.PRJ.ROAD.GetUpperBound(0); k >= 0; k += -1)
				{
					bool flag2 = true;
					int upperBound2 = Public2.PRJ.ROAD[k].CoordWM.GetUpperBound(0);
					for (int l = 0; l <= upperBound2; l++)
					{
						if (Form1_Public.ShowPointOnMap(Public2.PRJ.ROAD[k].CoordWM[l]))
						{
							flag2 = false;
							break;
						}
					}
					if (flag2)
					{
						eRemove(ref Public2.PRJ.ROAD, k);
					}
				}
				if (Public2.PRJ.ROAD.Length == 0)
				{
					Public2.PRJ.ROAD = null;
				}
			}
			if (!Information.IsNothing(Public2.PRJ.FENCE))
			{
				for (int m = Public2.PRJ.FENCE.GetUpperBound(0); m >= 0; m += -1)
				{
					bool flag3 = true;
					int upperBound3 = Public2.PRJ.FENCE[m].CoordWM.GetUpperBound(0);
					for (int n = 0; n <= upperBound3; n++)
					{
						if (Form1_Public.ShowPointOnMap(Public2.PRJ.FENCE[m].CoordWM[n]))
						{
							flag3 = false;
							break;
						}
					}
					if (flag3)
					{
						eRemove(ref Public2.PRJ.FENCE, m);
					}
				}
				if (Public2.PRJ.FENCE.Length == 0)
				{
					Public2.PRJ.FENCE = null;
				}
			}
			if (!Information.IsNothing(Public2.PRJ.TXT))
			{
				for (int num = Public2.PRJ.TXT.GetUpperBound(0); num >= 0; num += -1)
				{
					if (!Form1_Public.ShowPointOnMap(Public2.PRJ.TXT[num].CoordWM))
					{
						eRemove(ref Public2.PRJ.TXT, num);
					}
				}
				if (Public2.PRJ.TXT.Length == 0)
				{
					Public2.PRJ.TXT = null;
				}
			}
			if (!Information.IsNothing(Public2.PRJ.ElLN))
			{
				for (int num2 = Public2.PRJ.ElLN.GetUpperBound(0); num2 >= 0; num2 += -1)
				{
					bool flag4 = true;
					int upperBound4 = Public2.PRJ.ElLN[num2].CoordWM.GetUpperBound(0);
					for (int num3 = 0; num3 <= upperBound4; num3++)
					{
						if (Form1_Public.ShowPointOnMap(Public2.PRJ.ElLN[num2].CoordWM[num3]))
						{
							flag4 = false;
							break;
						}
					}
					if (flag4)
					{
						eRemove(ref Public2.PRJ.ElLN, num2);
					}
				}
				if (Public2.PRJ.ElLN.Length == 0)
				{
					Public2.PRJ.ElLN = null;
				}
			}
			if (!Information.IsNothing(Public2.PRJ.ElPT))
			{
				for (int num4 = Public2.PRJ.ElPT.GetUpperBound(0); num4 >= 0; num4 += -1)
				{
					if (!Form1_Public.ShowPointOnMap(Public2.PRJ.ElPT[num4].CoordWM))
					{
						eRemove(ref Public2.PRJ.ElPT, num4);
					}
				}
				if (Public2.PRJ.ElPT.Length == 0)
				{
					Public2.PRJ.ElPT = null;
				}
			}
			PB.Refresh();
		}
	}

	private void cmdCleanOutsideArea_Click(object sender, EventArgs e)
	{
		if (Interaction.MsgBox(Localization.LocMSG(99), MsgBoxStyle.YesNo) != MsgBoxResult.Yes)
		{
			return;
		}
		Public2.CoordWebMerc[] array = new Public2.CoordWebMerc[5];
		array[0].Xwm = Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm;
		array[0].Ywm = Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm;
		array[1].Xwm = Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm;
		array[1].Ywm = Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm;
		array[2].Xwm = Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm;
		array[2].Ywm = Public2.PRJ.SETT.HorAreaCoordWM[1].Ywm;
		array[3].Xwm = Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm;
		array[3].Ywm = Public2.PRJ.SETT.HorAreaCoordWM[1].Ywm;
		array[4].Xwm = Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm;
		array[4].Ywm = Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm;
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.BLD))
			{
				for (int i = Public2.PRJ.BLD.GetUpperBound(0); i >= 0; i += -1)
				{
					if (!RepWordAlias.CrossPolygon(Public2.PRJ.BLD[i].CoordWM, array))
					{
						eRemove(ref Public2.PRJ.BLD, i);
					}
				}
				if (Public2.PRJ.BLD.Length == 0)
				{
					Public2.PRJ.BLD = null;
				}
			}
			if (!Information.IsNothing(Public2.PRJ.ROAD))
			{
				for (int j = Public2.PRJ.ROAD.GetUpperBound(0); j >= 0; j += -1)
				{
					if (!RepWordAlias.CrossPolygon(Public2.PRJ.ROAD[j].CoordWM, array))
					{
						eRemove(ref Public2.PRJ.ROAD, j);
					}
				}
				if (Public2.PRJ.ROAD.Length == 0)
				{
					Public2.PRJ.ROAD = null;
				}
			}
			if (!Information.IsNothing(Public2.PRJ.FENCE))
			{
				for (int k = Public2.PRJ.FENCE.GetUpperBound(0); k >= 0; k += -1)
				{
					if (!RepWordAlias.CrossPolygon(Public2.PRJ.FENCE[k].CoordWM, array))
					{
						eRemove(ref Public2.PRJ.FENCE, k);
					}
				}
				if (Public2.PRJ.FENCE.Length == 0)
				{
					Public2.PRJ.FENCE = null;
				}
			}
			if (!Information.IsNothing(Public2.PRJ.TXT))
			{
				for (int l = Public2.PRJ.TXT.GetUpperBound(0); l >= 0; l += -1)
				{
					if (!Form1_Public.PointIsideArea(Public2.PRJ.TXT[l].CoordWM))
					{
						eRemove(ref Public2.PRJ.TXT, l);
					}
				}
				if (Public2.PRJ.TXT.Length == 0)
				{
					Public2.PRJ.TXT = null;
				}
			}
			PB.Refresh();
		}
	}

	private void eRemove<T>(ref T[] ar, int ii)
	{
		int num = ar.Length;
		checked
		{
			if (ii >= 0 && ii <= num - 1)
			{
				Array.Copy(ar, ii + 1, ar, ii, ar.GetUpperBound(0) - ii);
				ar = (T[])Utils.CopyArray(ar, new T[num - 2 + 1]);
			}
		}
	}

	private void PB_Paint(object sender, PaintEventArgs e)
	{
		e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
		e.Graphics.InterpolationMode = InterpolationMode.Bicubic;
		e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
		if (Operators.CompareString(Form1_Public.NodeName, "Origin", TextCompare: false) == 0)
		{
			DrawLines(e);
			DrawBuildings(e);
			DrawElevation(e);
			DrawHorizontalPlanes(e);
			DrawHorizontalArea(e);
			DrawAntennas(e);
			DrawControlPoints(e);
			DrawOrigin(e);
			DrawRuler(e);
		}
		else if (Operators.CompareString(Form1_Public.NodeName, "Raster", TextCompare: false) == 0)
		{
			DrawRasterScaling(e);
			DrawRuler(e);
		}
		else if ((Operators.CompareString(Form1_Public.NodeName, "Antennas", TextCompare: false) == 0) | (Operators.CompareString(Form1_Public.NodeName, "ControlPoints", TextCompare: false) == 0))
		{
			DrawLines(e);
			DrawBuildings(e);
			DrawElevation(e);
			DrawOrigin(e);
			DrawHorizontalPlanes(e);
			DrawHorizontalArea(e);
			DrawAntennas(e);
			DrawControlPoints(e);
			DrawConcentricCircles(e);
			DrawRuler(e);
		}
		else if (Operators.CompareString(Form1_Public.NodeName, "HorizPlanes", TextCompare: false) == 0)
		{
			DrawLines(e);
			DrawBuildings(e);
			DrawElevation(e);
			DrawOrigin(e);
			DrawHorizontalPlanes(e);
			DrawHorizontalArea(e);
			DrawVertPlineLines(e);
			DrawAntennas(e);
			DrawControlPoints(e);
			DrawConcentricCircles(e);
			DrawRuler(e);
		}
		else if (Operators.CompareString(Form1_Public.NodeName, "Buildings", TextCompare: false) == 0)
		{
			DrawLines(e);
			DrawElevation(e);
			DrawOrigin(e);
			DrawHorizontalPlanes(e);
			DrawHorizontalArea(e);
			DrawAntennas(e);
			DrawControlPoints(e);
			DrawBuildings(e);
			DrawRuler(e);
		}
		else if (Operators.CompareString(Form1_Public.NodeName, "Lines", TextCompare: false) == 0)
		{
			DrawBuildings(e);
			DrawElevation(e);
			DrawOrigin(e);
			DrawHorizontalPlanes(e);
			DrawHorizontalArea(e);
			DrawAntennas(e);
			DrawControlPoints(e);
			DrawLines(e);
			DrawRuler(e);
		}
		else if (Operators.CompareString(Form1_Public.NodeName, "Relief", TextCompare: false) == 0)
		{
			DrawLines(e);
			DrawBuildings(e);
			DrawOrigin(e);
			DrawHorizontalPlanes(e);
			DrawHorizontalArea(e);
			DrawAntennas(e);
			DrawControlPoints(e);
			DrawElevation(e);
			DrawTriangles(e);
			DrawRuler(e);
		}
	}

	public void DrawPIC()
	{
		SemStackTiles.WaitOne();
		PB.Image = new Bitmap(PB.Width, PB.Height);
		G = Graphics.FromImage(PB.Image);
		PB.Location = new Point(PointX, PointY);
		checked
		{
			if (Public2.PRJ.SETT.ShowMap)
			{
				FlagShowingTiles = true;
				int upperBound = ShowingTile.GetUpperBound(0);
				string NameTileCACH = default;
				string NameTileURL = default;
				string NameTileDir = default;
				for (int i = 0; i <= upperBound; i++)
				{
					int upperBound2 = ShowingTile.GetUpperBound(1);
					for (int j = 0; j <= upperBound2; j++)
					{
						int num = TileX + i;
						int num2 = TileY + j;
						CalcTileWeb.CalcNameTile(num, num2, Conversions.ToString(Public2.PRJ.SETT.ZOOM), NameInCacheTileServer, ExtentionTileServer, UrlTileServer, ref NameTileCACH, ref NameTileURL, ref NameTileDir);
						ShowingTile[i, j] = NameTileCACH;
						bool flag = false;
						int upperBound3 = StackTiles.GetUpperBound(0);
						for (int k = 1; k <= upperBound3; k++)
						{
							if (Operators.CompareString(StackTiles[k], NameTileCACH, TextCompare: false) == 0)
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							continue;
						}
						if (MyProject.Computer.FileSystem.FileExists(NameTileCACH))
						{
							try
							{
								G.DrawImage(Module1.CBrightness2(new Bitmap(NameTileCACH), Public2.PRJ.SETT.MapBrightness), i * 256, j * 256, 256, 256);
							}
							catch (Exception projectError)
							{
								ProjectData.SetProjectError(projectError);
								try
								{
									File.Delete(NameTileCACH);
								}
								catch (Exception projectError2)
								{
									ProjectData.SetProjectError(projectError2);
									ProjectData.ClearProjectError();
								}
								ProjectData.ClearProjectError();
							}
						}
						else if (!flag)
						{
							if (Operators.CompareString(NameTileURL, "SITPLAN", TextCompare: false) != 0 && !MyProject.Computer.FileSystem.DirectoryExists(NameTileDir))
							{
								Microsoft.VisualBasic.FileSystem.MkDir(NameTileDir);
							}
							ref string[] stackTiles = ref StackTiles;
							stackTiles = (string[])Utils.CopyArray(stackTiles, new string[StackTiles.GetUpperBound(0) + 1 + 1]);
							StackTiles[StackTiles.GetUpperBound(0)] = NameTileCACH;
							Thread thread = new Thread(DownloadAndDraw);
							RequestParam requestParam = new RequestParam(NameTileURL, NameTileCACH, num, num2, Public2.PRJ.SETT.ZOOM);
							thread.Start(requestParam);
						}
					}
				}
				FlagShowingTiles = false;
			}
			SemStackTiles.Release();
			GC.Collect();
		}
	}

	public void DownloadAndDraw(object obj)
	{
		RequestParam requestParam = ((obj != null) ? ((RequestParam)obj) : default(RequestParam));
		string nameTileURL = requestParam.NameTileURL;
		string nameTileCache = requestParam.NameTileCache;
		if (Operators.CompareString(nameTileURL, "SITPLAN", TextCompare: false) != 0)
		{
			MyWebClient myWebClient = new MyWebClient();
			myWebClient.timeout = 10000;
			myWebClient.UseDefaultCredentials = true;
			WebProxy webProxy = new WebProxy();
			if (Public2.AppSetup.FlagProxy)
			{
				if (Public2.AppSetup.FlagProxyLogin)
				{
					webProxy.UseDefaultCredentials = false;
					webProxy.Credentials = new NetworkCredential(Public2.AppSetup.ProxyLogin, Public2.AppSetup.ProxyPassword);
				}
				else
				{
					webProxy.UseDefaultCredentials = true;
				}
				Uri address = new Uri("http://" + Public2.AppSetup.ProxyIP + ":" + Conversions.ToString(Public2.AppSetup.ProxyPort));
				webProxy.Address = address;
				myWebClient.Proxy = webProxy;
			}
			else
			{
				myWebClient.Proxy = WebRequest.GetSystemWebProxy();
			}
			myWebClient.Headers.Add("User-Agent", "Referer: openstreetmap");
			try
			{
				myWebClient.DownloadFile(nameTileURL, nameTileCache);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				ProjectData.ClearProjectError();
			}
			myWebClient.Dispose();
		}
		else
		{
			CalcTileWeb.CropeOneTileFromRaster(requestParam.X, requestParam.Y, requestParam.Z);
		}
		SemStackTiles.WaitOne();
		if (MyProject.Computer.FileSystem.FileExists(nameTileCache))
		{
			if (Microsoft.VisualBasic.FileSystem.FileLen(nameTileCache) == 0L)
			{
				File.Delete(nameTileCache);
			}
			if (nameTileURL.Contains("melda") && Microsoft.VisualBasic.FileSystem.FileLen(nameTileCache) == 3343)
			{
				File.Delete(nameTileCache);
			}
		}
		int upperBound = StackTiles.GetUpperBound(0);
		int num = upperBound;
		checked
		{
			for (int i = 1; i <= num; i++)
			{
				if (Operators.CompareString(StackTiles[i], nameTileCache, TextCompare: false) != 0)
				{
					continue;
				}
				if (i < upperBound)
				{
					int num2 = i;
					int num3 = upperBound - 1;
					for (int j = num2; j <= num3; j++)
					{
						StackTiles[j] = StackTiles[j + 1];
					}
				}
				ref string[] stackTiles = ref StackTiles;
				stackTiles = (string[])Utils.CopyArray(stackTiles, new string[upperBound - 1 + 1]);
				break;
			}
			SemStackTiles.Release();
			int upperBound2 = ShowingTile.GetUpperBound(0);
			for (int k = 0; k <= upperBound2; k++)
			{
				int upperBound3 = ShowingTile.GetUpperBound(1);
				for (int l = 0; l <= upperBound3; l++)
				{
					try
					{
						if (Operators.CompareString(ShowingTile[k, l], nameTileCache, TextCompare: false) == 0)
						{
							DrawPicOneTile(nameTileCache, k, l);
						}
					}
					catch (Exception projectError)
					{
						ProjectData.SetProjectError(projectError);
						ProjectData.ClearProjectError();
					}
				}
			}
		}
	}

	public void DrawPicOneTile(string NameTileCache, int X, int Y)
	{
		checked
		{
			try
			{
				if (PB.InvokeRequired)
				{
					Invoke(new Del1(DrawPicOneTile), NameTileCache, X, Y);
					return;
				}
				try
				{
					G.DrawImage(Module1.CBrightness2(new Bitmap(NameTileCache), Public2.PRJ.SETT.MapBrightness), X * 256, Y * 256, 256, 256);
					PB.Refresh();
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					ProjectData.ClearProjectError();
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		}
	}

	private void PB_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Middle)
		{
			MapMoving = true;
			MouseDownX = e.X;
			MouseDownY = e.Y;
		}
		else
		{
			if (e.Button != MouseButtons.Left)
			{
				return;
			}
			if (Form1_Public.RulerOn)
			{
				RulerPick(e);
			}
			if (FlagRulerPick)
			{
				return;
			}
			if (Operators.CompareString(Form1_Public.NodeName, "Origin", TextCompare: false) == 0)
			{
				OriginPick(e);
				if (!FlagOriginPick)
				{
					HorizontalAreaPick(e);
				}
			}
			else if (Operators.CompareString(Form1_Public.NodeName, "Raster", TextCompare: false) == 0)
			{
				RasterScalingPick(e);
			}
			else if (Operators.CompareString(Form1_Public.NodeName, "Antennas", TextCompare: false) == 0)
			{
				AntennaPick(e);
			}
			else if (Operators.CompareString(Form1_Public.NodeName, "ControlPoints", TextCompare: false) == 0)
			{
				ControlPointPick(e);
			}
			else if (Operators.CompareString(Form1_Public.NodeName, "HorizPlanes", TextCompare: false) == 0)
			{
				HorizontalAreaPick(e);
				VertPlanePick(e);
			}
			else if (Operators.CompareString(Form1_Public.NodeName, "Buildings", TextCompare: false) == 0)
			{
				BuildingPick(e);
			}
			else if (Operators.CompareString(Form1_Public.NodeName, "Lines", TextCompare: false) == 0)
			{
				LinePick(e);
			}
			else if (Operators.CompareString(Form1_Public.NodeName, "Relief", TextCompare: false) == 0)
			{
				ElevPick(e);
			}
		}
	}

	private void PB_MouseMove(object sender, MouseEventArgs e)
	{
		checked
		{
			if (e.Button == MouseButtons.Middle)
			{
				if (MapMoving)
				{
					PointX = PB.Location.X + (e.X - MouseDownX);
					PointY = PB.Location.Y + (e.Y - MouseDownY);
					PB.Location = new Point(PointX, PointY);
				}
				return;
			}
			if (e.Button == MouseButtons.Left)
			{
				if (Operators.CompareString(Form1_Public.NodeName, "Origin", TextCompare: false) == 0)
				{
					OriginDrag(e);
					HorizontalAreaDrag(e);
				}
				else if (Operators.CompareString(Form1_Public.NodeName, "Raster", TextCompare: false) == 0)
				{
					RasterScalingDrag(e);
				}
				else if (Operators.CompareString(Form1_Public.NodeName, "Antennas", TextCompare: false) == 0)
				{
					AntennaDrag(e);
				}
				else if (Operators.CompareString(Form1_Public.NodeName, "ControlPoints", TextCompare: false) == 0)
				{
					ControlPointDrag(e);
				}
				else if (Operators.CompareString(Form1_Public.NodeName, "HorizPlanes", TextCompare: false) == 0)
				{
					HorizontalAreaDrag(e);
				}
				else if (Operators.CompareString(Form1_Public.NodeName, "Buildings", TextCompare: false) == 0)
				{
					BuildingDrag(e);
				}
				else if (Operators.CompareString(Form1_Public.NodeName, "Lines", TextCompare: false) == 0)
				{
					LineDrag(e);
				}
				else if (Operators.CompareString(Form1_Public.NodeName, "Relief", TextCompare: false) == 0)
				{
					ElevDrag(e);
				}
				if (Form1_Public.RulerOn)
				{
					RulerDrag(e);
				}
				return;
			}
			if (Operators.CompareString(Form1_Public.NodeName, "Origin", TextCompare: false) == 0)
			{
				OriginHover(e);
			}
			else if (Operators.CompareString(Form1_Public.NodeName, "Raster", TextCompare: false) == 0)
			{
				RasterScalingHover(e);
			}
			else if (Operators.CompareString(Form1_Public.NodeName, "Antennas", TextCompare: false) == 0)
			{
				AntennaHover(e);
			}
			else if (Operators.CompareString(Form1_Public.NodeName, "ControlPoints", TextCompare: false) == 0)
			{
				ControlPointHover(e);
			}
			else if (Operators.CompareString(Form1_Public.NodeName, "HorizPlanes", TextCompare: false) == 0)
			{
				HorizontalAreaHover(e);
			}
			else if (Operators.CompareString(Form1_Public.NodeName, "Buildings", TextCompare: false) == 0)
			{
				BuildingHover(e);
			}
			else if (Operators.CompareString(Form1_Public.NodeName, "Lines", TextCompare: false) == 0)
			{
				LineHover(e);
			}
			else if (Operators.CompareString(Form1_Public.NodeName, "Relief", TextCompare: false) == 0)
			{
				ElevHover(e);
			}
			double ShirDeg = default;
			double DolgDeg = default;
			CalcTileWeb.TileToCoordNEW(Public2.PRJ.SETT.Projection, (double)TileX + (double)e.X / 256.0, (double)TileY + (double)e.Y / 256.0, Public2.PRJ.SETT.ZOOM, ref ShirDeg, ref DolgDeg);
			ShirCursor = ShirDeg;
			DolgCursor = DolgDeg;
			if (DolgCursor < 180.0)
			{
				Public2.CoordWebMerc CoordWM = default;
				CalcCoords.ConvTileToWebMercMeters((double)TileX + (double)e.X / 256.0, (double)TileY + (double)e.Y / 256.0, ref CoordWM);
				Public2.CoordMeters coordMeters = CalcCoords.ConvWebMercMetersToMetersRound(CoordWM);
				lblCoordFromOrigin.Text = "X=" + Conversions.ToString(coordMeters.Xm) + "m  Y=" + Conversions.ToString(coordMeters.Ym) + "m";
				double LatWgs = default;
				double LonWgs = default;
				CalcCoords.ConvWebMercMetersToWGS84(CoordWM, ref LatWgs, ref LonWgs);
				lblCOORD.Text = CalcCoords.ConvLatToStr(LatWgs) + " " + CalcCoords.ConvLonToStr(LonWgs);
				lblHeight.Text = Conversions.ToString(Math.Round(Triangul.FindHeightTriang(CoordWM.Xwm, CoordWM.Ywm), 1)) + "m";
			}
			else
			{
				lblCoordFromOrigin.Text = "";
				lblCOORD.Text = "";
				lblHeight.Text = "";
			}
		}
	}

	private void PB_MouseUp(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			if (Operators.CompareString(Form1_Public.NodeName, "Origin", TextCompare: false) == 0)
			{
				OriginDrop(e);
				HorizontalAreaDrop(e);
			}
			else if (Operators.CompareString(Form1_Public.NodeName, "Raster", TextCompare: false) == 0)
			{
				RasterScalingDrop(e);
			}
			else if (Operators.CompareString(Form1_Public.NodeName, "Antennas", TextCompare: false) == 0)
			{
				AntennaDrop(e);
			}
			else if (Operators.CompareString(Form1_Public.NodeName, "ControlPoints", TextCompare: false) == 0)
			{
				ControlPointDrop(e);
			}
			else if (Operators.CompareString(Form1_Public.NodeName, "HorizPlanes", TextCompare: false) == 0)
			{
				HorizontalAreaDrop(e);
			}
			else if (Operators.CompareString(Form1_Public.NodeName, "Buildings", TextCompare: false) == 0)
			{
				BuildingDrop(e);
			}
			else if (Operators.CompareString(Form1_Public.NodeName, "Lines", TextCompare: false) == 0)
			{
				LineDrop(e);
			}
			else if (Operators.CompareString(Form1_Public.NodeName, "Relief", TextCompare: false) == 0)
			{
				ElevDrop(e);
			}
			if (Form1_Public.RulerOn)
			{
				RulerDrop(e);
			}
		}
		checked
		{
			if (e.Button == MouseButtons.Right)
			{
				if (Operators.CompareString(Form1_Public.NodeName, "Buildings", TextCompare: false) == 0)
				{
					BuildingEndCreating();
				}
				else if (Operators.CompareString(Form1_Public.NodeName, "Lines", TextCompare: false) == 0)
				{
					LineEndCreating();
				}
				else if (Operators.CompareString(Form1_Public.NodeName, "Relief", TextCompare: false) == 0)
				{
					ElevEndCreating();
				}
				else if (Operators.CompareString(Form1_Public.NodeName, "ControlPoints", TextCompare: false) == 0)
				{
					ContCP.Show(PB, e.Location);
					Xmap = (short)e.X;
					Ymap = (short)e.Y;
				}
				else if (Operators.CompareString(Form1_Public.NodeName, "Antennas", TextCompare: false) == 0)
				{
					ContANT.Show(PB, e.Location);
					Xmap = (short)e.X;
					Ymap = (short)e.Y;
				}
				else if (Operators.CompareString(Form1_Public.NodeName, "Origin", TextCompare: false) == 0)
				{
					ContOrigin.Show(PB, e.Location);
					Xmap = (short)e.X;
					Ymap = (short)e.Y;
				}
			}
			bool mapMoving = MapMoving;
			if (e.Button == MouseButtons.Middle)
			{
				MapMoving = false;
			}
			int num = (int)Math.Round((double)TileX - Math.Ceiling((double)PointX / 256.0));
			int num2 = (int)Math.Round((double)TileY - Math.Ceiling((double)PointY / 256.0));
			if ((num != TileX) | (num2 != TileY))
			{
				TileX = num;
				TileY = num2;
				if (PointX > 0)
				{
					PointX = (int)Math.Round((double)PointX - 256.0 * Math.Ceiling((double)PointX / 256.0));
				}
				if (PointY > 0)
				{
					PointY = (int)Math.Round((double)PointY - 256.0 * Math.Ceiling((double)PointY / 256.0));
				}
				if (PointX < -256)
				{
					PointX = (int)Math.Round((double)PointX - 256.0 * Math.Ceiling((double)PointX / 256.0));
				}
				if (PointY < -256)
				{
					PointY = (int)Math.Round((double)PointY - 256.0 * Math.Ceiling((double)PointY / 256.0));
				}
				DrawPIC();
			}
			Form1_Public.SetScreenBorders();
			if (mapMoving)
			{
				PB.Refresh();
			}
			Panel1.Focus();
		}
	}

	private void Form1_MouseWheel(object sender, MouseEventArgs e)
	{
		if (FlagMouseInPB)
		{
			Timer1.Stop();
			ref int wHEEL = ref WHEEL;
			wHEEL = checked((int)Math.Round((double)wHEEL + (double)e.Delta / 120.0));
			Timer1.Start();
		}
	}

	private void Timer1_Tick(object sender, EventArgs e)
	{
		Timer1.Stop();
		if (Control.MouseButtons != MouseButtons.None)
		{
			WHEEL = 0;
			return;
		}
		Public2.PRJ.SETT.ZOOM = checked(Public2.PRJ.SETT.ZOOM + WHEEL);
		if (Public2.PRJ.SETT.ZOOM > 20)
		{
			Public2.PRJ.SETT.ZOOM = 20;
		}
		if (Public2.PRJ.SETT.ZOOM < 3)
		{
			Public2.PRJ.SETT.ZOOM = 3;
		}
		TScmbZOOM.Text = Conversions.ToString(Public2.PRJ.SETT.ZOOM);
		WHEEL = 0;
	}

	private void TScmbZOOM_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (!FlagRedrawMap)
		{
			return;
		}
		Panel1.Focus();
		Public2.PRJ.SETT.ZOOM = Conversions.ToInteger(TScmbZOOM.Text);
		int PicX = default;
		int PicY = default;
		int TileX = default;
		int TileY = default;
		CalcTileWeb.CoordToTile(Public2.PRJ.SETT.Projection, ShirCursor, DolgCursor, Public2.PRJ.SETT.ZOOM, ref TileX, ref TileY, ref PicX, ref PicY);
		checked
		{
			this.TileX = (int)Math.Round((double)TileX - Math.Ceiling((double)Panel1.Width / 256.0 / 2.0));
			this.TileY = (int)Math.Round((double)TileY - Math.Ceiling((double)Panel1.Height / 256.0 / 2.0));
			PointX = (int)Math.Round((double)(-PicX) - Math.Ceiling((double)Panel1.Width / 256.0 / 2.0) * 256.0 + (double)Panel1.Width / 2.0);
			PointY = (int)Math.Round((double)(-PicY) - Math.Ceiling((double)Panel1.Height / 256.0 / 2.0) * 256.0 + (double)Panel1.Height / 2.0);
			TileX = (int)Math.Round((double)this.TileX - Math.Ceiling((double)PointX / 256.0));
			TileY = (int)Math.Round((double)this.TileY - Math.Ceiling((double)PointY / 256.0));
			if ((TileX != this.TileX) | (TileY != this.TileY))
			{
				this.TileX = TileX;
				this.TileY = TileY;
				if (PointX > 0)
				{
					PointX = (int)Math.Round((double)PointX - 256.0 * Math.Ceiling((double)PointX / 256.0));
				}
				if (PointY > 0)
				{
					PointY = (int)Math.Round((double)PointY - 256.0 * Math.Ceiling((double)PointY / 256.0));
				}
				if (PointX < -256)
				{
					PointX = (int)Math.Round((double)PointX - 256.0 * Math.Ceiling((double)PointX / 256.0));
				}
				if (PointY < -256)
				{
					PointY = (int)Math.Round((double)PointY - 256.0 * Math.Ceiling((double)PointY / 256.0));
				}
			}
			DrawPIC();
			Form1_Public.SetScreenBorders();
			PB.Refresh();
		}
	}

	private void PB_MouseEnter(object sender, EventArgs e)
	{
		FlagMouseInPB = true;
	}

	private void PB_MouseLeave(object sender, EventArgs e)
	{
		FlagMouseInPB = false;
		lblCOORD.Text = "";
		lblCoordFromOrigin.Text = "";
	}

	private void TScmbURL_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (!FlagRedrawMap)
		{
			return;
		}
		MenuStrip1.Focus();
		double ShirShag = default;
		double DolgShag = default;
		double ShirDeg = default;
		double DolgDeg = default;
		CalcTileWeb.TileToCoord(Public2.PRJ.SETT.Projection, this.TileX, this.TileY, Public2.PRJ.SETT.ZOOM, ref ShirDeg, ref DolgDeg, ref ShirShag, ref DolgShag);
		ShirDeg += ShirShag * ((double)Panel1.Height / 2.0 - (double)PB.Top);
		DolgDeg += DolgShag * ((double)Panel1.Width / 2.0 - (double)PB.Left);
		Public2.PRJ.SETT.NameTileServer = TScmbURL.Text;
		string nameTileServer = Public2.PRJ.SETT.NameTileServer;
		ref byte projection = ref Public2.PRJ.SETT.Projection;
		int PROJECTION = projection;
		CalcTileWeb.LoadTileServer(nameTileServer, ref PROJECTION, ref NameInCacheTileServer, ref ExtentionTileServer, ref UrlTileServer);
		checked
		{
			projection = (byte)PROJECTION;
			int PicX = default;
			int PicY = default;
			int TileX = default;
			int TileY = default;
			CalcTileWeb.CoordToTile(Public2.PRJ.SETT.Projection, ShirDeg, DolgDeg, Public2.PRJ.SETT.ZOOM, ref TileX, ref TileY, ref PicX, ref PicY);
			this.TileX = (int)Math.Round((double)TileX - Math.Ceiling((double)Panel1.Width / 256.0 / 2.0));
			this.TileY = (int)Math.Round((double)TileY - Math.Ceiling((double)Panel1.Height / 256.0 / 2.0));
			PointX = (int)Math.Round((double)(-PicX) - Math.Ceiling((double)Panel1.Width / 256.0 / 2.0) * 256.0 + (double)Panel1.Width / 2.0);
			PointY = (int)Math.Round((double)(-PicY) - Math.Ceiling((double)Panel1.Height / 256.0 / 2.0) * 256.0 + (double)Panel1.Height / 2.0);
			TileX = (int)Math.Round((double)this.TileX - Math.Ceiling((double)PointX / 256.0));
			TileY = (int)Math.Round((double)this.TileY - Math.Ceiling((double)PointY / 256.0));
			if ((TileX != this.TileX) | (TileY != this.TileY))
			{
				this.TileX = TileX;
				this.TileY = TileY;
				if (PointX > 0)
				{
					PointX = (int)Math.Round((double)PointX - 256.0 * Math.Ceiling((double)PointX / 256.0));
				}
				if (PointY > 0)
				{
					PointY = (int)Math.Round((double)PointY - 256.0 * Math.Ceiling((double)PointY / 256.0));
				}
				if (PointX < -256)
				{
					PointX = (int)Math.Round((double)PointX - 256.0 * Math.Ceiling((double)PointX / 256.0));
				}
				if (PointY < -256)
				{
					PointY = (int)Math.Round((double)PointY - 256.0 * Math.Ceiling((double)PointY / 256.0));
				}
			}
			DrawPIC();
			PB.Refresh();
		}
	}

	public void LoadTScmbURL()
	{
		TScmbURL.Items.Clear();
		TScmbURL.Items.Add("Calibrated map image");
		StreamReader streamReader = new StreamReader(Public2.AppDataFolder + "\\CustomBaseMaps.set", Encoding.GetEncoding(1251));
		while (!streamReader.EndOfStream)
		{
			string text = streamReader.ReadLine();
			if (Strings.Len(Strings.Trim(text)) > 7)
			{
				short length = checked((short)Strings.InStr(text, "\t"));
				string expression = text.Substring(0, length);
				expression = Strings.Replace(expression, "\t", "");
				TScmbURL.Items.Add(expression);
			}
		}
		streamReader.Close();
	}

	public void MapPositionWGS84(double Lat, double Lon)
	{
		int PicX = default;
		int PicY = default;
		int TileX = default;
		int TileY = default;
		CalcTileWeb.CoordToTile(Public2.PRJ.SETT.Projection, Lat, Lon, Public2.PRJ.SETT.ZOOM, ref TileX, ref TileY, ref PicX, ref PicY);
		checked
		{
			this.TileX = (int)Math.Round((double)TileX - Math.Ceiling((double)Panel1.Width / 512.0));
			this.TileY = (int)Math.Round((double)TileY - Math.Ceiling((double)Panel1.Height / 512.0));
			PointX = (int)Math.Round((double)(-PicX) - Math.Ceiling((double)Panel1.Width / 512.0) * 256.0 + (double)Panel1.Width / 2.0);
			PointY = (int)Math.Round((double)(-PicY) - Math.Ceiling((double)Panel1.Height / 512.0) * 256.0 + (double)Panel1.Height / 2.0);
			TileX = (int)Math.Round((double)this.TileX - Math.Ceiling((double)PointX / 256.0));
			TileY = (int)Math.Round((double)this.TileY - Math.Ceiling((double)PointY / 256.0));
			if ((TileX != this.TileX) | (TileY != this.TileY))
			{
				this.TileX = TileX;
				this.TileY = TileY;
				if (PointX > 0)
				{
					PointX = (int)Math.Round((double)PointX - 256.0 * Math.Ceiling((double)PointX / 256.0));
				}
				if (PointY > 0)
				{
					PointY = (int)Math.Round((double)PointY - 256.0 * Math.Ceiling((double)PointY / 256.0));
				}
				if (PointX <= -256)
				{
					PointX = (int)Math.Round((double)PointX - 256.0 * Math.Ceiling((double)PointX / 256.0));
				}
				if (PointY <= -256)
				{
					PointY = (int)Math.Round((double)PointY - 256.0 * Math.Ceiling((double)PointY / 256.0));
				}
			}
			Form1_Public.SetScreenBorders();
			DrawPIC();
			PB.Refresh();
		}
	}

	public void MapPositionWM(double Xwm, double Ywm)
	{
		Public2.CoordWebMerc coordWM = default;
		coordWM.Xwm = Xwm;
		coordWM.Ywm = Ywm;
		double Xt = default;
		double Yt = default;
		CalcCoords.ConvWebMercMetersToTile(coordWM, ref Xt, ref Yt);
		checked
		{
			int num = (int)Math.Floor(Xt);
			int num2 = (int)Math.Floor(Yt);
			int num3 = (int)Math.Round((Xt - Math.Floor(Xt)) * 256.0);
			int num4 = (int)Math.Round((Yt - Math.Floor(Yt)) * 256.0);
			TileX = (int)Math.Round((double)num - Math.Ceiling((double)Panel1.Width / 512.0));
			TileY = (int)Math.Round((double)num2 - Math.Ceiling((double)Panel1.Height / 512.0));
			PointX = (int)Math.Round((double)(-num3) - Math.Ceiling((double)Panel1.Width / 512.0) * 256.0 + (double)Panel1.Width / 2.0);
			PointY = (int)Math.Round((double)(-num4) - Math.Ceiling((double)Panel1.Height / 512.0) * 256.0 + (double)Panel1.Height / 2.0);
			Form1_Public.SetScreenBorders();
			DrawPIC();
			PB.Refresh();
		}
	}

	private void Panel1_Resize(object sender, EventArgs e)
	{
		try
		{
			Form1_Public.SetScreenBorders();
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}

	private void cmdNewCP_Click(object sender, EventArgs e)
	{
		if ((UC_ControlPoints1.SelRowDGV1 == -1) | Information.IsNothing(Public2.PRJ.CPG))
		{
			Interaction.MsgBox(Localization.LocMSG(52), MsgBoxStyle.Critical);
			return;
		}
		Public2.CoordWebMerc CoordWM = default;
		CalcCoords.ConvTileToWebMercMeters((double)TileX + (double)Xmap / 256.0, (double)TileY + (double)Ymap / 256.0, ref CoordWM);
		checked
		{
			if (Information.IsNothing(Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP))
			{
				Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP = new Public2.ControlPoint[1];
			}
			else
			{
				ref Public2.ControlPoint[] cP = ref Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP;
				cP = (Public2.ControlPoint[])Utils.CopyArray(cP, new Public2.ControlPoint[Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP.GetUpperBound(0) + 1 + 1]);
			}
			Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP[Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP.GetUpperBound(0)].CoordWM = CoordWM;
			short num = -1;
			if (!Information.IsNothing(Public2.PRJ.BLD))
			{
				int upperBound = Public2.PRJ.BLD.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if (Module1.IsPointInsidePolygonF(CoordWM, Public2.PRJ.BLD[i].CoordWM))
					{
						num = (short)i;
						break;
					}
				}
			}
			if (num == -1)
			{
				Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP[Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP.GetUpperBound(0)].Location = "Ground";
				Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP[Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP.GetUpperBound(0)].H = new Public2.ControlPointHeight[1];
				Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP[Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP.GetUpperBound(0)].H[0].H = 2f;
			}
			else
			{
				Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP[Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP.GetUpperBound(0)].Location = "Building";
				Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP[Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP.GetUpperBound(0)].Name = Localization.LocMSG(55) + " " + Public2.PRJ.BLD[num].Text;
				if (Public2.PRJ.BLD[num].H > 5f)
				{
					Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP[Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP.GetUpperBound(0)].H = new Public2.ControlPointHeight[2];
					Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP[Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP.GetUpperBound(0)].H[0].H = Public2.PRJ.BLD[num].H - 1f;
					Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP[Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP.GetUpperBound(0)].H[0].Name = Localization.LocMSG(57);
					Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP[Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP.GetUpperBound(0)].H[0].L = 15f;
					Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP[Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP.GetUpperBound(0)].H[1].H = Public2.PRJ.BLD[num].H + 2f;
					Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP[Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP.GetUpperBound(0)].H[1].Name = Localization.LocMSG(58);
				}
				else
				{
					Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP[Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP.GetUpperBound(0)].H = new Public2.ControlPointHeight[1];
					Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP[Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP.GetUpperBound(0)].H[0].H = 2f;
				}
			}
			UC_ControlPoints1.FlagPaintAllow = false;
			UC_ControlPoints1.FillDGV2();
			UC_ControlPoints1.FlagPaintAllow = true;
			UC_ControlPoints1.DGV2.CurrentCell = UC_ControlPoints1.DGV2[0, Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP.GetUpperBound(0)];
			Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP[Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP.GetUpperBound(0)].CoordM = CalcCoords.ConvWebMercMetersToMetersRound(Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP[Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP.GetUpperBound(0)].CoordWM);
		}
	}

	private void cmdMoveCP_Click(object sender, EventArgs e)
	{
		if ((UC_ControlPoints1.SelRowDGV1 == -1) | Information.IsNothing(Public2.PRJ.CPG))
		{
			Interaction.MsgBox(Localization.LocMSG(52), MsgBoxStyle.Critical);
			return;
		}
		if (UC_ControlPoints1.SelRowDGV2 < 0)
		{
			Interaction.MsgBox(Localization.LocMSG(45), MsgBoxStyle.Critical);
			return;
		}
		Public2.CoordWebMerc CoordWM = default;
		CalcCoords.ConvTileToWebMercMeters((double)TileX + (double)Xmap / 256.0, (double)TileY + (double)Ymap / 256.0, ref CoordWM);
		Public2.PRJ.CPG[UC_ControlPoints1.SelRowDGV1].CP[UC_ControlPoints1.SelRowDGV2].CoordWM = CoordWM;
		PB.Refresh();
		short selRowDGV = UC_ControlPoints1.SelRowDGV2;
		UC_ControlPoints1.FlagPaintAllow = false;
		UC_ControlPoints1.FillDGV1();
		UC_ControlPoints1.FlagPaintAllow = true;
		UC_ControlPoints1.DGV2.CurrentCell = UC_ControlPoints1.DGV2[0, selRowDGV];
	}

	private void cmdMoveANT_Click(object sender, EventArgs e)
	{
		if (Information.IsNothing(Public2.PRJ.TX) | (UC_Antennas1.DGV1.SelectedRows.Count == 0))
		{
			Interaction.MsgBox(Localization.LocMSG(100), MsgBoxStyle.Critical);
			return;
		}
		Public2.CoordWebMerc CoordWM = default;
		CalcCoords.ConvTileToWebMercMeters((double)TileX + (double)Xmap / 256.0, (double)TileY + (double)Ymap / 256.0, ref CoordWM);
		checked
		{
			int num = UC_Antennas1.DGV1.SelectedRows.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				Public2.PRJ.TX[UC_Antennas1.DGV1.SelectedRows[i].Index].CoordWM = CoordWM;
			}
			PB.Refresh();
			UC_Antennas1.FillDGV1();
		}
	}

	private void cmdSetOrigin_Click(object sender, EventArgs e)
	{
		Public2.CoordWebMerc CoordWM = default;
		CalcCoords.ConvTileToWebMercMeters((double)TileX + (double)Xmap / 256.0, (double)TileY + (double)Ymap / 256.0, ref CoordWM);
		Public2.PRJ.SETT.OriginCoordWM = CoordWM;
		if (Public2.PRJ.SETT.OriginCoordWM.Xwm > 20037508.343)
		{
			Public2.PRJ.SETT.OriginCoordWM.Xwm = Public2.PRJ.SETT.OriginCoordWM.Xwm - 40075016.686;
		}
		CalcCoords.ConvWebMercMetersToWGS84(Public2.PRJ.SETT.OriginCoordWM, ref Public2.PRJ.SETT.OriginCoordWGS.Lat, ref Public2.PRJ.SETT.OriginCoordWGS.Lon);
		CalcCoords.ConvScaleWebMercMetersToMeters();
		CalcCoords.ConvAllTxWebMercMetersToMeters();
		CalcCoords.ConvAllControlPointsWebMercMetersToMeters();
		CalcCoords.ConvAllVertPlanesWebMercMetersToMeters();
		UC_Origin1.FillCoords();
		PB.Refresh();
	}

	public void DrawAntennas(PaintEventArgs e)
	{
		if (!Public2.PRJ.SETT.ShowAntennas || Information.IsNothing(Public2.PRJ.TX))
		{
			return;
		}
		checked
		{
			bool[] array = new bool[Public2.PRJ.TX.GetUpperBound(0) + 1];
			int upperBound = Public2.PRJ.TX.GetUpperBound(0);
			string text = default;
			for (int i = 0; i <= upperBound; i++)
			{
				if (!((Operators.CompareString(Form1_Public.NodeName, "Antennas", TextCompare: false) == 0) | Public2.PRJ.TX[i].Active))
				{
					continue;
				}
				CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.TX[i].CoordWM, ref tmpTileX1, ref tmpTileY1);
				LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
				LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
				float num = Public2.PRJ.TX[i].Az;
				if (num < 0f)
				{
					num = 0f;
				}
				if (Public2.PRJ.SETT.ShowAntennaAzimuth)
				{
					ArrowAz((int)LineX1, (int)LineY1, num, e);
				}
				if (Public2.PRJ.SETT.ShowLongAzimuth)
				{
					LongLineAz((int)LineX1, (int)LineY1, num, e);
				}
				if ((Operators.CompareString(Form1_Public.NodeName, "Antennas", TextCompare: false) == 0) & Public2.PRJ.TX[i].Selected)
				{
					e.Graphics.DrawImage(Resources.bs_mark_red_, LineX1 - 7, LineY1 - 7);
				}
				else
				{
					e.Graphics.FillEllipse(new SolidBrush(Public2.PRJ.TX[i].Color), LineX1 - 4, LineY1 - 4, 8f, 8f);
					e.Graphics.DrawImage(Resources.bs_mark_transparent, LineX1 - 7, LineY1 - 7);
				}
				if (!Public2.PRJ.SETT.ShowAntennaLabels)
				{
					continue;
				}
				bool flag = false;
				if (!array[i])
				{
					text = Conversions.ToString(i + 1);
					array[i] = true;
					flag = true;
				}
				int num2 = i + 1;
				int upperBound2 = Public2.PRJ.TX.GetUpperBound(0);
				for (int j = num2; j <= upperBound2; j++)
				{
					if ((Public2.PRJ.TX[i].CoordWM.Xwm == Public2.PRJ.TX[j].CoordWM.Xwm) & (Public2.PRJ.TX[i].CoordWM.Ywm == Public2.PRJ.TX[j].CoordWM.Ywm) & !array[j] & ((Operators.CompareString(Form1_Public.NodeName, "Antennas", TextCompare: false) == 0) | Public2.PRJ.TX[j].Active))
					{
						text = text + "," + Conversions.ToString(j + 1);
						array[j] = true;
					}
				}
				if (flag)
				{
					e.Graphics.DrawString(text, Font8, BrushBlue, LineX1 + 6, LineY1 - 12);
				}
			}
		}
	}

	public void ArrowAz(int X1, int Y1, float Azimuth, PaintEventArgs e)
	{
		int num = 200;
		checked
		{
			int num2 = (int)Math.Round((double)X1 + (double)num * Math.Sin((double)Azimuth / (180.0 / Math.PI)));
			int num3 = (int)Math.Round((double)Y1 - (double)num * Math.Cos((double)Azimuth / (180.0 / Math.PI)));
			int num4 = (int)Math.Round((double)(X1 + num2) / 2.0);
			int num5 = (int)Math.Round((double)(Y1 + num3) / 2.0);
			float angle = ((!((Azimuth >= 0f) & (Azimuth < 180f))) ? (Azimuth + 90f) : (Azimuth - 90f));
			e.Graphics.DrawLine(new Pen(Color.Black, 2f), X1, Y1, num2, num3);
			int x = (int)Math.Round(8.0 * Math.Sin((double)(Azimuth + 160f) / (180.0 / Math.PI)) + (double)num2);
			int y = (int)Math.Round(-8.0 * Math.Cos((double)(Azimuth + 160f) / (180.0 / Math.PI)) + (double)num3);
			e.Graphics.DrawLine(new Pen(Color.Black, 2f), num2, num3, x, y);
			x = (int)Math.Round(8.0 * Math.Sin((double)(Azimuth + 200f) / (180.0 / Math.PI)) + (double)num2);
			y = (int)Math.Round(-8.0 * Math.Cos((double)(Azimuth + 200f) / (180.0 / Math.PI)) + (double)num3);
			e.Graphics.DrawLine(new Pen(Color.Black, 2f), num2, num3, x, y);
			e.Graphics.TranslateTransform(num4, num5);
			e.Graphics.RotateTransform(angle);
			SizeF sizeF = default;
			sizeF = e.Graphics.MeasureString(Conversions.ToString(Azimuth) + "°", new Font("Arial", 10f, FontStyle.Regular));
			e.Graphics.FillRectangle(new SolidBrush(Color.White), (0f - sizeF.Width) / 2f, (0f - sizeF.Height) / 2f - 1f, sizeF.Width, sizeF.Height);
			e.Graphics.DrawString(Conversions.ToString(Azimuth) + "°", new Font("Arial", 10f, FontStyle.Regular), new SolidBrush(Color.Black), (0f - sizeF.Width) / 2f, (0f - sizeF.Height) / 2f);
			e.Graphics.ResetTransform();
		}
	}

	public void LongLineAz(int X1, int Y1, float Azimuth, PaintEventArgs e)
	{
		Pen pen = new Pen(Color.Black, 1f);
		pen.DashStyle = DashStyle.Custom;
		pen.DashPattern = new float[2] { 4f, 2f };
		int num = 2000;
		checked
		{
			int x = (int)Math.Round((double)X1 + (double)num * Math.Sin((double)Azimuth / (180.0 / Math.PI)));
			int y = (int)Math.Round((double)Y1 - (double)num * Math.Cos((double)Azimuth / (180.0 / Math.PI)));
			e.Graphics.DrawLine(pen, X1, Y1, x, y);
		}
	}

	public void DrawControlPoints(PaintEventArgs e)
	{
		if (!Public2.PRJ.SETT.ShowControlPoints || Information.IsNothing(Public2.PRJ.CPG))
		{
			return;
		}
		int upperBound = Public2.PRJ.CPG.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				if (!Public2.PRJ.CPG[i].Active || Information.IsNothing(Public2.PRJ.CPG[i].CP))
				{
					continue;
				}
				int upperBound2 = Public2.PRJ.CPG[i].CP.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.CPG[i].CP[j].CoordWM, ref tmpTileX1, ref tmpTileY1);
					LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					if ((Operators.CompareString(Form1_Public.NodeName, "ControlPoints", TextCompare: false) == 0) & Public2.PRJ.CPG[i].CP[j].Selected)
					{
						e.Graphics.DrawImage(Resources.bs_mark_red, LineX1 - 7, LineY1 - 7);
					}
					else
					{
						e.Graphics.DrawImage(Resources.ring8x8, LineX1 - 7, LineY1 - 7);
					}
					string text = Public2.PRJ.SETT.ControlPointPrefix;
					if (Public2.PRJ.SETT.ShowControlPointGroups)
					{
						text = text + Conversions.ToString(i + 1) + ".";
					}
					text += Conversions.ToString(j + 1);
					if (Public2.PRJ.SETT.ShowControlPointHeight)
					{
						text += "(";
						int upperBound3 = Public2.PRJ.CPG[i].CP[j].H.GetUpperBound(0);
						for (int k = 0; k <= upperBound3; k++)
						{
							text += Conversions.ToString(Public2.PRJ.CPG[i].CP[j].H[k].H);
							if (k < Public2.PRJ.CPG[i].CP[j].H.GetUpperBound(0))
							{
								text += ";";
							}
						}
						text += "m)";
					}
					e.Graphics.DrawString(text, Font8, BrushBlue, LineX1 + 4, LineY1 - 10);
				}
			}
		}
	}

	public void DrawOrigin(PaintEventArgs e)
	{
		checked
		{
			if (Public2.PRJ.SETT.ShowOrigin)
			{
				CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.SETT.OriginCoordWM, ref tmpTileX1, ref tmpTileY1);
				LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
				LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
				e.Graphics.DrawImage(Resources.OriginYellow, LineX1 - 15, LineY1 - 15);
				e.Graphics.DrawString(Localization.LocMSG(38), Font8, BrushBlue, LineX1 + 15, LineY1 - 15);
			}
		}
	}

	public void DrawRasterScaling(PaintEventArgs e)
	{
		checked
		{
			if (Public2.PRJ.RPL.UseScalingLine)
			{
				CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.RPL.PointMap[1], ref tmpTileX1, ref tmpTileY1);
				LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
				LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
				CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.RPL.PointMap[2], ref tmpTileX1, ref tmpTileY1);
				LineX2 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
				LineY2 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
				e.Graphics.DrawLine(new Pen(Color.Yellow, 3f), LineX1, LineY1, LineX2, LineY2);
				e.Graphics.DrawImage(Resources.ring8x8, LineX1 - 7, LineY1 - 7);
				e.Graphics.DrawImage(Resources.ring8x8, LineX2 - 7, LineY2 - 7);
				e.Graphics.DrawString(Localization.LocMSG(15), Font8, BrushBlue, LineX1 + 3, LineY1 - 12);
				e.Graphics.DrawString(Localization.LocMSG(15), Font8, BrushBlue, LineX2 + 3, LineY2 - 12);
			}
			else
			{
				CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.RPL.PointMap[0], ref tmpTileX1, ref tmpTileY1);
				LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
				LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
				e.Graphics.DrawImage(Resources.OriginYellow, LineX1 - 15, LineY1 - 15);
				e.Graphics.DrawString(Localization.LocMSG(14), Font8, BrushBlue, LineX1 + 15, LineY1 - 15);
			}
		}
	}

	public void DrawHorizontalArea(PaintEventArgs e)
	{
		double Xt = default;
		double Yt = default;
		CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.SETT.OriginCoordWM, ref Xt, ref Yt);
		checked
		{
			long num = (long)Math.Round((Xt - (double)TileX) * 256.0);
			long num2 = (long)Math.Round((Yt - (double)TileY) * 256.0);
			double Xt2 = default;
			double Yt2 = default;
			CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.SETT.HorAreaCoordWM[0], ref Xt2, ref Yt2);
			long num3 = (long)Math.Round((Xt2 - (double)TileX) * 256.0);
			long num4 = (long)Math.Round((Yt2 - (double)TileY) * 256.0);
			double Xt3 = default;
			double Yt3 = default;
			CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.SETT.HorAreaCoordWM[1], ref Xt3, ref Yt3);
			long num5 = (long)Math.Round((Xt3 - (double)TileX) * 256.0);
			long num6 = (long)Math.Round((Yt3 - (double)TileY) * 256.0);
			e.Graphics.DrawLine(PenMagenta2, num3, num4, num3, num6);
			e.Graphics.DrawLine(PenMagenta2, num5, num4, num5, num6);
			e.Graphics.DrawLine(PenMagenta2, num3, num4, num5, num4);
			e.Graphics.DrawLine(PenMagenta2, num3, num6, num5, num6);
			if (Public2.PRJ.SETT.ShowHorizPlaneScale)
			{
				SizeF sizeF = default;
				double num7 = CalcCoords.CalcMashtMeterstoPixel();
				float xmin = (float)Public2.PRJ.SETT.HorAreaCoordM[0].Xm;
				float xmax = (float)Public2.PRJ.SETT.HorAreaCoordM[1].Xm;
				short num8 = default;
				int N = num8;
				float dX = default;
				float X = default;
				Module1.RazmetkaOSI(xmin, xmax, 8, 13, ref N, ref dX, ref X);
				num8 = (short)N;
				N = num8 - 1;
				for (int i = 0; i <= N; i++)
				{
					float num9 = X + (float)i * dX;
					float num10 = (float)((double)num + (double)num9 * num7);
					e.Graphics.DrawLine(PenBlue1, num10, num4, num10, num6);
					sizeF = e.Graphics.MeasureString(Conversions.ToString(num9), Font8);
					e.Graphics.DrawString(Conversions.ToString(num9), Font8, BrushDarkBlue, num10 - sizeF.Width / 2f, num4 + 2);
				}
				e.Graphics.DrawString("X, m", Font8, BrushDarkBlue, num5 + 3, num4 - 10);
				float xmin2 = (float)Public2.PRJ.SETT.HorAreaCoordM[0].Ym;
				float xmax2 = (float)Public2.PRJ.SETT.HorAreaCoordM[1].Ym;
				int N2 = num8;
				Module1.RazmetkaOSI(xmin2, xmax2, 8, 13, ref N2, ref dX, ref X);
				num8 = (short)N2;
				N2 = num8 - 1;
				for (int j = 0; j <= N2; j++)
				{
					float num9 = X + (float)j * dX;
					float num10 = (float)((double)num2 - (double)num9 * num7);
					e.Graphics.DrawLine(PenBlue1, num3, num10, num5, num10);
					sizeF = e.Graphics.MeasureString(Conversions.ToString(num9), Font8);
					e.Graphics.DrawString(Conversions.ToString(num9), Font8, BrushDarkBlue, (float)num3 - sizeF.Width - 2f, num10 - 6f);
				}
				e.Graphics.DrawString("Y, m", Font8, BrushDarkBlue, num3 - 2, num6 - 15);
			}
		}
	}

	public void DrawBuildings(PaintEventArgs e)
	{
		if (!Public2.PRJ.SETT.ShowAllBuildings)
		{
			return;
		}
		Pen pen = new Pen(Public2.PRJ.SETT.BuildingColor, (int)Public2.PRJ.SETT.BuildingLineWidth);
		Pen pen2 = new Pen(Public2.PRJ.SETT.BuildingColor, 2f);
		Font font = new Font("Arial", (int)Public2.PRJ.SETT.BuildingTextSize, FontStyle.Regular);
		Pen pen3 = new Pen(Color.Black, 1f);
		pen3.DashStyle = DashStyle.Custom;
		pen3.DashPattern = new float[2] { 4f, 2f };
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.BLD))
			{
				int upperBound = Public2.PRJ.BLD.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if (!((Operators.CompareString(Form1_Public.NodeName, "Buildings", TextCompare: false) == 0) | Public2.PRJ.BLD[i].Show))
					{
						continue;
					}
					bool flag = false;
					if (Public2.PRJ.SETT.ZOOM < 18)
					{
						int upperBound2 = Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0);
						for (int j = 0; j <= upperBound2; j++)
						{
							if (Form1_Public.ShowPointOnMap(Public2.PRJ.BLD[i].CoordWM[j]))
							{
								flag = true;
								break;
							}
						}
					}
					else
					{
						flag = true;
					}
					if (!flag)
					{
						continue;
					}
					Point[] array = new Point[Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0) + 1];
					int upperBound3 = Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0);
					for (int k = 0; k <= upperBound3; k++)
					{
						CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.BLD[i].CoordWM[k], ref tmpTileX1, ref tmpTileY1);
						LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
						LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
						array[k].X = (int)LineX1;
						array[k].Y = (int)LineY1;
					}
					if (IndexBLD == i)
					{
						e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(100, Color.Red)), array);
						e.Graphics.DrawLines(pen, array);
						int upperBound4 = Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0);
						for (int l = 0; l <= upperBound4; l++)
						{
							CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.BLD[i].CoordWM[l], ref tmpTileX1, ref tmpTileY1);
							LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
							LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
							array[l].X = (int)LineX1;
							array[l].Y = (int)LineY1;
							e.Graphics.FillRectangle(Brushes.Black, LineX1 - 3, LineY1 - 3, 6f, 6f);
						}
					}
					else if ((Operators.CompareString(Form1_Public.NodeName, "Buildings", TextCompare: false) == 0) & !Public2.PRJ.BLD[i].Show)
					{
						e.Graphics.DrawLines(pen3, array);
					}
					else
					{
						e.Graphics.DrawLines(pen, array);
					}
					if (Public2.PRJ.SETT.ShowBuildingText | Public2.PRJ.SETT.ShowBuildingHeight | Public2.PRJ.SETT.ShowBuildingFoundation)
					{
						string text = "";
						if (Public2.PRJ.SETT.ShowBuildingText)
						{
							text = text + Public2.PRJ.BLD[i].Text + "\r\n";
						}
						if (Public2.PRJ.SETT.ShowBuildingHeight)
						{
							text = text + Conversions.ToString(Public2.PRJ.BLD[i].H) + "m\r\n";
						}
						if (Public2.PRJ.SETT.ShowBuildingFoundation)
						{
							text = text + Conversions.ToString(Public2.PRJ.BLD[i].Fundation) + "m\r\n";
						}
						text = text.Substring(0, text.Length - 1);
						CalcCoords.ConvWebMercMetersToTile(Module1.CoordCenterSign1(Public2.PRJ.BLD[i].CoordWM), ref tmpTileX1, ref tmpTileY1);
						LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
						LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
						stringSize = e.Graphics.MeasureString(text, font);
						e.Graphics.DrawString(text, font, new SolidBrush(Public2.PRJ.SETT.BuildingColor), (float)LineX1 - stringSize.Width / 2f, (float)LineY1 - stringSize.Height / 2f);
					}
				}
			}
			if (Form1_Public.StartDrawingBuilding && !Information.IsNothing(Form1_Public.NewBLD.CoordWM) && Form1_Public.NewBLD.CoordWM.GetUpperBound(0) > 0)
			{
				Point[] array2 = new Point[Form1_Public.NewBLD.CoordWM.GetUpperBound(0) + 1];
				int upperBound5 = Form1_Public.NewBLD.CoordWM.GetUpperBound(0);
				for (int m = 0; m <= upperBound5; m++)
				{
					CalcCoords.ConvWebMercMetersToTile(Form1_Public.NewBLD.CoordWM[m], ref tmpTileX1, ref tmpTileY1);
					LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					array2[m].X = (int)LineX1;
					array2[m].Y = (int)LineY1;
				}
				e.Graphics.DrawLines(pen2, array2);
			}
		}
	}

	public void DrawLines(PaintEventArgs e)
	{
		new Pen(Brushes.Black, 1f);
		new Pen(Color.Black, 1f)
		{
			DashStyle = DashStyle.Custom,
			DashPattern = new float[2] { 8f, 4f }
		};
		checked
		{
			if (Public2.PRJ.SETT.ShowRoads && !Information.IsNothing(Public2.PRJ.ROAD))
			{
				Point[][] array = new Point[Public2.PRJ.ROAD.GetUpperBound(0) + 1][];
				int upperBound = Public2.PRJ.ROAD.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					int upperBound2 = Public2.PRJ.ROAD[i].CoordWM.GetUpperBound(0);
					for (int j = 0; j <= upperBound2; j++)
					{
						if (Form1_Public.ShowPointOnMap(Public2.PRJ.ROAD[i].CoordWM[j]))
						{
							ref Point[] reference = ref array[i];
							reference = (Point[])Utils.CopyArray(reference, new Point[Public2.PRJ.ROAD[i].CoordWM.GetUpperBound(0) + 1]);
							break;
						}
					}
					if (!Information.IsNothing(array[i]))
					{
						int upperBound3 = Public2.PRJ.ROAD[i].CoordWM.GetUpperBound(0);
						for (int k = 0; k <= upperBound3; k++)
						{
							CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.ROAD[i].CoordWM[k], ref tmpTileX1, ref tmpTileY1);
							array[i][k].X = (int)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
							array[i][k].Y = (int)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
						}
					}
				}
				double num = CalcCoords.CalcMashtMeterstoPixel();
				int upperBound4 = Public2.PRJ.ROAD.GetUpperBound(0);
				for (int l = 0; l <= upperBound4; l++)
				{
					if (!Information.IsNothing(array[l]))
					{
						e.Graphics.DrawLines(new Pen(Brushes.Black, (float)((double)unchecked((int)Public2.PRJ.ROAD[l].WidthM) * num)), array[l]);
					}
				}
				int upperBound5 = Public2.PRJ.ROAD.GetUpperBound(0);
				for (int m = 0; m <= upperBound5; m++)
				{
					if (!Information.IsNothing(array[m]))
					{
						e.Graphics.DrawLines(new Pen(Brushes.White, (float)unchecked((double)(int)Public2.PRJ.ROAD[m].WidthM * num - (double)(int)Public2.PRJ.ROAD[m].WidthBorder)), array[m]);
					}
				}
				if (IndexRoad > -1 && !Information.IsNothing(array[IndexRoad]))
				{
					e.Graphics.DrawLines(PenBlack2, array[IndexRoad]);
					int upperBound6 = Public2.PRJ.ROAD[IndexRoad].CoordWM.GetUpperBound(0);
					for (int n = 0; n <= upperBound6; n++)
					{
						e.Graphics.FillRectangle(Brushes.Black, array[IndexRoad][n].X - 3, array[IndexRoad][n].Y - 3, 6, 6);
					}
				}
			}
			if (Public2.PRJ.SETT.ShowFences && !Information.IsNothing(Public2.PRJ.FENCE))
			{
				Point[][] array2 = new Point[Public2.PRJ.FENCE.GetUpperBound(0) + 1][];
				int upperBound7 = Public2.PRJ.FENCE.GetUpperBound(0);
				for (int num2 = 0; num2 <= upperBound7; num2++)
				{
					int upperBound8 = Public2.PRJ.FENCE[num2].CoordWM.GetUpperBound(0);
					for (int num3 = 0; num3 <= upperBound8; num3++)
					{
						if (Form1_Public.ShowPointOnMap(Public2.PRJ.FENCE[num2].CoordWM[num3]))
						{
							ref Point[] reference2 = ref array2[num2];
							reference2 = (Point[])Utils.CopyArray(reference2, new Point[Public2.PRJ.FENCE[num2].CoordWM.GetUpperBound(0) + 1]);
							break;
						}
					}
					if (!Information.IsNothing(array2[num2]))
					{
						int upperBound9 = Public2.PRJ.FENCE[num2].CoordWM.GetUpperBound(0);
						for (int num4 = 0; num4 <= upperBound9; num4++)
						{
							CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.FENCE[num2].CoordWM[num4], ref tmpTileX1, ref tmpTileY1);
							array2[num2][num4].X = (int)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
							array2[num2][num4].Y = (int)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
						}
					}
				}
				int upperBound10 = Public2.PRJ.FENCE.GetUpperBound(0);
				for (int num5 = 0; num5 <= upperBound10; num5++)
				{
					Public1.PenDash[Public2.PRJ.FENCE[num5].Type].Width = unchecked((int)Public2.PRJ.FENCE[num5].Width);
					if (!Information.IsNothing(array2[num5]))
					{
						e.Graphics.DrawLines(Public1.PenDash[Public2.PRJ.FENCE[num5].Type], array2[num5]);
					}
				}
				if (IndexFence > -1 && !Information.IsNothing(array2[IndexFence]))
				{
					e.Graphics.DrawLines(PenBlack2, array2[IndexFence]);
					int upperBound11 = array2[IndexFence].GetUpperBound(0);
					for (int num6 = 0; num6 <= upperBound11; num6++)
					{
						e.Graphics.FillRectangle(Brushes.Black, array2[IndexFence][num6].X - 3, array2[IndexFence][num6].Y - 3, 6, 6);
					}
				}
			}
			if (Public2.PRJ.SETT.ShowText && !Information.IsNothing(Public2.PRJ.TXT))
			{
				int upperBound12 = Public2.PRJ.TXT.GetUpperBound(0);
				for (int num7 = 0; num7 <= upperBound12; num7++)
				{
					if (!Form1_Public.ShowPointOnMap(Public2.PRJ.TXT[num7].CoordWM))
					{
						continue;
					}
					CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.TXT[num7].CoordWM, ref tmpTileX1, ref tmpTileY1);
					LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					e.Graphics.TranslateTransform(LineX1, LineY1);
					e.Graphics.RotateTransform(Public2.PRJ.TXT[num7].Angle);
					unchecked
					{
						stringSize = e.Graphics.MeasureString(Public2.PRJ.TXT[num7].Text, new Font(Public2.PRJ.TXT[num7].FontName, (int)Public2.PRJ.TXT[num7].Size, FontStyle.Regular));
						if (num7 == IndexText)
						{
							e.Graphics.FillRectangle(new SolidBrush(Color.Yellow), 0f, 0f, stringSize.Width, stringSize.Height);
						}
						e.Graphics.DrawString(Public2.PRJ.TXT[num7].Text, new Font(Public2.PRJ.TXT[num7].FontName, (int)Public2.PRJ.TXT[num7].Size, FontStyle.Regular), BrushBlack, 0f, 0f);
						if (!Information.IsNothing(Public2.PRJ.TXT[num7].Icon))
						{
							if (Public2.PRJ.TXT[num7].Point)
							{
								e.Graphics.DrawImage(Public2.PRJ.TXT[num7].Icon, -8, -8, 16, 16);
							}
						}
						else if (Public2.PRJ.TXT[num7].Point)
						{
							e.Graphics.FillEllipse(Brushes.Black, -2, -2, 4, 4);
						}
						e.Graphics.ResetTransform();
					}
				}
			}
			if (Form1_Public.StartDrawingRoad)
			{
				if (!Information.IsNothing(Form1_Public.NewRoad.CoordWM) && Form1_Public.NewRoad.CoordWM.GetUpperBound(0) > 0)
				{
					Point[] array3 = new Point[Form1_Public.NewRoad.CoordWM.GetUpperBound(0) + 1];
					int upperBound13 = Form1_Public.NewRoad.CoordWM.GetUpperBound(0);
					for (int num8 = 0; num8 <= upperBound13; num8++)
					{
						CalcCoords.ConvWebMercMetersToTile(Form1_Public.NewRoad.CoordWM[num8], ref tmpTileX1, ref tmpTileY1);
						array3[num8].X = (int)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
						array3[num8].Y = (int)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					}
					e.Graphics.DrawLines(PenBlack2, array3);
				}
			}
			else if (Form1_Public.StartDrawingFence && !Information.IsNothing(Form1_Public.NewFence.CoordWM) && Form1_Public.NewFence.CoordWM.GetUpperBound(0) > 0)
			{
				Point[] array4 = new Point[Form1_Public.NewFence.CoordWM.GetUpperBound(0) + 1];
				int upperBound14 = Form1_Public.NewFence.CoordWM.GetUpperBound(0);
				for (int num9 = 0; num9 <= upperBound14; num9++)
				{
					CalcCoords.ConvWebMercMetersToTile(Form1_Public.NewFence.CoordWM[num9], ref tmpTileX1, ref tmpTileY1);
					array4[num9].X = (int)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					array4[num9].Y = (int)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
				}
				e.Graphics.DrawLines(PenBlack2, array4);
			}
		}
	}

	public void DrawElevation(PaintEventArgs e)
	{
		if (!Public2.PRJ.SETT.ShowRelief)
		{
			return;
		}
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.ElLN))
			{
				Point[][] array = new Point[Public2.PRJ.ElLN.GetUpperBound(0) + 1][];
				int upperBound = Public2.PRJ.ElLN.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					int upperBound2 = Public2.PRJ.ElLN[i].CoordWM.GetUpperBound(0);
					for (int j = 0; j <= upperBound2; j++)
					{
						if (Form1_Public.ShowPointOnMap(Public2.PRJ.ElLN[i].CoordWM[j]))
						{
							ref Point[] reference = ref array[i];
							reference = (Point[])Utils.CopyArray(reference, new Point[Public2.PRJ.ElLN[i].CoordWM.GetUpperBound(0) + 1]);
							break;
						}
					}
					if (!Information.IsNothing(array[i]))
					{
						int upperBound3 = Public2.PRJ.ElLN[i].CoordWM.GetUpperBound(0);
						for (int k = 0; k <= upperBound3; k++)
						{
							CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.ElLN[i].CoordWM[k], ref tmpTileX1, ref tmpTileY1);
							array[i][k].X = (int)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
							array[i][k].Y = (int)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
						}
						if (Public2.PRJ.ElLN[i].CloseLine)
						{
							ref Point[] reference2 = ref array[i];
							reference2 = (Point[])Utils.CopyArray(reference2, new Point[array[i].GetUpperBound(0) + 1 + 1]);
							array[i][array[i].GetUpperBound(0)] = array[i][0];
						}
					}
				}
				int upperBound4 = Public2.PRJ.ElLN.GetUpperBound(0);
				for (int l = 0; l <= upperBound4; l++)
				{
					if (!Information.IsNothing(array[l]))
					{
						e.Graphics.DrawLines(PenBrown1, array[l]);
						int upperBound5 = array[l].GetUpperBound(0);
						for (int m = 0; m <= upperBound5; m++)
						{
							e.Graphics.DrawString(Conversions.ToString(Public2.PRJ.ElLN[l].H), Font8, Brushes.Brown, array[l][m].X, array[l][m].Y - 12);
						}
					}
				}
				if (IndexElevLine > -1 && !Information.IsNothing(array[IndexElevLine]))
				{
					e.Graphics.DrawLines(PenBlack2, array[IndexElevLine]);
					int upperBound6 = Public2.PRJ.ElLN[IndexElevLine].CoordWM.GetUpperBound(0);
					for (int n = 0; n <= upperBound6; n++)
					{
						e.Graphics.FillRectangle(Brushes.Black, array[IndexElevLine][n].X - 3, array[IndexElevLine][n].Y - 3, 6, 6);
					}
				}
			}
			if (!Information.IsNothing(Public2.PRJ.ElPT))
			{
				int upperBound7 = Public2.PRJ.ElPT.GetUpperBound(0);
				for (int num = 0; num <= upperBound7; num++)
				{
					if (Form1_Public.ShowPointOnMap(Public2.PRJ.ElPT[num].CoordWM))
					{
						CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.ElPT[num].CoordWM, ref tmpTileX1, ref tmpTileY1);
						LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
						LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
						if (num == IndexElevPoint)
						{
							e.Graphics.FillEllipse(new SolidBrush(Color.Yellow), LineX1 - 4, LineY1 - 4, 8f, 8f);
						}
						e.Graphics.FillEllipse(Brushes.Brown, LineX1 - 2, LineY1 - 2, 4f, 4f);
						e.Graphics.DrawString(Conversions.ToString(Public2.PRJ.ElPT[num].H), Font8, Brushes.Brown, LineX1, LineY1 - 12);
					}
				}
			}
			if (Form1_Public.StartDrawingElevLine && !Information.IsNothing(Form1_Public.NewElevLine.CoordWM) && Form1_Public.NewElevLine.CoordWM.GetUpperBound(0) > 0)
			{
				Point[] array2 = new Point[Form1_Public.NewElevLine.CoordWM.GetUpperBound(0) + 1];
				int upperBound8 = Form1_Public.NewElevLine.CoordWM.GetUpperBound(0);
				for (int num2 = 0; num2 <= upperBound8; num2++)
				{
					CalcCoords.ConvWebMercMetersToTile(Form1_Public.NewElevLine.CoordWM[num2], ref tmpTileX1, ref tmpTileY1);
					array2[num2].X = (int)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					array2[num2].Y = (int)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
				}
				e.Graphics.DrawLines(PenBlack2, array2);
			}
		}
	}

	public void DrawTriangles(PaintEventArgs e)
	{
		checked
		{
			if (Form1_Public.ShowTriangles && !Information.IsNothing(Triangul.TRG))
			{
				Point[] array = new Point[4];
				Public2.CoordWebMerc[] array2 = new Public2.CoordWebMerc[4];
				int upperBound = Triangul.TRG.GetUpperBound(0);
				Public2.CoordWebMerc coordWebMerc = default;
				for (int i = 0; i <= upperBound; i++)
				{
					coordWebMerc.Xwm = Triangul.TRG[i].X1;
					coordWebMerc.Ywm = Triangul.TRG[i].Y1;
					CalcCoords.ConvWebMercMetersToTile(coordWebMerc, ref tmpTileX1, ref tmpTileY1);
					array[0].X = (int)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					array[0].Y = (int)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					array2[0] = coordWebMerc;
					coordWebMerc.Xwm = Triangul.TRG[i].X2;
					coordWebMerc.Ywm = Triangul.TRG[i].Y2;
					CalcCoords.ConvWebMercMetersToTile(coordWebMerc, ref tmpTileX1, ref tmpTileY1);
					array[1].X = (int)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					array[1].Y = (int)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					array2[1] = coordWebMerc;
					coordWebMerc.Xwm = Triangul.TRG[i].X3;
					coordWebMerc.Ywm = Triangul.TRG[i].Y3;
					CalcCoords.ConvWebMercMetersToTile(coordWebMerc, ref tmpTileX1, ref tmpTileY1);
					array[2].X = (int)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					array[2].Y = (int)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					array2[2] = coordWebMerc;
					array[3] = array[0];
					array2[3] = array2[0];
					e.Graphics.DrawLines(PenYellow1, array);
				}
			}
		}
	}

	public void DrawHorizontalPlanes(PaintEventArgs e)
	{
		if (Information.IsNothing(Public2.PRJ.HP))
		{
			return;
		}
		checked
		{
			if (Public2.PRJ.SETT.FlagLastRezManyLimits)
			{
				int num = 0;
				do
				{
					if (Public2.PRJ.SETT.HorizCutLimits[num] && !Information.IsNothing(Public2.PRJ.HP_PLNwm[num]))
					{
						Region region = new Region(new GraphicsPath(FillMode.Winding));
						int upperBound = Public2.PRJ.HP_PLNwm[num].GetUpperBound(0);
						for (int i = 0; i <= upperBound; i++)
						{
							Point[] array = new Point[Public2.PRJ.HP_PLNwm[num][i].GetUpperBound(0) + 1];
							int upperBound2 = Public2.PRJ.HP_PLNwm[num][i].GetUpperBound(0);
							for (int j = 0; j <= upperBound2; j++)
							{
								CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.HP_PLNwm[num][i][j], ref tmpTileX1, ref tmpTileY1);
								array[j].X = (int)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
								array[j].Y = (int)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
							}
							if (Public2.PRJ.SETT.TransparFillZO > 0)
							{
								GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);
								graphicsPath.AddLines(array);
								region.Xor(graphicsPath);
							}
							if (Public2.PRJ.SETT.ShowConturZO)
							{
								Pen pen = new Pen(Public2.PRJ.SETT.LimitColor[num], unchecked((int)Public2.PRJ.SETT.LineWidthZO));
								pen.LineJoin = LineJoin.Bevel;
								e.Graphics.DrawLines(pen, array);
							}
						}
						if (Public2.PRJ.SETT.TransparFillZO > 0)
						{
							e.Graphics.FillRegion(new SolidBrush(Color.FromArgb((int)Math.Round((double)unchecked((int)Public2.PRJ.SETT.TransparFillZO) * 25.5), Public2.PRJ.SETT.LimitColor[num])), region);
						}
					}
					num++;
				}
				while (num <= 3);
				return;
			}
			for (int k = Public2.PRJ.HP.GetUpperBound(0); k >= 0; k += -1)
			{
				if (!Public2.PRJ.HP[k].Activ || Information.IsNothing(Public2.PRJ.HP[k].PLNwm))
				{
					continue;
				}
				Region region2 = new Region(new GraphicsPath(FillMode.Winding));
				int upperBound3 = Public2.PRJ.HP[k].PLNwm.GetUpperBound(0);
				for (int l = 0; l <= upperBound3; l++)
				{
					Point[] array2 = new Point[Public2.PRJ.HP[k].PLNwm[l].GetUpperBound(0) + 1];
					int upperBound4 = Public2.PRJ.HP[k].PLNwm[l].GetUpperBound(0);
					for (int m = 0; m <= upperBound4; m++)
					{
						CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.HP[k].PLNwm[l][m], ref tmpTileX1, ref tmpTileY1);
						array2[m].X = (int)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
						array2[m].Y = (int)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					}
					if (Public2.PRJ.SETT.TransparFillZO > 0)
					{
						GraphicsPath graphicsPath2 = new GraphicsPath(FillMode.Winding);
						graphicsPath2.AddLines(array2);
						region2.Xor(graphicsPath2);
					}
					unchecked
					{
						if (Public2.PRJ.SETT.ShowConturZO)
						{
							if (Public2.PRJ.HP[k].LineType == 0)
							{
								e.Graphics.DrawLines(new Pen(Public2.PRJ.HP[k].Color, (int)Public2.PRJ.SETT.LineWidthZO), array2);
								continue;
							}
							Pen pen2 = Public1.PenDash[Public2.PRJ.HP[k].LineType];
							pen2.LineJoin = LineJoin.Bevel;
							pen2.Width = (int)Public2.PRJ.SETT.LineWidthZO;
							pen2.Color = Public2.PRJ.HP[k].Color;
							pen2.DashStyle = DashStyle.Custom;
							e.Graphics.DrawLines(pen2, array2);
						}
					}
				}
				if (Public2.PRJ.SETT.TransparFillZO > 0)
				{
					e.Graphics.FillRegion(new SolidBrush(Color.FromArgb((int)Math.Round((double)unchecked((int)Public2.PRJ.SETT.TransparFillZO) * 25.5), Public2.PRJ.HP[k].Color)), region2);
				}
			}
		}
	}

	public void DrawRuler(PaintEventArgs e)
	{
		checked
		{
			if (Form1_Public.RulerOn)
			{
				float num = (float)(Form1_Public.Ruler[1].Xwm - Form1_Public.Ruler[0].Xwm);
				float num2 = (float)(Form1_Public.Ruler[1].Ywm - Form1_Public.Ruler[0].Ywm);
				float num3 = (float)(Math.Sqrt(num * num + num2 * num2) * CalcCoords.CalcMashtWMtoMeters());
				float num4 = (float)(Math.Atan2(num, num2) * (180.0 / Math.PI));
				if (num4 < 0f)
				{
					num4 += 360f;
				}
				string s = Conversions.ToString(Math.Round(num3, 1)) + "m\r\n" + Conversions.ToString(Math.Round(num4, 1)) + "°";
				CalcCoords.ConvWebMercMetersToTile(Form1_Public.Ruler[0], ref tmpTileX1, ref tmpTileY1);
				LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
				LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
				CalcCoords.ConvWebMercMetersToTile(Form1_Public.Ruler[1], ref tmpTileX1, ref tmpTileY1);
				LineX2 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
				LineY2 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
				e.Graphics.DrawLine(new Pen(Color.White, 4f), LineX1, LineY1, LineX2, LineY2);
				e.Graphics.DrawLine(new Pen(Color.Black, 2f), LineX1, LineY1, LineX2, LineY2);
				e.Graphics.DrawImage(Resources.ring6x6, LineX1 - 7, LineY1 - 7);
				int num5 = (int)Math.Round(8.0 * Math.Sin((double)(num4 + 160f) / (180.0 / Math.PI)) + (double)LineX2);
				int num6 = (int)Math.Round(-8.0 * Math.Cos((double)(num4 + 160f) / (180.0 / Math.PI)) + (double)LineY2);
				e.Graphics.DrawLine(new Pen(Color.Black, 2f), LineX2, LineY2, num5, num6);
				num5 = (int)Math.Round(8.0 * Math.Sin((double)(num4 + 200f) / (180.0 / Math.PI)) + (double)LineX2);
				num6 = (int)Math.Round(-8.0 * Math.Cos((double)(num4 + 200f) / (180.0 / Math.PI)) + (double)LineY2);
				e.Graphics.DrawLine(new Pen(Color.Black, 2f), LineX2, LineY2, num5, num6);
				int num7 = (int)Math.Round((double)(LineX1 + LineX2) / 2.0);
				int num8 = (int)Math.Round((double)(LineY1 + LineY2) / 2.0);
				SizeF sizeF = default;
				sizeF = e.Graphics.MeasureString(s, new Font("Arial", 8f, FontStyle.Regular));
				e.Graphics.FillRectangle(new SolidBrush(Color.White), (float)num7 - sizeF.Width / 2f, (float)num8 - sizeF.Height / 2f, sizeF.Width, sizeF.Height);
				e.Graphics.DrawString(s, new Font("Arial", 8f, FontStyle.Regular), Brushes.Black, (float)num7 - sizeF.Width / 2f, (float)num8 - sizeF.Height / 2f);
			}
		}
	}

	public void DrawVertPlineLines(PaintEventArgs e)
	{
		checked
		{
			if (Public2.PRJ.SETT.ShowVertPlaneLines && !Information.IsNothing(Public2.PRJ.VP))
			{
				Pen pen = new Pen(Color.BlueViolet, 3f);
				pen.DashStyle = DashStyle.Custom;
				pen.DashPattern = new float[2] { 3f, 1f };
				int upperBound = Public2.PRJ.VP.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.VP[i].CoordWM, ref tmpTileX1, ref tmpTileY1);
					LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					int num = (int)Math.Round((double)Public2.PRJ.VP[i].L1 * CalcCoords.CalcMashtMeterstoPixel());
					int num2 = (int)Math.Round((double)LineX1 + (double)num * Math.Sin((double)Public2.PRJ.VP[i].Azimuth / (180.0 / Math.PI)));
					int num3 = (int)Math.Round((double)LineY1 - (double)num * Math.Cos((double)Public2.PRJ.VP[i].Azimuth / (180.0 / Math.PI)));
					e.Graphics.DrawLine(pen, LineX1, LineY1, num2, num3);
				}
			}
		}
	}

	public void DrawConcentricCircles(PaintEventArgs e)
	{
		checked
		{
			if (Public2.PRJ.SETT.ShowCircles)
			{
				float num = (float)CalcCoords.CalcMashtMeterstoPixel();
				Pen pen = new Pen(Color.Black, 1f);
				pen.DashStyle = DashStyle.Custom;
				pen.DashPattern = new float[2] { 4f, 4f };
				float circleStep = Public2.PRJ.SETT.CircleStep;
				float circleStep2 = Public2.PRJ.SETT.CircleStep;
				bool flag = circleStep2 >= 0f;
				for (float num2 = circleStep; flag ? (num2 <= 500f) : (num2 >= 500f); num2 += circleStep2)
				{
					CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.SETT.OriginCoordWM, ref tmpTileX1, ref tmpTileY1);
					LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					int num3 = (int)Math.Round(num2 * num);
					int num4 = 2 * num3;
					e.Graphics.DrawEllipse(pen, LineX1 - num3, LineY1 - num3, num4, num4);
				}
			}
		}
	}

	public void OriginHover(MouseEventArgs e)
	{
		CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.SETT.OriginCoordWM, ref tmpTileX1, ref tmpTileY1);
		checked
		{
			LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
			LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
			if ((Math.Abs(LineX1 - e.X) < 16) & (Math.Abs(LineY1 - e.Y) < 16))
			{
				PB.Cursor = Cursors.Arrow;
				return;
			}
			if (Form1_Public.RulerOn)
			{
				int num = 0;
				do
				{
					CalcCoords.ConvWebMercMetersToTile(Form1_Public.Ruler[num], ref tmpTileX1, ref tmpTileY1);
					LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					if ((Math.Abs(LineX1 - e.X) < 4) & (Math.Abs(LineY1 - e.Y) < 4))
					{
						PB.Cursor = Cursors.Arrow;
						return;
					}
					num++;
				}
				while (num <= 1);
			}
			double Xt = default;
			double Yt = default;
			CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.SETT.HorAreaCoordWM[0], ref Xt, ref Yt);
			long num2 = (long)Math.Round((Xt - (double)TileX) * 256.0);
			long num3 = (long)Math.Round((Yt - (double)TileY) * 256.0);
			CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.SETT.HorAreaCoordWM[1], ref tmpTileX1, ref tmpTileY1);
			LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
			LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
			if ((Math.Abs(num2 - e.X) < 5) & (e.Y < num3) & (e.Y > LineY1))
			{
				PB.Cursor = Cursors.Arrow;
			}
			else if ((Math.Abs(LineX1 - e.X) < 5) & (e.Y < num3) & (e.Y > LineY1))
			{
				PB.Cursor = Cursors.Arrow;
			}
			else if ((Math.Abs(num3 - e.Y) < 5) & (e.X > num2) & (e.X < LineX1))
			{
				PB.Cursor = Cursors.Arrow;
			}
			else if ((Math.Abs(LineY1 - e.Y) < 5) & (e.X > num2) & (e.X < LineX1))
			{
				PB.Cursor = Cursors.Arrow;
			}
			else
			{
				PB.Cursor = Cursors.Cross;
			}
		}
	}

	public void OriginPick(MouseEventArgs e)
	{
		FlagOriginPick = false;
		CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.SETT.OriginCoordWM, ref tmpTileX1, ref tmpTileY1);
		checked
		{
			LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
			LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
			if ((Math.Abs(LineX1 - e.X) < 16) & (Math.Abs(LineY1 - e.Y) < 16))
			{
				FlagOriginPick = true;
			}
		}
	}

	public void OriginDrag(MouseEventArgs e)
	{
		if (FlagOriginPick)
		{
			CalcCoords.ConvTileToWebMercMeters((double)TileX + (double)e.X / 256.0, (double)TileY + (double)e.Y / 256.0, ref Public2.PRJ.SETT.OriginCoordWM);
			CalcCoords.ConvWebMercMetersToWGS84(Public2.PRJ.SETT.OriginCoordWM, ref Public2.PRJ.SETT.OriginCoordWGS.Lat, ref Public2.PRJ.SETT.OriginCoordWGS.Lon);
			CalcCoords.ConvScaleWebMercMetersToMeters();
			PB.Refresh();
		}
	}

	public void OriginDrop(MouseEventArgs e)
	{
		if (FlagOriginPick)
		{
			UC_Origin1.FillCoords();
		}
		FlagOriginPick = false;
		CalcCoords.ConvAllTxWebMercMetersToMeters();
		CalcCoords.ConvAllControlPointsWebMercMetersToMeters();
		CalcCoords.ConvAllVertPlanesWebMercMetersToMeters();
	}

	public void RasterScalingHover(MouseEventArgs e)
	{
		int num = 0;
		checked
		{
			do
			{
				CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.RPL.PointMap[num], ref tmpTileX1, ref tmpTileY1);
				LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
				LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
				if (num == 0)
				{
					if ((Math.Abs(LineX1 - e.X) < 16) & (Math.Abs(LineY1 - e.Y) < 16))
					{
						PB.Cursor = Cursors.Arrow;
						return;
					}
				}
				else if ((Math.Abs(LineX1 - e.X) < 4) & (Math.Abs(LineY1 - e.Y) < 4))
				{
					PB.Cursor = Cursors.Arrow;
					return;
				}
				num++;
			}
			while (num <= 2);
			if (Form1_Public.RulerOn)
			{
				int num2 = 0;
				do
				{
					CalcCoords.ConvWebMercMetersToTile(Form1_Public.Ruler[num2], ref tmpTileX1, ref tmpTileY1);
					LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					if ((Math.Abs(LineX1 - e.X) < 4) & (Math.Abs(LineY1 - e.Y) < 4))
					{
						PB.Cursor = Cursors.Arrow;
						return;
					}
					num2++;
				}
				while (num2 <= 1);
			}
			PB.Cursor = Cursors.Cross;
		}
	}

	public void RasterScalingPick(MouseEventArgs e)
	{
		FlagPick = false;
		IndexRPL = -1;
		int num = 0;
		checked
		{
			do
			{
				CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.RPL.PointMap[num], ref tmpTileX1, ref tmpTileY1);
				LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
				LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
				if (num == 0)
				{
					if ((Math.Abs(LineX1 - e.X) < 16) & (Math.Abs(LineY1 - e.Y) < 16))
					{
						IndexRPL = num;
						FlagPick = true;
					}
				}
				else if ((Math.Abs(LineX1 - e.X) < 4) & (Math.Abs(LineY1 - e.Y) < 4))
				{
					IndexRPL = num;
					FlagPick = true;
				}
				num++;
			}
			while (num <= 2);
		}
	}

	public void RasterScalingDrag(MouseEventArgs e)
	{
		if (FlagPick && IndexRPL >= 0)
		{
			Public2.CoordWebMerc CoordWM = default;
			CalcCoords.ConvTileToWebMercMeters((double)TileX + (double)e.X / 256.0, (double)TileY + (double)e.Y / 256.0, ref CoordWM);
			Public2.PRJ.RPL.PointMap[IndexRPL] = CoordWM;
			PB.Refresh();
		}
	}

	public void RasterScalingDrop(MouseEventArgs e)
	{
		FlagPick = false;
	}

	public void AntennaHover(MouseEventArgs e)
	{
		if (Information.IsNothing(Public2.PRJ.TX))
		{
			return;
		}
		int upperBound = Public2.PRJ.TX.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.TX[i].CoordWM, ref tmpTileX1, ref tmpTileY1);
				LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
				LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
				if ((Math.Abs(LineX1 - e.X) < 5) & (Math.Abs(LineY1 - e.Y) < 5))
				{
					PB.Cursor = Cursors.Arrow;
					return;
				}
			}
			if (Form1_Public.RulerOn)
			{
				int num = 0;
				do
				{
					CalcCoords.ConvWebMercMetersToTile(Form1_Public.Ruler[num], ref tmpTileX1, ref tmpTileY1);
					LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					if ((Math.Abs(LineX1 - e.X) < 4) & (Math.Abs(LineY1 - e.Y) < 4))
					{
						PB.Cursor = Cursors.Arrow;
						return;
					}
					num++;
				}
				while (num <= 1);
			}
			PB.Cursor = Cursors.Cross;
		}
	}

	public void AntennaPick(MouseEventArgs e)
	{
		if (Information.IsNothing(Public2.PRJ.TX))
		{
			return;
		}
		FlagPick = false;
		IndexTX1 = -1;
		int upperBound = Public2.PRJ.TX.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.TX[i].CoordWM, ref tmpTileX1, ref tmpTileY1);
				LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
				LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
				if ((Math.Abs(LineX1 - e.X) < 5) & (Math.Abs(LineY1 - e.Y) < 5))
				{
					FlagPick = true;
					IndexTX1 = (short)i;
					if (Public2.PRJ.TX[i].Selected)
					{
						break;
					}
				}
			}
		}
	}

	public void AntennaDrag(MouseEventArgs e)
	{
		if (!FlagPick || IndexTX1 == -1)
		{
			return;
		}
		Public2.CoordWebMerc CoordWM = default;
		CalcCoords.ConvTileToWebMercMeters((double)TileX + (double)e.X / 256.0, (double)TileY + (double)e.Y / 256.0, ref CoordWM);
		bool flag = false;
		float num = (float)((double)(int)Public2.PRJ.SETT.LengthAttractAntennas / 10.0);
		int upperBound = Public2.PRJ.TX.GetUpperBound(0);
		for (int i = 0; i <= upperBound; i = checked(i + 1))
		{
			if ((Math.Abs(Public2.PRJ.TX[i].CoordWM.Xwm - CoordWM.Xwm) < (double)num) & (Math.Abs(Public2.PRJ.TX[i].CoordWM.Ywm - CoordWM.Ywm) < (double)num) & (IndexTX1 != i))
			{
				Public2.PRJ.TX[IndexTX1].CoordWM.Xwm = Public2.PRJ.TX[i].CoordWM.Xwm;
				Public2.PRJ.TX[IndexTX1].CoordWM.Ywm = Public2.PRJ.TX[i].CoordWM.Ywm;
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			Public2.PRJ.TX[IndexTX1].CoordWM = CoordWM;
		}
		PB.Refresh();
	}

	public void AntennaDrop(MouseEventArgs e)
	{
		if (Information.IsNothing(Public2.PRJ.TX))
		{
			return;
		}
		if (FlagPick)
		{
			UC_Antennas1.FillDGV1();
			UC_Antennas1.DGV1.CurrentCell = UC_Antennas1.DGV1[0, IndexTX1];
			UC_Antennas1.DGV1.Rows[IndexTX1].Selected = true;
			if (IndexTX1 == 0)
			{
				UC_Antennas1.DGV1.FirstDisplayedScrollingRowIndex = IndexTX1;
			}
			else
			{
				UC_Antennas1.DGV1.FirstDisplayedScrollingRowIndex = checked(IndexTX1 - 1);
			}
		}
		FlagPick = false;
		PB.Refresh();
	}

	public void ControlPointHover(MouseEventArgs e)
	{
		if (Information.IsNothing(Public2.PRJ.CPG))
		{
			return;
		}
		int upperBound = Public2.PRJ.CPG.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				if (!Public2.PRJ.CPG[i].Active || Information.IsNothing(Public2.PRJ.CPG[i].CP))
				{
					continue;
				}
				int upperBound2 = Public2.PRJ.CPG[i].CP.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.CPG[i].CP[j].CoordWM, ref tmpTileX1, ref tmpTileY1);
					LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					if ((Math.Abs(LineX1 - e.X) < 5) & (Math.Abs(LineY1 - e.Y) < 5))
					{
						PB.Cursor = Cursors.Arrow;
						return;
					}
				}
			}
			if (Form1_Public.RulerOn)
			{
				int num = 0;
				do
				{
					CalcCoords.ConvWebMercMetersToTile(Form1_Public.Ruler[num], ref tmpTileX1, ref tmpTileY1);
					LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					if ((Math.Abs(LineX1 - e.X) < 4) & (Math.Abs(LineY1 - e.Y) < 4))
					{
						PB.Cursor = Cursors.Arrow;
						return;
					}
					num++;
				}
				while (num <= 1);
			}
			PB.Cursor = Cursors.Cross;
		}
	}

	public void ControlPointPick(MouseEventArgs e)
	{
		if (Information.IsNothing(Public2.PRJ.CPG))
		{
			return;
		}
		FlagPick = false;
		IndexGR = -1;
		IndexCP = -1;
		int upperBound = Public2.PRJ.CPG.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				if (!Public2.PRJ.CPG[i].Active || Information.IsNothing(Public2.PRJ.CPG[i].CP))
				{
					continue;
				}
				int upperBound2 = Public2.PRJ.CPG[i].CP.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.CPG[i].CP[j].CoordWM, ref tmpTileX1, ref tmpTileY1);
					LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					if ((Math.Abs(LineX1 - e.X) < 5) & (Math.Abs(LineY1 - e.Y) < 5))
					{
						FlagPick = true;
						IndexGR = (short)i;
						IndexCP = (short)j;
						return;
					}
				}
			}
		}
	}

	public void ControlPointDrag(MouseEventArgs e)
	{
		if (FlagPick && !((IndexGR == -1) | (IndexCP == -1)))
		{
			Public2.CoordWebMerc CoordWM = default;
			CalcCoords.ConvTileToWebMercMeters((double)TileX + (double)e.X / 256.0, (double)TileY + (double)e.Y / 256.0, ref CoordWM);
			Public2.PRJ.CPG[IndexGR].CP[IndexCP].CoordWM = CoordWM;
			PB.Refresh();
		}
	}

	public void ControlPointDrop(MouseEventArgs e)
	{
		if (!Information.IsNothing(Public2.PRJ.CPG))
		{
			if (FlagPick)
			{
				UC_ControlPoints1.FillDGV1();
				UC_ControlPoints1.DGV1.CurrentCell = UC_ControlPoints1.DGV1[0, IndexGR];
				UC_ControlPoints1.DGV1.Rows[IndexGR].Selected = true;
				UC_ControlPoints1.DGV2.CurrentCell = UC_ControlPoints1.DGV2[0, IndexCP];
				UC_ControlPoints1.DGV2.Rows[IndexCP].Selected = true;
				UC_ControlPoints1.DGV2.FirstDisplayedScrollingRowIndex = IndexCP;
			}
			FlagPick = false;
			PB.Refresh();
		}
	}

	public void HorizontalAreaHover(MouseEventArgs e)
	{
		double Xt = default;
		double Yt = default;
		CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.SETT.HorAreaCoordWM[0], ref Xt, ref Yt);
		checked
		{
			long num = (long)Math.Round((Xt - (double)TileX) * 256.0);
			long num2 = (long)Math.Round((Yt - (double)TileY) * 256.0);
			double Xt2 = default;
			double Yt2 = default;
			CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.SETT.HorAreaCoordWM[1], ref Xt2, ref Yt2);
			long num3 = (long)Math.Round((Xt2 - (double)TileX) * 256.0);
			long num4 = (long)Math.Round((Yt2 - (double)TileY) * 256.0);
			if ((Math.Abs(num - e.X) < 5) & (e.Y < num2) & (e.Y > num4))
			{
				PB.Cursor = Cursors.Arrow;
				return;
			}
			if ((Math.Abs(num3 - e.X) < 5) & (e.Y < num2) & (e.Y > num4))
			{
				PB.Cursor = Cursors.Arrow;
				return;
			}
			if ((Math.Abs(num2 - e.Y) < 5) & (e.X > num) & (e.X < num3))
			{
				PB.Cursor = Cursors.Arrow;
				return;
			}
			if ((Math.Abs(num4 - e.Y) < 5) & (e.X > num) & (e.X < num3))
			{
				PB.Cursor = Cursors.Arrow;
				return;
			}
			if (Form1_Public.RulerOn)
			{
				int num5 = 0;
				do
				{
					CalcCoords.ConvWebMercMetersToTile(Form1_Public.Ruler[num5], ref Xt2, ref Yt2);
					num3 = (long)Math.Round((Xt2 - (double)TileX) * 256.0);
					num4 = (long)Math.Round((Yt2 - (double)TileY) * 256.0);
					if ((Math.Abs(num3 - e.X) < 4) & (Math.Abs(num4 - e.Y) < 4))
					{
						PB.Cursor = Cursors.Arrow;
						return;
					}
					num5++;
				}
				while (num5 <= 1);
			}
			if (Public2.PRJ.SETT.ShowVertPlaneLines & !Information.IsNothing(Public2.PRJ.VP))
			{
				int upperBound = Public2.PRJ.VP.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.VP[i].CoordWM, ref Xt2, ref Yt2);
					num3 = (long)Math.Round((Xt2 - (double)TileX) * 256.0);
					num4 = (long)Math.Round((Yt2 - (double)TileY) * 256.0);
					int num6 = (int)Math.Round((double)Public2.PRJ.VP[i].L1 * CalcCoords.CalcMashtMeterstoPixel());
					int num7 = (int)Math.Round((double)num3 + (double)num6 * Math.Sin((double)Public2.PRJ.VP[i].Azimuth / (180.0 / Math.PI)));
					int num8 = (int)Math.Round((double)num4 - (double)num6 * Math.Cos((double)Public2.PRJ.VP[i].Azimuth / (180.0 / Math.PI)));
					if (Module1.PointLineD(num3, num4, num7, num8, e.X, e.Y, 5.0))
					{
						PB.Cursor = Cursors.Arrow;
						return;
					}
				}
			}
			PB.Cursor = Cursors.Cross;
		}
	}

	public void HorizontalAreaPick(MouseEventArgs e)
	{
		FlagPick = false;
		IndexHorAreaX = -1;
		IndexHorAreaY = -1;
		double Xt = default;
		double Yt = default;
		CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.SETT.HorAreaCoordWM[0], ref Xt, ref Yt);
		checked
		{
			long num = (long)Math.Round((Xt - (double)TileX) * 256.0);
			long num2 = (long)Math.Round((Yt - (double)TileY) * 256.0);
			double Xt2 = default;
			double Yt2 = default;
			CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.SETT.HorAreaCoordWM[1], ref Xt2, ref Yt2);
			long num3 = (long)Math.Round((Xt2 - (double)TileX) * 256.0);
			long num4 = (long)Math.Round((Yt2 - (double)TileY) * 256.0);
			if ((Math.Abs(num - e.X) < 5) & (e.Y < num2) & (e.Y > num4))
			{
				IndexHorAreaX = 0;
				FlagPick = true;
			}
			else if ((Math.Abs(num3 - e.X) < 5) & (e.Y < num2) & (e.Y > num4))
			{
				IndexHorAreaX = 1;
				FlagPick = true;
			}
			else if ((Math.Abs(num2 - e.Y) < 5) & (e.X > num) & (e.X < num3))
			{
				IndexHorAreaY = 0;
				FlagPick = true;
			}
			else if ((Math.Abs(num4 - e.Y) < 5) & (e.X > num) & (e.X < num3))
			{
				IndexHorAreaY = 1;
				FlagPick = true;
			}
		}
	}

	public void HorizontalAreaDrag(MouseEventArgs e)
	{
		if (FlagPick && ((IndexHorAreaX >= 0) | (IndexHorAreaY >= 0)))
		{
			Public2.CoordWebMerc CoordWM = default;
			CalcCoords.ConvTileToWebMercMeters((double)TileX + (double)e.X / 256.0, (double)TileY + (double)e.Y / 256.0, ref CoordWM);
			if (IndexHorAreaX == 0)
			{
				Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm = CoordWM.Xwm;
				Public2.PRJ.SETT.HorAreaCoordM[0] = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.SETT.HorAreaCoordWM[0]);
			}
			else if (IndexHorAreaX == 1)
			{
				Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm = CoordWM.Xwm;
				Public2.PRJ.SETT.HorAreaCoordM[1] = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.SETT.HorAreaCoordWM[1]);
			}
			else if (IndexHorAreaY == 0)
			{
				Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm = CoordWM.Ywm;
				Public2.PRJ.SETT.HorAreaCoordM[0] = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.SETT.HorAreaCoordWM[0]);
			}
			else if (IndexHorAreaY == 1)
			{
				Public2.PRJ.SETT.HorAreaCoordWM[1].Ywm = CoordWM.Ywm;
				Public2.PRJ.SETT.HorAreaCoordM[1] = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.SETT.HorAreaCoordWM[1]);
			}
			PB.Refresh();
		}
	}

	public void HorizontalAreaDrop(MouseEventArgs e)
	{
		if (IndexHorAreaX >= 0)
		{
			if (Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm > Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm)
			{
				double xwm = Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm;
				Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm = Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm;
				Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm = xwm;
				Public2.PRJ.SETT.HorAreaCoordM[0] = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.SETT.HorAreaCoordWM[0]);
				Public2.PRJ.SETT.HorAreaCoordM[1] = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.SETT.HorAreaCoordWM[1]);
				PB.Refresh();
			}
		}
		else if (IndexHorAreaY >= 0 && Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm > Public2.PRJ.SETT.HorAreaCoordWM[1].Ywm)
		{
			double ywm = Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm;
			Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm = Public2.PRJ.SETT.HorAreaCoordWM[1].Ywm;
			Public2.PRJ.SETT.HorAreaCoordWM[1].Ywm = ywm;
			Public2.PRJ.SETT.HorAreaCoordM[0] = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.SETT.HorAreaCoordWM[0]);
			Public2.PRJ.SETT.HorAreaCoordM[1] = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.SETT.HorAreaCoordWM[1]);
			PB.Refresh();
		}
		FlagPick = false;
	}

	public void BuildingHover(MouseEventArgs e)
	{
		if (Information.IsNothing(Public2.PRJ.BLD))
		{
			return;
		}
		checked
		{
			if (IndexBLD >= 0)
			{
				int num = Public2.PRJ.BLD[IndexBLD].CoordWM.GetUpperBound(0) - 1;
				for (int i = 0; i <= num; i++)
				{
					CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.BLD[IndexBLD].CoordWM[i], ref tmpTileX1, ref tmpTileY1);
					LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					if ((Math.Abs(LineX1 - e.X) < 5) & (Math.Abs(LineY1 - e.Y) < 5))
					{
						PB.Cursor = Cursors.Hand;
						return;
					}
				}
			}
			Public2.CoordWebMerc CoordWM = default;
			CalcCoords.ConvTileToWebMercMeters((double)e.X / 256.0 + (double)TileX, (double)e.Y / 256.0 + (double)TileY, ref CoordWM);
			int upperBound = Public2.PRJ.BLD.GetUpperBound(0);
			for (int j = 0; j <= upperBound; j++)
			{
				if (Module1.IsPointInsidePolygonF(CoordWM, Public2.PRJ.BLD[j].CoordWM))
				{
					PB.Cursor = Cursors.Arrow;
					return;
				}
			}
			if (Form1_Public.RulerOn)
			{
				int num2 = 0;
				do
				{
					CalcCoords.ConvWebMercMetersToTile(Form1_Public.Ruler[num2], ref tmpTileX1, ref tmpTileY1);
					LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					if ((Math.Abs(LineX1 - e.X) < 4) & (Math.Abs(LineY1 - e.Y) < 4))
					{
						PB.Cursor = Cursors.Arrow;
						return;
					}
					num2++;
				}
				while (num2 <= 1);
			}
			PB.Cursor = Cursors.Cross;
		}
	}

	public void BuildingPick(MouseEventArgs e)
	{
		checked
		{
			if (!Form1_Public.StartDrawingBuilding)
			{
				if (Information.IsNothing(Public2.PRJ.BLD))
				{
					return;
				}
				FlagPick = false;
				IndexPNT = -1;
				if (IndexBLD >= 0)
				{
					int num = Public2.PRJ.BLD[IndexBLD].CoordWM.GetUpperBound(0) - 1;
					Public2.CoordWebMerc CoordWM = default;
					for (int i = 0; i <= num; i++)
					{
						CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.BLD[IndexBLD].CoordWM[i], ref tmpTileX1, ref tmpTileY1);
						LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
						LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
						if ((Math.Abs(LineX1 - e.X) < 5) & (Math.Abs(LineY1 - e.Y) < 5))
						{
							if (Form1_Public.StartDeletingNodes)
							{
								if (Public2.PRJ.BLD[IndexBLD].CoordWM.GetUpperBound(0) > 3)
								{
									int num2 = i;
									int num3 = Public2.PRJ.BLD[IndexBLD].CoordWM.GetUpperBound(0) - 1;
									for (int j = num2; j <= num3; j++)
									{
										Public2.PRJ.BLD[IndexBLD].CoordWM[j] = Public2.PRJ.BLD[IndexBLD].CoordWM[j + 1];
									}
									ref Public2.CoordWebMerc[] coordWM = ref Public2.PRJ.BLD[IndexBLD].CoordWM;
									coordWM = (Public2.CoordWebMerc[])Utils.CopyArray(coordWM, new Public2.CoordWebMerc[Public2.PRJ.BLD[IndexBLD].CoordWM.GetUpperBound(0) - 1 + 1]);
									if (i == 0)
									{
										Public2.PRJ.BLD[IndexBLD].CoordWM[Public2.PRJ.BLD[IndexBLD].CoordWM.GetUpperBound(0)] = Public2.PRJ.BLD[IndexBLD].CoordWM[0];
									}
								}
							}
							else
							{
								FlagPick = true;
								IndexPNT = (short)i;
								DragStart = true;
							}
							return;
						}
						if (!Form1_Public.StartAddingNodes)
						{
							continue;
						}
						CalcCoords.ConvTileToWebMercMeters((double)e.X / 256.0 + (double)TileX, (double)e.Y / 256.0 + (double)TileY, ref CoordWM);
						if (Module1.PointLine(Public2.PRJ.BLD[IndexBLD].CoordWM[i], Public2.PRJ.BLD[IndexBLD].CoordWM[i + 1], CoordWM))
						{
							ref Public2.CoordWebMerc[] coordWM2 = ref Public2.PRJ.BLD[IndexBLD].CoordWM;
							coordWM2 = (Public2.CoordWebMerc[])Utils.CopyArray(coordWM2, new Public2.CoordWebMerc[Public2.PRJ.BLD[IndexBLD].CoordWM.GetUpperBound(0) + 1 + 1]);
							int upperBound = Public2.PRJ.BLD[IndexBLD].CoordWM.GetUpperBound(0);
							int num4 = i + 1;
							for (int k = upperBound; k >= num4; k += -1)
							{
								Public2.PRJ.BLD[IndexBLD].CoordWM[k] = Public2.PRJ.BLD[IndexBLD].CoordWM[k - 1];
							}
							Public2.PRJ.BLD[IndexBLD].CoordWM[i + 1] = CoordWM;
							return;
						}
					}
				}
				IndexBLD = -1;
				int upperBound2 = Public2.PRJ.BLD.GetUpperBound(0);
				Public2.CoordWebMerc CoordWM2 = default;
				for (int l = 0; l <= upperBound2; l++)
				{
					CalcCoords.ConvTileToWebMercMeters((double)e.X / 256.0 + (double)TileX, (double)e.Y / 256.0 + (double)TileY, ref CoordWM2);
					if (Module1.IsPointInsidePolygonF(CoordWM2, Public2.PRJ.BLD[l].CoordWM))
					{
						FlagPick = true;
						IndexBLD = (short)l;
						PickPoint.Xpix = (short)e.X;
						PickPoint.Ypix = (short)e.Y;
						PickBLD = Module1.DeepClone(Public2.PRJ.BLD[l]);
						UC_Buildings1.gbBuildingParameters.Visible = true;
						UC_Buildings1.txtHeight.Text = Conversions.ToString(Public2.PRJ.BLD[IndexBLD].H);
						UC_Buildings1.txtText.Text = Public2.PRJ.BLD[IndexBLD].Text;
						UC_Buildings1.txtFundation.Text = Conversions.ToString(Public2.PRJ.BLD[IndexBLD].Fundation);
						UC_Buildings1.chkShow.Checked = Public2.PRJ.BLD[IndexBLD].Show;
						UC_Buildings1.chkUseAttenuation.Checked = Public2.PRJ.BLD[IndexBLD].UseAttenuation;
						DragStart = true;
						Form1_Public.StartDeletingNodes = false;
						Form1_Public.StartAddingNodes = false;
						UC_Buildings1.cmdDeleteNode.BackColor = SystemColors.Control;
						UC_Buildings1.cmdAddNode.BackColor = SystemColors.Control;
						return;
					}
				}
				Form1_Public.StartDeletingNodes = false;
				Form1_Public.StartAddingNodes = false;
				UC_Buildings1.cmdDeleteNode.BackColor = SystemColors.Control;
				UC_Buildings1.cmdAddNode.BackColor = SystemColors.Control;
				UC_Buildings1.gbBuildingParameters.Visible = false;
			}
			else
			{
				if (Information.IsNothing(Form1_Public.NewBLD.CoordWM))
				{
					Module1.BuildingBackUp();
					Form1_Public.NewBLD.CoordWM = new Public2.CoordWebMerc[1];
				}
				else
				{
					ref Public2.CoordWebMerc[] coordWM3 = ref Form1_Public.NewBLD.CoordWM;
					coordWM3 = (Public2.CoordWebMerc[])Utils.CopyArray(coordWM3, new Public2.CoordWebMerc[Form1_Public.NewBLD.CoordWM.GetUpperBound(0) + 1 + 1]);
				}
				CalcCoords.ConvTileToWebMercMeters((double)e.X / 256.0 + (double)TileX, (double)e.Y / 256.0 + (double)TileY, ref Form1_Public.NewBLD.CoordWM[Form1_Public.NewBLD.CoordWM.GetUpperBound(0)]);
				PB.Refresh();
			}
		}
	}

	public void BuildingDrag(MouseEventArgs e)
	{
		if (!FlagPick)
		{
			return;
		}
		checked
		{
			if ((IndexBLD > -1) & (IndexPNT > -1))
			{
				if (DragStart)
				{
					Module1.BuildingBackUp();
					DragStart = false;
				}
				Public2.CoordWebMerc CoordWM = default;
				CalcCoords.ConvTileToWebMercMeters((double)TileX + (double)e.X / 256.0, (double)TileY + (double)e.Y / 256.0, ref CoordWM);
				Public2.PRJ.BLD[IndexBLD].CoordWM[IndexPNT] = CoordWM;
				if (IndexPNT == 0)
				{
					Public2.PRJ.BLD[IndexBLD].CoordWM[Public2.PRJ.BLD[IndexBLD].CoordWM.GetUpperBound(0)] = CoordWM;
				}
				else if (IndexPNT == Public2.PRJ.BLD[IndexBLD].CoordWM.GetUpperBound(0))
				{
					Public2.PRJ.BLD[IndexBLD].CoordWM[0] = CoordWM;
				}
			}
			else if (IndexBLD > -1)
			{
				if (DragStart)
				{
					Module1.BuildingBackUp();
					DragStart = false;
				}
				int num = e.X - PickPoint.Xpix;
				int num2 = e.Y - PickPoint.Ypix;
				double num3 = CalcCoords.CalcMashtWMtoPixel();
				int upperBound = Public2.PRJ.BLD[IndexBLD].CoordWM.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					Public2.PRJ.BLD[IndexBLD].CoordWM[i].Xwm = PickBLD.CoordWM[i].Xwm + (double)num / num3;
					Public2.PRJ.BLD[IndexBLD].CoordWM[i].Ywm = PickBLD.CoordWM[i].Ywm - (double)num2 / num3;
				}
			}
			PB.Refresh();
		}
	}

	public void BuildingDrop(MouseEventArgs e)
	{
		if (!Information.IsNothing(Public2.PRJ.BLD))
		{
			IndexPNT = -1;
			FlagPick = false;
			PB.Refresh();
		}
	}

	public void BuildingEndCreating()
	{
		checked
		{
			if (Form1_Public.StartDrawingBuilding)
			{
				if (!Information.IsNothing(Form1_Public.NewBLD.CoordWM) && Form1_Public.NewBLD.CoordWM.GetUpperBound(0) > 1)
				{
					if (Information.IsNothing(Public2.PRJ.BLD))
					{
						Public2.PRJ.BLD = new Public2.StructBuilding[1];
					}
					else
					{
						ref Public2.StructBuilding[] bLD = ref Public2.PRJ.BLD;
						bLD = (Public2.StructBuilding[])Utils.CopyArray(bLD, new Public2.StructBuilding[Public2.PRJ.BLD.GetUpperBound(0) + 1 + 1]);
					}
					Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)] = Module1.DeepClone(Form1_Public.NewBLD);
					Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)].Show = true;
					Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)].UseAttenuation = true;
					ref Public2.CoordWebMerc[] coordWM = ref Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)].CoordWM;
					coordWM = (Public2.CoordWebMerc[])Utils.CopyArray(coordWM, new Public2.CoordWebMerc[Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)].CoordWM.GetUpperBound(0) + 1 + 1]);
					Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)].CoordWM[Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)].CoordWM.GetUpperBound(0)] = Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)].CoordWM[0];
					Form1_Public.NewBLD = default;
					Form1_Public.StartDrawingBuilding = false;
					UC_Buildings1.cmdAddBuilding.BackColor = SystemColors.Control;
					IndexBLD = (short)Public2.PRJ.BLD.GetUpperBound(0);
					IndexPNT = -1;
					PB.Refresh();
					UC_Buildings1.gbBuildingParameters.Visible = true;
					UC_Buildings1.txtHeight.Text = Conversions.ToString(Public2.PRJ.BLD[IndexBLD].H);
					UC_Buildings1.txtText.Text = Public2.PRJ.BLD[IndexBLD].Text;
					UC_Buildings1.txtFundation.Text = Conversions.ToString(Public2.PRJ.BLD[IndexBLD].Fundation);
					UC_Buildings1.chkShow.Checked = Public2.PRJ.BLD[IndexBLD].Show;
					UC_Buildings1.chkUseAttenuation.Checked = Public2.PRJ.BLD[IndexBLD].UseAttenuation;
				}
			}
			else
			{
				UC_Buildings1.cmdDeleteNode.BackColor = SystemColors.Control;
				UC_Buildings1.cmdAddNode.BackColor = SystemColors.Control;
				Form1_Public.StartDeletingNodes = false;
				Form1_Public.StartAddingNodes = false;
				PB.Refresh();
			}
		}
	}

	public void LineHover(MouseEventArgs e)
	{
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.ROAD) && IndexRoad >= 0)
			{
				int upperBound = Public2.PRJ.ROAD[IndexRoad].CoordWM.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.ROAD[IndexRoad].CoordWM[i], ref tmpTileX1, ref tmpTileY1);
					LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					if ((Math.Abs(LineX1 - e.X) < 5) & (Math.Abs(LineY1 - e.Y) < 5))
					{
						PB.Cursor = Cursors.Hand;
						return;
					}
				}
			}
			if (!Information.IsNothing(Public2.PRJ.FENCE) && IndexFence >= 0)
			{
				int upperBound2 = Public2.PRJ.FENCE[IndexFence].CoordWM.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.FENCE[IndexFence].CoordWM[j], ref tmpTileX1, ref tmpTileY1);
					LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					if ((Math.Abs(LineX1 - e.X) < 5) & (Math.Abs(LineY1 - e.Y) < 5))
					{
						PB.Cursor = Cursors.Hand;
						return;
					}
				}
			}
			if (!Information.IsNothing(Public2.PRJ.TXT))
			{
				int upperBound3 = Public2.PRJ.TXT.GetUpperBound(0);
				for (int k = 0; k <= upperBound3; k++)
				{
					stringSize = Form1_Public.MeasureString(Public2.PRJ.TXT[k].Text, new Font(Public2.PRJ.TXT[k].FontName, unchecked((int)Public2.PRJ.TXT[k].Size), FontStyle.Regular));
					CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.TXT[k].CoordWM, ref tmpTileX1, ref tmpTileY1);
					LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					if ((LineX1 - 10 < e.X) & ((float)LineX1 + stringSize.Width + 10f > (float)e.X) & ((float)LineY1 + stringSize.Height + 5f > (float)e.Y) & ((float)LineY1 - stringSize.Height - 5f < (float)e.Y))
					{
						PB.Cursor = Cursors.Arrow;
						return;
					}
				}
			}
			if (!Information.IsNothing(Public2.PRJ.ROAD))
			{
				Public2.CoordWebMerc CoordWM = default;
				CalcCoords.ConvTileToWebMercMeters((double)e.X / 256.0 + (double)TileX, (double)e.Y / 256.0 + (double)TileY, ref CoordWM);
				int upperBound4 = Public2.PRJ.ROAD.GetUpperBound(0);
				for (int l = 0; l <= upperBound4; l++)
				{
					int num = Public2.PRJ.ROAD[l].CoordWM.GetUpperBound(0) - 1;
					for (int m = 0; m <= num; m++)
					{
						if (Module1.PointLine(Public2.PRJ.ROAD[l].CoordWM[m], Public2.PRJ.ROAD[l].CoordWM[m + 1], CoordWM))
						{
							PB.Cursor = Cursors.Arrow;
							return;
						}
					}
				}
			}
			if (!Information.IsNothing(Public2.PRJ.FENCE))
			{
				Public2.CoordWebMerc CoordWM2 = default;
				CalcCoords.ConvTileToWebMercMeters((double)e.X / 256.0 + (double)TileX, (double)e.Y / 256.0 + (double)TileY, ref CoordWM2);
				int upperBound5 = Public2.PRJ.FENCE.GetUpperBound(0);
				for (int n = 0; n <= upperBound5; n++)
				{
					int num2 = Public2.PRJ.FENCE[n].CoordWM.GetUpperBound(0) - 1;
					for (int num3 = 0; num3 <= num2; num3++)
					{
						if (Module1.PointLine(Public2.PRJ.FENCE[n].CoordWM[num3], Public2.PRJ.FENCE[n].CoordWM[num3 + 1], CoordWM2))
						{
							PB.Cursor = Cursors.Arrow;
							return;
						}
					}
				}
			}
			if (Form1_Public.RulerOn)
			{
				int num4 = 0;
				do
				{
					CalcCoords.ConvWebMercMetersToTile(Form1_Public.Ruler[num4], ref tmpTileX1, ref tmpTileY1);
					LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					if ((Math.Abs(LineX1 - e.X) < 4) & (Math.Abs(LineY1 - e.Y) < 4))
					{
						PB.Cursor = Cursors.Arrow;
						return;
					}
					num4++;
				}
				while (num4 <= 1);
			}
			PB.Cursor = Cursors.Cross;
		}
	}

	public void LinePick(MouseEventArgs e)
	{
		checked
		{
			if (!Form1_Public.StartDrawingRoad & !Form1_Public.StartDrawingFence & !Form1_Public.StartDrawingText)
			{
				FlagPick = false;
				IndexPNT = -1;
				if (IndexRoad > -1)
				{
					int upperBound = Public2.PRJ.ROAD[IndexRoad].CoordWM.GetUpperBound(0);
					Public2.CoordWebMerc CoordWM = default;
					for (int i = 0; i <= upperBound; i++)
					{
						CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.ROAD[IndexRoad].CoordWM[i], ref tmpTileX1, ref tmpTileY1);
						LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
						LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
						if ((Math.Abs(LineX1 - e.X) < 5) & (Math.Abs(LineY1 - e.Y) < 5))
						{
							if (Form1_Public.StartDeletingNodes)
							{
								if (Public2.PRJ.ROAD[IndexRoad].CoordWM.GetUpperBound(0) > 1)
								{
									int num = i;
									int num2 = Public2.PRJ.ROAD[IndexRoad].CoordWM.GetUpperBound(0) - 1;
									for (int j = num; j <= num2; j++)
									{
										Public2.PRJ.ROAD[IndexRoad].CoordWM[j] = Public2.PRJ.ROAD[IndexRoad].CoordWM[j + 1];
									}
									ref Public2.CoordWebMerc[] coordWM = ref Public2.PRJ.ROAD[IndexRoad].CoordWM;
									coordWM = (Public2.CoordWebMerc[])Utils.CopyArray(coordWM, new Public2.CoordWebMerc[Public2.PRJ.ROAD[IndexRoad].CoordWM.GetUpperBound(0) - 1 + 1]);
								}
							}
							else if (Form1_Public.StartSplittingNodes)
							{
								if ((i > 0) & (i < Public2.PRJ.ROAD[IndexRoad].CoordWM.GetUpperBound(0)))
								{
									ref Public2.StructRoad[] rOAD = ref Public2.PRJ.ROAD;
									rOAD = (Public2.StructRoad[])Utils.CopyArray(rOAD, new Public2.StructRoad[Public2.PRJ.ROAD.GetUpperBound(0) + 1 + 1]);
									short num3 = (short)(Public2.PRJ.ROAD[IndexRoad].CoordWM.GetUpperBound(0) - i);
									ref Public2.CoordWebMerc[] coordWM2 = ref Public2.PRJ.ROAD[Public2.PRJ.ROAD.GetUpperBound(0)].CoordWM;
									coordWM2 = (Public2.CoordWebMerc[])Utils.CopyArray(coordWM2, new Public2.CoordWebMerc[num3 + 1]);
									Public2.PRJ.ROAD[Public2.PRJ.ROAD.GetUpperBound(0)].WidthBorder = Public2.PRJ.ROAD[IndexRoad].WidthBorder;
									Public2.PRJ.ROAD[Public2.PRJ.ROAD.GetUpperBound(0)].WidthM = Public2.PRJ.ROAD[IndexRoad].WidthM;
									int num4 = num3;
									for (int k = 0; k <= num4; k++)
									{
										Public2.PRJ.ROAD[Public2.PRJ.ROAD.GetUpperBound(0)].CoordWM[k] = Public2.PRJ.ROAD[IndexRoad].CoordWM[k + i];
									}
									float num5 = (float)Math.Atan2(Public2.PRJ.ROAD[IndexRoad].CoordWM[i - 1].Ywm - Public2.PRJ.ROAD[IndexRoad].CoordWM[i].Ywm, Public2.PRJ.ROAD[IndexRoad].CoordWM[i - 1].Xwm - Public2.PRJ.ROAD[IndexRoad].CoordWM[i].Xwm);
									Public2.PRJ.ROAD[IndexRoad].CoordWM[i].Xwm = Public2.PRJ.ROAD[IndexRoad].CoordWM[i].Xwm + Math.Cos(num5) * 5.0;
									Public2.PRJ.ROAD[IndexRoad].CoordWM[i].Ywm = Public2.PRJ.ROAD[IndexRoad].CoordWM[i].Ywm + Math.Sin(num5) * 5.0;
									ref Public2.CoordWebMerc[] coordWM3 = ref Public2.PRJ.ROAD[IndexRoad].CoordWM;
									coordWM3 = (Public2.CoordWebMerc[])Utils.CopyArray(coordWM3, new Public2.CoordWebMerc[i + 1]);
									Form1_Public.StartSplittingNodes = false;
									UC_Lines1.cmdSplitNode.BackColor = SystemColors.Control;
								}
							}
							else
							{
								FlagPick = true;
								IndexPNT = (short)i;
								DragStart = true;
							}
							return;
						}
						if (!Form1_Public.StartAddingNodes)
						{
							continue;
						}
						CalcCoords.ConvTileToWebMercMeters((double)e.X / 256.0 + (double)TileX, (double)e.Y / 256.0 + (double)TileY, ref CoordWM);
						if (i < Public2.PRJ.ROAD[IndexRoad].CoordWM.GetUpperBound(0) && Module1.PointLine(Public2.PRJ.ROAD[IndexRoad].CoordWM[i], Public2.PRJ.ROAD[IndexRoad].CoordWM[i + 1], CoordWM))
						{
							ref Public2.CoordWebMerc[] coordWM4 = ref Public2.PRJ.ROAD[IndexRoad].CoordWM;
							coordWM4 = (Public2.CoordWebMerc[])Utils.CopyArray(coordWM4, new Public2.CoordWebMerc[Public2.PRJ.ROAD[IndexRoad].CoordWM.GetUpperBound(0) + 1 + 1]);
							int upperBound2 = Public2.PRJ.ROAD[IndexRoad].CoordWM.GetUpperBound(0);
							int num6 = i + 1;
							for (int l = upperBound2; l >= num6; l += -1)
							{
								Public2.PRJ.ROAD[IndexRoad].CoordWM[l] = Public2.PRJ.ROAD[IndexRoad].CoordWM[l - 1];
							}
							Public2.PRJ.ROAD[IndexRoad].CoordWM[i + 1] = CoordWM;
							return;
						}
					}
				}
				else if (IndexFence > -1)
				{
					int upperBound3 = Public2.PRJ.FENCE[IndexFence].CoordWM.GetUpperBound(0);
					Public2.CoordWebMerc CoordWM2 = default;
					for (int m = 0; m <= upperBound3; m++)
					{
						CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.FENCE[IndexFence].CoordWM[m], ref tmpTileX1, ref tmpTileY1);
						LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
						LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
						if ((Math.Abs(LineX1 - e.X) < 5) & (Math.Abs(LineY1 - e.Y) < 5))
						{
							if (Form1_Public.StartDeletingNodes)
							{
								if (Public2.PRJ.FENCE[IndexFence].CoordWM.GetUpperBound(0) > 1)
								{
									int num7 = m;
									int num8 = Public2.PRJ.FENCE[IndexFence].CoordWM.GetUpperBound(0) - 1;
									for (int n = num7; n <= num8; n++)
									{
										Public2.PRJ.FENCE[IndexFence].CoordWM[n] = Public2.PRJ.FENCE[IndexFence].CoordWM[n + 1];
									}
									ref Public2.CoordWebMerc[] coordWM5 = ref Public2.PRJ.FENCE[IndexFence].CoordWM;
									coordWM5 = (Public2.CoordWebMerc[])Utils.CopyArray(coordWM5, new Public2.CoordWebMerc[Public2.PRJ.FENCE[IndexFence].CoordWM.GetUpperBound(0) - 1 + 1]);
								}
							}
							else if (Form1_Public.StartSplittingNodes)
							{
								if ((m > 0) & (m < Public2.PRJ.FENCE[IndexFence].CoordWM.GetUpperBound(0)))
								{
									ref Public2.StructFence[] fENCE = ref Public2.PRJ.FENCE;
									fENCE = (Public2.StructFence[])Utils.CopyArray(fENCE, new Public2.StructFence[Public2.PRJ.FENCE.GetUpperBound(0) + 1 + 1]);
									short num9 = (short)(Public2.PRJ.FENCE[IndexFence].CoordWM.GetUpperBound(0) - m);
									ref Public2.CoordWebMerc[] coordWM6 = ref Public2.PRJ.FENCE[Public2.PRJ.FENCE.GetUpperBound(0)].CoordWM;
									coordWM6 = (Public2.CoordWebMerc[])Utils.CopyArray(coordWM6, new Public2.CoordWebMerc[num9 + 1]);
									Public2.PRJ.FENCE[Public2.PRJ.FENCE.GetUpperBound(0)].Type = Public2.PRJ.FENCE[IndexFence].Type;
									Public2.PRJ.FENCE[Public2.PRJ.FENCE.GetUpperBound(0)].Width = Public2.PRJ.FENCE[IndexFence].Width;
									int num10 = num9;
									for (int num11 = 0; num11 <= num10; num11++)
									{
										Public2.PRJ.FENCE[Public2.PRJ.FENCE.GetUpperBound(0)].CoordWM[num11] = Public2.PRJ.FENCE[IndexFence].CoordWM[num11 + m];
									}
									float num12 = (float)Math.Atan2(Public2.PRJ.FENCE[IndexFence].CoordWM[m - 1].Ywm - Public2.PRJ.FENCE[IndexFence].CoordWM[m].Ywm, Public2.PRJ.FENCE[IndexFence].CoordWM[m - 1].Xwm - Public2.PRJ.FENCE[IndexFence].CoordWM[m].Xwm);
									Public2.PRJ.FENCE[IndexFence].CoordWM[m].Xwm = Public2.PRJ.FENCE[IndexFence].CoordWM[m].Xwm + Math.Cos(num12) * 5.0;
									Public2.PRJ.FENCE[IndexFence].CoordWM[m].Ywm = Public2.PRJ.FENCE[IndexFence].CoordWM[m].Ywm + Math.Sin(num12) * 5.0;
									ref Public2.CoordWebMerc[] coordWM7 = ref Public2.PRJ.FENCE[IndexFence].CoordWM;
									coordWM7 = (Public2.CoordWebMerc[])Utils.CopyArray(coordWM7, new Public2.CoordWebMerc[m + 1]);
									Form1_Public.StartSplittingNodes = false;
									UC_Lines1.cmdSplitNode.BackColor = SystemColors.Control;
								}
							}
							else
							{
								FlagPick = true;
								IndexPNT = (short)m;
								DragStart = true;
							}
							return;
						}
						if (!Form1_Public.StartAddingNodes)
						{
							continue;
						}
						CalcCoords.ConvTileToWebMercMeters((double)e.X / 256.0 + (double)TileX, (double)e.Y / 256.0 + (double)TileY, ref CoordWM2);
						if (m < Public2.PRJ.FENCE[IndexFence].CoordWM.GetUpperBound(0) && Module1.PointLine(Public2.PRJ.FENCE[IndexFence].CoordWM[m], Public2.PRJ.FENCE[IndexFence].CoordWM[m + 1], CoordWM2))
						{
							ref Public2.CoordWebMerc[] coordWM8 = ref Public2.PRJ.FENCE[IndexFence].CoordWM;
							coordWM8 = (Public2.CoordWebMerc[])Utils.CopyArray(coordWM8, new Public2.CoordWebMerc[Public2.PRJ.FENCE[IndexFence].CoordWM.GetUpperBound(0) + 1 + 1]);
							int upperBound4 = Public2.PRJ.FENCE[IndexFence].CoordWM.GetUpperBound(0);
							int num13 = m + 1;
							for (int num14 = upperBound4; num14 >= num13; num14 += -1)
							{
								Public2.PRJ.FENCE[IndexFence].CoordWM[num14] = Public2.PRJ.FENCE[IndexFence].CoordWM[num14 - 1];
							}
							Public2.PRJ.FENCE[IndexFence].CoordWM[m + 1] = CoordWM2;
							return;
						}
					}
				}
				IndexText = -1;
				IndexRoad = -1;
				IndexFence = -1;
				if (!Information.IsNothing(Public2.PRJ.TXT))
				{
					int upperBound5 = Public2.PRJ.TXT.GetUpperBound(0);
					for (int num15 = 0; num15 <= upperBound5; num15++)
					{
						stringSize = Form1_Public.MeasureString(Public2.PRJ.TXT[num15].Text, new Font(Public2.PRJ.TXT[num15].FontName, unchecked((int)Public2.PRJ.TXT[num15].Size), FontStyle.Regular));
						CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.TXT[num15].CoordWM, ref tmpTileX1, ref tmpTileY1);
						LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
						LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
						if ((LineX1 - 10 < e.X) & ((float)LineX1 + stringSize.Width + 10f > (float)e.X) & ((float)LineY1 + stringSize.Height + 5f > (float)e.Y) & ((float)LineY1 - stringSize.Height - 5f < (float)e.Y))
						{
							FlagPick = true;
							IndexText = (short)num15;
							PickPoint.Xpix = (short)e.X;
							PickPoint.Ypix = (short)e.Y;
							PickText = Module1.DeepClone(Public2.PRJ.TXT[num15]);
							UC_Lines1.gbRoad.Visible = false;
							UC_Lines1.gbFence.Visible = false;
							UC_Lines1.gbText.Visible = true;
							UC_Lines1.txtText.Text = Public2.PRJ.TXT[IndexText].Text;
							UC_Lines1.cbFont.Text = Public2.PRJ.TXT[IndexText].FontName;
							UC_Lines1.txtFontSize.Value = new decimal(Public2.PRJ.TXT[IndexText].Size);
							UC_Lines1.chkShowPoint.Checked = Public2.PRJ.TXT[IndexText].Point;
							UC_Lines1.tbAngle.Value = Public2.PRJ.TXT[IndexText].Angle;
							if (!Information.IsNothing(Public2.PRJ.TXT[IndexText].Icon))
							{
								UC_Lines1.cmdIcon.Image = Public2.PRJ.TXT[IndexText].Icon;
							}
							else
							{
								UC_Lines1.cmdIcon.Image = null;
							}
							DragStart = true;
							Form1_Public.StartDeletingNodes = false;
							Form1_Public.StartAddingNodes = false;
							Form1_Public.StartSplittingNodes = false;
							UC_Lines1.cmdDeleteNode.BackColor = SystemColors.Control;
							UC_Lines1.cmdAddNode.BackColor = SystemColors.Control;
							UC_Lines1.cmdSplitNode.BackColor = SystemColors.Control;
							return;
						}
					}
				}
				if (!Information.IsNothing(Public2.PRJ.ROAD))
				{
					Public2.CoordWebMerc CoordWM3 = default;
					CalcCoords.ConvTileToWebMercMeters((double)e.X / 256.0 + (double)TileX, (double)e.Y / 256.0 + (double)TileY, ref CoordWM3);
					int upperBound6 = Public2.PRJ.ROAD.GetUpperBound(0);
					for (int num16 = 0; num16 <= upperBound6; num16++)
					{
						int num17 = Public2.PRJ.ROAD[num16].CoordWM.GetUpperBound(0) - 1;
						for (int num18 = 0; num18 <= num17; num18++)
						{
							if (Module1.PointLine(Public2.PRJ.ROAD[num16].CoordWM[num18], Public2.PRJ.ROAD[num16].CoordWM[num18 + 1], CoordWM3))
							{
								FlagPick = true;
								IndexRoad = (short)num16;
								PickPoint.Xpix = (short)e.X;
								PickPoint.Ypix = (short)e.Y;
								PickROAD = Module1.DeepClone(Public2.PRJ.ROAD[num16]);
								UC_Lines1.gbRoad.Visible = true;
								UC_Lines1.gbFence.Visible = false;
								UC_Lines1.gbText.Visible = false;
								UC_Lines1.txtRoadWidth.Text = Conversions.ToString(Public2.PRJ.ROAD[IndexRoad].WidthM);
								UC_Lines1.txtWidthBorder.Text = Conversions.ToString(Public2.PRJ.ROAD[IndexRoad].WidthBorder);
								DragStart = true;
								Form1_Public.StartDeletingNodes = false;
								Form1_Public.StartAddingNodes = false;
								Form1_Public.StartSplittingNodes = false;
								UC_Lines1.cmdDeleteNode.BackColor = SystemColors.Control;
								UC_Lines1.cmdAddNode.BackColor = SystemColors.Control;
								UC_Lines1.cmdSplitNode.BackColor = SystemColors.Control;
								return;
							}
						}
					}
				}
				if (!Information.IsNothing(Public2.PRJ.FENCE))
				{
					Public2.CoordWebMerc CoordWM4 = default;
					CalcCoords.ConvTileToWebMercMeters((double)e.X / 256.0 + (double)TileX, (double)e.Y / 256.0 + (double)TileY, ref CoordWM4);
					int upperBound7 = Public2.PRJ.FENCE.GetUpperBound(0);
					for (int num19 = 0; num19 <= upperBound7; num19++)
					{
						int num20 = Public2.PRJ.FENCE[num19].CoordWM.GetUpperBound(0) - 1;
						for (int num21 = 0; num21 <= num20; num21++)
						{
							if (Module1.PointLine(Public2.PRJ.FENCE[num19].CoordWM[num21], Public2.PRJ.FENCE[num19].CoordWM[num21 + 1], CoordWM4))
							{
								FlagPick = true;
								IndexFence = (short)num19;
								PickPoint.Xpix = (short)e.X;
								PickPoint.Ypix = (short)e.Y;
								PickFence = Module1.DeepClone(Public2.PRJ.FENCE[num19]);
								UC_Lines1.gbRoad.Visible = false;
								UC_Lines1.gbFence.Visible = true;
								UC_Lines1.gbText.Visible = false;
								UC_Lines1.pbFenceLineType.Image = ilICO.Images[Public2.PRJ.FENCE[IndexFence].Type];
								UC_Lines1.txtFenceWidth.Value = new decimal(Public2.PRJ.FENCE[IndexFence].Width);
								DragStart = true;
								Form1_Public.StartDeletingNodes = false;
								Form1_Public.StartAddingNodes = false;
								Form1_Public.StartSplittingNodes = false;
								UC_Lines1.cmdDeleteNode.BackColor = SystemColors.Control;
								UC_Lines1.cmdAddNode.BackColor = SystemColors.Control;
								UC_Lines1.cmdSplitNode.BackColor = SystemColors.Control;
								return;
							}
						}
					}
				}
				Form1_Public.StartDeletingNodes = false;
				Form1_Public.StartAddingNodes = false;
				Form1_Public.StartSplittingNodes = false;
				UC_Lines1.cmdDeleteNode.BackColor = SystemColors.Control;
				UC_Lines1.cmdAddNode.BackColor = SystemColors.Control;
				UC_Lines1.cmdSplitNode.BackColor = SystemColors.Control;
				UC_Lines1.gbRoad.Visible = false;
				UC_Lines1.gbFence.Visible = false;
				UC_Lines1.gbText.Visible = false;
			}
			else if (Form1_Public.StartDrawingRoad)
			{
				if (Information.IsNothing(Form1_Public.NewRoad.CoordWM))
				{
					Module1.LineBackUp();
					Form1_Public.NewRoad.CoordWM = new Public2.CoordWebMerc[1];
				}
				else
				{
					ref Public2.CoordWebMerc[] coordWM9 = ref Form1_Public.NewRoad.CoordWM;
					coordWM9 = (Public2.CoordWebMerc[])Utils.CopyArray(coordWM9, new Public2.CoordWebMerc[Form1_Public.NewRoad.CoordWM.GetUpperBound(0) + 1 + 1]);
				}
				CalcCoords.ConvTileToWebMercMeters((double)e.X / 256.0 + (double)TileX, (double)e.Y / 256.0 + (double)TileY, ref Form1_Public.NewRoad.CoordWM[Form1_Public.NewRoad.CoordWM.GetUpperBound(0)]);
				PB.Refresh();
			}
			else if (Form1_Public.StartDrawingFence)
			{
				if (Information.IsNothing(Form1_Public.NewFence.CoordWM))
				{
					Module1.LineBackUp();
					Form1_Public.NewFence.CoordWM = new Public2.CoordWebMerc[1];
				}
				else
				{
					ref Public2.CoordWebMerc[] coordWM10 = ref Form1_Public.NewFence.CoordWM;
					coordWM10 = (Public2.CoordWebMerc[])Utils.CopyArray(coordWM10, new Public2.CoordWebMerc[Form1_Public.NewFence.CoordWM.GetUpperBound(0) + 1 + 1]);
				}
				CalcCoords.ConvTileToWebMercMeters((double)e.X / 256.0 + (double)TileX, (double)e.Y / 256.0 + (double)TileY, ref Form1_Public.NewFence.CoordWM[Form1_Public.NewFence.CoordWM.GetUpperBound(0)]);
				PB.Refresh();
			}
			else if (Form1_Public.StartDrawingText)
			{
				Module1.LineBackUp();
				if (Information.IsNothing(Public2.PRJ.TXT))
				{
					Public2.PRJ.TXT = new Public2.StructText[1];
				}
				else
				{
					ref Public2.StructText[] tXT = ref Public2.PRJ.TXT;
					tXT = (Public2.StructText[])Utils.CopyArray(tXT, new Public2.StructText[Public2.PRJ.TXT.GetUpperBound(0) + 1 + 1]);
				}
				IndexText = (short)Public2.PRJ.TXT.GetUpperBound(0);
				CalcCoords.ConvTileToWebMercMeters((double)e.X / 256.0 + (double)TileX, (double)e.Y / 256.0 + (double)TileY, ref Public2.PRJ.TXT[Public2.PRJ.TXT.GetUpperBound(0)].CoordWM);
				Public2.PRJ.TXT[Public2.PRJ.TXT.GetUpperBound(0)].Text = Form1_Public.LastText;
				Public2.PRJ.TXT[Public2.PRJ.TXT.GetUpperBound(0)].Size = 8;
				Public2.PRJ.TXT[Public2.PRJ.TXT.GetUpperBound(0)].FontName = "Arial";
				Public2.PRJ.TXT[Public2.PRJ.TXT.GetUpperBound(0)].Point = true;
				Public2.PRJ.TXT[Public2.PRJ.TXT.GetUpperBound(0)].Angle = 0;
				if (!Information.IsNothing(Form1_Public.LastIcon))
				{
					Public2.PRJ.TXT[Public2.PRJ.TXT.GetUpperBound(0)].Icon = (Bitmap)Form1_Public.LastIcon;
				}
				PB.Refresh();
				UC_Lines1.gbRoad.Visible = false;
				UC_Lines1.gbFence.Visible = false;
				UC_Lines1.gbText.Visible = true;
				UC_Lines1.txtText.Text = Public2.PRJ.TXT[IndexText].Text;
				UC_Lines1.cbFont.Text = Public2.PRJ.TXT[IndexText].FontName;
				UC_Lines1.txtFontSize.Value = new decimal(Public2.PRJ.TXT[IndexText].Size);
				UC_Lines1.chkShowPoint.Checked = Public2.PRJ.TXT[IndexText].Point;
				UC_Lines1.tbAngle.Value = Public2.PRJ.TXT[IndexText].Angle;
				if (!Information.IsNothing(Form1_Public.LastIcon))
				{
					UC_Lines1.cmdIcon.Image = Form1_Public.LastIcon;
				}
			}
		}
	}

	public void LineDrag(MouseEventArgs e)
	{
		if (!FlagPick)
		{
			return;
		}
		checked
		{
			if (IndexRoad > -1)
			{
				if (IndexPNT >= 0)
				{
					if (DragStart)
					{
						Module1.LineBackUp();
						DragStart = false;
					}
					Public2.CoordWebMerc CoordWM = default;
					CalcCoords.ConvTileToWebMercMeters((double)TileX + (double)e.X / 256.0, (double)TileY + (double)e.Y / 256.0, ref CoordWM);
					Public2.PRJ.ROAD[IndexRoad].CoordWM[IndexPNT] = CoordWM;
				}
				else
				{
					if (DragStart)
					{
						Module1.LineBackUp();
						DragStart = false;
					}
					int num = e.X - PickPoint.Xpix;
					int num2 = e.Y - PickPoint.Ypix;
					double num3 = CalcCoords.CalcMashtWMtoPixel();
					int upperBound = Public2.PRJ.ROAD[IndexRoad].CoordWM.GetUpperBound(0);
					for (int i = 0; i <= upperBound; i++)
					{
						Public2.PRJ.ROAD[IndexRoad].CoordWM[i].Xwm = PickROAD.CoordWM[i].Xwm + (double)num / num3;
						Public2.PRJ.ROAD[IndexRoad].CoordWM[i].Ywm = PickROAD.CoordWM[i].Ywm - (double)num2 / num3;
					}
				}
			}
			else if (IndexFence > -1)
			{
				if (IndexPNT >= 0)
				{
					if (DragStart)
					{
						Module1.LineBackUp();
						DragStart = false;
					}
					Public2.CoordWebMerc CoordWM2 = default;
					CalcCoords.ConvTileToWebMercMeters((double)TileX + (double)e.X / 256.0, (double)TileY + (double)e.Y / 256.0, ref CoordWM2);
					Public2.PRJ.FENCE[IndexFence].CoordWM[IndexPNT] = CoordWM2;
				}
				else
				{
					if (DragStart)
					{
						Module1.LineBackUp();
						DragStart = false;
					}
					int num4 = e.X - PickPoint.Xpix;
					int num5 = e.Y - PickPoint.Ypix;
					double num6 = CalcCoords.CalcMashtWMtoPixel();
					int upperBound2 = Public2.PRJ.FENCE[IndexFence].CoordWM.GetUpperBound(0);
					for (int j = 0; j <= upperBound2; j++)
					{
						Public2.PRJ.FENCE[IndexFence].CoordWM[j].Xwm = PickFence.CoordWM[j].Xwm + (double)num4 / num6;
						Public2.PRJ.FENCE[IndexFence].CoordWM[j].Ywm = PickFence.CoordWM[j].Ywm - (double)num5 / num6;
					}
				}
			}
			else if (IndexText > -1)
			{
				if (DragStart)
				{
					Module1.LineBackUp();
					DragStart = false;
				}
				int num7 = e.X - PickPoint.Xpix;
				int num8 = e.Y - PickPoint.Ypix;
				double num9 = CalcCoords.CalcMashtWMtoPixel();
				Public2.PRJ.TXT[IndexText].CoordWM.Xwm = PickText.CoordWM.Xwm + (double)num7 / num9;
				Public2.PRJ.TXT[IndexText].CoordWM.Ywm = PickText.CoordWM.Ywm - (double)num8 / num9;
			}
			PB.Refresh();
		}
	}

	public void LineDrop(MouseEventArgs e)
	{
		IndexPNT = -1;
		FlagPick = false;
		PB.Refresh();
	}

	public void LineEndCreating()
	{
		checked
		{
			if (Form1_Public.StartDrawingRoad)
			{
				if (!Information.IsNothing(Form1_Public.NewRoad.CoordWM) && Form1_Public.NewRoad.CoordWM.GetUpperBound(0) > 0)
				{
					if (Information.IsNothing(Public2.PRJ.ROAD))
					{
						Public2.PRJ.ROAD = new Public2.StructRoad[1];
					}
					else
					{
						ref Public2.StructRoad[] rOAD = ref Public2.PRJ.ROAD;
						rOAD = (Public2.StructRoad[])Utils.CopyArray(rOAD, new Public2.StructRoad[Public2.PRJ.ROAD.GetUpperBound(0) + 1 + 1]);
					}
					Public2.PRJ.ROAD[Public2.PRJ.ROAD.GetUpperBound(0)] = Module1.DeepClone(Form1_Public.NewRoad);
					Public2.PRJ.ROAD[Public2.PRJ.ROAD.GetUpperBound(0)].WidthM = 3;
					Public2.PRJ.ROAD[Public2.PRJ.ROAD.GetUpperBound(0)].WidthBorder = 2;
					Form1_Public.NewRoad = default;
					Form1_Public.StartDrawingRoad = false;
					UC_Lines1.cmdAddRoad.BackColor = SystemColors.Control;
					IndexRoad = (short)Public2.PRJ.ROAD.GetUpperBound(0);
					IndexPNT = -1;
					PB.Refresh();
					UC_Lines1.gbRoad.Visible = true;
					UC_Lines1.gbFence.Visible = false;
					UC_Lines1.gbText.Visible = false;
					UC_Lines1.txtRoadWidth.Text = Conversions.ToString(Public2.PRJ.ROAD[Public2.PRJ.ROAD.GetUpperBound(0)].WidthM);
					UC_Lines1.txtWidthBorder.Text = Conversions.ToString(Public2.PRJ.ROAD[Public2.PRJ.ROAD.GetUpperBound(0)].WidthBorder);
				}
			}
			else if (Form1_Public.StartDrawingFence)
			{
				if (!Information.IsNothing(Form1_Public.NewFence.CoordWM) && Form1_Public.NewFence.CoordWM.GetUpperBound(0) > 0)
				{
					if (Information.IsNothing(Public2.PRJ.FENCE))
					{
						Public2.PRJ.FENCE = new Public2.StructFence[1];
					}
					else
					{
						ref Public2.StructFence[] fENCE = ref Public2.PRJ.FENCE;
						fENCE = (Public2.StructFence[])Utils.CopyArray(fENCE, new Public2.StructFence[Public2.PRJ.FENCE.GetUpperBound(0) + 1 + 1]);
					}
					Public2.PRJ.FENCE[Public2.PRJ.FENCE.GetUpperBound(0)] = Module1.DeepClone(Form1_Public.NewFence);
					Public2.PRJ.FENCE[Public2.PRJ.FENCE.GetUpperBound(0)].Type = 8;
					Public2.PRJ.FENCE[Public2.PRJ.FENCE.GetUpperBound(0)].Width = 1;
					Form1_Public.NewFence = default;
					Form1_Public.StartDrawingFence = false;
					UC_Lines1.cmdAddFence.BackColor = SystemColors.Control;
					IndexFence = (short)Public2.PRJ.FENCE.GetUpperBound(0);
					IndexPNT = -1;
					PB.Refresh();
					UC_Lines1.gbFence.Visible = true;
					UC_Lines1.gbRoad.Visible = false;
					UC_Lines1.gbText.Visible = false;
					UC_Lines1.txtFenceWidth.Text = Conversions.ToString(Public2.PRJ.FENCE[Public2.PRJ.FENCE.GetUpperBound(0)].Width);
					UC_Lines1.pbFenceLineType.Image = ilICO.Images[Public2.PRJ.FENCE[Public2.PRJ.FENCE.GetUpperBound(0)].Type];
				}
			}
			else
			{
				UC_Lines1.cmdDeleteNode.BackColor = SystemColors.Control;
				UC_Lines1.cmdAddNode.BackColor = SystemColors.Control;
				UC_Lines1.cmdSplitNode.BackColor = SystemColors.Control;
				UC_Lines1.cmdAddText.BackColor = SystemColors.Control;
				Form1_Public.StartDrawingText = false;
				Form1_Public.StartDeletingNodes = false;
				Form1_Public.StartAddingNodes = false;
				Form1_Public.StartSplittingNodes = false;
				PB.Refresh();
			}
		}
	}

	public void ElevHover(MouseEventArgs e)
	{
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.ElLN) && IndexElevLine >= 0)
			{
				int upperBound = Public2.PRJ.ElLN[IndexElevLine].CoordWM.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.ElLN[IndexElevLine].CoordWM[i], ref tmpTileX1, ref tmpTileY1);
					LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					if ((Math.Abs(LineX1 - e.X) < 5) & (Math.Abs(LineY1 - e.Y) < 5))
					{
						PB.Cursor = Cursors.Hand;
						return;
					}
				}
			}
			if (!Information.IsNothing(Public2.PRJ.ElLN))
			{
				Public2.CoordWebMerc CoordWM = default;
				CalcCoords.ConvTileToWebMercMeters((double)e.X / 256.0 + (double)TileX, (double)e.Y / 256.0 + (double)TileY, ref CoordWM);
				int upperBound2 = Public2.PRJ.ElLN.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					int num = Public2.PRJ.ElLN[j].CoordWM.GetUpperBound(0) - 1;
					for (int k = 0; k <= num; k++)
					{
						if (Module1.PointLine(Public2.PRJ.ElLN[j].CoordWM[k], Public2.PRJ.ElLN[j].CoordWM[k + 1], CoordWM))
						{
							PB.Cursor = Cursors.Arrow;
							return;
						}
					}
					if (Public2.PRJ.ElLN[j].CloseLine && Module1.PointLine(Public2.PRJ.ElLN[j].CoordWM[0], Public2.PRJ.ElLN[j].CoordWM[Public2.PRJ.ElLN[j].CoordWM.GetUpperBound(0)], CoordWM))
					{
						PB.Cursor = Cursors.Arrow;
						return;
					}
				}
			}
			if (!Information.IsNothing(Public2.PRJ.ElPT))
			{
				Public2.CoordWebMerc CoordWM2 = default;
				CalcCoords.ConvTileToWebMercMeters((double)e.X / 256.0 + (double)TileX, (double)e.Y / 256.0 + (double)TileY, ref CoordWM2);
				int upperBound3 = Public2.PRJ.ElPT.GetUpperBound(0);
				for (int l = 0; l <= upperBound3; l++)
				{
					if ((Math.Abs(Public2.PRJ.ElPT[l].CoordWM.Xwm - CoordWM2.Xwm) < 2.0) & (Math.Abs(Public2.PRJ.ElPT[l].CoordWM.Ywm - CoordWM2.Ywm) < 2.0))
					{
						PB.Cursor = Cursors.Arrow;
						return;
					}
				}
			}
			if (Form1_Public.RulerOn)
			{
				int num2 = 0;
				do
				{
					CalcCoords.ConvWebMercMetersToTile(Form1_Public.Ruler[num2], ref tmpTileX1, ref tmpTileY1);
					LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
					LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
					if ((Math.Abs(LineX1 - e.X) < 4) & (Math.Abs(LineY1 - e.Y) < 4))
					{
						PB.Cursor = Cursors.Arrow;
						return;
					}
					num2++;
				}
				while (num2 <= 1);
			}
			PB.Cursor = Cursors.Cross;
		}
	}

	public void ElevPick(MouseEventArgs e)
	{
		checked
		{
			if (!Form1_Public.StartDrawingElevLine & !Form1_Public.StartDrawingElevPoint)
			{
				FlagPick = false;
				IndexPNT = -1;
				if (IndexElevLine > -1)
				{
					int upperBound = Public2.PRJ.ElLN[IndexElevLine].CoordWM.GetUpperBound(0);
					Public2.CoordWebMerc CoordWM = default;
					for (int i = 0; i <= upperBound; i++)
					{
						CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.ElLN[IndexElevLine].CoordWM[i], ref tmpTileX1, ref tmpTileY1);
						LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
						LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
						if ((Math.Abs(LineX1 - e.X) < 5) & (Math.Abs(LineY1 - e.Y) < 5))
						{
							if (Form1_Public.StartDeletingNodes)
							{
								if (Public2.PRJ.ElLN[IndexElevLine].CoordWM.GetUpperBound(0) > 1)
								{
									int num = i;
									int num2 = Public2.PRJ.ElLN[IndexElevLine].CoordWM.GetUpperBound(0) - 1;
									for (int j = num; j <= num2; j++)
									{
										Public2.PRJ.ElLN[IndexElevLine].CoordWM[j] = Public2.PRJ.ElLN[IndexElevLine].CoordWM[j + 1];
									}
									ref Public2.CoordWebMerc[] coordWM = ref Public2.PRJ.ElLN[IndexElevLine].CoordWM;
									coordWM = (Public2.CoordWebMerc[])Utils.CopyArray(coordWM, new Public2.CoordWebMerc[Public2.PRJ.ElLN[IndexElevLine].CoordWM.GetUpperBound(0) - 1 + 1]);
								}
							}
							else if (Form1_Public.StartSplittingNodes)
							{
								if ((i > 0) & (i < Public2.PRJ.ElLN[IndexElevLine].CoordWM.GetUpperBound(0)))
								{
									ref Public2.StructElevLine[] elLN = ref Public2.PRJ.ElLN;
									elLN = (Public2.StructElevLine[])Utils.CopyArray(elLN, new Public2.StructElevLine[Public2.PRJ.ElLN.GetUpperBound(0) + 1 + 1]);
									short num3 = (short)(Public2.PRJ.ElLN[IndexElevLine].CoordWM.GetUpperBound(0) - i);
									ref Public2.CoordWebMerc[] coordWM2 = ref Public2.PRJ.ElLN[Public2.PRJ.ElLN.GetUpperBound(0)].CoordWM;
									coordWM2 = (Public2.CoordWebMerc[])Utils.CopyArray(coordWM2, new Public2.CoordWebMerc[num3 + 1]);
									Public2.PRJ.ElLN[Public2.PRJ.ElLN.GetUpperBound(0)].H = Public2.PRJ.ElLN[IndexElevLine].H;
									int num4 = num3;
									for (int k = 0; k <= num4; k++)
									{
										Public2.PRJ.ElLN[Public2.PRJ.ElLN.GetUpperBound(0)].CoordWM[k] = Public2.PRJ.ElLN[IndexElevLine].CoordWM[k + i];
									}
									float num5 = (float)Math.Atan2(Public2.PRJ.ElLN[IndexElevLine].CoordWM[i - 1].Ywm - Public2.PRJ.ElLN[IndexElevLine].CoordWM[i].Ywm, Public2.PRJ.ElLN[IndexElevLine].CoordWM[i - 1].Xwm - Public2.PRJ.ElLN[IndexElevLine].CoordWM[i].Xwm);
									Public2.PRJ.ElLN[IndexElevLine].CoordWM[i].Xwm = Public2.PRJ.ElLN[IndexElevLine].CoordWM[i].Xwm + Math.Cos(num5) * 5.0;
									Public2.PRJ.ElLN[IndexElevLine].CoordWM[i].Ywm = Public2.PRJ.ElLN[IndexElevLine].CoordWM[i].Ywm + Math.Sin(num5) * 5.0;
									ref Public2.CoordWebMerc[] coordWM3 = ref Public2.PRJ.ElLN[IndexElevLine].CoordWM;
									coordWM3 = (Public2.CoordWebMerc[])Utils.CopyArray(coordWM3, new Public2.CoordWebMerc[i + 1]);
									Form1_Public.StartSplittingNodes = false;
								}
							}
							else
							{
								FlagPick = true;
								IndexPNT = (short)i;
								DragStart = true;
							}
							return;
						}
						if (!Form1_Public.StartAddingNodes)
						{
							continue;
						}
						CalcCoords.ConvTileToWebMercMeters((double)e.X / 256.0 + (double)TileX, (double)e.Y / 256.0 + (double)TileY, ref CoordWM);
						if (i < Public2.PRJ.ElLN[IndexElevLine].CoordWM.GetUpperBound(0) && Module1.PointLine(Public2.PRJ.ElLN[IndexElevLine].CoordWM[i], Public2.PRJ.ElLN[IndexElevLine].CoordWM[i + 1], CoordWM))
						{
							ref Public2.CoordWebMerc[] coordWM4 = ref Public2.PRJ.ElLN[IndexElevLine].CoordWM;
							coordWM4 = (Public2.CoordWebMerc[])Utils.CopyArray(coordWM4, new Public2.CoordWebMerc[Public2.PRJ.ElLN[IndexElevLine].CoordWM.GetUpperBound(0) + 1 + 1]);
							int upperBound2 = Public2.PRJ.ElLN[IndexElevLine].CoordWM.GetUpperBound(0);
							int num6 = i + 1;
							for (int l = upperBound2; l >= num6; l += -1)
							{
								Public2.PRJ.ElLN[IndexElevLine].CoordWM[l] = Public2.PRJ.ElLN[IndexElevLine].CoordWM[l - 1];
							}
							Public2.PRJ.ElLN[IndexElevLine].CoordWM[i + 1] = CoordWM;
							return;
						}
					}
				}
				IndexElevLine = -1;
				IndexElevPoint = -1;
				if (!Information.IsNothing(Public2.PRJ.ElPT))
				{
					int upperBound3 = Public2.PRJ.ElPT.GetUpperBound(0);
					for (int m = 0; m <= upperBound3; m++)
					{
						CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.ElPT[m].CoordWM, ref tmpTileX1, ref tmpTileY1);
						LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
						LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
						if ((Math.Abs(LineX1 - e.X) < 7) & (Math.Abs(LineY1 - e.Y) < 7))
						{
							FlagPick = true;
							IndexElevPoint = (short)m;
							UC_Relief1.gbElev.Visible = true;
							UC_Relief1.chkCloseLine.Visible = false;
							UC_Relief1.txtHeight.Text = Conversions.ToString(Public2.PRJ.ElPT[IndexElevPoint].H);
							DragStart = true;
							Form1_Public.StartDeletingNodes = false;
							Form1_Public.StartAddingNodes = false;
							UC_Relief1.cmdDeleteNode.BackColor = SystemColors.Control;
							UC_Relief1.cmdAddNode.BackColor = SystemColors.Control;
							return;
						}
					}
				}
				if (!Information.IsNothing(Public2.PRJ.ElLN))
				{
					Public2.CoordWebMerc CoordWM2 = default;
					CalcCoords.ConvTileToWebMercMeters((double)e.X / 256.0 + (double)TileX, (double)e.Y / 256.0 + (double)TileY, ref CoordWM2);
					int upperBound4 = Public2.PRJ.ElLN.GetUpperBound(0);
					for (int n = 0; n <= upperBound4; n++)
					{
						int num7 = Public2.PRJ.ElLN[n].CoordWM.GetUpperBound(0) - 1;
						for (int num8 = 0; num8 <= num7; num8++)
						{
							if (Module1.PointLine(Public2.PRJ.ElLN[n].CoordWM[num8], Public2.PRJ.ElLN[n].CoordWM[num8 + 1], CoordWM2))
							{
								FlagPick = true;
								IndexElevLine = (short)n;
								PickPoint.Xpix = (short)e.X;
								PickPoint.Ypix = (short)e.Y;
								PickElevLine = Module1.DeepClone(Public2.PRJ.ElLN[n]);
								UC_Relief1.gbElev.Visible = true;
								UC_Relief1.chkCloseLine.Visible = true;
								UC_Relief1.txtHeight.Text = Conversions.ToString(Public2.PRJ.ElLN[IndexElevLine].H);
								UC_Relief1.chkCloseLine.Checked = Public2.PRJ.ElLN[IndexElevLine].CloseLine;
								DragStart = true;
								Form1_Public.StartDeletingNodes = false;
								Form1_Public.StartAddingNodes = false;
								UC_Relief1.cmdDeleteNode.BackColor = SystemColors.Control;
								UC_Relief1.cmdAddNode.BackColor = SystemColors.Control;
								return;
							}
						}
						if (Public2.PRJ.ElLN[n].CloseLine && Module1.PointLine(Public2.PRJ.ElLN[n].CoordWM[0], Public2.PRJ.ElLN[n].CoordWM[Public2.PRJ.ElLN[n].CoordWM.GetUpperBound(0)], CoordWM2))
						{
							FlagPick = true;
							IndexElevLine = (short)n;
							PickPoint.Xpix = (short)e.X;
							PickPoint.Ypix = (short)e.Y;
							PickElevLine = Module1.DeepClone(Public2.PRJ.ElLN[n]);
							UC_Relief1.gbElev.Visible = true;
							UC_Relief1.chkCloseLine.Visible = true;
							UC_Relief1.txtHeight.Text = Conversions.ToString(Public2.PRJ.ElLN[IndexElevLine].H);
							UC_Relief1.chkCloseLine.Checked = Public2.PRJ.ElLN[IndexElevLine].CloseLine;
							DragStart = true;
							Form1_Public.StartDeletingNodes = false;
							Form1_Public.StartAddingNodes = false;
							UC_Relief1.cmdDeleteNode.BackColor = SystemColors.Control;
							UC_Relief1.cmdAddNode.BackColor = SystemColors.Control;
							return;
						}
					}
				}
				Form1_Public.StartDeletingNodes = false;
				Form1_Public.StartAddingNodes = false;
				Form1_Public.StartSplittingNodes = false;
				UC_Relief1.cmdDeleteNode.BackColor = SystemColors.Control;
				UC_Relief1.cmdAddNode.BackColor = SystemColors.Control;
				UC_Relief1.gbElev.Visible = false;
			}
			else if (Form1_Public.StartDrawingElevLine)
			{
				if (Information.IsNothing(Form1_Public.NewElevLine.CoordWM))
				{
					Module1.ElevBackUp();
					Form1_Public.NewElevLine.CoordWM = new Public2.CoordWebMerc[1];
				}
				else
				{
					ref Public2.CoordWebMerc[] coordWM5 = ref Form1_Public.NewElevLine.CoordWM;
					coordWM5 = (Public2.CoordWebMerc[])Utils.CopyArray(coordWM5, new Public2.CoordWebMerc[Form1_Public.NewElevLine.CoordWM.GetUpperBound(0) + 1 + 1]);
				}
				CalcCoords.ConvTileToWebMercMeters((double)e.X / 256.0 + (double)TileX, (double)e.Y / 256.0 + (double)TileY, ref Form1_Public.NewElevLine.CoordWM[Form1_Public.NewElevLine.CoordWM.GetUpperBound(0)]);
				PB.Refresh();
			}
			else if (Form1_Public.StartDrawingElevPoint)
			{
				Module1.ElevBackUp();
				if (Information.IsNothing(Public2.PRJ.ElPT))
				{
					Public2.PRJ.ElPT = new Public2.StructElevPoint[1];
				}
				else
				{
					ref Public2.StructElevPoint[] elPT = ref Public2.PRJ.ElPT;
					elPT = (Public2.StructElevPoint[])Utils.CopyArray(elPT, new Public2.StructElevPoint[Public2.PRJ.ElPT.GetUpperBound(0) + 1 + 1]);
				}
				IndexElevPoint = (short)Public2.PRJ.ElPT.GetUpperBound(0);
				CalcCoords.ConvTileToWebMercMeters((double)e.X / 256.0 + (double)TileX, (double)e.Y / 256.0 + (double)TileY, ref Public2.PRJ.ElPT[Public2.PRJ.ElPT.GetUpperBound(0)].CoordWM);
				Public2.PRJ.ElPT[Public2.PRJ.ElPT.GetUpperBound(0)].H = 0f;
				PB.Refresh();
				UC_Relief1.gbElev.Visible = true;
				UC_Relief1.chkCloseLine.Visible = false;
				UC_Relief1.txtHeight.Text = Conversions.ToString(Public2.PRJ.ElPT[IndexElevPoint].H);
			}
		}
	}

	public void ElevDrag(MouseEventArgs e)
	{
		if (!FlagPick)
		{
			return;
		}
		checked
		{
			if (IndexElevLine > -1)
			{
				if (IndexPNT >= 0)
				{
					if (DragStart)
					{
						Module1.ElevBackUp();
						DragStart = false;
					}
					Public2.CoordWebMerc CoordWM = default;
					CalcCoords.ConvTileToWebMercMeters((double)TileX + (double)e.X / 256.0, (double)TileY + (double)e.Y / 256.0, ref CoordWM);
					Public2.PRJ.ElLN[IndexElevLine].CoordWM[IndexPNT] = CoordWM;
				}
				else
				{
					if (DragStart)
					{
						Module1.ElevBackUp();
						DragStart = false;
					}
					int num = e.X - PickPoint.Xpix;
					int num2 = e.Y - PickPoint.Ypix;
					double num3 = CalcCoords.CalcMashtWMtoPixel();
					int upperBound = Public2.PRJ.ElLN[IndexElevLine].CoordWM.GetUpperBound(0);
					for (int i = 0; i <= upperBound; i++)
					{
						Public2.PRJ.ElLN[IndexElevLine].CoordWM[i].Xwm = PickElevLine.CoordWM[i].Xwm + (double)num / num3;
						Public2.PRJ.ElLN[IndexElevLine].CoordWM[i].Ywm = PickElevLine.CoordWM[i].Ywm - (double)num2 / num3;
					}
				}
			}
			else if (IndexElevPoint > -1)
			{
				if (DragStart)
				{
					Module1.ElevBackUp();
					DragStart = false;
				}
				Public2.CoordWebMerc CoordWM2 = default;
				CalcCoords.ConvTileToWebMercMeters((double)TileX + (double)e.X / 256.0, (double)TileY + (double)e.Y / 256.0, ref CoordWM2);
				Public2.PRJ.ElPT[IndexElevPoint].CoordWM = CoordWM2;
			}
			PB.Refresh();
		}
	}

	public void ElevDrop(MouseEventArgs e)
	{
		IndexPNT = -1;
		FlagPick = false;
		PB.Refresh();
	}

	public void ElevEndCreating()
	{
		checked
		{
			if (Form1_Public.StartDrawingElevLine)
			{
				if (!Information.IsNothing(Form1_Public.NewElevLine.CoordWM) && Form1_Public.NewElevLine.CoordWM.GetUpperBound(0) > 0)
				{
					if (Information.IsNothing(Public2.PRJ.ElLN))
					{
						Public2.PRJ.ElLN = new Public2.StructElevLine[1];
					}
					else
					{
						ref Public2.StructElevLine[] elLN = ref Public2.PRJ.ElLN;
						elLN = (Public2.StructElevLine[])Utils.CopyArray(elLN, new Public2.StructElevLine[Public2.PRJ.ElLN.GetUpperBound(0) + 1 + 1]);
					}
					Public2.PRJ.ElLN[Public2.PRJ.ElLN.GetUpperBound(0)] = Module1.DeepClone(Form1_Public.NewElevLine);
					Public2.PRJ.ElLN[Public2.PRJ.ElLN.GetUpperBound(0)].H = 0f;
					Public2.PRJ.ElLN[Public2.PRJ.ElLN.GetUpperBound(0)].CloseLine = false;
					Form1_Public.NewElevLine = default;
					Form1_Public.StartDrawingElevLine = false;
					UC_Relief1.cmdAddLine.BackColor = SystemColors.Control;
					IndexElevLine = (short)Public2.PRJ.ElLN.GetUpperBound(0);
					IndexPNT = -1;
					PB.Refresh();
					UC_Relief1.gbElev.Visible = true;
					UC_Relief1.chkCloseLine.Visible = true;
					UC_Relief1.txtHeight.Text = Conversions.ToString(Public2.PRJ.ElLN[Public2.PRJ.ElLN.GetUpperBound(0)].H);
					UC_Relief1.chkCloseLine.Checked = Public2.PRJ.ElLN[Public2.PRJ.ElLN.GetUpperBound(0)].CloseLine;
				}
			}
			else
			{
				UC_Relief1.cmdSelect.PerformClick();
			}
		}
	}

	public void RulerHover(MouseEventArgs e)
	{
		int num = 0;
		checked
		{
			do
			{
				CalcCoords.ConvWebMercMetersToTile(Form1_Public.Ruler[num], ref tmpTileX1, ref tmpTileY1);
				LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
				LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
				if (num == 0)
				{
					if ((Math.Abs(LineX1 - e.X) < 16) & (Math.Abs(LineY1 - e.Y) < 16))
					{
						PB.Cursor = Cursors.Arrow;
						return;
					}
				}
				else if ((Math.Abs(LineX1 - e.X) < 4) & (Math.Abs(LineY1 - e.Y) < 4))
				{
					PB.Cursor = Cursors.Arrow;
					return;
				}
				num++;
			}
			while (num <= 1);
			PB.Cursor = Cursors.Cross;
		}
	}

	public void RulerPick(MouseEventArgs e)
	{
		FlagRulerPick = false;
		IndexRuler = -1;
		int num = 0;
		checked
		{
			do
			{
				CalcCoords.ConvWebMercMetersToTile(Form1_Public.Ruler[num], ref tmpTileX1, ref tmpTileY1);
				LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
				LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
				if ((Math.Abs(LineX1 - e.X) < 4) & (Math.Abs(LineY1 - e.Y) < 4))
				{
					IndexRuler = num;
					FlagRulerPick = true;
				}
				num++;
			}
			while (num <= 1);
		}
	}

	public void RulerDrag(MouseEventArgs e)
	{
		if (FlagRulerPick && IndexRuler >= 0)
		{
			Public2.CoordWebMerc CoordWM = default;
			CalcCoords.ConvTileToWebMercMeters((double)TileX + (double)e.X / 256.0, (double)TileY + (double)e.Y / 256.0, ref CoordWM);
			Form1_Public.Ruler[IndexRuler] = CoordWM;
			PB.Refresh();
		}
	}

	public void RulerDrop(MouseEventArgs e)
	{
		FlagRulerPick = false;
	}

	public void VertPlaneHover(MouseEventArgs e)
	{
		if (!Public2.PRJ.SETT.ShowVertPlaneLines || Information.IsNothing(Public2.PRJ.VP))
		{
			return;
		}
		int upperBound = Public2.PRJ.VP.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.VP[i].CoordWM, ref tmpTileX1, ref tmpTileY1);
				LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
				LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
				int num = (int)Math.Round((double)Public2.PRJ.VP[i].L1 * CalcCoords.CalcMashtMeterstoPixel());
				int num2 = (int)Math.Round((double)LineX1 + (double)num * Math.Sin((double)Public2.PRJ.VP[i].Azimuth / (180.0 / Math.PI)));
				int num3 = (int)Math.Round((double)LineY1 - (double)num * Math.Cos((double)Public2.PRJ.VP[i].Azimuth / (180.0 / Math.PI)));
				if (Module1.PointLineD(LineX1, LineY1, num2, num3, e.X, e.Y, 5.0))
				{
					PB.Cursor = Cursors.Arrow;
					return;
				}
			}
			PB.Cursor = Cursors.Cross;
		}
	}

	public void VertPlanePick(MouseEventArgs e)
	{
		IndexVertPlane = -1;
		if (!Public2.PRJ.SETT.ShowVertPlaneLines || Information.IsNothing(Public2.PRJ.VP))
		{
			return;
		}
		int upperBound = Public2.PRJ.VP.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.VP[i].CoordWM, ref tmpTileX1, ref tmpTileY1);
				LineX1 = (long)Math.Round((tmpTileX1 - (double)TileX) * 256.0);
				LineY1 = (long)Math.Round((tmpTileY1 - (double)TileY) * 256.0);
				int num = (int)Math.Round((double)Public2.PRJ.VP[i].L1 * CalcCoords.CalcMashtMeterstoPixel());
				int num2 = (int)Math.Round((double)LineX1 + (double)num * Math.Sin((double)Public2.PRJ.VP[i].Azimuth / (180.0 / Math.PI)));
				int num3 = (int)Math.Round((double)LineY1 - (double)num * Math.Cos((double)Public2.PRJ.VP[i].Azimuth / (180.0 / Math.PI)));
				if (Module1.PointLineD(LineX1, LineY1, num2, num3, e.X, e.Y, 5.0))
				{
					IndexVertPlane = i;
					MyProject.Forms.frmQuickVertPlane.ShowDialog();
					break;
				}
			}
		}
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		checked
		{
			if ((Operators.CompareString(Form1_Public.NodeName, "Buildings", TextCompare: false) == 0) & (IndexBLD > -1))
			{
				double num = 1.0 / CalcCoords.CalcMashtWMtoPixel();
				if (msg.WParam.ToInt32() == 46)
				{
					UC_Buildings1.cmdDelOneBuilding.PerformClick();
				}
				else if (msg.WParam.ToInt32() == 39)
				{
					int upperBound = Public2.PRJ.BLD[IndexBLD].CoordWM.GetUpperBound(0);
					for (int i = 0; i <= upperBound; i++)
					{
						Public2.PRJ.BLD[IndexBLD].CoordWM[i].Xwm = Public2.PRJ.BLD[IndexBLD].CoordWM[i].Xwm + num;
					}
					PB.Refresh();
				}
				else if (msg.WParam.ToInt32() == 37)
				{
					int upperBound2 = Public2.PRJ.BLD[IndexBLD].CoordWM.GetUpperBound(0);
					for (int j = 0; j <= upperBound2; j++)
					{
						Public2.PRJ.BLD[IndexBLD].CoordWM[j].Xwm = Public2.PRJ.BLD[IndexBLD].CoordWM[j].Xwm - num;
					}
					PB.Refresh();
				}
				else if (msg.WParam.ToInt32() == 38)
				{
					int upperBound3 = Public2.PRJ.BLD[IndexBLD].CoordWM.GetUpperBound(0);
					for (int k = 0; k <= upperBound3; k++)
					{
						Public2.PRJ.BLD[IndexBLD].CoordWM[k].Ywm = Public2.PRJ.BLD[IndexBLD].CoordWM[k].Ywm + num;
					}
					PB.Refresh();
				}
				else if (msg.WParam.ToInt32() == 40)
				{
					int upperBound4 = Public2.PRJ.BLD[IndexBLD].CoordWM.GetUpperBound(0);
					for (int l = 0; l <= upperBound4; l++)
					{
						Public2.PRJ.BLD[IndexBLD].CoordWM[l].Ywm = Public2.PRJ.BLD[IndexBLD].CoordWM[l].Ywm - num;
					}
					PB.Refresh();
				}
			}
			else if ((Operators.CompareString(Form1_Public.NodeName, "Lines", TextCompare: false) == 0) & ((IndexRoad > -1) | (IndexFence > -1) | (IndexText > -1)))
			{
				double num2 = 1.0 / CalcCoords.CalcMashtWMtoPixel();
				if (msg.WParam.ToInt32() == 46)
				{
					UC_Lines1.cmdDelOneObject.PerformClick();
				}
				else if (msg.WParam.ToInt32() == 39)
				{
					if (IndexRoad > -1)
					{
						int upperBound5 = Public2.PRJ.ROAD[IndexRoad].CoordWM.GetUpperBound(0);
						for (int m = 0; m <= upperBound5; m++)
						{
							Public2.PRJ.ROAD[IndexRoad].CoordWM[m].Xwm = Public2.PRJ.ROAD[IndexRoad].CoordWM[m].Xwm + num2;
						}
						PB.Refresh();
					}
					else if (IndexFence > -1)
					{
						int upperBound6 = Public2.PRJ.FENCE[IndexFence].CoordWM.GetUpperBound(0);
						for (int n = 0; n <= upperBound6; n++)
						{
							Public2.PRJ.FENCE[IndexFence].CoordWM[n].Xwm = Public2.PRJ.FENCE[IndexFence].CoordWM[n].Xwm + num2;
						}
						PB.Refresh();
					}
				}
				else if (msg.WParam.ToInt32() == 37)
				{
					if (IndexRoad > -1)
					{
						int upperBound7 = Public2.PRJ.ROAD[IndexRoad].CoordWM.GetUpperBound(0);
						for (int num3 = 0; num3 <= upperBound7; num3++)
						{
							Public2.PRJ.ROAD[IndexRoad].CoordWM[num3].Xwm = Public2.PRJ.ROAD[IndexRoad].CoordWM[num3].Xwm - num2;
						}
						PB.Refresh();
					}
					else if (IndexFence > -1)
					{
						int upperBound8 = Public2.PRJ.FENCE[IndexFence].CoordWM.GetUpperBound(0);
						for (int num4 = 0; num4 <= upperBound8; num4++)
						{
							Public2.PRJ.FENCE[IndexFence].CoordWM[num4].Xwm = Public2.PRJ.FENCE[IndexFence].CoordWM[num4].Xwm - num2;
						}
						PB.Refresh();
					}
				}
				else if (msg.WParam.ToInt32() == 38)
				{
					if (IndexRoad > -1)
					{
						int upperBound9 = Public2.PRJ.ROAD[IndexRoad].CoordWM.GetUpperBound(0);
						for (int num5 = 0; num5 <= upperBound9; num5++)
						{
							Public2.PRJ.ROAD[IndexRoad].CoordWM[num5].Ywm = Public2.PRJ.ROAD[IndexRoad].CoordWM[num5].Ywm + num2;
						}
						PB.Refresh();
					}
					else if (IndexFence > -1)
					{
						int upperBound10 = Public2.PRJ.FENCE[IndexFence].CoordWM.GetUpperBound(0);
						for (int num6 = 0; num6 <= upperBound10; num6++)
						{
							Public2.PRJ.FENCE[IndexFence].CoordWM[num6].Ywm = Public2.PRJ.FENCE[IndexFence].CoordWM[num6].Ywm + num2;
						}
						PB.Refresh();
					}
				}
				else if (msg.WParam.ToInt32() == 40)
				{
					if (IndexRoad > -1)
					{
						int upperBound11 = Public2.PRJ.ROAD[IndexRoad].CoordWM.GetUpperBound(0);
						for (int num7 = 0; num7 <= upperBound11; num7++)
						{
							Public2.PRJ.ROAD[IndexRoad].CoordWM[num7].Ywm = Public2.PRJ.ROAD[IndexRoad].CoordWM[num7].Ywm - num2;
						}
						PB.Refresh();
					}
					else if (IndexFence > -1)
					{
						int upperBound12 = Public2.PRJ.FENCE[IndexFence].CoordWM.GetUpperBound(0);
						for (int num8 = 0; num8 <= upperBound12; num8++)
						{
							Public2.PRJ.FENCE[IndexFence].CoordWM[num8].Ywm = Public2.PRJ.FENCE[IndexFence].CoordWM[num8].Ywm - num2;
						}
						PB.Refresh();
					}
				}
			}
			else if (((Operators.CompareString(Form1_Public.NodeName, "Relief", TextCompare: false) == 0) & ((IndexElevLine > -1) | (IndexElevPoint > -1))) && msg.WParam.ToInt32() == 46)
			{
				UC_Relief1.cmdDelOneObject.PerformClick();
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}
	}

	public void ShowCheckBoxes()
	{
		FlagProcessing = true;
		chkShowMap.Checked = Public2.PRJ.SETT.ShowMap;
		txtMapBrightness.Value = new decimal(Public2.PRJ.SETT.MapBrightness);
		chkShowOrigin.Checked = Public2.PRJ.SETT.ShowOrigin;
		chkShowAllBuildings.Checked = Public2.PRJ.SETT.ShowAllBuildings;
		chkShowBuildingText.Checked = Public2.PRJ.SETT.ShowBuildingText;
		chkShowBuildingHeight.Checked = Public2.PRJ.SETT.ShowBuildingHeight;
		chkShowBuildingFoundation.Checked = Public2.PRJ.SETT.ShowBuildingFoundation;
		chkShowRelief.Checked = Public2.PRJ.SETT.ShowRelief;
		chkShowRoads.Checked = Public2.PRJ.SETT.ShowRoads;
		chkShowFences.Checked = Public2.PRJ.SETT.ShowFences;
		chkShowText.Checked = Public2.PRJ.SETT.ShowText;
		chkShowAntennas.Checked = Public2.PRJ.SETT.ShowAntennas;
		chkShowAntennaLabels.Checked = Public2.PRJ.SETT.ShowAntennaLabels;
		chkShowAntennaAzimuth.Checked = Public2.PRJ.SETT.ShowAntennaAzimuth;
		chkShowLongAzimuth.Checked = Public2.PRJ.SETT.ShowLongAzimuth;
		chkShowControlPoints.Checked = Public2.PRJ.SETT.ShowControlPoints;
		chkShowControlPointGroups.Checked = Public2.PRJ.SETT.ShowControlPointGroups;
		chkShowControlPointHeight.Checked = Public2.PRJ.SETT.ShowControlPointHeight;
		chkShowHorizPlaneScale.Checked = Public2.PRJ.SETT.ShowHorizPlaneScale;
		chkShowConturZO.Checked = Public2.PRJ.SETT.ShowConturZO;
		chkShowVertPlaneLines.Checked = Public2.PRJ.SETT.ShowVertPlaneLines;
		chkShowCircles.Checked = Public2.PRJ.SETT.ShowCircles;
		txtCircleStep.Value = new decimal(Public2.PRJ.SETT.CircleStep);
		FlagProcessing = false;
	}

	private void txtMapBrightness_MouseUp(object sender, MouseEventArgs e)
	{
		Public2.PRJ.SETT.MapBrightness = Convert.ToByte(txtMapBrightness.Value);
		DrawPIC();
	}

	private void chkShowMap_MouseClick(object sender, MouseEventArgs e)
	{
		Public2.PRJ.SETT.ShowMap = chkShowMap.Checked;
		DrawPIC();
	}

	private void chkShowOrigin_MouseClick(object sender, MouseEventArgs e)
	{
		Public2.PRJ.SETT.ShowOrigin = chkShowOrigin.Checked;
		PB.Refresh();
	}

	private void chkShowAllBuildings_MouseClick(object sender, MouseEventArgs e)
	{
		Public2.PRJ.SETT.ShowAllBuildings = chkShowAllBuildings.Checked;
		PB.Refresh();
	}

	private void chkShowBuildingText_MouseClick(object sender, MouseEventArgs e)
	{
		Public2.PRJ.SETT.ShowBuildingText = chkShowBuildingText.Checked;
		PB.Refresh();
	}

	private void chkShowBuildingHeight_MouseClick(object sender, MouseEventArgs e)
	{
		Public2.PRJ.SETT.ShowBuildingHeight = chkShowBuildingHeight.Checked;
		PB.Refresh();
	}

	private void chkShowBuildingFoundation_MouseClick(object sender, MouseEventArgs e)
	{
		Public2.PRJ.SETT.ShowBuildingFoundation = chkShowBuildingFoundation.Checked;
		PB.Refresh();
	}

	private void chkShowRelief_MouseClick(object sender, MouseEventArgs e)
	{
		Public2.PRJ.SETT.ShowRelief = chkShowRelief.Checked;
		PB.Refresh();
	}

	private void chkShowRoads_MouseClick(object sender, MouseEventArgs e)
	{
		Public2.PRJ.SETT.ShowRoads = chkShowRoads.Checked;
		PB.Refresh();
	}

	private void chkShowFences_MouseClick(object sender, MouseEventArgs e)
	{
		Public2.PRJ.SETT.ShowFences = chkShowFences.Checked;
		PB.Refresh();
	}

	private void chkShowText_MouseClick(object sender, MouseEventArgs e)
	{
		Public2.PRJ.SETT.ShowText = chkShowText.Checked;
		PB.Refresh();
	}

	private void chkShowAntennas_MouseClick(object sender, MouseEventArgs e)
	{
		Public2.PRJ.SETT.ShowAntennas = chkShowAntennas.Checked;
		PB.Refresh();
	}

	private void chkShowAntennaLabels_MouseClick(object sender, MouseEventArgs e)
	{
		Public2.PRJ.SETT.ShowAntennaLabels = chkShowAntennaLabels.Checked;
		PB.Refresh();
	}

	private void chkShowAntennaAzimuth_MouseClick(object sender, MouseEventArgs e)
	{
		Public2.PRJ.SETT.ShowAntennaAzimuth = chkShowAntennaAzimuth.Checked;
		PB.Refresh();
	}

	private void chkShowLongAzimuth_MouseClick(object sender, MouseEventArgs e)
	{
		Public2.PRJ.SETT.ShowLongAzimuth = chkShowLongAzimuth.Checked;
		PB.Refresh();
	}

	private void chkShowControlPoints_MouseClick(object sender, MouseEventArgs e)
	{
		Public2.PRJ.SETT.ShowControlPoints = chkShowControlPoints.Checked;
		PB.Refresh();
	}

	private void chkShowControlPointGroups_MouseClick(object sender, MouseEventArgs e)
	{
		Public2.PRJ.SETT.ShowControlPointGroups = chkShowControlPointGroups.Checked;
		PB.Refresh();
	}

	private void chkShowControlPointHeight_MouseClick(object sender, MouseEventArgs e)
	{
		Public2.PRJ.SETT.ShowControlPointHeight = chkShowControlPointHeight.Checked;
		PB.Refresh();
	}

	private void chkShowHorizPlaneScale_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		Public2.PRJ.SETT.ShowHorizPlaneScale = chkShowHorizPlaneScale.Checked;
		PB.Refresh();
	}

	private void chkShowConturZO_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		Public2.PRJ.SETT.ShowConturZO = chkShowConturZO.Checked;
		PB.Refresh();
	}

	private void chkShowVertPlaneLines_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		Public2.PRJ.SETT.ShowVertPlaneLines = chkShowVertPlaneLines.Checked;
		PB.Refresh();
	}

	private void chkShowCircles_MouseClick(object sender, MouseEventArgs e)
	{
		Public2.PRJ.SETT.ShowCircles = chkShowCircles.Checked;
		DrawPIC();
	}

	private void txtCircleStep_ValueChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.CircleStep = Convert.ToSingle(txtCircleStep.Value);
			DrawPIC();
		}
	}

	public void GetServerName()
	{
		Ping ping = new Ping();
		long num = 1000000L;
		long num2 = 1000000L;
		try
		{
			PingReply pingReply = ping.Send("data1.mlinkplanner.com");
			if (pingReply.Status == IPStatus.Success)
			{
				num = pingReply.RoundtripTime;
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		try
		{
			PingReply pingReply2 = ping.Send("data2.mlinkplanner.com");
			if (pingReply2.Status == IPStatus.Success)
			{
				num2 = pingReply2.RoundtripTime;
			}
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
		bool flag = false;
		bool flag2 = false;
		try
		{
			if (num < 1000)
			{
				WebClient webClient = new WebClient
				{
					UseDefaultCredentials = true
				};
				new WebProxy();
				webClient.Proxy = WebRequest.GetSystemWebProxy();
				webClient.DownloadFile("http://data1.mlinkplanner.com/UPDATE/inservice.txt", Public2.AppDataFolder + "\\inservice1.txt");
				StreamReader streamReader = new StreamReader(Public2.AppDataFolder + "\\inservice1.txt", Encoding.GetEncoding(1251));
				string left = Strings.Trim(streamReader.ReadLine());
				streamReader.Close();
				if (Operators.CompareString(left, "1", TextCompare: false) == 0)
				{
					flag = true;
				}
			}
		}
		catch (Exception projectError3)
		{
			ProjectData.SetProjectError(projectError3);
			ProjectData.ClearProjectError();
		}
		try
		{
			if (num2 < 1000)
			{
				WebClient webClient2 = new WebClient
				{
					UseDefaultCredentials = true
				};
				new WebProxy();
				webClient2.Proxy = WebRequest.GetSystemWebProxy();
				webClient2.DownloadFile("http://data2.mlinkplanner.com/UPDATE/inservice.txt", Public2.AppDataFolder + "\\inservice2.txt");
				StreamReader streamReader2 = new StreamReader(Public2.AppDataFolder + "\\inservice2.txt", Encoding.GetEncoding(1251));
				string left2 = Strings.Trim(streamReader2.ReadLine());
				streamReader2.Close();
				if (Operators.CompareString(left2, "1", TextCompare: false) == 0)
				{
					flag2 = true;
				}
			}
		}
		catch (Exception projectError4)
		{
			ProjectData.SetProjectError(projectError4);
			ProjectData.ClearProjectError();
		}
		try
		{
			string text = Conversions.ToString(1);
			if (flag & flag2)
			{
				if (num > num2)
				{
					text = Conversions.ToString(2);
					Constants.UrlROOT = Constants.UrlROOT.Replace("1", text);
				}
			}
			else if (!flag && flag2)
			{
				text = Conversions.ToString(2);
				Constants.UrlROOT = Constants.UrlROOT.Replace("1", text);
			}
		}
		catch (Exception projectError5)
		{
			ProjectData.SetProjectError(projectError5);
			ProjectData.ClearProjectError();
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.Form1));
		this.ToolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
		this.ToolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
		this.cmdFile = new System.Windows.Forms.ToolStripDropDownButton();
		this.cmdNEW = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdOPEN = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdSAVE = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdSaveAS = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.cmdEXIT = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.mnuLastFile1 = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuLastFile2 = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuLastFile3 = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdSavePanel = new System.Windows.Forms.ToolStripButton();
		this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.ToolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
		this.cmdCleanOutsideScreen = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdCleanOutsideArea = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
		this.cmdAbout = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdUserManual = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdCheckForUpdates = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdPurchase = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdActivation = new System.Windows.Forms.ToolStripMenuItem();
		this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
		this.lblCOORD = new System.Windows.Forms.ToolStripStatusLabel();
		this.lblCoordFromOrigin = new System.Windows.Forms.ToolStripStatusLabel();
		this.lblHeight = new System.Windows.Forms.ToolStripStatusLabel();
		this.lblTime = new System.Windows.Forms.ToolStripStatusLabel();
		this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
		this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
		this.SplitContainer4 = new System.Windows.Forms.SplitContainer();
		this.TV = new System.Windows.Forms.TreeView();
		this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.GroupBox8 = new System.Windows.Forms.GroupBox();
		this.Label2 = new System.Windows.Forms.Label();
		this.txtCircleStep = new System.Windows.Forms.NumericUpDown();
		this.chkShowCircles = new System.Windows.Forms.CheckBox();
		this.GroupBox7 = new System.Windows.Forms.GroupBox();
		this.Label1 = new System.Windows.Forms.Label();
		this.txtMapBrightness = new System.Windows.Forms.NumericUpDown();
		this.chkShowMap = new System.Windows.Forms.CheckBox();
		this.GroupBox6 = new System.Windows.Forms.GroupBox();
		this.chkShowVertPlaneLines = new System.Windows.Forms.CheckBox();
		this.chkShowHorizPlaneScale = new System.Windows.Forms.CheckBox();
		this.chkShowConturZO = new System.Windows.Forms.CheckBox();
		this.GroupBox5 = new System.Windows.Forms.GroupBox();
		this.chkShowControlPointHeight = new System.Windows.Forms.CheckBox();
		this.chkShowControlPoints = new System.Windows.Forms.CheckBox();
		this.chkShowControlPointGroups = new System.Windows.Forms.CheckBox();
		this.GroupBox4 = new System.Windows.Forms.GroupBox();
		this.chkShowLongAzimuth = new System.Windows.Forms.CheckBox();
		this.chkShowAntennas = new System.Windows.Forms.CheckBox();
		this.chkShowAntennaAzimuth = new System.Windows.Forms.CheckBox();
		this.chkShowAntennaLabels = new System.Windows.Forms.CheckBox();
		this.GroupBox3 = new System.Windows.Forms.GroupBox();
		this.chkShowFences = new System.Windows.Forms.CheckBox();
		this.chkShowRoads = new System.Windows.Forms.CheckBox();
		this.chkShowText = new System.Windows.Forms.CheckBox();
		this.chkShowRelief = new System.Windows.Forms.CheckBox();
		this.GroupBox2 = new System.Windows.Forms.GroupBox();
		this.chkShowAllBuildings = new System.Windows.Forms.CheckBox();
		this.chkShowBuildingText = new System.Windows.Forms.CheckBox();
		this.chkShowBuildingFoundation = new System.Windows.Forms.CheckBox();
		this.chkShowBuildingHeight = new System.Windows.Forms.CheckBox();
		this.chkShowOrigin = new System.Windows.Forms.CheckBox();
		this.SplitContainer2 = new System.Windows.Forms.SplitContainer();
		this.SplitContainer3 = new System.Windows.Forms.SplitContainer();
		this.Panel1 = new System.Windows.Forms.Panel();
		this.PB = new System.Windows.Forms.PictureBox();
		this.MenuStrip2 = new System.Windows.Forms.MenuStrip();
		this.TScmbZOOM = new System.Windows.Forms.ToolStripComboBox();
		this.TScmbURL = new System.Windows.Forms.ToolStripComboBox();
		this.cmdRuler = new System.Windows.Forms.ToolStripMenuItem();
		this.pbBM = new System.Windows.Forms.PictureBox();
		this.OFD1 = new System.Windows.Forms.OpenFileDialog();
		this.OFD2 = new System.Windows.Forms.OpenFileDialog();
		this.SFD1 = new System.Windows.Forms.SaveFileDialog();
		this.Timer1 = new System.Windows.Forms.Timer(this.components);
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.ilICO = new System.Windows.Forms.ImageList(this.components);
		this.ContCP = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.cmdNewCP = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdMoveCP = new System.Windows.Forms.ToolStripMenuItem();
		this.ContANT = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.cmdMoveANT = new System.Windows.Forms.ToolStripMenuItem();
		this.ContOrigin = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.cmdSetOrigin = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuStrip1.SuspendLayout();
		this.StatusStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).BeginInit();
		this.SplitContainer1.Panel1.SuspendLayout();
		this.SplitContainer1.Panel2.SuspendLayout();
		this.SplitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer4).BeginInit();
		this.SplitContainer4.Panel1.SuspendLayout();
		this.SplitContainer4.Panel2.SuspendLayout();
		this.SplitContainer4.SuspendLayout();
		this.GroupBox1.SuspendLayout();
		this.GroupBox8.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtCircleStep).BeginInit();
		this.GroupBox7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtMapBrightness).BeginInit();
		this.GroupBox6.SuspendLayout();
		this.GroupBox5.SuspendLayout();
		this.GroupBox4.SuspendLayout();
		this.GroupBox3.SuspendLayout();
		this.GroupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer2).BeginInit();
		this.SplitContainer2.Panel2.SuspendLayout();
		this.SplitContainer2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer3).BeginInit();
		this.SplitContainer3.Panel2.SuspendLayout();
		this.SplitContainer3.SuspendLayout();
		this.Panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.PB).BeginInit();
		this.MenuStrip2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pbBM).BeginInit();
		this.ContCP.SuspendLayout();
		this.ContANT.SuspendLayout();
		this.ContOrigin.SuspendLayout();
		base.SuspendLayout();
		this.ToolStripSeparator10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.ToolStripSeparator10.Name = "ToolStripSeparator10";
		this.ToolStripSeparator10.Size = new System.Drawing.Size(6, 28);
		this.ToolStripSeparator9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.ToolStripSeparator9.Name = "ToolStripSeparator9";
		this.ToolStripSeparator9.Size = new System.Drawing.Size(6, 28);
		this.MenuStrip1.AutoSize = false;
		this.MenuStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
		this.MenuStrip1.GripMargin = new System.Windows.Forms.Padding(0);
		this.MenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
		this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.cmdFile, this.cmdSavePanel, this.ToolStripSeparator4, this.ToolStripDropDownButton1, this.ToolStripSeparator10, this.ToolStripDropDownButton3, this.ToolStripSeparator9 });
		this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
		this.MenuStrip1.Name = "MenuStrip1";
		this.MenuStrip1.ShowItemToolTips = true;
		this.MenuStrip1.Size = new System.Drawing.Size(1479, 32);
		this.MenuStrip1.TabIndex = 7;
		this.MenuStrip1.Text = "MenuStrip1";
		this.cmdFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.cmdFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[10] { this.cmdNEW, this.cmdOPEN, this.cmdSAVE, this.cmdSaveAS, this.ToolStripSeparator3, this.cmdEXIT, this.ToolStripSeparator1, this.mnuLastFile1, this.mnuLastFile2, this.mnuLastFile3 });
		this.cmdFile.Image = (System.Drawing.Image)resources.GetObject("cmdFile.Image");
		this.cmdFile.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.cmdFile.Margin = new System.Windows.Forms.Padding(0);
		this.cmdFile.Name = "cmdFile";
		this.cmdFile.Size = new System.Drawing.Size(37, 28);
		this.cmdNEW.Image = (System.Drawing.Image)resources.GetObject("cmdNEW.Image");
		this.cmdNEW.Name = "cmdNEW";
		this.cmdNEW.Size = new System.Drawing.Size(125, 30);
		this.cmdNEW.Text = "New";
		this.cmdOPEN.Image = (System.Drawing.Image)resources.GetObject("cmdOPEN.Image");
		this.cmdOPEN.Name = "cmdOPEN";
		this.cmdOPEN.Size = new System.Drawing.Size(125, 30);
		this.cmdOPEN.Text = "Open";
		this.cmdSAVE.Enabled = false;
		this.cmdSAVE.Image = (System.Drawing.Image)resources.GetObject("cmdSAVE.Image");
		this.cmdSAVE.Name = "cmdSAVE";
		this.cmdSAVE.Size = new System.Drawing.Size(125, 30);
		this.cmdSAVE.Text = "Save";
		this.cmdSaveAS.Enabled = false;
		this.cmdSaveAS.Image = (System.Drawing.Image)resources.GetObject("cmdSaveAS.Image");
		this.cmdSaveAS.Name = "cmdSaveAS";
		this.cmdSaveAS.Size = new System.Drawing.Size(125, 30);
		this.cmdSaveAS.Text = "Save as";
		this.ToolStripSeparator3.Name = "ToolStripSeparator3";
		this.ToolStripSeparator3.Size = new System.Drawing.Size(122, 6);
		this.cmdEXIT.Name = "cmdEXIT";
		this.cmdEXIT.Size = new System.Drawing.Size(125, 30);
		this.cmdEXIT.Text = "Exit";
		this.ToolStripSeparator1.Name = "ToolStripSeparator1";
		this.ToolStripSeparator1.Size = new System.Drawing.Size(122, 6);
		this.mnuLastFile1.Name = "mnuLastFile1";
		this.mnuLastFile1.Size = new System.Drawing.Size(125, 30);
		this.mnuLastFile1.Text = "1";
		this.mnuLastFile2.Name = "mnuLastFile2";
		this.mnuLastFile2.Size = new System.Drawing.Size(125, 30);
		this.mnuLastFile2.Text = "2";
		this.mnuLastFile3.Name = "mnuLastFile3";
		this.mnuLastFile3.Size = new System.Drawing.Size(125, 30);
		this.mnuLastFile3.Text = "3";
		this.cmdSavePanel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.cmdSavePanel.Enabled = false;
		this.cmdSavePanel.Image = (System.Drawing.Image)resources.GetObject("cmdSavePanel.Image");
		this.cmdSavePanel.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.cmdSavePanel.Margin = new System.Windows.Forms.Padding(0);
		this.cmdSavePanel.Name = "cmdSavePanel";
		this.cmdSavePanel.Size = new System.Drawing.Size(28, 28);
		this.cmdSavePanel.ToolTipText = "Save";
		this.ToolStripSeparator4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.ToolStripSeparator4.Name = "ToolStripSeparator4";
		this.ToolStripSeparator4.Size = new System.Drawing.Size(6, 28);
		this.ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.ToolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.cmdCleanOutsideScreen, this.cmdCleanOutsideArea });
		this.ToolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("ToolStripDropDownButton1.Image");
		this.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.ToolStripDropDownButton1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
		this.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1";
		this.ToolStripDropDownButton1.Padding = new System.Windows.Forms.Padding(2);
		this.ToolStripDropDownButton1.Size = new System.Drawing.Size(41, 24);
		this.ToolStripDropDownButton1.Text = "Additional tools";
		this.cmdCleanOutsideScreen.Name = "cmdCleanOutsideScreen";
		this.cmdCleanOutsideScreen.Size = new System.Drawing.Size(444, 22);
		this.cmdCleanOutsideScreen.Text = "Delete objects outside the screen: buildings, terrain, lines and labels";
		this.cmdCleanOutsideArea.Name = "cmdCleanOutsideArea";
		this.cmdCleanOutsideArea.Size = new System.Drawing.Size(444, 22);
		this.cmdCleanOutsideArea.Text = "Delete objects outside the screen: buildings, lines and labels";
		this.ToolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.ToolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.cmdAbout, this.cmdUserManual, this.cmdCheckForUpdates, this.cmdPurchase, this.cmdActivation });
		this.ToolStripDropDownButton3.Image = (System.Drawing.Image)resources.GetObject("ToolStripDropDownButton3.Image");
		this.ToolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.ToolStripDropDownButton3.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
		this.ToolStripDropDownButton3.Name = "ToolStripDropDownButton3";
		this.ToolStripDropDownButton3.Padding = new System.Windows.Forms.Padding(2);
		this.ToolStripDropDownButton3.Size = new System.Drawing.Size(41, 24);
		this.ToolStripDropDownButton3.Text = "Help";
		this.cmdAbout.Name = "cmdAbout";
		this.cmdAbout.Size = new System.Drawing.Size(228, 22);
		this.cmdAbout.Text = "About EMF Planner";
		this.cmdUserManual.Name = "cmdUserManual";
		this.cmdUserManual.Size = new System.Drawing.Size(228, 22);
		this.cmdUserManual.Text = "User Manual";
		this.cmdCheckForUpdates.Name = "cmdCheckForUpdates";
		this.cmdCheckForUpdates.Size = new System.Drawing.Size(228, 22);
		this.cmdCheckForUpdates.Text = "Check for updates";
		this.cmdCheckForUpdates.Visible = false;
		this.cmdPurchase.Name = "cmdPurchase";
		this.cmdPurchase.Size = new System.Drawing.Size(228, 22);
		this.cmdPurchase.Text = "Purchase";
		this.cmdActivation.Name = "cmdActivation";
		this.cmdActivation.Size = new System.Drawing.Size(228, 22);
		this.cmdActivation.Text = "Enter your Activation ID code";
		this.cmdActivation.Visible = false;
		this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.lblCOORD, this.lblCoordFromOrigin, this.lblHeight, this.lblTime, this.ProgressBar });
		this.StatusStrip1.Location = new System.Drawing.Point(0, 939);
		this.StatusStrip1.Name = "StatusStrip1";
		this.StatusStrip1.Size = new System.Drawing.Size(1479, 22);
		this.StatusStrip1.TabIndex = 0;
		this.StatusStrip1.Text = "StatusStrip1";
		this.lblCOORD.AutoSize = false;
		this.lblCOORD.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
		this.lblCOORD.Name = "lblCOORD";
		this.lblCOORD.Size = new System.Drawing.Size(150, 17);
		this.lblCOORD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblCOORD.ToolTipText = "Координаты курсора WGS-84";
		this.lblCoordFromOrigin.AutoSize = false;
		this.lblCoordFromOrigin.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
		this.lblCoordFromOrigin.Name = "lblCoordFromOrigin";
		this.lblCoordFromOrigin.Size = new System.Drawing.Size(150, 17);
		this.lblCoordFromOrigin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblCoordFromOrigin.ToolTipText = "Координаты курсора относительно начала координат в метрах";
		this.lblHeight.AutoSize = false;
		this.lblHeight.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
		this.lblHeight.Name = "lblHeight";
		this.lblHeight.Size = new System.Drawing.Size(50, 17);
		this.lblHeight.Text = "0";
		this.lblHeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblHeight.ToolTipText = "Отметка рельефа";
		this.lblTime.AutoSize = false;
		this.lblTime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
		this.lblTime.Name = "lblTime";
		this.lblTime.Size = new System.Drawing.Size(100, 17);
		this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.ProgressBar.Name = "ProgressBar";
		this.ProgressBar.Size = new System.Drawing.Size(100, 16);
		this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.SplitContainer1.Location = new System.Drawing.Point(0, 32);
		this.SplitContainer1.Name = "SplitContainer1";
		this.SplitContainer1.Panel1.Controls.Add(this.SplitContainer4);
		this.SplitContainer1.Panel2.Controls.Add(this.SplitContainer2);
		this.SplitContainer1.Size = new System.Drawing.Size(1479, 907);
		this.SplitContainer1.SplitterDistance = 157;
		this.SplitContainer1.TabIndex = 8;
		this.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SplitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.SplitContainer4.Location = new System.Drawing.Point(0, 0);
		this.SplitContainer4.Name = "SplitContainer4";
		this.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.SplitContainer4.Panel1.Controls.Add(this.TV);
		this.SplitContainer4.Panel2.Controls.Add(this.GroupBox1);
		this.SplitContainer4.Size = new System.Drawing.Size(157, 907);
		this.SplitContainer4.SplitterDistance = 279;
		this.SplitContainer4.TabIndex = 1;
		this.TV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.TV.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TV.HideSelection = false;
		this.TV.ImageIndex = 0;
		this.TV.ImageList = this.ImageList1;
		this.TV.Location = new System.Drawing.Point(0, 0);
		this.TV.Name = "TV";
		this.TV.Scrollable = false;
		this.TV.SelectedImageIndex = 0;
		this.TV.ShowLines = false;
		this.TV.ShowNodeToolTips = true;
		this.TV.ShowPlusMinus = false;
		this.TV.ShowRootLines = false;
		this.TV.Size = new System.Drawing.Size(157, 279);
		this.TV.TabIndex = 0;
		this.ImageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ImageList1.ImageStream");
		this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.ImageList1.Images.SetKeyName(0, "Settings.png");
		this.ImageList1.Images.SetKeyName(1, "Антенны.png");
		this.ImageList1.Images.SetKeyName(2, "вертикальные сечения.png");
		this.ImageList1.Images.SetKeyName(3, "Горизонтальные сечения.png");
		this.ImageList1.Images.SetKeyName(4, "Графики распределения.png");
		this.ImageList1.Images.SetKeyName(5, "Здания.png");
		this.ImageList1.Images.SetKeyName(6, "Контрольные точки.png");
		this.ImageList1.Images.SetKeyName(7, "линии и надписи.png");
		this.ImageList1.Images.SetKeyName(8, "Начало координат.png");
		this.ImageList1.Images.SetKeyName(9, "Привязка растра.png");
		this.ImageList1.Images.SetKeyName(10, "рельеф.png");
		this.ImageList1.Images.SetKeyName(11, "Состав ПРТО.png");
		this.ImageList1.Images.SetKeyName(12, "Таблицы распределения.png");
		this.ImageList1.Images.SetKeyName(13, "Reports.png");
		this.ImageList1.Images.SetKeyName(14, "edit-list-rtl-icon.png");
		this.ImageList1.Images.SetKeyName(15, "Поиск в БД.png");
		this.ImageList1.Images.SetKeyName(16, "CAD.png");
		this.ImageList1.Images.SetKeyName(17, "3D-icon.png");
		this.GroupBox1.Controls.Add(this.GroupBox8);
		this.GroupBox1.Controls.Add(this.GroupBox7);
		this.GroupBox1.Controls.Add(this.GroupBox6);
		this.GroupBox1.Controls.Add(this.GroupBox5);
		this.GroupBox1.Controls.Add(this.GroupBox4);
		this.GroupBox1.Controls.Add(this.GroupBox3);
		this.GroupBox1.Controls.Add(this.chkShowRelief);
		this.GroupBox1.Controls.Add(this.GroupBox2);
		this.GroupBox1.Controls.Add(this.chkShowOrigin);
		this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox1.Location = new System.Drawing.Point(0, 0);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(157, 624);
		this.GroupBox1.TabIndex = 170;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "Display options";
		this.GroupBox8.Controls.Add(this.Label2);
		this.GroupBox8.Controls.Add(this.txtCircleStep);
		this.GroupBox8.Controls.Add(this.chkShowCircles);
		this.GroupBox8.Location = new System.Drawing.Point(1, 549);
		this.GroupBox8.Name = "GroupBox8";
		this.GroupBox8.Size = new System.Drawing.Size(154, 46);
		this.GroupBox8.TabIndex = 178;
		this.GroupBox8.TabStop = false;
		this.GroupBox8.Text = "Circles from origin";
		this.Label2.AutoSize = true;
		this.Label2.Location = new System.Drawing.Point(82, 21);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(43, 13);
		this.Label2.TabIndex = 0;
		this.Label2.Text = "Step, m";
		this.txtCircleStep.Location = new System.Drawing.Point(37, 18);
		this.txtCircleStep.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.txtCircleStep.Name = "txtCircleStep";
		this.txtCircleStep.Size = new System.Drawing.Size(39, 20);
		this.txtCircleStep.TabIndex = 168;
		this.txtCircleStep.TabStop = false;
		this.txtCircleStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.ToolTip1.SetToolTip(this.txtCircleStep, "Circle step, m");
		this.txtCircleStep.Value = new decimal(new int[4] { 50, 0, 0, 0 });
		this.chkShowCircles.AutoSize = true;
		this.chkShowCircles.BackColor = System.Drawing.Color.Transparent;
		this.chkShowCircles.Location = new System.Drawing.Point(8, 22);
		this.chkShowCircles.Name = "chkShowCircles";
		this.chkShowCircles.Size = new System.Drawing.Size(15, 14);
		this.chkShowCircles.TabIndex = 169;
		this.chkShowCircles.TabStop = false;
		this.chkShowCircles.UseVisualStyleBackColor = false;
		this.GroupBox7.Controls.Add(this.Label1);
		this.GroupBox7.Controls.Add(this.txtMapBrightness);
		this.GroupBox7.Controls.Add(this.chkShowMap);
		this.GroupBox7.Location = new System.Drawing.Point(1, 19);
		this.GroupBox7.Name = "GroupBox7";
		this.GroupBox7.Size = new System.Drawing.Size(154, 46);
		this.GroupBox7.TabIndex = 177;
		this.GroupBox7.TabStop = false;
		this.GroupBox7.Text = "Base map";
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(82, 21);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(56, 13);
		this.Label1.TabIndex = 0;
		this.Label1.Text = "Brightness";
		this.txtMapBrightness.Location = new System.Drawing.Point(37, 18);
		this.txtMapBrightness.Maximum = new decimal(new int[4] { 9, 0, 0, 0 });
		this.txtMapBrightness.Name = "txtMapBrightness";
		this.txtMapBrightness.ReadOnly = true;
		this.txtMapBrightness.Size = new System.Drawing.Size(39, 20);
		this.txtMapBrightness.TabIndex = 168;
		this.txtMapBrightness.TabStop = false;
		this.txtMapBrightness.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.ToolTip1.SetToolTip(this.txtMapBrightness, "Base map brightness");
		this.chkShowMap.AutoSize = true;
		this.chkShowMap.BackColor = System.Drawing.Color.Transparent;
		this.chkShowMap.Location = new System.Drawing.Point(8, 22);
		this.chkShowMap.Name = "chkShowMap";
		this.chkShowMap.Size = new System.Drawing.Size(15, 14);
		this.chkShowMap.TabIndex = 169;
		this.chkShowMap.TabStop = false;
		this.chkShowMap.UseVisualStyleBackColor = false;
		this.GroupBox6.Controls.Add(this.chkShowVertPlaneLines);
		this.GroupBox6.Controls.Add(this.chkShowHorizPlaneScale);
		this.GroupBox6.Controls.Add(this.chkShowConturZO);
		this.GroupBox6.Location = new System.Drawing.Point(1, 468);
		this.GroupBox6.Name = "GroupBox6";
		this.GroupBox6.Size = new System.Drawing.Size(154, 75);
		this.GroupBox6.TabIndex = 176;
		this.GroupBox6.TabStop = false;
		this.GroupBox6.Text = "Horizontal cuts";
		this.chkShowVertPlaneLines.AutoSize = true;
		this.chkShowVertPlaneLines.Location = new System.Drawing.Point(8, 53);
		this.chkShowVertPlaneLines.Name = "chkShowVertPlaneLines";
		this.chkShowVertPlaneLines.Size = new System.Drawing.Size(84, 17);
		this.chkShowVertPlaneLines.TabIndex = 7;
		this.chkShowVertPlaneLines.Text = "Vertical cuts";
		this.chkShowVertPlaneLines.UseVisualStyleBackColor = true;
		this.chkShowHorizPlaneScale.AutoSize = true;
		this.chkShowHorizPlaneScale.Location = new System.Drawing.Point(8, 17);
		this.chkShowHorizPlaneScale.Name = "chkShowHorizPlaneScale";
		this.chkShowHorizPlaneScale.Size = new System.Drawing.Size(45, 17);
		this.chkShowHorizPlaneScale.TabIndex = 4;
		this.chkShowHorizPlaneScale.Text = "Grid";
		this.chkShowHorizPlaneScale.UseVisualStyleBackColor = true;
		this.chkShowConturZO.AutoSize = true;
		this.chkShowConturZO.Location = new System.Drawing.Point(8, 35);
		this.chkShowConturZO.Name = "chkShowConturZO";
		this.chkShowConturZO.Size = new System.Drawing.Size(133, 17);
		this.chkShowConturZO.TabIndex = 5;
		this.chkShowConturZO.Text = "EMF exposure contour";
		this.chkShowConturZO.UseVisualStyleBackColor = true;
		this.GroupBox5.Controls.Add(this.chkShowControlPointHeight);
		this.GroupBox5.Controls.Add(this.chkShowControlPoints);
		this.GroupBox5.Controls.Add(this.chkShowControlPointGroups);
		this.GroupBox5.Location = new System.Drawing.Point(1, 381);
		this.GroupBox5.Name = "GroupBox5";
		this.GroupBox5.Size = new System.Drawing.Size(154, 81);
		this.GroupBox5.TabIndex = 175;
		this.GroupBox5.TabStop = false;
		this.GroupBox5.Text = "Control points";
		this.chkShowControlPointHeight.AutoSize = true;
		this.chkShowControlPointHeight.Location = new System.Drawing.Point(8, 59);
		this.chkShowControlPointHeight.Name = "chkShowControlPointHeight";
		this.chkShowControlPointHeight.Size = new System.Drawing.Size(122, 17);
		this.chkShowControlPointHeight.TabIndex = 5;
		this.chkShowControlPointHeight.Text = "Control point heights";
		this.chkShowControlPointHeight.UseVisualStyleBackColor = true;
		this.chkShowControlPoints.AutoSize = true;
		this.chkShowControlPoints.Location = new System.Drawing.Point(8, 19);
		this.chkShowControlPoints.Name = "chkShowControlPoints";
		this.chkShowControlPoints.Size = new System.Drawing.Size(90, 17);
		this.chkShowControlPoints.TabIndex = 3;
		this.chkShowControlPoints.Text = "Control points";
		this.chkShowControlPoints.UseVisualStyleBackColor = true;
		this.chkShowControlPointGroups.AutoSize = true;
		this.chkShowControlPointGroups.Location = new System.Drawing.Point(8, 39);
		this.chkShowControlPointGroups.Name = "chkShowControlPointGroups";
		this.chkShowControlPointGroups.Size = new System.Drawing.Size(120, 17);
		this.chkShowControlPointGroups.TabIndex = 4;
		this.chkShowControlPointGroups.Text = "Control point groups";
		this.chkShowControlPointGroups.UseVisualStyleBackColor = true;
		this.GroupBox4.Controls.Add(this.chkShowLongAzimuth);
		this.GroupBox4.Controls.Add(this.chkShowAntennas);
		this.GroupBox4.Controls.Add(this.chkShowAntennaAzimuth);
		this.GroupBox4.Controls.Add(this.chkShowAntennaLabels);
		this.GroupBox4.Location = new System.Drawing.Point(1, 287);
		this.GroupBox4.Name = "GroupBox4";
		this.GroupBox4.Size = new System.Drawing.Size(154, 91);
		this.GroupBox4.TabIndex = 174;
		this.GroupBox4.TabStop = false;
		this.GroupBox4.Text = "Antennas";
		this.chkShowLongAzimuth.AutoSize = true;
		this.chkShowLongAzimuth.Location = new System.Drawing.Point(8, 71);
		this.chkShowLongAzimuth.Name = "chkShowLongAzimuth";
		this.chkShowLongAzimuth.Size = new System.Drawing.Size(114, 17);
		this.chkShowLongAzimuth.TabIndex = 7;
		this.chkShowLongAzimuth.TabStop = false;
		this.chkShowLongAzimuth.Text = "Beams of azimuths";
		this.chkShowLongAzimuth.UseVisualStyleBackColor = true;
		this.chkShowAntennas.AutoSize = true;
		this.chkShowAntennas.Location = new System.Drawing.Point(8, 17);
		this.chkShowAntennas.Name = "chkShowAntennas";
		this.chkShowAntennas.Size = new System.Drawing.Size(94, 17);
		this.chkShowAntennas.TabIndex = 4;
		this.chkShowAntennas.TabStop = false;
		this.chkShowAntennas.Text = "Antenna icons";
		this.chkShowAntennas.UseVisualStyleBackColor = true;
		this.chkShowAntennaAzimuth.AutoSize = true;
		this.chkShowAntennaAzimuth.Location = new System.Drawing.Point(8, 53);
		this.chkShowAntennaAzimuth.Name = "chkShowAntennaAzimuth";
		this.chkShowAntennaAzimuth.Size = new System.Drawing.Size(110, 17);
		this.chkShowAntennaAzimuth.TabIndex = 6;
		this.chkShowAntennaAzimuth.TabStop = false;
		this.chkShowAntennaAzimuth.Text = "Antenna azimuths";
		this.chkShowAntennaAzimuth.UseVisualStyleBackColor = true;
		this.chkShowAntennaLabels.AutoSize = true;
		this.chkShowAntennaLabels.Location = new System.Drawing.Point(8, 35);
		this.chkShowAntennaLabels.Name = "chkShowAntennaLabels";
		this.chkShowAntennaLabels.Size = new System.Drawing.Size(109, 17);
		this.chkShowAntennaLabels.TabIndex = 5;
		this.chkShowAntennaLabels.TabStop = false;
		this.chkShowAntennaLabels.Text = "Antenna numbers";
		this.chkShowAntennaLabels.UseVisualStyleBackColor = true;
		this.GroupBox3.Controls.Add(this.chkShowFences);
		this.GroupBox3.Controls.Add(this.chkShowRoads);
		this.GroupBox3.Controls.Add(this.chkShowText);
		this.GroupBox3.Location = new System.Drawing.Point(1, 211);
		this.GroupBox3.Name = "GroupBox3";
		this.GroupBox3.Size = new System.Drawing.Size(154, 73);
		this.GroupBox3.TabIndex = 173;
		this.GroupBox3.TabStop = false;
		this.GroupBox3.Text = "Lines and labels";
		this.chkShowFences.AutoSize = true;
		this.chkShowFences.Location = new System.Drawing.Point(8, 35);
		this.chkShowFences.Name = "chkShowFences";
		this.chkShowFences.Size = new System.Drawing.Size(61, 17);
		this.chkShowFences.TabIndex = 166;
		this.chkShowFences.TabStop = false;
		this.chkShowFences.Text = "Barriers";
		this.chkShowFences.UseVisualStyleBackColor = true;
		this.chkShowRoads.AutoSize = true;
		this.chkShowRoads.Location = new System.Drawing.Point(8, 17);
		this.chkShowRoads.Name = "chkShowRoads";
		this.chkShowRoads.Size = new System.Drawing.Size(57, 17);
		this.chkShowRoads.TabIndex = 165;
		this.chkShowRoads.TabStop = false;
		this.chkShowRoads.Text = "Roads";
		this.chkShowRoads.UseVisualStyleBackColor = true;
		this.chkShowText.AutoSize = true;
		this.chkShowText.Location = new System.Drawing.Point(8, 53);
		this.chkShowText.Name = "chkShowText";
		this.chkShowText.Size = new System.Drawing.Size(57, 17);
		this.chkShowText.TabIndex = 164;
		this.chkShowText.TabStop = false;
		this.chkShowText.Text = "Labels";
		this.chkShowText.UseVisualStyleBackColor = true;
		this.chkShowRelief.AutoSize = true;
		this.chkShowRelief.Location = new System.Drawing.Point(9, 191);
		this.chkShowRelief.Name = "chkShowRelief";
		this.chkShowRelief.Size = new System.Drawing.Size(59, 17);
		this.chkShowRelief.TabIndex = 172;
		this.chkShowRelief.TabStop = false;
		this.chkShowRelief.Text = "Terrain";
		this.chkShowRelief.UseVisualStyleBackColor = true;
		this.GroupBox2.Controls.Add(this.chkShowAllBuildings);
		this.GroupBox2.Controls.Add(this.chkShowBuildingText);
		this.GroupBox2.Controls.Add(this.chkShowBuildingFoundation);
		this.GroupBox2.Controls.Add(this.chkShowBuildingHeight);
		this.GroupBox2.Location = new System.Drawing.Point(1, 92);
		this.GroupBox2.Name = "GroupBox2";
		this.GroupBox2.Size = new System.Drawing.Size(154, 93);
		this.GroupBox2.TabIndex = 171;
		this.GroupBox2.TabStop = false;
		this.GroupBox2.Text = "Buildings";
		this.chkShowAllBuildings.AutoSize = true;
		this.chkShowAllBuildings.Location = new System.Drawing.Point(8, 19);
		this.chkShowAllBuildings.Name = "chkShowAllBuildings";
		this.chkShowAllBuildings.Size = new System.Drawing.Size(68, 17);
		this.chkShowAllBuildings.TabIndex = 166;
		this.chkShowAllBuildings.TabStop = false;
		this.chkShowAllBuildings.Text = "Buildings";
		this.chkShowAllBuildings.UseVisualStyleBackColor = true;
		this.chkShowBuildingText.AutoSize = true;
		this.chkShowBuildingText.Location = new System.Drawing.Point(8, 37);
		this.chkShowBuildingText.Name = "chkShowBuildingText";
		this.chkShowBuildingText.Size = new System.Drawing.Size(47, 17);
		this.chkShowBuildingText.TabIndex = 165;
		this.chkShowBuildingText.TabStop = false;
		this.chkShowBuildingText.Text = "Text";
		this.chkShowBuildingText.UseVisualStyleBackColor = true;
		this.chkShowBuildingFoundation.AutoSize = true;
		this.chkShowBuildingFoundation.Location = new System.Drawing.Point(8, 73);
		this.chkShowBuildingFoundation.Name = "chkShowBuildingFoundation";
		this.chkShowBuildingFoundation.Size = new System.Drawing.Size(96, 17);
		this.chkShowBuildingFoundation.TabIndex = 164;
		this.chkShowBuildingFoundation.TabStop = false;
		this.chkShowBuildingFoundation.Text = "Base elevation";
		this.chkShowBuildingFoundation.UseVisualStyleBackColor = true;
		this.chkShowBuildingHeight.AutoSize = true;
		this.chkShowBuildingHeight.Location = new System.Drawing.Point(8, 55);
		this.chkShowBuildingHeight.Name = "chkShowBuildingHeight";
		this.chkShowBuildingHeight.Size = new System.Drawing.Size(57, 17);
		this.chkShowBuildingHeight.TabIndex = 163;
		this.chkShowBuildingHeight.TabStop = false;
		this.chkShowBuildingHeight.Text = "Height";
		this.chkShowBuildingHeight.UseVisualStyleBackColor = true;
		this.chkShowOrigin.AutoSize = true;
		this.chkShowOrigin.Location = new System.Drawing.Point(9, 71);
		this.chkShowOrigin.Name = "chkShowOrigin";
		this.chkShowOrigin.Size = new System.Drawing.Size(53, 17);
		this.chkShowOrigin.TabIndex = 170;
		this.chkShowOrigin.TabStop = false;
		this.chkShowOrigin.Text = "Origin";
		this.chkShowOrigin.UseVisualStyleBackColor = true;
		this.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.SplitContainer2.Location = new System.Drawing.Point(0, 0);
		this.SplitContainer2.Name = "SplitContainer2";
		this.SplitContainer2.Panel2.Controls.Add(this.SplitContainer3);
		this.SplitContainer2.Size = new System.Drawing.Size(1318, 907);
		this.SplitContainer2.SplitterDistance = 228;
		this.SplitContainer2.TabIndex = 0;
		this.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SplitContainer3.Location = new System.Drawing.Point(0, 0);
		this.SplitContainer3.Name = "SplitContainer3";
		this.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.SplitContainer3.Panel2.Controls.Add(this.Panel1);
		this.SplitContainer3.Panel2.Controls.Add(this.MenuStrip2);
		this.SplitContainer3.Panel2.Controls.Add(this.pbBM);
		this.SplitContainer3.Size = new System.Drawing.Size(1086, 907);
		this.SplitContainer3.SplitterDistance = 454;
		this.SplitContainer3.TabIndex = 0;
		this.Panel1.Controls.Add(this.PB);
		this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Panel1.Location = new System.Drawing.Point(0, 27);
		this.Panel1.Name = "Panel1";
		this.Panel1.Size = new System.Drawing.Size(1086, 422);
		this.Panel1.TabIndex = 41;
		this.PB.BackColor = System.Drawing.SystemColors.Window;
		this.PB.Cursor = System.Windows.Forms.Cursors.Cross;
		this.PB.ImageLocation = "";
		this.PB.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.PB.InitialImage = (System.Drawing.Image)resources.GetObject("PB.InitialImage");
		this.PB.Location = new System.Drawing.Point(0, 0);
		this.PB.Name = "PB";
		this.PB.Size = new System.Drawing.Size(2000, 1300);
		this.PB.TabIndex = 41;
		this.PB.TabStop = false;
		this.MenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.TScmbZOOM, this.TScmbURL, this.cmdRuler });
		this.MenuStrip2.Location = new System.Drawing.Point(0, 0);
		this.MenuStrip2.Name = "MenuStrip2";
		this.MenuStrip2.Size = new System.Drawing.Size(1086, 27);
		this.MenuStrip2.TabIndex = 1;
		this.MenuStrip2.Text = "MenuStrip2";
		this.TScmbZOOM.AutoSize = false;
		this.TScmbZOOM.BackColor = System.Drawing.SystemColors.MenuBar;
		this.TScmbZOOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.TScmbZOOM.Items.AddRange(new object[18]
		{
			"3", "4", "5", "6", "7", "8", "9", "10", "11", "12",
			"13", "14", "15", "16", "17", "18", "19", "20"
		});
		this.TScmbZOOM.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
		this.TScmbZOOM.Name = "TScmbZOOM";
		this.TScmbZOOM.Size = new System.Drawing.Size(40, 23);
		this.TScmbZOOM.ToolTipText = "Масштаб базовой карты";
		this.TScmbURL.AutoSize = false;
		this.TScmbURL.BackColor = System.Drawing.SystemColors.MenuBar;
		this.TScmbURL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.TScmbURL.Margin = new System.Windows.Forms.Padding(10, 0, 1, 0);
		this.TScmbURL.MaxDropDownItems = 15;
		this.TScmbURL.Name = "TScmbURL";
		this.TScmbURL.Size = new System.Drawing.Size(240, 23);
		this.TScmbURL.ToolTipText = "Наименование базовой карты";
		this.cmdRuler.Checked = true;
		this.cmdRuler.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cmdRuler.Image = (System.Drawing.Image)resources.GetObject("cmdRuler.Image");
		this.cmdRuler.Name = "cmdRuler";
		this.cmdRuler.Size = new System.Drawing.Size(62, 23);
		this.cmdRuler.Text = "Ruler";
		this.pbBM.BackColor = System.Drawing.Color.Transparent;
		this.pbBM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.pbBM.Location = new System.Drawing.Point(74, 46);
		this.pbBM.Name = "pbBM";
		this.pbBM.Size = new System.Drawing.Size(256, 256);
		this.pbBM.TabIndex = 40;
		this.pbBM.TabStop = false;
		this.Timer1.Interval = 300;
		this.ilICO.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ilICO.ImageStream");
		this.ilICO.TransparentColor = System.Drawing.Color.Transparent;
		this.ilICO.Images.SetKeyName(0, "0.bmp");
		this.ilICO.Images.SetKeyName(1, "1.bmp");
		this.ilICO.Images.SetKeyName(2, "2.bmp");
		this.ilICO.Images.SetKeyName(3, "3.bmp");
		this.ilICO.Images.SetKeyName(4, "4.bmp");
		this.ilICO.Images.SetKeyName(5, "5.bmp");
		this.ilICO.Images.SetKeyName(6, "6.bmp");
		this.ilICO.Images.SetKeyName(7, "7.bmp");
		this.ilICO.Images.SetKeyName(8, "8.bmp");
		this.ilICO.Images.SetKeyName(9, "9.bmp");
		this.ilICO.Images.SetKeyName(10, "10.bmp");
		this.ilICO.Images.SetKeyName(11, "11.bmp");
		this.ilICO.Images.SetKeyName(12, "12.bmp");
		this.ilICO.Images.SetKeyName(13, "13.bmp");
		this.ilICO.Images.SetKeyName(14, "14.bmp");
		this.ilICO.Images.SetKeyName(15, "15.bmp");
		this.ilICO.Images.SetKeyName(16, "16.bmp");
		this.ilICO.Images.SetKeyName(17, "17.bmp");
		this.ilICO.Images.SetKeyName(18, "18.bmp");
		this.ilICO.Images.SetKeyName(19, "19.bmp");
		this.ilICO.Images.SetKeyName(20, "20.bmp");
		this.ilICO.Images.SetKeyName(21, "21.bmp");
		this.ilICO.Images.SetKeyName(22, "22.bmp");
		this.ilICO.Images.SetKeyName(23, "23.bmp");
		this.ilICO.Images.SetKeyName(24, "24.bmp");
		this.ilICO.Images.SetKeyName(25, "25.bmp");
		this.ilICO.Images.SetKeyName(26, "26.bmp");
		this.ilICO.Images.SetKeyName(27, "27.bmp");
		this.ContCP.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.cmdNewCP, this.cmdMoveCP });
		this.ContCP.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
		this.ContCP.Name = "ContextMenuSite";
		this.ContCP.ShowImageMargin = false;
		this.ContCP.ShowItemToolTips = false;
		this.ContCP.Size = new System.Drawing.Size(198, 48);
		this.cmdNewCP.Name = "cmdNewCP";
		this.cmdNewCP.Size = new System.Drawing.Size(197, 22);
		this.cmdNewCP.Text = "Create new control point";
		this.cmdMoveCP.Name = "cmdMoveCP";
		this.cmdMoveCP.Size = new System.Drawing.Size(197, 22);
		this.cmdMoveCP.Text = "Move selected control point";
		this.ContANT.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.cmdMoveANT });
		this.ContANT.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
		this.ContANT.Name = "ContextMenuSite";
		this.ContANT.ShowImageMargin = false;
		this.ContANT.ShowItemToolTips = false;
		this.ContANT.Size = new System.Drawing.Size(197, 26);
		this.cmdMoveANT.Name = "cmdMoveANT";
		this.cmdMoveANT.Size = new System.Drawing.Size(196, 22);
		this.cmdMoveANT.Text = "Move the selected antennas";
		this.ContOrigin.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.cmdSetOrigin });
		this.ContOrigin.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
		this.ContOrigin.Name = "ContextMenuSite";
		this.ContOrigin.ShowImageMargin = false;
		this.ContOrigin.ShowItemToolTips = false;
		this.ContOrigin.Size = new System.Drawing.Size(186, 26);
		this.cmdSetOrigin.Name = "cmdSetOrigin";
		this.cmdSetOrigin.Size = new System.Drawing.Size(185, 22);
		this.cmdSetOrigin.Text = "Set the origin in this point";
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.ClientSize = new System.Drawing.Size(1479, 961);
		base.Controls.Add(this.SplitContainer1);
		base.Controls.Add(this.StatusStrip1);
		base.Controls.Add(this.MenuStrip1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.KeyPreview = true;
		base.MainMenuStrip = this.MenuStrip2;
		base.Name = "Form1";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "EMF Planner 7.1 Trial";
		this.MenuStrip1.ResumeLayout(false);
		this.MenuStrip1.PerformLayout();
		this.StatusStrip1.ResumeLayout(false);
		this.StatusStrip1.PerformLayout();
		this.SplitContainer1.Panel1.ResumeLayout(false);
		this.SplitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).EndInit();
		this.SplitContainer1.ResumeLayout(false);
		this.SplitContainer4.Panel1.ResumeLayout(false);
		this.SplitContainer4.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.SplitContainer4).EndInit();
		this.SplitContainer4.ResumeLayout(false);
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox1.PerformLayout();
		this.GroupBox8.ResumeLayout(false);
		this.GroupBox8.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtCircleStep).EndInit();
		this.GroupBox7.ResumeLayout(false);
		this.GroupBox7.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtMapBrightness).EndInit();
		this.GroupBox6.ResumeLayout(false);
		this.GroupBox6.PerformLayout();
		this.GroupBox5.ResumeLayout(false);
		this.GroupBox5.PerformLayout();
		this.GroupBox4.ResumeLayout(false);
		this.GroupBox4.PerformLayout();
		this.GroupBox3.ResumeLayout(false);
		this.GroupBox3.PerformLayout();
		this.GroupBox2.ResumeLayout(false);
		this.GroupBox2.PerformLayout();
		this.SplitContainer2.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.SplitContainer2).EndInit();
		this.SplitContainer2.ResumeLayout(false);
		this.SplitContainer3.Panel2.ResumeLayout(false);
		this.SplitContainer3.Panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer3).EndInit();
		this.SplitContainer3.ResumeLayout(false);
		this.Panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.PB).EndInit();
		this.MenuStrip2.ResumeLayout(false);
		this.MenuStrip2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pbBM).EndInit();
		this.ContCP.ResumeLayout(false);
		this.ContANT.ResumeLayout(false);
		this.ContOrigin.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
