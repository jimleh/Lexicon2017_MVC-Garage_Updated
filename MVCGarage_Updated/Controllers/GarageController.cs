using MVCGarage_Updated.Models;
using MVCGarage_Updated.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

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

        [ResponseType(typeof(VehicleViewModel))]
        public IHttpActionResult Get(int? id)
        {
            if(id == null)
            { 
                return BadRequest("ID cannot be null!");
            }

            var vehicle = repo.GetVehicle(id.Value);
            if(vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        [ResponseType(typeof(VehicleType))]
        public IHttpActionResult GetTypes()
        {
            return Ok(repo.GetVehicleTypes());
        }

        [ResponseType(typeof(VehicleViewModel))]
        public IHttpActionResult Post([FromBody]VehicleViewModel vm)
        {
            if(ModelState.IsValid)
            {
                repo.AddVehicle(vm);
                return Ok("Vehicle added successfully! " + vm);
            }
            return BadRequest("ModelState is not valid!");
        }

        [ResponseType(typeof(VehicleViewModel))]
        public IHttpActionResult Put(int? id, [FromBody]VehicleViewModel vm)
        {
            if (id == null || id != vm.ID || !ModelState.IsValid)
            {
                return BadRequest("Something went horribly wrong! " + vm);
            }

            var vehicle = repo.GetVehicle(id.Value);
            if(vehicle == null)
            {
                return NotFound();
            }

            repo.EditVehicle(vm);

            return Ok("Vehicle Edited Successfully! " + vm);
        }

        public IHttpActionResult Delete(int? id)
        {
            if(id == null)
            {
                return BadRequest("You need to provide an ID! " + id);
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
