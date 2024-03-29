﻿namespace MinimalWebApi.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime? PublishedAt { get; set; }
    }

    public record ArticleRequest(string? Title, string? Content, DateTime? PublishedAt);
}
