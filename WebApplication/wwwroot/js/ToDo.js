uri = "/ToDo/getItems";

const model_base = {
    id: 0,                   
    name: "",                
    description: "",         
    isCompleted: "",      
    startDate: null,         
    finishDate: null
}

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
                $("<td>").text(formatDate(item.startDate)),
                $("<td>").text(formatDate(item.finishDate)),
                $("<td>").append(
                    $("<button>").addClass("btn btn-primary btn-sm me-2 btn-edit")
                        .data("model", item).text("Edit"),
                    $("<button>").addClass("btn btn-danger btn-sm btn-delete")
                        .data("txtId", item.id).text("Delete")
                )
            ));
        });
    }).catch((error) => {
        console.error('Error fetching data:', error);
    });
}
function ShowModal(model) {

    $("#txtName").val(model.name);
    $("#txtId").val(model.id);
    $("#txtDescription").val(model.description);
    $("#txtIsCompleted").val(model.isCompleted ? "true" : "false");
    $("#txtStartDate").val(model.startDate);
    $("#txtFinishDate").val(model.finishDate);

    $('.modal').modal("show");
}
function Clean() {
    $("#txtName").val('');
    $("#txtId").val('');
    $("#txtDescription").val('');
    $("#txtIsCompleted").val('');
    $("#txtStartDate").val('');
    $("#txtFinishDate").val('');
}
function formatDate(isoString) {
    const date = new Date(isoString);
    return date.toLocaleString('en-US', {
        year: 'numeric',
        month: 'short',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
    });
}

$("#btnNew").click(() => {
    ShowModal({
        id: 0,
        name: "",
        description: "",
        isCompleted: 0,
        startDate: "",
        finishDate: ""
    });
})

$("#btnSave").click(() => {

    if ($("#txtName").val() == "") {
        return alert('Name is required!');
    }
    
    let newModel = model_base;
    newModel["id"] = $("#txtId").val();
    newModel["name"] = $("#txtName").val();
    newModel["description"] = $("#txtDescription").val();
    newModel["isCompleted"] = $("#txtIsCompleted").val() === "true";
    newModel["startDate"] = new Date($("#txtStartDate").val());
    newModel["finishDate"] = new Date($("#txtFinishDate").val());
    if ($("#txtId").val() == "0") {

        fetch("/ToDo/Create", {
            method: "POST",
            headers: {
                'Content-type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(newModel)
        }).then((response) => {
            return response.ok ? response.json() : Promise.reject(response)
        }).then((dataJson) => {
            if (dataJson.valor) {
                alert('Item added successfully!');
                $('.modal').modal('hide');
                Clean();
                getItems();
            }
        })
    } else {
        fetch("/ToDo/Update", {
            method: "PUT",
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(newModel)
        }).then((response) => {
            return response.ok ? response.json() : Promise.reject(response)
        }).then((dataJson) => {
            if (dataJson) {
                alert('Item updated successfully!');
                $('.modal').modal('hide');
                Clean();
                getItems();
            }
        })
    }
})

$("#tbList tbody").on("click", ".btn-edit", function () {
    let item = $(this).data("model");
    ShowModal(item);
})

$("#tbList tbody").on("click", ".btn-delete", function () {
    let id = $(this).data("txtId");
    let result = window.confirm("Do you want delete this item?");
    if (result == true) {
        fetch("/ToDo/Delete?id=" + id, {
            method: 'DELETE'
        }).then((response) => {
            return response.ok ? response.json() : Promise.reject(response)
        }).then((dataJson) => {
            if (dataJson.valor) {
                Clean();
                getItems();
            }
        })
    }
})