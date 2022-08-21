using System;
using TMPro;
using UnityEngine;


namespace Game.Logic.Score
{
    public class ScoreCounterVisual : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI output;
        [SerializeField] private ScoreCounter scoreCounter;
        [SerializeField] private int numberOfCharacters = 2;
        
        private void Start()
        {
            UpdateScore();
        }
        
        public void UpdateScore()
        {
            output.text = $"{Math.Round(scoreCounter.Score, numberOfCharacters)}";
        }

        private void OnEnable()
        {
            scoreCounter.changeScoreEvent.AddListener(UpdateScore);
            UpdateScore();
        }

        private void OnDisable()
        {
            scoreCounter.changeScoreEvent.RemoveListener(UpdateScore);
        }
    }
}