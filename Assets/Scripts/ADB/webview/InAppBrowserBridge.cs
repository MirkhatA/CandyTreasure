using UnityEngine;
using UnityEngine.Events;

public class InAppBrowserBridge : MonoBehaviour {

	[System.Serializable]
	public class BrowseLoad: UnityEvent<string> {
		
	}

	[System.Serializable]
	public class BrowseLoadError: UnityEvent<string, string> {
		
	}

	public BrowseLoad jsBrowser = new BrowseLoad();

	public BrowseLoad loadFinished = new BrowseLoad();

	public BrowseLoad browserStart = new BrowseLoad();

	public BrowseLoadError finishWithError = new BrowseLoadError();

	public UnityEvent closeB = new UnityEvent();

	public UnityEvent backBtn = new UnityEvent();

	void JSBrowser(string callback) {
        jsBrowser.Invoke(callback);
	}

	void LoadFinished(string url) {
		loadFinished.Invoke(url);
	}

	void BrowserStart(string url) {
        browserStart.Invoke(url);
	}

	void FinishWithError(string urlAndError) {
		string[] parts = urlAndError.Split(',');
        finishWithError.Invoke(parts[0], parts[1]);
	}

	void CloseB() {
		closeB.Invoke();
	}

	void BackBtn() {
		backBtn.Invoke();
	}
}
