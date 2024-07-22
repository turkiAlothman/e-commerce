// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
import {createProductCard} from "./Components.js";

///<reference path="jquery-3.6.0.min.js" />



window.onload = function() {
        
    function GetProducts(){
        
        $.ajax({
            type: "GET",
            url: "/api/Product",
            data: "data",
            success: function (response) {
                response.forEach(element => {
                    $('#loading').hide();
                   $("#products").append($(createProductCard(element)));
                   
                });
            }
        });

    }

    GetProducts()
            
  }