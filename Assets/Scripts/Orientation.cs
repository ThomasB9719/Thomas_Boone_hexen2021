using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Orientation
{
    public readonly double F0;
    public readonly double F1;
    public readonly double F2;
    public readonly double F3;
    public readonly double B0;
    public readonly double B1;
    public readonly double B2;
    public readonly double B3;
    public readonly double StartAngle;

    //public readonly Orientation Layout_pointy = 
    //    new Orientation(Mathf.Sqrt(3.0f), Mathf.Sqrt(3.0f) / 2.0, 0.0, 3.0 / 2.0,
    //            Mathf.Sqrt(3.0f) / 3.0, -1.0 / 3.0, 0.0, 2.0 / 3.0,
    //            0.5);

    //public readonly Orientation Layout_Flat = 
    //    new Orientation(3.0 / 2.0, 0.0, Mathf.Sqrt(3.0f) / 2.0, Mathf.Sqrt(3.0f),
    //            2.0 / 3.0, 0.0, -1.0 / 3.0, Mathf.Sqrt(3.0f) / 3.0,
    //            0.0);

    public Orientation(double f0, double f1, double f2, double f3, 
        double b0, double b1, double b2, double b3, double startAngle)
    {
        this.F0 = f0;
        this.F1 = f1;
        this.F2 = f2;
        this.F3 = f3;
        this.B0 = b0;
        this.B1 = b1;
        this.B2 = b2;
        this.B3 = b3;
        this.StartAngle = startAngle;
    }
}
