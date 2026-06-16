using System;
using UnityEngine;

namespace CodeBase.Domain.Player.Input
{
    public class InputHandler : IDisposable
    {
        private IInput _input;
        public Action<Vector3> ClickLeftDown { get; internal set; }
        public Action<Vector3> ClickRightDown { get; internal set; }

        public InputHandler(DesktopInput input)
        {
            _input = input;
            _input.ClickLeftDown += OnLeftClickDown;
            _input.ClickRightDown += OnRightClickDown;
        }

        private void OnLeftClickDown(Vector3 vector) => ClickLeftDown?.Invoke(vector);
        private void OnRightClickDown(Vector3 vector) => ClickRightDown?.Invoke(vector);

        public void Dispose()
        {
            _input.ClickLeftDown -= OnLeftClickDown;
        }
    }
}
