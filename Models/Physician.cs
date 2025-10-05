using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareManagementApp.Models
{
    public class Physician
    {
        private string? name;
        private string? licenseNumber;
        private DateOnly gradDate;
        private string? specializations;

        public Physician(string? name, string? licenseNumber, DateOnly gradDate, string? specializations)
        {
            Name = name;
            LicenseNumber = licenseNumber;
            GradDate = gradDate;
            Specializations = specializations;
        }

        public string? Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Name cannot be empty!");
                name = value;

            }
        }
        public string? LicenseNumber
        {
            get { return licenseNumber; }
            set { licenseNumber = value; }
        }

        public DateOnly GradDate
        {
            get { return gradDate; }
            set
            {
                if (value > DateOnly.FromDateTime(DateTime.Today))
                    throw new ArgumentException("Graduation date cannot be in the future!");
                gradDate = value;
            }
        }
        public string? Specializations
        {
            get { return specializations; }
            set { specializations = value; }
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Physician other)
                return false;
            return (licenseNumber == other.licenseNumber);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(licenseNumber);
        }

        public override string ToString()
        {
            return $"Name: {Name}\nLicense number: {LicenseNumber}\nGraduation date: {GradDate}\nSpecilizations: {Specializations}";
        }


    }

}
