document.getElementById("EditUser").addEventListener("click", () => {

    let userNick = document.getElementById("nickName").value
    let userID = document.getElementById("idUser").value

    if (userNick === "") return

    let data = {
        UsurioId: userID,
        nickName: userNick
    }

    let statusRequest;
    document.getElementById("EditUser").disabled = true;
    document.getElementById("DeleteUser").disabled = true;

    fetch("/Usuario/UpdateUser", {
        body: JSON.stringify(data),
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        }
    }).then(data => {
        statusRequest = data.ok
        return data.json();
    })
        .then(({ data, message }) => {

            if (!statusRequest) throw new Error(message);

            showNotificacion(typeMessage.Succes, message)
            document.getElementById("closeModal").click()
        })
        .catch((data) => {
            let message = data.message ?? "❌ ha ocurrido un error"
            showNotificacion(typeMessage.Error, message)
        })
        .finally(() => {
            document.getElementById("EditUser").disabled = false;
            document.getElementById("DeleteUser").disabled = false;
        });


});

document.getElementById("DeleteUser").addEventListener("click", () => {

    let idUser = document.getElementById("idUser").value;
    document.getElementById("EditUser").disabled = true;
    document.getElementById("DeleteUser").disabled = true;

    let statusResponse;

    fetch(`/Usuario/DeleteUser/${idUser}`, {
        method: "Delete"
    })
        .then((data) => {
            if (!data.ok) {
                statusResponse = data.ok;
                return data.json();
            } else {
                location.reload("~/")
            }
        })
        .then(data => {
            if (!statusResponse) throw new Error(message);        
        })
        .catch((data) => {
            let message = data.message ?? "❌ ha ocurrido un error"
            showNotificacion(typeMessage.Error, message)
        })
        .finally(() => {
            document.getElementById("EditUser").disabled = false;
            document.getElementById("DeleteUser").disabled = false;
        });
})
export let typeMessage = {
    Error: "Error", Succes: "Success"
}
export const showNotificacion = (type, message) => {
    let modalContainer = document.getElementById("NotificacionContainer")
    modalContainer.classList.remove("bg-primary")
    modalContainer.classList.remove("bg-danger")

    if (type === typeMessage.Error) {
        modalContainer.classList.add("bg-danger")
    }

    if (type === typeMessage.Succes) {
        modalContainer.classList.add("bg-primary")
    }

    modalContainer.classList.add("show")

    let contentModal = document.getElementById("notificacionMessage").innerHTML = message
}