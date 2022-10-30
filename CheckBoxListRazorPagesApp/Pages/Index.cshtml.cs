using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CheckBoxListRazorPagesApp.Data;
using CheckBoxListRazorPagesApp.Models;

namespace CheckBoxListRazorPagesApp.Pages
{
    public class MessagesListModel : PageModel
    {
        private readonly CheckBoxListRazorPagesApp.Data.CheckBoxListContext _context;

        public MessagesListModel(CheckBoxListRazorPagesApp.Data.CheckBoxListContext context)
        {
            _context = context;
        }

        public IList<Message> Messages { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Messages != null)
            {
                Messages = await _context.Messages.ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostAsync([FromForm] int[] checkedMessagesIds)
        {

            if (checkedMessagesIds == null || _context.Messages == null)
                return NotFound();
            
            var checkedMessages = await _context.Messages.Where(m => 
                checkedMessagesIds.Contains(m.Id)).ToListAsync();

            if (checkedMessages.Count != checkedMessagesIds.Length)
                return BadRequest();

            _context.Messages.RemoveRange(checkedMessages);
                
            await _context.SaveChangesAsync();
            await OnGetAsync();

            return Page();
        }
    }
}
