using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Reminder.Interfaces;

namespace Reminder
{
    public abstract class Reminder(IReminderApi reminderAPI)
    {
        protected IReminderApi reminderAPI = reminderAPI;

        public abstract void Execute();
    }

    public class InvoiceReminder(IReminderApi reminderAPI) : Reminder(reminderAPI)
    {
        public override void Execute()
        {
            string invoiceNumber = "INV-1234";
            string message = $"Invoice Payment Reminder for Invoice: {invoiceNumber}";
            reminderAPI.Remind(message);
        }
    }

    public class ReservationReminder(IReminderApi reminderAPI) : Reminder(reminderAPI)
    {
        public override void Execute()
        {
            int reservationId = 5678;
            string message = $"Reservation Reminder for Reservation ID: {reservationId}";
            reminderAPI.Remind(message);
        }
    }

    public class AdministratorCallReminder(IReminderApi reminderAPI) : Reminder(reminderAPI)
    {
        public override void Execute()
        {
            string warningCode = "WARN-789";
            string message = $"Reminder: Call the administrator regarding code: {warningCode}";
            reminderAPI.Remind(message);
        }
    }

    public class NewsletterReminder(IReminderApi reminderAPI) : Reminder(reminderAPI)
    {
        public override void Execute()
        {
            string newsletterTitle = "News";
            string message = $"Newsletter Reminder: {newsletterTitle}";
            reminderAPI.Remind(message);
        }
    }

    public class EmailReminderAPI : IReminderApi
    {
        public void Remind(string message)
        {
            Console.WriteLine($"Sending an Email: {message}");
        }
    }

    public class AllAPI : IReminderApi
    {
        public void Remind(string message)
        {
            Console.WriteLine($"Sending through AllAPI: {message}");
        }
    }

    public class CallAdministratorAPI : IReminderApi
    {
        public void Remind(string message)
        {
            Console.WriteLine($"Calling Administrator with message: {message}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Configure the DI container
            var serviceProvider = new ServiceCollection()
                   .AddTransient<InvoiceReminder>(provider =>
                       new InvoiceReminder(new EmailReminderAPI()))
                   .AddTransient<ReservationReminder>(provider =>
                       new ReservationReminder(new AllAPI()))
                   .AddTransient<AdministratorCallReminder>(provider =>
                       new AdministratorCallReminder(new CallAdministratorAPI()))
                   .AddTransient<NewsletterReminder>(provider =>
                       new NewsletterReminder(new EmailReminderAPI()))
                   .BuildServiceProvider();


    //        var serviceProvider = new ServiceCollection()
    //.AddSingleton<IReminderAPI, EmailReminderAPI>()
    //.AddSingleton<IReminderAPI, CallAdministratorAPI>()
    //.AddSingleton<IReminderAPI, AllAPI>()
    //.AddTransient<InvoiceReminder>()
    //.AddTransient<ReservationReminder>()
    //.AddTransient<AdministratorCallReminder>()
    //.AddTransient<NewsletterReminder>()
    //.BuildServiceProvider();

            // Retrieve and execute all reminders
            var reminders = GetReminders(serviceProvider);
            foreach (var reminder in reminders)
            {

                Console.WriteLine($"Executing reminder: {reminder.name}");
                reminder.reminder.Execute();
            }

            Console.WriteLine("All reminders have been executed.");
            Console.ReadKey();
        }

        // Method to create and return a list of reminders using DI
        private static List<(string name, Reminder reminder)> GetReminders(IServiceProvider serviceProvider)
        {
            return
            [
                ("Invoice Reminder", serviceProvider.GetRequiredService<InvoiceReminder>()),
                ("Reservation Reminder", serviceProvider.GetRequiredService<ReservationReminder>()),
                ("Administrator Call Reminder", serviceProvider.GetRequiredService<AdministratorCallReminder>()),
                ("Newsletter Reminder", serviceProvider.GetRequiredService<NewsletterReminder>()),
            ];
        }
    }
}
