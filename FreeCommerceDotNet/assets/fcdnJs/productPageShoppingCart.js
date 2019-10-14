// Global Variable to hold productCount in Product Page

window.productCount = 1;

// Add To Card Button

$(document).on('click', '.productPageAddToCard', function () {
    console.log("Add Product");
    $('#productAddToCardModalImageSection').empty();
    $('#productAddToCardModalBodySection').empty();


    var productImageHtml =
        '<a href="#">' +
        '<img class="img-fluid blur-up lazyload pro-img" src = ' +
        window.productImageUrl +
        ' alt = "" >' +
        '</a>';

    var productDescriptionSection =
        '<a href="#"><h6><i class="fa fa-check" ></i>Product<span> ' +
        window.productName +
        ' </span><span> successfully added to your Cart</span></h6 ></a>';


    var cartItem = {
        productId: window.productId,
        productName: window.productName,
        productCount: window.productCount,
        productPrice: window.productPrice,
        productImageUrl: window.productImageUrl
    };

    var cartItems = [];
    cartItems = getCookie('cartListCookie');
    if (cartItems == null) {
        cartItems = [];
        cartItems.push(cartItem);
        setCookie('cartListCookie', JSON.stringify(cartItems), 90);

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
                '<a href="#"><h6><i class="fa fa-exclamation" ></i>Product<span> ' +
                window.productName +
                ' </span><span> has been added to your Cart.Therefore,Incremented count.</span></h6 ></a>';
            if (window.productCount == cartItems[idx].productCount) {
                productDescriptionSection =
                    '<a href="#"><h6><i class="fa fa-exclamation" ></i>Product<span> ' +
                    window.productName +
                    ' </span><span> has been added to your Cart as much as same count .</span></h6 ></a>';
            } else {
                productDescriptionSection =
                    '<a href="#"><h6><i class="fa fa-exclamation" ></i>Product<span> ' +
                    window.productName +
                    ' </span><span> has been added to your Cart.Product Count Changed</span></h6 ></a>';
                cartItems[idx].productCount = window.productCount;
            }
            console.log(window.productCount);
            //cartItems[idx].productCount++;
        }
    }


    $('#productAddToCardModalImageSection').append(productImageHtml);
    $('#productAddToCardModalBodySection').append(productDescriptionSection);

    setCookie('cartListCookie', JSON.stringify(cartItems), 90);

    //Update Layout Price Total
    updateLayoutCartSubTotal();

    //Add Product Section

});

// Add To Cart Minus
$(document).on('click', '.quantity-left-minus', function () {

    if (checkIsExist(window.productId)) {
        if ((window.productCount - 1) >= 0) {
            window.productCount--;
            if (window.productCount == 0) {
                // Sil
                console.log("Cartta mevcut ürünün miktarı azaltıldı ve 0 olduğu için silinecek");
                removeFromCart(window.productId);
                location.reload();
            } else {
                // Güncelle
                updateProductCountInList(window.productId, window.productCount);
                updateLayoutCartSubTotal();
            }
        }
    } else {
        if ((window.productCount - 1) > 0) {
            // Ürün yok sadece miktarı güncelle
            window.productCount--;

        }
    }


});

// Add To Cart Plus

$(document).on('click', '.quantity-right-plus', function () {
    if (checkIsExist(window.productId)) {
        if ((window.productCount + 1) <= 999) {
            window.productCount++;
            // Güncelle
            updateProductCountInList(window.productId, window.productCount);
            updateLayoutCartSubTotal();
        }
    } else {
        if ((window.productCount + 1) <= 999) {
            // Ürün yok sadece miktarı güncelle
            window.productCount++;
            console.log("ürün miktarı arttı");
        }
    }


});

// Cart Page Update Quantity

$(document).on('input', '.cartPageQuantityInput', function () {
    var inputElementId = this.id;
    var productId = inputElementId.replace('productcountbyid_', '');
    var productNewCount = this.value;
    console.log(productNewCount === '');
    if (productNewCount === '') {
        productNewCount = 0;
        removeFromCart(productId);
        location.reload();
        console.log('Product Id ' + productId + ' Silindi');
    }
    else if (productNewCount == 0) {
        removeFromCart(productId);
        console.log('Product Id ' + productId + ' Silindi');
        location.reload();
    } else {
        changeProductCount(productId, productNewCount);
    }
    console.log('Product Id ' + productId + ' New Count ' + productNewCount);

});

// Cart Page Remove Button

$(document).on('click', '.cartPageRemoveProduct', function () {
    var inputElementId = this.id;
    var productId = inputElementId.replace('productremovebyid_', '');
    removeFromCart(productId);
    console.log('Product Id ' + productId + ' Silindi');
    location.reload();

});

$('input[name=quantity]').on('input', function () {
    console.log('changed');
});

// Get Current Items

var getCartItems = function () {
    return JSON.parse(getCookie("cartListCookie"));
};

// Remove Item From List

var removeFromCart = function (productId) {
    var cartItems = getCartItems();
    const idx = cartItems.findIndex(x => x.productId == productId);
    if (idx !== undefined) cartItems.splice(idx, 1);
    setCookie('cartListCookie', JSON.stringify(cartItems), 90);
    updateCartTotalPrice();

};

// Change Product Count In Cart page

var changeProductCount = function (productId, productNewCount) {
    updateProductCountInList(productId, productNewCount);

    var price = $('#mobileProductPriceById_' + productId).html().replace('₺', '').replace(' ', '');
    var totalPrice = price * productNewCount;

    $('#totalProductPriceById_' + productId).html(totalPrice + ' ₺');
    updateCartTotalPrice();
    updateLayoutCartSubTotal();

};

// Change Product Count In List

var updateProductCountInList = function (productId, productNewCount) {
    var cartItems = getCartItems();
    const idx = cartItems.findIndex(x => x.productId == productId);
    if (idx !== undefined) {
        cartItems[idx].productCount = productNewCount;
    };


    setCookie('cartListCookie', JSON.stringify(cartItems), 90);
};

// Update Cart Page Total Price

var updateCartTotalPrice = function () {

    var itemsInCart = getCartItems();
    window.gelenUrunler = itemsInCart;

    var genelToplam = 0;

    for (var i = 0; i < itemsInCart.length; i++) {
        var item = itemsInCart[i];
        var productCount = item.productCount;
        var productPrice = item.productPrice;
        genelToplam += productPrice * productCount;
    }

    if (genelToplam == undefined) {
        genelToplam = 0;
    }
    $('#totalPriceH2').html(genelToplam + ' ₺');

};

// Update Layout Page Total Price

var updateLayoutCartSubTotal = function () {
    var subtotalTextLayout = $('#cartSubTotalText');
    var Items = getCartItems();
    var subtotal = 0.0;
    for (var i = 0; i < Items.length; i++) {
        subtotal += Items[i].productCount * Items[i].productPrice;
    }
    subtotalTextLayout.html(subtotal + ' ₺');
}

// Fill Current Items In Cart

var fillProductsInCart = function () {
    var tbody = $('#cartItemsTbody');
    var itemsInCart = getCartItems();
    window.gelenUrunler = itemsInCart;

    var genelToplam = 0;

    if (itemsInCart==null || itemsInCart.length == 0) {
        tbody.append('<tr><td colspan="6">Sepet Boş</td></tr>');
    }

    for (var i = 0; i < itemsInCart.length; i++) {
        var item = itemsInCart[i];
        var productId = item.productId;
        var productName = item.productName;
        var productCount = item.productCount;
        var productImageUrl = item.productImageUrl;
        var productPrice = item.productPrice;

        var totalPrice = productPrice * productCount;
        genelToplam += totalPrice;
        tbody.append(' <tr>' +
            '<td><a href="#"><img src=' + productImageUrl + ' alt=""></a></td>' +
            '<td><a href="#">' + productName + '</a>' +
            '<div class="mobile-cart-content row"><div class="col-xs-3">' +
            '<div class="qty-box"><div class="input-group">' +
            '<input type="text" name="quantity" class="form-control input-number" value="' + productCount + '">' +
            '</div>' +
            '</div>' +
            '</div>' +
            '<div class="col-xs-3">' +
            '<h2 class="td-color" id="mobileProductPriceById_' + productId + '">' + productPrice + '₺</h2>' +
            '</div>' +
            '<div class="col-xs-3">' +
            '<h2 class="td-color">' +
            '<a href="#" class="icon">' +
            '<i class="ti-close">' +
            '</i>' +
            '</a>' +
            '</h2>' +
            '</div>' +
            '</div>' +
            '</td>' +
            '<td><h2 id="ProductPriceById_' + productId + '">' + productPrice + ' ₺</h2>' +
            '</td>' +
            '<td>' +
            '<div class="qty-box">' +
            '<div class="input-group">' +
            '<input type="number" id="productcountbyid_' + productId + '" name="quantity" class="form-control input-number cartPageQuantityInput" value="' + productCount + '">' +
            '</div></div></td>' +
            '<td><h2 class="td-color" id="totalProductPriceById_' + productId + '">' + totalPrice + ' ₺</h2></td>' +
            '<td><a href="#" class="icon cartPageRemoveProduct" id="productremovebyid_' + productId + '"><i class="ti-close"></i></a></td>' +
            '</tr>');
    }
    console.log(totalPrice);
    if (genelToplam == undefined) {
        genelToplam = 0;
    }
    $('#totalPriceH2').html(genelToplam + ' ₺');


};

// Load Product Cart Item If Exist in Product Page

var LoadIfExists = function (value) {
    var cartItems = getCartItems();
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
    if (isExist == true) {
        var cartItem = cartItems[idx];
        $('#productQuantityInput').val(cartItem.productCount);
        window.productCount = cartItem.productCount;
        return cartItem;
    }

};

// Check is item exists

var checkIsExist = function (value) {
    var cartItems = getCartItems();
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
    return isExist;
};