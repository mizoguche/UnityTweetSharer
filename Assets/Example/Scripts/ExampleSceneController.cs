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

    void Callback (TweetResult result)
    {
        // TweetResult.Done: complete tweeting
        // TweetResult.Cancelled: cancelled tweeting
        if (result == TweetResult.Done) {
            // Give some rewards.
        }

        Debug.Log (result.ToString ());
    }
}
