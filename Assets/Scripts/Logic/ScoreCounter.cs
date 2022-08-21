using UnityEngine;
using UnityEngine.Events;

namespace Game.Logic.Score
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private float score = 0;
        [SerializeField] private float minScore = 0;
        [SerializeField] private float maxScore = 100;


        public UnityEvent changeScoreEvent;
        public UnityEvent takeAwayScoreEvent;
        public UnityEvent addScoreEvent;
        public UnityEvent isMinScoreEvent;
        public UnityEvent isMaxScoreEvent;

        public float Score
        {
            get => score;
            private set => score = value;
        }

        public float MinScore
        {
            get => minScore;
            private set => minScore = value;
        }

        public float MaxScore
        {
            get => maxScore;
            private set => maxScore = value;
        }


        public void Add(float value)
        {
            if (value < 0) return;
            score = (score + value) >= maxScore ? maxScore : (score + value);

            // Events
            changeScoreEvent?.Invoke();
            addScoreEvent?.Invoke();
            if (CheckIsMax()) isMaxScoreEvent?.Invoke();
        }

        public void TakeAway(float value)
        {
            if (value < 0) return;
            score = (score - value) <= minScore ? minScore : (score - value);

            // Events
            changeScoreEvent?.Invoke();
            takeAwayScoreEvent?.Invoke();
            if (CheckIsMin()) isMinScoreEvent?.Invoke();
        }

        public bool CheckIsMin()
        {
            return score <= minScore ? true : false;
        }

        public bool CheckIsMax()
        {
            return score >= maxScore ? true : false;
        }

        public void CorrectScore()
        {
            if (CheckIsMax()) score = maxScore;
            if (CheckIsMin()) score = minScore;
        }

        private void Awake()
        {
            CorrectScore();
        }
    }
}
