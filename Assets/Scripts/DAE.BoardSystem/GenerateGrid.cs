using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject _hexModel;

    public int MapRadius = 3;

    private float _distance = 2.05f;

    private void Awake()
    {
        CreateHexTiles();
    }

    private void CreateHexTiles()
    {
        for (int q = -MapRadius; q <= MapRadius; q++)
        {
            int r1 = Mathf.Max(-MapRadius, -q - MapRadius);
            int r2 = Mathf.Min(MapRadius, -q + MapRadius);

            for (int r = r1; r <= r2; r++)
            {
                var x = _distance * (Mathf.Sqrt(3f) * q + Mathf.Sqrt(3f) / 2f * r);
                var y = _distance * (3f / 2f * r);

                GameObject hexInstance = Instantiate(_hexModel, new Vector3(x, 0, y), Quaternion.identity);

                GiveHexInfo(hexInstance, x, y, q, r);
            }
        }
    }

    private void GiveHexInfo(GameObject hex, float x, float z, int q, int r)
    {
        hex.transform.parent = this.transform;
        hex.name = "World" + x.ToString() + ", " + z.ToString() + "/ Grid " + q.ToString() + ", " + r.ToString();
    }
}
