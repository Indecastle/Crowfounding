using Crowfounding.Models.Finance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.Models
{
    public enum Theme
    {
        Electronics,
        Education,
        Service,
        ITcompany,
        Food
    }

    public class Company
    {
        public const int limitMessage = 20;

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Theme Theme { get; set; }
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }
        public int AllRating { get; set; }
        public int NumberUsers { get; set; }
        public double Rating { get; set; }
        [Required]
        public decimal CurrentMoney { get; set; }
        [Required]
        public decimal NeedMoney { get; set; }
        public string URLVideo { get; set; }
        public DateTime LastEdit { get; set; }
        public DateTime DataCreate { get; set; }
        [Required]
        public string MainImage { get; set; }
        public bool EndAtNextDay { get;set;}
        public bool IsEnd { get; set; }

        public virtual List<ImagesCompany> CompanyImages { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public string UserID { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Donation> Donations { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<TotalDonate> TotalDonates { get; set; }
        public virtual ICollection<Bonuse> Bonuses { get; set; }
        
        public virtual ICollection<UserBonuse> UserBonuses { get; set; }


        public event Action<Comment, object> Sent;
        public event Action<List<Comment>> Updated;
        public event Action<string, Comment> ChangedComment;
        public event Action UpdatedRating;

        public void SendMessage(Comment comment, object sender)
        {
            Sent?.Invoke(comment, sender);
        }

        public void UpdateChat(List<Comment> removeComments)
        {
            Updated?.Invoke(removeComments);
        }

        public void ChangeComment(string textComment, Comment editingComment)
        {
            ChangedComment.Invoke(textComment, editingComment);
        }

        public void UpdateRating()
        {
            UpdatedRating?.Invoke();
        }

        static public void RemoveMessages(List<Comment> comments, List<Comment> removedComments)
        {
            removedComments.ForEach(m => comments.RemoveAt(comments.FindIndex(m2 => m.Id == m2.Id)));
        }
    }
}
