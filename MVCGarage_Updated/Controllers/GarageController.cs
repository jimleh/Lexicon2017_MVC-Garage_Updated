using MVCGarage_Updated.Models;
using MVCGarage_Updated.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVCGarage_Updated.Controllers
{
    public class GarageController : ApiController
    {
        GarageRepository repo;
        public GarageController()
        {
            repo = new GarageRepository();
        }

        public IHttpActionResult Get()
        {
            return Ok(repo.GetAllVehicles());
        }

        public IHttpActionResult Get(int? id)
        {
            if(id == null)
            { 
                return BadRequest();
            }

            var vehicle = repo.GetVehicle(id.Value);
            if(vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        public IHttpActionResult Post([FromBody]Vehicle vehicle)
        {
            if(ModelState.IsValid)
            {
                repo.AddVehicle(vehicle);
                return Ok("Vehicle added successfully!");
            }
            return BadRequest("What are you doing?" + vehicle.VehicleID + ":" + vehicle.VehicleNum + ":" + vehicle.VehicleSize + ":" + vehicle.VehicleType + ":" + vehicle.VehicleDate);
        }

        public IHttpActionResult Put(int? id, [FromBody]Vehicle newVehicle)
        {
            if (id == null || id != newVehicle.VehicleID || !ModelState.IsValid)
            {
                return BadRequest("What are you doing?");
            }

            var vehicle = repo.GetVehicle(id.Value);
            if(vehicle == null)
            {
                return NotFound();
            }

            repo.EditVehicle(newVehicle);

            return Ok("Vehicle Edited Successfully!");
        }

        public IHttpActionResult Delete(int? id)
        {
            if(id == null)
            {
                return BadRequest("You need to provide and ID!");
            }

            var vehicle = repo.GetVehicle(id.Value);
            if(vehicle == null)
            {
                return NotFound();
            }

            repo.RemoveVehicle(vehicle);
            return Ok("Vehicle Deleted Successfully!");
        }
    }
}
