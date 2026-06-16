using System;
using UnityEngine;
using VContainer.Unity;

namespace CodeBase.Domain.Player.Input
{
    public class DesktopInput : IInput, ITickable
    {
        public event Action<Vector3> ClickLeftDown;
        public event Action<Vector3> ClickRightDown;
        private const int LeftMouseButton = 0;
        private const int RightMouseButton = 1;

        public void Tick()
        {
            ProcessLeftClickDown();
            ProcessRightClickDown();
        }

        private void ProcessLeftClickDown()
        {
            if (UnityEngine.Input.GetMouseButtonDown(LeftMouseButton))
                ClickLeftDown?.Invoke(UnityEngine.Input.mousePosition);
        }

        private void ProcessRightClickDown()
        {
            if (UnityEngine.Input.GetMouseButtonDown(RightMouseButton))
                ClickRightDown?.Invoke(UnityEngine.Input.mousePosition);
        }
    }
}
