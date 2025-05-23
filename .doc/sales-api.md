[Back to README](../README.md)

### Sales

#### GET /sales/{id}
- Description: Retrieve a specific sale by Id.
- Id: Sale Id.
- Response: 
  ```json
    {
    "id": "string",
    "saleNumber": "string",
    "saleAt": "string (ISO 8601 format)",
    "customerId": "string",
    "customerName": "string",
    "totalSaleAmount": "number",
    "branchSale": "string",
    "totalDiscounts": "number",
    "products": [
      {
        "id": "string",
        "productId": "string",
        "productName": "string",
        "quantity": "integer",
        "unitPrice": "number",
        "totalAmount": "number"
      }
    ]
  }
  ```

#### POST /sales
- Description: Add a new sale.
- Request Body:
  ```json
    {
    "saleAt": "string (ISO 8601 format)",
    "customerId": "string",
    "customerName": "string",
    "totalSaleAmount": "number",
    "branchSale": "string",
    "products": [
      {
        "productId": "string",
        "productName": "string",
        "quantity": "integer",
        "unitPrice": "number"
      }
    ]
  }
  ```
- Response: 
  ```json
    {
    "id": "string",
    "saleNumber": "string",
    "saleAt": "string (ISO 8601 format)",
    "customerId": "string",
    "customerName": "string",
    "totalSaleAmount": "number",
    "branchSale": "string",
    "totalDiscounts": "number",
    "products": [
      {
        "id": "string",
        "productId": "string",
        "productName": "string",
        "quantity": "integer",
        "unitPrice": "number",
        "totalAmount": "number"
      }
    ]
  }
  ```



#### PUT /sales/{id}
- Description: Update a specific sale by Id.
- Path Parameters:
  - `id`: Sale Id 
- Request Body:
  ```json
    {
    "saleAt": "2025-02-24T15:24:41.954Z",
    "customerId": "string",
    "customerName": "string",
    "branchSale": "string",
    "products": [
      {
        "productId": "string",
        "productName": "string",
        "quantity": 0,
        "unitPrice": 0
      }
    ],
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "saleNumber": "string"
   }
  ```
- Response: 
  ```json
    {
    "id": "string",
    "saleAt": "string (ISO 8601 format)",
    "customerId": "string",
    "customerName": "string",
    "totalSaleAmount": "number",
    "branchSale": "string",
    "products": [
      {
        "id": "string",
        "productId": "string",
        "productName": "string",
        "quantity": "integer",
        "unitPrice": "number",
        "totalAmount": "number"
      }
    ]
   }
  ```

#### DELETE /sales/{id}
- Description: : Delete a specific sale by Id.
- Path Parameters:
  - `id`: Sale Id
- Response: 
  ```json
  {
    "message": "string"
  }
  ```

<br>
<div style="display: flex; justify-content: space-between;">
  <a href="./general-api.md">Previous: General API</a>
  <a href="./tech-stack.md">Next: Tech Stack</a>
</div>