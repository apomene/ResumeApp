
async function loadDegrees() {
    const response = await fetch('/api/Degrees');
    const degrees = await response.json();
    const tableBody = document.getElementById('degressTableBody');
    tableBody.innerHTML = '';

    degrees.forEach(degree => {
        const row = `<tr>
                    <td>${degree.name}</td>
                    <td>${degree.creationTime}</td>
                   
                    <td>
                        <a href="/api/Degrees/Edit/${degree.id}" class="btn btn-warning btn-sm">Edit</a>
                        <button class="btn btn-danger btn-sm" onclick="deleteDegree(${degree.id})">Delete</button>
                    </td>
                </tr>`;
        tableBody.innerHTML += row;
    });
}

async function createDegree() {

    const degree = {
        Name: document.getElementById("degree").value.trim(),
    };


    let url = isEdit ? `/api/Degrees/${degreeId}` : `/api/Degrees`;
    let method = isEdit ? "PUT" : "POST";


    try {
        const response = await fetch(url, {
            method: method,
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(degree)
        });

        if (response.ok) {
            alert(isEdit ? "Degree updated successfully!" : "Degree created successfully!");
            window.location.href = "/Degrees";
        } else {
            alert("Error saving degree.");
        }
    } catch (error) {
        console.error(error);
        alert("Failed to save degree.");
    }
}

async function deleteDegree(id) {
    if (confirm('Are you sure you want to delete this degree?')) {
        await fetch(`/api/Degrees/${id}`, { method: 'DELETE' });
        loadDegrees();
    }
}

document.addEventListener('DOMContentLoaded', loadDegrees);