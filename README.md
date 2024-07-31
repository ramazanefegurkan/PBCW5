[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/NKa989in)
## Completed Tasks

### 1. Controllers for All Domain Models
- Controllers have been created for all domain models.
- Each controller includes the following methods:
  - **GET**: Retrieve a list of all records.
  - **GETBYID**: Retrieve a record by its ID.
  - **POST**: Create a new record.
  - **PUT**: Update an existing record.
  - **DELETE**: Remove a record.

### 2. CQRS Implementation
- CQRS pattern is implemented for each controller.
- Separate command and query classes are prepared for handling requests and responses efficiently.

### 3. Fluent Validation
- Fluent Validation classes are created for all models.
- Validation rules ensure data integrity and enforce business rules.
- Fluent Validation is integrated into MediatR's behavior pipeline to validate requests before they are processed.

### 4. Middleware for Logging Requests and Responses
- A middleware logger class is designed to log all incoming requests and outgoing responses.
- This ensures that every interaction with the API is tracked and can be reviewed for debugging and auditing purposes.
