using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word;

[ComImport]
[CompilerGenerated]
[DefaultMember("Visible")]
[Guid("0002093B-0000-0000-C000-000000000046")]
[TypeIdentifier]
public interface Border
{
	void _VtblGap1_3();

	[DispId(0)]
	bool Visible
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(0)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(0)]
		[param: In]
		set;
	}
}
