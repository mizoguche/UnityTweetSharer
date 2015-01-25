Tweet Sharer
===

## Overview
Tweet Sharer make easier to tweet and to receive tweet result in Unity.

## Feature
- Tweet by a simple method call
- Receive tweet result by your delegate method

## Usage
### 1. Put TweetSharer gameobject in your scene
Put `Assets/TweetSharer/Prefabs/TweetSharer.prefab` in your scene.

This gameobject is singleton and not destroyed on load another scene.

### 2. Call TweetSharer#Tweet
Call `TweetSharer#Tweet`

```csharp
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
```

## License
[MIT](https://github.com/mizoguche/TweetSharer/blob/master/LICENSE)
