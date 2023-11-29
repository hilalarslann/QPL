import "devextreme/bundles/modules/core";
import "devextreme/ui/data_grid";
import 'devextreme/integration/jquery';
import Swal from "sweetalert2";
import aspnet from "devextreme-aspnet-data";
import { productModel } from '../data';

let productForm = null;
let productDefinitionId = null;
var checkVisible = false;

function init() {
    // productDefinitionId = model.id
    // $.ajax({
    //     method: "GET",
    //     url: `/api/productDefinition/${model.id}`,
    //     success: (response) => {
    //         if (response.responseCode == 0) {
    //             // var tableBody = $('#productDetailTable tbody');
    //             // var row =
    //             //     '<tr>' +
    //             //     '<td>' + 'CPC' + '</td>' +
    //             //     '<td>' + response.product.CPC + '</td>' +
    //             //     '</tr>' +
    //             //     '<tr>' +
    //             //     '<td>' + 'Nortel CPC' + '</td>' +
    //             //     '<td>' + response.product.nortelCpc + '</td>' +
    //             //     '</tr>' +
    //             //     '<tr>' +
    //             //     '<td>' + 'Entry Date' + '</td>' +
    //             //     '<td>' + response.product.createdDate + '</td>' +
    //             //     '</tr>' +
    //             //     '<tr>' +
    //             //     '<td>' + 'Modify Date' + '</td>' +
    //             //     '<td>' + response.product.updatedDate + '</td>' +
    //             //     '</tr>' +
    //             //     '<tr>' +
    //             //     '<td>' + 'ROHS Status' + '</td>' +
    //             //     '<td>' + response.product.roshStatus + '</td>' +
    //             //     '</tr>' +
    //             //     '<tr>' +
    //             //     '<td>' + 'Flammability Rating' + '</td>' +
    //             //     '<td>' + response.product.flammabilityRating + '</td>' +
    //             //     '</tr>' +
    //             //     '<tr>' +
    //             //     '<td>' + 'Radiassion Rating' + '</td>' +
    //             //     '<td>' + response.product.radiassionRating + '</td>' +
    //             //     '</tr>' +
    //             //     '<tr>' +
    //             //     '<td>' + 'Disassembled Or Reusable' + '</td>' +
    //             //     '<td>' + response.product.disassembledOrReusable + '</td>' +
    //             //     '</tr>' +
    //             //     '<tr>' +
    //             //     '<td>' + 'Package Type' + '</td>' +
    //             //     '<td>' + response.product.packageType + '</td>' +
    //             //     '</tr>' +
    //             //     '<tr>' +
    //             //     '<td>' + 'Concept' + '</td>' +
    //             //     '<td>' + response.product.concept + '</td>' +
    //             //     '</tr>' +
    //             //     '<tr>' +
    //             //     '<td>' + 'Description' + '</td>' +
    //             //     '<td>' + response.product.description + '</td>' +
    //             //     '</tr>' +
    //             //     '<tr>' +
    //             //     '<td>' + 'Comments' + '</td>' +
    //             //     '<td>' + response.product.comments + '</td>' +
    //             //     '</tr>';
    //             // tableBody.append(row);

    //             $.each(response.productDef, function(propertyName, productDefinition) {
    //                 var row =
    //                 '<tr>' +
    //                     '<td>' + propertyName + '</td>' +
    //                     '<td>' + productDefinition + '</td>' +
    //                 '</tr>';
    //                     tableBody.append(row);
    //             });
    //         }
    //     }
    // });
    const gridDataSource = new DataSource(aspnet.createStore());
    // datagrid($(selectors.grid), aspnet.createStore({
    //     key: "CPC",
    //     loadUrl: `/api/productDetail/product/${model.id}`,
    // }), "qpl-productDetails");
}

function initEditor(selectors, model) {
    if (model.id == 0) {
        $.ajax({
            method: "GET",
            url: `/api/productDefinition/${model.productDefinitionId}`,
            success: (response) => {
                if (response.responseCode == 0) {
                    var product = {
                        CPC: response.productDefinition.CPC,
                        entryDate: new Date().toLocaleDateString()
                    }

                    checkVisible = false;
                    productForm = initProductForm(selectors.editFormDx, product);
                    productDefinitionId = model.productDefinitionId;
                    $(selectors.editForm).on("submit", createProduct);
                }
            }
        })
    }
    else {
        checkVisible = true;
        $.ajax({
            method: "GET",
            url: `/api/product/${model.id}`,
            success: (response) => {
                if (response.responseCode == 0) {
                    productForm = initProductForm($(selectors.editFormDx), response.product);
                    $(selectors.editForm).on("submit", function (e) { updateProduct(e, model.id); });
                    $(selectors.deleteBtn).on("click", function (e) { deleteProduct(e, model); });
                }
            }
        });
    }
}

function initProductForm(formItem, product) {
    const engineeringStatus = [
        'Approved',
        'Disqualified',
        'Not For New Designs',
        'Obsolete',
        'Trial'
    ];
    return $(formItem).dxForm({
        formData: product,
        colCount: 2,
        items: [{
            dataField: 'CPC',
            label: {
                text: productModel.captions.CPC
            },
            editorOptions: {

                disabled: true,
                showClearButton: true,
                value: product.CPC
            },
            validationRules: [{
                type: "required",
                message: "Manufacturer name is required"
            }],
        },
        {
            dataField: 'manufacturerId',
            editorType: 'dxSelectBox',
            editorOptions: {
                dataSource: aspnet.createStore({
                    key: "id",
                    loadUrl: "/api/manufacturer"
                }),
                searchEnabled: true,
                valueExpr: 'id',
                displayExpr: 'name',
                value: product.manufacturerId,
            },
            label: {
                text: "Manufacturer Name"
            },
            validationRules: [{
                type: "required",
                message: "Manufacturer Name is required"
            }],
        },
        {
            dataField: 'engineeringStatus',
            editorType: 'dxSelectBox',
            editorOptions: {
                items: engineeringStatus,
                searchEnabled: true,
                value: product.engineeringStatus,
            },
            label: {
                text: productModel.captions.engineeringStatus
            },
            validationRules: [{
                type: "required",
                message: "Engineering Status is required"
            }],
        },
        {
            dataField: 'manufacturerCode',
            label: {
                template: productModel.captions.manufacturerCode
            },
            editorOptions: {
                showClearButton: true,
                onValueChanged: function (e) {
                    e.component.option('value', e.value.toUpperCase());
                },
            },
            validationRules: [{
                type: "required",
                message: "Manufacturer Code is required"
            }],
        },
        {
            dataField: 'previousEngineeringStatus',
            editorOptions: {
                disabled: true,
                width: '100%',
                value: product.prevEngineeringStatus
            },
            label: {
                text: productModel.captions.prevEngineeringStatus
            },
            visible: checkVisible,
        },
        {
            dataField: 'entryDate',
            editorOptions: {
                disabled: true,
                width: '100%',
            },
            label: {
                text: productModel.captions.entryDate
            },
            editorType: 'dxDateBox',
        },
        {
            dataField: 'modifyDate',
            editorOptions: {
                disabled: true,
                width: '100%',
            },
            label: {
                text: 'Modify Date'
            },
            editorType: 'dxDateBox',
            visible: checkVisible
        },
        ]
    }).dxForm('instance');
}

function createProduct(e) {
    e.preventDefault();
    debugger
    var result = productForm.validate();
    if (result.isValid) {
        $.ajax({
            type: "POST",
            data: JSON.stringify(Object.fromEntries(new FormData(e.target).entries())),
            url: `/api/product/${productDefinitionId}`,
            contentType: "application/json; charset=utf-8",
            processData: true,
            cache: false,
            success: function (response) {
                if (response.responseCode == 0) {
                    Swal.fire(
                        'Product created',
                        '',
                        'success'
                    ).then((result) => {
                        window.location.href = `/productDefinition/${productDefinitionId}/edit`;
                    })
                }
            }
        });
    }
}

function updateProduct(e, id) {
    e.preventDefault();
    var result = productForm.validate();
    if (result.isValid) {
        $.ajax({
            type: "PATCH",
            data: JSON.stringify(Object.fromEntries(new FormData(e.target).entries())),
            url: `/api/product/${id}`,
            contentType: "application/json; charset=utf-8",
            processData: true,
            cache: false,
            success: function (response) {
                if (response.responseCode == 0) {
                    Swal.fire(
                        'Product saved',
                        '',
                        'success'
                    ).then(function () {
                        window.location.reload()
                    });
                }
            }
        });
    }
    else {
        Swal.fire(
            'Invalid form',
            "<ul class='text-start'>" + result.brokenRules.map(x => "<li>" + x.message + "</li>").join("") + "</ul>",
            'error'
        );
    }
}

function deleteProduct(e, model) {
    e.preventDefault();

    Swal.fire({
        title: "Delete product",
        icon: 'warning',
        showDenyButton: true,
        denyButtonColor: '#d33',
        confirmButtonColor: '#3085d6',
        denyButtonText: 'Delete Product',
        confirmButtonText: 'Keep it'
    }).then((result) => {
        if (result.isDenied) {
            $.ajax({
                type: "DELETE",
                url: `/api/product/${model.id}`,
                contentType: "application/json; charset=utf-8",
                processData: true,
                cache: false,
                success: function (response) {
                    if (response.responseCode == 0) {
                        Swal.fire(
                            'Product deleted',
                            '',
                            'success'
                        ).then((result) => {
                            window.location.href = `/ProductDefinition/${model.productDefinitionId}/Edit`;
                        })
                    }
                }
            });
        }
    });
}






// function initDetails(selectors, productDefinitionId) {
//     const gridDataSource = new DataSource(aspnet.createStore({
//         key: 'id',
//         loadUrl: `/api/product/${productDefinitionId}/product`,
//     }));

//     datagrid.init(selectors.grid, gridDataSource, "qpl-product-details", {});
// }


// function createProduct(e) {
//     e.preventDefault(); // avoid to execute the actual submit of the form.
//     $.ajax({
//         type: "POST",
//         data: JSON.stringify(Object.fromEntries(new FormData(e.target).entries())), //I assume getIDs returns an array of integers
//         url: `/api/productDefinition/${productDefinitionId}/Product`,
//         contentType: "application/json; charset=utf-8",
//         processData: true,
//         cache: false,
//         success: function (response) {
//             if (response.responseCode == 0) {   
//                 window.location.href = `/productDefinition/Edit/${productDefinitionId}`;
//             }
//         }
//     });
// }
export default {
    initEditor: initEditor,
    init: init
}