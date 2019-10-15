$(document).on('click', '.productCompareButton', function () {

    var productId = this.id.replace("compareProductById_", "");

    var compareItems = [];
    var compareItem = {
        productId: productId
    };

    $('#categoryPageCompareBody').empty();
    var description = '';
    compareItems = getCompareItemCookie("compareList");
    if (compareItems === null) {
        compareItems = [];

        compareItems.push(compareItem);
        description =
            '<div class="container-fluid text-center"><div class="row text-center"><h3><span class="badge badge-success">Ürün Karşılaştırma listesine eklendi</span></h3></div></div>';

    } else {
        if (checkIsExist(productId)) {
            description =
                '<div class="container-fluid text-center"><div class="row text-center"><h3><span class="badge badge-info">Ürün zaten karşılaştırma listesinde</span></h3></div></div>';

        } else {
            if (compareItems.length < 4) {
                description =
                    '<div class="container-fluid text-center"><div class="row text-center"><h3><span class="badge badge-success">Ürün Karşılaştırma listesine eklendi</span></h3></div></div>';
                compareItems.push(compareItem);
            } else {
                description =
                    '<div class="container-fluid text-center"><div class="row text-center"><h3><span class="badge badge-danger">Karşılaştırma listesi dolu</span></h3></div></div>';
            }
        }
    }
    $('#categoryPageCompareBody').append(description);
    $('#categoryPageCompareModal').modal("show");
    setCompareItemCookie(compareItems);


});

var setCompareItemCookie = function (list) {
    setCookie("compareList", JSON.stringify(list), 15);
};
var getCompareItemCookie = function() {
    return JSON.parse(getCookie("compareList"));
};
var removeItemFromCookie = function (value) {
    console.log("Remove " + value);
    var Items = getCompareItemCookie();
    const idx = Items.findIndex(x => x.productId == value);
    if (idx !== undefined) Items.splice(idx, 1);
    setCompareItemCookie(Items);
    location.href = location.protocol+'//'+location.host+'/Home/ProductCompare';
};
var checkIsExist = function (value) {
    var Items = getCompareItemCookie();
    var isExist = false;
    for (var i = 0; i < Items.length; i++) {
        if (Items[i].productId === value) {
            isExist = true;
            break;
        }
    }
    return isExist;
}