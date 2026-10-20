using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word;

[ComImport]
[CompilerGenerated]
[DefaultMember("Name")]
[Guid("0002096B-0000-0000-C000-000000000046")]
[TypeIdentifier]
public interface _Document
{
	[DispId(0)]
	string Name
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	void _VtblGap1_7();

	[DispId(6)]
	Tables Tables
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(6)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	void _VtblGap2_12();

	[DispId(15)]
	Sections Sections
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(15)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	void _VtblGap3_14();

	[DispId(31)]
	TablesOfContents TablesOfContents
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(31)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	void _VtblGap4_13();

	[DispId(42)]
	Window ActiveWindow
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(42)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	void _VtblGap5_37();

	[DispId(72)]
	bool ShowGrammaticalErrors
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(72)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(72)]
		[param: In]
		set;
	}

	[DispId(73)]
	bool ShowSpellingErrors
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(73)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(73)]
		[param: In]
		set;
	}
}
