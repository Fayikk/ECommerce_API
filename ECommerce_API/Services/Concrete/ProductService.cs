using AutoMapper;
using ECommerce_API.DataContext;
using ECommerce_API.Services.Abstract;
using Entities;
using Entities.DTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_API.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductService(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ProductDTO>> AddProduct(ProductDTO productDTO)
        {
            var result = await _context.Products.FirstOrDefaultAsync(x => x.ProductName.ToLower().Equals(productDTO.ProductName.ToLower()));

            if (result == null)
            {
                var obj = _mapper.Map<ProductDTO, Product>(productDTO);
                var addedObj = _context.Products.Add(obj);

                await _context.SaveChangesAsync();
                var response = _mapper.Map<Product, ProductDTO>(addedObj.Entity);
                return new ServiceResponse<ProductDTO>
                {
                    Data = productDTO,
                    Message = "Product is added",
                    Success = true,
                };
            }
               
            

            return new ServiceResponse<ProductDTO>
            {
                Message = "Product Exist",
                Success = false,

            };
         
        }

        public async Task<ServiceResponse<int>> DeleteProduct(int id)
        {
            var result = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return new ServiceResponse<int>
                {
                    Message = "Item is not found",
                    Success = false,
                };
            }
            else
            {
                _context.Products.Remove(result);
                await _context.SaveChangesAsync();
                return new ServiceResponse<int>
                {
                    Success = true,
                    Data = id,
                };
            }

        }

        public async Task<ServiceResponse<IEnumerable<ProductDTO>>> GetAll()
        {
            var response =  _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_context.Products);

            return new ServiceResponse<IEnumerable<ProductDTO>>
            {
                Data = response,
               
            };

        }

        public async Task<ServiceResponse<ProductDTO>> GetProduct(string productName)
        {
            var result = await _context.Products.FirstOrDefaultAsync(x => x.ProductName.ToLower().Equals(productName.ToLower()));
            var response = _mapper.Map<Product, ProductDTO>(result);
            if (response == null)
            {
                return new ServiceResponse<ProductDTO>
                {
                    Success = false,
                };
            }
            return new ServiceResponse<ProductDTO>
            {
                Data = response,
            };
        }

        public async Task<ServiceResponse<ProductSearchResult>> GetProducts(int page)
        {
            var pageResults = 3f;
            var result = await _context.Products.ToListAsync();
            var pageCount = Math.Ceiling((result).Count / pageResults);
            var products = await _context.Products.Skip((page - 1) * (int)pageResults).Take((int)pageResults).ToListAsync();
            var response = new ServiceResponse<ProductSearchResult>
            {
                Data = new ProductSearchResult
                {
                    Products = products,
                    Pages = (int)pageCount,
                    CurrentPage = page,
                }
            };
            return response;
        
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategory(int categoryId)
        {
            var result = await _context.Products.Where(x => x.CategoryId == categoryId).ToListAsync();
            if (result == null)
            {
                return new ServiceResponse<List<Product>>
                {
                    Message = "Product is not found",
                    Success = false,
                };
            }

            return new ServiceResponse<List<Product>>
            {
                Data = result,
                Success = true,
            };

        }

        public async Task<ServiceResponse<List<Product>>> SearchProducts(string searchText)
        {
            var result = await _context.Products.Where(x => x.ProductName.ToLower().Contains(searchText.ToLower())

            || x.Description.ToLower().Contains(searchText.ToLower())).OrderByDescending(x => x.Price).ToListAsync();

            var response = new ServiceResponse<List<Product>>
            {
                Data = result,
            };
            return response;
        }

        public async Task<ServiceResponse<ProductDTO>> UpdateProduct(ProductDTO productDTO)
        {
           var obj = await _context.Products.FirstOrDefaultAsync(x=>x.Id == productDTO.Id);

            if (obj == null)
            {
                return new ServiceResponse<ProductDTO>
                {
                    Success = false,
                    Message = "Item is not found",
                };
            }
            else
            {
                obj.ProductName = productDTO.ProductName;
                obj.Price = productDTO.Price;
                obj.Description = productDTO.Description;
                _context.Products.Update(obj);
                await _context.SaveChangesAsync();
                return new ServiceResponse<ProductDTO>
                {
                    Data = productDTO,
                    Success = true,
                    Message = "Product is updated",
                };
            }
        }
    }
}
