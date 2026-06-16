using UnityEngine;
using CodeBase.Domain.Dice;

namespace CodeBase.Domain
{
    public class BortSurface : MonoBehaviour
    {
        public enum ForceType
        {
            Additive,
            Multiplicative,
        }

        public ForceType forceType = ForceType.Additive;
        public float bounceStrength = 0f;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            DiceMove dice = collision.gameObject.GetComponent<DiceMove>();

            if (dice != null)
            {
                switch (forceType)
                {
                    case ForceType.Additive:
                        dice._currentSpeed += bounceStrength;
                        break;
                    case ForceType.Multiplicative:
                        dice._currentSpeed *= bounceStrength;
                        break;
                }
            }
        }
    }
}
