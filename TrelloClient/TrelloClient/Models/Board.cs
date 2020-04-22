using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrelloClient.Models
{
    public class Board
    {
        private int board_id;
        private string boardname;
        private string create_date;
        private int created_by;

        public int Id
        {
            get { return board_id; }
            set { board_id = value; }
        }

        
        public string Boardname
        {
            get { return boardname; }
            set { boardname = value; }
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