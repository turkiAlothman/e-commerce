// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
import {createProductCard } from "./Components.js";
import { pagination} from "./Components.js";

///<reference path="jquery-3.6.0.min.js" />

var pagesize = 9;
var pageNumber = 2;

window.onload = function() {
        
    function GetProducts(size,number){
        $("#products").empty()
        $("#pagination").empty()
        $('#loading').show();
        $.ajax({
            type: "GET",
            url: "/api/Product?pageNumber="+number+"&pageSize="+size+"",
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
        console.log(dataIndex);
        pageNumber = dataIndex;
        GetProducts(pagesize,pageNumber);
    })
            
  }