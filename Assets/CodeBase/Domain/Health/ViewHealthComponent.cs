using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Domain.Health
{
    public class ViewHealthComponent : MonoBehaviour
    {
        [SerializeField] Image _healthImage;

        public void UpdateHealth(int currentHealth, int maxHealth)
        {
            float healthPercentage = (float)currentHealth / maxHealth * 100f;
            _healthImage.fillAmount = healthPercentage / 100f;
        }
    }
}
