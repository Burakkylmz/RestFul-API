using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFul_API.Infrastructure.Entities.Concrete;
using RestFul_API.Infrastructure.Repository.Abstract;
using RestFul_API.Models.Dtos;

namespace RestFul_API.Controllers
{
    /*ProduceResponseTypes => Bir acrion metodu içerisinde bir çok dönüş türü ve yolu bulunma ihtimali yüksektir. Örneğin aşağıda bulunan "CreateNationalPark" metodun içeriisnde 2 adet değişik dönüş tipi bulunmaktadır. "ProduceResponseTypes" özniteliği kullanrak bu dönüş tiplerini Swagger gibi araçlar tarafından web API dokümantasyonlarında istemciler için daha açıklayıcı yanıt ayrıntıları üretir. */
    [ProducesResponseType(400)]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IAppRepository _repository;
        private readonly IMapper _mapper;

        public CategoryController(IAppRepository nationalParkRepository, IMapper mapper)
        {
            this._repository = nationalParkRepository;
            this._mapper = mapper;
        }

        //Swaggre UI aracılığı ile API üzerinde bazı testler yapmak isteyen geliştiriciler için bazı özet bilgiler ekliyoruz ki ilgili geliştirici API'yi rahatlıkla test etsin. Yani API'nin yetenekleri hakkında açıklama yapıyoruz. İlgili Action methodunun ne paremetre aldığı ne iş yaptığı vb.
        /// <summary>
        /// Get list of categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CategoryDto>))]
        public IActionResult GetCategory()
        {
            var categoryList = _repository.GetCategory();

            var objDto = new List<CategoryDto>();

            foreach (var obj in categoryList)
            {
                objDto.Add(_mapper.Map<CategoryDto>(obj));
            }

            return Ok(objDto);
        }


        /// <summary>
        /// Get individual category
        /// </summary>
        /// <param name="id">The Id of category</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetCategory")]
        [ProducesResponseType(200), ProducesResponseType(404)]
        public IActionResult GetCategory(Guid id)
        {
            var obj = _repository.GetCategory(id);

            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                var objDto = _mapper.Map<CategoryDto>(obj);
                return Ok(objDto);
            }
        }

        /// <summary>
        /// Add the new category
        /// </summary>
        /// <param name="categoryDto">In this process, Category Name and Description does requiert fields</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateNationalPark([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_repository.CategoryExists(categoryDto.Name))
            {
                ModelState.AddModelError("", "This category already exsist..!");
                return StatusCode(404, ModelState);
            }

            var categoryObj = _mapper.Map<Category>(categoryDto);

            if (!_repository.CreateCategory(categoryObj))
            {
                ModelState.AddModelError("", $"Something went wrong when creating a category {categoryObj.Name} or {categoryObj.Description}");

                return StatusCode(500, ModelState);
            }

            return Ok(categoryObj);
        }


        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="id">You must to type into field a id information</param>
        /// <param name="categoryDto">In this process, Category Name and Description does requiert fields</param>
        /// <returns></returns>
        [HttpPut("{id}", Name = "UpdateCategory")]
        public IActionResult UpdateCategory(Guid id, [FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null || categoryDto.Id != id)
            {
                return BadRequest(ModelState);
            }

            var categoryObj = _mapper.Map<Category>(categoryDto);

            if (!_repository.UpdateCategory(categoryDto.Id))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {categoryObj.Name}");
                return StatusCode(500, ModelState);
            }

            return Ok(categoryObj);
        }

        /// <summary>
        /// Delete the category
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(Guid id)
        {
            var categoryObj = _repository.GetCategory(id);

            if (! _repository.DeleteCategory(categoryObj.Id))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the this record {categoryObj.Id}");
            }

            return Ok(categoryObj);
        }
    }
}