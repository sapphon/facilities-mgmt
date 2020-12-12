using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class Route
    {
        private Vector3 _begin;
        private List<Vector3> _moves;

        public Route(Vector3 beginning)
        {
            _moves = new List<Vector3>();
            _begin = beginning;
        }

        public void AddMove(Vector3 move)
        {
            _moves.Add(move);
        }

        public List<Vector3> getMoves()
        {
            return _moves;
        }
    }
}