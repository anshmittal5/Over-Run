using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RestartButtonP : MonoBehaviour
{
    [SerializeField] GameObject _pausePanel;

    [SerializeField] GameObject _playerPrefab;

    EnvironmentSpawner _ES;

    EnemySpawner _enemySpawner;

    void Awake()
    {
        GameObject es = GameObject.Find("EnvironmentSpawner");
        if(es == null)
        {
            Debug.LogError("restart button : Environment Spawner is null");
            return;
        }
        _ES = es.GetComponent<EnvironmentSpawner>();

        GameObject enemysp = GameObject.Find("EnemySpawner");
        if(enemysp == null)
        {
            Debug.LogError("restart button: enemy spawner is null");
            return;
        }
        _enemySpawner = enemysp.GetComponent<EnemySpawner>();
    }
    public void OnReStart()
    {
        Time.timeScale = 1;
        GameObject currentPlayer = GameObject.FindWithTag("Player");
        if(currentPlayer != null)
        {
            Destroy(currentPlayer);
            StartCoroutine(NewPlayer());
        }
    }

    //Wait one frame so the destroyed player is fully removed before creating the replacement.
    IEnumerator NewPlayer()
    {
        yield return new WaitForNextFrameUnit();
        Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
        _enemySpawner.StopEnemySpawning();
        _enemySpawner.StartSpawning();

        _pausePanel.SetActive(false);
        _ES.ReSpawn();
    }
}
