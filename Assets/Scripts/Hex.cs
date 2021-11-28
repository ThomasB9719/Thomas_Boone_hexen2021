using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class Hex : MonoBehaviour
{
    public readonly int Q;
    public readonly int R;
    public readonly int S;

    public Hex(int q, int r, int s)
    {
        this.Q = q;
        this.R = r;
        this.S = s;
        if (q + r + s != 0)
        {
            throw new ArgumentException("q + r + s must be 0");
        }
    }

    public Hex Add(Hex b)
    {
        return new Hex(Q + b.Q, R + b.R, S + b.S);
    }

    public Hex Subtract(Hex b)
    {
        return new Hex(Q - b.Q, R - b.Q, S - b.S);
    }

    public Hex Scale(int k)
    {
        return new Hex(Q * k, R * k, S * k);
    }

    public int Length()
    {
        return (int)(Math.Abs(Q) + Math.Abs(R) + Math.Abs(S)) / 2;
    }

    public int Distance(Hex b)
    {
        return Subtract(b).Length();
    }
}
