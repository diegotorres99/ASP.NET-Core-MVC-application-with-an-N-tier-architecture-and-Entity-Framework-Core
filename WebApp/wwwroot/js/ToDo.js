import { error } from "jquery";
uri = "/ToDoList/getItems";

const model = {
    idItem: 0,
    name: "",
    description: "",
    startDate: "",
    finishDate: ""
}

//Get and show data when page load.
$(document).ready(() => {
    getItems();
});
function getItems() {
    fetch(uri).then((response) => {
        return response.ok ? response.json() : Promise.reject(response);
    }).then((dataJson) => {
        $("#tbList tbody").empty(); // Clear existing rows

        dataJson.forEach((item) => {
            $("#tbList tbody").append($("<tr>").append(
                $("<td>").text(item.name),
                $("<td>").text(item.description),
                $("<td>").text(item.isCompleted),
                $("<td>").text(item.startDate),
                $("<td>").text(item.finishDate),
                $("<td>").append(
                    $("<button>").addClass("btn btn-primary btn-sm me-2 btn-editar")
                        .data("modelo", item).text("Edit"),
                    $("<button>").addClass("btn btn-danger btn-sm btn-eliminar")
                        .data("id", item.idItem).text("Delete")
                )
            ));
        });
    }).catch((error) => {
        console.error('Error fetching data:', error);
    });
}