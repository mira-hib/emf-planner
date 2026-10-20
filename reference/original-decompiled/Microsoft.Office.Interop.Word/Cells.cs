using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word;

[ComImport]
[CompilerGenerated]
[Guid("0002094A-0000-0000-C000-000000000046")]
[TypeIdentifier]
public interface Cells : IEnumerable
{
	void _VtblGap1_11();

	[DispId(1104)]
	WdCellVerticalAlignment VerticalAlignment
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(1104)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(1104)]
		[param: In]
		set;
	}

	void _VtblGap2_3();

	[DispId(0)]
	Cell this[[In] int Index]
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}
}
