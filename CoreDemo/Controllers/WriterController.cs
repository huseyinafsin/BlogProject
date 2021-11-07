﻿using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using CoreDemo.Models;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace CoreDemo.Controllers
{

    public class WriterController : Controller
    {
        readonly IWriterService _writerService;

        public WriterController(IWriterService writerService)
        {
            _writerService = writerService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WriterProfile()
        {
            return View();
        }

        public IActionResult WriterMail()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Test()
        {
            return View();
        }

        [AllowAnonymous]
        public PartialViewResult WriterNavbarPartial()
        {
            return PartialView();
        }

        [AllowAnonymous]
        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult WriterEditProfile()
        {
            var writervalues = _writerService.GetById(1);

            return View(writervalues);
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult WriterEditProfile(Writer p)
        {
            WriterValidator wl = new WriterValidator();

            ValidationResult results = wl.Validate(p);

            if (results.IsValid)
            {
                _writerService.TUpdate(p);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }

                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult WriterAdd()
        {
           

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult WriterAdd(AddProfileImage p)
        {
            Writer w = new Writer();

            if (p.WriterImage != null)
            {
                var extension = Path.GetExtension(p.WriterImage.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                p.WriterImage.CopyTo(stream);
                w.WriterImage = newimagename;
            }
            w.WriterMail = p.WriterMail;
            w.WriterName = p.WriterName;
            w.WriterPassword = p.WriterPassword;
            w.WriterStatus = true;
            w.WriterAbout = p.WriterAbout;
            _writerService.TAdd(w);
            return RedirectToAction("Index", "Dashboard");
        }

    }

}
