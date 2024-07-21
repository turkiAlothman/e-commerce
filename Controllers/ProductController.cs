using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_commerce.Configurations;
using e_commerce.Domain.Models;
using e_commerce.Domain.Repositories.Interfaces;
using e_commerce.DTOs.Requests;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Attributes;

namespace e_commerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        
        public ProductController(IProductRepository productRepository){
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> All(){
            return Ok(await _productRepository.all());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Products( string id){
            Product? product;
            
            try
            {
                product =  await _productRepository.getById(id);
            }
            catch(FormatException e)
            {
                return BadRequest(new {message ="Id is not valid"});    
            }

            if (product is null) return NotFound(new {message ="product not found"});
          
            return Ok(await _productRepository.getById(id));
        }

        [HttpPost]
        public async Task<IActionResult> create( [FromBody] CreateProductForm form){
            await _productRepository.create(new Product(form.productName,form.Describtion,form.Price,form.Quantity));
            return Ok(new {meesage ="product created successfully"});
        }
        
        [HttpPatch("{id}")]
        public async Task<IActionResult> patch(string id, [FromBody] JsonPatchDocument<Product> patch){
            
            // prevent deleteting attributes + protect the Id attribute from change
            
            foreach (var operation in patch.Operations)
            {
                if(operation.op.Equals("remove"))
                    return Unauthorized(new {message="product attribute cannot be removed"});
                if(operation.path.ToLower().Equals("/Id"))
                    return Unauthorized(new {message="id cannot be changed"});  
            }

            Product product ;
            
            try
            {
               product = await _productRepository.getById(id);
            }
            catch (System.Exception)
            {   
                return BadRequest(new {message ="Id is not valid"});
            }
            
            patch.ApplyTo(product,ModelState);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            await _productRepository.update(product);
            return Ok(new {meesage ="product updated successfully"});
            
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(string id){
            Product? product;
            
            try{
                product =  await _productRepository.getById(id);
            }
            catch(FormatException e){
                return BadRequest(new {message ="Id is not valid"});    
            }

            if (product is null) return NotFound(new {message ="product not found"});
            await _productRepository.delete(id);
            return Ok(new {message="product deleted successfully"});
        }
    }
}