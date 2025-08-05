# GraphQL Usage Examples

This file contains practical examples of using the GraphQL API with complex data structures.

## Create Customer with Addresses

### GraphQL Mutation
```graphql
mutation CreateCustomerWithAddresses {
  createCustomer(input: {
    name: "John Doe"
    email: "john.doe@example.com"
    addresses: [
      {
        street: "123 Main St"
        city: "New York"
        postalCode: "10001"
      },
      {
        street: "456 Business Ave"
        city: "New York"
        postalCode: "10002"
      }
    ]
  }) {
    id
    name
    email
    addresses {
      id
      street
      city
      postalCode
    }
  }
}
```

### Expected Response
```json
{
  "data": {
    "createCustomer": {
      "id": "f47ac10b-58cc-4372-a567-0e02b2c3d479",
      "name": "John Doe",
      "email": "john.doe@example.com",
      "addresses": [
        {
          "id": "6ba7b810-9dad-11d1-80b4-00c04fd430c8", 
          "street": "123 Main St",
          "city": "New York",
          "postalCode": "10001"
        },
        {
          "id": "6ba7b811-9dad-11d1-80b4-00c04fd430c8",
          "street": "456 Business Ave", 
          "city": "New York",
          "postalCode": "10002"
        }
      ]
    }
  }
}
```

### cURL Example
```bash
curl -X POST http://localhost:5003/graphql \
  -H "Content-Type: application/json" \
  -d '{
    "query": "mutation CreateCustomerWithAddresses { createCustomer(input: { name: \"John Doe\", email: \"john.doe@example.com\", addresses: [{ street: \"123 Main St\", city: \"New York\", postalCode: \"10001\" }, { street: \"456 Business Ave\", city: \"New York\", postalCode: \"10002\" }] }) { id name email addresses { id street city postalCode } } }"
  }'
```

### Variables Example
You can also use GraphQL variables for cleaner queries:

```graphql
mutation CreateCustomerWithAddresses($input: CreateCustomerRequest!) {
  createCustomer(input: $input) {
    id
    name
    email
    addresses {
      id
      street
      city
      postalCode
    }
  }
}
```

**Variables:**
```json
{
  "input": {
    "name": "Jane Smith",
    "email": "jane.smith@example.com", 
    "addresses": [
      {
        "street": "789 Oak Street",
        "city": "Los Angeles",
        "postalCode": "90210"
      },
      {
        "street": "321 Pine Avenue", 
        "city": "Los Angeles",
        "postalCode": "90211"
      }
    ]
  }
}
```

## Update Customer with Addresses

### GraphQL Mutation
```graphql
mutation UpdateCustomerWithAddresses {
  updateCustomer(input: {
    id: "f47ac10b-58cc-4372-a567-0e02b2c3d479"
    name: "John Doe Updated"
    email: "john.doe.updated@example.com"
    addresses: [
      {
        street: "123 Updated Main St"
        city: "Updated New York" 
        postalCode: "10001"
      },
      {
        street: "789 New Business Ave"
        city: "Updated New York"
        postalCode: "10003"
      }
    ]
  }) {
    id
    name
    email
    addresses {
      id
      street
      city
      postalCode
    }
  }
}
```

## Query Customers with Addresses

### Basic Query
```graphql
query GetCustomersWithAddresses {
  customers {
    id
    name
    email
    addresses {
      id
      street
      city
      postalCode
    }
  }
}
```

### Filtered Query
```graphql
query GetCustomersByCity {
  customers(where: { 
    addresses: { 
      any: { 
        city: { eq: "New York" } 
      } 
    } 
  }) {
    id
    name
    email
    addresses(where: { city: { eq: "New York" } }) {
      id
      street
      city
      postalCode
    }
  }
}
```

### Query with Sorting
```graphql
query GetCustomersSorted {
  customers(order: [{ name: ASC }]) {
    id
    name
    email
    addresses(order: [{ city: ASC }, { street: ASC }]) {
      id
      street
      city
      postalCode
    }
  }
}
```

## Get Customer by ID with Addresses

```graphql
query GetCustomerById {
  customerById(id: "f47ac10b-58cc-4372-a567-0e02b2c3d479") {
    id
    name
    email
    addresses {
      id
      street
      city
      postalCode
    }
  }
}
```

## Create Customer with No Addresses

```graphql
mutation CreateCustomerNoAddresses {
  createCustomer(input: {
    name: "Simple Customer"
    email: "simple@example.com"
    addresses: []
  }) {
    id
    name
    email
    addresses {
      id
      street
      city
      postalCode
    }
  }
}
```

## Error Handling Examples

### Invalid Email Format
```graphql
mutation CreateCustomerInvalidEmail {
  createCustomer(input: {
    name: "Test User"
    email: "invalid-email"
    addresses: []
  }) {
    id
    name
    email
  }
}
```

**Expected Error Response:**
```json
{
  "errors": [
    {
      "message": "Validation failed",
      "extensions": {
        "code": "VALIDATION_ERROR",
        "details": {
          "Email": ["Invalid email format"]
        }
      }
    }
  ],
  "data": {
    "createCustomer": null
  }
}
```

### Missing Required Fields
```graphql
mutation CreateCustomerMissingName {
  createCustomer(input: {
    email: "test@example.com"
    addresses: []
  }) {
    id
    name
    email
  }
}
```

**Expected Error Response:**
```json
{
  "errors": [
    {
      "message": "Validation failed",
      "extensions": {
        "code": "VALIDATION_ERROR", 
        "details": {
          "Name": ["Name is required"]
        }
      }
    }
  ],
  "data": {
    "createCustomer": null
  }
}
```

## Using GraphQL Playground

1. Start the application: `dotnet run`
2. Navigate to: `http://localhost:5003/graphql` (in development mode)
3. Use the interactive playground to test queries and mutations
4. Explore the schema documentation in the right panel
5. Use the query builder for complex queries

## Testing with Popular GraphQL Clients

### Using Apollo Client (JavaScript)
```javascript
import { gql, useMutation } from '@apollo/client';

const CREATE_CUSTOMER = gql`
  mutation CreateCustomer($input: CreateCustomerRequest!) {
    createCustomer(input: $input) {
      id
      name
      email
      addresses {
        id
        street
        city
        postalCode
      }
    }
  }
`;

function CreateCustomerForm() {
  const [createCustomer] = useMutation(CREATE_CUSTOMER);
  
  const handleSubmit = (formData) => {
    createCustomer({
      variables: {
        input: {
          name: formData.name,
          email: formData.email,
          addresses: formData.addresses
        }
      }
    });
  };
}
```

### Using GraphQL-Request (Node.js)
```javascript
import { request, gql } from 'graphql-request';

const CREATE_CUSTOMER = gql`
  mutation CreateCustomer($input: CreateCustomerRequest!) {
    createCustomer(input: $input) {
      id
      name
      email
      addresses {
        id
        street
        city
        postalCode
      }
    }
  }
`;

const variables = {
  input: {
    name: "API Customer",
    email: "api@example.com",
    addresses: [
      {
        street: "API Street 123",
        city: "API City",
        postalCode: "12345"
      }
    ]
  }
};

const data = await request('http://localhost:5003/graphql', CREATE_CUSTOMER, variables);
console.log(data);
```