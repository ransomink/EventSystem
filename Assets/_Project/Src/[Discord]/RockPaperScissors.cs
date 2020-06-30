using UnityEngine;

namespace Ransomink
{
    public class RockPaperScissors : MonoBehaviour
    {
        public enum Hand
        {
            ROCK = 0, PAPER = 1, SCISSORS = 2
        }

        [SerializeField] private Hand player1;
        [SerializeField] private Hand player2;

        private void Update()
        {
            var p1 = (int)player1;
            var p2 = (int)player2;
            var result = p1 - p2;

            switch (result % 2)
            {
                case 0:
                    Debug.Log($"It's a Tie...");
                    break;
                case 1:
                    Debug.Log($"Player 1 Wins!");
                    break;
                case -1:
                    Debug.Log($"Player 2 Wins!");
                    break;
            }
        }
    }
}
