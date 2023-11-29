import 'devextreme/dist/css/dx.common.css';
import $ from "jquery";
import "devextreme/bundles/modules/core";
import 'devextreme/integration/jquery';
import DataSource from 'devextreme/data/data_source';
import aspnet from "devextreme-aspnet-data";
import { manufacturerModel, productModel } from '../data';
import Swal from "sweetalert2";
import { datagrid, } from '../components';

let manufacturerForm = null;

function init(selectors) {
    const gridDataSource = new DataSource(aspnet.createStore({
        key: 'id',
        loadUrl: '/api/manufacturer',
    }));

    datagrid.init(selectors.grid, gridDataSource, "qpl-manufacturers", {
        columns: [
            {
                caption: manufacturerModel.captions["name"],
                dataField: "name",
                cellTemplate: function (container, options) {
                    container.html(`<a href="/Manufacturer/${options.data.id}">${options.data.name}</a>`);
                }
            },
            "code",
            {
                caption: manufacturerModel.captions["createdDate"],
                dataField: "createdDate",
                dataType: "date"
            },
            {
                caption: manufacturerModel.captions["updatedDate"],
                dataField: "updatedDate",
                dataType: "date"
            },
            {
                caption: "",
                cellTemplate: function (container, options) {
                    container.html(`<a href="/Manufacturer/${options.data.id}/Edit" class="btn btn-sm btn-outline-success w-100"><i class="fa fa-edit"></i> Edit</a>`);
                }
            }
        ]
    });
}

function initManufacturerForm(formItem, manufacturer) {
    return $(formItem).dxForm({
        formData: manufacturer,
        colCount: 1,
        items: [{
            dataField: 'name',
            label: {
                text: manufacturerModel.captions.name
            },
            editorOptions: {
                showClearButton: true,
                onValueChanged: function (e) {
                    e.component.option('value', e.value.toUpperCase());
                }
            },
            validationRules: [{
                type: "required",
                message: "Manufacturer name is required"
            }]
        },
        {
            dataField: 'code',
            label: {
                text: manufacturerModel.captions.code
            },
            editorOptions: {
                showClearButton: true,
                onValueChanged: function (e) {
                    e.component.option('value', e.value.toUpperCase());
                }
            },
            validationRules: [{
                type: "required",
                message: "Manufacturer code is required"
            }]
        }]
    }).dxForm('instance');
}

function createManufacturer(e) {
    e.preventDefault();

    var result = manufacturerForm.validate();
    if (result.isValid) {
        $.ajax({
            type: "POST",
            data: JSON.stringify(Object.fromEntries(new FormData(e.target).entries())),
            url: `/api/manufacturer`,
            contentType: "application/json; charset=utf-8",
            processData: true,
            cache: false,
            success: function (response) {
                if (response.responseCode == 0) {
                    Swal.fire(
                        'Manufacturer created',
                        '',
                        'success'
                    ).then((result) => {
                        window.location.href = `/Manufacturer/${response.id}/edit`;
                    })
                }
                else {
                    Swal.fire(
                        response.message,
                        "<ul class='text-start'>" + result.brokenRules.map(x => "<li>" + x.message + "</li>").join("") + "</ul>",
                        'error'
                    );
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

function updateManufacturer(e, manufacturerId) {
    e.preventDefault();

    var result = manufacturerForm.validate();
    if (result.isValid) {
        $.ajax({
            type: "PATCH",
            data: JSON.stringify(Object.fromEntries(new FormData(e.target).entries())),
            url: `/api/manufacturer/${manufacturerId}`,
            contentType: "application/json; charset=utf-8",
            processData: true,
            cache: false,
            success: function (response) {
                if (response.responseCode == 0) {
                    Swal.fire(
                        'Manufacturer saved',
                        '',
                        'success'
                    );
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

function deleteManufacturer(e, manufacturerId) {
    e.preventDefault();

    Swal.fire({
        title: "Delete manufacturer",
        icon: 'warning',
        showDenyButton: true,
        denyButtonColor: '#d33',
        confirmButtonColor: '#3085d6',
        denyButtonText: 'Delete Manufacturer',
        confirmButtonText: 'Keep it'
    }).then((result) => {
        if (result.isDenied) {
            $.ajax({
                type: "DELETE",
                url: `/api/manufacturer/${manufacturerId}`,
                contentType: "application/json; charset=utf-8",
                processData: true,
                cache: false,
                success: function (response) {
                    if (response.responseCode == 0) {
                        Swal.fire(
                            'Manufacturer deleted',
                            '',
                            'success'
                        ).then((result) => {
                            window.location.href = `/Manufacturer`;
                        })
                    }
                }
            });
        }
    });
}

function initEditor(selectors, manufacturerId) {
    if (manufacturerId == 0) {
        manufacturerForm = initManufacturerForm(selectors.editFormDx, {});

        $(selectors.editForm).on("submit", createManufacturer);
    }
    else {
        $.ajax({
            method: "GET",
            url: `/api/manufacturer/${manufacturerId}`,
            success: (response) => {
                if (response.responseCode == 0) {
                    manufacturerForm = initManufacturerForm($(selectors.editFormDx), response.manufacturer);

                    $(selectors.editForm).on("submit", function (e) { updateManufacturer(e, manufacturerId); });
                    $(selectors.deleteBtn).on("click", function (e) { deleteManufacturer(e, manufacturerId); });
                }
            }
        });
    }
}

function initDetails(selectors, manufacturerId) {
    const gridDataSource = new DataSource(aspnet.createStore({
        key: 'id',
        loadUrl: `/api/manufacturer/${manufacturerId}/product`,
    }));
    debugger
    datagrid.init(selectors.grid, gridDataSource, "qpl-manufacturer-products", {
        columns: productModel.summaryFields.map(fieldName => fieldName)
    });
}

export default {
    init: init,
    initEditor: initEditor,
    initDetails: initDetails
}