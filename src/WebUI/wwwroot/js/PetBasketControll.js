function addToCart(id) {
    var existingCart = JSON.parse(sessionStorage.getItem("MyCart")) || [];
    if (existingCart.some(function (elem) { return elem === id; })) {
        return;
    } // avoiding duplicates
    existingCart.push(id);
    sessionStorage.setItem("MyCart", JSON.stringify(existingCart));
    showNumberOfElementsInBasket();
}
function showNumberOfElementsInBasket() {
    // Get the element by ID
    var changeMeElement = document.getElementById("petBasketBadge");
    var existingCart = JSON.parse(sessionStorage.getItem("MyCart")) || [];
    if (existingCart.length == 0) {
        changeMeElement.textContent = "";
    }
    // Set the new content
    else {
        changeMeElement.textContent = existingCart.length.toString();
    }
}
document.addEventListener("DOMContentLoaded", showNumberOfElementsInBasket);
//# sourceMappingURL=PetBasketControll.js.map