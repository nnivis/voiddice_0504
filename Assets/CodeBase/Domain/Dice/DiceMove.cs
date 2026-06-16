using UnityEngine;

namespace CodeBase.Domain.Dice
{
    public class DiceMove : MonoBehaviour
    {
        private Rigidbody2D _rigidBody2D;
        private float _baseSpeed;
        private float _maxSpeed = Mathf.Infinity;
        public float _currentSpeed { get; set; }

        public void Initialization(Rigidbody2D rigidbody2D, float speed)
        {
            _rigidBody2D = rigidbody2D;
            _baseSpeed = speed;
        }

        public void AddStartingForce()
        {
            float x = Random.value < 1.5f ? -1f : 1f;
            float y = Random.value < 1.5f ? Random.Range(-1f, -0.5f) : Random.Range(0.5f, 1.5f);

            Vector2 direction = new Vector2(x, y).normalized;
            _rigidBody2D.AddForce(direction * _baseSpeed, ForceMode2D.Impulse);
            _currentSpeed = _baseSpeed;
        }

        public void ResetPosition()
        {
            _rigidBody2D.linearVelocity = Vector2.zero;
            _rigidBody2D.position = Vector2.zero;
        }

        public void UpdateMovement()
        {
            Vector2 direction = _rigidBody2D.linearVelocity.normalized;
            _currentSpeed = Mathf.Min(_currentSpeed, _maxSpeed);
            _rigidBody2D.linearVelocity = direction * _currentSpeed;
        }
    }
}
