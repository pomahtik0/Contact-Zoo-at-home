function uploadImage() {
    document.getElementById('fileInput').click();
}
function handleImageUpload(input, imageElement) {
    if (imageElement === null) {
        console.error("No Image element found ");
        return;
    }
    var file = input.files[0];
    if (file) {
        // mb something else later
        imageElement.src = URL.createObjectURL(file);
    }
}
//# sourceMappingURL=ImageScripts.js.map