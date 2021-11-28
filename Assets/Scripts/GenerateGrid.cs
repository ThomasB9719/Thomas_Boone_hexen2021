using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    [SerializeField]
    private Hex _hex;

    //[SerializeField]
    //private Orientation _orientation;

    //[SerializeField]
    //private Layout _layout;

    //[SerializeField]
    //private Point _point;

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j > -3; j--)
            {
                Hex hex = new Hex(i, 0, j);
                _hex.Add(hex);
                Debug.Log(i);
                Debug.Log(j);
            }
        }
    }
}
