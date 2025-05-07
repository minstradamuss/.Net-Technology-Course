using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBook.Entities
{
    public class BookWithReview
    {
        public Book Book { get; set; }
        public string ReviewerNickname { get; set; }
        public string Review { get; set; }
    }

}
