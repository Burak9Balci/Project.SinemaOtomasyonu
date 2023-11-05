using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Enums
{
    public enum UserRole
    {
        // Automation dediğimiz kişiler sinema departmanındaki kişilerdir 
        // DiAutomation ise admin in yetki verdiği kişilerin yeni Rolleridir.
       Customer = 1 , Automation = 2, Director = 3 , Admin = 4 , DiAutomation = 5
    }
}
