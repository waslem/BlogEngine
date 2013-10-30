using BlogEngine.Core.Contexts;
using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using BlogEngine.Core.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Repositorys
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogContext _context;

        public TagRepository(BlogContext context)
        {
            _context = context;
        }

        public ICollection<Tag> GetTagsForBlog(int blogId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Tag> GetAllTags()
        {
            throw new NotImplementedException();
        }

        public bool Create(Tag tag)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int tagId)
        {
            throw new NotImplementedException();
        }

        public bool Edit(Tag tag)
        {
            throw new NotImplementedException();
        }

        public Tag GetTagById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
