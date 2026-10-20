using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word;

[ComImport]
[CompilerGenerated]
[Guid("00020962-0000-0000-C000-000000000046")]
[DefaultMember("Caption")]
[TypeIdentifier]
public interface Window
{
	void _VtblGap1_3();

	[DispId(1)]
	Pane ActivePane
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(1)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	void _VtblGap2_15();

	[DispId(0)]
	string Caption
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(0)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	void _VtblGap3_60();

	[DispId(39)]
	int Hwnd
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(39)]
		get;
	}
}
