window.productCount = 1;

$(document).on('click', '.productPageAddToCard', function () {
    console.log("Add Product");
    $('#productAddToCardModalImageSection').empty();
    $('#productAddToCardModalBodySection').empty();


    var productImageHtml =
        '<a href="#">' +
        '<img class="img-fluid blur-up lazyload pro-img" src = ' + window.productImageUrl + ' alt = "" >' +
        '</a>';

    var productDescriptionSection =
        '<a href="#"><h6><i class="fa fa-check" ></i>Product<span> ' + window.productName + ' </span><span> successfully added to your Cart</span></h6 ></a>';


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

var getCartItems = function () {
    return JSON.parse(getCookie("cartListCookie"));
};

var removeFromCart = function (productId) {
    var cartItems = getCartItems();
    const idx = cartItems.findIndex(x => x.productId == productId);
    if (idx !== undefined) cartItems.splice(idx, 1);
    setCookie('cartListCookie', JSON.stringify(cartItems), 1);
    updateCartTotalPrice();
};

var changeProductCount = function(productId, productNewCount) {
    var cartItems = getCartItems();
    const idx = cartItems.findIndex(x => x.productId == productId);
    if (idx !== undefined) {
        cartItems[idx].productCount = productNewCount;
    };

    

    setCookie('cartListCookie', JSON.stringify(cartItems), 1);

    var price = $('#mobileProductPriceById_' + productId).html().replace('₺','').replace(' ','');
    var totalPrice = price * productNewCount;
    
    $('#totalProductPriceById_' + productId).html(totalPrice+' ₺');
    updateCartTotalPrice();

};

var updateCartTotalPrice = function() {

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


var fillProductsInCart = function() {
    var tbody = $('#cartItemsTbody');
    var itemsInCart = getCartItems();
    window.gelenUrunler = itemsInCart;

    var genelToplam = 0;

    if (itemsInCart.length == 0) {
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
            '<td><a href="#">'+productName+'</a>' +
            '<div class="mobile-cart-content row"><div class="col-xs-3">' +
            '<div class="qty-box"><div class="input-group">' +
            '<input type="text" name="quantity" class="form-control input-number" value="'+productCount+'">' +
            '</div>' +
            '</div>' +
            '</div>' +
            '<div class="col-xs-3">' +
            '<h2 class="td-color" id="mobileProductPriceById_'+productId+'">'+productPrice+'₺</h2>' +
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
            '<td><h2 id="ProductPriceById_' + productId +'">' + productPrice +' ₺</h2>' +
            '</td>' +
            '<td>' +
            '<div class="qty-box">' +
            '<div class="input-group">' +
            '<input type="number" id="productcountbyid_'+productId+'" name="quantity" class="form-control input-number cartPageQuantityInput" value="'+productCount+'">' +
            '</div></div></td>' +
            '<td><h2 class="td-color" id="totalProductPriceById_' + productId +'">' + totalPrice+' ₺</h2></td>' +
            '<td><a href="#" class="icon cartPageRemoveProduct" id="productremovebyid_'+productId+'"><i class="ti-close"></i></a></td>' +
            '</tr>');
    }
    if (totalPrice == undefined) {
        totalPrice = 0;
    }
    $('#totalPriceH2').html(totalPrice+' ₺');


};