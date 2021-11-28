using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject _hexModel;

    [SerializeField]
    private int _mapWidth = 11;

    [SerializeField]
    private int _mapHeight = 11;

    [SerializeField]
    private float _xOffset = 1;

    [SerializeField]
    private float _zOffset = 1;

    private void Start()
    {
        CreateHexTiles();
    }

    private void CreateHexTiles()
    {
        for (int x = 0; x < _mapWidth; x++)
        {
            for (int z = 0; z < _mapHeight; z++)
            {
                GameObject hex = Instantiate(_hexModel);

                if (z % 2 == 0)
                {
                    hex.transform.position = new Vector3(x * _xOffset, 0, z * _zOffset);
                }
                else
                {
                    hex.transform.position = new Vector3(x * _xOffset + _xOffset / 2, 0, z * _zOffset);
                }
                GiveHexInfo(hex, x, z);
            }
        }
    }

    private void GiveHexInfo(GameObject hex, int x, int z)
    {
        hex.transform.parent = this.transform;
        hex.name = x.ToString() + ", " + z.ToString();
    }
}
