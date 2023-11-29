import "devextreme/bundles/modules/core";
import "devextreme/ui/data_grid";
import 'devextreme/integration/jquery';
import { saveAs } from 'file-saver';

function createFilterExpression(formData) {
  const filters = [];
  for (const field in formData) {
      if (formData.hasOwnProperty(field) && formData[field]) {
          filters.push([field, 'contains', formData[field]]);
      }
  }
  return filters.length == 0 ? null : filters;
}

export default {
  init: function(gridItem, dataSource, excelFileName, options) {
    return $(gridItem).dxDataGrid({
      columns: options?.columns,
      dataSource: dataSource,
      remoteOperations: true,
      showBorders: true,
      showColumnLines: true,
      showRowLines: false,
      rowAlternationEnabled: true,
      onRowClick: options?.onRowClick,
      paging: {
          pageSize: 10,
      },
      pager: {
          visible: true,
          showPageSizeSelector: true,
          showInfo: true,
          showNavigationButtons: true,
          allowedPageSizes: [10, 25, 50, 100, 'all'],
      },    
      searchPanel: {
          visible: true,
          width: "auto",
          placeholder: 'Search...',
          highlightCaseSensitive: true,
      },
      allowColumnReordering: true,
      filterRow: {
        visible: true,
        applyFilter: 'auto',
      },
      headerFilter: {
        visible: true,
      },
      focusedRowEnabled: false,
      columnChooser: {
        enabled: true,
      },
      columnFixing: {
        enabled: true,
      },
      export: {
        enabled: true
      },
      onExporting(e) {
        const workbook = new ExcelJS.Workbook();
        const worksheet = workbook.addWorksheet(excelFileName);
  
        DevExpress.excelExporter.exportDataGrid({
          component: e.component,
          worksheet,
          autoFilterEnabled: true,
        }).then(() => {
          workbook.xlsx.writeBuffer().then((buffer) => {
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), `${excelFileName}.xlsx`);
          });
        });
        e.cancel = true;
      },
    }).dxDataGrid('instance');
  },
  createFilterExpression: createFilterExpression
}
