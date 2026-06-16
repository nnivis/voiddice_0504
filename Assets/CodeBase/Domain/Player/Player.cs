using System.Collections;
using UnityEngine;
using VContainer;
using CodeBase.Domain.Player.Input;
using CodeBase.Domain.Player.State;

namespace CodeBase.Domain.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        private const float _delayBlock = 4.0f;
        private InputHandler _inputHandler;
        private PlayerStateMachine _stateMachine;
        private MoveTowardsCursor _moveCursor;
        private bool _isBlock;

        [Inject]
        private void Construct(InputHandler inputHandler)
        {
            Cursor.lockState = CursorLockMode.None;
            _inputHandler = inputHandler;
            _stateMachine = new PlayerStateMachine(transform.GetChild(0).gameObject, transform.GetChild(1).gameObject);
            _moveCursor = new MoveTowardsCursor();
        }

        private void OnEnable()
        {
            _inputHandler.ClickLeftDown += OnClickLeftDown;
            _inputHandler.ClickRightDown += OnClickRightDown;
        }

        private void OnDisable()
        {
            _inputHandler.ClickLeftDown -= OnClickLeftDown;
            _inputHandler.ClickRightDown -= OnClickRightDown;
        }

        private void Update()
        {
            _moveCursor.MoveTowards(transform, _mainCamera);

            if (CheckRaycast())
                _stateMachine.SwitchState<ActivePlayerState>();
            else
                _stateMachine.SwitchState<PassiveState>();
        }

        private bool CheckRaycast()
        {
            Vector2 rayOrigin = _mainCamera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);
            return hit.collider != null;
        }

        private void OnClickLeftDown(Vector3 cursorPosition)
        {
            if (_stateMachine.CurrentState.isDice() && !_isBlock)
                _stateMachine.CurrentState.HandleLeftClick();
            else
                StartCoroutine(BlockPlayer());
        }

        private void OnClickRightDown(Vector3 cursorPosition)
        {
            if (_stateMachine.CurrentState.isDice() && !_isBlock)
                _stateMachine.CurrentState.HandleRightClick();
            else
                StartCoroutine(BlockPlayer());
        }

        private IEnumerator BlockPlayer()
        {
            Debug.Log("Block");
            _isBlock = true;
            yield return new WaitForSeconds(_delayBlock);
            Debug.Log("NOT Block");
            _isBlock = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _stateMachine.CurrentState.HandleTriggerEnter2D(other);
        }
    }
}
