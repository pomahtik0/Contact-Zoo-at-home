function uploadImage() {
    document.getElementById('fileInput').click();
}

function handleImageUpload(input: HTMLInputElement, imageElement: HTMLImageElement) {

    if (imageElement === null) {
        console.error("No Image element found ")
        return;
    }

    const file: File = input.files[0];
    if (file) {
        // mb something else later
        imageElement.src = URL.createObjectURL(file);
    }
}