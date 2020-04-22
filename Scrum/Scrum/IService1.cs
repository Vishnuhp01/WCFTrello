using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Scrum
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        User login(string username, string password);

        [OperationContract]
        bool register(string username, string password, string email, string contact);

        [OperationContract]
        bool addBoard(int user_id, string boardname);

        [OperationContract]
        bool addList(int user_id, string listname, int board_id);

        [OperationContract]
        bool addCard(int user_id, string cardname, int list_id, string c_info);

        [OperationContract]
        List<Board> fetchBoard(int user_id);


        [OperationContract]
        List<Lists> fetchList(int board_id);

        [OperationContract]
        List<Card> fetchCard(int list_id);

        [OperationContract]
        Dictionary<int, string> fetchUsers(int user_id);


        [OperationContract]
        bool updateCard(int card_id, string cardname, string c_info);

        [OperationContract]
        bool delCard(int card_id);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "Scrum.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    [DataContract]
    public class User
    {
        private int user_id;
        private string username;
        private string password;
        private string create_date;
        private string email;
        private string contact;

        [DataMember]
        public int Id
        {
            get { return user_id; }
            set { user_id = value; }
        }

        [DataMember]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [DataMember]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        [DataMember]
        public string Date
        {
            get { return create_date; }
            set { create_date = value; }
        }

        [DataMember]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [DataMember]
        public string Contact
        {
            get { return contact; }
            set { contact = value; }
        }
    }

    [DataContract]
    public class Board
    {
        private int board_id;
        private string boardname;
        private string create_date;
        private int created_by;

        [DataMember]
        public int Id
        {
            get { return board_id; }
            set { board_id = value; }
        }

        [DataMember]
        public string Boardname
        {
            get { return boardname; }
            set { boardname = value; }
        }
        

        [DataMember]
        public string Date
        {
            get { return create_date; }
            set { create_date = value; }
        }

        [DataMember]
        public int Creater
        {
            get { return created_by; }
            set { created_by = value; }
        }

    }

    [DataContract]
    public class Lists
    {
        private int list_id;
        private string listname;
        private string create_date;
        private int created_by;

        [DataMember]
        public int Id
        {
            get { return list_id; }
            set { list_id = value; }
        }

        [DataMember]
        public string Listname
        {
            get { return listname; }
            set { listname = value; }
        }


        [DataMember]
        public string Date
        {
            get { return create_date; }
            set { create_date = value; }
        }

        [DataMember]
        public int Creater
        {
            get { return created_by; }
            set { created_by = value; }
        }

    }

    [DataContract]
    public class Card
    {
        private int card_id;
        private string cardname;
        private string c_info;
        private string create_date;
        private int created_by;

        [DataMember]
        public int Id
        {
            get { return card_id; }
            set { card_id = value; }
        }

        [DataMember]
        public string Cardname
        {
            get { return cardname; }
            set { cardname = value; }
        }

        [DataMember]
        public string Cinfo
        {
            get { return c_info; }
            set { c_info = value; }
        }

        [DataMember]
        public string Date
        {
            get { return create_date; }
            set { create_date = value; }
        }

        [DataMember]
        public int Creater
        {
            get { return created_by; }
            set { created_by = value; }
        }

    }
}
