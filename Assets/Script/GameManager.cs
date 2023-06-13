using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameover;

    static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void GameOver()
    {
        gameover.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
