using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lombardia.Classes
{
    class Contract
    {
        /// <summary>
        /// Պայմանագրի համարը
        /// </summary>
        public string number;
        /// <summary>
        /// Պայմանագրի բացման ամսաթիվը 
        /// </summary>
        public string startDate;
        /// <summary>
        /// Պայմանագրի ավարտման ամսաթիվը
        /// </summary>
        public string endDate;
        /// <summary>
        /// Տրամադրված գումարի չափը
        /// </summary>
        public double amountProvided;
        /// <summary>
        /// Գույքի գնահատված արժեքի չափը
        /// </summary>
        public double amountEstimated;
        /// <summary>
        /// Տարեկան տոկոսի չափը
        /// </summary>
        public double percent;
        /// <summary>
        /// Տարեկան փաստացի տոկոսադրույքը
        /// </summary>
        public double percentLegal;
        /// <summary>
        /// Տոկոսագումարը
        /// </summary>
        public double amountPercent;
        /// <summary>
        /// Վճարման ենթակա ընդհանուր գումարը
        /// </summary>
        public double amountExpected;
        /// <summary>
        /// Տարեկան տոկոսադրույքը` պայմանագրի ժամկետը լրանալուց հետո
        /// </summary>
        public double percentAdded;

        public Contract()
        {
        }

        public Contract(string cNumber, double cAmountProvided, double cAmountEstimated, double cPercent)
        {
            number = cNumber;
            startDate = System.DateTime.Now.ToShortDateString();
            endDate = System.DateTime.Now.AddMonths(1).ToShortDateString();
            amountProvided = cAmountProvided;
            amountEstimated = cAmountEstimated;
            percent = cPercent;
            amountPercent = getPercentAmount(amountProvided, percent);
            percentLegal = getPercentLegal(amountProvided, amountPercent);
            amountExpected = amountProvided + amountPercent;
            percentAdded = 1.2 * cPercent;
        }

        private double getPercentLegal(double Amount, double PercentAmount)
        {
            int daysInMonth = System.DateTime.DaysInMonth(System.DateTime.Now.Year, System.DateTime.Now.Month);
            int daysInYear = (new DateTime(System.DateTime.Now.Year, 12, 31)).DayOfYear;
            double val = 100 * Math.Pow((Amount + PercentAmount) / Amount, daysInYear / daysInMonth) - 100;
            val = Math.Round(val, 1);
            return val;
        }

        private double getPercentAmount(double Amount, double Percent)
        {
            int daysInMonth = System.DateTime.DaysInMonth(System.DateTime.Now.Year, System.DateTime.Now.Month);
            int daysInYear = (new DateTime(System.DateTime.Now.Year, 12, 31)).DayOfYear;
            double val = (Amount * Percent * daysInMonth) / daysInYear;
            val = Math.Round(val / 10) * 10;
            return val;
        }
        
    }
}
