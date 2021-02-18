using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Lodging.Domain.Interfaces;
using RVTR.Lodging.Domain.Models;

namespace RVTR.Campground.Service.Controllers
{
  /// <summary>
  /// The CampgroundController handles campground resources
  /// </summary>
  [ApiController]
  [ApiVersion("0.0")]
  [EnableCors("public")]
  [Route("rest/lodging/{version:apiVersion}/[controller]")]
  public class CampgroundController : ControllerBase
  {
    private readonly ILogger<CampgroundController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Constructor for the CampgroundController sets up logger and unitOfWork dependencies
    /// </summary>
    /// <param name="logger">The Logger</param>
    /// <param name="unitOfWork">The UnitOfWork</param>
    public CampgroundController(ILogger<CampgroundController> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Gets all the campgrounds in the database
    /// </summary>
    /// <returns>The Campgrounds</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CampgroundModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
      _logger.LogInformation($"Getting all campgrounds...");
      return Ok(await _unitOfWork.Campground.SelectAsync());
    }

    /// <summary>
    /// Gets one Campground based on its id
    /// </summary>
    /// <param name="id">The Campground Id</param>
    /// <returns>The Campgrounds if successful or NotFound if no campground was found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CampgroundModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
      try
      {
        _logger.LogInformation($"Getting a campground @ id = {id}...");
        var campground = await _unitOfWork.Campground.SelectAsync(id);
        return Ok(campground);
      }
      catch (KeyNotFoundException e)
      {
        _logger.LogInformation(e, "Caught: {e.Message}. Id = {id}.", e, id);
        return NotFound(id);
      }
    }

    /// <summary>
    /// Gets all campgrounds with available campsites by City, State/Province, Country and occupancy
    /// </summary>
    /// <param name="city">The city</param>
    /// <param name="state">The state/province</param>
    /// <param name="country">The country</param>
    /// <param name="occupancy">The occupancy</param>
    /// <returns>The filtered Campgrounds</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CampgroundModel>), StatusCodes.Status200OK)]
    [Route("available")]
    public async Task<IActionResult> GetCampgroundsByLocationAndOccupancy(string city, string state, string country, int occupancy)
    {
      _logger.LogInformation($"Getting all available campgrounds matching City: {city}, State: {state}, Country: {country}, Occupancy: {occupancy}...");
      return Ok(await _unitOfWork.Campground.CampgroundByLocationAndOccupancy(occupancy, city, state, country));
    }

    /// <summary>
    /// Action method for deleting a campground by campground id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        _logger.LogInformation($"Deleting a campground @ id = {id}...");
        CampgroundModel campground = await _unitOfWork.Campground.SelectAsync(id);
        await _unitOfWork.Campground.DeleteAsync(campground.Id);
        await _unitOfWork.CommitAsync();
        _logger.LogInformation($"Successfully deleted a campground @ id = {campground.Id}.");
        return Ok();
      }
      catch (KeyNotFoundException e)
      {
        _logger.LogInformation(e, "Caught: {e.Message}. Id = {id}.", e, id);
        return NotFound(id);
      }
    }

    /// <summary>
    /// Action method for creating a new campground
    /// </summary>
    /// <param name="campground"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(CampgroundModel campground)
    {
      _logger.LogInformation($"Creating a new campground @ {campground}...");
      await _unitOfWork.Campground.InsertAsync(campground);
      await _unitOfWork.CommitAsync();
      _logger.LogInformation($"Successfully created a new campground @ {campground}.");
      return Accepted(campground);
    }

    /// <summary>
    /// Action method for updating a preexisting campground
    /// </summary>
    /// <param name="campground"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(CampgroundModel campground)
    {
      try
      {
        _logger.LogInformation($"Updating a campground @ {campground}...");
        var newcampground = await _unitOfWork.Campground.SelectAsync(campground.Id);
        _unitOfWork.Campground.Update(newcampground);
        await _unitOfWork.CommitAsync();
        _logger.LogInformation($"Successfully updated a campground @ {newcampground}.");
        return Accepted(campground);
      }
      catch (NullReferenceException e)
      {
        _logger.LogInformation(e, "Caught: {e}. Given campground parameter was null.", e);
        return NotFound(campground);
      }
      catch (KeyNotFoundException e)
      {
        _logger.LogInformation(e, "Caught: {e.Message}. Id = {campground.Id}.", e, campground);
        return NotFound(campground.Id);
      }
    }
  }
}
