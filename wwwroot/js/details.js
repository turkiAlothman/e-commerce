// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


///<reference path="jquery-3.6.0.min.js" />

window.onload = function() {
        
        $("#addchart").on("click",function(){
          var id =  $("#addchart").data("product_id");
            console.log(id)
          $.ajax({
            type: "POST",
            url: "/api/Cart/" + id,
            data: "data",
            success: function (response) {
              console.log(response.status);
                $("#addModal").modal("show")            
            },
            error:function(response){
              $("#error-p").text(response.responseJSON.message ?? "something went wrong")
             $("#addModalfail").modal("show")
            }


        });
        })
  }