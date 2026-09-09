using UnityEngine;

public class ReStartButton : MonoBehaviour
{
    [SerializeField] GameObject _gameOverScreen;

    [SerializeField] GameObject _playerPrefab;

    [SerializeField] AudioSource _backgroundMusic;

    EnvironmentSpawner _ES;

    void Awake()
    {
        GameObject es = GameObject.Find("EnvironmentSpawner");
        if(es == null)
        {
            Debug.LogError("restart button : Environment Spawner is null");
            return;
        }
        _ES = es.GetComponent<EnvironmentSpawner>();
    }
    public void OnReStart()
    {
        Time.timeScale = 1;
        _gameOverScreen.SetActive(false);
        Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
        _backgroundMusic.Play();
        _ES.ReSpawn();
    }
}
