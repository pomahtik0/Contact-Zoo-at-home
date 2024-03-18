function addToCart(id: string) {
    const existingCart: string[] = JSON.parse(sessionStorage.getItem("MyCart")) || [];
    if (existingCart.some(elem => elem === id)) { return; } // avoiding duplicates
    existingCart.push(id);
    sessionStorage.setItem("MyCart", JSON.stringify(existingCart));
    showNumberOfElementsInBasket();
}
function removeFromBasket(id: string) {
    const existingCart: string[] = JSON.parse(sessionStorage.getItem("MyCart")) || [];

    const index = existingCart.indexOf(id, 0);
    if (index > -1) {
        existingCart.splice(index, 1);
        sessionStorage.setItem("MyCart", JSON.stringify(existingCart)); // remove item from cart
        showNumberOfElementsInBasket();

        const htmlRow = document.getElementById("basketRow " + id);
        if (htmlRow != null) { // delete row if we are in cart
            htmlRow.remove();
        }
    }
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

async function openBasket() {
    if (document.getElementById("partialBasket") === null) { return; }
    const data = JSON.stringify(JSON.parse(sessionStorage.getItem("MyCart")) || []);
    try {
        const response = await fetch("Basket/MyPetBasket", {
            method: "Post",
            headers: {
                "Content-Type": "application/json",
            },
            body: data,
        }); // Replace with your API endpoint
        if (response.ok) {
            const partialHtml = await response.text();
            document.getElementById("partialBasket").innerHTML = partialHtml;
            return response;
        } else {
            console.error('Error fetching data:', response.statusText);
        }
    }
    catch (error) {
        console.error('An error occurred:', error);
    }
}

document.addEventListener("DOMContentLoaded", openBasket);
document.addEventListener("DOMContentLoaded", showNumberOfElementsInBasket)