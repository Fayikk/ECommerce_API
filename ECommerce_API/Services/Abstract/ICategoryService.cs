using Entities;
using Entities.DTO;

namespace ECommerce_API.Services.Abstract
{
    public interface ICategoryService
    {
        Task<ServiceResponse<CategoryDTO>> AddCategory(CategoryDTO categoryDTO);
        Task<ServiceResponse<List<Category>>> GetAllCategories();
    }
}
