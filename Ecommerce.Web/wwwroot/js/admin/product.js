function AllProducts() {
    Get("Product/all-products", (data) => {
        var html = '<table class="table table-hover">' +
            '<tr><th style="width:50px">Id</th><th>Ürün Adı</th><th>Ürün Açıklama</th><th>Ürün Fiyat</th><th>Ürün Adet</th><th>Ürün Stok Durumu</th><th>Ürün CategoryId</th><th>Ürün Img</th></tr>';

        var arr = data;

        for (var i = 0; i < arr.length; i++) {
            html += '<tr>';
            html += `<td>${arr[i].id}</td><td>${arr[i].title}</td><td>${arr[i].product_description}</td><td>${arr[i].price}</td><td>${arr[i].quantity}</td><td>${arr[i].in_stock}</td><td>${arr[i].category_id}</td><td>${arr[i].img}</td>`;
            html += `<td><i class="bi bi-trash text-danger" onclick='DeleteProduct(${arr[i].id})'></i><i class="bi bi-pencil-square" onclick='UpdateProduct(${arr[i].id}, "${arr[i].title}", "${arr[i].product_description}", "${arr[i].price}", "${arr[i].quantity}", "${arr[i].category_id}")'></i></td>`;
            html += '</tr>';
        }
        html += '</table>';

        $("#divProducts").html(html);
    });
}

let selectedProductId = 0;

function NewProduct() {
    selectedProductId = 0;
    $("#inputProductTitle").val();
    $("#inputProductDescription").val();
    $("#inputProductPrice").val();
    $("#inputProductQuantity").val();
    $("#inputProductCategoryId").val();
    $("#inputProductImg").val();
    $("#productModal").modal("show");
}

function SaveProduct() {
    var newProduct = {
        Id: 0,
        title: $("#inputProductTitle").val(),
        product_description: $("#inputProductDescription").val(),
        price: $("#inputProductPrice").val(),
        quantity: $("#inputProductQuantity").val(),
        category_id: $("#inputProductCategoryId").val(),
        img: $("#inputProductImg").val(),
    }
    Post("Product/add-product", newProduct, (data) => { AllProducts(); });
}

function DeleteProduct(id) {
    Delete(`Product/delete?id=${id}`, (data) => {
        AllProducts();
    });
}

function UpdateProduct(id, title, product_desc, price, quantity, category_id, img) {
    selectedProductId = id;
    $("#inputProductTitle").val(title);
    $("#inputProductDescription").val(product_desc);
    $("#inputProductPrice").val(price);
    $("#inputProductQuantity").val(quantity);
    $("#inputProductCategoryId").val(category_id);
    $("#inputProductImg").val(img);
    $("#productModal").modal("show");
}

$(document).ready(function () {
    AllProducts();
})