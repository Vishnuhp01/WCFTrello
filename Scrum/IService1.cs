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
        int login(string username, string password);

        [OperationContract]
        bool register(string username, string password, string email, string contact);

        [OperationContract]
        bool addBoard(int user_id, string boardname);

        [OperationContract]
        bool addList(int user_id, string listname, int board_id);

        [OperationContract]
        bool addCard(int user_id, string cardname, int list_id, string c_info);

        [OperationContract]
        List<int> fetchBoard(int user_id);


        [OperationContract]
        List<int> fetchList(int board_id);

        [OperationContract]
        List<int> fetchCard(int list_id);

        [OperationContract]
        Dictionary<int, string> fetchUsers(int user_id);


        [OperationContract]
        bool updateCard(int card_id, string cardname, string c_info);

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
}
