using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyEvernote.Entities.ValueObjects
{
    public class LoginViewModel
    {
        [DisplayName("Kullanıcı Adı"),Required(ErrorMessage ="{0} alanı boş geçilemez!!"),StringLength(20, ErrorMessage = "{0} alanı max. {1}karakter olmalıdır!!")]
        public string username { get; set; }
        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez!!"),DataType(DataType.Password),StringLength(20, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır!!")]
        public string password { get; set; }

    }
}