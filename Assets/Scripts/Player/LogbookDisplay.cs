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

        void Start()
        {
            _line = new VectorLine(
                "Chart Line", new List<Vector3>(), lineWidth, LineType.Continuous);
            _line.Draw3DAuto();
        }

        public void AddPlanet(Transform planet)
        {
            _line.points3.Add(planet.position);
        }
    }
}