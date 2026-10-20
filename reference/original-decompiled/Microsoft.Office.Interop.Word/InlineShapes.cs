using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word;

[ComImport]
[CompilerGenerated]
[Guid("000209A9-0000-0000-C000-000000000046")]
[TypeIdentifier]
public interface InlineShapes : IEnumerable
{
	void _VtblGap1_5();

	[DispId(0)]
	InlineShape this[[In] int Index]
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(100)]
	[return: MarshalAs(UnmanagedType.Interface)]
	InlineShape AddPicture([In][MarshalAs(UnmanagedType.BStr)] string FileName, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object LinkToFile, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object SaveWithDocument, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object Range);
}
