using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Snake snake;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private int score;

    public AudioSource bgm;

    public void Start() {
        NewGame();
        Cursor.visible = false;
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (bgm.isPlaying) {
                bgm.Pause();
            } else {
                bgm.UnPause();
            }
        }
    }

    public void NewGame() {
        SetScore(0);
        highScoreText.text = LoadHighScore().ToString();

        snake.ResetState();
    }

    public void IncreaseScore(int points) {
        SetScore(score + points);
    }

    private void SetScore(int score) {
        this.score = score;

        scoreText.text = score.ToString();

        SaveHighScore();
    }

    private void SaveHighScore() {
        int highScore = LoadHighScore();

        if (score > highScore) {
            PlayerPrefs.SetInt("highScore", score);
        }
    }

    private int LoadHighScore() {
        return PlayerPrefs.GetInt("highScore", 0);
    }
}
