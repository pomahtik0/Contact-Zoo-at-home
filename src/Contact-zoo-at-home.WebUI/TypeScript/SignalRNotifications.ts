import * as signalR from "@microsoft/signalr";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .build();

connection.on("notificationRecived", (notificationType: string, notificationId: string, ) => {
    showNotification(notificationType, notificationId);
});

connection.start().catch((err) => document.write(err));

async function showNotification(notificationType: string, notificationId: string)
{
    const url = "notification/" + notificationType + "/" + notificationId;

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