using UnityEngine;
using System;

#if UNITY_IPHONE
using System.Runtime.InteropServices;
#endif

namespace UnityTweetSharer
{
    public class TweetSharerIos : TweetSharerImplementor
    {
#if UNITY_IPHONE
        [DllImport("__Internal")]
        private static extern void TweetSharer_Tweet (string text, string url, string imagePath);

        public override void Tweet (string text, string url = null, string imagePath = null)
        {
            TweetSharer_Tweet (text, url, imagePath);
        }
#endif
    }
}