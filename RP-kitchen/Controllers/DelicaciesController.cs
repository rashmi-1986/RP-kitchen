using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using RP_kitchen.Data;
using RP_kitchen.Models;
using RP_kitchen.Models.Domain;
using System.Drawing.Text;

namespace RP_kitchen.Controllers
{
    [Authorize]
    public class DelicaciesController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        private string uniqueFileName;

        public DelicaciesController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var delicacies = await applicationDbContext.Delicacies.ToListAsync();
            return View(delicacies);
        }


        [HttpGet]
        public IActionResult Add()
        {
            
            return View();
        }

        

        [HttpPost]
        public async Task<IActionResult> Add(AddDelicacyViewModel addDelicacyRequest)
        {
            var delicacy = new Delicacy()
            {
               
           
                Id = Guid.NewGuid(),
                Catagory = addDelicacyRequest.Catagory,
                Name = addDelicacyRequest.Name,
                Picture = addDelicacyRequest.Picture,
                Date = addDelicacyRequest.Date,
                Price = addDelicacyRequest.Price
            };
            await applicationDbContext.Delicacies.AddAsync(delicacy);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("Index");

            if (delicacy.Picture != null) ;

            {
                string uploadsFolder = Path.Combine(ApplicationDbContext.applicationRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + delicacy.Picture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    delicacy.Picture.CopyTo(filestream);
                }
            }
            return View(delicacy);
        }

        [HttpGet]

        public async Task<IActionResult> View(Guid id)
        {
            var delicacy = await applicationDbContext.Delicacies.FirstOrDefaultAsync(x => x.Id == id);

            if (delicacy != null)
            {
                var viewModel = new UpdateDelicacyViewModel()
                {
                    Id = delicacy.Id,
                    Catagory = delicacy.Catagory,
                    Name = delicacy.Name,
                    Date = delicacy.Date,
                    Picture = delicacy.Picture,
                    Price = delicacy.Price

                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");


           

        }

            [HttpPost]
        
        public async Task<IActionResult> View(UpdateDelicacyViewModel model)
            {
                var delicacy = await applicationDbContext.Delicacies.FindAsync(model.Id);

                if (delicacy != null)
                {
                    delicacy.Catagory = model.Catagory;
                    delicacy.Name = model.Name;
                    delicacy.Date = model.Date;
                    delicacy.Picture = model.Picture;
                    delicacy.Price = model.Price;

                    await applicationDbContext.SaveChangesAsync();
                }

                     return RedirectToAction("Index");
             }

            [HttpPost]
            public async Task<IActionResult> Delete(UpdateDelicacyViewModel model)
            {
                var delicacy = await applicationDbContext.Delicacies.FindAsync(model.Id);

                if (delicacy != null)
                {
                    applicationDbContext.Delicacies.Remove(delicacy);
                    await applicationDbContext.SaveChangesAsync();

                    return RedirectToAction("Index");

                }

                return RedirectToAction("Index");

            }
    }
}
