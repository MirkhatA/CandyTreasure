using UnityEngine;
using UnityEngine.UI;

public class SwitchSetting : MonoBehaviour
{
    [SerializeField] private Sprite toggleOff;
    [SerializeField] private Sprite toggleOn;

    [SerializeField] private Image darkThemeSprite;
    [SerializeField] private Image musicSprite;
    [SerializeField] private Image soundSprite;
    [SerializeField] private Image notificationSprite;

    private bool _darkThemeStatus;
    private bool _musicStatus;
    private bool _soundStatus;
    private bool _notificationStatus;

    [Header("Theme")]
    [SerializeField] private Sprite[] boardSprites;
    [SerializeField] private Sprite[] headerSprites;
    [SerializeField] private Sprite[] closeSprites;
    [SerializeField] private Image boardBg;
    [SerializeField] private Image headerBg;
    [SerializeField] private Image closeBg;

    private void Start()
    {
        boardBg.sprite = boardSprites[PlayerPrefs.GetInt("DarkThemeSetting")];
        headerBg.sprite = headerSprites[PlayerPrefs.GetInt("DarkThemeSetting")];
        closeBg.sprite = closeSprites[PlayerPrefs.GetInt("DarkThemeSetting")];

        if (PlayerPrefs.GetInt("DarkThemeSetting") == 1) _darkThemeStatus = true;
        if (PlayerPrefs.GetInt("MusicSetting") == 1) _musicStatus = true;
        if (PlayerPrefs.GetInt("SoundSetting") == 1) _soundStatus = true;
        if (PlayerPrefs.GetInt("NotificationSetting") == 1) _notificationStatus = true;

        SetSprite(darkThemeSprite, _darkThemeStatus);
        SetSprite(musicSprite, _musicStatus);
        SetSprite(soundSprite, _soundStatus);
        SetSprite(notificationSprite, _notificationStatus);
    }

    public void SetToggleDarkTheme()
    {
        _darkThemeStatus = !_darkThemeStatus;
        PlayerPrefs.SetInt("DarkThemeSetting", (_darkThemeStatus) ? 1 : 0);
        SetSprite(darkThemeSprite, _darkThemeStatus);

        boardBg.sprite = boardSprites[PlayerPrefs.GetInt("DarkThemeSetting")];
        headerBg.sprite = headerSprites[PlayerPrefs.GetInt("DarkThemeSetting")];
        closeBg.sprite = closeSprites[PlayerPrefs.GetInt("DarkThemeSetting")];
    }

    public void SetToggleMusic()
    {
        _musicStatus = !_musicStatus;
        PlayerPrefs.SetInt("MusicSetting", (_musicStatus) ? 1 : 0);
        SetSprite(musicSprite, _musicStatus);
    }

    public void SetToggleSound()
    {   
        _soundStatus = !_soundStatus;
        PlayerPrefs.SetInt("SoundSetting", (_soundStatus) ? 1 : 0);
        SetSprite(soundSprite, _soundStatus);
    }

    public void SetToggleNotification()
    {
        _notificationStatus = !_notificationStatus;
        PlayerPrefs.SetInt("NotificationSetting", (_notificationStatus) ? 1 : 0);
        SetSprite(notificationSprite, _notificationStatus);
    }

    public void SetSprite(Image obj, bool isOn)
    {
        if (isOn) {
            obj.sprite = toggleOn;
        } else {
            obj.sprite = toggleOff;
        }
    }

    public void SetSprite(Image obj, int isOn)
    {
        if (isOn == 1) {
            obj.sprite = toggleOn;
        } else
        {
            obj.sprite = toggleOff;
        }
    }

    public void ResetSettings()
    {
        if (_darkThemeStatus) SetToggleDarkTheme();
        if (_musicStatus) SetToggleMusic();
        if (_soundStatus) SetToggleSound();
    }
}
