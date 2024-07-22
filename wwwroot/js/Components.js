
var placeholder = "https://cdn.shopify.com/s/files/1/0533/2089/files/placeholder-images-image_large.png?v=1530129081";
    
export function createProductCard(element) {
    if(element.imageUrl == null) element.imageUrl = placeholder;
    return `
        <div class="col-md-4 mb-4">
            <div class="card">
                <img src="${element.imageUrl}" class="object-fit-cover shadow-sm" style="width:100%;height:300px" alt="Product 2">
                <div class="card-body">
                    <h5 class="card-title">${element.productName}</h5>
                    <p class="card-text">Price: $${element.price}</p>
                    <a href="/home/details" class="btn btn-primary">View Details</a>
                </div>
            </div>
        </div>
    `;
}




export function pagination(pageNumber, pagesize, count) {
    
    var pages =  Math.ceil(count/pagesize);
    
    var previous = ""
    var Next = ""
    var middle = ""

    if(pageNumber !=1){
       previous = `
            <a class="page-link" data-index="${pageNumber - 1}" href="javascript:void(0)" aria-label="Previous">
                <span aria-hidden="true">&laquo;</span>
                <span class="sr-only" data-index="${pageNumber - 1}">Previous</span>
            </a>
        ` 
          middle = middle +`<li class="page-item"><a class="page-link" data-index="${pageNumber - 1}" href="javascript:void(0)">${pageNumber - 1}</a></li>`
    }

    middle = middle +`<li class="page-item active"><a class="page-link" data-index="${pageNumber}" href="javascript:void(0)">${pageNumber}</a></li>`
        
    if(pageNumber != pages){
        Next = `
            <a class="page-link" data-index="${pageNumber +1}" href="javascript:void(0)" aria-label="Next">
                <span aria-hidden="true">&raquo;</span>
                <span class="sr-only" data-index="${pageNumber +1}">Next</span>
            </a>
                `
            middle = middle +`<li class="page-item"><a class="page-link" data-index="${pageNumber +1}" href="javascript:void(0)">${pageNumber + 1}</a></li>`
            }
    
    
    
    


    return `
                <nav aria-label="Page navigation example">
        <ul class="pagination">
            <li class="page-item">
            ${previous}
            </li>
            
            ${middle}
            <li class="page-item">
            ${Next}
            </li>
        </ul>
        </nav>
    `;
}