using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word;

[ComImport]
[CompilerGenerated]
[Guid("000209A8-0000-0000-C000-000000000046")]
[TypeIdentifier]
public interface InlineShape
{
	void _VtblGap1_11();

	[DispId(8)]
	float Height
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
	float Width
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(9)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(9)]
		[param: In]
		set;
	}

	void _VtblGap2_14();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(104)]
	[return: MarshalAs(UnmanagedType.Interface)]
	Shape ConvertToShape();
}
