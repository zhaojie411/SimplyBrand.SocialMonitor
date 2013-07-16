using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SimplyBrand.SocialMonitor.Business.Validation
{
    public class CommonValidator
    {
        RegexOptions regexoptions = RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled;
        public bool ValidateIntList(string idstring)
        {
            if (string.IsNullOrEmpty(idstring))
            {
                throw new InvalidIntValueException("参数不能为空");
            }
            List<string> ids = idstring.Split(',').ToList();
            Regex regex = new Regex(@"^\d+$", RegexOptions.Compiled);
            foreach (var item in ids)
            {
                if (!regex.IsMatch(item))
                {
                    throw new InvalidIntValueException("对应的参数不是int类型");
                }
            }
            return true;
        }



        public bool ValidateFilePath(string filepath)
        {
            if (string.IsNullOrEmpty(filepath))
                throw new InValidFilepathException("文件路径不能为空");

            if (!File.Exists(filepath))
            {
                throw new InValidFilepathException("该文件路径不存在：" + filepath);
            }

            return true;
        }
        public bool ValidatePhone(string Phone)
        {
            Match mt = Regex.Match(Phone, @"^(?:13\d|15\d|18\d)\d{5}(\d{3}|\*{3})$", regexoptions);
            if (mt.Success)
            {
                return true;
            }
            else
            {
                return false;
            }           
        }
        public bool ValidateEmail(string mail)
        {
            Match mt = Regex.Match(mail, @"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$", regexoptions);
            if (mt.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
