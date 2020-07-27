using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http.ModelBinding;

namespace NabzeArz.Service
{
    public class ValidationService
    {
        public static List<string> GetModelStateErrors(ModelStateDictionary ModelState)
        {
            List<string> errorMessages = new List<string>();

            var validationErrors = ModelState.Values.Select(x => x.Errors);
            validationErrors.ToList().ForEach(ve => {
                var errorStrings = ve.Select(x => x.ErrorMessage);
                errorStrings.ToList().ForEach(em => {
                    errorMessages.Add(em);
                });
            });

            return errorMessages;
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidNumber(string number)
        {
            try
            {
                string pattern = @"^(?:(\u0660\u0669[\u0660-\u0669][\u0660-\u0669]{8})|(\u06F0\u06F9[\u06F0-\u06F9][\u06F0-\u06F9]{8})|(09[0-9][0-9]{8}))$" ; //define a pattern 
                Regex r = new Regex(pattern); //regular expression using for check input has a valid address or not
                Match match = r.Match(number); //match number with pattern
                return match.Success;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}