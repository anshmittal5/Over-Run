using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] TMP_Text _score;

    int _scoreAmt;

    public void AddScore(int enemyScore)
    {
        _scoreAmt += enemyScore;
        _score.text = "Score : " + _scoreAmt;
    }

    public int GiveScore()
    {
        return _scoreAmt;
    }

    public void ReSetScore()
    {
        _scoreAmt = 0;
        _score.text = "Score : " + _scoreAmt;
    }

}