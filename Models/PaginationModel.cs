namespace TherapEase.Models
{
    public class PaginationModel<T>
    {
        public int Total { get; set; }

        public required List<T> Data { get; set; }
    }
}
