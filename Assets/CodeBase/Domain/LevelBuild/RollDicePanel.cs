using System;
using UnityEngine;
using CodeBase.Domain.LevelBuild.View;
using CodeBase.Domain.Location;
using CodeBase.Domain.RollDice;

namespace CodeBase.Domain.LevelBuild
{
    public class RollDicePanel : MonoBehaviour
    {
        public Action<int> OnGenereteRollDiceDone;
        [SerializeField] private RollDiceButton _rollDiceButton;
        [SerializeField] private Transform _parentSpawn;
        private RollDiceView _rollDiceView;
        private ILocationProvaider _locationProvaider;

        public void Initialize(ILocationProvaider locationProvaider)
        {
            _locationProvaider = locationProvaider;
            _rollDiceButton.OnActiveRollDiceClicked += StartGeneratedRollDice;
        }

        public void StartGeneratedRollDice()
        {
            _rollDiceButton.DeactivateRollDiceButton();
            _rollDiceView = SpawnRollDice();
            _rollDiceView.OnNumberOfLevels += GenereteRollDiceDone;
            _rollDiceView.ActivateRollDiceView();
        }

        private void GenereteRollDiceDone(int numberOfLevel)
        {
            OnGenereteRollDiceDone(numberOfLevel);
        }

        private RollDiceView SpawnRollDice()
        {
            RollDiceView rollDiceView = Instantiate(_locationProvaider.rollDiceConfig.RollDice, _parentSpawn);
            rollDiceView.Initialize(_locationProvaider.rollDiceConfig);
            rollDiceView.transform.localScale = Vector3.one;
            rollDiceView.DeactivateRollDiceView();
            return rollDiceView;
        }

        public void Clear()
        {
            Destroy(_rollDiceView);
            _rollDiceButton.ActiveRollDiceButton();
        }
    }
}
