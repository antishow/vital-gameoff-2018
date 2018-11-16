using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameController : MonoBehaviour {
    private static GameController _instance;
    const int STARTING_LIVES = 3;

    public int Level = 0;
    public int Score = 0;
    public int Lives = 0;

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

    public static void ResetActors() {
        Respawner[] actors = FindObjectsOfType<Respawner>().Select(r => {
            r.Respawn();
            return r;
        }).ToArray();
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
        Lives = STARTING_LIVES;
        GoToLevel(1);
    }

    public static void EatDot() {
        if (GetDotCount() == 0) {
            _instance.Invoke("GoToNextLevel", 1.0f);
        }
    }

    public static void GetPoints(int points) {
        _instance.Score += points;
    }
}
