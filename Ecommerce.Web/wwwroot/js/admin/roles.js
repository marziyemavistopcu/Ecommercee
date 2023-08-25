function AllRoles() {
    Get("Role/all-roles", (data) => {
        var html = '<table class="table table-hover">' +
            '<tr><th style="width:50px">Id</th><th>Rol Adý</th><th></th></tr>';

        var arr = data;

        for (var i = 0; i < arr.length; i++) {
            html += '<tr>';
            html += `<td>${arr[i].id}</td><td>${arr[i].role}</td>`;
            html += `<td><i class="bi bi-trash text-danger" onclick='DeleteRoles(${arr[i].id})'></i><i class="bi bi-pencil-square" onclick='UpdateRole(${arr[i].id}, "${arr[i].role}")'></i></td>`;
            html += '</tr>';
        }
        html += '</table>';

        $("#divRoles").html(html);
    });
}

let selectedRoleId = 0;

function NewRole() {
    selectedRoleId = 0;
    $("#inputRoleName").val("");
    $("#roleModal").modal("show");
}

function SaveRoles() {
    var newRole = {
        Id: 0,
        role: $("#inputRoleName").val()
    }
    Post("Role/add-role", newRole, (data) => { AllRoles(); });
}

function UpdateRole(id, role) {
    selectedRoleId = id;
    $("#inputRoleName").val(role);
    $("#roleModal").modal("show");
}

function DeleteRoles(id) {
    Delete(`Role/delete?id=${id}`, (data) =>
    {
        AllRoles();
    });
}

$(document).ready(function () {
    AllRoles();
})