using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        
        public string Text { get; set; }
        public DateTime When { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserID { get; set; }
        public int CompanyId { get; set; }
        public virtual Company CompanyComment { get; set; }
        public bool IsFile { get; set; }
        public string FileName { get; set; }
        [NotMapped]
        public bool Selecting { get; set; }

        public Comment()
        {
            When = DateTime.Now;
        }

        public Comment(Comment comment)
        {
            this.Id = comment.Id;
            this.UserName = comment.UserName;
            this.Text = comment.Text;
            this.When = comment.When;
            this.Selecting = comment.Selecting;
            this.CompanyId = comment.CompanyId;
            this.CompanyComment = comment.CompanyComment;
            this.IsFile = comment.IsFile;
            this.FileName = comment.FileName;
        }

        public static List<Comment> CopyComments(List<Comment> comments)
        {
            return comments.Select(c => new Comment(c)).ToList();
        }
    }
}
