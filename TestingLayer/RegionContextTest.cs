using BusinessLayer;
using DataLayer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestingLayer
{
    [TestFixture]
    public class RegionContextTest
    {
        private RegionsContext context = new RegionsContext(SetupFixture.dbContext);
        private Region region;
        private User u1, u2;
        private Interest i1, i2;

        [SetUp]
        public void CreateRegion()
        {
            region = new Region("Chess");

            i1 = new Interest("Sicilian Deffence");
            i2 = new Interest("Queen's Gambit");

            u1 = new User("Todor", "Demirov", 72, "toshee", "kolednichukmops722", "t.k.demirov@gmail.com", region);
            u2 = new User("Maria", "Petrova", 21, "mariika62", "obichamtoshko123", "mariyapetrova_zh19@schoolmath.eu", region);

            region.Interests.Add(i1);
            region.Interests.Add(i2);

            region.Users.Add(u1);
            region.Users.Add(u2);
            context.Create(region);
        }
        [TearDown]
        public void DropRegion()
        {
            foreach (Region item in SetupFixture.dbContext.Regions)
            {
                SetupFixture.dbContext.Regions.Remove(item);
            }

            SetupFixture.dbContext.SaveChanges();
        }
        [Test]
        public void Create()
        {
            Region newRegion = new Region("Toshko");

            int regionsBefore = SetupFixture.dbContext.Regions.Count();
            context.Create(newRegion);

            int regionsAfter = SetupFixture.dbContext.Regions.Count();
            Assert.IsTrue(regionsBefore + 1 == regionsAfter, "Create() does not work!");
        }
        [Test]
        public void Read()
        {
            Region readRegion = context.Read(region.Id);

            Assert.AreEqual(region, readRegion, "Read does not return the same object!");
        }

        [Test]
        public void ReadWithNavigationalProperties()
        {
            Region readRegion = context.Read(region.Id, true);

            Assert.That(readRegion.Interests.Contains(i1)
                && readRegion.Interests.Contains(i2)
                && readRegion.Users.Contains(u1)
                && readRegion.Users.Contains(u2),
                "I1 and I2 are not in the Interests list and U1 and U2 are not in the users list!");
        }
        [Test]
        public void ReadAll()
        {
            List<Region> regions = (List<Region>)context.ReadAll();

            Assert.That(regions.Count != 0, "ReadAll() does not return regions!");
        }
        [Test]
        public void Update()
        {
            Region changedRegion = context.Read(region.Id);

            changedRegion.Name = "Updated " + region.Name;

            context.Update(changedRegion);

            region = context.Read(region.Id);

            Assert.AreEqual(changedRegion, region, "Update() does not work!");
        }
        [Test]
        public void Delete()
        {
            int regionsBefore = SetupFixture.dbContext.Regions.Count();

            context.Delete(region.Id);
            int regionsAfter = SetupFixture.dbContext.Regions.Count();

            Assert.IsTrue(regionsBefore - 1 == regionsAfter, "Delete() does not work!");
        }
    }
}
