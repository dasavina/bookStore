namespace DapperPart.Entities
{
    public class Comment : BaseEntity
    {
        public int id { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public string CommentBody { get; set; }
    }
}
