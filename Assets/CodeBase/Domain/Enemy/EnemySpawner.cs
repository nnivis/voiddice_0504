using UnityEngine;
using VContainer;
using CodeBase.Domain.Location;
using CodeBase.Services.GamePlay;

namespace CodeBase.Domain.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        private const float BaseScale = 3.0f;
        private GamePlayMediator _gamePlayMediator;
        private EnemyFactory _enemyFactory;
        private ILocationProvaider _locationProvaider;

        [Inject]
        private void Construct(GamePlayMediator gamePlayMediator, ILocationProvaider locationProvaider)
        {
            _gamePlayMediator = gamePlayMediator;
            _locationProvaider = locationProvaider;
        }

        public Enemy SpawnEnemy()
        {
            _enemyFactory = _locationProvaider.enemyFactory;
            Enemy enemy = _enemyFactory.Get();
            enemy.onDead += (GameOverType) => _gamePlayMediator.NotifyGameOver(enemy.GameOverType);
            enemy.transform.SetParent(_spawnPoint);
            enemy.transform.localScale = new Vector3(BaseScale, BaseScale, BaseScale);
            enemy.transform.position = _spawnPoint.position;
            return enemy;
        }
    }
}
