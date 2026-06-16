using UnityEngine;

namespace CodeBase.Domain.Player
{
    public class MoveTowardsCursor
    {
        public void MoveTowards(Transform transform, Camera mainCamera)
        {
            Vector3 mousePosition = new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, transform.position.z);
            Vector3 playerPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            transform.position = playerPosition;
        }
    }
}
