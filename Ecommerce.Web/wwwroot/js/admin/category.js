function AllCategories() {
    Get("Category/all-categories", (data) => {
        var html = '<table class="table table-hover">' +
            '<tr><th style="width:50px">Id</th><th>Categori Adý</th><th></th></tr>';

        var arr = data;

        for (var i = 0; i < arr.length; i++) {
            html += '<tr>';
            html += `<td>${arr[i].id}</td><td>${arr[i].category_name}</td>`;
            html += `<td><i class="bi bi-pencil-square" onclick='UpdateCategory(${arr[i].id}, "${arr[i].category_name}")'></i></td>`;
            html += '</tr>';
        }
        html += '</table>';

        $("#divCategories").html(html);
    });
}

let selectedCategoryId = 0;

function NewCategory() {
    selectedCategoryId = 0;
    $("#inputCategoryName").val("");
    $("#categoryModal").modal("show");
}

function UpdateCategory(id, category_name) {
    selectedCategoryId = id;
    $("#inputCategoryName").val(category_name);
    $("#categoryModal").modal("show");
}

function SaveCategories() {
    var newCategory = {
        Id: 0,
        category_name: $("#inputCategoryName").val()
    }
    Post("Category/add-category", newCategory, (data) => { AllCategories(); });
}

$(document).ready(function () {
    AllCategories();
})

