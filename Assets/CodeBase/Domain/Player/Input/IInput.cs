using System;
using UnityEngine;

namespace CodeBase.Domain.Player.Input
{
    public interface IInput
    {
        event Action<Vector3> ClickLeftDown;
        event Action<Vector3> ClickRightDown;
    }
}
