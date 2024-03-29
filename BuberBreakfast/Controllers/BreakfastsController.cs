﻿using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.Services;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BreakfastsController: ControllerBase
    {
        private readonly IBreakfastService _breakfastService;
        public BreakfastsController(IBreakfastService breakfastService)
        {
            _breakfastService = breakfastService;
        }

        [HttpPost]
        public IActionResult CreateBreakfast(CreateBreakfastRequest request)
        {
            var breakfast = new Breakfast(
                Guid.NewGuid(),
                request.Name,
                request.Description,
                request.StartDateTime,
                request.EndDateTime,
                DateTime.UtcNow,
                request.Savory,
                request.Sweet
                );

           _breakfastService.CreateBreakfast(breakfast);

            var breakfastResponse = new BreakfastResponse(
                breakfast.Id,
                breakfast.Name,
                breakfast.Description,
                breakfast.StartDateTime,
                breakfast.LastModifiedDateTime,
                DateTime.UtcNow,
                breakfast.Savory,
                breakfast.Sweet);

            
            return CreatedAtAction(
                actionName: nameof(GetBreakfast),
                routeValues: new {id = breakfast.Id},
                value: breakfastResponse);
        }

        [HttpGet("/{id:guid}")]
        public IActionResult GetBreakfast(Guid id)
        {
            Breakfast breakfast = _breakfastService.GetBreakfast(id);

            var breakfastResponse = new BreakfastResponse(
                breakfast.Id,
                breakfast.Name,
                breakfast.Description,
                breakfast.StartDateTime,
                breakfast.EndDateTime,
                breakfast.LastModifiedDateTime,
                breakfast.Savory,
                breakfast.Sweet);
            return Ok(breakfastResponse);
        }

        [HttpPut("/{id:guid}")]
        public IActionResult UpSertBreakfast(Guid id, UpsertBreakfastRequest request)
        {
           var breakfast = new Breakfast(
                              id,
                              request.Name,
                              request.Description,
                              request.StartDateTime,
                              request.EndDateTime,
                              DateTime.UtcNow,
                              request.Savory,
                              request.Sweet
                              );

            _breakfastService.UpsertBreakfast(breakfast);

            var breakfastResponse = new BreakfastResponse(
                               breakfast.Id,
                               breakfast.Name,
                               breakfast.Description,
                               breakfast.StartDateTime,
                               breakfast.EndDateTime,
                               breakfast.LastModifiedDateTime,
                               breakfast.Savory,
                               breakfast.Sweet);

            return NoContent();
        }

        [HttpDelete("/{id:guid}")]
        public IActionResult DeleteBreakfast(Guid id)
        {
            _breakfastService.DeleteBreakfast(id);
            return NoContent();
        }
    }
}
