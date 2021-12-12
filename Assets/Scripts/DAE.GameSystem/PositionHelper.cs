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

        public (int x, int y) ToGridPosition(Grid<Position> grid, Transform parent, Vector3 worldPosition)
        {
            float hexRadius = /*0.5f*/2f;
            var q = (Mathf.Sqrt(3f)/3f * worldPosition.x - 1f/3f * worldPosition.z)/ hexRadius;
            var r = (2f / 3f * worldPosition.z) / hexRadius;

            return ((int) q, (int) r);
        }

        public Vector3 ToWorldPosition(Grid<Position> grid, Transform parent, int q, int r)
        {
            //changed method parameters to q and r instead of int x and int y!!!!
            float hexRadius = 0.5f;

            var x = hexRadius * (Mathf.Sqrt(3f) * q + Mathf.Sqrt(3f) / 2f * r);
            var z = hexRadius * (3f / 2f * r);
            var worldPosition = new Vector3(x, 0, z);
            return worldPosition;
        }
    }
}
