using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareManagementApp.Models
{
    public class Patient
    {

        public Guid Id { get; private set; }
        private string? name;
        private string? address;
        private DateOnly birthday;
        private string? race;
        private string? gender;
        private string? medicalNotes;


        public Patient(string? _name, string? _address, DateOnly _birthday, string? _race, string? _gender, string? _medical_notes)
        {
            Id = Guid.NewGuid(); 
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
                    throw new ArgumentException("Please enter a name");
                name = value;

            }
        }

        public string? Address
        {
            get { return address; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Please enter an address");
                address = value;

            }
        }

        public DateOnly Birthday
        {
            get { return birthday; }
            set
            {
                if (value > DateOnly.FromDateTime(DateTime.Today))
                    throw new ArgumentException("Birthday cannot be in the future");
                birthday = value;

            }
        }

        public string? Race
        {
            get { return race; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Please enter a race/ethnicity");
                race = value;
            }
        }

        public string? Gender
        {
            get { return gender; }
            set
            {
                if (value == "Male" || value == "Female")

                    gender = value;
                else
                    throw new ArgumentException("Gender must be Male or Female");
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
            return this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"Name: {Name} \nAddress: {Address} \nBirthday: {Birthday}\nRace: {Race}\nGender: {Gender}\nNotes: {MedicalNotes}";
        }
    }
}
