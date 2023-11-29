import dxForm from "devextreme/ui/form";

export default {
  init: function(formElement, formItems,  colCount, formData, model) {
      return new dxForm($(formElement), {
        formData: formData ?? {},
        colCount: colCount,
        items: formItems.map(x => { 
            return {
                editorOptions: {
                    showClearButton: true
                },
                validationRules: x.validationRules,
                dataField: typeof x == "object" ? x.dataField : x,
                label: {
                    text: model ? model.captions[x] : x
                }
            }
        }),
    });
  }
}
