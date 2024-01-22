import { showNotificacion, typeMessage } from "./Usuarios.js"

let signalrContext = new signalR.HubConnectionBuilder().withUrl("/Chat").build();
let messagepanel = document.getElementById("messagePanel")
let currentRoom = null

document.querySelectorAll(".btnNewConnection").forEach(button => {

    let { room } = button.dataset

    button.addEventListener("click", () => {

        signalrContext.stop();
        currentRoom = room
        fetch(`/Home/ShowMessagesByRoom/${room}`)
            .then(data => {
                if (!data.ok) throw new Error("Ha ocurrido un error");
                return data.json()
            })
            .then(data => {
                messagepanel.innerHTML = "";
                document.getElementById("SendNewMessage").disabled = false;
                data?.data.forEach(m => {
                    let message = `
                        <div id='messageId_${data.mensajeId}'>
                        <p> <strong>${m.userNick} dijo: </strong> ${m.contenido} </p>
                        </div>
                    `
                    messagepanel.insertAdjacentHTML("beforeend", message)
                })

                document.querySelector(".formContainer").scrollIntoView();
            })
            .catch(e => showNotificacion(typeMessage.Error, "❌ ha ocurrido un error"));

        signalrContext
            .start()
            .then(_ => {
                let userName = document.getElementById("userName").value;
                signalrContext.invoke("instanceConnection", Number.parseInt(room), userName)
                    .catch(() => showNotificacion(typeMessage.Error, "❌ ha ocurrido un error"))

                signalrContext.on("newConnect", (name) => {
                    let message = `
                        <div class="d-flex justify-content-end text-info">
                        <p> <strong>${name}</strong> se ha unido a la sala </p>
                        </div>
                    `
                    messagepanel.insertAdjacentHTML("beforeend", message)
                });

                document.getElementById("SendNewMessage").addEventListener("click", () => {

                    let { value: contentMessage } = document.getElementById("messageContent")
                    console.log(currentRoom)
                    if (contentMessage == null || contentMessage == "")
                        return showNotificacion(typeMessage.Error, "❌ El contenido del mensaje es obligatorio")

                    if (!currentRoom)
                        return showNotificacion(typeMessage.Error, "❌ No se ha encontrado una sala para enviar el mensaje")

                    let { value: userid } = document.getElementById("userId")

                    console.log(currentRoom, userid)
                    signalrContext.invoke("SendMessage",
                        Number.parseInt(currentRoom),
                        Number.parseInt(userid),
                        contentMessage)
                        .then(() => document.getElementById("messageContent").value = "" )
                        .catch(() => showNotificacion(typeMessage.Error, "❌ Ha ocurrido un error enviando el mensaje"))
                });

                signalrContext.on("newMessage", (newMessage, messageId, NickName) => {

                    let message = `<div id='messageId_${messageId}'>
                        <p> <strong>${NickName} dijo: </strong> ${newMessage} </p>
                        </div>`

                    messagepanel.insertAdjacentHTML("beforeend", message)
                })

            })
        //.catch(e => showNotificacion(typeMessage.Error, "❌ ha ocurrido un error"))
    });
})

