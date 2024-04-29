using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text totalScoreText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private GameObject winBoardUI;
    [SerializeField] private float timeLeft;

    private int _points;
    private int _levelNumber;
    private bool _timerOn = false;
    private float _money;

    private void Start()
    {
        _points = 0;
        _levelNumber = 1;
        _timerOn = true;
        
        winBoardUI.SetActive(false);
    }

    private void Update()
    {
        _money = PlayerPrefs.GetFloat("money", 0f);
        moneyText.text = _money.ToString();

        if (_points >= 50)
        {
            SetNextLevel();
        }

        if (_timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else
            {
                timeLeft = 0;
                _timerOn = false;
                winBoardUI.SetActive(true);
                totalScoreText.text = _points.ToString();
            }
        }
    }

    public void IncreasePoints()
    {
        _points += 1;
        pointsText.text = _points.ToString() + "/50";
    }

    private void SetNextLevel()
    {
        _levelNumber += 1;
        levelText.text = "Óð. " + _levelNumber.ToString();
        _points = 0;
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timeText.text = string.Format("{0:00} : {1:00}", (float)minutes, (float)seconds);
    }
}
