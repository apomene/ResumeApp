
The purpose of this project is to create a simple application for managing resumes (CVs) with the following functionalities:
Features:

    Candidate Management:

        Add, edit, and delete candidates.

    Degree Management:

        Add, edit, and delete degrees.

    Candidate-Degree Association:

        Link a candidate to a degree from the list of registered degrees.

    Cleanup:

        Delete degrees from the degrees table that are no longer associated with any candidates.

Database Tables:

    Candidates:

        Id (Auto number)

        LastName – Required field.

        FirstName – Required field.

        Email – Required field with email validation.

        Mobile – Optional, validated for 10 digits.

        Degree – Optional (Pick from the list of degrees like BSc, MSc, PhD).

        CV – Optional (Upload PDF or Word document as a blob).

        CreationTime – Default to the current date and time.

    Degrees:

        Id (Auto number)

        Name – Required field.

        CreationTime – Default to the current date and time.

Requirements:

    The application is built using .NET Core 8.0 and utilizes Entity Framework Core with an In-Memory Database.


This project is intended to be a functional, backend-focused application rather than one with a complex user interface.
