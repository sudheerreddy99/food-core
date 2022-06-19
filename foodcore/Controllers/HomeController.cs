using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using foodcore.Models;
using System.Linq;

namespace foodcore.Controllers
{

    public class HomeController : Controller
    {
        myinter m;
        public HomeController(myinter i)
        {
            m = i;

        }
        FoodcoreContext dc = new FoodcoreContext();
        [ActionName("h")]
        public string helloindia()
        {
            return "hi";
        }

        [NonAction] // this method cannot be called
        public string welcome()
        {
            return "welcome called";
        }
        public string Index()
        {
            return "Hello world!!";
        }
        public ViewResult Home()
        {
            // logic for home page goes here
            ViewData["a"] = 100;
            ViewData["b"] = 200;
            ViewData["c"] = "hello students3";

            int[] data = { 10, 20, 30, 40 };
            ViewBag.k = data;
            // dynamic - no typecasting

            ViewBag.m = 50;
            ViewBag.n = 60;

            // object - can be used across multiple pages

            TempData["p"] = "hello world";

            TempData.Keep("p");

            return View();

        }

        // i will not write html logic here, instead its there in view page
        [HttpGet]
        public ViewResult Login()
        {// logic for login page goes here

            return View();

        }
        [HttpPost]
        public ActionResult Login(IFormCollection frm)
        {// logic for login page goes here

            string uname = frm["uname"];
            string pwd = frm["pwd"];


            HttpContext.Session.SetString("uid", uname);

            //var res = (from t in dc.Registers
            //           where t.Username == uname && t.Pwd == pwd
            //           select t).Count();


            var c = dc.Registers.ToList().Where(c => c.Username == uname && c.Pwd == pwd).Count();

            if (c > 0)
            {
                return RedirectToAction("Menu");

                // valid
            }
            else
            {

                ViewData["v"] = "Invalid username or password";
                // not valid
                return View();
            }



        }
        [HttpGet]
        public ViewResult Register()
        {
            // logic for register page goes here
            return View();

        }
        [HttpPost]
        public ViewResult Register(Register r)
        {



            //Register r = new Register();
            //r.Username = f["t1"];
            //r.Pwd = f["t2"];
            //r.Dob =  Convert.ToDateTime(f["t3"]);
            //r.Email = f["t4"];
            //r.Gender = Convert.ToBoolean(f["t5"]);
            //r.Phone = f["t6"];
            //r.Nationality = f["t7"];

            dc.Registers.Add(r);
            int i = dc.SaveChanges();
            if (ModelState.IsValid)
            {
                if (i > 0)
            {
                ViewData["a"] = "New User created successfully";
            }
            else
            {
                ViewData["a"] = "Error occured try after some time";
            }

            return View();
            }
    
            else
            {
                return View();
               }
        }

        public ViewResult Contact()
        {// logic for contact page goes here

            return View();

        }
        public ViewResult Logout()
        {// logic for login page goes here

            
            return View();

        }
        


        public ViewResult Menu()
        {// logic for contact page goes here
            //var result = from t in dc.Menus
            //             select t;

            var result = m.DisplayFoodItem();
            return View(result);


            

        }
        [HttpGet]
        public ViewResult Myorders(string myitemid)
        {
            // is it 1 or many

            var result = dc.Menus.ToList().Find(c => c.Itemid == myitemid);

            TempData["p"] = result.Price;
            TempData["i"] = result.Itemid;

            TempData.Keep();


            return View(result);


        }

        [HttpPost]
        public ActionResult Myorders(IFormCollection c)
        {

            // insert new value to myorders table

            if (HttpContext.Session.GetString("uid") == null)
            {
                return RedirectToAction("Login");
            }

            else
            {


                Order o = new Order();
                o.Username = HttpContext.Session.GetString("uid");
                o.Itemid = TempData["i"].ToString();
                o.Price = Convert.ToInt32(TempData["p"]);
                o.Qty = Convert.ToInt32(c["t1"]);

                dc.Orders.Add(o);
                int i = dc.SaveChanges();

                if (i > 0)
                {
                    ViewData["a"] = "Your order placed successfully";
                }
                else
                {
                    ViewData["a"] = "Error occured try after some time";
                }

                return View();

            }


        }
        public ViewResult Mypage()
        {// logic for contact page goes here

            return View();

        }
        
        [HttpGet]
        public ActionResult Addcart(string myitemid, string myitemname)
        {
            //  var result = dc.Menus.ToList().Find(c => c.Itemid == myitemid);

            //  li.Add(result);
            if (HttpContext.Session.GetString("uid") == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                Mycart m = new Mycart();
                m.Username = HttpContext.Session.GetString("uid");
                m.Itemid = myitemid;
                m.Itemname = myitemname;
                dc.Mycarts.Add(m);

                int i = dc.SaveChanges();
                if (i > 0)
                {
                    ViewData["a"] = myitemid + "  Item Add successfully to cart";
                }
                else
                {
                    ViewData["a"] = myitemid + "failed to add try again";
                }

                //TempData.Add(myitemid, myitemid);

                var res = dc.Menus.ToList().Join(dc.Mycarts.ToList(), c => c.Itemid, w => w.Itemid, (c, w) => new { c.Itemid, c.Itemname, c.Price, c.Images, c.Itemdesc });

                int? sum = 0;

                foreach (var item in res)
                {
                    sum = sum + item.Price;


                }

                ViewData["total"] = sum;


                return View(res);
            }


        }

    }
}
