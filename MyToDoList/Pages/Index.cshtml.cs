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
        private readonly ToDoDbContext toDoDbContext;

        public IndexModel(ToDoDbContext context)
        {
            toDoDbContext = context;
        }
        public void OnGet()
        {
            MyToDoItems = toDoDbContext.ToDoItems.ToList();
        }

        public async Task<IActionResult> OnPostAsync(ToDoItem item)
        {
            if (!ModelState.IsValid)
                return Page();

            item.AssignedTo = "Golois";
            item.CreatedDate = DateTime.Now;

            toDoDbContext.ToDoItems.Add(item);
            await toDoDbContext.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnGetDeleteAsync(int id)
        {
            var item = await toDoDbContext.ToDoItems.FindAsync(id);

            if(item != null)
            {
                toDoDbContext.ToDoItems.Remove(item);
                await toDoDbContext.SaveChangesAsync();
            }
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostUpdateItemAsync(ToDoItem item)
        {
            if (!ModelState.IsValid)
                return Page();

            toDoDbContext.ToDoItems.Update(item);
            await toDoDbContext.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
