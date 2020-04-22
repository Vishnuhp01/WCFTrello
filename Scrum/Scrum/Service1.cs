using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Scrum
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        // Useless function
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        // This function will check for user which is already registered.
        // If username and password matches than return user_id for further use.
        // else return 0 because userid starts with 1.
        public User login(string username, string password)
        {
            User user = new User();
            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\0_Drive_E\SEM-6\SOC\Scrum\ScrumDB.mdf;Integrated Security=True;";
            string query = "select * from Users where username='"+username+ "' and password='"+ password+"' ;";
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user.Id = int.Parse(reader["user_id"].ToString());
                        user.Username = reader["username"].ToString();
                        user.Password = reader["password"].ToString();
                        user.Date = reader["create_date"].ToString();
                        user.Email = reader["email"].ToString();
                        user.Contact = reader["contact_no"].ToString();
                    }
                }
            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return user;
        }

        // This function will return true if user register successfully.
        // All the parameters are compulsory.
        // else return false.
        public bool register(string username, string password, string email, string contact)
        {
            if (username == null || password == null || email == null || contact == null)
                return false;
            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\0_Drive_E\SEM-6\SOC\Scrum\ScrumDB.mdf;Integrated Security=True;";
            int Id = 1;
           // Console.WriteLine("Heelo");
            string query1 = "select max(user_id) from Users;";
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd1 = new SqlCommand(query1, con);
            try
            {
                con.Open();
                Id = (int)cmd1.ExecuteScalar() + 1;
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(Id);
                Console.WriteLine(ex.Message);
            }
            string query = "insert into Users values('"+Id+"','"+username+"','"+password+ "','"+DateTime.Now.ToString()+"','" +email+ "','" +contact+"');";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
       // This will create new board when user add details.
       // This will create entry in both the table Boards and Board_User.
       public bool addBoard(int user_id, string boardname)
        {
            int board_id = 1;
            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\0_Drive_E\SEM-6\SOC\Scrum\ScrumDB.mdf;Integrated Security=True;";
            string query1 = "select COALESCE(max(board_id),0) from Boards;";
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd1 = new SqlCommand(query1, con);
            try
            {
                con.Open();
                board_id = (int)cmd1.ExecuteScalar() + 1;
                Console.WriteLine(board_id);
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(board_id);
                Console.WriteLine(ex.Message);
            }
            string query = "insert into Boards values('" +board_id  + "','" + boardname + "','" + DateTime.Now.ToString() + "','" + user_id + "');";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            string query2 = "insert into Board_User values('" + board_id + "','" + user_id + "');";
            SqlCommand cmd2 = new SqlCommand(query2, con);
            try
            {
                con.Open();
                int j = cmd2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        // Same as addBoard.
        public bool addList(int user_id, string listname, int board_id)
        {
            int list_id = 1;
            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\0_Drive_E\SEM-6\SOC\Scrum\ScrumDB.mdf;Integrated Security=True;";
            string query1 = "select COALESCE(max(list_id),0) from Lists;";
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd1 = new SqlCommand(query1, con);
            try
            {
                con.Open();
                list_id = (int)cmd1.ExecuteScalar() + 1;
                Console.WriteLine(list_id);
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(list_id);
                Console.WriteLine(ex.Message);
            }
            string query = "insert into Lists values('" + list_id + "','" + listname + "','" + DateTime.Now.ToString() + "','" + user_id + "');";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            string query2 = "insert into List_Board values('" + list_id + "','" + board_id + "');";
            SqlCommand cmd2 = new SqlCommand(query2, con);
            try
            {
                con.Open();
                int j = cmd2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        // Same as addBoard.
        public bool addCard(int user_id, string cardname, int list_id, string c_info)
        {
            int card_id = 1;
            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\0_Drive_E\SEM-6\SOC\Scrum\ScrumDB.mdf;Integrated Security=True;";
            string query1 = "select COALESCE(max(card_id),0) from Cards;";
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd1 = new SqlCommand(query1, con);
            try
            {
                con.Open();
                card_id = (int)cmd1.ExecuteScalar() + 1;
                Console.WriteLine(card_id);
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(card_id);
                Console.WriteLine(ex.Message);
            }
            string query = "insert into Cards values('" + card_id + "','" + cardname + "','"+ c_info + "','" + DateTime.Now.ToString() + "','" + user_id + "');";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            string query2 = "insert into Card_List values('" + card_id + "','" + list_id + "');";
            SqlCommand cmd2 = new SqlCommand(query2, con);
            try
            {
                con.Open();
                int j = cmd2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        // This will fetch all the board created by or it will added by its friend.
        // Returns list of board_id for the current user_id.
        public List<Board> fetchBoard(int user_id)
        {
            List<Board> boardlist = new List<Board>();
            List<int> temp_list = new List<int>();
            SqlDataReader temp;
            Board temp_board;
            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\0_Drive_E\SEM-6\SOC\Scrum\ScrumDB.mdf;Integrated Security=True;";
            string query1 = "select board_id from Board_User where user_id='"+user_id+"';";
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd1 = new SqlCommand(query1, con);

            try
            {
                con.Open();
                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        temp_list.Add(int.Parse(reader["board_id"].ToString()));
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            foreach (var id in temp_list)
            {
                string query = "select * from Boards where board_id='" + id + "';";
                SqlCommand cmd = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    using (temp = cmd.ExecuteReader())
                    {
                        while (temp.Read())
                        {
                            temp_board = new Board();
                            temp_board.Id = int.Parse(temp["board_id"].ToString());
                            temp_board.Boardname = temp["boardname"].ToString();
                            temp_board.Date = temp["create_date"].ToString();
                            temp_board.Creater = int.Parse(temp["created_by"].ToString());
                            boardlist.Add(temp_board);
                        }
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
             

            return boardlist;
        }


        // Same as fetchBoard.
        public List<Lists> fetchList(int board_id)
        {
            List<Lists> listlist = new List<Lists>();
            List<int> temp_list = new List<int>();
            SqlDataReader temp;
            Lists temp_lists;
            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\0_Drive_E\SEM-6\SOC\Scrum\ScrumDB.mdf;Integrated Security=True;";
            string query1 = "select list_id from List_Board where board_id='" + board_id + "';";
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd1 = new SqlCommand(query1, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        temp_list.Add(int.Parse(reader["list_id"].ToString()));
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            foreach (var id in temp_list)
            {
                string query = "select * from Lists where list_id='" + id + "';";
                SqlCommand cmd = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    using (temp = cmd.ExecuteReader())
                    {
                        while (temp.Read())
                        {
                            temp_lists = new Lists();
                            temp_lists.Id = int.Parse(temp["list_id"].ToString());
                            temp_lists.Listname = temp["listname"].ToString();
                            temp_lists.Date = temp["create_date"].ToString();
                            temp_lists.Creater = int.Parse(temp["created_by"].ToString());
                            listlist.Add(temp_lists);
                        }
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return listlist;
        }

        // Same as fetchBoard.
        public List<Card> fetchCard(int list_id)
        {
            List<Card> cardlist = new List<Card>();
            List<int> temp_list = new List<int>();
            SqlDataReader temp;
            Card temp_card;
            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\0_Drive_E\SEM-6\SOC\Scrum\ScrumDB.mdf;Integrated Security=True;";
            string query1 = "select card_id from Card_List where list_id='" + list_id + "';";
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd1 = new SqlCommand(query1, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       temp_list.Add(int.Parse(reader["card_id"].ToString()));
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            foreach (var id in temp_list)
            {
                string query = "select * from Cards where card_id='" + id + "';";
                SqlCommand cmd = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    using (temp = cmd.ExecuteReader())
                    {
                        while (temp.Read())
                        {
                            temp_card = new Card();
                            temp_card.Id = int.Parse(temp["card_id"].ToString());
                            temp_card.Cardname = temp["cardname"].ToString();
                            temp_card.Cinfo = temp["c_info"].ToString();
                            temp_card.Date = temp["create_date"].ToString();
                            temp_card.Creater = int.Parse(temp["created_by"].ToString());
                            cardlist.Add(temp_card);
                        }
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return cardlist;
        }

        // fetch all the users to make friends or team.
        // Return dictionary with user_id as key and username as value.
        // User himself do not appear in this list.
        public Dictionary<int,string> fetchUsers(int user_id)
        {
            Dictionary<int,string> userlist = new Dictionary<int,string>();
            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\0_Drive_E\SEM-6\SOC\Scrum\ScrumDB.mdf;Integrated Security=True;";
            string query1 = "select * from Users;";
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd1 = new SqlCommand(query1, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (int.Parse(reader["user_id"].ToString()) != user_id)
                            userlist.Add(int.Parse(reader["user_id"].ToString()),reader["username"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return userlist;
        }

        // This will delete card from board.
        // Deletes recoed from Cards and Card_List.
        public bool delCard(int card_id)
        {
            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\0_Drive_E\SEM-6\SOC\Scrum\ScrumDB.mdf;Integrated Security=True;";
            string query1 = "delete from Card_List where card_id='"+card_id+"';";
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd1 = new SqlCommand(query1, con);
            try
            {
                con.Open();
                int j = cmd1.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            string query = "delete from Cards where card_id='" + card_id + "';";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                int j = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        // This will update the card. 
        public bool updateCard(int card_id, string cardname, string c_info)
        {
            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\0_Drive_E\SEM-6\SOC\Scrum\ScrumDB.mdf;Integrated Security=True;";
            string query = "update Cards set cardname='" + cardname + "', c_info='"+c_info+"' where card_id='" + card_id + "';";
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
