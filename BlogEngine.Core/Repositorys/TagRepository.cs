using BlogEngine.Core.Contexts;
using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using BlogEngine.Core.ViewModels;
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

        public IEnumerable<Tag> GetTagsForBlog(int blogId)
        {
            var tags = from t in _context.Tags
                       from b in t.BlogEntries
                       where b.BlogEntryId == blogId
                       select t;
            
            return tags;
        }

        public ICollection<Tag> GetAllTags()
        {
            var tags = _context.Tags;

            return tags.ToList();
        }

        public bool Create(Tag tag)
        {
            // if tag is valid
            _context.Tags.Add(tag);

            return true;
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
            var tag = _context.Tags.Find(id);

            return tag;
        }


        public List<TagCheckViewModel> GetAllTagsForVM()
        {
            var tags = _context
                .Tags
                .Select(s => new TagCheckViewModel 
                    { 
                        Id = s.TagId, 
                        Checked = false, 
                        Name = s.Name
                    });

            return tags.ToList();
        }
    }
}
