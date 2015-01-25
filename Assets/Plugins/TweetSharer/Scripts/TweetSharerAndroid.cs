using UnityEngine;
using System;

namespace UnityTweetSharer
{
	public class TweetSharerAndroid : TweetSharerImplementor
	{
		#if UNITY_ANDROID
		public override void Tweet (string text, string url = null, string imagePath = null)
		{
			using (AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent")) {
				if (!string.IsNullOrEmpty (url)) {
					text += "\n" + url;
				}

				if (!string.IsNullOrEmpty (text)) {
					intent.Call<AndroidJavaObject> ("putExtra", "text", text);
				}
				
				if (!string.IsNullOrEmpty (imagePath)) {
					intent.Call<AndroidJavaObject> ("putExtra", "imagePath", imagePath);
				}
				
				AndroidJavaObject clazz = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
				AndroidJavaObject activity = clazz.GetStatic<AndroidJavaObject> ("currentActivity");

				
				intent.Call<AndroidJavaObject> ("setClassName", activity, "info.mizoguche.tweetsharer.TweetSharerActivity");
				activity.Call ("startActivity", intent);
			}
		}
		#endif
	}
}