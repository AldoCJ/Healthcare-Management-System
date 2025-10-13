using System;
using System.Collections.Generic;
using System.Linq;
using HealthcareManagementApp.Models;

namespace HealthcareManagementApp.Services
{
    public class PhysicianService
    {
        private readonly List<Physician> physicians = new();
        private readonly object _lock = new();

        public PhysicianService()
        {
            // Initialize with some sample data
            physicians.Add(new Physician("Alex Johnson", "LIC12345", new DateOnly(2005, 6, 15), "Cardiology"));
            physicians.Add(new Physician("Allison Smith", "LIC67890", new DateOnly(2010, 5, 12), "Pediatrics"));
            physicians.Add(new Physician("Robert Jones", "LIC54321", new DateOnly(2000, 3, 8), "Neurology"));
            physicians.Add(new Physician("Eren Brown", "LIC98765", new DateOnly(1995, 11, 22), "Orthopedics"));

        }

        public void AddPhysician(Physician physician)
        {
            if (physician == null)
                throw new ArgumentNullException("Physician cannot be null!");
            lock (_lock)
            {
                physicians.Add(physician);
            }
        }

        public IEnumerable<Physician> GetAllPhysicians()
        {
            lock (_lock)
            {
                // Return a copy to avoid exposing internal list
                return physicians.ToList();
            }
        }

        public IEnumerable<Physician> GetPhysiciansByName(string name)
        {
            lock (_lock)
            {
                return physicians
                    .Where(p => !string.IsNullOrWhiteSpace(p.Name) && p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
        }

        public void UpdatePhysician(Physician updatedPhysician)
        {
            lock (_lock)
            {
                var existing = physicians.FirstOrDefault(p => p.Id == updatedPhysician.Id);
                if (existing == null)
                    throw new KeyNotFoundException("Physician not found.");

                existing.Name = updatedPhysician.Name;
                existing.LicenseNumber = updatedPhysician.LicenseNumber;
                existing.GradDate = updatedPhysician.GradDate;
                existing.Specializations = updatedPhysician.Specializations;
            }
        }

        public void DeletePhysician(Physician physician)
        {
            lock (_lock)
            {
                if (!physicians.Remove(physician))
                    throw new KeyNotFoundException("Physician not found.");
            }
        }
    }
}
