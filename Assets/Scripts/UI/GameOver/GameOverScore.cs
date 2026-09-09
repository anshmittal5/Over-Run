using TMPro;
using UnityEngine;

public class GameOverScore : MonoBehaviour
{
    Score _score;

    [SerializeField] TMP_Text _GMScore;

    void Awake()
    {
        GameObject sco = GameObject.Find("Score");
        if(!sco.TryGetComponent(out _score))
        {
            Debug.LogError("game over score : score is null");
            return;
        }
    }

    // Update the final score displayed on the game-over screen.
    void Update()
    {
        int FinalScore = _score.GiveScore();
        _GMScore.text = "SCORE - " + FinalScore;
    }
}
