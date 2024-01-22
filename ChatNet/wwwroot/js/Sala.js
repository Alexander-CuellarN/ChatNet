import { showNotificacion, typeMessage } from "./Usuarios.js"

document.querySelectorAll(".btnDelete").forEach(b => {

    console.log(b)
    let { salaid } = b.dataset;

    b.addEventListener("click", () => {
        let statusResult = true

        fetch(`/Sala/Delete/${salaid}`, { method: "Delete" })
            .then(data => {
                if (!data.ok) {
                    statusResult = data.ok
                    return data.json();
                }

                return
            })
            .then(data => {
                if (!statusResult) throw new Error(data?.message ?? "❌ no se ha podido eliminar la Sala.");
                document.querySelector(`.trTarget_${salaid}`).remove();
                showNotificacion(typeMessage.Succes, data?.message ?? "✅ las sala se ha eliminado exitosamente")
            })
            .catch(eror => {
                showNotificacion(typeMessage.Error, eror.message)
            })
    })
})