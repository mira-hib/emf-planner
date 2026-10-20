using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word;

[ComImport]
[CompilerGenerated]
[Guid("00020951-0000-0000-C000-000000000046")]
[DefaultMember("Range")]
[TypeIdentifier]
public interface Table
{
	[DispId(0)]
	Range Range
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	void _VtblGap1_3();

	[DispId(100)]
	Columns Columns
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(100)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(101)]
	Rows Rows
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(101)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1100)]
	Borders Borders
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(1100)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(1100)]
		[param: In]
		[param: MarshalAs(UnmanagedType.Interface)]
		set;
	}

	void _VtblGap2_3();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(200)]
	void Select();

	void _VtblGap3_7();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(17)]
	[return: MarshalAs(UnmanagedType.Interface)]
	Cell Cell([In] int Row, [In] int Column);
}
