using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class Route
    {
        public enum RouteFailureReason
        {
            NULL, RECORDED_BY_CAMERA, CAUGHT_BY_GUARD, TOOK_TOO_LONG
        }

        private Vector3 _begin;
        private List<Vector3> _moves;
        private RouteFailureReason _failureReason;
        private bool _hasEnded;

        public Route(Vector3 beginning)
        {
            _moves = new List<Vector3>();
            _begin = beginning;
            _failureReason = RouteFailureReason.NULL;
            _hasEnded = false;
        }

        public void AddMove(Vector3 move)
        {
            _moves.Add(move);
        }

        public void Fail(RouteFailureReason reason)
        {
            this._hasEnded = true;
            this._failureReason = reason;
        }
        
        public void Succeed()
        {
            this._hasEnded = true;
        }

        public bool WasSuccessful()
        {
            return HasEnded() && this._failureReason == RouteFailureReason.NULL;
        }

        public bool HasEnded()
        {
            return this._hasEnded;
        }

        public List<Vector3> getMoves()
        {
            return _moves;
        }
    }
}