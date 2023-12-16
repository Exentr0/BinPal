using Backend.Data;
using Backend.Models;
using Backend.Services.Storage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SoftwareController : ControllerBase  
    {
        private readonly DataContext _context;
        private readonly SoftwarePicturesBlobService _softwarePicturesBlobService;

        public SoftwareController(DataContext context, SoftwarePicturesBlobService softwarePicturesBlobService)
        {
            _context = context;
            _softwarePicturesBlobService = softwarePicturesBlobService;
        }

        // Endpoint to get all software with their names and icon URLs
        [HttpGet("get-all-software")]
        public ActionResult<IEnumerable<object>> GetAllSoftware()
        {
            try
            {
                // Create a list to store software objects with ID, name, and icon URL
                var allSoftware = _context.Software.ToList().Select(software => new
                {
                    id = software.Id,
                    name = software.Name,
                    iconUrl = _softwarePicturesBlobService.GetSoftwarePictureUrl(software.Id)
                }).ToList();

                // Return the list of software objects as a successful response
                return Ok(allSoftware);
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error if an exception occurs
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpGet("plugins/{softwareId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetSoftwarePlugins(int softwareId)
        {
            try
            {
                // Retrieve plugins for the given softwareId
                var plugins = await _context.Plugins
                    .Where(p => p.SoftwareId == softwareId)
                    .Select(p => new
                    {
                        id = p.Id,
                        name = p.Name
                    })
                    .ToListAsync();

                // Return the list of id and name as a successful response
                return Ok(plugins);
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpGet("categories/{softwareId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetSoftwareCategories(int softwareId)
        {
            try
            {
                // Retrieve software categories for the given softwareId
                var softwareCategories = await _context.SoftwareCategories
                    .Where(sc => sc.SoftwareId == softwareId)
                    .Select(sc => new
                    {
                        id = sc.Category.Id,
                        name = sc.Category.Name
                    })
                    .ToListAsync();

                // Return the list of id and name as a successful response
                return Ok(softwareCategories);
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
