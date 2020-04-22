using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrelloClient.Models
{
    public class Card
    {
        private int card_id;
        private string cardname;
        private string c_info;
        private string create_date;
        private int created_by;

        
        public int Id
        {
            get { return card_id; }
            set { card_id = value; }
        }

        
        public string Cardname
        {
            get { return cardname; }
            set { cardname = value; }
        }

        
        public string Cinfo
        {
            get { return c_info; }
            set { c_info = value; }
        }

        
        public string Date
        {
            get { return create_date; }
            set { create_date = value; }
        }

        
        public int Creater
        {
            get { return created_by; }
            set { created_by = value; }
        }

    }
}