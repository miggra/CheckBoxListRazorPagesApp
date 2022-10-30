using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CheckBoxListRazorPagesApp.Models;

namespace CheckBoxListRazorPagesApp.Data
{
    public class CheckBoxListContext : DbContext
    {
        public CheckBoxListContext (DbContextOptions<CheckBoxListContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; } = default!;

        public void InitialFillTheMessageDb()
        {
            const string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            Random random = new Random();
            int initialMessageCount = random.Next(5, 16);

            for (int i = 1; i <= initialMessageCount; i++)
            {
                this.Messages.Add(new Message
                {
                    Id = i,
                    Title = $"Message #{i}",
                    Body = lorem.Substring(0, random.Next(11, lorem.Length))
                }) ;
            }
            this.SaveChanges();
        }
    }
}
