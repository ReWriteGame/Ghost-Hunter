using System;
using TMPro;
using UnityEngine;

namespace Game.Logic.Timer
{
    public class TimerVisual : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI output;
        [SerializeField] private Timer timer;
        [SerializeField] private int numberOfCharacters = 2;
        
        private void Update()
        {
            UpdateTime();
        }
        
        public void UpdateTime()
        {
            output.text = $"{Math.Round(timer.CurrentTime, numberOfCharacters)}";
        }
    }
}
