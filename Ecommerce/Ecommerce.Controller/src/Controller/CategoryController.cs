using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<CategoryReadDTO>> GetAll()
        {
            return Ok(_categoryService.GetAllCategories());
        }
        [HttpGet("{id}")]
        public ActionResult<UserReadDTO> GetCategoryById([FromRoute] Guid id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost()]
        public ActionResult<UserReadDTO> CreateCategory([FromBody] CategoryCreateDTO categoryCreateDTO)
        {
            return CreatedAtAction(nameof(CreateCategory), _categoryService.CreateCategory(categoryCreateDTO));
        }
        [HttpPut("{id}")]
        public ActionResult<CategoryUpdateDTO> UpdateCategory(Guid id, [FromBody] CategoryUpdateDTO categoryUpdateDTO)
        {
            if (id != categoryUpdateDTO.Id)
            {
                return BadRequest("The provided ID does not match the user ID.");
            }
            _categoryService.UpdateCategory(categoryUpdateDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(Guid id)
        {
            if (_categoryService.DeleteUser(id))
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }


    }
}