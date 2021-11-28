using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct FractionalHex 
{
    public readonly double Q;
    public readonly double R;
    public readonly double S;

    public FractionalHex(double q, double r, double s)
    {
        this.Q = q;
        this.R = r;
        this.S = s;
        if (Math.Round(q + r +s) != 0)
        {
            throw new ArgumentException("q + r + s must be 0");
        }
    }

    public Hex HexRound()
    {
        int qi = (int)(Math.Round(Q));
        int ri = (int)(Math.Round(R));
        int si = (int)(Math.Round(S));

        double q_diff = Math.Abs(qi - Q);
        double r_diff = Math.Abs(ri - R);
        double s_diff = Math.Abs(si - S);

        if (q_diff > r_diff && q_diff > s_diff)
        {
            qi = ri - si;
        }
        else
        {
            if(r_diff > s_diff)
            {
                ri = -qi - si;
            }
            else
            {
                si = -qi - ri;
            }
        }
        return new Hex(qi, ri, si);
    }

    public FractionalHex HexLerp(FractionalHex b, double t)
    {
        return new FractionalHex(Q * (1 - t) + b.Q * t,
            R * (1 - t) + b.R * t,
            S * (1 - t) + b.S * t);
    }

    static public List<Hex> HexLinedraw (Hex a, Hex b)
    {
        int N = a.Distance(b);
        FractionalHex a_nudge = new FractionalHex(a.Q + 1e-06, a.R + 1e-06, a.S + 1e-06);
        FractionalHex b_nudge = new FractionalHex(b.Q + 1e-06, b.R + 1e-06, b.S + 1e-06);
        List<Hex> results = new List<Hex>();
        double step = 1.0 / Math.Max(N, 1);
        for (int i = 0; i <= N; i++)
        {
            results.Add(a_nudge.HexLerp(b_nudge, step * i).HexRound());
        }
        return results;
    }
}
