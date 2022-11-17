using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Captcha.Manager.ApplicationServics.Image;
using Common.Captcha.Manager.Config.Entity;
using Common.Captcha.Manager.Config.Enum;
using Common.Captcha.Manager.Config.IServices.Image;
using Microsoft.Extensions.Options;

namespace Common.Captcha.Manager.ApplicationServics
{
   public class CaptchaService
   {
       private readonly CaptchaOptions _options;
       private readonly ICaptchaImageProvider _captchaImage;

       public CaptchaService( IOptions<CaptchaOptions> options, ICaptchaImageProvider captchaImage)
       {
           _captchaImage = captchaImage;
           _options = options == null ? throw new ArgumentNullException(nameof(options)) : options.Value;
       }

       public string GenerateStringCaptchaCode()
       {
           string letters = _options.CaptchaType switch
           {
               CaptchaType.Letter => "ABCDEFGHJKLMNPRTUVWXYZ",
               CaptchaType.Number => "2346789",
               CaptchaType.LetterAndNumber => "2346789ABCDEFGHJKLMNPRTUVWXYZ",
               _ => throw  new ArgumentNullException($"نوع کپچا معتبر نیست")
           };
            
           Random rand = new Random();
           int maxRand = letters.Length - 1;

           StringBuilder sb = new StringBuilder();

           for (int i = 0; i < _options.CaptchaLength-1; i++)
           {
               int index = rand.Next(maxRand);
               sb.Append(letters[index]);
           }

           return sb.ToString();
       }

       public byte[] GenerateCaptchaImage(string captcha,CaptchaImageParams? imageParams)
       {
           if (imageParams == null)
           {
               return _captchaImage.DrawCaptcha(captcha, _options.CaptchaImageParams);
           }

          

          
           return _captchaImage.DrawCaptcha(captcha,imageParams);
       }
   }
}
