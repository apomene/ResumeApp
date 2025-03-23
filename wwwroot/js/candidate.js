
async function loadCandidates() {
    const response = await fetch('/api/Candidates');
    const candidates = await response.json();
    const tableBody = document.getElementById('candidatesTableBody');
    if (tableBody != undefined) {
        tableBody.innerHTML = '';


        candidates.forEach(candidate => {
            const row = `<tr>
                    <td>${candidate.lastName}</td>
                    <td>${candidate.firstName}</td>
                    <td>${candidate.email}</td>
                    <td>${candidate.mobile || 'N/A'}</td>
                    <td>${candidate.degree ? candidate.degree.name : 'N/A'}</td>
                     <td>${candidate.creationTime}</td>
                    <td>
                        <a href="/api/Candidates/Edit/${candidate.id}" class="btn btn-warning btn-sm">Edit</a>
                        <button class="btn btn-danger btn-sm" onclick="deleteCandidate(${candidate.id})">Delete</button>
                    </td>
                </tr>`;
            tableBody.innerHTML += row;
        });
    }
}
async function createCandidate() {

    const candidate = {
        firstName: document.getElementById("firstname").value.trim(),
        lastName: document.getElementById("lastname").value.trim(),
        email: document.getElementById("email").value.trim(),
        mobile: document.getElementById("mobile").value.trim(),
        degreeId: document.getElementById("degree").value || null
    };

    const validationErrors = validateForm(candidate);
    if (Object.keys(validationErrors).length > 0) {
        displayValidationErrors(validationErrors);
        return;
    }
    let url = isEdit ? `/api/Candidates/${candidateId}` : `/api/Candidates`;
    let method = isEdit ? "PUT" : "POST";


    try {
        const response = await fetch(url, {
            method: method,
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(candidate)
        });

        if (response.ok) {
            alert(isEdit ? "Candidate updated successfully!" : "Candidate created successfully!");
            window.location.href = "/Candidates";
        } else {
            alert("Error saving candidate.");
        }
    } catch (error) {
        console.error(error);
        alert("Failed to save candidate.");
    }
}

async function deleteCandidate(id) {
    if (confirm('Are you sure you want to delete this candidate?')) {
        await fetch(`/api/Candidates/${id}`, { method: 'DELETE' });
        loadCandidates();
    }
}
function validateForm(candidate) {
    const errors = {};
    if (!candidate.firstName) errors.firstName = "First name is required.";
    if (!candidate.lastName) errors.lastName = "Last name is required.";
    if (!candidate.email || !/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/.test(candidate.email)) {
        errors.email = "Invalid email format.";
    }
    if (candidate.mobile && !/^\d{10}$/.test(candidate.mobile)) {
        errors.mobile = "Mobile number must be 10 digits.";
    }
    return errors;
}

function displayValidationErrors(errors) {
    for (const key in errors) {
        document.getElementById(`${key}Error`).textContent = errors[key];
    }
}

document.addEventListener('DOMContentLoaded', loadCandidates);



