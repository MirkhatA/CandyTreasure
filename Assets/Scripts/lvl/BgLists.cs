using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgLists : MonoBehaviour
{
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

        PlayerPrefs.SetInt("hasBgWithId0", 1);
        PlayerPrefs.SetInt("hasBgWithId1", 1);
        PlayerPrefs.SetInt("hasBgWithId2", 1);
    }
}
