using System.Linq;
using BlogEngine.Core.Models;
using BlogEngine.Core.ViewModels;
using System;
using System.Collections.Generic;
using WebMatrix.WebData;
using System.Web.Security;

namespace BlogEngine.Web.Helpers
{
    public class ModelBinder
    {
        public static CategoryViewModel Category(Category category)
        {
            var model = new CategoryViewModel
            {
                CategoryId = category.CategoryId,
                CreatedDate = category.CreatedDate,
                Description = category.Description,
                Name = category.Name
            };

            return model;
        }

        public static List<CategoryViewModel> Categories(List<Category> categories)
        {
            List<CategoryViewModel> cvm = new List<CategoryViewModel>();

            foreach (var cat in categories)
            {
                var model = new CategoryViewModel
                {
                    CategoryId = cat.CategoryId,
                    CreatedDate = cat.CreatedDate,
                    Description = cat.Description,
                    Name = cat.Name
                };
                cvm.Add(model);
            }
            return cvm;
        }

        public static BlogViewModel Blog(BlogEntry blogEntry)
        {
            var blogViewModel = new BlogViewModel
                {
                    SelectedCategory = blogEntry.CategoryId.ToString(),
                    BlogShortDescription = blogEntry.BlogShortDescription,
                    BlogEntryText = blogEntry.BlogEntryText,
                    BlogTitle = blogEntry.BlogTitle,
                    DateCreated = blogEntry.DateCreated,
                    BlogId = blogEntry.BlogEntryId
                };

            return blogViewModel;
        }

        public static BlogEntry BlogCreate(BlogViewModel model)
        {
            var blogEntry = new BlogEntry
                {
                    UserId = model.UserId,
                    BlogShortDescription = model.BlogShortDescription,
                    BlogEntryText = model.BlogEntryText,
                    BlogTitle = model.BlogTitle,
                    DateCreated = DateTime.Now,
                    CategoryId = Int32.Parse(model.SelectedCategory)
                };

            if (model.BlogImage != null)
            {
                blogEntry.Image = new BlogImage { ImagePath = "~/Images/" + model.BlogImage.FileName };
            }
            else
            {
                blogEntry.Image = new BlogImage();
            }

            blogEntry.Tags = new List<Tag>();
            //todo add tags to blog here
            foreach (var tag in model.Tags)
            {
                if (tag.Checked)
                {
                    blogEntry.Tags.Add(new Tag() { TagId = tag.Id});
                }
            }

            return blogEntry;
        }

        public static ICollection<BlogSummaryView> BlogSummary(ICollection<BlogEntry> blogs)
        {
            List<BlogSummaryView> blogSummary = new List<BlogSummaryView>();
            foreach (var blog in blogs)
            {
                blogSummary.Add(new BlogSummaryView { BlogId = blog.BlogEntryId, BlogTitle = blog.BlogTitle, Comments = blog.Comments.Count });
            }

            return blogSummary;
        }

        public static IEnumerable<BlogEntryView> BlogEntryView(List<BlogEntry> blogs)
        {
            return blogs.Select(blog => new BlogEntryView
                {
                    BlogEntryId = blog.BlogEntryId, 
                    BlogEntryText = blog.BlogEntryText, 
                    BlogShortDescription = blog.BlogShortDescription, 
                    BlogTitle = blog.BlogTitle, 
                    Category = blog.Category, 
                    CategoryId = blog.CategoryId, 
                    Comments = blog.Comments, 
                    DateCreated = blog.DateCreated.ToShortDateString(), 
                    Image = blog.Image, 
                    User = blog.User, 
                    UserId = blog.UserId, 
                    Tags = blog.Tags
                }).ToList();
        }

        public static UserEditViewModel User(User user)
        {
            var model = new UserEditViewModel
            {
                Email = user.Email,
                Roles = RolesCheckBoxCreator(user.UserName), 
                FirstName = user.FirstName, 
                LastName = user.LastName, 
                Username = user.UserName
            };

            return model;
        }

        public static List<UsersViewModel> Users(ICollection<User> users)
        {
            var model = new List<UsersViewModel>();

            foreach (var user in users)
            {
                model.Add(new UsersViewModel
                {
                    Name = user.FirstName + " " + user.LastName, 
                    EmailAddress = user.Email,
                    UserId = user.UserId, 
                    Roles = RolesConcat(user.UserName),
                    Username = user.UserName
                });
            }

            return model;
        }

        // helper method to return a concatonated string of the list of the roles for the given user
        // used by grid.mvc to display the users roles in a string format
        private static string RolesConcat(string username)
        {
            var roles = Roles.GetRolesForUser(username).ToList();
            string roleString = "";

            foreach (var role in roles)
            {
                roleString = roleString + role + ", ";
            }

            // if there are roles, prevents exception when trying to create a substring if roleString.Length = 0
            if (roles.Count > 0)
            {
                // remove the last 2 characters of the string (this will be the ", " added to the last role)
                roleString = roleString.Substring(0, roleString.Length - 2);
            }

            return roleString;
        }

        private static List<RoleCheckBox> RolesCheckBoxCreator(string username)
        {
            var roleList = Roles.GetAllRoles();
            var rolecheckboxList = new List<RoleCheckBox>();

            foreach (var role in roleList)
            {
                rolecheckboxList.Add(new RoleCheckBox
                    {
                        IsChecked = Roles.IsUserInRole(username, role),
                        Name = role
                    });
            }

            return rolecheckboxList;
        }
    }
}