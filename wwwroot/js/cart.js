// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


///<reference path="jquery-3.6.0.min.js" />




window.onload = function() {  
    $(document).on("click",".incementb",function(e){
        var id = $(e.target).data("product")
        var action = $(e.target).data("action")
        var data =  $("#counter-"+id)
        var card = $("#card-"+id)

        var max = data.data("max")
        var count = data.data("count")
        

        console.log(action);
        
        if(action === "add"){
            
            if (count >= max) {
                $("#errorbody").text('There are only '+max+' units of this product available.')
                $("#errormessage").toast('show')
                return
            }
            
            send("/api/Cart/"+id,"POST",function (response) {
                console.log(response);          
              })
            data.data("count",count + 1)
            data.val(count + 1)
        }
        else if(action === "remove"){
            
            if (count == 1) {
                return
            }
            send("/api/Cart/"+id,"DELETE",function (response) {
                console.log(response);          
              })
            data.data("count",count - 1)
            data.val(count - 1)
        }
        else if(action === "delete"){
            send("/api/Cart/remove/"+id,"DELETE",function (response) {
                console.log(response);          
              })
            card.remove()
        }
    })



        function send(url, method,func){
                $.ajax({
                type: method,
                url: url,
                data: "data",
                success: func,
                error: function(status){
                    console.log(status.responseJSON.message);
                    $("#error-p").text(status.responseJSON.message)
                    $("#odalfail").modal("show")
                }


            });
}


$("#checkout").on("click",function(){
    send("/api/Cart/checkout","POST",function (response) {
        $("#successModal").modal("show")      
      })
})


}

