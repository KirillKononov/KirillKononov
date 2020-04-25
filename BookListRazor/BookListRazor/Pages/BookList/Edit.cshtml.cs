using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        public EditModel(AppDbContext db)
        {
            _db = db;
        }

        public async Task OnGet(int id)
        {
            Book = await _db.Books.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var bookForUpdate = await _db.Books.FindAsync(Book.Id);
                bookForUpdate.Name = Book.Name;
                bookForUpdate.Author = Book.Author;

                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }

            return RedirectToPage();
        }

        [BindProperty]
        public Book Book { get; set; }
        private readonly AppDbContext _db;
    }
}