using System;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

public class PointD
{
	public double X;

	public double Y;

	public double EpsilonBase;

	public PointD()
	{
		X = 0.0;
		Y = 0.0;
		EpsilonBase = 1E-15;
	}

	public PointD(double nx, double ny)
	{
		X = 0.0;
		Y = 0.0;
		EpsilonBase = 1E-15;
		X = nx;
		Y = ny;
	}

	public PointD(PointD p)
	{
		X = 0.0;
		Y = 0.0;
		EpsilonBase = 1E-15;
		X = p.X;
		Y = p.Y;
	}

	public string ToStringXY()
	{
		return ToStringXY("", " ", withLabel: false);
	}

	public string ToStringXY(string fmt)
	{
		return ToStringXY(fmt, " ", withLabel: true);
	}

	public string ToStringXY(string fmt, string seperator, bool withLabel)
	{
		string text = X.ToString();
		string text2 = Y.ToString();
		if (Operators.CompareString(fmt, "", TextCompare: false) != 0)
		{
			text = X.ToString(fmt);
			text2 = Y.ToString(fmt);
		}
		if (withLabel)
		{
			text = "X=" + text;
			text2 = "Y=" + text2;
		}
		return $"{text}{seperator}{text2}";
	}

	public static PointD operator +(PointD p1, PointD p2)
	{
		return new PointD(p1.X + p2.X, p1.Y + p2.Y);
	}

	public static PointD operator +(PointD p, double d)
	{
		return new PointD(p.X + d, p.Y + d);
	}

	public static PointD operator +(double d, PointD p)
	{
		return p + d;
	}

	public static PointD operator -(PointD p1, PointD p2)
	{
		return new PointD(p1.X - p2.X, p1.Y - p2.Y);
	}

	public static PointD operator -(PointD p, double d)
	{
		return new PointD(p.X - d, p.Y - d);
	}

	public static PointD operator -(double d, PointD p)
	{
		return p - d;
	}

	public static PointD operator *(PointD p1, PointD p2)
	{
		return new PointD(p1.X * p2.X, p1.Y * p2.Y);
	}

	public static PointD operator *(PointD p, double d)
	{
		return new PointD(p.X * d, p.Y * d);
	}

	public static PointD operator *(double d, PointD p)
	{
		return p * d;
	}

	public static PointD operator /(PointD p1, PointD p2)
	{
		return new PointD(p1.X / p2.X, p1.Y / p2.Y);
	}

	public static PointD operator /(PointD p, double d)
	{
		return new PointD(p.X / d, p.Y / d);
	}

	public static PointD operator /(double d, PointD p)
	{
		return new PointD(d / p.X, d / p.Y);
	}

	public bool IsCoincidentX(PointD otherPointD, double snap)
	{
		if (Math.Abs(X - otherPointD.X) <= Math.Abs(snap))
		{
			return true;
		}
		return false;
	}

	public bool IsCoincidentX(PointD otherPointD)
	{
		double snap = Math.Abs(X) * EpsilonBase;
		return IsCoincidentX(otherPointD, snap);
	}

	public bool IsCoincidentY(PointD otherPointD, double snap)
	{
		if (Math.Abs(Y - otherPointD.Y) <= Math.Abs(snap))
		{
			return true;
		}
		return false;
	}

	public bool IsCoincidentY(PointD otherPointD)
	{
		double snap = Math.Abs(Y) * EpsilonBase;
		return IsCoincidentY(otherPointD, snap);
	}

	public bool IsCoincident(PointD otherPointD, double snapx, double snapy)
	{
		if (IsCoincidentX(otherPointD, snapx) && IsCoincidentY(otherPointD, snapy))
		{
			return true;
		}
		return false;
	}

	public bool IsCoincident(PointD otherPointD, double snap)
	{
		return IsCoincident(otherPointD, snap, snap);
	}

	public bool IsCoincident(PointD otherPointD)
	{
		double snap = Math.Max(Math.Abs(X), Math.Abs(Y)) * EpsilonBase;
		return IsCoincident(otherPointD, snap);
	}
}
