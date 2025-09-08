[Back to Requirements](./REQUIREMENTS.md)

### Branches

#### GET /branches
- Description: Retrieve a list of all branches
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
        "code": "string",
        "name": "string",
        "address": "string",
      }
    ],
    "totalItems": "integer",
    "currentPage": "integer",
    "totalPages": "integer"
  }
  ```

#### POST /branches
- Description: Add a new branch
- Request Body:
  ```json
  {
    "code": "string",
    "name": "string",
    "address": "string",
  }
  ```
- Response: 
  ```json
  {
    "id": "integer",
    "code": "string",
    "name": "string",
    "address": "string",
  }
  ```

#### GET /branches/{id}
- Description: Retrieve a specific branch by ID
- Path Parameters:
  - `id`: Branch ID
- Response: 
  ```json
  {
    "id": "integer",
    "code": "string",
    "name": "string",
    "address": "string",
  }
  ```

#### PUT /branches/{id}
- Description: Update a specific branch
- Path Parameters:
  - `id`: Branch ID
- Request Body:
  ```json
  {
    "code": "string",
    "name": "string",
    "address": "string",
  }
  ```
- Response: 
  ```json
  {
    "id": "integer",
    "code": "string",
    "name": "string",
    "address": "string",
  }
  ```

#### DELETE /branches/{id}
- Description: Delete a specific branch
- Path Parameters:
  - `id`: Branch ID
- Response: 
  ```json
  {
    "message": "string"
  }
  ```


<br>
<div style="display: flex; justify-content: space-between;">
  <a href="./products-api.md">Previous: Products API</a>
  <a href="./carts-api.md">Next: Carts API</a>
</div>