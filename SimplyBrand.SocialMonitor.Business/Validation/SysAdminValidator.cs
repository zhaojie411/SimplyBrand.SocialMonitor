using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RulesEngine;
using RulesEngine.Rules;
using SimplyBrand.SocialMonitor.DAL.Entities;
namespace SimplyBrand.SocialMonitor.Business.Validation
{
    public class SysAdminValidator
    {
        public bool ValidateSysAdminName(string sysAdminName)
        {
            if (string.IsNullOrEmpty(sysAdminName))
            {
                throw new Exception("用户名不能为空");
            }
            return true;
        }
        public bool ValidateSysAdminPwd(string sysAdminPwd)
        {
            if (string.IsNullOrEmpty(sysAdminPwd))
            {
                throw new Exception("密码不能为空");
            }
            return true;
        }


        public bool ValidateSysAdmin(SysAdmin entity, out string msg)
        {
            var engine = new Engine();
            engine.For<SysAdmin>()
                .Setup(m => m.SysAdminName)
                    .MustNotBeNullOrEmpty()
                .Setup(m => m.SysAdminPwd)
                    .MustNotBeNullOrEmpty();
            if (!engine.Validate(entity))
            {
                msg = "用户名或密码不能为空";
                return false;
            }
            engine.For<SysAdmin>()
                .Setup(m => m.SysAdminName.Length)
                      .MustBeLessThanOrEqualTo(50)
                      .WithMessage("用户名的长度不能超过50")
                .Setup(m => m.SysAdminPwd.Length)
                      .MustBeLessThanOrEqualTo(50)
                      .WithMessage("密码长度不能超过50");
            var report = new ValidationReport(engine);
            var result = report.Validate(entity);
            var errors = report.GetErrorMessages(entity);
            if (errors != null && errors.Count() > 0)
                msg = string.Join(",", errors);
            else
                msg = "success";
            return result;
        }
    }
}
