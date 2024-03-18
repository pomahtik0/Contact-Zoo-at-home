var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (g && (g = 0, op[0] && (_ = 0)), _) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
function addToCart(id) {
    var existingCart = JSON.parse(sessionStorage.getItem("MyCart")) || [];
    if (existingCart.some(function (elem) { return elem === id; })) {
        return;
    } // avoiding duplicates
    existingCart.push(id);
    sessionStorage.setItem("MyCart", JSON.stringify(existingCart));
    showNumberOfElementsInBasket();
}
function removeFromBasket(id) {
    var existingCart = JSON.parse(sessionStorage.getItem("MyCart")) || [];
    var index = existingCart.indexOf(id, 0);
    if (index > -1) {
        existingCart.splice(index, 1);
        sessionStorage.setItem("MyCart", JSON.stringify(existingCart)); // remove item from cart
        showNumberOfElementsInBasket();
        var htmlRow = document.getElementById("basketRow " + id);
        if (htmlRow != null) { // delete row if we are in cart
            htmlRow.remove();
        }
    }
}
function showNumberOfElementsInBasket() {
    // Get the element by ID
    var changeMeElement = document.getElementById("petBasketBadge");
    if (changeMeElement == null) {
        return;
    }
    var existingCart = JSON.parse(sessionStorage.getItem("MyCart")) || [];
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
function openBasket() {
    return __awaiter(this, void 0, void 0, function () {
        var data, response, partialHtml, error_1;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    if (document.getElementById("partialBasket") === null) {
                        return [2 /*return*/];
                    }
                    data = JSON.stringify(JSON.parse(sessionStorage.getItem("MyCart")) || []);
                    _a.label = 1;
                case 1:
                    _a.trys.push([1, 6, , 7]);
                    return [4 /*yield*/, fetch("Basket/MyPetBasket", {
                            method: "Post",
                            headers: {
                                "Content-Type": "application/json",
                            },
                            body: data,
                        })];
                case 2:
                    response = _a.sent();
                    if (!response.ok) return [3 /*break*/, 4];
                    return [4 /*yield*/, response.text()];
                case 3:
                    partialHtml = _a.sent();
                    document.getElementById("partialBasket").innerHTML = partialHtml;
                    return [2 /*return*/, response];
                case 4:
                    console.error('Error fetching data:', response.statusText);
                    _a.label = 5;
                case 5: return [3 /*break*/, 7];
                case 6:
                    error_1 = _a.sent();
                    console.error('An error occurred:', error_1);
                    return [3 /*break*/, 7];
                case 7: return [2 /*return*/];
            }
        });
    });
}
document.addEventListener("DOMContentLoaded", openBasket);
document.addEventListener("DOMContentLoaded", showNumberOfElementsInBasket);
//# sourceMappingURL=PetBasketControll.js.map