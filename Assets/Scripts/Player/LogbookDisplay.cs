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
        [SerializeField]
        Color32 mainColour = Color.white;

        Color32 _clearColour;
        VectorLine _line;
        List<Color32> _colors;

        void Awake()
        {
            InitLine();
        }

        void InitLine()
        {
            if (_line != null) return;

            _clearColour = new Color32(mainColour.r, mainColour.g, mainColour.b, 0);
            
            _line = new VectorLine(
                "Chart Line", new List<Vector3>(), lineWidth, LineType.Continuous);
            _colors = new List<Color32>();
            _line.smoothColor = true;
            _line.Draw3DAuto();
            Debug.Log("Chart line created.");
        }

        public void ResetLine()
        {
            Vector3 first = _line.points3[0];
            _line.points3.Clear();
            _line.points3.Add(first);
            
            _colors.Clear();
            _line.SetColors(_colors);
        }
        
        public void AddPlanet(Transform planet)
        {
            if (_line == null) InitLine();
            if (_line.points3.Count > 0)
            {
                Vector3 lastSeg = _line.points3[_line.points3.Count - 1];
                Vector3 position = planet.position;

                Vector3 vector = (position - lastSeg);
                
                _line.points3.Add(lastSeg + vector.normalized * 2.5f);
                _colors.Add(_clearColour);

                _line.points3.Add(lastSeg + vector * 0.6f);
                _colors.Add(mainColour);
                
                _line.points3.Add(lastSeg + vector * 0.75f);

                if (vector.magnitude > 12)
                {
                    _colors.Add(mainColour);
                    _line.points3.Add(position + vector.normalized * -2);
                }
                _colors.Add(_clearColour);

                _colors.Add(_clearColour);
            }
            _line.points3.Add(planet.position);

            if (_colors.Count > 0)
            {
                _line.SetColors(_colors);
            }
            Debug.Log("Chart line updated.");
        }
    }
}