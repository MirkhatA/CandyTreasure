using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGameScene()
    {
        AudioManager.Instance.PlaySFX("Click");
        SceneManager.LoadScene("GameScene");
    }
    
    public void LoadMainMenu()
    {
        AudioManager.Instance.PlaySFX("Click");
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadShopMenu()
    {
        AudioManager.Instance.PlaySFX("Click");
        SceneManager.LoadScene("ShopScene");
    }

    public void LoadSettingsMenu()
    {
        AudioManager.Instance.PlaySFX("Click");
        SceneManager.LoadScene("SettingsScene");
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlaySFX("Click");
        Application.Quit();
    }
}
