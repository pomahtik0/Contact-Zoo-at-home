function addToCart(id) {
    var existingCart = JSON.parse(sessionStorage.getItem("MyCart")) || [];
    if (existingCart.some(function (elem) { return elem === id; })) {
        return;
    } // avoiding duplicates
    existingCart.push(id);
    sessionStorage.setItem("MyCart", JSON.stringify(existingCart));
}
//# sourceMappingURL=PetBasketControll.js.map