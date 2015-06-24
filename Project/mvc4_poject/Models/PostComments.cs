using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc4_poject.Models
{
    public class PostComments
    {
        public  Post post;
        public IQueryable<Comment> comments;
        public int numberOfCom;
        public Comment newComment;
    }
}