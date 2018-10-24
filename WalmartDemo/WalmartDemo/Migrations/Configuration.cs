namespace WalmartDemo.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    

    internal sealed class Configuration : DbMigrationsConfiguration<WalmartDemo.Models.CoursesContext>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = true;
            //AutomaticMigrationDataLossAllowed = true;
            ContextKey = "WalmartDemo.Models.CoursesContext";
        }

        protected override void Seed(WalmartDemo.Models.CoursesContext context)
        {
            //Change file to your local file path
            string[] text = System.IO.File.ReadAllLines(@"C:\Users\nakshatras\source\repos\WalmartDemo\WalmartDemo\Migrations\courses.txt");


            foreach (String str in text)
            {
                String[] coursedetails = str.Split(new char[0]);
                if (coursedetails[0].Length>5 || (int.TryParse(coursedetails[0], out int n)) || (int.TryParse(coursedetails[coursedetails.Length-2], out int s)))
                {
                    if (coursedetails.Length == 4)
                    {
                        context.Courses.AddOrUpdate(
                       p => new { p.Id, p.Name, p.Length, p.Subject },
                       new Models.Courses { Id = coursedetails[0], Name = coursedetails[1], Length = coursedetails[2], Subject = coursedetails[3] }
                    );
                    }
                    else
                    {
                        int len = coursedetails.Length;
                        String coursename = "";
                        for (int i = 1; i <= len - 3; i++)
                        {
                            coursename += coursedetails[i]+" ";
                        }
                        context.Courses.AddOrUpdate(
                      p => new { p.Id, p.Name, p.Length, p.Subject },
                      new Models.Courses { Id = coursedetails[0], Name = coursename, Length = coursedetails[len - 2], Subject = coursedetails[len - 1] }
                   );

                    }
                }

            }
            

        }
    }
}
