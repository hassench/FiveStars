using Entities.Models;

namespace Entities.ViewModels
{
    public class CommentViewModel
    {
        public int     ZoneId  { get; set; }
        public Comment comment { get; set; }
    }
}
