using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreMvcExamApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CoreMvcExamApp.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly StoreContext _context;

        public InvoicesController(StoreContext context)
        {
            _context = context;
        }

        

        // GET: Invoices/Create
        public IActionResult Create()
        {
            return View(new InvoiceViewModel());
        }

        // POST: Invoices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvoiceViewModel invoiceViewModel, string command = "")
        {
            if (command == "Add")
            {
                invoiceViewModel.Items.Add(new InvoiceItemViewModel());
                return View(invoiceViewModel);
            }
            else if (command.Contains("delete"))
            {
                int idx = int.Parse(command.Split('-')[1]);
                invoiceViewModel.Items.RemoveAt(idx);
                ModelState.Clear();
                return View(invoiceViewModel);
            }

            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    CustomerName = invoiceViewModel.CustomerName,
                    Address = invoiceViewModel.Address,
                    ContactNo = invoiceViewModel.ContactNo
                };

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                var invoice = new Invoice
                {
                    InvoiceDate = invoiceViewModel.InvoiceDate,
                    CustomerId = customer.CustomerId,
                    Items = invoiceViewModel.Items.Select(i => new InvoiceItem
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice = _context.Products.First(p => p.ProductId == i.ProductId).Price
                    }).ToList()
                };

                _context.Invoices.Add(invoice);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(invoiceViewModel);
        }
        //// GET: Invoices/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var invoice = await _context.Invoices.Include(i => i.Items).ThenInclude(p => p.Product).FirstOrDefaultAsync(i => i.Id == id);
        //    if (invoice == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(invoice);
        //}

        //// POST: Invoices/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Invoice invoice, string command = "")
        //{
        //    if (command == "Add")
        //    {
        //        invoice.Items.Add(new());
        //        return View(invoice);
        //    }
        //    else if (command.Contains("delete"))// delete-3   ["delete", "3"]
        //    {
        //        int idx = int.Parse(command.Split('-')[1]);

        //        invoice.Items.RemoveAt(idx);
        //        ModelState.Clear();
        //        return View(invoice);
        //    }
        //    if (id != invoice.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {

        //            //_context.Update(invoice);
        //            ////await _context.SaveChangesAsync();

        //            //var itemsIdList = invoice.Items.Select(i => i.ItemId).ToList();

        //            //var delItems = await _context.InvoiceItems.Where(i => i.InvoiceId == id).Where(i => !itemsIdList.Contains(i.ItemId)).ToListAsync();


        //            //_context.InvoiceItems.RemoveRange(delItems);


        //            //await _context.SaveChangesAsync();



        //            var row = await _context.Database.ExecuteSqlRawAsync("exec SpUpdateInvoice @p0, @p1, @p2, @p3, @p4", invoice.Id, invoice.InvoiceDate, invoice.CustomerName, invoice.Address, invoice.ContactNo);


        //            foreach (var item in invoice.Items)
        //            {
        //                await _context.Database.ExecuteSqlRawAsync("exec SpInsertInvoiceItem @p0, @p1, @p2", invoice.Id, item.ProductId, item.Quantity);
        //            }

        //            return RedirectToAction(nameof(Index));





        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!InvoiceExists(invoice.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(invoice);
        //}

        //// GET: Invoices/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var invoice = await _context.Invoices.Include(i => i.Items).ThenInclude(p => p.Product)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (invoice == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(invoice);
        //}

        //// POST: Invoices/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    //var invoice = await _context.Invoices.FindAsync(id);
        //    //if (invoice != null)
        //    //{             

        //    //    //_context.Invoices.Remove(invoice);
        //    //}
        //    await _context.Database.ExecuteSqlAsync($"exec SpDeleteInvoice {id}");

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        //[HttpDelete]
        //[Route("~/deleteinvoice/{id}")]
        //public async Task<IActionResult> DeleteAjax(int id)
        //{
        //    //var invoice = await _context.Invoices.FindAsync(id);
        //    //if (invoice != null)
        //    //{             

        //    //    //_context.Invoices.Remove(invoice);
        //    //}
        //    await _context.Database.ExecuteSqlAsync($"exec spDeleteInvoice {id}");

        //    //await _context.SaveChangesAsync();

        //    return Ok();
        //}
        //private bool InvoiceExists(int id)
        //{
        //    return _context.Invoices.Any(e => e.Id == id);
        //}
    }
}
