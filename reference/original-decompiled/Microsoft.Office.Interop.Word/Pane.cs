using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word;

[ComImport]
[CompilerGenerated]
[Guid("00020960-0000-0000-C000-000000000046")]
[TypeIdentifier]
public interface Pane
{
	void _VtblGap1_11();

	[DispId(10)]
	View View
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(10)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	void _VtblGap2_4();

	[DispId(14)]
	int VerticalPercentScrolled
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(14)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(14)]
		[param: In]
		set;
	}
}
