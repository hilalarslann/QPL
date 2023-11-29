import 'devextreme/dist/css/dx.common.css';
import $ from "jquery";
import "devextreme/bundles/modules/core";
import 'devextreme/integration/jquery';
import DataSource from 'devextreme/data/data_source';
import aspnet from "devextreme-aspnet-data";
import {productDefinitionModel} from '../data';
import {datagrid, productDefinitionSummary, form} from '../components';

function init(selectors) {
    const gridDataSource = new DataSource(aspnet.createStore({
        key: 'id',
        loadUrl: '/api/detailedSearch',
    }));
    
    datagrid.init(selectors.grid, gridDataSource, "qpl-detailed-search", {
        columns: [
            {
                caption: productDefinitionModel.captions["CPC"],
                dataField: "CPC", 
                cellTemplate : function (container, options) {
                    // container.html(`<a href="/ProductDetail/${options.data.id}">${options.data.CPC}</a>`);
                    container.html(`<a href="/ProductDefinition/${options.data.id}">${options.data.CPC}</a>`);
                }
            },
            "engineeringCode",
            "concept",
            "specNo",
            "manufacturerCode",
            "nortelCpc",
            "roshStatus",
            "packageType",
            "description",
            "comments",
            "radiassionRating",
        ],
        onRowClick: function(e) {
            if (e.rowType === "data")
                productDefinitionSummary.display(e.data.id);
        }
    });

    initScForm(selectors.editFormDx);

    $(selectors.editForm).on("submit", function(e) {
        e.preventDefault(); 
        filterDxGrid(e, gridDataSource);
    });
}

function filterDxGrid(e, gridDataSource) {
    gridDataSource.filter(datagrid.createFilterExpression(Object.fromEntries(new FormData(e.target).entries())));
    gridDataSource.load();
}
  
function initScForm(formElement) {
    return form.init(formElement, [
        'CPC','engineeringCode', 'concept','specNo','manufacturerCode','nortelCpc','rohsStatus','packageType','description','comments','radiassionRating',
    ], 3, {}, productDefinitionModel);
}

export default {
    init: init,
    initScForm: initScForm
};