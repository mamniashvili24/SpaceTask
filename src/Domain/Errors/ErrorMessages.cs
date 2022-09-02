namespace Domain.Errors
{
    public static class ErrorMessages
    {
        public static string ApiCallError => nameof(ApiCallError);
        public static string ApiResponseIsEmpty => nameof(ApiResponseIsEmpty);
        public static string FilmNotExistInWatchList => nameof(FilmNotExistInWatchList);
        public static string UserIdShouldNotBeNullOrEmpty => nameof(UserIdShouldNotBeNullOrEmpty);
        public static string FilmIdShouldNotBeNullOrEmpty => nameof(FilmIdShouldNotBeNullOrEmpty);
        public static string IsWatchedShouldNotBeNullOrEmpty => nameof(IsWatchedShouldNotBeNullOrEmpty);
        public static string WatchlistIdShouldNotBeNullOrEmpty => nameof(WatchlistIdShouldNotBeNullOrEmpty);
    }
}