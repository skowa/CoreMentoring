const baseUri = 'https://localhost:44326/api/';
const categoriesPath = 'categories';
const productsPath = 'products';

$(document).ready(function () {
    $.getJSON(baseUri + categoriesPath)
        .done(function (data) {
        $.each(data, function (key, item) {
            $('<li>', { class: 'list-group-item', text: formatCategory(item) }).appendTo($('#categories'));
        });
        });

    $.getJSON(baseUri + productsPath)
        .done(function (data) {
        $.each(data, function (key, item) {
            $('<li>', { class: 'list-group-item', text: formatProduct(item) }).appendTo($('#products'));
        });
        });
});

function formatCategory(item) {
    return `Id: ${item.categoryId}: ${item.categoryName}`;
}

function formatProduct(item) {
    return `Product Id - ${item.productId}; Product Name - ${item.productName}; Category Name - ${item.categoryName}; Supplier Name - ${item.supplierName}`;
}