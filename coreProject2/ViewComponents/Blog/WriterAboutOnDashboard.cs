﻿using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreProject2.ViewComponents.Blog
{
    public class WriterAboutOnDashboard:ViewComponent
    {
        //dashboard sayfasına yazar hakkında kısmı çekilecek
        WriterManager _writerManager = new WriterManager(new EfWriterRepository());
        public IViewComponentResult Invoke()
        {
            Context context = new Context();
            var writerMail = User.Identity.Name;
            var loginid = context.Writers.Where(x => x.WriterMail == writerMail).Select(y => y.WriterId).FirstOrDefault();




            var data = _writerManager.GetWriterById(loginid);
            return View(data);
        }
    }
}
