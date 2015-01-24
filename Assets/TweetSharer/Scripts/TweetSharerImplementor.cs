using System;
using UnityEngine;

namespace UnityTweetSharer
{
    public class TweetSharerImplementor
    {
        public virtual void Tweet (string text, string url = null, string imagePath = null)
        {
            Debug.Log ("Tweet text: " + text);
            Debug.Log ("Tweet failed because not running on iOS or Android.");

            TweetSharer.Instance.OnComplete ("Cancelled");
        }
    }
}