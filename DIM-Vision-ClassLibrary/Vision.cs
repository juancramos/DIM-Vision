using DIM_Vision_Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIM_Vision_ClassLibrary
{
    public class Vision
    {
        public Vision()
        {
            using (DatabaseContext context = new DatabaseContext())
            {

                //var std = new Student()
                //{
                //    Name = "Bill"
                //};

                //context.Students.Add(std);
                //context.SaveChanges();
            }
        }
    }
}
