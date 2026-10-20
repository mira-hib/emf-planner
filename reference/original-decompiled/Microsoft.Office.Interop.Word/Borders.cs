using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word;

[ComImport]
[CompilerGenerated]
[Guid("0002093C-0000-0000-C000-000000000046")]
[TypeIdentifier]
public interface Borders : IEnumerable
{
	void _VtblGap1_11();

	[DispId(6)]
	WdLineStyle InsideLineStyle
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(6)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(6)]
		[param: In]
		set;
	}

	[DispId(7)]
	WdLineStyle OutsideLineStyle
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(7)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(7)]
		[param: In]
		set;
	}

	[DispId(8)]
	WdLineWidth InsideLineWidth
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(8)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(8)]
		[param: In]
		set;
	}

	[DispId(9)]
	WdLineWidth OutsideLineWidth
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(9)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(9)]
		[param: In]
		set;
	}

	void _VtblGap2_26();

	[DispId(0)]
	Border this[[In] WdBorderType Index]
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}
}
