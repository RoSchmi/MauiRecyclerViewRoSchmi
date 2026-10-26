using System;
using System.Collections.Generic;
using System.Text;

namespace MauiRecyclerViewRoSchmi.Models
{
    public class ProductResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; } // название чата
        public double Price { get; set; }
        public string ImageUrl { get; set; } // аватар
        public string Description { get; set; } // последнее сообщение, макс. линий - 1 линия
        public DateTime PublishedDate { get; set; } // дата последнего сообщения
        public bool HasUnreadMessages { get; set; } // есть или нет непрочитанные сообщения
        public int UnreadMessagesCount { get; set; } // количество непрочитанных сообщений

    }
}
