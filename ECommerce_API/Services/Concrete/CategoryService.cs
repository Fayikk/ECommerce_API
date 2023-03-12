using AutoMapper;
using ECommerce_API.DataContext;
using ECommerce_API.Services.Abstract;
using Entities;
using Entities.DTO;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_API.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CategoryService(IMapper mapper,ApplicationDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<CategoryDTO>> AddCategory(CategoryDTO categoryDTO)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(x=>x.CategoryName.ToLower().Equals(categoryDTO.CategoryName.ToLower()));

            if (result != null)
            {
                return new ServiceResponse<CategoryDTO>
                {
                    Message = "Category Exist",
                    Success = false,
                };
            }
            else
            {
                var obj = _mapper.Map<CategoryDTO, Category>(categoryDTO);
                var addedObj = _context.Categories.Add(obj);
                await _context.SaveChangesAsync();
                return new ServiceResponse<CategoryDTO>
                {
                    Success = true,
                    Message = "Your insert process is success",
                    Data = categoryDTO,
                };
            }


        }

        public async Task<ServiceResponse<List<Category>>> GetAllCategories()
        {
            var result = await _context.Categories.ToListAsync();
            if (result == null)
            {
                new ServiceResponse<List<Category>>
                {
                    Message = "Does not any categories",
                    Success = false,
                };
            }

            return new ServiceResponse<List<Category>>
            {
                Data = result,
                Success = true,
            };
        }
    }
}
