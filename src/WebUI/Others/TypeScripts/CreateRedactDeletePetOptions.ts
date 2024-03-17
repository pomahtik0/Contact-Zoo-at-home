async function removeItem_PetOption(index: number) {
    const url = "/PetOwner/RemovePetOption"; // Specify your controller action URL
    const formData = new FormData(document.getElementById("form") as HTMLFormElement); // Serialize the form data

    formData.append("itemToDelete", index.toString());
    try {
        const response = await fetch(url, {
            method: "POST",
            body: formData,
        });

        if (response.ok) {
            const partialHtml = await response.text();
            document.getElementById("patrialPetOptions").innerHTML = partialHtml;
        } else {
            console.error("Error fetching data:", response.status, response.statusText);
        }
    } catch (error) {
        console.error("An error occurred:", error);
    }
}

async function addNewItem_PetOption() {
    const url = "/PetOwner/AddNewPetOption"; // Specify your controller action URL
    const formData = new FormData(document.getElementById("form") as HTMLFormElement); // Serialize the form data
    try {
        const response = await fetch(url, {
            method: "POST",
            body: formData,
        });

        if (response.ok) {
            const partialHtml = await response.text();
            document.getElementById("patrialPetOptions").innerHTML = partialHtml;
        } else {
            console.error("Error fetching data:", response.status, response.statusText);
        }
    } catch (error) {
        console.error("An error occurred:", error);
    }
}