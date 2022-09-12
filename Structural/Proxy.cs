using System;
using System.Collections.Generic;
using System.Text;

namespace designs.Structural
{
    public class Proxy
    {
        public interface ICar
        {
            void Drive();
        }

        public class Car : ICar
        {
            public void Drive()
            {
                Console.WriteLine("Car is driving");
            }
        }
        public class Driver
        {
            public int Age { get; set; }
        }

        /// <summary>
        /// </summary>
        public class ProxyDriver : ICar
        {
            private Driver Driver { get; }
            public ProxyDriver(Driver driver)
            {
                Driver = driver;
            }
            public void Drive()
            {
                if (Driver.Age > 16)
                    Console.WriteLine("Car is driving");
                else
                    throw new Exception("Car does not have a valid driver");
            }
        }
    }

}
