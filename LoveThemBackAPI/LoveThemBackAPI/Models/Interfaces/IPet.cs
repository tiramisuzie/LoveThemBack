using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveThemBackAPI.Models.Interfaces
{
    public interface IPet
    {
        ActionResult<List<Pet>> GetAll();

        ActionResult<Pet> GetById(int id);

        ActionResult AddPet(Pet Pet);

        ActionResult<Pet> UpdatePet(int id, Pet Pet);

        ActionResult DeletePet(int id, int pass);
    }
}
