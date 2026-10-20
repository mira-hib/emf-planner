using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner.My.Resources;

[StandardModule]
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
[HideModuleName]
internal sealed class Resources
{
	private static ResourceManager resourceMan;

	private static CultureInfo resourceCulture;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager
	{
		get
		{
			if (object.ReferenceEquals(resourceMan, null))
			{
				resourceMan = new ResourceManager("EMFPlanner.Resources", typeof(Resources).Assembly);
			}
			return resourceMan;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static CultureInfo Culture
	{
		get
		{
			return resourceCulture;
		}
		set
		{
			resourceCulture = value;
		}
	}

	internal static Bitmap bs_mark => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("bs_mark", resourceCulture));

	internal static Bitmap bs_mark_blue_ => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("bs_mark_blue_", resourceCulture));

	internal static Bitmap bs_mark_red => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("bs_mark_red", resourceCulture));

	internal static Bitmap bs_mark_red_ => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("bs_mark_red_", resourceCulture));

	internal static Bitmap bs_mark_transparent => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("bs_mark_transparent", resourceCulture));

	internal static Bitmap cancel_icon => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("cancel_icon", resourceCulture));

	internal static Bitmap Cross16x16 => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("Cross16x16", resourceCulture));

	internal static Bitmap cross6x6 => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("cross6x6", resourceCulture));

	internal static Bitmap cross8x8 => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("cross8x8", resourceCulture));

	internal static Bitmap Goal => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("Goal", resourceCulture));

	internal static Bitmap Industry_Radio_Tower_icon__2_ => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("Industry_Radio_Tower_icon__2_", resourceCulture));

	internal static Bitmap Origin => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("Origin", resourceCulture));

	internal static Bitmap OriginYellow => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("OriginYellow", resourceCulture));

	internal static Bitmap playstation_circle_icon => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("playstation_circle_icon", resourceCulture));

	internal static Bitmap point2x2 => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("point2x2", resourceCulture));

	internal static Bitmap point4x4 => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("point4x4", resourceCulture));

	internal static Bitmap point6x6 => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("point6x6", resourceCulture));

	internal static Bitmap ring6x6 => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("ring6x6", resourceCulture));

	internal static Bitmap ring8x8 => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("ring8x8", resourceCulture));

	internal static Bitmap Upload => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("Upload", resourceCulture));
}
