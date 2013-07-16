using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyBrand.SocialMonitor.DAL.Entities;

namespace SimplyBrand.SocialMonitor.Business.JsonEntity
{
    public class JsonEntityUtility
    {
        public static BaseJson SetJsonEntity(BaseJson jsonEntity, EntityBase entityBase)
        {
            if (jsonEntity is RoleJson && entityBase is Role)
            {
                RoleJson roleJson = jsonEntity as RoleJson;
                Role role = entityBase as Role;
                roleJson.id = role.RoleId;
                roleJson.name = role.RoleName;
                roleJson.desc = role.RoleDescription;
                return roleJson;
            }
            if (jsonEntity is SysAdminJson && entityBase is SysAdmin)
            {
                SysAdminJson sysAdminJson = jsonEntity as SysAdminJson;
                SysAdmin sysAdmin = entityBase as SysAdmin;
                sysAdminJson.id = sysAdmin.SysAdminId;
                sysAdminJson.name = sysAdmin.SysAdminName;
                return sysAdminJson;
            }
            if (jsonEntity is SysUserJson && entityBase is SysUser)
            {
                SysUserJson sysUserJson = jsonEntity as SysUserJson;
                SysUser sysUser = entityBase as SysUser;
                sysUserJson.id = sysUser.SysUserId;
                sysUserJson.name = sysUser.SysUserName;
                sysUserJson.status = sysUser.SysUserStatus;
                sysUserJson.createdate = sysUser.SysUserAddDate.ToString("yyyy/MM/dd");
                sysUserJson.logo = sysUser.SysUserLogo;
                sysUserJson.password = sysUser.SysUserPwd;
                sysUserJson.enddate = sysUser.SysUserEndTime.ToString("yyyy/MM/dd");
                sysUserJson.realname = sysUser.SysUserRealName;
                return sysUserJson;
            }
            if (jsonEntity is PlatformJson && entityBase is Platform)
            {
                PlatformJson platformJson = new PlatformJson();
                Platform platform = entityBase as Platform;
                platformJson.id = platform.PlatformId;
                platformJson.name = platform.PlatformName;
                return platformJson;
            }
            if (jsonEntity is ContactUserJson && entityBase is ContactUser)
            {
                ContactUserJson contactuserJson = new ContactUserJson();
                ContactUser contactuser = entityBase as ContactUser;
                contactuserJson.ContactUserId = contactuser.ContactUserId;
                contactuserJson.ContactUserName = contactuser.ContactUserName;
                contactuserJson.ContactUserTel = contactuser.ContactUserTel;
                contactuserJson.ContactUserIsprimary = Convert.ToBoolean(contactuser.IsPrimary);
                contactuserJson.ContactUserEmail = contactuser.ContactUserEmail;
                contactuserJson.SysUserId = Convert.ToInt32(contactuser.SysUserId);
                return contactuserJson;
            }
            if (jsonEntity is KeywordFamilyJson && entityBase is KeywordFamily)
            {
                KeywordFamilyJson keywordFamilyJson = new KeywordFamilyJson();
                KeywordFamily keywordFamily = entityBase as KeywordFamily;
                keywordFamilyJson.id = keywordFamily.KeywordFamilyId;
                keywordFamilyJson.name = keywordFamily.KeywordFamilyName;
                keywordFamilyJson.KeywordStatus = keywordFamily.KeywordStatus;
                keywordFamilyJson.SysUserId = keywordFamily.SysUserId;
                return keywordFamilyJson;
            }
            if (jsonEntity is UserKeywordJson && entityBase is UserKeyword)
            {
                UserKeywordJson userkeywordjson = new UserKeywordJson();
                UserKeyword userkeyword = entityBase as UserKeyword;
                userkeywordjson.id = userkeyword.KeywordId;
                userkeywordjson.name = userkeyword.KeywordContent;
                userkeywordjson.IsPrimary = userkeyword.IsPrimary;
                userkeywordjson.CreateTime = userkeyword.CreateTime.ToString();
                userkeywordjson.IsForbid = userkeyword.IsForbid;
                userkeywordjson.FamilyId = userkeyword.KeywordFamilyId;
                return userkeywordjson;

            }
            if (jsonEntity is PermissionJson && entityBase is Permission)
            {
                PermissionJson permissionJson = new PermissionJson();
                Permission permission = entityBase as Permission;
                permissionJson.id = permission.PermissionId;
                permissionJson.name = permission.PermissionName;
                permissionJson.desc = permission.PermissionDescription;
                return permissionJson;
            }
            if (jsonEntity is DataCenterJson && entityBase is DataCenter)
            {
                DataCenterJson dataCenterJson = new DataCenterJson();
                DataCenter dataCenter = entityBase as DataCenter;
                dataCenterJson.dataid = dataCenter.Dataid;
                dataCenterJson.datasourceid = dataCenter.Datasourceid;
                dataCenterJson.dataurl = dataCenter.Dataurl == null ? "" : dataCenter.Dataurl.ToString();
                dataCenterJson.datatitle = dataCenter.Datatitle == null ? "" : dataCenter.Datatitle.ToString();
                dataCenterJson.datavalue = dataCenter.Datavalue == null ? "" : dataCenter.Datavalue.ToString();
                dataCenterJson.datatime = dataCenter.Datatime == null ? "" : dataCenter.Datatime.ToString();
                dataCenterJson.createtime = dataCenter.Createtime == null ? "" : dataCenter.Createtime.ToString();
                dataCenterJson.databody = dataCenter.Databody == null ? "" : dataCenter.Databody.ToString();
                dataCenterJson.dataauthor = dataCenter.Dataauthor == null ? "" : dataCenter.Dataauthor.ToString();
                dataCenterJson.newsplatform = dataCenter.Newsplatform == null ? "" : dataCenter.Newsplatform.ToString();
                dataCenterJson.sitename = dataCenter.Sitename == null ? "" : dataCenter.Sitename.ToString();
                dataCenterJson.dataforward = dataCenter.Dataforward == null ? "" : dataCenter.Dataforward.ToString();
                dataCenterJson.datacomment = dataCenter.Datacomment == null ? "" : dataCenter.Datacomment.ToString();
                dataCenterJson.bbsclick = dataCenter.Bbsclick == null ? "" : dataCenter.Bbsclick.ToString();
                dataCenterJson.datasource = dataCenter.Datasource == null ? "" : dataCenter.Datasource.ToString();
                dataCenterJson.weiboid = dataCenter.Weiboid == null ? "" : dataCenter.Weiboid.ToString();
                dataCenterJson.emotionalvalue = dataCenter.Emotionalvalue == null ? 0 : Convert.ToInt16(dataCenter.Emotionalvalue);
                dataCenterJson.uid = dataCenter.Uid;
                return dataCenterJson;
            }
            if (jsonEntity is UserReportJson && entityBase is UserReport)
            {
                UserReportJson userReportJson = new UserReportJson();
                UserReport userReport = entityBase as UserReport;
                userReportJson.id = userReport.UserReportId;
                userReportJson.filename = userReport.FileName;
                userReportJson.filepath = userReport.FilePath;
                userReportJson.link = userReport.Link;
                userReportJson.createdate = userReport.CreateDate.ToString("");
                userReportJson.desc = userReport.Description;
                userReportJson.reporttype = userReport.ReportType;
                userReportJson.issysgen = userReport.IsSysGen;
                userReportJson.starttime = userReport.StartTime.ToString("yyyy-MM-dd");
                userReportJson.endtime = userReport.EndTime.ToString("yyyy-MM-dd");
                userReportJson.reportstatus = userReport.ReportStatus;
                return userReportJson;
            }
            if (jsonEntity is HotKeywordJson && entityBase is HotKeyword)
            {
                HotKeywordJson hotKeywordJson = new HotKeywordJson();
                HotKeyword hotKeyword = entityBase as HotKeyword;
                hotKeywordJson.id = hotKeyword.HotKeywordId;
                hotKeywordJson.name = hotKeyword.HotKeywordName;
                hotKeywordJson.weight = hotKeyword.HotKeywordWeight;
                hotKeywordJson.keywordFamilyID = hotKeyword.KeywordFamilyId;
                return hotKeywordJson;
            }
            return new BaseJson();
        }
    }
}
