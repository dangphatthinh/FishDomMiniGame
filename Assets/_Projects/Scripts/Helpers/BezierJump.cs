using System;
using System.Collections;
using UnityEngine;

namespace Helpers
{
    public class BezierJump : MonoBehaviour
    {
        public event Action OnJump;
        public event Action OnComplete;

        private Vector3 _targetPosition = Vector2.zero;
        private Vector3 _startPosition = Vector2.zero;
        private Vector3 _controlPosition = Vector3.zero;

        private float _velocity = 0.0f;
        private Vector3 _velocityVector = Vector2.zero;
        private float _movingTime = 0.0f;
        private float _movedTime = 0.0f;
        private bool _doMove = false;

        public float MovingTime { get { return _movingTime; } }

        public void Init(float velocity)
        {
            _velocity = velocity;
            _movedTime = 0.0f;
            _doMove = false;
        }

        public void Jump(Vector3 start, Vector3 end, Vector3 control)
        {
            _targetPosition = end;
            _startPosition = start;
            _controlPosition = control;

            _velocityVector = (_targetPosition - _startPosition).normalized * _velocity;
            _movingTime = (Vector3.Distance(_startPosition, _controlPosition) + Vector3.Distance(_controlPosition, _targetPosition)) / _velocity;

            _doMove = true;
            _movedTime = 0.0f;
        }

        public void Update()
        {
            if (_doMove && _movedTime < _movingTime)
            {
                _movedTime += Time.deltaTime;
                var percent = _movedTime / _movingTime;

                var nextPosition = Helpers.MathHelper.GetPositionWithBezier(_startPosition, _controlPosition, _targetPosition, percent);
                gameObject.transform.position = new Vector3(nextPosition.x, nextPosition.y, 0);

                if (OnJump != null) OnJump();

                if (_movedTime >= _movingTime)
                {
                    _doMove = false;
                    gameObject.transform.position = _targetPosition;
                    if (OnComplete != null) OnComplete();
                }
            }
        }
    }
}