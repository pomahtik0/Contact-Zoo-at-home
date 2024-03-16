function addToCart(id: string) {
    const existingCart: string[] = JSON.parse(sessionStorage.getItem("MyCart")) || [];
    if (existingCart.some(elem => elem === id)) { return; } // avoiding duplicates
    existingCart.push(id);
    sessionStorage.setItem("MyCart", JSON.stringify(existingCart));
    showNumberOfElementsInBasket();
}
function showNumberOfElementsInBasket() {
    // Get the element by ID
    const changeMeElement = document.getElementById("petBasketBadge");
    if (changeMeElement == null) { return; }
    const existingCart: string[] = JSON.parse(sessionStorage.getItem("MyCart")) || [];
    if (existingCart.length == 0) {
        changeMeElement.textContent = "";
    }
    // Set the new content
    else {
        changeMeElement.textContent = existingCart.length.toString();
    }
}

function clearBasket() {
    sessionStorage.removeItem("MyCart");
}
document.addEventListener("DOMContentLoaded", showNumberOfElementsInBasket)