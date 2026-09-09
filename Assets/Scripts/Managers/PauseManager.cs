using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    PlayerControls _controls;

    [SerializeField] GameObject _pausePanel;

    [SerializeField] GameObject _gameOverPanel;

    [SerializeField] AudioSource _backgroundMusic;

    void Awake()
    {
        _controls = new PlayerControls();
        _controls.Pause.PauseButton.performed += OnPause;
    }

    void OnEnable()
    {
        _controls.Enable();
    }

    void OnDisable()
    {
        _controls.Disable();
    }

    // Toggle pause only while the game is not over.
    void OnPause(InputAction.CallbackContext context)
    {
        if(!_gameOverPanel.activeSelf)
        {
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0f;
                _backgroundMusic.Pause();
                _pausePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                _backgroundMusic.UnPause();
                _pausePanel.SetActive(false);
            }
        }
    }
}
