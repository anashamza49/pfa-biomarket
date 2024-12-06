# PFA Project

## Description

This project is a web application built using ASP.NET Core, SignalR, and JWT authentication. It provides features for user registration, login, profile management, and cooperative validation, as well as real-time notifications using SignalR.

## Features

- **Admin Roles**:
  - View list of clients and their details
  - Validate cooperatives
- **Client Roles**:
  - Update user profile
- **Cooperative Roles**:
  - Add and update cooperatives
  - Add magasins (stores) for cooperatives

## Technologies Used

- ASP.NET Core
- Entity Framework Core
- SignalR
- JWT Authentication
- Google OAuth for login
- Microsoft Identity for user management
- CORS for cross-origin requests

## API Endpoints

### Authentication

- `POST /api/authentication/register`: Registers a new user.
- `POST /api/authentication/login`: Logs in a user and returns a JWT token.
- `POST /api/authentication/delete-account`: Deletes the current user's account.
- `GET /api/authentication/login-google`: Starts Google login process.
- `GET /api/authentication/google-response`: Handles the Google login response.

### Admin

- `GET /api/admin/clients`: Returns a list of clients.
- `GET /api/admin/clients/details`: Returns detailed information about clients.
- `GET /api/admin/unvalidated-cooperatives`: Returns a list of unvalidated cooperatives.
- `POST /api/admin/validate-cooperative/{cooperativeName}`: Validates a cooperative by its name.

### Client

- `POST /api/client/update-profile`: Updates the user's profile.

### Cooperative

- `POST /api/cooperative/add`: Adds a new cooperative.
- `PUT /api/cooperative/update`: Updates an existing cooperative.
- `POST /api/cooperative/add-magasin`: Adds a magasin (store) for a cooperative.

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/pfaproject.git
