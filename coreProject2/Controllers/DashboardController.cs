﻿using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreProject2.Controllers
{
    public class DashboardController : Controller
    {
        BlogManager _blogManager = new BlogManager(new EfBlogRepository());
        CategoryManager _categoryManager = new CategoryManager(new EfCategoryRepository());
        //[AllowAnonymous]
        public IActionResult Index()
        {
            Context context = new Context();
            var userName = User.Identity.Name;
            var userMail = context.Users.Where(x => x.UserName == userName).Select(x => x.Email).FirstOrDefault();
            var writerId = context.Writers.Where(x => x.WriterMail == userMail).Select(y => y.WriterId).FirstOrDefault();
            ViewBag.ToplamBlog = _blogManager.GetList().Count();
            ViewBag.ToplamKategoriSayisi = _categoryManager.GetList().Count();
            ViewBag.SizinBlogSayiniz = context.Blogs.Where(x => x.WriterId == writerId).Count();
            ViewBag.UcGunBlog = _blogManager.GetList().Where(x => x.CreateDate >= DateTime.Now.Date.AddDays(-3) && x.CreateDate <= DateTime.Now.Date).Count();
            var value = _blogManager.BlogListele(0).OrderByDescending(x => x.CreateDate).Take(5).ToList();






            //ViewBag.score = _blogManager.GetList().Where(x => x.CreateDate >= DateTime.Now.Date.AddDays(-3) && x.CreateDate <= DateTime.Now.Date).Select(x => x.BlogScore).Average();
            //var score2 = (from a in context.Blogs
            //              where
            //              a.CreateDate >= DateTime.Now.Date.AddDays(-3) &&
            //              a.CreateDate <= DateTime.Now.Date
            //              select a.BlogScore).Average();
            //ViewBag.score22 = score2;
            return View(value);
        }
    }
}
