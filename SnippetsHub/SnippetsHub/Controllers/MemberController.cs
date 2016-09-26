using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SnippetsHub.Models;

namespace SnippetsHub.Controllers
{
    public class MemberController : Controller
    {
        
        DAO dao = new DAO();
        
        // GET: Member
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult RegistrationPage()
        {
            return View();
        }
       
        public ActionResult Snippet()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult CreateGroup()
        {
            return View();
        }

        public ActionResult Group()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Member member)
        {
            int count;
            if (ModelState.IsValid)
            {
                count = dao.Insert(member);
                if (count > 0)
                {
                    ViewData["message"] = "Registered Successfully";
                }
                else ViewData["message"] = "Error: " + dao.message;
                return View("Message");
            }

            else return View("RegistrationPage", member);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            Member member = new Member();
            if (ModelState.IsValid)
            {
                member.Email = login.Email;
                member.Password = login.Password;
                member.FirstName = dao.CheckLogin(member);

                Session.Add("memberName", member.FirstName);
                Session.Add("memberEmail", member.Email);

                if (Session["memberName"] == null)
                {
                    ViewData["message"] = "Error: " + dao.message;
                    return View("Message");
                }

                return View("Snippet");
             
            }
            else return View("Login");      
        }

        public ActionResult LogOff()
        {

            Session.Clear();
            return View("Login");
        }

  
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateSnippet(Snippet snippet)
        {
            int count;
            snippet.Email = Session["memberEmail"].ToString();
            //attempts to show snippets in the correct format 
            // Overwrite the newline characters with html equivalents, to preserve the snippet format
            //snippet.Content = snippet.Content.Replace("\r\n", "<br />");
            if (ModelState.IsValid)
            {               
                count = dao.InsertSnippet(snippet);
                if (count > 0)
                {
                    ViewData["message"] = "Snippet created successfully";
                }
                else ViewData["message"] = "Error: " + dao.message;
                return View();
            }

            return View("Index", snippet);
        }

        public ActionResult ShowSnippets()
        {           
            List<Snippet> list = dao.ShowAllSnippets(Session["memberEmail"].ToString());
            return View(list);
        }

        [HttpPost]
        public ActionResult NewGroup(Group group)
        {
                int count;
            if (ModelState.IsValid)
            {
                count = dao.InsertGroup(group);
                if (count > 0)
                {
                    ViewData["message"] = "Group created successfully";
                }
                else ViewData["message"] = "Error: " + dao.message;
                return View("Snippet");
            }

            else return View("Index", group);
        }

        public ActionResult MyGroups()
        {
            // just display view
            string email = Session["memberEmail"].ToString();
            List<Group> myGroups = dao.ShowMemberGroups(email);
            return View(myGroups);
        }

        [HttpPost]
        public ActionResult MyGroups(string groupName)
        {
            string email = Session["memberEmail"].ToString();
            if (groupName != null)
            {
                List<Group> grouplist = dao.FindGroups(groupName); 
                if(grouplist.Count > 0)
                {
                    dao.InsertintoGroupMembers(grouplist[0], email);
                }          
            }
            List<Group> myGroups = dao.ShowMemberGroups(email);
            return View(myGroups);
           
        }

        [HttpPost]
        public ActionResult GetGroupPage(string groupName)
        {
            Snippet snippet = new Snippet();
            snippet.GroupName = groupName;
            return View("GroupPage", snippet);
           
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GroupPage(Snippet snippet)
        {
            snippet.Email = Session["memberEmail"].ToString();
            dao.InsertSnippet(snippet);
            return View("SnippetAddedSuccessfully");     
        }

        [HttpPost]
        public ActionResult ShowGroupSnippets(string groupName)
        {
            List<Snippet> list = dao.ShowGroupSnippets(groupName);
            return View(list);
        }


    }
}