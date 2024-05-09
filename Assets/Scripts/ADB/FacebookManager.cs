using Facebook.Unity;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;

public class FacebookManager : MonoBehaviour
{
    public string deepLink;
    public BrowserOpener browserOpener;
    public string starterUrl = "http://candytreasures.xyz?bonus1=";

    public string DEEPLINK_STR = "LinkFB";

    void Start()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
    }

    void DeepLinkCallback(IAppLinkResult linkResult)
    {
        if (!string.IsNullOrEmpty(linkResult.TargetUrl) || PlayerPrefs.GetString(DEEPLINK_STR) != "")
        {
            var res = linkResult.TargetUrl;
            Console.WriteLine("res::" + res);
            if (res != "null")
            {
                deepLink = $"54mGbLCw?{res}";
                browserOpener.deeplink = deepLink;
                browserOpener.OpenBrowserGame(starterUrl);
            }
        }
        else
        {
            SceneManager.LoadScene(1);
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }

    private void InitCallback()
    {
        Console.WriteLine("InitCallback!");
        if (FB.IsInitialized)
        {
            Console.WriteLine("IsInitialized!");
            FB.ActivateApp();
            FB.Mobile.FetchDeferredAppLinkData(DeepLinkCallback);
        }
        else
        {
            Console.WriteLine("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    
}
