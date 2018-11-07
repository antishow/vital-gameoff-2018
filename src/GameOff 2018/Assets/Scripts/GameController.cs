using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    private static GameController _instance;
    const int STARTING_LIVES = 3;

    public int Level = 0;
    public int Score = 0;

    private void Awake() {
        GameObject gc = GameObject.Find("GameController");
        if (gc && gc != this.gameObject) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start() {
        NewGame();
    }

    private void GoToLevel(int n) {
        Debug.LogFormat("Starting Level {0}!", n);

        SceneManager.LoadSceneAsync(1);
        Level = n;
    }

    private void GoToNextLevel() {
        GoToLevel(Level + 1);
    }

    private static int GetDotCount() {
        return GameObject.FindGameObjectsWithTag("Dot").Length;
    }

    public void NewGame() {
        Debug.Log("NEW GAME!");
        Score = 0;
        GoToLevel(1);
    }

    public static void EatDot() {
        //Minus 1 because the dot I just ate still exists
        int count = GetDotCount() - 1;

        Debug.LogFormat("There are {0} dots left!!", count);
        if (count == 0) {
            Debug.Log("ATE EM ALL!");
            _instance.Invoke("GoToNextLevel", 1.0f);
        }
    }

    public static void GetPoints(int points) {
        Debug.LogFormat("You got {0} points. Great Job!", points);
        _instance.Score += points;
        Debug.LogFormat("  Your total score is now {0}", _instance.Score);
    }
}
