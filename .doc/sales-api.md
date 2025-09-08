[Back to Requirements](./REQUIREMENTS.md)

### Sales

#### GET /sales
- Description: Retrieve a list of all sales
- Query Parameters:
  - `_page` (optional): Page number for pagination (default: 1)
  - `_size` (optional): Number of items per page (default: 10)
  - `_order` (optional): Ordering of results (e.g., "id desc, userId asc")
- Response: 
  ```json
  {
    "id": "uuid",
    "number": "string",
    "date": "datetime",
    "branch": {
      "id": "uuid",
      "name": "string",
      "code": "string"
    },
    "products": [
      {
      "product": {
        "id": "uuid",
        "name": "string"
      },
      "customer": {
        "id": "uuid",
        "name": "string"
      },
      "quantity": "integer",
      "unitPrice": "decimal",
      "discount": "decimal",
      "totalAmount": "decimal"
      }
    ],
    "totalAmount": "decimal",
    "status": "CREATED | CANCELLED",
    "createdAt": "datetime",
    "updatedAt": "datetime"
  }
  ```

#### POST /sales
- Description: Add a new sale
- Request Body:
  ```json
  {
    "cartId": "uuid",
  }
  ```
- Response: 
  ```json
  {
    "id": "uuid",
    "number": "string",
    "date": "datetime",
    "branch": {
      "id": "uuid",
      "name": "string",
      "code": "string"
    },
    "products": [
      {
      "product": {
        "id": "uuid",
        "name": "string"
      },
      "customer": {
        "id": "uuid",
        "name": "string"
      },
      "quantity": "integer",
      "unitPrice": "decimal",
      "discount": "decimal",
      "totalAmount": "decimal"
      }
    ],
    "totalAmount": "decimal",
    "status": "CREATED | CANCELLED",
    "createdAt": "datetime",
    "updatedAt": "datetime"
  }
  ```

#### GET /sales/{id}
- Description: Retrieve a specific sale by ID
- Path Parameters:
  - `id`: Sale ID
- Response: 
  ```json
  {
    "id": "uuid",
    "number": "string",
    "date": "datetime",
    "branch": {
      "id": "uuid",
      "name": "string",
      "code": "string"
    },
    "products": [
      {
      "product": {
        "id": "uuid",
        "name": "string"
      },
      "customer": {
        "id": "uuid",
        "name": "string"
      },
      "quantity": "integer",
      "unitPrice": "decimal",
      "discount": "decimal",
      "totalAmount": "decimal"
      }
    ],
    "totalAmount": "decimal",
    "status": "CREATED | CANCELLED",
    "createdAt": "datetime",
    "updatedAt": "datetime"
  }
  ```

#### PUT /sales/{id}
- Description: Update a specific sale
- Path Parameters:
  - `id`: Sale ID
  - `date`: date to complete the sale
- Request Body:
  ```json
  {
    "products": [
      {
        "productId": "uuid",
        "quantity": "integer",
        "unitPrice": "decimal",
      }
    ]
  }
  ```
- Response: 
  ```json
  {
    "id": "uuid",
    "number": "string",
    "date": "datetime",
    "branch": {
      "id": "uuid",
      "name": "string",
      "code": "string"
    },
    "products": [
      {
      "product": {
        "id": "uuid",
        "name": "string"
      },
      "customer": {
        "id": "uuid",
        "name": "string"
      },
      "quantity": "integer",
      "unitPrice": "decimal",
      "discount": "decimal",
      "totalAmount": "decimal"
      }
    ],
    "totalAmount": "decimal",
    "status": "CREATED | CANCELLED",
    "createdAt": "datetime",
    "updatedAt": "datetime"
  }
  ```

#### DELETE /sales/{id}
- Description: Delete a specific sale
- Path Parameters:
  - `id`: Sale ID
- Response: 
  ```json
  {
    "message": "string"
  }
  ```
  
<br>
<div style="display: flex; justify-content: space-between;">
  <a href="./carts-api.md">Previous: Carts API</a>
  <a href="./users-api.md">Next: Users API</a>
</div>