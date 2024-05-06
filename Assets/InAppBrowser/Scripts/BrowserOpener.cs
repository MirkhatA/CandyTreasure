using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.Android;

public class BrowserOpener : MonoBehaviour {

	private void Start()
    {
		StartWebView("https://www.google.com/");
	}

	private void StartWebView(string url)
    {

		InAppBrowser.DisplayOptions options = new InAppBrowser.DisplayOptions();
		options.displayURLAsPageTitle = false;
		options.hidesTopBar = true;
		options.hidesHistoryButtons = true; 
		options.insets = new InAppBrowser.EdgeInsets { bottom = 0, left = 0 };
		//options.androidBackButtonCustomBehaviour = true;


		InAppBrowser.SetUserAgent("Mozilla/5.0 (Linux; Android 10) AppleWebKit/537.36 (KHTML, like Gecko) Firefox/85.0 Mobile Safari/537.36");
		InAppBrowser.OpenURL(url, options);
	}

}