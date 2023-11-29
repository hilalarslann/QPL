function createProductDefinition(productDefiniton) {
    var result = productDefinitionForm.validate();
    if (result.isValid) {
        $.ajax({
            type: "POST",
            data: JSON.stringify(productDefiniton),
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
}