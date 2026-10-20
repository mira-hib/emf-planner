using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word;

[ComImport]
[CompilerGenerated]
[DefaultMember("Text")]
[Guid("00020975-0000-0000-C000-000000000046")]
[TypeIdentifier]
public interface Selection
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

	void _VtblGap2_16();

	[DispId(61)]
	Shading Shading
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(61)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	void _VtblGap3_3();

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

	void _VtblGap4_17();

	[DispId(262)]
	Find Find
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(262)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(400)]
	Range Range
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(400)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	void _VtblGap5_11();

	[DispId(410)]
	WdTextOrientation Orientation
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(410)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(410)]
		[param: In]
		set;
	}

	[DispId(411)]
	InlineShapes InlineShapes
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(411)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	void _VtblGap6_7();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(101)]
	void Collapse([Optional][In][MarshalAs(UnmanagedType.Struct)] ref object Direction);

	void _VtblGap7_8();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(111)]
	int MoveEnd([Optional][In][MarshalAs(UnmanagedType.Struct)] ref object Unit, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object Count);

	void _VtblGap8_41();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(503)]
	int MoveDown([Optional][In][MarshalAs(UnmanagedType.Struct)] ref object Unit, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object Count, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object Extend);

	void _VtblGap9_1();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(505)]
	int EndKey([Optional][In][MarshalAs(UnmanagedType.Struct)] ref object Unit, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object Extend);

	void _VtblGap10_1();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(507)]
	void TypeText([In][MarshalAs(UnmanagedType.BStr)] string Text);

	void _VtblGap11_2();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(512)]
	void TypeParagraph();

	void _VtblGap12_34();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(457)]
	[return: MarshalAs(UnmanagedType.Interface)]
	Table ConvertToTable([Optional][In][MarshalAs(UnmanagedType.Struct)] ref object Separator, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object NumRows, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object NumColumns, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object InitialColumnWidth, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object Format, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object ApplyBorders, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object ApplyShading, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object ApplyFont, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object ApplyColor, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object ApplyHeadingRows, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object ApplyLastRow, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object ApplyFirstColumn, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object ApplyLastColumn, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object AutoFit, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object AutoFitBehavior, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object DefaultTableBehavior);
}
