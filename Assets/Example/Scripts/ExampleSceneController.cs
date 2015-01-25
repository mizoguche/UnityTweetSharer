using UnityEngine;
using UnityTweetSharer;

public class ExampleSceneController : MonoBehaviour
{
	
	public void OnClickTweet (string text)
	{
		// text: text on the new tweet
		// Callback: method receives TweetResult
		TweetSharer.Instance.Tweet (text, Callback);
	}
	
	public void OnClickTweetWithURL (string text)
	{
		// text: text on the new tweet
		// Callback: method receives TweetResult
		// url: related url
		string url = "https://github.com/mizoguche/TweetSharer";
		TweetSharer.Instance.Tweet (text, Callback, url);
	}
	
	public void OnClickTweetWithImage (string text)
	{
		// text: text on the new tweet
		// Callback: method receives TweetResult
		// url: related url
		// imagePath: attaching image path
		string url = "https://github.com/mizoguche/TweetSharer";
		TakeScreenShot ();
		string imagePath = GetScreenShotPath ();
		TweetSharer.Instance.Tweet (text, Callback, url, imagePath);
	}

	void Callback (TweetResult result)
	{
		// TweetResult.Done: complete tweeting
		// TweetResult.Cancelled: cancelled tweeting
		if (result == TweetResult.Done) {
			// Give some rewards.
		}

		Debug.Log (result.ToString ());
	}

	
	public static void TakeScreenShot ()
	{
		Application.CaptureScreenshot ("screenshot.png");
	}
	
	public static string GetScreenShotPath ()
	{
		#if UNITY_IOS && !UNITY_EDITOR
		string path = Application.persistentDataPath + "/screenshot.png";
		#elif UNITY_ANDROID && !UNITY_EDITOR
		string path = GetScreenShotPathAndroid();
		#else
		string path = "screenshot.png";
		#endif
		return path;
	}
	
	#if UNITY_ANDROID && !UNITY_EDITOR
	private static readonly string SCREENSHOT_DIR = "/Android/data/info.mizoguche.tweetsharere.sample/files";
	private static string GetStoragePath ()
	{
		AndroidJavaClass environment = new AndroidJavaClass ("android.os.Environment");
		AndroidJavaObject file = environment.CallStatic<AndroidJavaObject> ("getExternalStorageDirectory");
		string storagePath = file.Call<string> ("getAbsolutePath");
		return storagePath;
	}
	
	private static string GetScreenShotPathAndroid ()
	{
		string path = GetStoragePath () + SCREENSHOT_DIR + "/screenshot.png";
		return path;
	}
	#endif
}
