using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assignment2.Models;
using Assignment2.ViewModels;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment2.Controllers
{
    public class Home : Controller
    {

        private Assignment2DataContext _context;

        public Home(Assignment2DataContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_context.BlogPosts.ToList());
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {


            var existingAccount = (from u in _context.Users
                                   where (u.EmailAddress == user.EmailAddress)
                                   select u).FirstOrDefault();

            if (existingAccount == null)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AuthenticateUser()
        {
            String formEmail = Request.Form["EmailAddress"];
            String formPassword = Request.Form["Password"];
            System.Console.WriteLine("Looking for user");
            var user = (from u in _context.Users
                        where (u.EmailAddress == formEmail && u.Password == formPassword)
                        select u).FirstOrDefault();
            if (user != null)
            {
                System.Console.WriteLine("Found user");
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetInt32("RoleId", user.RoleId);
                HttpContext.Session.SetString("UserName", user.FirstName + " " + user.LastName);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DisplayFullBlogPost(int id)
        {
            var blogPost = (from item in _context.BlogPosts
                            where item.BlogPostId == id
                            select item).FirstOrDefault();
            if (blogPost != null)
            {
                BlogPostViewModel viewModel = new BlogPostViewModel();
                viewModel.blogPost = blogPost;
                viewModel.comments = new List<CommentViewModel>();

                var commentList = (from item in _context.Comments
                                      where item.BlogPostId == id
                                      select item).ToList();
                foreach(Comment item in commentList)
                {
                    CommentViewModel c = new CommentViewModel();
                    c.comment = item;
                    var author = (from user in _context.Users
                        where user.UserId == item.UserId
                        select user).FirstOrDefault();
                    c.authorName = author.FirstName + " " + author.LastName;
                    viewModel.comments.Add(c);
                }

                viewModel.user = (from user in _context.Users
                                  where user.UserId == blogPost.UserId
                                  select user).FirstOrDefault();
                return View(viewModel);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult DisplayFullBlogPost()
        {
            Comment comment = new Models.Comment();
            comment.BlogPostId = Convert.ToInt32(Request.Form["BlogPostId"]);
            comment.UserId = (int) HttpContext.Session.GetInt32("UserId");
            comment.Content = Request.Form["Content"];

            _context.Comments.Add(comment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult AddBlogPost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBlogPost(BlogPost post)
        {
            post.Posted = DateTime.Now;
            post.UserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            _context.BlogPosts.Add(post);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult EditBlogPost(int id)
        {
            var postToEdit = (from item in _context.BlogPosts
                              where item.BlogPostId == id
                              select item)
                              .FirstOrDefault();
            return View(postToEdit);
        }

        [HttpPost]
        public IActionResult EditBlogPost(BlogPost post)
        {
            var id = Convert.ToInt32(Request.Form["BlogPostId"]);

            var postToUpdate = (from item in _context.BlogPosts
                                where item.BlogPostId == post.BlogPostId
                                select item)
                                .FirstOrDefault();
            if (postToUpdate == null) return RedirectToAction("Index");
            postToUpdate.Title = post.Title;
            postToUpdate.Content = post.Content;

            _context.Entry(postToUpdate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
