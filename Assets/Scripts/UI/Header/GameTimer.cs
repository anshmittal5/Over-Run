using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] TMP_Text _gameTimer;

    float _timer;

    bool _gameRunning;

    void Awake()
    {
        _gameRunning = true;
    }

    // Track elapsed time and update the timer display.
    void Update()
    {
        if (_gameRunning)
        {
            _timer += Time.deltaTime;
            
            int mins = Mathf.FloorToInt(_timer / 60);
            int secs = Mathf.FloorToInt(_timer % 60);

            _gameTimer.text = "Timer : " + mins.ToString("00") + ":" + secs.ToString("00");
        }
    }

    public void StopUI()
    {
        _gameRunning = false;
    }

    // Provide the current elapsed time to other scripts.
    public float GiveTime()
    {
        return _timer;
    }

    public void ReSetTimer()
    {
        _timer = 0;
        _gameRunning = true;
    }
}