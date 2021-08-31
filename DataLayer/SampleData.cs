using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class SampleData
    {
        public static void InitData(EFDBContext context)
        {
            if (!context.User.Any())
            {
                context.User.Add(new Entityes.User() {FIO="Петров Иван Александрович",Email="PetrovIA@mail.com",Phone="79512044544",Password="Pass1234",LastLogin=DateTime.Now });
                context.SaveChanges();
            }
        }
    }
}
