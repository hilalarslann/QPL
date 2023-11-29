import "devextreme/bundles/modules/core";
import "devextreme/ui/accordion";
import 'devextreme/integration/jquery';
import 'devextreme/dist/css/dx.common.css';
import $ from "jquery";
import aspnet from "devextreme-aspnet-data";
import {datagrid, productDefinitionSummary, form} from '../components';
import {searchModel} from '../data';
import DataSource from "devextreme/data/data_source";




function init(searchPanel) {
    const gridDataStore = new DataSource(aspnet.createStore({
        key: 'CPC',
        loadUrl: '/api/search',
    }));
    datagrid.init(searchPanel.grid, gridDataStore, "qpl-search",{
        columns: [{
            caption: searchModel.captions["CPC"],
            dataField: "CPC",
            cellTemplate: function (container,options){
                container.html(`<a href="/ProductDefinition/${options.data.id}">${options.data.CPC}</a>`);
            }
        },
        "description",
        "manufacturerName",
        "manufacturerCode",
        "prevEngineeringStatus",
        "engineeringStatus",
        ],
        onRowClick: function(e) {
            if (e.rowType === "data")
                productDefinitionSummary.display(e.data.id);
        }
    });     
        initForm(searchPanel.editFormDx, {});
    $(searchPanel.editForm).on("submit", function(e) {
        e.preventDefault(); 
        filterDxGrid(e, gridDataStore)
    });
}

function initForm(formItem, searchPanel) {
    return $(formItem).dxForm({
        formData: searchPanel,
        colCount:3,
        items: [
            {
                dataField: 'searchField',
                label: {
                    text: 'Arama'
                },
                editorOptions: {
                    placeholder: 'Search...'
                }
            },      
        ],
    }).dxForm("instance");
}


function filterDxGrid(e, gridDataStore) {
    const formData = new FormData(e.target);
    const searchField = formData.get('searchField');
    
    gridDataStore.filter([
        ['CPC', 'contains', searchField],
        'or',
        ['manufacturerName', 'contains', searchField],
        'or',
        ['manufacturerCode', 'contains', searchField],
        'or',
        ['description', 'contains', searchField],
        'or',
        ['prevEngineeringStatus', 'contains', searchField],
        'or',
        ['engineeringStatus', 'contains', searchField]
    ]);
    gridDataStore.load();
}

    function excFilter() {        
        const searchText = document.getElementById("myTextarea").value;
         if(searchText == '')
         {
            var searchPanel = {
                grid:"#qpl-search-datagrid",
                editFormDx:"#search-form #dxForm",
                editForm: "#search-form"
            };
          init(searchPanel);
          return;
         }

        const gridDataStore = new DataSource (aspnet.createStore({
            key: 'CPC',        
            loadUrl: `/api/search/${searchText}/getbyexcelsearch`,
        }));
                
        datagrid.init("#qpl-search-datagrid", gridDataStore, "qpl-search",{
            columns: [{
                caption: searchModel.captions["CPC"],
                dataField: "CPC",
                cellTemplate: function (container,options){
                    container.html(`<a href="/ProductDefinition/${options.data.id}">${options.data.CPC}</a>`)
                }
            },
            "description",
            "manufacturerName",
            "manufacturerCode",
            "prevEngineeringStatus",
            "engineeringStatus",
            {
                caption: "",
                cellTemplate : function (container, options) {
                    container.html(`<a href="/ProductDefinition/${options.data.id}/Edit" class="btn btn-sm btn-outline-success w-100"><i class="fa fa-edit"></i> Edit</a>`);
                }
            }
            ],
            onRowClick: function(e) {
                if (e.rowType === "data")
                    productDefinitionSummary.display(e.data.id);
            }
        });        
    
            initForm("#search-form #dxForm", {});
        $("#search-form").on("submit", function(e) {
            e.preventDefault(); 
            filterDxGrid(e, gridDataStore)
            excFilter(gridDataStore)
    })}; 

export default {
    init: init,
    initForm: initForm,
    excFilter : excFilter
    };
