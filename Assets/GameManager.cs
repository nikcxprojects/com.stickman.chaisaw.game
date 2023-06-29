using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Main")] 
    [SerializeField] private PlayerController player;
    [SerializeField] private TopScores topScore;

    [Header("UI")] 
    [SerializeField] private Text scoreText;
    [SerializeField] private Text scoreFinishText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private AudioClip gameOverClip;

    private int _time;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        StartCoroutine(TimeCounter());
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        scoreFinishText.text = _time.ToString();
        player.StopMovement();   
        AudioManager.getInstance().PlayAudio(gameOverClip);
        AudioManager.getInstance().Vibrate(500);
        topScore.Scores.Add(_time);
        StopAllCoroutines();
    }

    private IEnumerator TimeCounter()
    {
        while (true)
        {
            scoreText.text = _time.ToString();
            yield return new WaitForSeconds(1);
            _time++;
        }
    }
}
