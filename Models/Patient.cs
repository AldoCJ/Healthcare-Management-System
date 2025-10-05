using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareManagementApp.Models
{
    public class Patient
    {
        private string? name;
        private string? address;
        private DateOnly birthday;
        private string? race;
        private string? gender;
        private string? medicalNotes;

        public Patient(string? _name, string? _address, DateOnly _birthday, string? _race, string? _gender, string? _medical_notes)
        {
            Name = _name;
            Address = _address;
            Birthday = _birthday;
            Race = _race;
            Gender = _gender;
            MedicalNotes = _medical_notes;
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

        public string? Address
        {
            get { return address; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Address cannot be empty!");
                address = value;

            }
        }

        public DateOnly Birthday
        {
            get { return birthday; }
            set
            {
                if (value > DateOnly.FromDateTime(DateTime.Today))
                    throw new ArgumentNullException("Birthday cannot be in the future!");
                birthday = value;

            }
        }

        public string? Race
        {
            get { return race; }
            set { race = value; }
        }

        public string? Gender
        {
            get { return gender; }
            set
            {
                if (value == "m" || value == "f" || value == "M" || value == "F")

                    gender = value.ToUpper();
                else
                    throw new ArgumentException("Gender must be 'M' or 'F'");
            }
        }

        public string? MedicalNotes
        {
            get { return medicalNotes; }
            set { medicalNotes = value; }
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Patient other)
                return false;
            return string.Equals(this.Name, other.Name, StringComparison.OrdinalIgnoreCase) && (this.Birthday == other.Birthday);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name?.ToLower(), Birthday);
        }

        public override string ToString()
        {
            return $"Name: {Name} \nAddress: {Address} \nBirthday: {Birthday}\nRace: {Race}\nGender: {Gender}\nNotes: {MedicalNotes}";
        }
    }
}
