using System.Threading.Tasks;
using AnseremTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnseremTest.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SalesController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            return View(await _db.Sales.ToListAsync());
        }

        // GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sale sale)
        {
            if (ModelState.IsValid)
            {
                _db.Sales.Add(sale);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(sale);
        }

        //POST - DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var sale = await _db.Sales.FindAsync(id);

            if (sale == null)
                return NotFound();

            var counterparty = await _db.Counterparties.FindAsync(sale.СounterpartyId);
            var contact = await _db.Contacts.FindAsync(sale.ContactId);
            var city = await _db.Cities.FindAsync(counterparty.CityId);

            _db.Sales.Remove(sale);
            _db.Counterparties.Remove(counterparty);
            _db.Contacts.Remove(contact);
            _db.Cities.Remove(city);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var sale = await _db.Sales.FindAsync(id);

            if (sale == null)
                return NotFound();

            return View(sale);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Sale sale)
        {
            if (ModelState.IsValid)
            {
                var saleForUpdate = await _db.Sales.FindAsync(sale.Id);

                saleForUpdate.Name = sale.Name;
                saleForUpdate.Сounterparty.Name = sale.Сounterparty.Name;
                saleForUpdate.Contact.Name = sale.Contact.Name;
                saleForUpdate.Contact.ResponsibleForSale = sale.Contact.ResponsibleForSale;
                saleForUpdate.Сounterparty.City.Name = sale.Сounterparty.City.Name;

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(sale);
        }
    }
}