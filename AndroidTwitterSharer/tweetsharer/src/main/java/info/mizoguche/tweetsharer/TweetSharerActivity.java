package info.mizoguche.tweetsharer;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.util.Log;

import com.unity3d.player.UnityPlayer;

import java.io.File;

public class TweetSharerActivity extends Activity{
    private static final String TAG = "TweetSharerActivity";
    private static final String EXTRA_KEY_TEXT = "text";
    private static final String EXTRA_KEY_IMAGE_PATH = "imagePath";

    private static final String GAMEOBJECT = "TweetSharer";
    private static final String CALLBACK_METHOD = "OnComplete";
    private static final String MESSAGE_DONE = "ResultDone";
    private static final String MESSAGE_CANCELLED = "Cancelled";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        Intent data = getIntent();
        String text = data.getStringExtra(EXTRA_KEY_TEXT);
        String imagePath = data.getStringExtra(EXTRA_KEY_IMAGE_PATH);

        Intent tweet = new Intent();
        tweet.setAction(Intent.ACTION_SEND);
        tweet.setPackage("com.twitter.android");
        if(text != null){
            tweet.putExtra(Intent.EXTRA_TEXT, text);
        }

        if(imagePath != null){
            Uri uri = Uri.fromFile(new File(imagePath));
            tweet.putExtra("android.intent.extra.STREAM", uri);
        }

        tweet.setType(imagePath == null ? "text/plain" : "image/png");
        startActivityForResult(tweet, 1);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        Log.d(TAG, "resultCode: " + resultCode);
        if(resultCode == 0){
            UnityPlayer.UnitySendMessage(GAMEOBJECT, CALLBACK_METHOD, MESSAGE_CANCELLED);
        }else{
            UnityPlayer.UnitySendMessage(GAMEOBJECT, CALLBACK_METHOD, MESSAGE_DONE);
        }
        finish();
    }
}
