using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word;

[ComImport]
[CompilerGenerated]
[DefaultMember("Range")]
[Guid("0002094E-0000-0000-C000-000000000046")]
[TypeIdentifier]
public interface Cell
{
	[DispId(0)]
	Range Range
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	void _VtblGap1_17();

	[DispId(105)]
	Shading Shading
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(105)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	void _VtblGap2_7();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(204)]
	void Merge([In][MarshalAs(UnmanagedType.Interface)] Cell MergeTo);
}
