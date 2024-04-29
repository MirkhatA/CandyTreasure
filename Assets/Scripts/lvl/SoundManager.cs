using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private void Update()
    {
        AudioListener.volume = PlayerPrefs.GetInt("MusicSetting");
    }
}
