# My-eCommerce Project

This project is a microservices-based e-commerce solution developed as part of a technical evaluation. It implements a complete sales management system with associated services for user management and product catalog.

## Table of Contents

- [Overview](#overview)
- [Requirements](#requirements)
- [Tech Stack](.doc/tech-stack.md)
- [Project Structure](.doc/project-structure.md)
- [Getting Started](#getting-started)
- [Features Implemented](#features-implemented)
- [Areas for Improvement](#areas-for-improvement)

## Overview

The project demonstrates proficiency in:

- C# and .NET 8.0 development
- Clean Architecture and DDD principles
- Microservices architecture
- RESTful API design
- PostgreSQL integration
- Entity Framework Core
- Unit testing with xUnit
- Mocking with NSubstitute
- Object mapping with AutoMapper
- API response pagination and filtering
- Git Flow and Semantic Commits

## Requirements

The main requirement was to implement a Sales API that handles:

- Sale number
- Sale date
- Customer information
- Total sale amount
- Branch information
- Product details
- Quantities
- Unit prices
- Discounts
- Total amount per item
- Cancellation status

### Business Rules

- 10% discount for purchases of 4+ identical items
- 20% discount for purchases of 10-20 identical items
- Maximum limit of 20 identical items per purchase
- No discounts for purchases under 4 items

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- Docker Desktop
- Git

### Local Setup

1. Clone the repository:
```bash
git clone https://github.com/yourusername/My-eCommerce.git
cd My-eCommerce
```

2. Start the infrastructure services:
```bash
docker-compose up -d sql.database nosql.database cache.redis
```

3. Build the solution:
```bash
dotnet build
```

4. Run the database migrations:
```bash
cd src/ORM.Migrations
dotnet ef database update
```

5. Start the services:
```bash
docker-compose up -d
```

The services will be available at:
- User Management API: http://localhost:8080
- Catalog Management API: http://localhost:8081
- Sales Management API: http://localhost:8082

## Features Implemented

✅ Complete CRUD operations for sales management  
✅ Business rules implementation  
✅ Input validation  
✅ Error handling  
✅ API documentation  
✅ Unit tests  
✅ Integration tests  
✅ Docker containerization  
✅ Database migrations  
✅ Clean Architecture implementation  
✅ API versioning  
✅ Logging  

## Areas for Improvement

While the project meets the core requirements, several areas could be enhanced:

### Testing
- Implement BDD tests using SpecFlow
- Increase test coverage (currently focused on critical paths)
- Add more integration tests
- Implement end-to-end testing

### Event Processing
- Implement proper event publishing for:
  - SaleCreated
  - SaleModified
  - SaleCancelled
  - ItemCancelled
- Integrate Rebus for message processing
- Implement proper event handling and retry policies

### Database Optimization
- Implement MongoDB for read models
- Add Redis caching for frequently accessed data
- Optimize query performance

### Infrastructure
- Add API Gateway
- Implement service discovery
- Add circuit breakers
- Implement proper logging aggregation
- Add monitoring and alerting

### Documentation
- Add API documentation using Swagger
- Improve code documentation
- Add architectural decision records (ADRs)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---
*Note: This project was developed as part of a technical evaluation and is not intended for production
