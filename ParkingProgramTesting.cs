using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ParkingProgramUnitTesting
{
   

    [TestClass]
    public class ParkingProgramTesting
    {
        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }



        public VehicleTracker vt;
        public Vehicle vehicle;
        public Vehicle vehicle2;
        public Vehicle vehicle3;
        public int initialCapacity = 20;

       

        [TestInitialize]
        public void TestInitialize()
        {
            vt = new VehicleTracker(initialCapacity, "1330 Keananston Rd");
            vehicle = new Vehicle("sitight",true);
            vehicle2 = new Vehicle("saltbae", true);
            vehicle3 = new Vehicle("tesla", false);
        }

        public int AvailableSlots()
        {
            int count = 0;
            int availableSlots = 0;
            foreach ( KeyValuePair<int, Vehicle> slot in vt.VehicleList )
            {
                if ( slot.Value != null )
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            availableSlots = initialCapacity - count;
            return availableSlots;
        }

        [TestMethod]
        public void GenerateSlotsTesting()
        {
            //act and assert
            Assert.IsFalse(vt.VehicleList.ContainsKey(0));
            Assert.IsTrue(vt.VehicleList.ContainsKey(1));

        }


        [TestMethod]
        public void AddVehicleTesting()
        {

            //act
            vt.AddVehicle(vehicle);
            vt.AddVehicle(vehicle2);
           
            int availableSlots = AvailableSlots();
            //assert slots available should equal to capacity substract total number of parked cars
            Assert.AreEqual(vt.SlotsAvailable, availableSlots);

        }

        [TestMethod]
        public void RemoveVehicleLicenseTesting()
        {
            //arrange
            // Vehicle vehicle2 = new Vehicle("saltbae", true);
            //save the available slots when initialized slots
            int currentAvailableSlots = vt.SlotsAvailable;

            //act 
            vt.AddVehicle(vehicle);    
            vt.RemoveVehicle(vehicle.Licence);

            //assert
            Assert.AreEqual(vt.SlotsAvailable, currentAvailableSlots);
        }

        [TestMethod]
        public void RemoveVehicleSlotNumberTesting()
        {
            //arrange
            //save the available slots when initialized slots
            int AvailableSlots = vt.SlotsAvailable;

            //act 
            vt.AddVehicle(vehicle2);
            vt.AddVehicle(vehicle3);
            bool result = vt.RemoveVehicle(1);

            //assert
            Assert.IsTrue(result);
            Assert.AreEqual(vt.SlotsAvailable, 29);
        }

        [TestMethod]
        public void AvailableSlotsTesting()
        {

            //act
            vt.AddVehicle(vehicle);
            vt.AddVehicle(vehicle2);
            vt.AddVehicle(vehicle3);
            vt.RemoveVehicle(3);
            vt.RemoveVehicle(1);

            //assert
            Assert.AreEqual(vt.SlotsAvailable, AvailableSlots());
        }


        [TestMethod]
        public void ParkedPassholdersTesting()
        {

            //act
            vt.AddVehicle(vehicle);
            vt.AddVehicle(vehicle2);
            vt.AddVehicle(vehicle3);
            List<Vehicle> vehicles =  vt.ParkedPassholders();

            //assert
            Assert.IsTrue( vehicles.Count == 2 );
        }

        [TestMethod]
        public void PassholderPercentageTesting()
        {
            //act
            vt.AddVehicle(vehicle2);
            vt.AddVehicle(vehicle3);
            double result = vt.PassholderPercentage();
            string strResult = result.ToString();
            TestContext.Write("result: "+ strResult);
            //assert
            Assert.AreEqual(vt.PassholderPercentage(),50);
        }
    }
}