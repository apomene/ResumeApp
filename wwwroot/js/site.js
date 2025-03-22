
//async function loadDegrees() {
//    try {
//        const response = await fetch('/api/Degrees');
//        if (!response.ok) throw new Error("Failed to fetch degrees.");

//        const degrees = await response.json();
//        const degreeSelect = document.getElementById('degree');
//        degrees.forEach(degree => {
//            const option = document.createElement('option');
//            option.value = degree.id;
//            option.textContent = degree.name;
//            degreeSelect.appendChild(option);
//        });
//    } catch (error) {
//        console.error(error);
//        alert("Error loading degrees.");
//    }
//}

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

document.addEventListener('DOMContentLoaded', loadDegrees);
document.addEventListener('DOMContentLoaded', loadCandidates);


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