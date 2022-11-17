using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RP_kitchen.Data;
using RP_kitchen.Models;
using RP_kitchen.Models.Domain;

namespace RP_kitchen.Controllers
{
    public class DelicaciesController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

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
