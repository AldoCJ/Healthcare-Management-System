using System;
using System.Collections.Generic;
using System.Linq;
using HealthcareManagementApp.Models;

namespace HealthcareManagementApp.Services
{
    public class PatientService
    {
        private readonly List<Patient> patients = new();
        private readonly object _lock = new();

        public PatientService()
        {
            // Initialize with some sample data
            patients.Add(new Patient("John Doe", "123 Main St", new DateOnly(1980, 5, 15), "Caucasian", "Male", "No notes"));
            patients.Add(new Patient("Alice Smith", "123 Main St", new DateOnly(1990, 5, 12), "Caucasian", "Female", "No notes"));
            patients.Add(new Patient("Bob Jones", "456 Oak Ave", new DateOnly(1985, 3, 8), "African American", "Male", "Diabetic"));
            patients.Add(new Patient("Charlie Brown", "789 Pine Rd", new DateOnly(1978, 11, 22), "Hispanic", "Male", "Allergies"));
        }

        public void AddPatient(Patient patient)
        {
            if (patient == null)
                throw new ArgumentNullException("Patient cannot be null!");
            lock (_lock)
            {
                patients.Add(patient);
            }
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            lock (_lock)
            {
                // Return a copy to avoid exposing internal list
                return patients.ToList();
            }
        }

        public IEnumerable<Patient> GetPatientsByName(string name)
        {
            lock (_lock)
            {
                return patients
                    .Where(p => !string.IsNullOrWhiteSpace(p.Name) && p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
        }

        public void UpdatePatient(Patient updatedPatient)
        {
            lock (_lock)
            {
                var existing = patients.FirstOrDefault(p => p.Id == updatedPatient.Id);
                if (existing == null)
                    throw new KeyNotFoundException("Patient not found.");

                existing.Name = updatedPatient.Name;
                existing.Address = updatedPatient.Address;
                existing.Birthday = updatedPatient.Birthday;
                existing.Gender = updatedPatient.Gender;
                existing.Race = updatedPatient.Race;
                existing.MedicalNotes = updatedPatient.MedicalNotes;
            }
        }

        public void DeletePatient(Patient patient)
        {
            lock (_lock)
            {
                if (!patients.Remove(patient))
                    throw new KeyNotFoundException("Patient not found.");
            }
        }
    }
}
