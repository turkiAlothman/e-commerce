// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
import {createProductCard } from "./Components.js";
import { pagination} from "./Components.js";

///<reference path="jquery-3.6.0.min.js" />

var pagesize = 9;
var pageNumber = 1;

window.onload = function() {
        
    function GetProducts(size,number){
        $("#products").empty()
        $("#pagination").empty()
        $('#loading').show();
        
        var url ="/api/Product?pageNumber="+number+"&pageSize="+size+"";
        // window.history.pushState(url,'Updated the URL',url );
        
        $.ajax({
            type: "GET",
            url: url,
            data: "data",
            success: function (response) {
                response.data.forEach(element => {
                   $('#loading').hide();
                   $("#products").append($(createProductCard(element)));
                   
                   
                }
            
            );
            $("#pagination").append($(pagination(pageNumber, pagesize, response.count)))
            }
        });

    }

    GetProducts(pagesize,pageNumber);


    $(document).on("click",".page-link",function(e){
        var dataIndex = $(e.target).data("index");
        pageNumber = dataIndex;
        GetProducts(pagesize,pageNumber);
    })
            
  }