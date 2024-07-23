using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_commerce.Domain.Models;
using e_commerce.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers.REST
{
    [ApiController]
    [Route("api/test")]
    public class test : ControllerBase
    {
        private readonly IProductRepository _ProductRepository;

        
        public test(IProductRepository _ProductRepository){
            this._ProductRepository = _ProductRepository;
            }


            [HttpGet]
            public async Task<IActionResult> Index(){
                return Ok(await this._ProductRepository.join(new List<ProductItem>{new ProductItem{ProductId= "669dbf9b961513b41b34abdf",Quentity=2},new ProductItem{ProductId= "669dbf9b961513b41b34abe0",Quentity=2} }));
            }
        
    }
}