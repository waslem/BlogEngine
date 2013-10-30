using BlogEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Infrastructure
{
    public interface ITagRepository
    {
        ICollection<Tag> GetTagsForBlog(int blogId);
        ICollection<Tag> GetAllTags();

        bool Create(Tag tag);
        bool Delete(int tagId);
        bool Edit(Tag tag);
        Tag GetTagById(int id);
    }
}
