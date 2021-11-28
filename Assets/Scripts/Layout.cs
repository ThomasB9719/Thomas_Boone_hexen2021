using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layout : MonoBehaviour
{
    public readonly Orientation Orientation;
    public readonly Point Size;
    public readonly Point Origin;

    static public Orientation pointy = new Orientation(Mathf.Sqrt(3.0f), Mathf.Sqrt(3.0f) / 2.0, 0.0, 3.0 / 2.0,
        Mathf.Sqrt(3.0f) / 3.0, -1.0 / 3.0, 0.0, 2.0 / 3.0, 0.5);
    static public Orientation flat = new Orientation(3.0 / 2.0, 0.0, Mathf.Sqrt(3.0f) / 2.0, Mathf.Sqrt(3.0f), 
        2.0 / 3.0, 0.0, -1.0 / 3.0, Mathf.Sqrt(3.0f) / 3.0, 0.0);

    public Layout(Orientation orientation, Point size, Point origin)
    {
        this.Orientation = orientation;
        this.Size = size;
        this.Origin = origin;
    }

    public Point HexToPixel(Hex h)
    {
        Orientation M = Orientation;
        double x = (M.F0 * h.Q + M.F1 * h.R) * Size.X;
        double y = (M.F2 * h.Q + M.F3 * h.R) * Size.Y;
        return new Point(x + Origin.X, y + Origin.Y);
    }

    public FractionalHex PixelToHex(Point p)
    {
        Orientation M = Orientation;
        Point point = new Point((p.X - Origin.X) / Size.X, (p.Y - Origin.Y) / Size.Y);
        double q = M.B0 * point.X + M.B1 * point.Y;
        double r = M.B2 * point.X + M.B3 * point.Y;
        return new FractionalHex(q, r, -q -r);
    }

    public Point HexCornerOffset (int corner)
    {
        Orientation M = Orientation;
        double angle = 2 * Math.PI * (M.StartAngle - corner) / 6;
        return new Point(Size.X * Math.Cos(angle), Size.Y * Math.Sin(angle));
    }

    public List<Point> PolygonCorners(Hex h)
    {
        List<Point> corners = new List<Point>();
        Point center = HexToPixel(h);
        for (int i = 0; i < 6; i++)
        {
            Point offset = HexCornerOffset(i);
            corners.Add(new Point(center.X + offset.X, center.Y + offset.Y));
        }

        return corners;
    }
}
