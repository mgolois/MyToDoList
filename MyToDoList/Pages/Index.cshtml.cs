using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyToDoList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDoList.Pages
{
    public class IndexModel : PageModel
    {
        public List<ToDoItem> MyToDoItems { get; set; }
        private readonly IDbFactory db;

        public IndexModel(IDbFactory context)
        {
            db = context;
        }
        public void OnGet()
        {
            MyToDoItems = db.Context.ToDoItems.ToList();
        }

        public async Task<IActionResult> OnPostAsync(ToDoItem item)
        {
            if (!ModelState.IsValid)
                return Page();

            item.AssignedTo = "Golois";
            item.CreatedDate = DateTime.Now;

            db.Context.ToDoItems.Add(item);
            await db.Context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnGetDeleteAsync(int id)
        {
            var item = await db.Context.ToDoItems.FindAsync(id);

            if(item != null)
            {
                db.Context.ToDoItems.Remove(item);
                await db.Context.SaveChangesAsync();
            }
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostUpdateItemAsync(ToDoItem item)
        {
            if (!ModelState.IsValid)
                return Page();

            db.Context.ToDoItems.Update(item);
            await db.Context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
