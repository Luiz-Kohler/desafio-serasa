﻿using Domain.Validators;
using Domain.Validators.ExtensionsValidators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; private set; }
        public int Reliability { get; private set; }

        public virtual List<Invoice> Invoices { get; private set; }
        public virtual List<Debit> Debits { get; private set; }

        public Company()
        {

        }

        public Company(string name)
        {
            this.Name = name;
            this.Reliability = 50;
            this.Invoices = new List<Invoice>();
            this.Debits = new List<Debit>();
        }

        public void CalculateReliability()
        {
            CalculateInvoices();
            CalculateDebits();

            if (Reliability > 100)
                Reliability = 100;

            if (Reliability < 1)
                Reliability = 1;
        }

        private void CalculateInvoices()
        {
            for (int i = 0; i < Invoices.Count; i++)
            {
                var valueToAdd = Reliability * 0.02;
                var reliability = Reliability + valueToAdd;
                Reliability = (int)Math.Floor(reliability);
            }
        }

        private void CalculateDebits()
        {
            for (int i = 0; i < Debits.Count; i++)
            {
                var valueToRemove = Reliability * 0.04;
                var reliability = Reliability - valueToRemove;
                Reliability = (int)Math.Ceiling(reliability);
            }
        }

        public void FormatProps()
        {
            this.Name = Name?.RemoveExtraWhiteSpaces();
        }

        public override HashSet<string> GetErrors()
        {
            return new CompanyValidator().CustomValidate(this);
        }

        public override bool IsValid()
        {
            return !GetErrors().Any();
        }
    }
}
