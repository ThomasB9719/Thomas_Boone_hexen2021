using DAE.BoardSystem;
using DAE.HexesSystem;
using UnityEngine;

namespace DAE.GameSystem
{
    [CreateAssetMenu(menuName = "DAE/PositionHelper")]
    public class PositionHelper: ScriptableObject
    {
        [SerializeField]
        private float _tileDimension;

        //public (int x, int y) ToGridPosition(Grid<Position> grid, Transform parent, Vector3 worldPosition)
        //{
        //    var relativePosition = worldPosition - parent.position;
        //    var scaledRelativePosition = relativePosition / _tileDimension;

        //    var scaleBoardOffset = new Vector3(grid.Columns / 2.0f, 0, grid.Rows / 2.0f);
        //    scaledRelativePosition += scaleBoardOffset;

        //    var scaleHalfTileOffset= new Vector3(0.5f, 0, 0.5f);
        //    scaledRelativePosition -= scaleHalfTileOffset;

        //    var x = (int) Mathf.Round(scaledRelativePosition.x);
        //    var y = (int)Mathf.Round(scaledRelativePosition.z);

        //    return (x, y);
        //}

        //public Vector3 ToWorldPosition(Grid<Position> grid, Transform parent, int x, int y)
        //{
        //    var scaledRelativePosition = new Vector3(x, 0, y);

        //    var scaleHalfTileOffset = new Vector3(0.5f, 0, 0.5f);
        //    scaledRelativePosition += scaleHalfTileOffset;

        //    var scaleBoardOffset = new Vector3(grid.Columns / 2.0f, 0, grid.Rows / 2.0f);
        //    scaledRelativePosition -= scaleBoardOffset;

        //    var relativePosition = scaledRelativePosition * _tileDimension;
        //    var worldPosition = relativePosition + parent.position;

        //    return worldPosition;
        //}
    }
}
