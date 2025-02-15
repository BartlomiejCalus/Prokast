document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("accountForm").addEventListener("submit", async function (event) {
        event.preventDefault();

        const formData = new FormData(event.target);
        const data = {
            firstName: formData.get("firstName"),
            lastName: formData.get("lastName"),
            email: formData.get("email"),
            warehouseID: parseInt(formData.get("warehouseID")),
            role: parseInt(formData.get("role"))
        };

        try {
            const response = await fetch("https://prokast-axgwbmd6cnezbmet.polandcentral-01.azurewebsites.net/api/login/create", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(data)
            });
            const result = await response.json();

            // Mapping API response keys to user-friendly names
            const fieldNames = {
                firstName: "Nazwa użytkownika: ",
                lastName: "Hasło: ",
                email: "Email: ",
                warehouseID: "ID magazynu: ",
                role: "Rola: ",
                id: "ID konta: ",
                clientID: "ID klienta: ",
                createdDate: "Konto utworzono: "
            };

            let responseHTML = "";
            for (const [key, value] of Object.entries(result)) {
                if (typeof value === "object" && value !== null) {
                    for (const [subKey, subValue] of Object.entries(value)) {
                        responseHTML += `<p class="response-item"><strong>${fieldNames[subKey] || subKey}:</strong> ${subValue}</p>`;
                    }
                } else {
                    responseHTML += `<p class="response-item"><strong>${fieldNames[key] || key}:</strong> ${value}</p>`;
                }
            }
            
            document.getElementById("response").innerHTML = responseHTML;
        } catch (error) {
            document.getElementById("response").innerHTML = `<p class="response-item">Error: ${error.message}</p>`;
        }
    });
});
