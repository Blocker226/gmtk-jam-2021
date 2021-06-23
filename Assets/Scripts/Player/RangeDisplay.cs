using System;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField]
        Color32 mainColour = Color.cyan;

        Color32 _clearColour;

        readonly List<VectorLine> _lines = new List<VectorLine>();

        void Awake()
        {
            _clearColour = new Color32(mainColour.r, mainColour.g, mainColour.b, 0);
        }

        public void DrawLines(Transform origin, Transform previous)
        {
            ClearLines();
            Vector2 originPosition = origin.position;
            RaycastHit2D[] hits = Physics2D.CircleCastAll(originPosition, range, Vector2.zero, 0, mask);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform == origin || hit.transform == previous) continue;

                Vector2 hitVector = (Vector2)hit.transform.position - originPosition;

                RaycastHit2D[] obstructions = Physics2D.RaycastAll(originPosition, 
                    hitVector.normalized,
                    hitVector.magnitude,
                    mask);

                if (obstructions.Any(obstruction =>
                    obstruction.transform != origin && obstruction.transform != hit.transform)) continue;

                //Create a line
                var points = new List<Vector3> {originPosition};
                var colors = new List<Color32>();
                
                Vector3 lastSeg = points[0];
                Vector3 position = hit.transform.position;

                Vector3 vector = (position - lastSeg);
                
                if (vector.magnitude > 15)
                {
                    points.Add(lastSeg + vector.normalized * 5);
                }
                else
                {
                    points.Add(lastSeg + vector * 0.25f);
                }
                colors.Add(_clearColour);
                
                points.Add(lastSeg + vector / 2);
                colors.Add(mainColour);

                if (vector.magnitude > 15)
                {
                    points.Add(position + vector.normalized * -2.5f);
                }
                else
                {
                    points.Add(lastSeg + vector * 0.85f);
                }
                colors.Add(_clearColour);
                
                points.Add(position);
                colors.Add(_clearColour);

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
