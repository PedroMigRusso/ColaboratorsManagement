# Collaborators Management

## Description

Collaborators Management is a web application developed in ASP.NET Core MVC (.NET 8) that allows you to manage a list of collaborators with full CRUD functionality.

This project uses:
- Entity Framework Core with SQLite for data persistence
- A layered architecture with service interfaces
- Pagination, filtering, and searching capabilities
- Unit testable service layer for clean and maintainable code

## Features

- Create, read, update, and delete collaborators
- Filter by name or technology
- Dropdown with search or free text input for technologies
- Pagination for long lists
- Separation of data access logic using service interfaces
- Clean UI with basic Bootstrap styling

## Collaborator Entity

Each collaborator record includes the following fields:

- **Id** – Unique identifier (int)
- **Name** – Full name of the collaborator (string)
- **BirthDate** – Date of birth (DateTime)
- **YearsOfExperience** – Total years of professional experience (int)
- **BetterTechnology** – Technology where the collaborator has the strongest skills (string)
