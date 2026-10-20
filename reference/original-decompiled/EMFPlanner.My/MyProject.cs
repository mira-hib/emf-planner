using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner.My;

[StandardModule]
[HideModuleName]
[GeneratedCode("MyTemplate", "11.0.0.0")]
internal sealed class MyProject
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	[MyGroupCollection("System.Windows.Forms.Form", "Create__Instance__", "Dispose__Instance__", "My.MyProject.Forms")]
	internal sealed class MyForms
	{
		[ThreadStatic]
		private static Hashtable m_FormBeingCreated;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public Form1 m_Form1;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmAbout m_frmAbout;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmAntennasViewer m_frmAntennasViewer;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmAreaSize m_frmAreaSize;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmAutoBuildingHeight m_frmAutoBuildingHeight;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmAzimuthSpan m_frmAzimuthSpan;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmBckGrWrkDownloadTiles m_frmBckGrWrkDownloadTiles;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmBckGrWrkHoriz m_frmBckGrWrkHoriz;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmBckGrWrkHorizEkr m_frmBckGrWrkHorizEkr;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmBckGrWrkHorizVectorize m_frmBckGrWrkHorizVectorize;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmBckGrWrkVert m_frmBckGrWrkVert;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmCableCalc m_frmCableCalc;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmCalcAllControlPoints m_frmCalcAllControlPoints;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmCalcOneControlPoint m_frmCalcOneControlPoint;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmDBW m_frmDBW;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmEnvelope m_frmEnvelope;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmHeight05 m_frmHeight05;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmHeightSpan m_frmHeightSpan;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmIcoSelect m_frmIcoSelect;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmLineTypeSelect m_frmLineTypeSelect;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmParabola m_frmParabola;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmQuickVertPlane m_frmQuickVertPlane;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmTrialExpired m_frmTrialExpired;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmUpdateMessage m_frmUpdateMessage;

		public Form1 Form1
		{
			get
			{
				m_Form1 = Create__Instance__(m_Form1);
				return m_Form1;
			}
			set
			{
				if (value != m_Form1)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_Form1);
				}
			}
		}

		public frmAbout frmAbout
		{
			get
			{
				m_frmAbout = Create__Instance__(m_frmAbout);
				return m_frmAbout;
			}
			set
			{
				if (value != m_frmAbout)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmAbout);
				}
			}
		}

		public frmAntennasViewer frmAntennasViewer
		{
			get
			{
				m_frmAntennasViewer = Create__Instance__(m_frmAntennasViewer);
				return m_frmAntennasViewer;
			}
			set
			{
				if (value != m_frmAntennasViewer)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmAntennasViewer);
				}
			}
		}

		public frmAreaSize frmAreaSize
		{
			get
			{
				m_frmAreaSize = Create__Instance__(m_frmAreaSize);
				return m_frmAreaSize;
			}
			set
			{
				if (value != m_frmAreaSize)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmAreaSize);
				}
			}
		}

		public frmAutoBuildingHeight frmAutoBuildingHeight
		{
			get
			{
				m_frmAutoBuildingHeight = Create__Instance__(m_frmAutoBuildingHeight);
				return m_frmAutoBuildingHeight;
			}
			set
			{
				if (value != m_frmAutoBuildingHeight)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmAutoBuildingHeight);
				}
			}
		}

		public frmAzimuthSpan frmAzimuthSpan
		{
			get
			{
				m_frmAzimuthSpan = Create__Instance__(m_frmAzimuthSpan);
				return m_frmAzimuthSpan;
			}
			set
			{
				if (value != m_frmAzimuthSpan)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmAzimuthSpan);
				}
			}
		}

		public frmBckGrWrkDownloadTiles frmBckGrWrkDownloadTiles
		{
			get
			{
				m_frmBckGrWrkDownloadTiles = Create__Instance__(m_frmBckGrWrkDownloadTiles);
				return m_frmBckGrWrkDownloadTiles;
			}
			set
			{
				if (value != m_frmBckGrWrkDownloadTiles)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmBckGrWrkDownloadTiles);
				}
			}
		}

		public frmBckGrWrkHoriz frmBckGrWrkHoriz
		{
			get
			{
				m_frmBckGrWrkHoriz = Create__Instance__(m_frmBckGrWrkHoriz);
				return m_frmBckGrWrkHoriz;
			}
			set
			{
				if (value != m_frmBckGrWrkHoriz)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmBckGrWrkHoriz);
				}
			}
		}

		public frmBckGrWrkHorizEkr frmBckGrWrkHorizEkr
		{
			get
			{
				m_frmBckGrWrkHorizEkr = Create__Instance__(m_frmBckGrWrkHorizEkr);
				return m_frmBckGrWrkHorizEkr;
			}
			set
			{
				if (value != m_frmBckGrWrkHorizEkr)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmBckGrWrkHorizEkr);
				}
			}
		}

		public frmBckGrWrkHorizVectorize frmBckGrWrkHorizVectorize
		{
			get
			{
				m_frmBckGrWrkHorizVectorize = Create__Instance__(m_frmBckGrWrkHorizVectorize);
				return m_frmBckGrWrkHorizVectorize;
			}
			set
			{
				if (value != m_frmBckGrWrkHorizVectorize)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmBckGrWrkHorizVectorize);
				}
			}
		}

		public frmBckGrWrkVert frmBckGrWrkVert
		{
			get
			{
				m_frmBckGrWrkVert = Create__Instance__(m_frmBckGrWrkVert);
				return m_frmBckGrWrkVert;
			}
			set
			{
				if (value != m_frmBckGrWrkVert)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmBckGrWrkVert);
				}
			}
		}

		public frmCableCalc frmCableCalc
		{
			get
			{
				m_frmCableCalc = Create__Instance__(m_frmCableCalc);
				return m_frmCableCalc;
			}
			set
			{
				if (value != m_frmCableCalc)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmCableCalc);
				}
			}
		}

		public frmCalcAllControlPoints frmCalcAllControlPoints
		{
			get
			{
				m_frmCalcAllControlPoints = Create__Instance__(m_frmCalcAllControlPoints);
				return m_frmCalcAllControlPoints;
			}
			set
			{
				if (value != m_frmCalcAllControlPoints)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmCalcAllControlPoints);
				}
			}
		}

		public frmCalcOneControlPoint frmCalcOneControlPoint
		{
			get
			{
				m_frmCalcOneControlPoint = Create__Instance__(m_frmCalcOneControlPoint);
				return m_frmCalcOneControlPoint;
			}
			set
			{
				if (value != m_frmCalcOneControlPoint)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmCalcOneControlPoint);
				}
			}
		}

		public frmDBW frmDBW
		{
			get
			{
				m_frmDBW = Create__Instance__(m_frmDBW);
				return m_frmDBW;
			}
			set
			{
				if (value != m_frmDBW)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmDBW);
				}
			}
		}

		public frmEnvelope frmEnvelope
		{
			get
			{
				m_frmEnvelope = Create__Instance__(m_frmEnvelope);
				return m_frmEnvelope;
			}
			set
			{
				if (value != m_frmEnvelope)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmEnvelope);
				}
			}
		}

		public frmHeight05 frmHeight05
		{
			get
			{
				m_frmHeight05 = Create__Instance__(m_frmHeight05);
				return m_frmHeight05;
			}
			set
			{
				if (value != m_frmHeight05)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmHeight05);
				}
			}
		}

		public frmHeightSpan frmHeightSpan
		{
			get
			{
				m_frmHeightSpan = Create__Instance__(m_frmHeightSpan);
				return m_frmHeightSpan;
			}
			set
			{
				if (value != m_frmHeightSpan)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmHeightSpan);
				}
			}
		}

		public frmIcoSelect frmIcoSelect
		{
			get
			{
				m_frmIcoSelect = Create__Instance__(m_frmIcoSelect);
				return m_frmIcoSelect;
			}
			set
			{
				if (value != m_frmIcoSelect)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmIcoSelect);
				}
			}
		}

		public frmLineTypeSelect frmLineTypeSelect
		{
			get
			{
				m_frmLineTypeSelect = Create__Instance__(m_frmLineTypeSelect);
				return m_frmLineTypeSelect;
			}
			set
			{
				if (value != m_frmLineTypeSelect)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmLineTypeSelect);
				}
			}
		}

		public frmParabola frmParabola
		{
			get
			{
				m_frmParabola = Create__Instance__(m_frmParabola);
				return m_frmParabola;
			}
			set
			{
				if (value != m_frmParabola)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmParabola);
				}
			}
		}

		public frmQuickVertPlane frmQuickVertPlane
		{
			get
			{
				m_frmQuickVertPlane = Create__Instance__(m_frmQuickVertPlane);
				return m_frmQuickVertPlane;
			}
			set
			{
				if (value != m_frmQuickVertPlane)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmQuickVertPlane);
				}
			}
		}

		public frmTrialExpired frmTrialExpired
		{
			get
			{
				m_frmTrialExpired = Create__Instance__(m_frmTrialExpired);
				return m_frmTrialExpired;
			}
			set
			{
				if (value != m_frmTrialExpired)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmTrialExpired);
				}
			}
		}

		public frmUpdateMessage frmUpdateMessage
		{
			get
			{
				m_frmUpdateMessage = Create__Instance__(m_frmUpdateMessage);
				return m_frmUpdateMessage;
			}
			set
			{
				if (value != m_frmUpdateMessage)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmUpdateMessage);
				}
			}
		}

		[DebuggerHidden]
		private static T Create__Instance__<T>(T Instance) where T : Form, new()
		{
			if (Instance == null || Instance.IsDisposed)
			{
				if (m_FormBeingCreated != null)
				{
					if (m_FormBeingCreated.ContainsKey(typeof(T)))
					{
						throw new InvalidOperationException(Utils.GetResourceString("WinForms_RecursiveFormCreate"));
					}
				}
				else
				{
					m_FormBeingCreated = new Hashtable();
				}
				m_FormBeingCreated.Add(typeof(T), null);
				try
				{
					return new T();
				}
				catch (TargetInvocationException ex) when (((Func<bool>)delegate
				{
					// Could not convert BlockContainer to single expression
					ProjectData.SetProjectError(ex);
					return ex.InnerException != null;
				}).Invoke())
				{
					throw new InvalidOperationException(Utils.GetResourceString("WinForms_SeeInnerException", ex.InnerException.Message), ex.InnerException);
				}
				finally
				{
					m_FormBeingCreated.Remove(typeof(T));
				}
			}
			return Instance;
		}

		[DebuggerHidden]
		private void Dispose__Instance__<T>(ref T instance) where T : Form
		{
			instance.Dispose();
			instance = null;
		}

		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public MyForms()
		{
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object o)
		{
			return base.Equals(RuntimeHelpers.GetObjectValue(o));
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		internal new Type GetType()
		{
			return typeof(MyForms);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[MyGroupCollection("System.Web.Services.Protocols.SoapHttpClientProtocol", "Create__Instance__", "Dispose__Instance__", "")]
	internal sealed class MyWebServices
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerHidden]
		public override bool Equals(object o)
		{
			return base.Equals(RuntimeHelpers.GetObjectValue(o));
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerHidden]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerHidden]
		internal new Type GetType()
		{
			return typeof(MyWebServices);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerHidden]
		public override string ToString()
		{
			return base.ToString();
		}

		[DebuggerHidden]
		private static T Create__Instance__<T>(T instance) where T : new()
		{
			if (instance == null)
			{
				return new T();
			}
			return instance;
		}

		[DebuggerHidden]
		private void Dispose__Instance__<T>(ref T instance)
		{
			instance = default;
		}

		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public MyWebServices()
		{
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[ComVisible(false)]
	internal sealed class ThreadSafeObjectProvider<T> where T : new()
	{
		[CompilerGenerated]
		[ThreadStatic]
		private static T m_ThreadStaticValue;

		internal T GetInstance
		{
			[DebuggerHidden]
			get
			{
				if (m_ThreadStaticValue == null)
				{
					m_ThreadStaticValue = new T();
				}
				return m_ThreadStaticValue;
			}
		}

		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ThreadSafeObjectProvider()
		{
		}
	}

	private static readonly ThreadSafeObjectProvider<MyComputer> m_ComputerObjectProvider = new ThreadSafeObjectProvider<MyComputer>();

	private static readonly ThreadSafeObjectProvider<MyApplication> m_AppObjectProvider = new ThreadSafeObjectProvider<MyApplication>();

	private static readonly ThreadSafeObjectProvider<User> m_UserObjectProvider = new ThreadSafeObjectProvider<User>();

	private static ThreadSafeObjectProvider<MyForms> m_MyFormsObjectProvider = new ThreadSafeObjectProvider<MyForms>();

	private static readonly ThreadSafeObjectProvider<MyWebServices> m_MyWebServicesObjectProvider = new ThreadSafeObjectProvider<MyWebServices>();

	[HelpKeyword("My.Computer")]
	internal static MyComputer Computer
	{
		[DebuggerHidden]
		get
		{
			return m_ComputerObjectProvider.GetInstance;
		}
	}

	[HelpKeyword("My.Application")]
	internal static MyApplication Application
	{
		[DebuggerHidden]
		get
		{
			return m_AppObjectProvider.GetInstance;
		}
	}

	[HelpKeyword("My.User")]
	internal static User User
	{
		[DebuggerHidden]
		get
		{
			return m_UserObjectProvider.GetInstance;
		}
	}

	[HelpKeyword("My.Forms")]
	internal static MyForms Forms
	{
		[DebuggerHidden]
		get
		{
			return m_MyFormsObjectProvider.GetInstance;
		}
	}

	[HelpKeyword("My.WebServices")]
	internal static MyWebServices WebServices
	{
		[DebuggerHidden]
		get
		{
			return m_MyWebServicesObjectProvider.GetInstance;
		}
	}
}
