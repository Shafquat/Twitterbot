using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TweetSharp;

namespace Twitterbot
{
    class Program
    {
        static void Main(string[] args)
        {
            // Pass your credentials to the service
            TwitterService service = new TwitterService("App_ConsumerKey", "App_ConsumerSecret");
            service.AuthenticateWith("accessToken", "tokenSecret");

            SearchOptions options = new SearchOptions { Q = "indie pop", Resulttype = TwitterSearchResultType.Recent };
            var searchedTweets = service.Search(options);

            if (searchedTweets != null)
            {
                foreach (var tweet in searchedTweets.Statuses )
                {
                    if (!tweet.Text.Contains("RT"))
                    {
                        TwitterStatus s = service.SendTweet(new SendTweetOptions { Status = "RT: " + tweet.User.ScreenName + " " + tweet.Text });
                    }

                }
            }

            //shows most recent tweets in console
            var tweets = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions());
            foreach (var tweeti in tweets)
            {
                Console.WriteLine("{0} says '{1}'", tweeti.User.ScreenName, tweeti.Text);
            }
            Console.WriteLine("Press Enter to exit");
            Console.Read();

        }


    }
}
