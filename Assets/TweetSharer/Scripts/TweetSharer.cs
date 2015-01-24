using System;
using UnityEngine;

namespace UnityTweetSharer
{
    public enum TweetResult
    {
        Done,
        Cancelled
    }

    public class TweetSharer : MonoBehaviour
    {

#region Singleton
        static TweetSharer instance;
        
        public static TweetSharer Instance {
            get {
                if (instance == null) {
                    instance = FindObjectOfType (typeof(TweetSharer)) as TweetSharer;
                    if (instance == null) {
                        Debug.LogError (typeof(TweetSharer) + " is needed.");
                    }
                }
                return instance;
            }
        }

        void Awake ()
        {
            if (instance == null) {
                instance = this;
                DontDestroyOnLoad (gameObject);
            } else if (this != Instance) {
                Destroy (this);
            }
        }
#endregion
        
#if UNITY_IOS && !UNITY_EDITOR
        TweetSharerImplementor impl = new TweetSharerIos ();
#elif UNITY_ANDROID && !UNITY_EDITOR
        TweetSharerImplementor impl = new TweetSharerImplementor ();
#else
        TweetSharerImplementor impl = new TweetSharerImplementor ();
#endif
        Action<TweetResult> callback;

        public void Tweet (string text, Action<TweetResult> callback)
        {
            this.callback = callback;
            impl.Tweet (text);
        }

        public void OnComplete (string message)
        {
            if (message == "ResultDone") {
                callback (TweetResult.Done);
            } else if (message == "Cancelled") {
                callback (TweetResult.Cancelled);
            }
        }
    }
}