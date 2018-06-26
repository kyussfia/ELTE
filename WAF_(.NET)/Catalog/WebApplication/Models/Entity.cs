using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Spec { get; set; }

        public DateTime CreatedAt { get; set; }

        public byte[] Picture { get; set; }

        public Boolean isTook { get; set; }

        public virtual int Age
        {
            get
            {
                return DateTime.Now.Subtract(CreatedAt).Days;
            }
        }

        public virtual string AgeString
        {
            get
            {
                var Years = 0;
                var Months = 0;
                var Weeks = 0;
                var Days = 0;
                GetDifference(CreatedAt, DateTime.Now, out Years, out Months, out Weeks, out Days);
                return Years + " years " + Months + " months " + Weeks + " weeks "+ Days + " days";
            }
        }

        public static void GetDifference(DateTime date1, DateTime date2, out int Years, out int Months, out int Weeks, out int Days)
        {
            //assumes date2 is the bigger date for simplicity
            //----------------------------------------------
            //years
            TimeSpan diff = date2 - date1;
            Years = diff.Days / 366;
            DateTime workingDate = date1.AddYears(Years);
            while (workingDate.AddYears(1) <= date2)
            {
                workingDate = workingDate.AddYears(1);
                Years++;
            }
            //---------------------------------------------
            //months
            diff = date2 - workingDate;
            Months = diff.Days / 31;
            workingDate = workingDate.AddMonths(Months);
            while (workingDate.AddMonths(1) <= date2)
            {
                workingDate = workingDate.AddMonths(1);
                Months++;
            }
            //---------------------------------------------
            //weeks and days
            diff = date2 - workingDate;
            Weeks = diff.Days / 7; //weeks always have 7 days
            Days = diff.Days % 7;
            //---------------------------------------------
        }
    }
}
