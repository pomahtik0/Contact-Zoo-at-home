async function openPetCard(id: string) {
    const url = "Home/GetPetsMiddleCard";
    const data = JSON.stringify(id);
    try {
        const response = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: data,
        });

        if (response.ok) {
            const partialHtml = await response.text();
            console.log(partialHtml);
        } else {
            console.error("Error fetching data:", response.status, response.statusText);
        }
    } catch (error) {
        console.error("An error occurred:", error);
    }
}