using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VdoValley.Web.ViewModels
{
    public class VideosSearchFilter
    {
        public int VideoId { get; set; }
        public TextFilter Title { get; set; }
        public DateRangeFilter DateCreated { get; set; }
    }

    public class TextFilter
    {
        public string Text = string.Empty;
        public string Operation = string.Empty;

        private void ClearFilter()
        {
            this.Text = string.Empty;
            this.Operation = string.Empty;
        }

        public void StartsWith(string Text)
        {
            this.ClearFilter();
            this.Text = Text;
            this.Operation = "STARTS_WITH";
        }

        public void EndsWith(string Text)
        {
            this.ClearFilter();
            this.Text = Text;
            this.Operation = "ENDS_WITH";
        }

        public void Is(string Text)
        {
            this.ClearFilter();
            this.Text = Text;
            this.Operation = "IS";
        }
    }

    public class DateRangeFilter
    {
        public DateTime Date1 = DateTime.MinValue;
        public DateTime Date2 = DateTime.MinValue;
        public string Operation = string.Empty;

        private void ClearFilter()
        {
            this.Date1 = DateTime.MinValue;
            this.Date2 = DateTime.MinValue;
            this.Operation = string.Empty;
        }

        public void GreaterThan(DateTime DateTime)
        {
            this.ClearFilter();
            this.Date1 = DateTime;
            this.Operation = "GREATER_THAN";
        }

        public void LessThan(DateTime DateTime)
        {
            this.ClearFilter();
            this.Date1 = DateTime;
            this.Operation = "LESS_THAN";        
        }

        public void EqualTo(DateTime DateTime)
        {
            this.ClearFilter();
            this.Date1 = DateTime;
            this.Operation = "EQUAL_TO";
        }

        public void Between(DateTime DateFrom, DateTime DateTo)
        {
            this.ClearFilter();
            this.Date1 = DateFrom;
            this.Date2 = DateTo;
            this.Operation = "BETWEEN";
        }
    }

    public class AmountRangeFilter
    {
        public int Amount1 = 0;
        public int Amount2 = 0;
        public string Operation = string.Empty;

        private void ClearFilter()
        {
            this.Amount1 = 0;
            this.Amount2 = 0;
            this.Operation = string.Empty;
        }

        public void GreaterThan(int Amount)
        {
            this.ClearFilter();
            this.Amount1 = Amount;
            this.Operation = "GREATER_THAN";        
        }

        public void LessThan(int Amount)
        {
            this.ClearFilter();
            this.Amount1 = Amount;
            this.Operation = "LESS_THAN";        
        }

        public void EqualTo(int Amount)
        {
            this.ClearFilter();
            this.Amount1 = Amount;
            this.Operation = "EQUAL_TO";        
        }

        public void Between(int AmountFrom, int AmountTo)
        {
            this.ClearFilter();
            this.Amount1 = AmountFrom;
            this.Amount2 = AmountTo;
            this.Operation = "BETWEEN";
        }
    }
}