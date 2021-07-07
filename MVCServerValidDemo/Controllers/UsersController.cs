using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCServerValidDemo.Models;
using Newtonsoft.Json;


namespace MVCServerValidDemo.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult customView()
        {
            return View();
        }

        public ActionResult showname()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        
        public ActionResult Register()
        {
            //这里展示一下自定义的验证错误信息
            ModelState.AddModelError("CustomErrorkey","这是自定义错误信息");
            //然后可以在对应的View中取到
            return View();
        }

        //查找名字
        public ActionResult FindName() {
            return View("FindName");
        }
        


        //用户注册动作
        public ActionResult UserRegister(Users user)
        {
            //在User模型中，设置了特性检查。如何知道是否验证通过呢？
            if (ModelState.IsValid) //验证通过
            {
                //访问数据库代码
                return View("Login");
            }
            else {
                return View("Register");
            };
        }

        public string ListToJSON() 
        {
            List<Users> list = new List<Users>()
            {
                new Users(){ Account="zhangsan",Password1="abc123",Email="abcdefg@qq.com"},
                new Users(){ Account="lisi",Password1="123456",Email="wangyi@163.com"},
            };
            string jsonResult = JsonConvert.SerializeObject(list);
            return jsonResult;
        }

        public JsonResult useJSONResult() {
            var jr = new JsonResult();
            jr.Data = ListToJSON();
            return jr;
        }






        public JsonResult GetUserNames(string keyword) 
        {
            var jr = new JsonResult();
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var list = QueryUserByName(keyword);
            if (list.Count > 0)
            {
                jr.Data = list;
            }
            else
            {
                jr.Data = "0";
            }
            return jr;
        }

        private List<Users> QueryUserByName(string keyword)
        {
            List<Users> list = new List<Users>();
            string sql = @"select stdId,name from student where name like @keyword";
            SqlParameter[] p = new SqlParameter[] { new SqlParameter("@keyword", keyword + "%") };
            //SqlParameter[] p = { };
            using (var reader = DBHelper.ExecuteReader(sql, p))
            {
                while (reader.Read())
                {
                    var user1 = new Users();
                    user1.UserId = (int)reader["stdId"];
                    user1.Account = reader["name"].ToString();
                    list.Add(user1);
                }     
            }
            return list;
        }
    }
}