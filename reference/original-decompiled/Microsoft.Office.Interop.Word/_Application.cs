using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word;

[ComImport]
[CompilerGenerated]
[DefaultMember("Name")]
[Guid("00020970-0000-0000-C000-000000000046")]
[TypeIdentifier]
public interface _Application
{
	void _VtblGap1_3();

	[DispId(0)]
	string Name
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(6)]
	Documents Documents
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(6)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	void _VtblGap2_2();

	[DispId(4)]
	Window ActiveWindow
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(4)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(5)]
	Selection Selection
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(5)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	void _VtblGap3_4();

	[DispId(10)]
	AutoCorrect AutoCorrect
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(10)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	void _VtblGap4_12();

	[DispId(23)]
	bool Visible
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(23)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(23)]
		[param: In]
		set;
	}

	void _VtblGap5_61();

	[DispId(93)]
	Options Options
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(93)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	void _VtblGap6_66();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(372)]
	float MillimetersToPoints([In] float Millimeters);
}
