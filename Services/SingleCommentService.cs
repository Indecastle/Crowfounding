using Crowfounding.Data;
using Crowfounding.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.Services
{
    public class SingleCommentService
    {
        public ApplicationDbContext _db { get; set; }
        public CommentService _chatService { get; set; }
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public UserManager<User> _userManager { get; set; }

        public List<Comment> LocalComments { get; set; }
        public Company TotalCompany { get; set; }
        public User user { get; set; }

        public SingleCommentService(ApplicationDbContext db, CommentService chatService, AuthenticationStateProvider authenticationStateProvider, UserManager<User> userManager)
        {
            this._db = db;
            this._chatService = chatService;
            this.AuthenticationStateProvider = authenticationStateProvider;
            this._userManager = userManager;
        }

        public async Task<bool> InitChat(Company company, User user)
        {
            this.user = user;
            TotalCompany = GetActiveChat(company.Id);
            if (TotalCompany != null)
            {
                LocalComments = Comment.CopyComments(TotalCompany.Comments);
                return true;
            }
            return false;
        }

        public Company GetActiveChat(int companyId)
        {
            Company chat = _chatService.Companies.FirstOrDefault(c => c.Id == companyId);
            if (chat != null)
            {
            }
            else
            {
                chat = _db.Companies.Include(g => g.Comments).FirstOrDefault(c => c.Id == companyId);
                if (chat != null)
                    _chatService.Companies.Add(chat);
            }
            return chat;
        }

        public async Task<User> GetCurrentUser()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var claimUser = authState.User;
            return await _userManager.GetUserAsync(claimUser);
        }

        public void SendComment(string newMessage, User user)
        {
            var message = new Comment
            {
                UserName = user.UserName,
                UserID = user.Id,
                Text = newMessage,
                CompanyId = TotalCompany.Id,
                When = DateTime.Now
            };

            TotalCompany.Comments.Add(message);
            _db.Comments.Add(message);
            _db.SaveChanges();

            CutOutComments();

            TotalCompany.SendMessage(message);
        }

        public void GotComment(Comment newMessage)
        {
            Comment copyNewMessage = new Comment(newMessage);
            LocalComments.Add(copyNewMessage);
            if (LocalComments.Count > Company.limitMessage)
            {
                LocalComments.RemoveRange(0, LocalComments.Count - Company.limitMessage);
            }
        }

        public void RemoveSelectedComments()
        {
            List<Comment> removeMessages = LocalComments.Where(m => m.Selecting).ToList();

            removeMessages.ForEach(m => _db.Entry(m).State = EntityState.Deleted);
            _db.SaveChanges();
            Company.RemoveMessages(TotalCompany.Comments, removeMessages);
            TotalCompany.UpdateChat(removeMessages);
        }

        public void RemovedSelectedComments(List<Comment> removeMessages)
        {
            Company.RemoveMessages(LocalComments, removeMessages);
            //LocalMessages = Message.CopyMessages(groupChat.Messages);
        }

        public void ChangeComment(string textMessage, Comment editingMessage)
        {
            var message = TotalCompany.Comments.FirstOrDefault(m => m.Id == editingMessage.Id);
            message.Text = textMessage;
            _db.Entry(message).State = EntityState.Modified;
            _db.SaveChanges();

            TotalCompany.ChangeComment(textMessage, editingMessage);

        }

        public void ChangedComment(string textMessage, Comment editingMessage)
        {
            var message = LocalComments.FirstOrDefault(m => m.Id == editingMessage.Id);
            message.Text = textMessage;
        }

        public void UploadImageComment(string url, string fileName)
        {
            var message = new Comment
            {
                UserName = user.UserName,
                UserID = user.Id,
                Text = url,
                CompanyId = TotalCompany.Id,
                When = DateTime.Now,
                IsFile = true,
                FileName = fileName
            };

            TotalCompany.Comments.Add(message);
            _db.Comments.Add(message);
            _db.SaveChanges();

            CutOutComments();

            TotalCompany.SendMessage(message);
        }

        private void CutOutComments()
        {
            if (TotalCompany.Comments.Count > Company.limitMessage)
            {
                var removelist = TotalCompany.Comments.Take(TotalCompany.Comments.Count - Company.limitMessage).ToList();
                removelist.ForEach(m => _db.Entry(m).State = EntityState.Deleted);
                TotalCompany.Comments.RemoveRange(0, TotalCompany.Comments.Count - Company.limitMessage);
                _db.SaveChanges();
            }
        }
    }
}
