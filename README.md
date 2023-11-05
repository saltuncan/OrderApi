To run the project first you need to run the migration commands to create database&tables.
In Package Manager Console first run "add-migration init" command and after that "update-database" command.
With these commands the database will be ready.

A user and three products will be added to the related tables in OnModelCreating method of OrderDbContext.
After running the project, swagger will be opened and you can create an order using CreateOrder endpoint.

Expected input is products list and id of the user.
Example bodyRequest formats: 
To add create an order with one product for a user.
{
  "products": [
    {
      "productId": 1,
      "productCount": 1
    }
  ],
  "userId": 1
}

To add create an order with two products for a user.
{
  "products": [
    {
      "productId": 1,
      "productCount": 1
    },
    {
      "productId": 2,
      "productCount": 2
    }
  ],
  "userId": 1
}

All the orders inserted to database with setting the property IsInvoiceSent = false. And an email is sent to the user to inform the order created.

A background job is creted. This job is triggered every 30 seconds. This job gets the latest orders(the ones having the IsInvoiceSent property is false.).
After getting the orders, invoice mail is sent to the user and the IsInvoiceSent property of the order is set to true.
