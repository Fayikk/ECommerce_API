using Entities;
using Entities.DTO;
using Entities.Models;

namespace ECommerce_API.Services.Abstract
{
    public interface IProductService
    {
        Task<ServiceResponse<ProductDTO>> AddProduct(ProductDTO productDTO);//Ekleme
        Task<ServiceResponse<int>> DeleteProduct(int id);//Silme
        Task<ServiceResponse<ProductDTO>> UpdateProduct(ProductDTO productDTO);//Güncellme
        Task<ServiceResponse<IEnumerable<ProductDTO>>> GetAll();//tüme öğreleri döndür
        Task<ServiceResponse<ProductDTO>> GetProduct(string productName);//ilgili product dönsün
        Task<ServiceResponse<List<Product>>> GetProductsByCategory(int categoryId); //Kategorite ait ürünleri döndür
        Task<ServiceResponse<List<Product>>> SearchProducts(string searchText);//Arama Çubuğu
        Task<ServiceResponse<ProductSearchResult>> GetProducts(int page);//Sayfalama(Pagination)
    }
}
