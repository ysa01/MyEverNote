﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace MyEvernote.Entities.ValueObjects
{
    public class RegisterViewModel
    {
        [DisplayName("Kullanıcı Adı"),
            Required(ErrorMessage = "{0} alanı boş geçilemez!!"), 
            StringLength(20, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!!")]
        public string Username { get; set; }
        [DisplayName("E-Posta"),
            Required(ErrorMessage = "{0} alanı boş geçilemez!!"),
            StringLength(70, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!!"), 
            EmailAddress(ErrorMessage = "Lütfen {0} alanı için geçerli bir e-posta adresi giriniz!!")]
        public string EMail { get; set; }
        [DisplayName("Şifre"),
            Required(ErrorMessage = "{0} alanı boş geçilemez!!"), 
            DataType(DataType.Password), 
            StringLength(20, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!!")]
        public string Password { get; set; }
        [DisplayName("Şifre Tekrar"), 
            Required(ErrorMessage = "{0} alanı boş geçilemez!!"), 
            DataType(DataType.Password), 
            StringLength(20, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!!"),
            Compare("Password",ErrorMessage ="{0} ile {1} uyuşmuyor!!")]
        public string RePassword { get; set; }
    }
}