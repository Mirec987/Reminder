# Reminder Application

This project is a simple reminder application built with .NET 8 and C# 12. It uses Dependency Injection (DI) and demonstrates the creation and execution of different types of reminders, each with its own logic and message delivery API.

## Features

- **Reminders:**
  - `InvoiceReminder`: Sends an invoice payment reminder.
  - `ReservationReminder`: Sends a reservation reminder.
  - `AdministratorCallReminder`: Reminds you to call the administrator.
  - `NewsletterReminder`: Sends a reminder about a newsletter.

- **API Implementations:**
  - `EmailReminderAPI`: Sends reminders via email (simulated with a console message).
  - `AllAPI`: Sends reminders through a generic API (simulated with a console message).
  - `CallAdministratorAPI`: Calls an administrator with a reminder (simulated with a console message).

## Technologies

- .NET 8
- C# 12
- Microsoft.Extensions.DependencyInjection for Dependency Injection

## Usage

1. Clone the repository:
    ```bash
    git clone <repository-url>
    ```

2. Open the project in your preferred IDE (e.g., Visual Studio or VS Code).

3. Build and run the project.

4. The console will display the execution of various reminders, each using its specific message delivery mechanism.

## How It Works

1. The project defines an abstract `Reminder` class with an abstract method `Execute()`. Each reminder type extends this class and implements its own logic in the `Execute()` method.

2. The project uses Dependency Injection to manage the creation of reminder instances and their associated APIs.

3. In the `Main` method, all reminders are retrieved from the DI container and executed in sequence, displaying messages in the console.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## License

This project is licensed under the MIT License.
