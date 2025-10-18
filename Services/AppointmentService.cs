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
                throw new InvalidOperationException("Physician is already booked during this time");
            }

            if (appointment.StartTime >= appointment.EndTime)
            {
                throw new ArgumentException("Appointment start time must be before end time.");
            }

            if (appointment.StartTime < DateTime.Now)
            {
                throw new ArgumentException("Appointment cannot be in the past.");
            }

            appointments.Add(appointment);
        }

        public void RemoveAppointment(Appointment appointment)
        {
            var existing = appointments.FirstOrDefault(a => a.Id == appointment.Id);
            if (existing != null)
            {
                appointments.Remove(existing);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Appointment not found");
            }
        }

        public void DeletePatientAppointments(Guid patientId)
        {
            appointments.RemoveAll(a => a.Patient.Id == patientId);
        }

        public void DeletePhysicianAppointments(Guid physicianId)
        {
            appointments.RemoveAll(a => a.Physician.Id == physicianId);
        }

        public void RescheduleAppointment(Appointment appointment, DateTime newStart, DateTime newEnd)
        {
            if (newStart >= newEnd)
            {
                throw new ArgumentException("Start time must be before end time");
            }

            if (newStart < DateTime.Now)
            {
                throw new ArgumentException("Appointment cannot be in the past");
            }

            var existing = appointments.FirstOrDefault(a => a.Id == appointment.Id);

            if (existing != null)
                        {
                var tempAppointment = new Appointment(newStart, newEnd, existing.Patient, existing.Physician);
                if (IsOverlapping(tempAppointment, appointments.IndexOf(existing)))
                {
                    throw new InvalidOperationException("Physician is already booked during this time.");
                }
                existing.StartTime = newStart;
                existing.EndTime = newEnd;
            }
            else
            {
                throw new Exception("Appointment not found");
            }
        }


        public IEnumerable<Appointment> GetPatientOrPhysicianAppointments(string patientName, string physicianName)
        {
            return appointments.Where(a => a.Patient.Name.Contains(patientName, StringComparison.OrdinalIgnoreCase) || 
            a.Physician.Name.Contains(physicianName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public IEnumerable<Appointment> GetAllAppointments()
        {
            return appointments.ToList();
        }

        public int NumberOfAppointments()
        {
            return appointments.Count;
        }

        private bool IsOverlapping(Appointment newAppointment, int ignoreIndex = -1)
        {
            for (int i = 0; i < appointments.Count; i++)
            {
                if (i == ignoreIndex) continue;

                var existing = appointments[i];
                if (existing.Physician.Id == newAppointment.Physician.Id &&
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
