using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject _hexModel;

    [SerializeField]
    private int _mapRadius = 11;

    private float _distance = 2.05f;
    public (int x, int y) GridCoordinates;
    public Vector3 WorldCoordinates;

    private void Start()
    {
        CreateHexTiles();
    }

    private void CreateHexTiles()
    {
        for (int q = -_mapRadius; q <= _mapRadius; q++)
        {
            int r1 = Mathf.Max(-_mapRadius, -q - _mapRadius);
            int r2 = Mathf.Min(_mapRadius, -q + _mapRadius);

            for (int r = r1; r <= r2; r++)
            {
                var x = _distance * (Mathf.Sqrt(3f) * q + Mathf.Sqrt(3f) / 2f * r);
                var y = _distance * (3f / 2f * r);
                GameObject hexInstance = Instantiate(_hexModel, new Vector3(x, 0, y), Quaternion.identity);
                GiveHexInfo(hexInstance, x, y, q, r);
                GiveWorldPosition(x, y);
                GiveGridPosition(q, r);
            }
        }
    }

    public void GiveGridPosition(int q, int r)
    {
        GridCoordinates = (q, r);
    }

    public void GiveWorldPosition(float x, float y)
    {
        WorldCoordinates = new Vector3(x, 0, y);
    }

    private void GiveHexInfo(GameObject hex, float x, float z, int q, int r)
    {
        hex.transform.parent = this.transform;
        hex.name = "World" + x.ToString() + ", " + z.ToString() + "/ Grid " + q.ToString() + ", " + r.ToString();
    }
}
