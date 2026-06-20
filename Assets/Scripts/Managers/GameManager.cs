using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    #region Singleton Pattern


    private static GameManager _instance;
    public static GameManager Instance => _instance;
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    #region State Variables

    [SerializeField] private int maxLives = 9;
    private int _lives = 3;
    public int lives
    {
        get { return _lives; }
        set
        {
            if (value <= 0)
            {
                _lives = 0;
                GameOver();
                return;
            }
            //if (_lives > value)
            //{
            //    Respawn();
            //}
            else
            
                _lives = value;
                if (_lives > maxLives)
            {
                _lives = maxLives;
            }

            Debug.Log($"Lives have changed to {_lives}");
        }
    }
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public delegate void PlayerInstanceDelegate(PlayerController player);
    public event PlayerInstanceDelegate OnPlayerSpawned;


    [SerializeField] private PlayerController playerPrefab;
    private PlayerController _playerInstance;
    public PlayerController PlayerInstance => _playerInstance;

    private Vector3 currentCheckpoint;

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.L))
       {
            lives++;
       }
            if (Input.GetKeyDown(KeyCode.R))
            {
                lives--;
            }
            if (SceneManager.GetActiveScene().name == "GameOver")
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    SceneManager.LoadScene("Title");
                }
            }
    }

    public void SpawnPlayer (Vector3 spawnPos)
    {
        _playerInstance = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        UpdateCheckpoint(spawnPos);

        OnPlayerSpawned?.Invoke(_playerInstance);
        _lives = 3;
    }

    public void UpdateCheckpoint(Vector3 newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
    }
    private void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("GameOver");
    }

    private void Respawn()
    {
        //_playerInstance.Anim.SetTrigger("Respawn");
        _playerInstance.transform.position = currentCheckpoint;
    }
}

