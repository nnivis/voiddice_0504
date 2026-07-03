using System;
using UnityEngine;
using UnityEngine.UI;
using CodeBase.Services.GamePlay;

namespace CodeBase.Domain.Dice
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Dice : MonoBehaviour
    {
        public event Action<Dice> OnDestroyed;
        public DiceType CurrentType => _currentType;
        public int AbilityValue => _valueAbility;
        private Rigidbody2D _rigidBody2D;
        private int _valueAbility;
        private float _speed = 2.5f;
        private DiceType _currentType;
        private DiceMove _diceMove;
        private IDiceConfigProvider _configProvider;
        private AbilityMediator _abilityMediator;

        public void Initialize(IDiceConfigProvider configProvider, Sprite icon, int valueAbility, DiceType type)
        {
            _configProvider = configProvider;
            _valueAbility = valueAbility;
            _currentType = type;

            SetIcon(icon);

            _rigidBody2D = GetComponent<Rigidbody2D>();
            _diceMove = GetComponent<DiceMove>();
            _diceMove.Initialization(_rigidBody2D, _speed);
        }

        public void SetAbilityMediator(AbilityMediator abilityMediator)
        {
            _abilityMediator = abilityMediator;
        }

        void Start()
        {
            _diceMove.AddStartingForce();
        }

        public void OnMassegeDiceLeftClick()
        {
            _abilityMediator.HandleDiceAbility(_currentType, _valueAbility);
            OnDestroyed?.Invoke(this);
            Destroy(gameObject);
        }

        public void OnMassegeDiceRightClick()
        {
            ChangeType();
        }

        public void ChangeType()
        {
            _currentType = _configProvider.GetRandomType(_currentType);
            DiceConfig newConfig = _configProvider.GetConfig(_currentType);
            UpdateConfig(newConfig);
        }

        public void UpdateConfig(DiceConfig config)
        {
            SetIcon(config.Icon);
            _valueAbility = config.Value;
            _currentType = config.Type;
        }

        public void FixedUpdate() => _diceMove.UpdateMovement();
        public void SetIcon(Sprite sprite) => GetComponentInChildren<Image>().sprite = sprite;
        public void MoveTo(Vector3 position) => transform.position = position;
    }
}
