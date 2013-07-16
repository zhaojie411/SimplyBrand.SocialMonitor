using SimplyBrand.SocialMonitor.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Controller
{
   public class ContactUserController
    {    
       ContactUserBLL bll = new ContactUserBLL();
       public string Find(int sysuserid)
       {
           return bll.Find(sysuserid);
       }
       public string Update(int sysuserid,int linkId, string linkman, string linkTel, string linkEmail,int txtId, string linkmans, string linkTels, string linkEmails)
       {
           return bll.Update(sysuserid,linkId, linkman, linkTel, linkEmail,txtId,linkmans, linkTels, linkEmails);
       }

    }
}
