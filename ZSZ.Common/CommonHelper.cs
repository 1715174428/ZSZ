using Microsoft.Extensions.Logging;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace ZSZ.Common
{
    public static class CommonHelper
    {

        
        /// <summary>
        /// 获取一个字符串的MD5值
        /// </summary>
        /// <param name="input">要获取签名的字符串</param>
        /// <param name="isToUp">MD5值是否大写</param>
        /// <returns></returns>
        public static string GetMd5(string input, bool isToUp = false)
        {
            return GetMd5(Encoding.UTF8.GetBytes(input));
        }
        /// <summary>
        /// 获取byte数组的md5值
        /// </summary>
        /// <param name="buffer">要获取签名的byte数组</param>
        /// <param name="isToUp">MD5值是否大写</param>
        /// <returns></returns>
        public static string GetMd5(byte[] buffer, bool isToUp = false)
        {
            using (MD5 md5 = MD5.Create())
            {
                var comBuffer = md5.ComputeHash(buffer);
                string result = "";
                for (int i = 0; i < comBuffer.Length; i++)
                {
                    result += comBuffer[i].ToString("X").Length == 1 ? "0" + comBuffer[i].ToString("X") : comBuffer[i].ToString("X");
                }
                return isToUp ? result.ToUpper() : result.ToLower();
            }
        }
        /// <summary>
        /// 获取指定长度的验证码
        /// </summary>
        /// <param name="len">验证码的长度</param>
        /// <returns></returns>
        public static string GetCaptchaCode(int len)
        {
            char[] data = { 'a', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'm', 'n', 'p', 'r', 's', 't', 'w', 'x', 'y', '2', '3', '4', '5', '7', '8' };
            StringBuilder code = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < len; i++)
            {
                code.Append(data[rand.Next(0, data.Length)]);
            }
            return code.ToString();
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="toEmail">收件人,多个收件使用";"分隔</param>
        /// <param name="title">邮件标题</param>
        /// <param name="body">邮件正文</param>
        /// <returns></returns>
        public static string SendEmail(string toEmail, string title, string body)
        {
            try
            {
                using (var smtp = new SmtpClient(EmailConfig.SenderServerHost, EmailConfig.SenderPort) { Credentials = new NetworkCredential() { UserName = EmailConfig.SenderUsername, Password = EmailConfig.SenderPassword, } })
                using (var mailMessage = new MailMessage() { Subject = title, Body = body, Sender = new MailAddress(EmailConfig.SenderUsername) })
                {
                    mailMessage.From = new MailAddress(EmailConfig.SenderUsername);
                    foreach (var item in toEmail.Split(";"))
                    {
                        mailMessage.To.Add(item);
                    }
                    smtp.Send(mailMessage);
                    return "ok";
                }
            }
            catch (Exception ex)
            {

                return "发送失败!" + ex.Message + "------" + ex.InnerException;
            }
        }
        /// <summary>
        /// 根据原始图片生成指定宽高的缩略图
        /// </summary>
        /// <param name="originalImgUrl">原始图片地址</param>
        /// <param name="thumbnailImgUrl">缩略图地址</param>
        /// <param name="width">缩略图的宽度</param>
        /// <param name="height">缩略图的高度</param>
        /// <returns></returns>
        public static string GetThumbnailImgPath(string originalImgPath, int width,int height)
        {
            string thumbnailImgPath;
            using (Image<Rgba32> image = Image.Load(originalImgPath))
            {
                image.Mutate(x => x.Resize(width, height));
                thumbnailImgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/thumbnailimages",  Guid.NewGuid() + ".jpeg");
                image.Save(thumbnailImgPath);
            }
            return thumbnailImgPath;
        }
        /// <summary>
        /// 生成带水印的图片
        /// </summary>
        /// <param name="originalImgPath">原始图片路径</param>
        /// <param name="watermarkImgPath">带水印的图片路径</param>
        /// <param name="content">水印内容</param>
        /// <returns></returns>
        public static string GetWatermarkImgPath(string originalImgPath, string content)
        {
            string watermarkImgPath;
            var fontFiles = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/fonts"));
            var fontFile = fontFiles[new Random().Next(0, fontFiles.Length)];
            var installFamily = new FontCollection().Install(fontFile);
            var font = new Font(installFamily,35);  //字体
            using (Image<Rgba32> image = Image.Load(originalImgPath))
            {
                image.Mutate(x => x
                     .DrawText(
                        content.ToUpper(),   //文字内容
                         font,
                         Rgba32.HotPink,
                         new Vector2(10, 0),
                         TextGraphicsOptions.Default)
                     );
                watermarkImgPath =Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/watermarkimages", Guid.NewGuid()  + ".jpeg");
                image.Save(watermarkImgPath);
            }
            return watermarkImgPath;
        }
    }
    /// <summary>
    /// 邮件初始配置
    /// </summary>
    public static class EmailConfig
    {
        public static readonly int SenderPort = 25;   //发送邮件所用的端口号（htmp协议默认为25）
        public static readonly string SenderServerHost = "smtp.mxhichina.com";    //发件箱的邮件服务器地址（IP形式或字符串形式均可）
        public static readonly string SenderPassword = "Cs00000000";    //发件箱的密码
        public static readonly string SenderUsername = "ceshi@zmdcq.cn";   //发件箱的用户名（即@符号前面的字符串，例如：hello@163.com，用户名为：hello）
        public static readonly bool EnableSsl = false;    //是否对邮件内容进行socket层加密传输
        public static readonly bool EnablePwdAuthentication = true;  //是否对发件人邮箱进行密码验证
    }

}
