using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text totalScoreText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private GameObject winBoardUI;
    [SerializeField] private float timeLeft;
    [SerializeField] private Image bgImage;
    [SerializeField] private Slider slider;
    [SerializeField] private Sprite[] bgImages;

    [SerializeField] private Sprite[] themeUI;
    [SerializeField] private Image board;

    private int _points;
    private int _iterablePoint;
    private int _levelNumber;
    private bool _timerOn = false;
    private float _money;

    private void Start()
    {
        Application.targetFrameRate = 100;

        _points = 0;
        _iterablePoint = 0;
        _levelNumber = 1;
        _timerOn = true;

        int bgIndex = PlayerPrefs.GetInt("backgroundIndex", 0);
        bgImage.sprite = bgImages[bgIndex];

        board.sprite = themeUI[PlayerPrefs.GetInt("DarkThemeSetting")];

        winBoardUI.SetActive(false);
    }

    private void Update()
    {
        _money = PlayerPrefs.GetFloat("money", 0f);
        moneyText.text = _money.ToString();

        if (_iterablePoint >= 50) {
            SetNextLevel();
        }

        if (_timerOn) {
            if (timeLeft > 0) {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            } else {
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
        _iterablePoint += 1;
        // set _iterablePoint slider.value 
        // slider.value takes float value from 0 to 1
        // _iterablePoint can be from 0 to 50
        float sliderValue = (float)_iterablePoint / 50f;
        slider.value = sliderValue;

        if (_iterablePoint >= 50)
        {
            SetNextLevel();
        }
    }

    private void SetNextLevel()
    {
        _levelNumber += 1;
        levelText.text = "Óð. " + _levelNumber.ToString();
        _iterablePoint = 0;
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timeText.text = string.Format("{0:00} : {1:00}", (float)minutes, (float)seconds);
    }
}
