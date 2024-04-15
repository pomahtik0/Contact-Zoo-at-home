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

async function uploadMoreComments(petId : string, lastCommentId: string) {
    const url = "/pets/" + petId + "/comments/" + lastCommentId;

    try {
        const response = await fetch(url, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            },
        });

        if (response.ok) {
            document.getElementById("uploadMorePetCommentsButton").remove();
            const partialHtml = await response.text();
            document.getElementById("petComments").innerHTML += partialHtml;
        } else {
            console.error("Error fetching data:", response.status, response.statusText);
        }
    } catch (error) {
        console.error("An error occurred:", error);
    }
}