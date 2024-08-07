# Course.Microservice

Microservice Course project inspired by Udemy

## Table of Contents

- [Introduction](#introduction)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Running the Application](#running-the-application)
- [Contributing](#contributing)
- [License](#license)

## Introduction

This project is a microservice-based architecture inspired by Udemy's course structure. It leverages .NET and various other technologies to create a scalable and maintainable application.

## Project Structure
```
Course.Microservice/
├── Gateways/
│   └── Course.Gateway/
├── IdentityServer/
│   └── Course.IdentityServer/
├── Services/
├── Shared/
│   └── Course.Shared/
├── clients/
│   └── Course.Web/
├── .dockerignore
├── .gitignore
├── Microservices.sln
├── Ports.txt
├── ResourceOwnerPasswordTokenHandler.cs
├── docker-compose.dcproj
├── docker-compose.override.yml
├── docker-compose.yml
└── launchSettings.json
```


### Key Folders and Files

- **Gateways/**: Contains API Gateway for routing and aggregation.
  - **Course.Gateway/**: Implementation of the API Gateway.
- **IdentityServer/**: Manages authentication and authorization.
  - **Course.IdentityServer/**: Implementation of Identity Server.
- **Services/**: Contains individual microservices.
- **Shared/**: Shared libraries and components.
  - **Course.Shared/**: Implementation of shared components.
- **clients/**: Client applications.
  - **Course.Web/**: Frontend web application.
- **docker-compose.yml**: Docker Compose file for container orchestration.

## Technologies Used

- **Backend**: .NET Core
- **Frontend**: HTML, CSS, JavaScript
- **Authentication**: IdentityServer
- **API Gateway**: Ocelot
- **Communication**: RabbitMQ
- **Database**: PostgreSQL
- **Containerization**: Docker

## Getting Started

These instructions will help you set up and run the project on your local machine for development and testing purposes.

### Prerequisites

- .NET SDK 8.0
- Docker
- Visual Studio 2022 or later / Visual Studio Code
- PostgreSQL

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/BatuhanB/Course.Microservice.git
   cd Course.Microservice
   ```
2. Set up environment variables:
   Create a .env file in the root directory and add the necessary environment variables.
3. Build and run Docker containers:
   ```bash
   docker-compose up --build
   ```
### Running the Application

1. Start the services using Docker Compose:
   ```bash
   docker-compose up
   ```
2. Open the solution in Visual Studio and set multiple startup projects (Gateway, IdentityServer, Services).
3. Run the solution.
4. Access the web application at `http://localhost:<GatewayPort>`.

### Contributing

Contributions are welcome! Please open an issue or submit a pull request.
