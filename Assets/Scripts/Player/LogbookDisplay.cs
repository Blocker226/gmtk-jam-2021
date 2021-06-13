using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

namespace Player
{
    public class LogbookDisplay : MonoBehaviour
    {
        [SerializeField]
        float lineWidth = 4;
        
        VectorLine _line;
        List<Color32> _colors;

        void Start()
        {
            _line = new VectorLine(
                "Chart Line", new List<Vector3>(), lineWidth, LineType.Continuous);
            _colors = new List<Color32>();
            _line.smoothColor = true;
            _line.Draw3DAuto();
        }

        public void AddPlanet(Transform planet)
        {
            if (_line.points3.Count > 0)
            {
                Vector3 lastSeg = _line.points3[_line.points3.Count - 1];
                Vector3 position = planet.position;
                
                _line.points3.Add(lastSeg + (position - lastSeg) / 5);
                _colors.Add(new Color32(255, 255, 255, 0));

                _line.points3.Add(lastSeg + (position - lastSeg) / 2);
                _colors.Add(Color.white);
                
                _line.points3.Add(lastSeg + (position - lastSeg) * 0.8f);
                _colors.Add(new Color32(255, 255, 255, 0));

                _colors.Add(new Color32(255, 255, 255, 0));
            }
            _line.points3.Add(planet.position);

            if (_colors.Count > 0)
            {
                _line.SetColors(_colors);
            }
        }
    }
}