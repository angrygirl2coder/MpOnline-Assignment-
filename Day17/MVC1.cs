using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingSystem
{
    // ========== MODELS ==========
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }

    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }

    public class Appointment
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationMinutes { get; set; } = 30; // default 30 min
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

        // Foreign keys
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }

    public enum AppointmentStatus
    {
        Scheduled,
        Cancelled,
        Completed
    }

    // ========== DATABASE CONTEXT ==========
    public class AppDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Use SQLite (file-based DB)
            optionsBuilder.UseSqlite("Data Source=AppointmentDB.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed some doctors and patients
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { Id = 1, Name = "Dr. Smith", Specialty = "Cardiology" },
                new Doctor { Id = 2, Name = "Dr. Lee", Specialty = "Dermatology" }
            );

            modelBuilder.Entity<Patient>().HasData(
                new Patient { Id = 1, Name = "Alice", Phone = "123-456-7890" },
                new Patient { Id = 2, Name = "Bob", Phone = "987-654-3210" }
            );
        }
    }

    // ========== SERVICE LAYER ==========
    public class AppointmentService
    {
        private readonly AppDbContext _context;

        public AppointmentService(AppDbContext context)
        {
            _context = context;
        }

        // Check if doctor is available at given start time (no overlapping scheduled appointments)
        public bool IsDoctorAvailable(int doctorId, DateTime startTime, int durationMinutes = 30)
        {
            var endTime = startTime.AddMinutes(durationMinutes);

            // Any scheduled appointment that overlaps with the requested time?
            var overlapping = _context.Appointments
                .Where(a => a.DoctorId == doctorId
                            && a.Status == AppointmentStatus.Scheduled
                            && a.StartTime < endTime
                            && a.StartTime.AddMinutes(a.DurationMinutes) > startTime)
                .Any();

            return !overlapping;
        }

        // Book an appointment (checks availability first)
        public bool BookAppointment(int doctorId, int patientId, DateTime startTime, int durationMinutes = 30)
        {
            if (!IsDoctorAvailable(doctorId, startTime, durationMinutes))
                return false;

            var appointment = new Appointment
            {
                DoctorId = doctorId,
                PatientId = patientId,
                StartTime = startTime,
                DurationMinutes = durationMinutes,
                Status = AppointmentStatus.Scheduled
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            return true;
        }

        // Cancel an appointment (only if it's Scheduled)
        public bool CancelAppointment(int appointmentId)
        {
            var app = _context.Appointments.Find(appointmentId);
            if (app == null || app.Status != AppointmentStatus.Scheduled)
                return false;

            app.Status = AppointmentStatus.Cancelled;
            _context.SaveChanges();
            return true;
        }

        // Generate monthly report: total appointments and cancellations per month
        public Dictionary<string, (int Total, int Cancelled)> GetMonthlyReport()
        {
            var report = new Dictionary<string, (int, int)>();

            var appointments = _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .ToList(); // load in memory for grouping by month/year string

            var groups = appointments
                .GroupBy(a => new { a.StartTime.Year, a.StartTime.Month })
                .Select(g => new
                {
                    MonthKey = $"{g.Key.Year}-{g.Key.Month:D2}",
                    Total = g.Count(),
                    Cancelled = g.Count(a => a.Status == AppointmentStatus.Cancelled)
                });

            foreach (var g in groups)
            {
                report[g.MonthKey] = (g.Total, g.Cancelled);
            }

            return report;
        }

        // Helper to list all appointments (for demo)
        public List<Appointment> GetAllAppointments()
        {
            return _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .OrderBy(a => a.StartTime)
                .ToList();
        }

        // Helper to get doctors list
        public List<Doctor> GetDoctors() => _context.Doctors.ToList();
        public List<Patient> GetPatients() => _context.Patients.ToList();
    }

    // ========== CONSOLE UI ==========
    class Program
    {
        static AppointmentService _service;

        static void Main(string[] args)
        {
            // Ensure database is created and seeded
            using var db = new AppDbContext();
            db.Database.EnsureCreated();

            _service = new AppointmentService(db);

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("===== APPOINTMENT BOOKING SYSTEM =====");
                Console.WriteLine("1. View All Appointments");
                Console.WriteLine("2. Book Appointment");
                Console.WriteLine("3. Cancel Appointment");
                Console.WriteLine("4. Check Doctor Availability");
                Console.WriteLine("5. Generate Monthly Report");
                Console.WriteLine("6. Exit");
                Console.Write("Select option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ViewAllAppointments(); break;
                    case "2": BookAppointment(); break;
                    case "3": CancelAppointment(); break;
                    case "4": CheckAvailability(); break;
                    case "5": GenerateReport(); break;
                    case "6": exit = true; break;
                    default: Console.WriteLine("Invalid option."); break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey(true);
                }
            }
        }

        static void ViewAllAppointments()
        {
            var list = _service.GetAllAppointments();
            if (!list.Any())
            {
                Console.WriteLine("No appointments found.");
                return;
            }

            foreach (var a in list)
            {
                Console.WriteLine($"[{a.Id}] {a.StartTime:yyyy-MM-dd HH:mm} | {a.Doctor.Name} | {a.Patient.Name} | Status: {a.Status}");
            }
        }

        static void BookAppointment()
        {
            // Show doctors
            var doctors = _service.GetDoctors();
            Console.WriteLine("Doctors:");
            foreach (var d in doctors)
                Console.WriteLine($"  {d.Id}. {d.Name} ({d.Specialty})");

            Console.Write("Enter Doctor ID: ");
            if (!int.TryParse(Console.ReadLine(), out int doctorId))
            { Console.WriteLine("Invalid ID."); return; }

            // Show patients
            var patients = _service.GetPatients();
            Console.WriteLine("Patients:");
            foreach (var p in patients)
                Console.WriteLine($"  {p.Id}. {p.Name}");

            Console.Write("Enter Patient ID: ");
            if (!int.TryParse(Console.ReadLine(), out int patientId))
            { Console.WriteLine("Invalid ID."); return; }

            Console.Write("Enter date and time (yyyy-MM-dd HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startTime))
            { Console.WriteLine("Invalid date/time."); return; }

            Console.Write("Enter duration in minutes (default 30): ");
            if (!int.TryParse(Console.ReadLine(), out int duration))
                duration = 30;

            bool success = _service.BookAppointment(doctorId, patientId, startTime, duration);
            if (success)
                Console.WriteLine("Appointment booked successfully.");
            else
                Console.WriteLine("Booking failed. Doctor may be unavailable at that time.");
        }

        static void CancelAppointment()
        {
            Console.Write("Enter Appointment ID to cancel: ");
            if (!int.TryParse(Console.ReadLine(), out int appId))
            { Console.WriteLine("Invalid ID."); return; }

            bool success = _service.CancelAppointment(appId);
            if (success)
                Console.WriteLine("Appointment cancelled.");
            else
                Console.WriteLine("Cancellation failed. Appointment may not exist or already cancelled.");
        }

        static void CheckAvailability()
        {
            var doctors = _service.GetDoctors();
            Console.WriteLine("Doctors:");
            foreach (var d in doctors)
                Console.WriteLine($"  {d.Id}. {d.Name}");

            Console.Write("Enter Doctor ID: ");
            if (!int.TryParse(Console.ReadLine(), out int doctorId))
            { Console.WriteLine("Invalid ID."); return; }

            Console.Write("Enter date and time (yyyy-MM-dd HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startTime))
            { Console.WriteLine("Invalid date/time."); return; }

            Console.Write("Enter duration in minutes (default 30): ");
            if (!int.TryParse(Console.ReadLine(), out int duration))
                duration = 30;

            bool available = _service.IsDoctorAvailable(doctorId, startTime, duration);
            Console.WriteLine(available ? "Doctor is available." : "Doctor is not available (overlapping appointment).");
        }

        static void GenerateReport()
        {
            var report = _service.GetMonthlyReport();
            if (!report.Any())
            {
                Console.WriteLine("No appointments to report.");
                return;
            }

            Console.WriteLine("Monthly Report (Total vs Cancelled):");
            Console.WriteLine("Month        | Total | Cancelled");
            Console.WriteLine("-------------+-------+----------");
            foreach (var kvp in report.OrderBy(k => k.Key))
            {
                Console.WriteLine($"{kvp.Key} | {kvp.Value.Total,5} | {kvp.Value.Cancelled,8}");
            }
        }
    }
}
