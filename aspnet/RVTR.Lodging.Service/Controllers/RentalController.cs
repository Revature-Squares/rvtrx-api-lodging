using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Lodging.Domain.Interfaces;
using RVTR.Lodging.Domain.Models;

namespace RVTR.Lodging.Service.Controllers
{
  /// <summary>
  /// Campsite controller
  /// </summary>
  [ApiController]
  [ApiVersion("0.0")]
  [EnableCors("public")]
  [Route("rest/lodging/{version:apiVersion}/[controller]")]
  public class CampsiteController : ControllerBase
  {
    private readonly ILogger<CampsiteController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Constructor of the campsite controller
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public CampsiteController(ILogger<CampsiteController> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Action method for deleting a campsite by campsite id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        _logger.LogInformation($"Deleting a campsite @ id = {id}...");
        var campsite = await _unitOfWork.Campsite.SelectAsync(id);
        await _unitOfWork.Campsite.DeleteAsync(campsite.Id);
        await _unitOfWork.CommitAsync();
        _logger.LogInformation($"Successfully deleted a campsite @ id = {campsite.Id}.");
        return Ok();
      }
      catch (KeyNotFoundException e)
      {
        _logger.LogInformation(e, "Caught: {e.Message}. Id = {id}.", e, id);
        return NotFound(id);
      }
    }

    /// <summary>
    /// Action method for getting all campsites
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      _logger.LogInformation($"Getting all campsites...");
      return Ok(await _unitOfWork.Campsite.SelectAsync());
    }

    /// <summary>
    /// Action method for getting a campsite by campsite id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      try
      {
        _logger.LogInformation($"Getting a campsite @ id = {id}...");
        var campsite = await _unitOfWork.Campsite.SelectAsync(id);
        return Ok(campsite);
      }
      catch (KeyNotFoundException e)
      {
        _logger.LogInformation(e, "Caught: {e.Message}. Id = {id}.", e, id);
        return NotFound(id);
      }
    }

    /// <summary>
    /// Action method for creating a new campsite
    /// </summary>
    /// <param name="campsite"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(CampsiteModel campsite)
    {
      _logger.LogInformation($"Creating a new campsite @ {campsite}...");
      await _unitOfWork.Campsite.InsertAsync(campsite);
      await _unitOfWork.CommitAsync();
      _logger.LogInformation($"Successfully created a new campsite @ {campsite}.");
      return Accepted(campsite);
    }

    /// <summary>
    /// Action method for updating a preexisting campsite
    /// </summary>
    /// <param name="campsite"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(CampsiteModel campsite)
    {
      try
      {
        _logger.LogInformation($"Updating a campsite @ {campsite}...");
        var selectedCampsite = await _unitOfWork.Campsite.SelectAsync(campsite.Id);
        _unitOfWork.Campsite.Update(selectedCampsite);
        await _unitOfWork.CommitAsync();
        _logger.LogInformation($"Successfully updated a campsite @ {selectedCampsite}.");
        return Accepted(selectedCampsite);
      }
      catch (NullReferenceException e)
      {
        _logger.LogInformation(e, "Caught: {e}. Given campsite parameter was null.", e);
        return NotFound(campsite);
      }
      catch (KeyNotFoundException e)
      {
        _logger.LogInformation(e, "Caught: {e.Message}. Id = {campsite.Id}", e, campsite);
        return NotFound(campsite.Id);
      }
    }
  }
}
