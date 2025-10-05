using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareManagementApp.Models
{
    public class Appointment
    {
        private DateTime startTime;
        private DateTime endTime;
        private Patient patient;
        private Physician physician;

        public DateTime StartTime
        {
            get { return startTime; }
            set
            {
                if (IsCorrectTime(value))
                    startTime = value;
                else
                    throw new ArgumentException("Appointment start time is invalid!");
            }

        }

        public DateTime EndTime
        {
            get { return endTime; }
            set
            {
                if (IsCorrectTime(value))
                    endTime = value;
                else
                    throw new ArgumentException("Appointment end time is invalid!");
            }
        }

        public Patient Patient
        {
            get { return patient; }
        }

        public Physician Physician
        {
            get { return physician; }
        }

        public Appointment(DateTime startTime, DateTime endTime, Patient patient, Physician physician)
        {
            if (startTime < endTime)
            {
                StartTime = startTime;
                EndTime = endTime;
                this.patient = patient;
                this.physician = physician;
            }
            else
            {
                throw new ArgumentException("Appointment start/end time is invalid!");
            }
        }

        public override string ToString()
        {
            return $"Doctor: {physician.Name}\n" +
                   $"Patient: {patient.Name}\n" +
                   $"On {startTime:MM/dd/yyyy} from {startTime:hh:mm tt} to {endTime:hh:mm tt}";
        }


        public static bool IsCorrectTime(DateTime date)
        {
            if (date.DayOfWeek < DayOfWeek.Monday || date.DayOfWeek > DayOfWeek.Friday)
                return false;

            TimeSpan time = date.TimeOfDay;

            TimeSpan start = new TimeSpan(8, 0, 0);  // 8:00 AM
            TimeSpan end = new TimeSpan(17, 0, 0); // 5:00 PM

            return time >= start && time < end;
        }

    }

}
