using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Core.Character
{
    public class CharacterController : MonoBehaviour
    {
        [HideInInspector] public UnityEvent OnMoveStarted;
        [HideInInspector] public UnityEvent OnMoveEnded;
        [HideInInspector] public UnityEvent OnRotateStarted;
        [HideInInspector] public UnityEvent OnRotateEnded;

        private Vector3 _targetMovePosition;
        private float _moveSpeedMultiplier;
        private Vector3 _targetRotation;
        private float _rotateSpeedMultiplier;

        private Coroutine _moveCoroutine;
        private Coroutine _rotateCoroutine;

        private float _attackPoints;
        private float _defencePoints;

        public void MoveCharacter(Vector3 offset, float speedMultiplier)
        {
            StopMoving();
            _targetMovePosition = transform.position + offset;
            _moveSpeedMultiplier = speedMultiplier;
            _moveCoroutine = StartCoroutine(nameof(Move));
        }
        public void RotateCharacter(Vector3 targetRotation, float speedMultiplier)
        {
            StopRotating();
            _targetRotation = targetRotation;
            _rotateSpeedMultiplier = speedMultiplier;
            _rotateCoroutine = StartCoroutine(nameof(Rotate));
        }
        private IEnumerator Move()
        {
            OnMoveStarted?.Invoke();

            var threshold = 0.01f;

            while (Vector3.Distance(transform.position, _targetMovePosition) > threshold)
            {
                transform.position = Vector3.Lerp(transform.position, _targetMovePosition, _moveSpeedMultiplier * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            OnMoveEnded?.Invoke();
        }

        private IEnumerator Rotate()
        {
            OnRotateStarted?.Invoke();

            var threshold = 0.999f;

            Quaternion rotationQuaternion = Quaternion.Euler(_targetRotation);
            while (Quaternion.Dot(transform.rotation, rotationQuaternion) < threshold)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationQuaternion, _rotateSpeedMultiplier * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            OnRotateEnded?.Invoke();
        }

        public void StopMoving()
        {
            if (_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);
        }
        public void StopRotating()
        {
            if (_rotateCoroutine != null)
                StopCoroutine(_rotateCoroutine);
        }

        public void AddDefence(float points)
        {
            _defencePoints += points;
        }

        public void RemoveDefence(float points)
        {
            _defencePoints -= points;
            if (_defencePoints < 0)
                _defencePoints = 0;
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}
