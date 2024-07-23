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
using e_commerce.Extensions;
using Microsoft.AspNetCore.Authorization;
namespace e_commerce.Controllers
{
    [Authorize(Policy ="signed_in")]
    [ApiController]
    [Route("api/Product")]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        
        public ProductApiController(IProductRepository productRepository){
            _productRepository = productRepository;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] int pageNumber = 1 , [FromQuery] int pageSize = 10){
            
            var data =  _productRepository.all(pageNumber,pageSize);
            var count =  _productRepository.count();
            
            return Ok(new {data = await data, count = await count});
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
        public async Task<IActionResult> create( [FromForm] CreateProductForm form){
            
            //validate the file
            if(form.image is not null)
            {
                (bool valide , string message) =  form.image.validate();
                if(!valide) return BadRequest(new {message =message});
            }

            Product product = new Product(form.productName,form.Describtion,form.Price,form.Quantity);
            
            await _productRepository.create(product);
            
            if(form.image is not null)
            {
            
            string extenstion = form.image!.FileName.Split(".").Last();
            string url = $"static/productImages/{product.Id}.{extenstion}";
            string fullPath = Path.GetFullPath("wwwroot/"+ url);
            
            using(var file = System.IO.File.Create(fullPath)){
                await form.image.CopyToAsync(file);
            }
            
            product.imageUrl = url;
            await _productRepository.update(product);
            }

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