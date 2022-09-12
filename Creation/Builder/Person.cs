using System;
using System.Collections.Generic;
using System.Text;

namespace designs.Creation.Builder
{
    public class Person
    {
        // address
        public string StreetAddress, Postcode, City;
        // employment info
        public string CompanyName, Position;
        public int AnnualIncome;
    }

    public class PersonBuilder
    {
        // the object we're going to build
        protected Person person; // this is a reference!
        public PersonBuilder() => person = new Person();
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);
        public PersonJobBuilder Works => new PersonJobBuilder(person);

        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }
    }

    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person) 
        {
            this.person = person;
        }
        public PersonJobBuilder At(string company)
        {
            person.CompanyName = company;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            this.person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amt)
        {
            this.person.AnnualIncome = amt;
            return this;
        }
    }


    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }
        public PersonAddressBuilder At(string streetAddress)
        {
            person.StreetAddress = streetAddress;
            return this;
        }
        public PersonAddressBuilder WithPostcode(string postcode)
        {
            person.Postcode = postcode;
            return this;
        }
        public PersonAddressBuilder In(string city)
        {
            person.City = city;
            return this;
        }
    }

    [TestCase]
    public class PersonTest : ITest
    {
        public void Run()
        {
            var pb = new PersonBuilder();
            Person person = pb
                .Lives
            .At("123 London Road")
            .In("London")
            .WithPostcode("SW12BC")
            .Works
            .At("Fabrikam")
            .AsA("Engineer")
            .Earning(123000);

            Console.WriteLine(person);
        }
    }
}
