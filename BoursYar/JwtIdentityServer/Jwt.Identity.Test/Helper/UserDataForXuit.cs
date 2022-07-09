using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jwt.Identity.Domain.Models;

namespace Jwt.Identity.Test.Helper
{
    /// <summary>
    /// // Theory در بخش ClassData
    /// </summary>
    public class UserDataForXuit : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {

                new ApplicationUser
                {
                FirstName = "aFirstName",
                LastName = "bLastName",
                Email = "c@b.cEmail",
                UserName = "d@b.cUserName",
                Approved = true
                }

            };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }



}

