using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public readonly double X;
    public readonly double Y;

    public Point(double x, double y)
    {
        this.X = x;
        this.Y = y;
    }
}
