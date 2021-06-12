using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

namespace Player
{
    public class RangeDisplay : MonoBehaviour
    {
        [SerializeField]
        float range = 10;
        [SerializeField]
        LayerMask mask;
        [SerializeField]
        float lineWidth = 4;

        readonly List<VectorLine> _lines = new List<VectorLine>();

        public void DrawLines(Transform origin, Transform previous)
        {
            VectorLine.Destroy(_lines);
            _lines.Clear();
            Vector2 position = origin.position;
            RaycastHit2D[] hits = Physics2D.CircleCastAll(position, range, Vector2.zero, 0, mask);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform == origin || hit.transform == previous) continue;
                //Create a line
                var points = new List<Vector3>(2) {position, hit.transform.position};
                var line = new VectorLine("Planet Line", points, lineWidth);
                line.SetColor(Color.cyan);
                line.Draw3DAuto();
                _lines.Add(line);
            }
        }
    }
}
