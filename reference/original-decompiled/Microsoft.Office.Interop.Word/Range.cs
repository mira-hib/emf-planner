using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word;

[ComImport]
[CompilerGenerated]
[Guid("0002095E-0000-0000-C000-000000000046")]
[DefaultMember("Text")]
[TypeIdentifier]
public interface Range
{
	[DispId(0)]
	string Text
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(0)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	void _VtblGap1_6();

	[DispId(5)]
	Font Font
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(5)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(5)]
		[param: In]
		[param: MarshalAs(UnmanagedType.Interface)]
		set;
	}

	void _VtblGap2_9();

	[DispId(57)]
	Cells Cells
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(57)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	void _VtblGap3_10();

	[DispId(1102)]
	ParagraphFormat ParagraphFormat
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(1102)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(1102)]
		[param: In]
		[param: MarshalAs(UnmanagedType.Interface)]
		set;
	}

	void _VtblGap4_29();

	[DispId(301)]
	WdColorIndex HighlightColorIndex
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(301)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(301)]
		[param: In]
		set;
	}

	void _VtblGap5_7();

	[DispId(262)]
	Find Find
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(262)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	void _VtblGap6_9();

	[DispId(317)]
	WdTextOrientation Orientation
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(317)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(317)]
		[param: In]
		set;
	}

	[DispId(319)]
	InlineShapes InlineShapes
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(319)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	void _VtblGap7_5();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(65535)]
	void Select();
}
