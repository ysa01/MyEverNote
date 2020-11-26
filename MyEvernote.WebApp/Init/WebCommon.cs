﻿using MyEvernote.Entities;
using MyEvernoteCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEvernote.WebApp.Init
{
    public class WebCommon : ICommon
    {
        public string GetCurrentUsername()
        {
           if(HttpContext.Current.Session["login"]!=null)
            {
                EvernoteUser user = HttpContext.Current.Session["login"] as EvernoteUser;
                return user.UserName;
            }
            return "system";
        }
    }
}