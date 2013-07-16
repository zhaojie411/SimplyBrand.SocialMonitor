using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RulesEngine;
using SimplyBrand.SocialMonitor.DAL.Entities;

namespace SimplyBrand.SocialMonitor.Business.Validation
{
    public class SysUserValidator
    {
        public bool ValidationLogin(SysUser entity, out string msg)
        {
            var engine = new Engine();
            msg = "";
            engine.For<SysUser>()
                .Setup(m => m.SysUserName)
                    .MustNotBeNullOrEmpty()
                .Setup(m => m.SysUserPwd)
                    .MustNotBeNullOrEmpty();
            if (!engine.Validate(entity))
            {
                msg = "用户名或密码不能为空";
                return false;
            }
            return true;
        }
        public bool ValidationSysUser(SysUser entity, out string msg)
        {
            var engine = new Engine();
            engine.For<SysUser>()
                .Setup(m => m.SysUserName)
                    .MustNotBeNullOrEmpty()
                .Setup(m => m.SysUserPwd)
                    .MustNotBeNullOrEmpty()
                .Setup(m => m.SysUserRealName)
                    .MustNotBeNullOrEmpty();
            if (!engine.Validate(entity))
            {
                msg = "用户名或密码不能为空";
                return false;
            }
            engine.For<SysUser>()
                .Setup(m => m.SysUserName.Length)
                    .MustBeLessThanOrEqualTo(50)
                    .WithMessage("用户名长度不能超过50")
                .Setup(m => m.SysUserPwd.Length)
                    .MustBeLessThanOrEqualTo(50)
                    .WithMessage("密码长度不能超过50")
                //.Setup(m => m.SysUserStatus)
                //    .MustBeGreaterThan((short)0)
                //    .WithMessage("用户状态必须大于0")
                .Setup(m => m.SysUserRealName.Length)
                    .MustBeLessThanOrEqualTo(50)
                    .WithMessage("用户真实姓名长度不能超过50")
                .If(m => m.SysUserLogo != null)
                    .Setup(m => m.SysUserLogo.Length)
                        .MustBeLessThanOrEqualTo(255)
                        .WithMessage("logo长度不能超过255")
                .EndIf();
            var report = new ValidationReport(engine);
            var result = report.Validate(entity);
            var errors = report.GetErrorMessages(entity);
            if (errors != null && errors.Count() > 0)
                msg = string.Join(",", errors);
            else
                msg = "success";
            return result;
        }

        public bool ValidationSysUserPwd(string sysuserpwd, out string msg)
        {
            if (sysuserpwd.Length > 50)
            {
                msg = "密码长度不能超过50";
                return false;
            }
            else
            {
                msg = "success";
                return true;
            }
        }

    }
}
