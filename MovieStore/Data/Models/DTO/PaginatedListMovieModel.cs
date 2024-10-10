namespace MovieStore.Data.Models.DTO
{
    public class PaginatedListMovieModel
    {
        public List<MovieModel> MovieList { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public string? Term { get; set; }
    }
}