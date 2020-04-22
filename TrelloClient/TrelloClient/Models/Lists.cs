using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrelloClient.Models
{
    public class Lists
    {
        private int list_id;
        private string listname;
        private string create_date;
        private int created_by;

        
        public int Id
        {
            get { return list_id; }
            set { list_id = value; }
        }

        
        public string Listname
        {
            get { return listname; }
            set { listname = value; }
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