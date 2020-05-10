using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebAccount.Models;
using AppContext = WebAccount.Models.AppContext;

namespace WebAccount.Controllers
{
   
    public class AccountController : Controller
    {
        static List<AcademicPerformance> academicPerformances = new List<AcademicPerformance>();
        static List<Progress> progresses = new List<Progress>();
        static List<Teacher> teachers = new List<Teacher>();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;
                using (AppContext db = new AppContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, true);
                    Session["user"] = user.Name;
                    Session["id"] = user.Id;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (AppContext db = new AppContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Username == model.Username);
                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (AppContext db = new AppContext())
                    {
                        db.Users.Add(new User { Username = model.Username, Name = model.Name, Surname = model.Surname, Patr = model.Patr, Password = model.Password});
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Username == model.Username && u.Password == model.Password).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Username, true);
                        Session["user"] = user.Name;
                        Session["id"] = user.Id;
                        return RedirectToAction("Index", "Home");

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            Session["user"] = null;
            Session["id"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Cabinet()
        {
            AppContext db = new AppContext();
            int id = Convert.ToInt32(Session["id"]);
            var list = db.AcademicPerformances.Where(x => x.Student_Id == id).ToList();

            foreach(var el in list)
            {
                academicPerformances.Add(new AcademicPerformance { Id = el.Id, Student_Id = el.Student_Id, Semester = el.Semester, Name_Sub = el.Name_Sub, Mark = el.Mark});
            }

            return View();
        }
        public string GetData()
        {
            return JsonConvert.SerializeObject(academicPerformances);
        }


        public ActionResult ProgressCabinet()
        {
            AppContext db = new AppContext();
            int id = Convert.ToInt32(Session["id"]);
            var list = db.Progresses.Where(x => x.Student_Id == id).ToList();

            foreach (var el in list)
            {
                progresses.Add(new Progress { Id = el.Id, Student_Id = el.Student_Id, Activity = el.Activity, Descrption = el.Descrption });
            }


            return View();
        }

        public string GetDataProgress()
        {
            return JsonConvert.SerializeObject(progresses);
        }

        public ActionResult TeacherCabinet()
        {
            AppContext db = new AppContext();
            var list = db.Teachers.ToList();

            foreach (var el in list)
            {
                teachers.Add(new Teacher { Id = el.Id, Name = el.Name, Surame = el.Surame, Patr = el.Patr, Name_Sub = el.Name_Sub, Name_Dep = el.Name_Dep });
            }

            return View();
        }

        public string GetDataTeachers()
        {
            return JsonConvert.SerializeObject(teachers);
        }
    } 
}