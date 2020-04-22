using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrelloClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            ViewBag.Message = "Your login page.";

            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            TrelloService.User CurrentUser = AuthUser(fc["uname"].ToString(), fc["pass"].ToString());
            if (CurrentUser.Id != 0)
            {
                ViewBag.CurrentUserName = CurrentUser.Username;
                ViewBag.IsLoggedIn = true;
                Session["CurrentUsername"] = CurrentUser.Username;
                Session["CurrentPassword"] = CurrentUser.Password;
                Session["CurrentUserId"] = CurrentUser.Id;
                return RedirectToAction("BoardsDisplay");
            }
            else
            {
                ViewBag.CurrentUserName = "No Name";
                ViewBag.IsLoggedIn = false;
                return View("Login");
            }



            
        }

        [NonAction]
        public TrelloService.User AuthUser(string Username, string Password)
        {
            TrelloService.Service1Client client = new TrelloService.Service1Client();
            return client.login(Username, Password);

        }
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection fc)
        {
            TrelloService.Service1Client client = new TrelloService.Service1Client();

            if (client.register(fc["uname"].ToString(), fc["pass"].ToString(), fc["email"].ToString(), fc["contact"].ToString()) == false)
            {
                ViewBag.IsRegistered = false;
                ViewBag.RegisteredUsername = "no name";
            }
            else
            {
                ViewBag.IsRegistered = true;
                ViewBag.RegisteredUsername = fc["uname"].ToString();
            }

            return View("Register");
        }
        public ActionResult BoardsDisplay()
        {
            if (Session["CurrentUserId"] == null)
            {
                return View("Login");
            }
            TrelloService.Service1Client client = new TrelloService.Service1Client();
            List<TrelloService.Board> boards = client.fetchBoard(int.Parse(Session["CurrentUserId"].ToString())).ToList<TrelloService.Board>();
            List<Models.Board> UserBoards = new List<Models.Board>();
            foreach (var item in boards)
            {
                Models.Board temp = new Models.Board();
                temp.Id = item.Id;
                temp.Boardname = item.Boardname;
                temp.Creater = item.Creater;
                temp.Date = item.Date;
                UserBoards.Add(temp);
            }
            ViewBag.UserBoards = UserBoards;
            return View(UserBoards);
        }
        public ActionResult ShowBoard(int Id)
        {
            // show board contains lists 
            // lists contain cards so for each listshow cards
            //TrelloService.
            int BoardId = Id;
            Session["CurrentBoardId"] = BoardId;
            
            //fetch all list of this boards
            TrelloService.Service1Client client = new TrelloService.Service1Client();
            Session["CurrentBoardName"] = client.fetchBoard(int.Parse(Session["CurrentUserId"].ToString())).ToList<TrelloService.Board>().Where(x => x.Id == int.Parse(Session["CurrentBoardId"].ToString())).First().Boardname;
            List <TrelloService.Lists> Lists = client.fetchList(BoardId).ToList<TrelloService.Lists>();
            List<Models.Lists> BoardLists = new List<Models.Lists>();
            foreach (var i in Lists)
            {
                Models.Lists temp = new Models.Lists();
                temp.Id = i.Id;
                temp.Creater = i.Creater;
                temp.Date = i.Date;
                temp.Listname = i.Listname;
                BoardLists.Add(temp);
            }
            ViewBag.Lists = BoardLists;


            return View();
        }
        public ActionResult ShowCards(int Id)
        {
            // show board contains lists 
            // lists contain cards so for each listshow cards
            //TrelloService.
            int ListId = Id;
            Session["ListId"] = ListId;
            Session["CurrentListId"] = ListId;
            
            //fetch all list of this boards
            TrelloService.Service1Client client = new TrelloService.Service1Client();
            Session["CurrentListName"] = client.fetchList(int.Parse(Session["CurrentBoardId"].ToString())).ToList<TrelloService.Lists>().Where(x => x.Id == int.Parse(Session["CurrentListId"].ToString())).First().Listname;
            List<TrelloService.Card> Cards = client.fetchCard(ListId).ToList<TrelloService.Card>();
            List<Models.Card> ListCards = new List<Models.Card>();
            foreach (var i in Cards)
            {
                Models.Card temp = new Models.Card();
                temp.Id = i.Id;
                temp.Creater = i.Creater;
                temp.Date = i.Date;
                temp.Cardname = i.Cardname;
                temp.Cinfo = i.Cinfo;
                ListCards.Add(temp);
            }
            ViewBag.Cards = ListCards;
            return View();
        }
        public ActionResult CardDetails(int Id)
        {
            int CardId = Id;
            TrelloService.Service1Client client = new TrelloService.Service1Client();
            TrelloService.Card c = client.fetchCard(int.Parse(Session["ListId"].ToString())).ToList<TrelloService.Card>().Where<TrelloService.Card>(x => x.Id == Id).First<TrelloService.Card>();
            Models.Card card = new Models.Card();
            card.Cardname = c.Cardname;
            card.Cinfo = c.Cinfo;
            card.Creater = c.Creater;
            card.Date = c.Date;
            card.Id = c.Id;
            Session["card"] = card;
            Session["cardId"] = Id;
            Session["CurrentCardId"] = CardId;
            return View("CardEditableDetails",card);
            
        }
        [HttpPost]
        public ActionResult EditCard(FormCollection fc)
        {
            int cardId = int.Parse(fc["Id"].ToString());
            TrelloService.Service1Client client = new TrelloService.Service1Client();
            TrelloService.Card editedCard = new TrelloService.Card();
            editedCard.Id = int.Parse(fc["Id"].ToString());
            editedCard.Cardname = fc["Cardname"].ToString();
            editedCard.Cinfo = fc["Cinfo"].ToString();
            editedCard.Creater = int.Parse(fc["Creater"].ToString());
            editedCard.Date = fc["Date"].ToString();

            if(client.updateCard(editedCard.Id, editedCard.Cardname, editedCard.Cinfo))
            {
                TrelloService.Card c = client.fetchCard(int.Parse(Session["ListId"].ToString())).ToList<TrelloService.Card>().Where<TrelloService.Card>(x => x.Id == editedCard.Id).First<TrelloService.Card>();
                Models.Card card = new Models.Card();
                card.Cardname = c.Cardname;
                card.Cinfo = c.Cinfo;
                card.Creater = c.Creater;
                card.Date = c.Date;
                card.Id = c.Id;
                Session["card"] = card;
                Session["cardId"] = editedCard.Id;
                return View("CardEditableDetails", card);
            }
            else
            {
                return View("CardEditableDetails", (Models.Card)Session["card"]);
            }


            
        }
        public ActionResult AddNewCard()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddNewCard(FormCollection fc)
        {
            Models.Card c = new Models.Card();
            c.Cardname = fc["Cardname"].ToString();
            c.Cinfo = fc["Cinfo"].ToString();
            TrelloService.Service1Client client = new TrelloService.Service1Client();
            client.addCard(int.Parse(Session["CurrentUserId"].ToString()), c.Cardname, int.Parse(Session["CurrentListId"].ToString()), c.Cinfo);


            int ListId = int.Parse(Session["CurrentListId"].ToString());
            Session["ListId"] = ListId;
            Session["CurrentListId"] = ListId;
            //fetch all list of this boards
            List<TrelloService.Card> Cards = client.fetchCard(ListId).ToList<TrelloService.Card>();
            List<Models.Card> ListCards = new List<Models.Card>();
            foreach (var i in Cards)
            {
                Models.Card temp = new Models.Card();
                temp.Id = i.Id;
                temp.Creater = i.Creater;
                temp.Date = i.Date;
                temp.Cardname = i.Cardname;
                temp.Cinfo = i.Cinfo;
                ListCards.Add(temp);
            }
            ViewBag.Cards = ListCards;

            return View("ShowCards");
        }
        public ActionResult DeleteCard()
        {
            TrelloService.Service1Client client = new TrelloService.Service1Client();
            client.delCard(int.Parse(Session["CurrentCardId"].ToString()));
            int ListId = int.Parse(Session["CurrentListId"].ToString());
            List<TrelloService.Card> Cards = client.fetchCard(ListId).ToList<TrelloService.Card>();
            List<Models.Card> ListCards = new List<Models.Card>();
            foreach (var i in Cards)
            {
                Models.Card temp = new Models.Card();
                temp.Id = i.Id;
                temp.Creater = i.Creater;
                temp.Date = i.Date;
                temp.Cardname = i.Cardname;
                temp.Cinfo = i.Cinfo;
                ListCards.Add(temp);
            }
            ViewBag.Cards = ListCards;

            return View("ShowCards");
        }
    }
        
}