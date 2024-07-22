
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