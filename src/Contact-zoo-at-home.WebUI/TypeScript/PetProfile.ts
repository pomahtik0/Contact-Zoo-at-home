async function openPetCard(id: string) {
    const url = "Data/PetProfileCard/" + id;
    try {
        const response = await fetch(url, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            },
        });

        if (response.ok) {
            const partialHtml = await response.text();
            document.getElementById("modelDialog").innerHTML = partialHtml;
        } else {
            console.error("Error fetching data:", response.status, response.statusText);
        }
    } catch (error) {
        console.error("An error occurred:", error);
    }
}