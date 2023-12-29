using Ecommerce.Business.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src
{
    [ApiController]
    [Route("api/v1/[controller]s")]
    public class BaseController<T, TReadDTO, TCreateDTO, TUpdateDTO> : ControllerBase where T : BaseEntity
    {
        protected readonly IBaseService<T, TReadDTO, TCreateDTO, TUpdateDTO> _service;
        public BaseController(IBaseService<T, TReadDTO, TCreateDTO, TUpdateDTO> service)
        {
            _service = service;
        }

        [HttpPost()]
        public virtual async Task<ActionResult<TReadDTO>> CreateOneAsync([FromBody] TCreateDTO createObject)
        {
            return CreatedAtAction(nameof(CreateOneAsync), await _service.CreateOneAsync(createObject));
        }

        [HttpDelete("{id:guid}")]
        public virtual async Task<ActionResult<bool>> DeleteOneAsync([FromRoute] Guid id)
        {
            return Ok(await _service.DeleteOneAsync(id));
        }

        [HttpGet()]
        public virtual async Task<ActionResult<IEnumerable<TReadDTO>>> GetAllAsync([FromQuery] GetAllOptions getAllOptions)
        {
            return Ok(await _service.GetAllAsync(getAllOptions));
        }

        [HttpGet("{id:guid}")]
        public virtual async Task<ActionResult<TReadDTO>> GetByIdAsync([FromRoute] Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPatch("{id:guid}")]
        public virtual async Task<ActionResult<bool>> UpdateOneAsync([FromRoute] Guid id, [FromBody] TUpdateDTO updateObject)
        {
            return Ok(await _service.UpdateOneAsync(id, updateObject));
        }
    }
}