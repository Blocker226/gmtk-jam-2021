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
            ClearLines();
            Vector2 originPosition = origin.position;
            RaycastHit2D[] hits = Physics2D.CircleCastAll(originPosition, range, Vector2.zero, 0, mask);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform == origin || hit.transform == previous) continue;
                //Create a line
                var points = new List<Vector3> {originPosition};
                var colors = new List<Color32>();
                
                Vector3 lastSeg = points[0];
                Vector3 position = hit.transform.position;
                
                points.Add(lastSeg + (position - lastSeg).normalized * 5);
                colors.Add(new Color32(0, 255, 255, 0));
                
                points.Add(lastSeg + (position - lastSeg) / 2);
                colors.Add(Color.cyan);
                
                points.Add(lastSeg + (position - lastSeg) * 0.85f);
                colors.Add(new Color32(0, 255, 255, 0));
                
                points.Add(position);
                colors.Add(new Color32(0, 255, 255, 0));

                var line = new VectorLine("Planet Line", points, lineWidth, LineType.Continuous) 
                    {smoothColor = true};
                line.SetColors(colors);
                line.Draw3DAuto();
                _lines.Add(line);
            }
        }

        public void ClearLines()
        {
            VectorLine.Destroy(_lines);
            _lines.Clear();
        }
    }
}
