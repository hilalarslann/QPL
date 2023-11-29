import 'devextreme/dist/css/dx.common.css';
import $ from "jquery";
import "devextreme/bundles/modules/core";
import 'devextreme/integration/jquery';
import DataSource from 'devextreme/data/data_source';
import aspnet from "devextreme-aspnet-data";
import Swal from "sweetalert2";
import { productDefinitionModel, productModel } from '../data';
import { datagrid, productDefinitionSummary, form } from '../components';



let productDefinitionForm = null;

function init(selectors) {
    const gridDataSource = new DataSource(aspnet.createStore({
        key: 'id',
        loadUrl: '/api/productDefinition',
    }));

    datagrid.init(selectors.grid, gridDataSource, "qpl-product-definitions", {
        columns: [
            {
                caption: productDefinitionModel.captions["CPC"],
                dataField: "CPC",
                cellTemplate: function (container, options) {
                    container.html(`<a href="/ProductDefinition/${options.data.id}">${options.data.CPC}</a>`);
                }
            },
            "nortelCpc",
            "engineeringCode",
            "specNo",
            "description",
            "comments",
            "concept",
            "packageType",
            "roshStatus",
            "flammabilityRating",
            "radiassionRating",
            "disassembledOrReusable",
            {
                caption: productDefinitionModel.captions["createdDate"],
                dataField: "createdDate",
                dataType: "date"
            },
            {
                caption: productDefinitionModel.captions["updatedDate"],
                dataField: "updatedDate",
                dataType: "date"
            },
            {
                caption: "",
                cellTemplate: function (container, options) {
                    container.html(`<a href="/ProductDefinition/${options.data.id}/Edit" class="btn btn-sm btn-outline-success w-100"><i class="fa fa-edit"></i> Edit</a>`);
                }
            },
        ],
        onRowClick: function (e) {
            if (e.rowType === "data")
                productDefinitionSummary.display(e.data.id);
        }
    });
}

function initProductDefinitionForm(formItem, productDefinition) {
    return $(formItem).dxForm({
        formData: productDefinition,
        colCount: 3,
        items: [
            {
                dataField: 'CPC',
                editorOptions: {
                    showClearButton: true,
                    maxLength: 8,
                    onValueChanged: function (e) {
                        e.component.option('value', e.value.toUpperCase());
                    }
                },
                validationRules: [{
                    type: "required",
                    message: "CPC is required"
                },
                {
                    type: "stringLength",
                    max: 8,
                    message: "Max 8 character"
                }]
            }, {
                dataField: 'nortelCpc',
                editorOptions: {
                    showClearButton: true,
                    onValueChanged: function (e) {
                        e.component.option('value', e.value.toUpperCase());
                    }
                },
            }, {
                dataField: 'engineeringCode',
                editorOptions: {
                    showClearButton: true
                },
            }, {
                dataField: 'concept',
                editorOptions: {
                    showClearButton: true,
                    onValueChanged: function (e) {
                        e.component.option('value', e.value.toUpperCase());
                    }
                },
            }, {
                dataField: 'specNo',
                editorOptions: {
                    showClearButton: true,
                    onValueChanged: function (e) {
                        e.component.option('value', e.value.toUpperCase());
                    }
                },
            }, {
                dataField: 'description',
                editorOptions: {
                    showClearButton: true,
                    onValueChanged: function (e) {
                        e.component.option('value', e.value.toUpperCase());
                    }
                },
            }, {
                dataField: 'comments',
                editorOptions: {
                    showClearButton: true,
                    onValueChanged: function (e) {
                        e.component.option('value', e.value.toUpperCase());
                    }
                },
            }, {
                dataField: 'createdDate',
                editorOptions: {
                    disabled: true,
                    width: '100%',
                },
                editorType: 'dxDateBox',
            }, {
                dataField: 'updatedDate',
                editorOptions: {
                    disabled: true,
                    width: '100%',
                },
                editorType: 'dxDateBox',
            }, {
                dataField: 'packageType',
                editorOptions: {
                    showClearButton: true,
                    onValueChanged: function (e) {
                        e.component.option('value', e.value.toUpperCase());
                    }
                },
            }, {
                dataField: 'roshStatus',
                editorOptions: {
                    showClearButton: true,
                    onValueChanged: function (e) {
                        e.component.option('value', e.value.toUpperCase());
                    }
                },
            }, {
                dataField: 'flammabilityRating',
                editorOptions: {
                    showClearButton: true,
                    onValueChanged: function (e) {
                        e.component.option('value', e.value.toUpperCase());
                    }
                },
            }, {
                dataField: 'radiassionRating',
                editorOptions: {
                    showClearButton: true,
                    onValueChanged: function (e) {
                        e.component.option('value', e.value.toUpperCase());
                    }
                },
            }, {
                dataField: 'disassembledOrReusable',
                editorType: 'dxCheckBox',
            }]
    }).dxForm('instance');
}

function createProductDefinition(e) {
    e.preventDefault();
    debugger
    var result = productDefinitionForm.validate();
    if (result.isValid) {
        $.ajax({
            type: "POST",
            data: JSON.stringify(Object.fromEntries(new FormData(e.target).entries())),
            url: `/api/productDefinition`,
            contentType: "application/json; charset=utf-8",
            processData: true,
            cache: false,
            success: function (response) {
                if (response.responseCode == 0) {
                    Swal.fire(
                        'Product Definition created',
                        '',
                        'success'
                    ).then((result) => {
                        window.location.href = `/ProductDefinition/${response.id}/edit`;
                    })
                }
                else {
                    Swal.fire(
                        response.message,
                        "<ul class='text-start'>" + result.brokenRules.map(x => "<li>" + x.message + "</li>").join("") + "</ul>",
                        'error'
                    );
                    debugger
                    var a = response.message;
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

function updateProductDefinition(e, productDefinitionId) {
    e.preventDefault();

    var result = productDefinitionForm.validate();
    if (result.isValid) {
        $.ajax({
            type: "PATCH",
            data: JSON.stringify(Object.fromEntries(new FormData(e.target).entries())),
            url: `/api/productDefinition/${productDefinitionId}`,
            contentType: "application/json; charset=utf-8",
            processData: true,
            cache: false,
            success: function (response) {
                if (response.responseCode == 0) {
                    Swal.fire(
                        'Product Definition saved',
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

function deleteProductDefinition(e, productDefinitionId) {
    e.preventDefault();

    Swal.fire({
        title: "Delete productDefinition",
        icon: 'warning',
        showDenyButton: true,
        denyButtonColor: '#d33',
        confirmButtonColor: '#3085d6',
        denyButtonText: 'Delete ProductDefinition',
        confirmButtonText: 'Keep it'
    }).then((result) => {
        if (result.isDenied) {
            $.ajax({
                type: "DELETE",
                url: `/api/productDefinition/${productDefinitionId}`,
                contentType: "application/json; charset=utf-8",
                processData: true,
                cache: false,
                success: function (response) {
                    if (response.responseCode == 0) {
                        Swal.fire(
                            'Product Definition deleted',
                            '',
                            'success'
                        ).then((result) => {
                            window.location.href = `/ProductDefinition`;
                        })
                    }
                }
            });
        }
    });
}

function initEditor(selectors, productDefinitionId) {
    if (productDefinitionId == 0) {
        productDefinitionForm = initProductDefinitionForm(selectors.editFormDx, {});

        $(selectors.editForm).on("submit", createProductDefinition);
    }
    else {
        $.ajax({
            method: "GET",
            url: `/api/productDefinition/${productDefinitionId}`,
            success: (response) => {
                if (response.responseCode == 0) {
                    productDefinitionForm = initProductDefinitionForm(selectors.editFormDx, response.productDefinition);

                    $(selectors.editForm).on("submit", function (e) { updateProductDefinition(e, productDefinitionId); });
                    $(selectors.deleteBtn).on("click", function (e) { deleteProductDefinition(e, productDefinitionId); });
                }
            }
        });

        const gridDataSource = new DataSource(aspnet.createStore({
            key: 'id',
            loadUrl: `/api/productDefinition/${productDefinitionId}/product`,
        }));
        datagrid.init(selectors.grid, gridDataSource, "qpl-productDefinition-products", {
            columns: [
                "CPC",
                "engineeringCode",
                "manufacturerName",
                "manufacturerCode",
                "previousEngineeringStatus",
                "currentEngineeringStatus",
                "createdDate",
                "updatedDate",
                {
                    caption: "",
                    cellTemplate: function (container, options) {
                        container.html(`<a href="/Product/${options.data.id}/Edit/${productDefinitionId}" class="btn btn-sm btn-outline-success w-100"><i class="fa fa-edit"></i> Edit</a>`);
                    }
                }
            ],
        });
    }
}

function initDetails(selectors, productDefinitionId) {
    const gridDataSource = new DataSource(aspnet.createStore({
        key: 'id',
        loadUrl: `/api/productDefinition/${productDefinitionId}/product`,
    }));

    datagrid.init(selectors.grid, gridDataSource, "qpl-productDefinition-products", {
        columns: productModel.summaryFields.map(fieldName => fieldName)
    });
}

export default {
    init,
    initEditor,
    initDetails,
    initProductEditor: 1
}