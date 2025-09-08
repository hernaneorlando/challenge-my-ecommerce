[Back to Requirements](./REQUIREMENTS.md)

### Suppliers

#### GET /suppliers
- Description: Retrieve a list of all suppliers
- Query Parameters:
  - `_page` (optional): Page number for pagination (default: 1)
  - `_size` (optional): Number of items per page (default: 10)
  - `_order` (optional): Ordering of results (e.g., "id desc, userId asc")
- Response: 
  ```json
  {
    "data": [
      {
        "id": "integer",
        "name": "string",
        "document": "string",
        "email": "string",
        "phone": "string",
      }
    ],
    "totalItems": "integer",
    "currentPage": "integer",
    "totalPages": "integer"
  }
  ```

#### POST /suppliers
- Description: Add a new supplier
- Request Body:
  ```json
  {
    "name": "string",
    "document": "string",
    "email": "string",
    "phone": "string",
  }
  ```
- Response: 
  ```json
  {
    "id": "integer",
    "name": "string",
    "document": "string",
    "email": "string",
    "phone": "string",
  }
  ```

#### GET /suppliers/{id}
- Description: Retrieve a specific supplier by ID
- Path Parameters:
  - `id`: Supplier ID
- Response: 
  ```json
  {
    "id": "integer",
    "name": "string",
    "document": "string",
    "email": "string",
    "phone": "string",
  }
  ```

#### PUT /suppliers/{id}
- Description: Update a specific supplier
- Path Parameters:
  - `id`: Supplier ID
- Request Body:
  ```json
  {
    "name": "string",
    "document": "string",
    "email": "string",
    "phone": "string",
  }
  ```
- Response: 
  ```json
  {
    "id": "integer",
    "name": "string",
    "document": "string",
    "email": "string",
    "phone": "string",
  }
  ```

#### DELETE /suppliers/{id}
- Description: Delete a specific supplier
- Path Parameters:
  - `id`: Supplier ID
- Response: 
  ```json
  {
    "message": "string"
  }
  ```

<br>
<div style="display: flex; justify-content: space-between;">
  <a href="./general-api.md">Previous: General API</a>
  <a href="./products-api.md">Next: Products API</a>
</div>