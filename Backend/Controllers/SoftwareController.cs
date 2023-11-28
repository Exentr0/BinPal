using Backend.Data;
using Backend.Models;
using Backend.Services.Storage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SoftwareController : ControllerBase  
    {
        private readonly DataContext _context;
        private readonly SoftwarePicturesBlobService _softwarePicturesBlobService;

        // Constructor to initialize the controller with required services
        public SoftwareController(DataContext context, SoftwarePicturesBlobService softwarePicturesBlobService)
        {
            _context = context;
            _softwarePicturesBlobService = softwarePicturesBlobService;
        }

        [HttpGet("get-all-software")]
        public ActionResult<IEnumerable<Software>> GetAllSoftware()
        {
            // List to store tuples of software name and icon URL
            List<Tuple<string, string>> allSoftware = new List<Tuple<string, string>>();
            try
            {
                // Iterate through each software item in the database
                foreach (var software in _context.Software.ToList())
                {
                    // Get the icon URL for the current software
                    string iconUrl = _softwarePicturesBlobService.GetSoftwarePictureUrl(software.Id);

                    // Create a tuple with software name and icon URL
                    var softwareInfo = Tuple.Create(software.Name, iconUrl);

                    // Add the tuple to the list
                    allSoftware.Add(softwareInfo);
                }

                // Return the list of software tuples as a successful response
                return Ok(allSoftware);
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error if an exception occurs
                return StatusCode(500, "Internal server error");
            }
        }
    }
}