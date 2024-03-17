# 

**Create Airline Request :**

- **`IATA` Code Length:** Two characters
- **`ICAO` Code Length:** Four characters
- **`"iataCode"`** represents the IATA code for the airline, and it is set to "DL" (Delta's IATA code).
- **`"icaoCode"`** represents the ICAO code for the airline, and it is set to "DAL" (Delta's ICAO code).
- **`"name"`** represents the name of the airline, and it is set to "Delta Air Lines".

```json
{
	"iataCode": "DL",
	"icaoCode": "DAL",
	"name": "Delta Air Lines"
}
```

Response  :

```json
{
	"id": 1,
	"createdAt": "2024-03-13T00:56:28.1355978Z",
	"iataCode": "DL",
	"icaoCode": "DAL",
	"name": "Delta Air Lines"
}
```

**Get Airlines R**equest has default pagination settings 1 page and 10 details.‼️‼️‼️

If you want more details enter per page param from query ‼️‼️‼️

```json
[
  {
	"id": 1,
	"createdAt": "2024-03-13T00:56:28.135597Z",
	"iataCode": "DL",
	"icaoCode": "DAL",
	"name": "Delta Air Lines"
  },
  {
	"id": 2,
	"createdAt": "2024-03-13T01:04:10.961588Z",
	"iataCode": "AA",
	"icaoCode": "AAL",
	"name": "American Airlines"
  }
]
```

**Create Country Request:**

- **`name` (string):** The full name of the country. In this example, it is "United States of America."
- **`cioc` (string):** The three-letter code for the country used by the International Olympic Committee (IOC). Here, "USA" represents the United States.
- **`area` (number):** The total land area of the country in square kilometers. For the United States, it is approximately 9,372,610 square kilometers.
- **`capital` (string):** The name of the capital city of the country. In this case, it is "Washington, D.C."
- **`cca2` (string):** The two-letter country code according to the ISO 3166-1 alpha-2 standard. For the United States, it is "US."
- **`cca3` (string):** The three-letter country code according to the ISO 3166-1 alpha-3 standard. Here, it is "USA" for the United States.
- **`ccn3` (string):** The numeric country code according to the ISO 3166-1 numeric standard. For the United States, it is "840."

```json
{
	"name": "United States of America",
	"cioc": "USA",
	"area": 9372610,
	"capital": "Washington, D.C.",
	"cca2": "US",
	"cca3": "USA",
	"ccn3": "840"
}
```

Response:

```json
{
	"id": 1,
	"name": "United States of America",
	"cioc": "USA",
	"area": 9372610,
	"capital": "Washington, D.C.",
	"cca2": "US",
	"cca3": "USA",
	"ccn3": "840"
}
```

**Create Airport Request :**

- **`code` (string):** The three-letter IATA code for the airport, used for quick identification. In this example, it is "JFK" for John F. Kennedy International Airport.
- **`tz` (string):** The time zone ID for the airport. In this case, "America/New_York" represents the Eastern Time Zone.
- **`timeZone` (string):** The human-readable name of the time zone. Here, it is "Eastern Standard Time."
- **`type` (string):** The type of airport, indicating whether it is domestic or international. In this case, it is "international."
- **`label` (string):** The full name or label for the airport. Here, it is "John F. Kennedy International Airport."
- **`city` (string):** The city where the airport is located. For this example, it is "New York."
- **`countryId` (integer):** The unique identifier for the country where the airport is situated. The value "840" represents the United States.
- **`details` (object):** Additional details about the airport, including IATA code, ICAO code, and facilities.
    - **`iataCode` (string):** The IATA code for the airport. In this example, it is "JFK."
    - **`icaoCode` (string):** The ICAO code for the airport. Here, it is "KJFK."
    - **`facilities` (string):** A brief description of the airport's facilities. For instance, "Modern airport with multiple terminals and extensive amenities."
  - **`location` (object):** The geographical coordinates and elevation of the airport.
      - **`longitude` (number):** The longitude of the airport. Here, it is approximately -73.7781.
      - **`latitude` (number):** The latitude of the airport. In this case, it is approximately 40.6413.
      - **`elevation` (number):** The elevation of the airport above sea level. The value "13" represents 13 meters.

```json
{
	  "code": "JFK",
	  "tz": "America/New_York",
	  "timeZone": "Eastern Standard Time",
	  "type": "international",
	  "label": "John F. Kennedy International Airport",
	  "city": "New York",
	  "countryId": 1,
	  "details": {
		  "iataCode": "JFK",
		  "icaoCode": "KJFK",
		  "facilities": "Modern airport with multiple terminals and extensive amenities."
	  },
	  "location": {
	      "longitude": -73.7781,
	      "latitude": 40.6413,
	      "elevation": 13
	  }
}
```

Response :

```json
	{
	"id": 1,
	"code": "JFK",
	"tz": "America/New_York",
	"timeZone": "Eastern Standard Time",
	"type": "international",
	"label": "John F. Kennedy International Airport",
	"city": "New York",
	"countryId": 1,
	"details": {
	  "iataCode": "JFK",
	  "icaoCode": "KJFK",
	  "facilities": "Modern airport with multiple terminals and extensive amenities."
	},
		"location": {
		  "longtitude": 0,
		  "latitude": 40.6413,
		  "elevation": 13
		}
	}
```

    **Create Flight Request** :

      - **`airlineId` (integer):** The unique identifier of the airline operating the flight.
      - **`externalId` (string):** An external identifier for the flight nullable.
      - **`departureAirportId` (integer):** The unique identifier of the departure airport.
      - **`departureTime` (string):** The UTC date and time of departure in ISO 8601 format.
      - **`arrivalAirportId` (integer):** The unique identifier of the arrival airport.
      - **`arrivalTime` (string):** The UTC date and time of arrival in ISO 8601 format.
      - **`details` (object):** Additional details about the flight.
          - **`passengerCount` (integer):** The number of passengers booked on the flight.
          - **`isAvailable` (boolean):** Indicates whether the flight is currently available for booking.
          - **`hasFreeBaggage` (boolean):** Indicates whether the flight offers free baggage allowance.
          - **`transactionsCount` (integer):** The number of transactions associated with the flight.
          - **`hasTransaction` (boolean):** Indicates whether there are any transactions associated with the flight.
      - **`prices` (array of objects):** The pricing information for the flight, including different fare types.
          - **`amount` (number):** The price of the flight fare.
          - **`type` (string):** The type of fare, such as "main" or "discount".

      ```json
      {
                  "airlineId": 1234,
                  "externalId": "ABC123",
                  "departureAirportId": 456,
                  "departureTime": "2024-03-13T08:00:00Z",
                  "arrivalAirportId": 789,
                  "arrivalTime": "2024-03-13T12:00:00Z",
                  "details": {
                      "passengerCount": 150,
                      "isAvailable": true,
                      "hasFreeBaggage": true,
                      "transactionsCount": 50,
                      "hasTransaction": true
                  },
                  "prices": [
                          {
                              "amount": 200,
                              "type": "main"
                          },
                          {
                              "amount": 50,
                              "type": "discount"
                          },
                          {
                              "amount": 15,
                              "type": "VAT"
                          }
                  ]
      }
      ```

    Response :

      ```json
      {
        "id": 1,
        "externalId": null,
        "airlineId": 1,
        "departureAirportId": 3,
        "departueTime": "2024-03-13T01:28:01.638Z",
        "arrivalAirportId": 4,
        "arrrivalTime": "2024-03-13T01:28:01.638Z",
        "details": {
          "passengerCount": 250,
          "isAvailable": true,
          "hasFreeBagage": true,
          "transactionsCount": 0,
          "hasTransaction": true
        },
        "prices": [
          {
            "amount": 450,
            "type": "main"
          },
          {
            "amount": 50,
            "type": "discount"
          },
          {
            "amount": 10,
            "type": "vat"
          }
        ]
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
