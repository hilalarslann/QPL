import Swal from "sweetalert2";
import {productDefinitionModel} from "../data";

export default {
  display: function(id) {
    Swal.fire("Loading summary");
    Swal.showLoading();
    
    $.ajax({
      type: "GET",
      url: `/api/productDefinition/${id}`,
      contentType: "application/json; charset=utf-8",
      cache: true,
      success: function(response) {
        let list = productDefinitionModel.summaryFields.map(x => {
          return (response.productDefinition[x]) ? `<li class="list-group-item"><div class="row"><div class="col-6 text-start fw-bold">${productDefinitionModel.captions[x]}</div><div class="col-6 text-start">: ${response.productDefinition[x]}</div></div></li>` : "";
        }).join("");
    
        Swal.fire({
          title: "Product Definition",
          html: `<ul class="list-group">${list}</ul>`,
          icon: 'info',
          showCancelButton: true,
          showDenyButton: true,
          confirmButtonColor: '#3085d6',
          denyButtonColor: '#0fab1a',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Go to details',
          denyButtonText: 'Edit definition'
        }).then((result) => {
          if (result.isConfirmed) {
              window.location.href = "/ProductDefinition/" + response.productDefinition.id
          }
          if (result.isDenied) {
            window.location.href = "/ProductDefinition/" + response.productDefinition.id + "/edit"
          }
        });

        Swal.hideLoading();
      },
      error:function(e) {
        Swal.fire(e);
      },
      complete: function() {
        Swal.hideLoading();
      }
  });
  }  
}