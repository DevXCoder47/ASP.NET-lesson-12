namespace BookManagementAPI
{
    public struct BookFilter
    {
        public string? AuthorFirstName { get; set; }
        public string? Genre { get; set; }
        public DateTime? PublicationDate { get; set; }
    }
}
