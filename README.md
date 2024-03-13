# **AviaSales Project Overview**

## **Introduction**

The AviaSales project is a comprehensive solution divided into two API projects: AviaSales.Admin.Api and AviaSales.Api. These projects leverage various technologies such as Fluent Validation, Serilog, EF Core, EF Core PostgreSQL, and Swagger OpenAPI. The project encompasses functionality related to airlines, airports, passengers, bookings, countries, and flights.

## **Target Framework**

- **.NET Version:** 6 or higher.

## **AviaSales.Admin.Api**

### **Entities**

1. **Airline:** Perform CRUD operations on airline entities.
2. **Airport:** Manage CRUD operations for airport entities.
3. **Passenger:** Facilitates CRUD operations on passenger entities.
4. **Booking:** Provides CRUD functionalities for booking entities.
5. **Country:** Supports CRUD operations on country entities.
6. **Flight:** Enables CRUD operations on flight entities.

## **AviaSales.Api**

### **Functionality**

1. **Flights:**
    - Retrieve flight information.
    - Utilizes external resources for enhanced data.
2. **Booking and Passenger:**
    - Create and update booking entities.
    - Manage passenger entities.
3. **Airline and Airport:**
    - Retrieve information about airline and airport entities.

## **Technologies Used**

- **Fluent Validation:** Ensures robust validation for input data.
- **Serilog:** Facilitates structured logging for enhanced debugging and monitoring.
- **EF Core:** Serves as the Entity Framework Core for data access.
- **EF Core PostgreSQL:** Utilizes PostgreSQL as the database for persistent storage.
- **Swagger OpenAPI:** Empowers API documentation and exploration.

## **External Resources**

The Flights and Country services seamlessly integrate external resources to enrich data and enhance the overall user experience.
