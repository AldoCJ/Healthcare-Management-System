using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareManagementApp.Models;

namespace HealthcareManagementApp.Services
{
    public class AppointmentService
    {
        private readonly List<Appointment> appointments = new();

        public void AddAppointment(Appointment appointment)
        {

            if (IsOverlapping(appointment))
            {
                throw new InvalidOperationException("Physician is already booked during this time.");
            }

            appointments.Add(appointment);
        }

        public void RemoveAppointment(int index)
        {
            if (index >= 0 && index < appointments.Count)
                appointments.RemoveAt(index);
            else
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Invalid appointment selection.");
            }
        }

        public void RescheduleAppointment(int index, DateTime newStart, DateTime newEnd)
        {
            if (index < 0 || index >= appointments.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Invalid appointment index.");

            if (newEnd <= newStart)
                throw new ArgumentException("End time must be after start time.");

            Appointment appointment = appointments[index];
            var updated = new Appointment(newStart, newEnd, appointment.Patient, appointment.Physician);

            if (IsOverlapping(updated, index))
                throw new InvalidOperationException("New time conflicts with another appointment.");

            appointment.StartTime = newStart;
            appointment.EndTime = newEnd;
        }

        public IEnumerable<Appointment> GetPhysicianAppointments(Physician physician)
        {
            return appointments.Where(a => a.Physician == physician);
        }

        public IEnumerable<Appointment> GetPatientAppointments(Patient patient)
        {
            return appointments.Where(a => a.Patient == patient);
        }

        public void DeletePatientAppointments(Patient patient)
        {
            appointments.RemoveAll(a => a.Patient.Equals(patient));
        }

        public void DeletePhysicianAppointments(Physician physician)
        {
            appointments.RemoveAll(a => a.Physician.Equals(physician));
        }

        public Appointment GetAppointment(int index) => appointments[index];

        public int NumberOfAppointments()
        {
            return appointments.Count;
        }

        public void ShowAllAppointments()
        {
            Console.WriteLine("-------------------------------");
            foreach (Appointment appointment in appointments)
            {
                Console.WriteLine($"{appointment}");
                Console.WriteLine("-------------------------------");
            }
        }

        public void ListAppointments()
        {
            int i = 1;
            foreach (Appointment appointment in appointments)
            {
                Console.WriteLine($"{i}. {appointment.Patient.Name} with {appointment.Physician.Name} at {appointment.StartTime} ");
                i++;
            }
        }
        private bool IsOverlapping(Appointment newAppointment, int ignoreIndex = -1)
        {
            for (int i = 0; i < appointments.Count; i++)
            {
                if (i == ignoreIndex) continue;

                var existing = appointments[i];
                if (existing.Physician == newAppointment.Physician &&
                    newAppointment.StartTime < existing.EndTime &&
                    newAppointment.EndTime > existing.StartTime)
                {
                    return true;
                }
            }
            return false;
        }
    }

}
