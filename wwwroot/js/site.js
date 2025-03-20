


async function createCandidate() {
    clearValidationErrors();

    const candidate = {
        firstName: document.getElementById("firstName").value.trim(),
        lastName: document.getElementById("lastName").value.trim(),
        email: document.getElementById("email").value.trim(),
        mobile: document.getElementById("mobile").value.trim(),
        degreeId: document.getElementById("degree").value || null
    };

    const validationErrors = validateForm(candidate);
    if (Object.keys(validationErrors).length > 0) {
        displayValidationErrors(validationErrors);
        return;
    }

    try {
        const response = await fetch('/api/Candidates', {
            method: 'POST',
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(candidate)
        });

        if (response.ok) {
            alert("Candidate created successfully!");
            window.location.href = "/Candidates";
        } else {
            alert("Error creating candidate.");
        }
    } catch (error) {
        console.error(error);
        alert("Failed to create candidate.");
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

function clearValidationErrors() {
    ["firstNameError", "lastNameError", "emailError", "mobileError"].forEach(id => {
        document.getElementById(id).textContent = "";
    });
}
document.addEventListener('DOMContentLoaded', loadDegrees);