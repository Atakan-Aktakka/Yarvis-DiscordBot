using System;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace Yarvis.YoutubeAPI
{
	public class Engine
	{
        public string videoId = "";//channel_id
        public string apiKey = "";//google cloud with add
        public YoutubeVideo _video = new YoutubeVideo();

        public YoutubeVideo GetLatestVideo(string channelId, string apiKey)
        {
             string videoId;
             string videoTitle;
             string videoUrl;
             string thumbnail;
             DateTime? videoPublishedAt;

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = "MyApplication"
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.ChannelId = channelId;
            searchListRequest.MaxResults = 1;
            searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Date;

            var searchListResponse = searchListRequest.Execute();

            foreach(var searchResult in searchListResponse.Items)
            {
                if (searchResult.Id.Kind == "youtube#video")
                {
                    videoId = searchResult.Id.VideoId;
                    videoUrl = $"https://www.youtube.com/watch?v={videoId}";
                    videoTitle = searchResult.Snippet.Title;
                    videoPublishedAt = searchResult.Snippet.PublishedAt;
                    thumbnail = searchResult.Snippet.Thumbnails.Default__.Url;

                    return new YoutubeVideo()
                    {
                        videoId = videoId,
                        videoUrl = videoUrl,
                        videoTitle = videoTitle,
                        thumbnail = thumbnail,
                        PublishedAt = videoPublishedAt
                    };
                }
            }
            return null;
        }
    }
}

