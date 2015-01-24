#import <QuartzCore/QuartzCore.h>
#import "Social/Social.h"

extern "C" {
    void TweetSharer_Tweet(const char *text, const char *url, const char *imagePath) {

        NSString *_text = [NSString stringWithUTF8String:text ? text : ""];
        NSString *_url = [NSString stringWithUTF8String:url ? url : ""];
        NSString *_imagePath = [NSString stringWithUTF8String:imagePath ? imagePath : ""];

        SLComposeViewController *composeViewController = [SLComposeViewController composeViewControllerForServiceType:SLServiceTypeTwitter];

        composeViewController.completionHandler = ^(SLComposeViewControllerResult res) {
            if (res == SLComposeViewControllerResultCancelled) {
                UnitySendMessage("TweetSharer", "OnComplete", "Cancelled");
            }else if (res == SLComposeViewControllerResultDone) {
                UnitySendMessage("TweetSharer", "OnComplete", "ResultDone");
            }
            [composeViewController dismissViewControllerAnimated:YES completion:nil];
        };

        if ([_text length] != 0) {
            [composeViewController setInitialText:_text];
        }

        if ([_url length] != 0) {
            [composeViewController addURL:[NSURL URLWithString:_url]];
        }
        
        if ([_imagePath length] != 0) {
            UIImage *image = [UIImage imageWithContentsOfFile:_imagePath];
            if (image != nil) {
                [composeViewController addImage:image];
            }
        }

        [UnityGetGLViewController() presentViewController:composeViewController animated:YES completion:nil];
    }
}