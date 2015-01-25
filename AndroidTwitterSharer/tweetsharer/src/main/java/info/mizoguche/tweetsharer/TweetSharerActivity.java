package info.mizoguche.tweetsharer;

import android.app.Activity;
import android.content.ActivityNotFoundException;
import android.content.Intent;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.ViewGroup;
import android.widget.ProgressBar;

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
    private static final String MESSAGE_LOAD_LIBRARIES = "Load library";

    private class TestLoadedTask extends AsyncTask<Void, Void, Void>{

        @Override
        protected Void doInBackground(Void... params) {
            boolean loading = true;
            while(loading){
                try {
                    // test if Unity native library loaded
                    UnityPlayer.UnitySendMessage(GAMEOBJECT, CALLBACK_METHOD, MESSAGE_LOAD_LIBRARIES);
                    loading = false;
                }catch(UnsatisfiedLinkError e){
                    Log.i(TAG, "Unity is loading native libraries");
                    try {
                        Thread.sleep(300);
                    } catch (InterruptedException e1) {
                    }
                }
            }
            return null;
        }

        @Override
        protected void onPostExecute(Void aVoid) {
            super.onPostExecute(aVoid);
            tweet();
        }
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        ProgressBar progressBar = new ProgressBar(this);
        addContentView(progressBar, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WRAP_CONTENT, ViewGroup.LayoutParams.WRAP_CONTENT));
        TestLoadedTask task = new TestLoadedTask();
        task.execute();
    }

    private void tweet() {
        Intent tweet = getTweetIntent();
        tweet.setAction(Intent.ACTION_SEND);
        tweet.setPackage("com.twitter.android");

        try {
            startActivityForResult(tweet, 1);
        }catch(ActivityNotFoundException e){
            Intent share = getTweetIntent();
            share.setAction(Intent.ACTION_SEND);
            startActivity(share);
        }
    }

    private Intent getTweetIntent() {
        Intent data = getIntent();
        String text = data.getStringExtra(EXTRA_KEY_TEXT);
        String imagePath = data.getStringExtra(EXTRA_KEY_IMAGE_PATH);

        Intent tweet = new Intent();
        if(text != null){
            tweet.putExtra(Intent.EXTRA_TEXT, text);
        }

        if(imagePath != null){
            File file = new File(imagePath);
            Uri uri = Uri.fromFile(file);
            tweet.putExtra(Intent.EXTRA_STREAM, uri);
        }
        tweet.setType(imagePath == null ? "text/plain" : "image/png");
        return tweet;
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if(resultCode == 0){
            UnityPlayer.UnitySendMessage(GAMEOBJECT, CALLBACK_METHOD, MESSAGE_CANCELLED);
        }else{
            UnityPlayer.UnitySendMessage(GAMEOBJECT, CALLBACK_METHOD, MESSAGE_DONE);
        }
        finish();
    }
}
