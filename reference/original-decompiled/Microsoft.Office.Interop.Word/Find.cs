using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word;

[ComImport]
[CompilerGenerated]
[Guid("000209B0-0000-0000-C000-000000000046")]
[TypeIdentifier]
public interface Find
{
	void _VtblGap1_3();

	[DispId(10)]
	bool Forward
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(10)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(10)]
		[param: In]
		set;
	}

	void _VtblGap2_2();

	[DispId(12)]
	bool Found
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(12)]
		get;
	}

	void _VtblGap3_2();

	[DispId(14)]
	bool MatchCase
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(14)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(14)]
		[param: In]
		set;
	}

	void _VtblGap4_4();

	[DispId(17)]
	bool MatchWholeWord
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(17)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(17)]
		[param: In]
		set;
	}

	void _VtblGap5_8();

	[DispId(22)]
	string Text
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(22)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(22)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	void _VtblGap6_4();

	[DispId(25)]
	Replacement Replacement
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(25)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	void _VtblGap7_1();

	[DispId(27)]
	WdFindWrap Wrap
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(27)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(27)]
		[param: In]
		set;
	}

	void _VtblGap8_9();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(31)]
	void ClearFormatting();

	void _VtblGap9_2();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(444)]
	bool Execute([Optional][In][MarshalAs(UnmanagedType.Struct)] ref object FindText, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object MatchCase, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object MatchWholeWord, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object MatchWildcards, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object MatchSoundsLike, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object MatchAllWordForms, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object Forward, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object Wrap, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object Format, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object ReplaceWith, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object Replace, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object MatchKashida, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object MatchDiacritics, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object MatchAlefHamza, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object MatchControl);
}
