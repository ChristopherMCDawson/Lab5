using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab5.Data;
using Azure.Storage.Blobs;

namespace Lab5.Pages.Predictions
{
    public class CreateModel : PageModel
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string earthContainerName = "earthimages";
        private readonly string computerContainerName = "computerimages";
        private readonly Lab5.Data.PredictionDataContext _context;

        public CreateModel(Lab5.Data.PredictionDataContext context, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _blobServiceClient = blobServiceClient;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Prediction Prediction { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    // Upload the file to Azure Blob Storage
                    var containerClient = _blobServiceClient.GetBlobContainerClient("chrisddlab5");
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var blobClient = containerClient.GetBlobClient(fileName);
                    using (var stream = file.OpenReadStream())
                    {
                        await blobClient.UploadAsync(stream);
                    }

                    // Save the file URL to the database
                    var prediction = new Prediction
                    {
                        // Set other properties for the prediction
                        FileName = fileName,
                        Url = blobClient.Uri.AbsoluteUri,
                        // Set other properties for the prediction
                    };
                    _context.Predictions.Add(prediction);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
