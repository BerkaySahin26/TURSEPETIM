﻿using EntityLayer.Concrete;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraversalCoreProje.Models;

namespace TraversalCoreProje.Controllers
{
    [AllowAnonymous]
    public class PasswordChangeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public PasswordChangeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet] // Şifre sıfırlama talebi için GET isteği
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]     // Şifre sıfırlama talebi için POST isteği
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            var user = await _userManager.FindByEmailAsync(forgetPasswordViewModel.Mail); // Kullanıcının e-posta adresinden kullanıcıyı bulur
            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user); // Kullanıcı için şifre sıfırlama token'i oluşturur
            var passwordResetTokenLink = Url.Action("ResetPassword", "PasswordChange", new   // Şifre sıfırlama linki oluşturur
            {
                userId = user.Id,
                token = passwordResetToken
            }, HttpContext.Request.Scheme);

            MimeMessage mimeMessage = new MimeMessage(); // E-posta gönderme işlemi için MimeMessage oluşturur

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", "projekursapi@gmail.com");

            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", forgetPasswordViewModel.Mail);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = passwordResetTokenLink;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = "Şifre Değişiklik Talebi";

            SmtpClient client = new SmtpClient();  // SMTP sunucusuna bağlanarak e-postayı gönderir
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("projekursapi@gmail.com", "wbxfftognsrqxjxc");
            client.Send(mimeMessage);
            client.Disconnect(true);

            return View();
        }

        [HttpGet] // Şifre sıfırlama için GET isteği
        public IActionResult ResetPassword(string userid, string token)
        {
            TempData["userid"] = userid;
            TempData["token"] = token;
            return View();
        }

        [HttpPost] // Şifre sıfırlama için POST isteği
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            var userid = TempData["userid"]; // TempData üzerinden kullanıcı ID ve token bilgilerini alır
            var token = TempData["token"];
            if (userid == null || token == null)
            {
                //hata mesajı
            }
            var user = await _userManager.FindByIdAsync(userid.ToString());  // Kullanıcıyı ID üzerinden bulur
            var result = await _userManager.ResetPasswordAsync(user, token.ToString(), resetPasswordViewModel.Password);  // Yeni şifre ile şifre sıfırlama işlemini gerçekleştirir
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn", "Login");  // Başarılı ise kullanıcıyı giriş sayfasına yönlendirir
            }
            return View();  // Başarısız ise tekrar şifre sıfırlama sayfasını gösterir
        }
    }
}
