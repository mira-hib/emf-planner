using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word;

[ComImport]
[CompilerGenerated]
[Guid("000209B7-0000-0000-C000-000000000046")]
[TypeIdentifier]
public interface Options
{
	void _VtblGap1_144();

	[DispId(264)]
	bool AutoFormatAsYouTypeReplaceQuotes
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(264)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(264)]
		[param: In]
		set;
	}
}
