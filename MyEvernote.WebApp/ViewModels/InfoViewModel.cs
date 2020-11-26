using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEvernote.WebApp.ViewModels
{
    public class InfoViewModel:NotifyViewModelBase<string>
    {
        public InfoViewModel()
        {
            Title = "Bilgilendirme";
        }

        public static implicit operator InfoViewModel(OkViewModel v)
        {
            throw new NotImplementedException();
        }
    }
}