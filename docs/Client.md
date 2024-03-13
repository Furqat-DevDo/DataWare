    
**Create Booking Request** :

  - **`flightId` (integer):** The unique identifier of the flight associated with the booking.
  - **`passengerId` (integer):** The unique identifier of the passenger associated with the booking.
  - **`totalPrice` (number):** The total price of the booking, usually in the currency specified by the system.
  - **`status` (string):** The status of the booking, indicating whether it is pending, confirmed, cancelled, etc. Common statuses include "pending", "confirmed", "cancelled", and "inprogress".

  ```json
  {
      "flightId": 1,
      "passengerId": 1,
      "totalPrice": 1000,
      "status": "pending"
  }
  ```

    Response:
  ```json
  {
    "id": 1,
    "flightId": 1,
    "passengerId": 1,
    "totalPrice": 1000,
    "status": "pending",
    "createdAt": "2024-03-13T01:42:09.5538081Z",
    "updatedAt": null
  }
  ```

**Create Passenger Request :**

- **`userId` (integer, nullable):** The unique identifier of the user making the booking. If the user is not authenticated or registered, this field may be null.
- **`email` (string):** The email address of the user making the booking. This is used for communication and confirmation purposes.
- **`phone` (string):** The phone number of the user making the booking. This is often used for contact and notification purposes.
- **`fullname` (string):** The full name of the user making the booking. This is used for identification and ticketing purposes.

  ```json
  {
      "userId": null,
      "email": "[jon@gmail.com](mailto:jon@gmail.com)",
      "phone": "998900996767",
      "fullname": "Jon Dow"
  }
  ```

    Response :

  ```json
  {
    "id": 1,
    "userId": null,
    "email": "jon@gmail.com",
    "phone": "998900996767",
    "fullname": "Jon Dow",
    "createdAt": "2024-03-13T01:38:46.8027322Z"
  }
  ```