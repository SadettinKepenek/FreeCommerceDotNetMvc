var searchQuery;
function searchProduct(inputField) {
    window.searchQuery = inputField.value;
    console.log(window.searchQuery)
    $("#searchResultTable tbody").empty();
    if (inputField.value === '') {
        $("#searchResultTable tbody").empty();

    }
    else {

        getSearhResults();
    }

}

function getSearhResults() {
    $("#searchResultTable tbody").empty();

    var url = location.protocol + '//' + location.host + "/Home/GetSearchResults";
    $.ajax({
        type: 'GET',
        url: url,
        cache: false,
        data: { query: window.searchQuery },
        dataType: 'json',
        success: function (data) {
            $("#searchResultTable tbody").empty();

            var urlProduct;
            console.log(data.length);
            for (var i = 0; i < data.length; i++) {
                urlProduct = location.protocol + '//' + location.host + "/Home/Product/" + data[i].ProductId;

                var htmlString = '';
                if (i < 4) {
                    htmlString = '<tr><td scope="row" class="align-middle"><img class="img-fluid" src=' + data[i].ImageUrl + ' height="100" width="100"/></td><td class="align-middle"><a href=' + urlProduct + '>' + data[i].ProductName + '</a></td><td class="align-middle">' + data[i].ProductDescription.substring(0, 30) + '</td></tr>';
                    $('#searchResults').append(htmlString);


                }
                else {

                    break;
                }


            }
        }
    });
}
