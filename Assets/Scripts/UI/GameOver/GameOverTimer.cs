using UnityEngine;
using TMPro;

public class GameOverTimer : MonoBehaviour
{
    GameTimer _gameTimer;

    [SerializeField] TMP_Text _GMTimer;

    void Awake()
    {
        GameObject time = GameObject.Find("Timer");
        if(time == null)
        {
            Debug.LogError("game over timer : game timer is null");
            return;
        }
        _gameTimer = time.GetComponent<GameTimer>();
    }

    // Update the final time displayed on the game-over screen.
    void Update()
    {
        float finalTimer = _gameTimer.GiveTime();
        int mins = Mathf.FloorToInt(finalTimer / 60);
        int secs = Mathf.FloorToInt(finalTimer % 60);
        _GMTimer.text = "Final Time : " + mins.ToString("00") + ":" + secs.ToString("00");
    }
}
