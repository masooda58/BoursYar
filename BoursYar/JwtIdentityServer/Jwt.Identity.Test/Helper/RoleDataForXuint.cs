using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace Jwt.Identity.Test.Helper
{
    class RoleDataForXuint : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new List<string>()
                {
                    //new IdentityRole
                    {
                      //  Name = "Admin"
                      "Admin"
                    },
                   // new IdentityRole
                    {
                       // Name = "RegularUser"
                       "RegularUser"
                    }
                }





            };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}

