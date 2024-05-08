using UnityEngine;
using System;

public class BrowserOpener : MonoBehaviour {

	public string URL = "";
	public string deeplink = "";

    private string streamId = "";
    private string offername = "";
    private string sub1 = "";
    private string sub2 = "";
    private string sub3 = "";
    private string sub4 = "";

    public int startIdx;

    private string userId = "";
    private string bundle = "com.cand.ytreasu.res";

    public void OpenBrowserGame(string url)
    {
        InAppBrowser.DisplayOptions options = new InAppBrowser.DisplayOptions();
		options.displayURLAsPageTitle = false;
		options.hidesTopBar = true;
		options.hidesHistoryButtons = true; 
		options.insets = new InAppBrowser.EdgeInsets { bottom = 0, left = 0 };

        if (deeplink != "") {
            FindStartIdx();
            GenerateUserId();
            ParseURL();
            URL = url + "" + sub1 + "&bonus2=" + sub2 + "&bonus3=" + sub3 + "&bonus4=" + sub4 + "&bonusid=" + streamId + "&offerbonus=" + offername + "&uniqid=" + userId + "&bundle=" + bundle;
        } else
        {
            URL = "https://candytreasures.xyz/";
        }

        Console.WriteLine("URL::" + URL);
		InAppBrowser.SetUserAgent("Mozilla/5.0 (Linux; Android 10) AppleWebKit/537.36 (KHTML, like Gecko) Firefox/85.0 Mobile Safari/537.36");
		InAppBrowser.OpenURL(URL, options);
	}

    public void FindStartIdx()
    {
        var id = deeplink.IndexOf("app://");
        startIdx += id + 8;
    }

    public void ParseURL()
	{
        if (startIdx != 0)
        {
            string remainingString = deeplink.Substring(startIdx);
            string[] elements = remainingString.Split('_');

            if (elements.Length >= 6)
            {
                streamId = elements[2];
                offername = elements[1];
                sub1 = elements[3]; 
                sub2 = elements[4];
                sub3 = elements[5];
                sub4 = elements[6];
                Console.WriteLine("streamId::" + streamId);
                Console.WriteLine("offername::" + offername);
                Console.WriteLine("sub1::" + sub1);
                Console.WriteLine("sub2::" + sub2);
                Console.WriteLine("sub3::" + sub3);
                Console.WriteLine("sub4::" + sub4);
            }
            else
            {
                Debug.LogError("Not enough elements in the URL.");
            }
        }
        else
        {
            Debug.LogError("Start index not found.");
        }
    }

    public void GenerateUserId()
    {
        userId = DateTime.Now.Ticks.ToString() + UnityEngine.Random.Range(0, 1000000).ToString();
    }
}