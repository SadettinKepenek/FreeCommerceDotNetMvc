window.productCount = 1;

$(document).on('click', '.productPageAddToCard', function () {
    console.log("Add Product");
    $('#productAddToCardModalImageSection').empty();
    $('#productAddToCardModalBodySection').empty();


    var productImageHtml =
        '<a href="#">' +
            '<img class="img-fluid blur-up lazyload pro-img" src = '+window.productImageUrl+' alt = "" >' +
        '</a>';

    var productDescriptionSection =
        '<a href="#"><h6><i class="fa fa-check" ></i>Product<span> '+window.productName+' </span><span> successfully added to your Cart</span></h6 ></a>';


    var cartItem = {
        productId: window.productId,
        productName: window.productName,
        productCount:window.productCount
    };

    var cartItems = [];
    cartItems=getCookie('cartListCookie');
    if (cartItems == null) {
        cartItems = [];
        cartItems.push(cartItem);
        setCookie('cartListCookie', JSON.stringify(cartItems), 1);
        //Cookies.set('cartListCookie', JSON.stringify(cartItems));
    } else {
        cartItems = JSON.parse(cartItems);
        console.log(cartItems);
        var value = cartItem.productId;
        var isExist = false;
        var idx;
        for (var i = 0; i < cartItems.length; i++) {
            console.log(cartItems[i].productId);
            if (cartItems[i].productId == value) {
                isExist = true;
                idx = i;
                break;
            }

        }
        if (isExist === false) {
            cartItems.push(cartItem);
            productDescriptionSection = '<a href="#"><h6><i class="fa fa-check" ></i>Product<span> ' +
                window.productName +
                ' </span><span> successfully added to your Cart</span></h6 ></a>';
        } else {
            productDescriptionSection =
                '<a href="#"><h6><i class="fa fa-exclamation" ></i>Product<span> ' + window.productName + ' </span><span> has been added to your Cart.Therefore,Incremented count.</span></h6 ></a>';
            cartItems[idx].productCount++;
        }
    }


    $('#productAddToCardModalImageSection').append(productImageHtml);
    $('#productAddToCardModalBodySection').append(productDescriptionSection);

    setCookie('cartListCookie', JSON.stringify(cartItems), 1);

});

$(document).on('click', '.quantity-left-minus', function () {
    // Down

    if ((window.productCount - 1) > 0) {
        window.productCount--;
    }

});



$(document).on('click', '.quantity-right-plus', function () {
    if ((window.productCount + 1) <= 999) {
        window.productCount++;
    }

});


$('input[name=quantity]').on('input', function() {
    console.log('changed');
});

var getCartItems = function() {
    return JSON.parse(getCookie("cartListCookie"));
};