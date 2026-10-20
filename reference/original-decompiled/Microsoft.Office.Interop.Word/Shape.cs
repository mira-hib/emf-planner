using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Office.Core;

namespace Microsoft.Office.Interop.Word;

[ComImport]
[CompilerGenerated]
[Guid("000209A0-0000-0000-C000-000000000046")]
[TypeIdentifier]
public interface Shape
{
	void _VtblGap1_12();

	[DispId(109)]
	float Height
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(109)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(109)]
		[param: In]
		set;
	}

	void _VtblGap2_1();

	[DispId(111)]
	float Left
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(111)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(111)]
		[param: In]
		set;
	}

	void _VtblGap3_1();

	[DispId(113)]
	MsoTriState LockAspectRatio
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(113)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(113)]
		[param: In]
		set;
	}

	void _VtblGap4_10();

	[DispId(123)]
	float Top
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(123)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(123)]
		[param: In]
		set;
	}

	void _VtblGap5_5();

	[DispId(128)]
	float Width
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(128)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(128)]
		[param: In]
		set;
	}

	void _VtblGap6_2();

	[DispId(300)]
	WdRelativeHorizontalPosition RelativeHorizontalPosition
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(300)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(300)]
		[param: In]
		set;
	}

	[DispId(301)]
	WdRelativeVerticalPosition RelativeVerticalPosition
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(301)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(301)]
		[param: In]
		set;
	}

	void _VtblGap7_20();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(24)]
	void ZOrder([In] MsoZOrderCmd ZOrderCmd);
}
